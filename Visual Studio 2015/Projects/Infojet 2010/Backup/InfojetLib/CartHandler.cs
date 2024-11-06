using System;
using System.Data;
using System.Collections;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for CartHandler.
	/// </summary>
	public class CartHandler
	{
		private Database database;
		private Infojet infojetContext;

		private string sessionId;

        private float totalCartAmount;
        private float totalCartQuantity;
        private float totalCartVatAmount;

		public CartHandler(Database database, string sessionId, Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.infojetContext = infojetContext;
			this.sessionId = sessionId;

    	}


		public void clearCartInfo()
		{
			try
			{
				database.nonQuery("DELETE FROM ["+database.getTableName("Web Cart Header")+"] WHERE [Session ID] = '"+infojetContext.sessionId+"'");
			}
			catch(Exception)
			{}

		}

		public void emptyCart()
		{
			database.nonQuery("DELETE FROM ["+database.getTableName("Web Cart Line")+"] WHERE [Session ID] = '"+infojetContext.sessionId+"'");

		}

        public void removeItemFromCart()
        {
            int entryNo = 0;
            try
            {
                entryNo = int.Parse(System.Web.HttpContext.Current.Request["entryNo"]);
            }
            catch (Exception) { }

            if (entryNo > 0) removeItemFromCart(entryNo);
        }

        public void removeItemFromCart(int entryNo)
        {
            DatabaseQuery databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo AND [Web User Account No] = @webUserAccountNo");
            databaseQuery.addIntParameter("entryNo", entryNo);
            databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);

            databaseQuery.execute();
        }


		public void setCartOwner(string webUserAccountNo)
		{
            database.nonQuery("UPDATE [" + database.getTableName("Web Cart Line") + "] SET [Web User Account No] = '"+webUserAccountNo+"' WHERE [Session ID] = '" + infojetContext.sessionId + "' AND [Web User Account No] = ''");			
		}

        public void updateSession()
        {
            database.nonQuery("UPDATE [" + database.getTableName("Web Cart Line") + "] SET [Session ID] = '" + infojetContext.sessionId + "' WHERE [Web User Account No] = '"+infojetContext.userSession.webUserAccount.no+"'");
        }

		public void handleRequests()
		{
			if (System.Web.HttpContext.Current.Request["cartCommand"] == "add") addItemToCart();
            if (System.Web.HttpContext.Current.Request["cartCommand"] == "remove") removeItemFromCart();
            if (System.Web.HttpContext.Current.Request["cartCommand"] == "clear") emptyCart();

            if (System.Web.HttpContext.Current.Request["cartCommand"] != null) infojetContext.redirect(infojetContext.webPage.getUrl() + "&category=" + System.Web.HttpContext.Current.Request["category"]);
		}

		private void addItemToCart()
		{
			string itemNo = System.Web.HttpContext.Current.Request["itemNo"];
			string quantityStr = System.Web.HttpContext.Current.Request["quantity"];
			
			bool allowDecimalQuantity = true;
			if (System.Web.HttpContext.Current.Request["allowDecimalQuantity"] != "true") allowDecimalQuantity = false;

			string extra1 = "";
			string extra2 = "";
			string extra3 = "";
			string extra4 = "";
			string extra5 = "";

			if (System.Web.HttpContext.Current.Request["extra1"] != null) extra1 = System.Web.HttpContext.Current.Request["extra1"];
			if (System.Web.HttpContext.Current.Request["extra2"] != null) extra2 = System.Web.HttpContext.Current.Request["extra2"];
			if (System.Web.HttpContext.Current.Request["extra3"] != null) extra3 = System.Web.HttpContext.Current.Request["extra3"];
			if (System.Web.HttpContext.Current.Request["extra4"] != null) extra4 = System.Web.HttpContext.Current.Request["extra4"];
			if (System.Web.HttpContext.Current.Request["extra5"] != null) extra5 = System.Web.HttpContext.Current.Request["extra5"];

			this.addItemToCart(itemNo, quantityStr, allowDecimalQuantity, extra1, extra2, extra3, extra4, extra5);

		}

		public bool addItemToCart(string itemNo, string quantityStr, bool allowDecimalQuantity, string extra1, string extra2, string extra3, string extra4, string extra5)
		{
			return this.addItemToCart(itemNo, quantityStr, allowDecimalQuantity, extra1, extra2, extra3, extra4, extra5, "");
		}

        public bool addItemToCart(string itemNo, string quantityStr, bool allowDecimalQuantity, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo)
        {
            WebUserAccount webUserAccount = null;
            if (infojetContext.userSession != null) webUserAccount = infojetContext.userSession.webUserAccount;
            return this.addItemToCart(itemNo, quantityStr, allowDecimalQuantity, extra1, extra2, extra3, extra4, extra5, "", webUserAccount);
        }

		public bool addItemToCart(string itemNo, string quantityStr, bool allowDecimalQuantity, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, WebUserAccount webUserAccount)
		{
            if (infojetContext.userSession != null)
            {
                if (webUserAccount == null) webUserAccount = infojetContext.userSession.webUserAccount;
            }
			
			float totalCartAmount = getTotalCartAmount();
			float totalCartQuantity = getTotalCartQuantity();
		
			if (itemNo != null)
			{
				Item item = new Item(database, itemNo);

				if (System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",") quantityStr = quantityStr.Replace(".", ",");
				if (System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".") quantityStr = quantityStr.Replace(",", ".");

				float decimalQuantity = 0;
				float quantity = 0;

				try
				{
					decimalQuantity = float.Parse(quantityStr, System.Globalization.CultureInfo.InvariantCulture);
				}
				catch(Exception)
				{
					decimalQuantity = 1;
				}

				bool separateLines = true;

				if (!allowDecimalQuantity)
				{
					quantity = (float)((int)decimalQuantity);
					separateLines = false;

				}
			
				WebSiteUnit webSiteUnit = new WebSiteUnit(database, infojetContext.webSite.code, item.salesUnitOfMeasure);
				if (webSiteUnit.allowDecimalQuantity)
				{
					quantity = decimalQuantity;
					separateLines = true;
				}


				

				Items items = new Items();
				Hashtable itemInfoTable = items.getItemInfo(item, infojetContext, quantity, true, false);

				bool addItem = true;

				//Check User Account Limits
				if (webUserAccount != null)
				{
					if (webUserAccount.maxBuyType == 0)
					{
						if ((webUserAccount.maxBuyLimitPerOrder != 0) && (totalCartAmount + (((ItemInfo)itemInfoTable[item.no]).unitPrice * quantity)) > webUserAccount.maxBuyLimitPerOrder)
						{
							addItem = false;
						}
					}
					if (webUserAccount.maxBuyType == 1)
					{
						if ((webUserAccount.maxBuyLimitPerOrder != 0) && (totalCartQuantity + quantity) > webUserAccount.maxBuyLimitPerOrder)
						{
							addItem = false;
						}
					}
				}

				if (addItem)
				{

					WebCartLines webCartLines = new WebCartLines(database);

                    DataSet webCartLineDataSet;
                    if (webUserAccount == null)
                    {
                        webCartLineDataSet = webCartLines.getCartLines(sessionId, item.no, extra1, extra2, extra3, extra4, extra5, referenceNo);
                    }
                    else
                    {
                        webCartLineDataSet = webCartLines.getWebUserAccountCartLines(webUserAccount.no, item.no, extra1, extra2, extra3, extra4, extra5, referenceNo);
                    }

					if ((webCartLineDataSet.Tables[0].Rows.Count > 0) && (!separateLines))
					{
                        
                        WebCartLine webCartLine = new WebCartLine(infojetContext, webCartLineDataSet.Tables[0].Rows[0]);
						webCartLine.quantity = webCartLine.quantity + quantity;
					
						if (itemInfoTable.Contains(item.no))
						{
							webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
						}
						else
						{
							SalesPrices salesPrices = new SalesPrices(database, infojetContext);
							SalesPrice salesPrice = salesPrices.getItemPrice(item, infojetContext.userSession, infojetContext.currencyCode);		
							webCartLine.unitPrice = salesPrice.unitPrice;
						}


						webCartLine.save();

					}
					else
					{

                        WebCartLine webCartLine = new WebCartLine(infojetContext, infojetContext.sessionId, item);
						webCartLine.quantity = quantity;
						webCartLine.extra1 = extra1;
						webCartLine.extra2 = extra2;
						webCartLine.extra3 = extra3;
						webCartLine.extra4 = extra4;
						webCartLine.extra5 = extra5;
						webCartLine.referenceNo = referenceNo;
						if (webUserAccount != null) webCartLine.webUserAccountNo = webUserAccount.no;

						if (itemInfoTable.Contains(item.no))
						{
							webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
						}

						webCartLine.save();

					}

					if (infojetContext.webSite.reserveCart) this.reserveCart();

					return true;
				}
			}

			return false;
		}



		public float getCartQuantity(string itemNo, string extra1, string extra2, string extra3, string extra4, string extra5, string reference)
		{
			WebCartLines webCartLines = new WebCartLines(database);

            DataSet cartDataSet = webCartLines.getCartLines(sessionId, itemNo, extra1, extra2, extra3, extra4, extra5, reference);

			float totalQuantity = 0;

			int i = 0;
			while (i < cartDataSet.Tables[0].Rows.Count)
			{
				totalQuantity = totalQuantity + float.Parse(cartDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString());	

				i++;
			}

			return totalQuantity;

		}

		public void reCalculateCart()
		{
			this.reCalculateCart(infojetContext.userSession.customer.no, infojetContext.currencyCode);
		}

		public void reCalculateCart(string customerNo, string currencyCode)
		{
			WebCartLines webCartLines = new WebCartLines(database);
			DataSet cartDataSet = webCartLines.getCartLines(infojetContext.sessionId);

			DataSet itemDataSet = webCartLines.convertToItemDataSet(cartDataSet);
			

			Items items = new Items();
			Hashtable itemInfoTable = items.getItemInfo(itemDataSet, infojetContext, true, false);

			float totalAmount = 0;
	
			int i = 0;
			while (i < cartDataSet.Tables[0].Rows.Count)
			{
                WebCartLine webCartLine = new WebCartLine(infojetContext, cartDataSet.Tables[0].Rows[i]);

				if (itemInfoTable.Contains(webCartLine.itemNo))
				{
					webCartLine.unitPrice = ((ItemInfo)itemInfoTable[webCartLine.itemNo]).unitPrice;
					webCartLine.save();
				}
				else
				{
					Item item = new Item(database, webCartLine.itemNo);
					SalesPrices salesPrices = new SalesPrices(database, infojetContext);
					SalesPrice salesPrice = salesPrices.getItemPrice(item, infojetContext.userSession, currencyCode);		
					webCartLine.unitPrice = salesPrice.unitPrice;
					webCartLine.save();
				}
				
					
				totalAmount = totalAmount + (webCartLine.unitPrice * webCartLine.quantity);

				i++;
			}


		}

		private void calcCartAmounts()
		{
			WebCartLines webCartLines = new WebCartLines(database);

            DataSet cartDataSet;
            cartDataSet = webCartLines.getCartLines(sessionId);
 
			totalCartAmount = 0;
            totalCartQuantity = 0;
            totalCartVatAmount = 0;
	
			int i = 0;
			while (i < cartDataSet.Tables[0].Rows.Count)
			{
                WebCartLine webCartLine = new WebCartLine(infojetContext, cartDataSet.Tables[0].Rows[i]);

                Item item = new Item(database, webCartLine.itemNo);
                float vatFactor = 25;
                if (infojetContext.userSession != null)
                {
                    vatFactor = item.getVatFactor(infojetContext.userSession.customer);
                }

                totalCartAmount = totalCartAmount + (webCartLine.unitPrice * webCartLine.quantity);
                totalCartVatAmount = totalCartVatAmount + (((webCartLine.unitPrice * webCartLine.quantity) * vatFactor) - (webCartLine.unitPrice * webCartLine.quantity));
                totalCartQuantity = totalCartQuantity + webCartLine.quantity;

				i++;
			}

		}

		public float getTotalCartQuantity()
		{
            if (totalCartQuantity == 0) calcCartAmounts();
            return this.totalCartQuantity;
		}

        public float getTotalCartAmount()
        {
            if (totalCartAmount == 0) calcCartAmounts();
            return this.totalCartAmount;
        }

        public float getTotalCartVatAmount()
        {
            if (totalCartVatAmount == 0) calcCartAmounts();
            return this.totalCartVatAmount;
        }

		public string getFormatedTotalCartAmount()
		{
            if (totalCartAmount == 0) calcCartAmounts();
            return database.formatCurrency(totalCartAmount, infojetContext.presentationCurrencyCode);

		}

		public string renderAddLink(string itemNo, float quantity)
		{
            return infojetContext.webPage.getUrl() + "&category=" + System.Web.HttpContext.Current.Request["category"] + "&cartCommand=add&itemNo=" + itemNo + "&quantity=" + quantity.ToString();
		}

        public string renderRemoveLink(int entryNo)
        {
            return infojetContext.webPage.getUrl() + "&cartCommand=remove&entryNo=" + entryNo.ToString();
        }

		public string renderAddAction(string itemNo)
		{           
            
			return infojetContext.webPage.getUrl()+"&category="+System.Web.HttpContext.Current.Request["category"]+"&cartCommand=add&itemNo="+itemNo;
		}

		public string getCheckoutUrl(WebCart webCart)
		{
			if (webCart.webShopRegisterCode != "")
			{
                WebCheckout webCheckout = new WebCheckout(infojetContext, infojetContext.webSite.code, webCart.webShopRegisterCode);

                if (infojetContext.userSession == null)
                {
                    WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, webCheckout.loginRegistrationWebPageCode);
                    return webPage.getUrl();
                }

                WebUserAccountCustomerRelations webUserAccountCustomerRelations = new WebUserAccountCustomerRelations(infojetContext);
                if ((webUserAccountCustomerRelations.customerRelationsExists()) && (webCheckout.customerSearchWebPageCode != ""))
                {
                    WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, webCheckout.customerSearchWebPageCode);
                    return webPage.getUrl();
                }

                
                if (webCheckout.step1WebPageCode != "")
				{
                    WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, webCheckout.step1WebPageCode);
					return webPage.getUrl();
				}
				else
				{
					return infojetContext.webPage.getUrl();
				}
			}
			else
			{
				return infojetContext.webPage.getUrl();
			}
			
		}

		public void reserveCart()
		{
			if (infojetContext.userSession != null)
			{
                WebCartHeader webCartHeader = new WebCartHeader(infojetContext, infojetContext.sessionId);
				webCartHeader.userAccountNo = infojetContext.userSession.webUserAccount.no;
				webCartHeader.sessionId = infojetContext.sessionId;

				ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "reserveCart", webCartHeader));
				appServerConnection.processRequest();
			}

		}

	}
}
