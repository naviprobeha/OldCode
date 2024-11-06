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
        private float totalCartAmountInclVat;
        private float totalCartQuantity;
        private float totalCartVatAmount;
        private float totalCartGrossWeight;
        private float totalCartVolume;

        private bool forced;

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
            if (infojetContext.userSession != null)
            {
                DatabaseQuery databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo AND [Web User Account No] = @webUserAccountNo");
                databaseQuery.addIntParameter("entryNo", entryNo);
                databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);

                databaseQuery.execute();
            }
            else
            {
                
                DatabaseQuery databaseQuery = database.prepare("DELETE FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Entry No_] = @entryNo AND [Session ID] = @sessionId");
                databaseQuery.addIntParameter("entryNo", entryNo);
                databaseQuery.addStringParameter("sessionId", infojetContext.sessionId, 100);
                databaseQuery.execute();
            }
        }


		public void setCartOwner(string webUserAccountNo)
		{
            database.nonQuery("UPDATE [" + database.getTableName("Web Cart Line") + "] SET [Web User Account No] = '"+webUserAccountNo+"' WHERE [Session ID] = '" + infojetContext.sessionId + "' AND [Web User Account No] = ''");			
		}

        public void updateSession()
        {
            updateSession(infojetContext.userSession.webUserAccount);
        }

        public void updateSession(WebUserAccount webUserAccount)
        {
            database.nonQuery("UPDATE [" + database.getTableName("Web Cart Line") + "] SET [Session ID] = '" + infojetContext.sessionId + "' WHERE [Web User Account No] = '" + webUserAccount.no + "'");
            database.nonQuery("UPDATE [" + database.getTableName("Web Cart Line") + "] SET [Session ID] = '' WHERE [Session ID] = '" + infojetContext.sessionId + "' AND [Web User Account No] <> '" + webUserAccount.no + "'");
        }

        public void deleteUserCartLines()
        {
            database.nonQuery("DELETE FROM [" + database.getTableName("Web Cart Line") + "] WHERE [Session ID] <> '" + infojetContext.sessionId + "' AND [Web User Account No] = '"+infojetContext.userSession.webUserAccount.no+"'");
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
            return this.addItemToCart(itemNo, quantityStr, allowDecimalQuantity, extra1, extra2, extra3, extra4, extra5, "", webUserAccount, DateTime.MinValue, DateTime.MinValue);
        }

        public bool addItemToCart(string itemNo, string quantityStr, bool allowDecimalQuantity, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, WebUserAccount webUserAccount)
        {
            return addItemToCart(itemNo, quantityStr, allowDecimalQuantity, extra1, extra2, extra3, extra4, extra5, referenceNo, webUserAccount, DateTime.MinValue, DateTime.MinValue);
        }

		public bool addItemToCart(string itemNo, string quantityStr, bool allowDecimalQuantity, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, WebUserAccount webUserAccount, DateTime fromDate, DateTime toDate)
        {
            string customerNo = "";
            if (infojetContext.userSession != null) customerNo = infojetContext.userSession.customer.no;

            return addItemToCart(itemNo, quantityStr, allowDecimalQuantity, extra1, extra2, extra3, extra4, extra5, referenceNo, webUserAccount, fromDate, toDate, customerNo, infojetContext.currencyCode);
        }

        public bool addItemToCart(string itemNo, string quantityStr, bool allowDecimalQuantity, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, WebUserAccount webUserAccount, DateTime fromDate, DateTime toDate, WebItemConfigHeader itemConfiguration)
        {
            string customerNo = "";
            if (infojetContext.userSession != null) customerNo = infojetContext.userSession.customer.no;

            return addItemToCart(itemNo, quantityStr, allowDecimalQuantity, extra1, extra2, extra3, extra4, extra5, referenceNo, webUserAccount, fromDate, toDate, customerNo, infojetContext.currencyCode, itemConfiguration);
        }

        public bool addItemToCart(string itemNo, string quantityStr, bool allowDecimalQuantity, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, WebUserAccount webUserAccount, DateTime fromDate, DateTime toDate, string customerNo, string currencyCode)
        {
            return addItemToCart(itemNo, quantityStr, allowDecimalQuantity, extra1, extra2, extra3, extra4, extra5, referenceNo, webUserAccount, fromDate, toDate, customerNo, currencyCode, null);
               
        }

        public bool addItemToCart(string itemNo, string quantityStr, bool allowDecimalQuantity, string extra1, string extra2, string extra3, string extra4, string extra5, string referenceNo, WebUserAccount webUserAccount, DateTime fromDate, DateTime toDate, string customerNo, string currencyCode, WebItemConfigHeader itemConfiguration)
		{
            if (fromDate == DateTime.MinValue) fromDate = new DateTime(1753, 1, 1);
            if (toDate == DateTime.MinValue) toDate = new DateTime(1753, 1, 1);

            if (infojetContext.userSession != null)
            {
                if (webUserAccount == null) webUserAccount = infojetContext.userSession.webUserAccount;
            }

			float totalCartAmount = getTotalCartAmount();
			float totalCartQuantity = getTotalCartQuantity();
		
			if (itemNo != null)
			{
				//Item item = new Item(infojetContext, itemNo);
                Item item = Item.get(infojetContext, itemNo);

                quantityStr = quantityStr.Replace(",", ".");

                float decimalQuantity = 0;
                float quantity = 0;

                try
                {
                    decimalQuantity = float.Parse(quantityStr, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    decimalQuantity = 1;
                }

                if (itemConfiguration == null) itemConfiguration = WebItemConfigHeader.getConfiguration(infojetContext, itemNo);
                if (itemConfiguration != null)
                {
                    if (itemConfiguration.itemNo == itemNo)
                    {

                        itemConfiguration.validateConfiguration(decimalQuantity);
                        if ((itemConfiguration.validated))
                        {
                            referenceNo = itemConfiguration.getReference();
                            extra5 = itemConfiguration.webConfigId;
                        }
                        if ((!itemConfiguration.validated) && (itemConfiguration.required)) return false;
                    }
                }

                if ((System.Web.HttpContext.Current.Request["cartEntryNo"] != null) && (System.Web.HttpContext.Current.Request["cartEntryNo"] != ""))
                {
                    //Update existing... Remove old first.
                    WebCartLine oldWebCartLine = new WebCartLine(infojetContext, int.Parse(System.Web.HttpContext.Current.Request["cartEntryNo"]));
                    oldWebCartLine.delete();
                }


				//if (System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",") quantityStr = quantityStr.Replace(".", ",");
				//if (System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".") quantityStr = quantityStr.Replace(",", ".");

				bool separateLines = true;

				quantity = (float)((int)decimalQuantity);
				separateLines = false;
			
				WebSiteUnit webSiteUnit = new WebSiteUnit(database, infojetContext.webSite.code, item.salesUnitOfMeasure);
				if ((webSiteUnit.allowDecimalQuantity) && (allowDecimalQuantity))
				{
                    quantity = decimalQuantity;
					separateLines = true;
				}


				//Items items = new Items();
				//Hashtable itemInfoTable = items.getItemInfo(item, infojetContext, true, false);

				bool addItem = true;

				//Check User Account Limits
				if (webUserAccount != null)
				{
					if (webUserAccount.maxBuyType == 0)
					{
						//if ((webUserAccount.maxBuyLimitPerOrder != 0) && (totalCartAmount + (((ItemInfo)itemInfoTable[item.no]).unitPrice * quantity)) > webUserAccount.maxBuyLimitPerOrder)
						//{
						//	addItem = false;
						//}
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
					WebCartLines webCartLines = new WebCartLines(infojetContext);

                    DataSet webCartLineDataSet;
                    //if (webUserAccount == null)
                    //{
                        webCartLineDataSet = webCartLines.getCartLines(sessionId, item.no, extra1, extra2, extra3, extra4, extra5, referenceNo, fromDate, toDate, infojetContext.webSite.code);
                    //}
                    //else
                    //{
                    //    webCartLineDataSet = webCartLines.getWebUserAccountCartLines(webUserAccount.no, item.no, extra1, extra2, extra3, extra4, extra5, referenceNo, fromDate, toDate, infojetContext.webSite.code);
                    //}

                    WebCartLine savedWebCartLine = null;

					if ((webCartLineDataSet.Tables[0].Rows.Count > 0) && (!separateLines))
					{
                        
                        WebCartLine webCartLine = new WebCartLine(infojetContext, webCartLineDataSet.Tables[0].Rows[0]);
						webCartLine.quantity = webCartLine.quantity + quantity;
					
                        /*
						if (itemInfoTable.Contains(item.no))
						{
                            ItemInfo itemInfo = ((ItemInfo)itemInfoTable[item.no]);
                            float newUnitPrice = itemInfo.unitPrice;

                            ItemInfoPrice itemInfoPrice = itemInfo.itemInfoPriceCollection.getNearestQuantityPrice(webCartLine.quantity);
                            if (itemInfoPrice != null) newUnitPrice = itemInfoPrice.unitPrice;

                            webCartLine.unitPrice = newUnitPrice;
                            
						}
                        */
						webCartLine.save();
                        savedWebCartLine = webCartLine;

					}
					else
					{

                        WebCartLine webCartLine = new WebCartLine(infojetContext, infojetContext.sessionId, item);
                        webCartLine.webSiteCode = infojetContext.webSite.code;
						webCartLine.quantity = quantity;
						webCartLine.extra1 = extra1;
						webCartLine.extra2 = extra2;
						webCartLine.extra3 = extra3;
						webCartLine.extra4 = extra4;
						webCartLine.extra5 = extra5;
						webCartLine.referenceNo = referenceNo;
                        webCartLine.fromDate = fromDate;
                        webCartLine.toDate = toDate;
						if (webUserAccount != null) webCartLine.webUserAccountNo = webUserAccount.no;

                        /*
                        if (itemInfoTable.Contains(item.no))
                        {
                            ItemInfo itemInfo = ((ItemInfo)itemInfoTable[webCartLine.itemNo]);
                            float newUnitPrice = itemInfo.unitPrice;

                            ItemInfoPrice itemInfoPrice = itemInfo.itemInfoPriceCollection.getNearestQuantityPrice(webCartLine.quantity);
                            if (itemInfoPrice != null) newUnitPrice = itemInfoPrice.unitPrice;

                            webCartLine.unitPrice = newUnitPrice;
                        }                      
                        */

                        if ((itemConfiguration != null) && (itemConfiguration.validated))
                        {
                            if (itemConfiguration.unitPrice > 0) webCartLine.unitPrice = itemConfiguration.unitPrice;
                        }

						webCartLine.save();
                        savedWebCartLine = webCartLine;
					}

                    if ((itemConfiguration != null) && (itemConfiguration.validated))
                    {
                        itemConfiguration.applyConfiguration(quantity, savedWebCartLine);
                        
                    }

                    reCalculateCart(customerNo, currencyCode);

                    if (infojetContext.webSite.reserveCart) this.reserveCart();

					return true;
				}
			}

			return false;
		}



		public float getCartQuantity(string itemNo, string extra1, string extra2, string extra3, string extra4, string extra5, string reference)
		{
			WebCartLines webCartLines = new WebCartLines(infojetContext);

            DataSet cartDataSet = webCartLines.getCartLines(sessionId, itemNo, extra1, extra2, extra3, extra4, extra5, reference, infojetContext.webSite.code);

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
        	this.reCalculateCart(infojetContext.currencyCode);
		}

        public void reCalculateCart(string currencyCode)
        {
            if (infojetContext.userSession != null)
            {
                this.reCalculateCart(infojetContext.userSession.customer.no, currencyCode, "");
            }
            else
            {
                this.reCalculateCart("", currencyCode, "");
            }
        }

        public void reCalculateCart(string customerNo, string currencyCode)
        {
            this.reCalculateCart(customerNo, currencyCode, "");
        }

		public void reCalculateCart(string customerNo, string currencyCode, string campaignCode)
		{
			WebCartLines webCartLines = new WebCartLines(infojetContext);
            DataSet cartDataSet = webCartLines.getCartLines(infojetContext.sessionId, infojetContext.webSite.code);

			DataSet itemDataSet = webCartLines.convertToItemDataSet(cartDataSet);


			Items items = new Items();
            items.setCampaignCode(campaignCode);
			Hashtable itemInfoTable = items.getItemInfo(itemDataSet, infojetContext, customerNo, true, false, currencyCode);

			float totalAmount = 0;
            string priceStaffling = "";
	
			int i = 0;
			while (i < cartDataSet.Tables[0].Rows.Count)
			{
                WebCartLine webCartLine = new WebCartLine(infojetContext, cartDataSet.Tables[0].Rows[i]);
                float oldUnitPrice = webCartLine.unitPrice;

				if (itemInfoTable.Contains(webCartLine.itemNo))
				{
                    ItemInfo itemInfo = ((ItemInfo)itemInfoTable[webCartLine.itemNo]);
                    float newUnitPrice = itemInfo.unitPrice;

                    if (itemInfo.itemInfoPriceCollection.Count > 0)
                    {
                        float modelQty = webCartLines.calcModelQuantity(sessionId, infojetContext.webSite.code, webCartLine.itemNo);
                        if (modelQty == 0) modelQty = webCartLine.quantity;
                        ItemInfoPrice itemInfoPrice = itemInfo.itemInfoPriceCollection.getNearestQuantityPrice(modelQty);
                        if (itemInfoPrice != null)
                        {
                            newUnitPrice = itemInfoPrice.unitPrice;
                            priceStaffling = itemInfoPrice.minQuantity.ToString() + ", " + itemInfoPrice.lineDiscount.ToString() + ", " + itemInfo.itemInfoPriceCollection.Count.ToString();
                        }
                    }


                    

                    if ((webCartLine.unitPrice != newUnitPrice) && (newUnitPrice > 0))
                    {
                        webCartLine.unitPrice = newUnitPrice;
                        webCartLine.save();
                    }

				}

                if (infojetContext.configuration.debugMode)
                {
                    CartHandler.log(infojetContext, customerNo, currencyCode, webCartLine.entryNo, webCartLine.itemNo, webCartLine.quantity, oldUnitPrice, webCartLine.unitPrice, 0, "Recalc: " + priceStaffling);
                }				
	
				totalAmount = totalAmount + (webCartLine.unitPrice * webCartLine.quantity);

				i++;
			}


		}

		private void calcCartAmounts()
		{
            string vatBusPostingGroup = "";

            Customer currentCustomer = infojetContext.getCurrentCustomer();
            if (currentCustomer != null) vatBusPostingGroup = currentCustomer.vatBusPostingGroup;
        
            WebCartLines webCartLines = new WebCartLines(infojetContext);

            DataSet cartDataSet;
            cartDataSet = webCartLines.getCartLinesWithVAT(sessionId, infojetContext.webSite.code, vatBusPostingGroup);
 
			totalCartAmount = 0;
            totalCartAmountInclVat = 0;
            totalCartQuantity = 0;
            totalCartVatAmount = 0;
            totalCartGrossWeight = 0;
            totalCartVolume = 0;

			int i = 0;
			while (i < cartDataSet.Tables[0].Rows.Count)
			{
                WebCartLine webCartLine = new WebCartLine(infojetContext, cartDataSet.Tables[0].Rows[i]);

                float vatFactor = 0;
                try
                {
                    vatFactor = float.Parse(cartDataSet.Tables[0].Rows[i].ItemArray.GetValue(19).ToString());
                    if (cartDataSet.Tables[0].Rows[i].ItemArray.GetValue(22).ToString() == "1") vatFactor = 0;
                   
                }
                catch (Exception) { }
                vatFactor = 1 + (vatFactor / 100);


                float grossWeight = 0;
                try
                {
                    grossWeight = float.Parse(cartDataSet.Tables[0].Rows[i].ItemArray.GetValue(20).ToString());
                }
                catch (Exception) { }

                float unitVolume = 0;
                try
                {
                    unitVolume = float.Parse(cartDataSet.Tables[0].Rows[i].ItemArray.GetValue(21).ToString());
                }
                catch (Exception) { }

                totalCartAmount = totalCartAmount + (webCartLine.unitPrice * webCartLine.quantity);
                totalCartAmountInclVat = totalCartAmountInclVat + ((webCartLine.unitPrice * webCartLine.quantity) * vatFactor);
                totalCartVatAmount = totalCartVatAmount + (((webCartLine.unitPrice * webCartLine.quantity) * vatFactor) - (webCartLine.unitPrice * webCartLine.quantity));
                totalCartQuantity = totalCartQuantity + webCartLine.quantity;
                totalCartGrossWeight = totalCartGrossWeight + (webCartLine.quantity * grossWeight);
                totalCartVolume = totalCartVolume + (webCartLine.quantity * unitVolume);

				i++;
			}


            forced = false;
		}

        public void setForced(bool force)
        {
            this.forced = force;
        }

		public float getTotalCartQuantity()
		{
            if ((totalCartQuantity == 0) || (forced)) calcCartAmounts();            
            return this.totalCartQuantity;
		}

        public float getTotalCartAmount()
        {
            if ((totalCartAmount == 0) || (forced)) calcCartAmounts();
            return this.totalCartAmount;
        }

        public float getTotalCartVatAmount()
        {
            if ((totalCartVatAmount == 0) || (forced)) calcCartAmounts();
            return this.totalCartVatAmount;
        }

        public float getTotalCartGrossWeight()
        {
            if ((totalCartGrossWeight == 0) || (forced)) calcCartAmounts();
            return this.totalCartGrossWeight;
        }

        public float getTotalCartVolume()
        {
            if ((totalCartVolume == 0) || (forced)) calcCartAmounts();
            return this.totalCartVolume;
        }

        public string getFormatedTotalCartAmount(string currencyCode)
        {
            if ((totalCartAmount == 0) || (forced)) calcCartAmounts();
            if (infojetContext.webSite.showPriceInclVAT)
            {
                return database.formatCurrency(totalCartAmountInclVat, currencyCode);
            }
            else
            {
                return database.formatCurrency(totalCartAmount, currencyCode);
            }
        }

        public string getFormatedTotalCartAmount()
		{
            return getFormatedTotalCartAmount(infojetContext.presentationCurrencyCode);
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
            if (infojetContext.punchOutMode) infojetContext.redirect(infojetContext.punchOutCheckoutUrl);
                 
			if (webCart.webShopRegisterCode != "")
			{
                WebCheckout webCheckout = new WebCheckout(infojetContext, infojetContext.webSite.code, webCart.webShopRegisterCode);

                /*
                 * Löst genom en generell inloggningssida istället. Gäller för alla skyddade sidor.
                if (infojetContext.userSession == null)
                {
                    WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, webCheckout.loginRegistrationWebPageCode);
                    return webPage.getUrl();
                }
                */

                if (infojetContext.userSession != null)
                {
                    WebUserAccountCustomerRelations webUserAccountCustomerRelations = new WebUserAccountCustomerRelations(infojetContext);
                    if ((webUserAccountCustomerRelations.customerRelationsExists()) && (webCheckout.customerSearchWebPageCode != ""))
                    {
                        WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, webCheckout.customerSearchWebPageCode);
                        return webPage.getUrl();
                    }
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

        public static void log(Infojet infojetContext, string customerNo, string currencyCode, int cartEntryNo, string itemNo, float quantity, float unitPrice, float newUnitPrice, float lineAmount, string description)
        {
            DatabaseQuery dbQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Cart Log") + "] ([Cart Entry No], [Customer No], [Currency Code], [Item No], [Description], [Quantity], [Unit Price], [New Unit Price], [Line Amount], [Date Time]) VALUES (@cartEntryNo, @customerNo, @currencyCode, @itemNo, @description, @quantity, @unitPrice, @newUnitPrice, @lineAmount, @dateTime)");
            dbQuery.addIntParameter("cartEntryNo", cartEntryNo);
            dbQuery.addStringParameter("customerNo", customerNo, 20);
            dbQuery.addStringParameter("currencyCode", currencyCode, 20);
            dbQuery.addStringParameter("itemNo", itemNo, 20);
            dbQuery.addStringParameter("description", description, 50);
            dbQuery.addDecimalParameter("quantity", quantity);
            dbQuery.addDecimalParameter("unitPrice", unitPrice);
            dbQuery.addDecimalParameter("newUnitPrice", newUnitPrice);
            dbQuery.addDecimalParameter("lineAmount", lineAmount);
            dbQuery.addDateTimeParameter("dateTime", DateTime.Now);

            dbQuery.execute();

        }

	}
}
