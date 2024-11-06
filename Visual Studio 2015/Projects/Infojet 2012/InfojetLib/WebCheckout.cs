using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebCheckout
	{
		private string _webSiteCode;
        private string _code;
        private string _description;
        private string _step1WebPageCode;
        private string _step2WebPageCode;
        private string _step3WebPageCode;
        private string _step4WebPageCode;
        private string _step5WebPageCode;
        private string _loginRegistrationWebPageCode;
        private string _paymentWebPageCode;
        private string _orderConfirmationWebPageCode;
        private string _customerSearchWebPageCode;
        private bool _allowQuantityChange;
        private bool _showLineFieldExtra1;
        private bool _showLineFieldExtra2;
        private bool _showLineFieldExtra3;
        private bool _showLineFieldExtra4;
        private bool _showLineFieldExtra5;
        private bool _allowLineReference;
        private string _defaultPaymentMethod;
        private bool _allowDiscountCode;

		private string _lastErrorMessage = "";
		private string _lastOrderNoReceived = "";

        private string _formCode;
        private WebShipmentMethodCollection _webShipmentMethodCollection;
        private WebPaymentMethodCollection _webPaymentMethodCollection;

		private WebCartHeader _webCartHeader;
		private Infojet infojetContext;

        public WebCheckout(Infojet infojetContext, string webSiteCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.infojetContext = infojetContext;
			this._webSiteCode = webSiteCode;
			this._code = code;

            this._webCartHeader = WebCartHeader.get();
            if (_webCartHeader == null)
            {
                _webCartHeader = new WebCartHeader(infojetContext, infojetContext.sessionId);

            }

            if (!_webCartHeader.checkIfUserSessionSet())
            {
                if (infojetContext.userSession != null)
                {
                    _webCartHeader.setUserSession(infojetContext.userSession);
                    _webCartHeader.save();
                }
                else
                {
                    if ((infojetContext.webSite.commonCustomerNo != "") && (_webCartHeader.customerNo == "")) 
                    {
                        Customer customer = new Customer(infojetContext, infojetContext.webSite.commonCustomerNo);
                        _webCartHeader.setCustomer(customer);
                    }
                }

            }

			getFromDatabase();


		}


		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Step 1 Web Page Code], [Step 2 Web Page Code], [Step 3 Web Page Code], [Step 4 Web Page Code], [Step 5 Web Page Code], [Login Reg_ Web Page Code], [Allow Quantity Change], [Show Line Field Extra 1], [Show Line Field Extra 2], [Show Line Field Extra 3], [Show Line Field Extra 4], [Show Line Field Extra 5], [Payment Web Page Code], [Allow Line Reference], [Default Payment Method], [Form Code], [Order Confirm_ Web Page Code], [Customer Search Web Page Code], [Allow Discount Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Checkout") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				_webSiteCode = dataReader.GetValue(0).ToString();
				_code = dataReader.GetValue(1).ToString();
				_description = dataReader.GetValue(2).ToString();
				
				_step1WebPageCode = dataReader.GetValue(3).ToString();
				_step2WebPageCode = dataReader.GetValue(4).ToString();
				_step3WebPageCode = dataReader.GetValue(5).ToString();
				_step4WebPageCode = dataReader.GetValue(6).ToString();
				_step5WebPageCode = dataReader.GetValue(7).ToString();
				_loginRegistrationWebPageCode = dataReader.GetValue(8).ToString();

				_allowQuantityChange = false;
				if (dataReader.GetValue(9).ToString() == "1") _allowQuantityChange = true;

				_showLineFieldExtra1 = false;
				if (dataReader.GetValue(10).ToString() == "1") _showLineFieldExtra1 = true;

				_showLineFieldExtra2 = false;
				if (dataReader.GetValue(11).ToString() == "1") _showLineFieldExtra2 = true;

				_showLineFieldExtra3 = false;
				if (dataReader.GetValue(12).ToString() == "1") _showLineFieldExtra3 = true;

				_showLineFieldExtra4 = false;
				if (dataReader.GetValue(13).ToString() == "1") _showLineFieldExtra4 = true;

				_showLineFieldExtra5 = false;
				if (dataReader.GetValue(14).ToString() == "1") _showLineFieldExtra5 = true;

				_paymentWebPageCode = dataReader.GetValue(15).ToString();

				_allowLineReference = false;
				if (dataReader.GetValue(16).ToString() == "1") _allowLineReference = true;

				_defaultPaymentMethod = dataReader.GetValue(17).ToString();

                _formCode = dataReader.GetValue(18).ToString();

                _orderConfirmationWebPageCode = dataReader.GetValue(19).ToString();
                _customerSearchWebPageCode = dataReader.GetValue(20).ToString();

                _allowDiscountCode = false;
                if (dataReader.GetValue(21).ToString() == "1") _allowDiscountCode = true;

			}

			dataReader.Close();
			
		}

		public CartItemCollection getCartLines(Infojet infojetContext)
		{
            CartItemCollection cartItemCollection = new CartItemCollection();

			WebCartLines webCartLines = new WebCartLines(infojetContext);
            DataSet webCartLinesDataSet = webCartLines.getCartLines2(infojetContext.sessionId, infojetContext.webSite.code);

            DataSet itemDataSet = webCartLines.convertToItemDataSet(webCartLinesDataSet);
            
            Items items = new Items();
            Hashtable inventoryTable = items.getItemInfo(itemDataSet, infojetContext, false, true);

			int i = 0;
			while (i < webCartLinesDataSet.Tables[0].Rows.Count)
			{
                WebCartLine webCartLine = new WebCartLine(infojetContext, webCartLinesDataSet.Tables[0].Rows[i]);
              
                CartItem cartItem = new CartItem(webCartLine);

                Item item = Item.get(infojetContext, cartItem.itemNo);

                //ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                //cartItem.description = itemTranslation.description;
                cartItem.description = webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(17).ToString();
                if (webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(18).ToString() != "") cartItem.description = webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(18).ToString();
                if (webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(19).ToString() != "") cartItem.webModelNo = webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(19).ToString();

                cartItem.unitPrice = webCartLine.unitPrice;

                cartItem.amount = cartItem.unitPrice * cartItem.quantity;

                if (infojetContext.webSite.showPriceInclVAT)
                {
                    cartItem.formatedAmount = infojetContext.systemDatabase.formatCurrency(cartItem.amount*item.getVatFactor(), infojetContext.currencyCode);
                    cartItem.formatedUnitPrice = infojetContext.systemDatabase.formatCurrency(cartItem.unitPrice * item.getVatFactor(), "");
                }
                else
                {
                    cartItem.formatedAmount = infojetContext.systemDatabase.formatCurrency(cartItem.amount, infojetContext.currencyCode);
                    cartItem.formatedUnitPrice = infojetContext.systemDatabase.formatCurrency(cartItem.unitPrice, "");
                }

                cartItem.removeLink = infojetContext.cartHandler.renderRemoveLink(cartItem.lineNo);

                if (inventoryTable[cartItem.itemNo] != null)
                {
                    cartItem.inventory = ((ItemInfo)inventoryTable[cartItem.itemNo]).inventory;

                }

                if (inventoryTable[cartItem.itemNo + "_" + cartItem.extra1] != null)
                {
                    cartItem.inventory = ((ItemInfo)inventoryTable[cartItem.itemNo + "_" + cartItem.extra1]).inventory;

                }

                int availability = 0;
                int visibility = 0;
                int modelVisibility = 0;
                int fixedInventory = 0;
                int minOrderableQty = 0;

                try
                {
                    availability = int.Parse(webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(23).ToString());
                    visibility = int.Parse(webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(24).ToString());
                    fixedInventory = int.Parse(webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(25).ToString());
                    minOrderableQty = int.Parse(webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(27).ToString());
                }
                catch (Exception) { }

                if (webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(19).ToString() != "")
                {
                    try
                    {
                        availability = int.Parse(webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(20).ToString());
                        modelVisibility = int.Parse(webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(21).ToString());
                        fixedInventory = int.Parse(webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(28).ToString());
                        minOrderableQty = int.Parse(webCartLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(26).ToString());

                    }
                    catch (Exception) { }

                }
                cartItem.minOrderableQty = minOrderableQty;
                if (availability == 0) availability = infojetContext.webSite.availability+1;
                if (visibility == 0) visibility = infojetContext.webSite.visibility + 1;

                if (availability == 1)
                {
                    cartItem.checkInventory = true;
                }
                if ((visibility == 6) || (modelVisibility == 6))
                {
                    cartItem.inventory = fixedInventory;
                }

                
 

                if (availability < 3)
                {
                    cartItemCollection.Add(cartItem);
                }
                else
                {
                    webCartLine.delete();
                }

				i++;
			}



            return cartItemCollection;
		}

        public CartItemCollection addInventoryInfo(Infojet infojetContext, CartItemCollection cartItemCollection)
        {
            WebCartLines webCartLines = new WebCartLines(infojetContext);
            DataSet webCartLinesDataSet = webCartLines.getCartLines(infojetContext.sessionId, infojetContext.webSite.code);

            DataSet itemDataSet = webCartLines.convertToItemDataSet(webCartLinesDataSet);
            Items items = new Items();
            Hashtable itemInfoTable = items.getItemInfo(itemDataSet, infojetContext, false, true);

            int i = 0;
            while (i < cartItemCollection.Count)
            {
                CartItem cartItem = cartItemCollection[i];

                if (itemInfoTable.Contains(cartItem.itemNo.ToString()))
                {
                    ItemInfo itemInfo = (ItemInfo)itemInfoTable[cartItem.itemNo.ToString()];

                    cartItem.applyItemInfo(itemInfo);

                }

                i++;
            }

            return cartItemCollection;
        }

        public WebShipmentMethodCollection getShipmentMethods()
        {
            if (_webShipmentMethodCollection == null)
            {
                WebShipmentMethods webShipmentMethods = new WebShipmentMethods(infojetContext);
                _webShipmentMethodCollection = webShipmentMethods.getWebShipmentMethodCollection(infojetContext.webSite.code, webCartHeader.shipToCountryCode, webCartHeader.shipToPostCode, infojetContext.cartHandler.getTotalCartQuantity(), infojetContext.cartHandler.getTotalCartAmount(), infojetContext.cartHandler.getTotalCartGrossWeight(), infojetContext.cartHandler.getTotalCartVolume(), infojetContext.languageCode, this);
            }
            return _webShipmentMethodCollection;
        }

        public WebPaymentMethodCollection getPaymentMethods()
        {
            if (_webPaymentMethodCollection == null)
            {
                WebPaymentMethods webPaymentMethods = new WebPaymentMethods(infojetContext);
                _webPaymentMethodCollection = webPaymentMethods.getWebPaymentMethodCollection(infojetContext.webSite.code, infojetContext.languageCode, this);
            }
            return _webPaymentMethodCollection;

        }

        public void updateQuantity(int entryNo, int quantity)
        {
            Items items = new Items(); 
            WebCartLine webCartLine = new WebCartLine(infojetContext, entryNo);

            try
            {
                if (quantity <= 0)
                {
                    webCartLine.delete();
                    return;
                }

                //Item item = new Item(infojetContext, webCartLine.itemNo);
                Item item = Item.get(infojetContext, webCartLine.itemNo);

                webCartLine.quantity = quantity;

                items.setCampaignCode(webCartHeader.campaignCode);
                Hashtable itemInfoTable = items.getItemInfo(item, infojetContext, true, false);

                if (itemInfoTable.Contains(item.no))
                {
                    ItemInfo itemInfo = ((ItemInfo)itemInfoTable[webCartLine.itemNo]);
                    float newUnitPrice = itemInfo.unitPrice;

                    ItemInfoPrice itemInfoPrice = itemInfo.itemInfoPriceCollection.getNearestQuantityPrice(webCartLine.quantity);
                    if (itemInfoPrice != null) newUnitPrice = itemInfoPrice.unitPrice;

                    if ((webCartLine.unitPrice != newUnitPrice) && (newUnitPrice > 0))
                    {
                        webCartLine.unitPrice = newUnitPrice;
                        webCartLine.save();
                    }
                }

                webCartLine.amount = webCartLine.unitPrice * webCartLine.quantity;

                webCartLine.save();
            }
            catch (Exception)
            { }

 
        }

        public void updateReference(int entryNo, string referenceNo)
        {
            WebCartLine webCartLine = new WebCartLine(infojetContext, entryNo);

            webCartLine.referenceNo = referenceNo;
            webCartLine.save();

        }

        public void checkUserAuthorization()
        {
            if (!infojetContext.webSite.allowPurchaseNotLoggedIn)
            {
                if (infojetContext.userSession == null)
                {
                    WebPage newUserWebPage = new WebPage(infojetContext, infojetContext.webSite.code, this.loginRegistrationWebPageCode);
                    infojetContext.redirect(newUserWebPage.getUrl());
                }
            }
        }

		private void sendOrder()
		{
            CartItemCollection cartItemCollection = this.getCartLines(infojetContext);
            if (cartItemCollection.Count > 0)
            {
                this.webCartHeader.setWebCheckout(this);

                if (submitOrder())
                {

                    string customerNo = webCartHeader.customerNo;


                    if (webCartHeader.webPaymentMethod != null)
                    {
                        if (webCartHeader.webPaymentMethod.getPaymentModule(this) != null)
                        {
                            
                            webCartHeader.paymentOrderNo = this._lastOrderNoReceived;
                            WebPage paymentWebPage = new WebPage(infojetContext, infojetContext.webSite.code, this.paymentWebPageCode);
                            infojetContext.redirect(paymentWebPage.getUrl());
                        }
                    }

                    webCartHeader.deleteLines();
                    webCartHeader.delete();

                    WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, this.orderConfirmationWebPageCode);

                    Link link = webPage.getUrlLink();
                    link.addParameter("orderNo", this._lastOrderNoReceived);
                    link.addParameter("customerNo", customerNo);
                    link.addParameter("docType", "0");
                    link.addParameter("docNo", this._lastOrderNoReceived);

                    infojetContext.redirect(link.toUrl());
                }

				
			}
			else
			{
                WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, this.step2WebPageCode);
				infojetContext.redirect(webPage.getUrl());
			}
		}

        private bool submitOrder()
        {

            if (infojetContext.webSite.orderSubmitMethod == 1)
            {
                //try
                //{
                    ServiceRequest serviceRequest = new ServiceRequest(infojetContext, "createOrder", this.webCartHeader);
                    DatabaseQuery dbQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Order Message Queue") + "] ([Xml Document], [Customer No_], [To Process], [Done], [Created DateTime]) VALUES (@xmlDocument, '" + webCartHeader.customerNo + "', 0, 0, GETDATE())");
                    dbQuery.addImageParameter("xmlDocument", System.Text.Encoding.Default.GetBytes(serviceRequest.getDocument().OuterXml));
                    dbQuery.execute();

                    int entryNo = (int)infojetContext.systemDatabase.getInsertedSeqNo();
                    string orderNo = "W" + entryNo.ToString().PadLeft(10, '0');

                    infojetContext.systemDatabase.nonQuery("UPDATE [" + infojetContext.systemDatabase.getTableName("Web Order Message Queue") + "] SET [To Process] = 1 WHERE [Entry No_] = '" + entryNo + "'");

                    this._lastOrderNoReceived = orderNo;
                    return true;
                //}
                //catch (Exception e)
                //{
                 //   this._lastOrderNoReceived = "";
                 //   return false;
                //}
            }



            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createOrder", this.webCartHeader));
            if (!appServerConnection.processRequest())
            {
                if (appServerConnection.serviceResponse != null) _lastErrorMessage = appServerConnection.serviceResponse.errorMessage;
                this._lastOrderNoReceived = "";
                return false;
            }
            else
            {
                this._lastOrderNoReceived = appServerConnection.serviceResponse.orderNo;
                return true;
            }


            return false;
        }
    
		public string formatCurrency(float amount)
		{
			return infojetContext.systemDatabase.formatCurrency(amount, webCartHeader.currencyCode);
		}

		public DataSet getPaymentMethods(string currencyCode)
		{
			if (infojetContext.generalLedgerSetup.lcyCode == currencyCode) currencyCode = "";

			WebPaymentMethods webPaymentMethods = new WebPaymentMethods(infojetContext);

			return webPaymentMethods.getWebPaymentMethods(this.webSiteCode);

		}

        public Panel getFormPanel()
        {
            return getFormPanel(true);
        }

        public Panel getFormPanel(bool showNextButton)
        {
            Panel formPanel = null;

            WebForm webForm = new WebForm(infojetContext, this.webSiteCode, this.formCode);
            webForm.setParentControl(this);

            webCartToForm(webCartHeader, ref webForm);

            formPanel = webForm.getFormPanel();

            if (this._lastErrorMessage != "")
            {
                Label errorLabel = new Label();
                errorLabel.Text = _lastErrorMessage;
                errorLabel.CssClass = "errorMessage";
                formPanel.Controls.Add(errorLabel);
            }

            if (showNextButton)
            {
                Panel buttonPanel = new Panel();
                buttonPanel.CssClass = "submitBtn";

                Button nextButton = new Button();
                nextButton.Text = infojetContext.translate("NEXT");
                nextButton.ValidationGroup = webForm.code;
                nextButton.CssClass = "Button";
                nextButton.Click += new EventHandler(nextButton_Click);
                nextButton.ID = "btn_submit";

                buttonPanel.Controls.Add(nextButton);
                formPanel.Controls.Add(buttonPanel);
            }


            return formPanel;

        }

        public void processForm(Panel panel)
        {
            WebForm webForm = new WebForm(infojetContext, this.webSiteCode, this.formCode);
            WebFormDocument webFormDocument = webForm.readForm(panel);

            formToWebCart(webFormDocument, ref _webCartHeader);

            WebSiteCountry webSiteCountry = new WebSiteCountry(infojetContext.systemDatabase, webSiteCode, webCartHeader.shipToCountryCode);
            webCartHeader.vatBusPostingGroup = webSiteCountry.vatBusPostingGroup;

            webCartHeader.save();

        }

        void nextButton_Click(object sender, EventArgs e)
        {
            
            processForm((Panel)((Button)sender).Parent);

            goNext();
        }

        private void formToWebCart(WebFormDocument webFormDocument, ref WebCartHeader webCartHeader)
        {
            int i = 0;

            while (i < webFormDocument.keyList.Count)
            {
                string key = (string)webFormDocument.keyList[i];

                WebFormField webFormField = new WebFormField(infojetContext, webSiteCode, formCode, key);
                if (webFormField.connectionType == 1) webCartHeader.shipToName = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 2) webCartHeader.shipToAddress = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 3) webCartHeader.shipToAddress2 = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 4) webCartHeader.shipToPostCode = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 5) webCartHeader.shipToCity = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 6) webCartHeader.shipToCountryCode = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 7) webCartHeader.billToName = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 8) webCartHeader.billToAddress = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 9) webCartHeader.billToAddress2 = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 10) webCartHeader.billToPostCode = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 11) webCartHeader.billToCity = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 12) webCartHeader.billToCountryCode = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 13) webCartHeader.setExtraField(webFormField, webFormDocument.valueList[i].ToString());
                if (webFormField.connectionType == 14) webCartHeader.email = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 15) webCartHeader.phoneNo = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 16) webCartHeader.setExtraField(webFormField, webFormDocument.valueList[i].ToString());
                if (webFormField.connectionType == 17) webCartHeader.setExtraField(webFormField, webFormDocument.valueList[i].ToString());
                if (webFormField.connectionType == 18) webCartHeader.contactName = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 21) webCartHeader.customerNo = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 22) webCartHeader.customerOrderNo = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 23) webCartHeader.noteOfGoods = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 24) webCartHeader.message = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 25) webCartHeader.shippingAdvice = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 26) webCartHeader.shipmentDate = DateTime.Parse(webFormDocument.valueList[i].ToString());
                if (webFormField.connectionType == 29) webCartHeader.shipToName2 = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 30) webCartHeader.ordererName = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 31) webCartHeader.ordererPhoneNo = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 32) webCartHeader.billToName2 = webFormDocument.valueList[i].ToString();

                webCartHeader.setExtraField(webFormField, webFormDocument.valueList[i].ToString());


                i++;
            }


        }

        public float getTotalAmount()
        {           
            float amount = infojetContext.cartHandler.getTotalCartAmount() + webCartHeader.freightFee + webCartHeader.adminFee;
            return amount - getDiscountAmount();
        }

        public void forceAmountUpdate()
        {
            infojetContext.cartHandler.setForced(true);
            infojetContext.cartHandler.getTotalCartAmount();
        }

        public float getDiscountAmount()
        {
            if (System.Web.HttpContext.Current.Session["externalDiscount"] != null)
            {
                return (float)System.Web.HttpContext.Current.Session["externalDiscount"];
            }
            return 0;
        }

        public float getDiscountPercent()
        {
            if (getDiscountAmount() > 0)
            {
                return (getDiscountAmount() / (getTotalAmount() + getDiscountAmount())) * 100;
            }
            return 0;
        }

        public float getTotalVatAmount()
        {
            float freightVat = 0;
            float adminVat = 0;

            if (webCartHeader.webShipmentMethod != null)
            {
                freightVat = webCartHeader.webShipmentMethod.amountInclVat - webCartHeader.webShipmentMethod.amount;
            }

            if (webCartHeader.webPaymentMethod != null)
            {
                adminVat = webCartHeader.adminFeeInclVat - webCartHeader.adminFee;
            }

            float vatCartAmount = infojetContext.cartHandler.getTotalCartVatAmount();
            
            if (getDiscountAmount() > 0)
            {                
                vatCartAmount = vatCartAmount - (vatCartAmount * (getDiscountPercent() / 100));
            }

            return vatCartAmount + freightVat + adminVat;
        }

        public float getTotalAmountInclVat()
        {
            /*
            float freightAmount = 0;
            float adminAmount = 0;

            if (webCartHeader.webShipmentMethod != null)
            {
                freightAmount = webCartHeader.webShipmentMethod.amountInclVat;
            }

            if (webCartHeader.webPaymentMethod != null)
            {
                adminAmount = webCartHeader.webPaymentMethod.amountInclVat;
            }

            return infojetContext.cartHandler.getTotalCartAmount() + infojetContext.cartHandler.getTotalCartVatAmount() + freightAmount + adminAmount;
            */
            return getTotalAmount() + getTotalVatAmount();
        }

        public void goBack()
        {
            string prevWebPageCode = "";

            if (infojetContext.webPage.code == this._step2WebPageCode) prevWebPageCode = this.step1WebPageCode;
            if (infojetContext.webPage.code == this._step3WebPageCode) prevWebPageCode = this.step2WebPageCode;
            if (infojetContext.webPage.code == this._step4WebPageCode) prevWebPageCode = this.step3WebPageCode;
            if (infojetContext.webPage.code == this._step5WebPageCode) prevWebPageCode = this.step4WebPageCode;

            if (prevWebPageCode != "")
            {
                WebPage goBackWebPage = new WebPage(infojetContext, infojetContext.webSite.code, prevWebPageCode);
                infojetContext.redirect(goBackWebPage.getUrl());
            }
        }

        public void goNext()
        {
            string nextWebPageCode = "";

            if (infojetContext.webPage.code == this._step1WebPageCode) nextWebPageCode = this.step2WebPageCode;
            if (infojetContext.webPage.code == this._step2WebPageCode) nextWebPageCode = this.step3WebPageCode;
            if (infojetContext.webPage.code == this._step3WebPageCode) nextWebPageCode = this.step4WebPageCode;
            if (infojetContext.webPage.code == this._step4WebPageCode) nextWebPageCode = this.step5WebPageCode;

            if (nextWebPageCode != "")
            {
                WebPage nextWebPage = new WebPage(infojetContext, infojetContext.webSite.code, nextWebPageCode);
                infojetContext.redirect(nextWebPage.getUrl());
            }
            else
            {
                sendOrder();
            }

        }

        public string getUserControl()
        {
            string step = "_1";
            if (infojetContext.webPage.code == step1WebPageCode) step = "_1";
            if (infojetContext.webPage.code == step2WebPageCode) step = "_2";
            if (infojetContext.webPage.code == step3WebPageCode) step = "_3";
            if (infojetContext.webPage.code == step4WebPageCode) step = "_4";
            if (infojetContext.webPage.code == step5WebPageCode) step = "_5";
            if (infojetContext.webPage.code == loginRegistrationWebPageCode) step = "_REG";
            if (infojetContext.webPage.code == paymentWebPageCode) step = "_PAYMENT";
            if (infojetContext.webPage.code == customerSearchWebPageCode) step = "_CUSTOMER";

            return "Checkout" + step;
        }

        public bool sendOrderIsNext()
        {
            if ((infojetContext.webPage.code == this._step1WebPageCode) && (this.step2WebPageCode == "")) return true;
            if ((infojetContext.webPage.code == this._step2WebPageCode) && (this.step3WebPageCode == "")) return true;
            if ((infojetContext.webPage.code == this._step3WebPageCode) && (this.step4WebPageCode == "")) return true;
            if (infojetContext.webPage.code == this._step4WebPageCode) return true;

            return false;
        }

        private void webCartToForm(WebCartHeader webCartHeader, ref WebForm webForm)
        {
            DataSet webFormFieldDataSet = webForm.getFields();

            int i = 0;

            Hashtable valueTable = new Hashtable();

            while (i < webFormFieldDataSet.Tables[0].Rows.Count)
            {
                WebFormField webFormField = new WebFormField(infojetContext, webFormFieldDataSet.Tables[0].Rows[i]);

                if (webFormField.connectionType == 0)
                {
                    string formFieldValue = "";
                    if (infojetContext.userSession != null) formFieldValue = infojetContext.userSession.webUserAccount.getHistoryProfileValue(webForm.code, webFormField.code);
                    string currentFormFieldValue = webCartHeader.extraFields.getFieldValue(webFormField.code);
                    if (currentFormFieldValue != null) formFieldValue = currentFormFieldValue;

                    if (formFieldValue != "") valueTable.Add(webFormField.code, formFieldValue);

                }
                 
                if (webFormField.connectionType == 1) valueTable.Add(webFormField.code, webCartHeader.shipToName);
                if (webFormField.connectionType == 2) valueTable.Add(webFormField.code, webCartHeader.shipToAddress);
                if (webFormField.connectionType == 3) valueTable.Add(webFormField.code, webCartHeader.shipToAddress2);
                if (webFormField.connectionType == 4) valueTable.Add(webFormField.code, webCartHeader.shipToPostCode);
                if (webFormField.connectionType == 5) valueTable.Add(webFormField.code, webCartHeader.shipToCity);
                if (webFormField.connectionType == 6) valueTable.Add(webFormField.code, webCartHeader.shipToCountryCode);
                if (webFormField.connectionType == 7) valueTable.Add(webFormField.code, webCartHeader.billToName);
                if (webFormField.connectionType == 8) valueTable.Add(webFormField.code, webCartHeader.billToAddress);
                if (webFormField.connectionType == 9) valueTable.Add(webFormField.code, webCartHeader.billToAddress2);
                if (webFormField.connectionType == 10) valueTable.Add(webFormField.code, webCartHeader.billToPostCode);
                if (webFormField.connectionType == 11) valueTable.Add(webFormField.code, webCartHeader.billToCity);
                if (webFormField.connectionType == 12) valueTable.Add(webFormField.code, webCartHeader.billToCountryCode);
                if (webFormField.connectionType == 13) 
                {
                    string currentFormFieldValue = webCartHeader.extraFields.getFieldValue(webFormField.code);
                    if (currentFormFieldValue != null) valueTable.Add(webFormField.code, currentFormFieldValue);
                }
                if (webFormField.connectionType == 14) valueTable.Add(webFormField.code, webCartHeader.email);
                if (webFormField.connectionType == 15) valueTable.Add(webFormField.code, webCartHeader.phoneNo);
                if (webFormField.connectionType == 16)
                {
                    string currentFormFieldValue = webCartHeader.extraFields.getFieldValue(webFormField.code);
                    if (currentFormFieldValue != null) valueTable.Add(webFormField.code, currentFormFieldValue);
                }
                if (webFormField.connectionType == 17)
                {
                    string currentFormFieldValue = webCartHeader.extraFields.getFieldValue(webFormField.code);
                    if (currentFormFieldValue != null) valueTable.Add(webFormField.code, currentFormFieldValue);
                }

                if (webFormField.connectionType == 18) valueTable.Add(webFormField.code, webCartHeader.contactName);
                if (webFormField.connectionType == 21) valueTable.Add(webFormField.code, webCartHeader.customerNo);
                if (webFormField.connectionType == 22) valueTable.Add(webFormField.code, webCartHeader.customerOrderNo);
                if (webFormField.connectionType == 23) valueTable.Add(webFormField.code, webCartHeader.noteOfGoods);
                if (webFormField.connectionType == 24) valueTable.Add(webFormField.code, webCartHeader.message);
                if (webFormField.connectionType == 25) valueTable.Add(webFormField.code, webCartHeader.shippingAdvice);
                if (webFormField.connectionType == 26) valueTable.Add(webFormField.code, webCartHeader.shipmentDate.ToString("yyyy-MM-dd"));
                if (webFormField.connectionType == 27) valueTable.Add(webFormField.code, infojetContext.marketCountryCode);
                if (webFormField.connectionType == 29) valueTable.Add(webFormField.code, webCartHeader.shipToName2);
                if (webFormField.connectionType == 30) valueTable.Add(webFormField.code, webCartHeader.ordererName);
                if (webFormField.connectionType == 31) valueTable.Add(webFormField.code, webCartHeader.ordererPhoneNo);
                if (webFormField.connectionType == 32) valueTable.Add(webFormField.code, webCartHeader.billToName2);


                i++;
            }

            webForm.setFormValues(valueTable);

        }

        public void updateFreight()
        {
            updatePaymentAndFreight();
        }

        public void updatePaymentAndFreight()
        {
            WebShipmentMethodCollection webShipmentMethodCollection = this.getShipmentMethods();
            if (webShipmentMethodCollection.Count > 0)
            {
                if (webCartHeader.webShipmentMethod != null)
                {
                    int i = 0;
                    bool shipmentMethodApplied = false;
                    while (i < webShipmentMethodCollection.Count)
                    {
                        if (webShipmentMethodCollection[i].code == webCartHeader.webShipmentMethod.code)
                        {
                            webCartHeader.applyWebShipmentMethod(webShipmentMethodCollection[i]);
                            shipmentMethodApplied = true;
                        }

                        i++;
                    }
                    if (!shipmentMethodApplied)
                    {
                        webCartHeader.applyWebShipmentMethod(webShipmentMethodCollection[0]);
                    }
                }
                else
                {
                    webCartHeader.applyWebShipmentMethod(webShipmentMethodCollection[0]);
                }
                webCartHeader.save();
            }


            WebPaymentMethodCollection webPaymentMethodCollection = this.getPaymentMethods();
            if (webPaymentMethodCollection.Count > 0)
            {
                if (webCartHeader.webPaymentMethod != null)
                {
                    int i = 0;
                    bool paymentMethodApplied = false;
                    while (i < webPaymentMethodCollection.Count)
                    {
                        if (webPaymentMethodCollection[i].code == webCartHeader.webPaymentMethod.code)
                        {
                            webCartHeader.applyWebPaymentMethod(webPaymentMethodCollection[i]);
                            paymentMethodApplied = true;
                        }

                        i++;
                    }
                    if (!paymentMethodApplied)
                    {
                        webCartHeader.applyWebPaymentMethod(webPaymentMethodCollection[0]);
                    }
                }
                else
                {
                    webCartHeader.applyWebPaymentMethod(webPaymentMethodCollection[0]);
                }
                webCartHeader.save();
            }

        }

        public bool checkInventory()
        {
            CartItemCollection cartItemCollection = getCartLines(infojetContext);

            int i = 0;
            while (i < cartItemCollection.Count)
            {
                CartItem cartItem = cartItemCollection[i];
                if (cartItem.checkInventory)
                {
                    if (cartItem.quantity > cartItem.inventory) return false;

                }

                i++;
            }
           
            return true;

        }

        public bool checkMinQuantities()
        {
            return checkMinQuantities(false);
        }

        public bool checkMinQuantities(bool adjust)
        {
            CartItemCollection cartItemCollection = getCartLines(infojetContext);

            int i = 0;
            while (i < cartItemCollection.Count)
            {
                CartItem cartItem = cartItemCollection[i];
                if (!checkMinQuantity(ref cartItem, cartItemCollection, adjust)) return false;
                cartItemCollection[i] = cartItem;

                i++;
            }
            return true;
        }


        public bool checkMinQuantity(ref CartItem thisCartItem, CartItemCollection cartItemCollection, bool adjust)
        {
            float totalQty = 0;
            if (thisCartItem.webModelNo != "")
            {
                int i = 0;
                while (i < cartItemCollection.Count)
                {
                    CartItem cartItem = cartItemCollection[i];
                    if (cartItem.webModelNo == thisCartItem.webModelNo) totalQty = totalQty + cartItem.quantity;

                    i++;
                }
            }
            else
            {
                totalQty = thisCartItem.quantity;
            }

            if (totalQty >= thisCartItem.minOrderableQty)
            {
                return true;
            }
            else
            {
                if (adjust)
                {

                    float adjustQty = thisCartItem.quantity + thisCartItem.minOrderableQty - totalQty;

                    Navipro.Infojet.Lib.WebCartLine webCartLine = new Navipro.Infojet.Lib.WebCartLine(infojetContext, thisCartItem.lineNo);
                    webCartLine.quantity = adjustQty;
                    webCartLine.save();
                    thisCartItem.quantity = adjustQty;
                    return true;
                }
            }
            return false;
        }

        /* Depricated */
        private void directSubmitOrder(string orderNo, Navipro.Infojet.Lib.WebCartHeader webCartHeader)
        {

            DatabaseQuery databaseQuery = null;

            try
            {
                WebCartLines webCartLines = new WebCartLines(infojetContext);
                DataSet webCartLinesDataSet = webCartLines.getCartLines(infojetContext.sessionId, infojetContext.webSite.code);

                int i = 0;
                while (i < webCartLinesDataSet.Tables[0].Rows.Count)
                {
                    WebCartLine webCartLine = new WebCartLine(infojetContext, webCartLinesDataSet.Tables[0].Rows[i]);

                    databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] (" +
                        "[Document Type], [Sell-to Customer No_], [Document No_], [Line No_], [Item No_], [Unit Of Measure Code], [Quantity], [Unit Price], " +
                        "[Line Discount %], [Amount], [Reference], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No_], " +
                        "[Received From Date], [Received To Date]) VALUES (" +

                        "1, @sellToCustomerNo, @no, @lineNo, @itemNo, @unitOfMeasureCode, @quantity, @unitPrice, " +
                        "@lineDiscount, @amount, @reference, @extra1, @extra2, @extra3, @extra4, extra5, " +
                        "@webUserAccountNo, '1753-01-01 00:00:00', '1753-01-01 00:00:00')");

                    databaseQuery.addStringParameter("@sellToCustomerNo", webCartHeader.customerNo.ToUpper(), 20);
                    databaseQuery.addStringParameter("@no", orderNo, 20);
                    databaseQuery.addIntParameter("@lineNo", (i + 1) * 10000);
                    databaseQuery.addStringParameter("@itemNo", webCartLine.itemNo.ToUpper(), 20);
                    databaseQuery.addStringParameter("@unitOfMeasureCode", webCartLine.unitOfMeasureCode, 20);
                    databaseQuery.addDecimalParameter("@quantity", webCartLine.quantity);
                    databaseQuery.addDecimalParameter("@unitPrice", webCartLine.unitPrice);
                    databaseQuery.addDecimalParameter("@lineDiscount", 0);
                    databaseQuery.addDecimalParameter("@amount", webCartLine.unitPrice * webCartLine.quantity);
                    databaseQuery.addStringParameter("@reference", webCartLine.referenceNo, 30);
                    databaseQuery.addStringParameter("@extra1", webCartLine.extra1, 30);
                    databaseQuery.addStringParameter("@extra2", webCartLine.extra2, 30);
                    databaseQuery.addStringParameter("@extra3", webCartLine.extra3, 30);
                    databaseQuery.addStringParameter("@extra4", webCartLine.extra4, 30);
                    databaseQuery.addStringParameter("@extra5", webCartLine.extra5, 30);
                    databaseQuery.addStringParameter("@webUserAccountNo", webCartLine.webUserAccountNo, 20);

                    databaseQuery.execute();

                    i++;
                }

                i = 0;
                while (i < webCartHeader.extraFields.Count)
                {
                    databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Sales Header Extra Field") + "] (" +
                        "[Document Type], [Document No_], [Form Code], [Field Code], [Value]) VALUES (" +
                        "1, @no, @formCode, @fieldCode, @value)");

                    databaseQuery.addStringParameter("@no", orderNo, 20);
                    databaseQuery.addStringParameter("@formCode", webCartHeader.extraFields[i].webFormCode, 20);
                    databaseQuery.addStringParameter("@fieldCode", webCartHeader.extraFields[i].fieldCode, 20);
                    databaseQuery.addStringParameter("@value", webCartHeader.extraFields[i].value, 250);

                    databaseQuery.execute();

                    i++;
                }


                databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Sales Header") + "] (" +
                    "[Document Type], [Sell-to Customer No_], [No_], [Web Site Code], [User Account No_], [Order Date], [Bill-to Name], [Bill-to Name 2], " +
                    "[Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country Code], [Ship-to Name], [Ship-to Name 2], " +
                    "[Ship-to Address], [Ship-to Address 2], [Ship-to Post Code], [Ship-to City], [Ship-to Country Code], [Add Ship-to Address], [Contact Name], " +
                    "[Phone No_], [E-Mail], [Customer Order No_], [Note Of Goods], [Client IP Address], [Client User Agent], [Order Created Date Time], " +
                    "[Language Code], [Currency Code], [Web Payment Method Code], [Payment Reference No_], [Campaign Code], [Freight Fee], [Administrative Fee], " +
                    "[Freight G_L Account No_], [Admin G_L Account No_], [Freight VAT Prod Posting Group], [Admin VAT Prod Posting Group], [Shipping Agent Code], " +
                    "[Shipment Method Code], [Web Shipment Method Code], [Shipping Agent Service Code], [Shipping Advice Option], [Shipment Date], [Message Text], " +
                    "[Transfered], [Pre-Payment Received], [Shipped], [Invoiced], [Pre-Payment Expected], [Deleted], [Delegated To Company], [Delegated]) VALUES (" +

                    "1, @sellToCustomerNo, @no, @webSiteCode, @userAccountNo, @orderDate, @billToName, @billToName2, " +
                    "@billToAddress, @billToAddress2, @billToPostCode, @billToCity, @billToCountryCode, @shipToName, @shipToName2, " +
                    "@shipToAddress, @shipToAddress2, @shipToPostCode, @shipToCity, @shipToCountryCode, @addShipToAddress, @contactName, " +
                    "@phoneNo, @email, @customerOrderNo, @noteOfGoods, @clientIpAddress, @clientUserAgent, @orderCreatedDateTime, " +
                    "@languageCode, @currencyCode, @webPaymentMethodCode, '', @campaignCode, @freightFee, @administrativeFee, " +
                    "@freightGLAccountNo, @adminGLAccountNo, @freightVATProdPostingGroup, @adminVATProdPostingGroup, @shippingAgentCode, " +
                    "@shipmentMethodCode, @webShipmentMethodCode, @shippingAgentServiceCode, @shippingAdviceOption, @shipmentDate, @messageText, " +
                    "0, 0, 0, 0, @prepaymentExptected, @deleted, '', 0)");


                databaseQuery.addStringParameter("@sellToCustomerNo", webCartHeader.customerNo.ToUpper(), 20);
                databaseQuery.addStringParameter("@no", orderNo, 20);
                databaseQuery.addStringParameter("@webSiteCode", webCartHeader.webSiteCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@userAccountNo", webCartHeader.userAccountNo.ToUpper(), 20);
                databaseQuery.addDateTimeParameter("@orderDate", DateTime.Today);
                databaseQuery.addStringParameter("@billToName", webCartHeader.billToName, 50);
                databaseQuery.addStringParameter("@billToName2", webCartHeader.billToName2, 50);
                databaseQuery.addStringParameter("@billToAddress", webCartHeader.billToAddress, 50);
                databaseQuery.addStringParameter("@billToAddress2", webCartHeader.billToAddress2, 50);
                databaseQuery.addStringParameter("@billToPostCode", webCartHeader.billToPostCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@billToCity", webCartHeader.billToCity, 50);
                databaseQuery.addStringParameter("@billToCountryCode", webCartHeader.billToCountryCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shipToName", webCartHeader.shipToName, 50);
                databaseQuery.addStringParameter("@shipToName2", webCartHeader.shipToName2, 50);
                databaseQuery.addStringParameter("@shipToAddress", webCartHeader.shipToAddress, 50);
                databaseQuery.addStringParameter("@shipToAddress2", webCartHeader.shipToAddress2, 50);
                databaseQuery.addStringParameter("@shipToPostCode", webCartHeader.shipToPostCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shipToCity", webCartHeader.shipToCity, 50);
                databaseQuery.addStringParameter("@shipToCountryCode", webCartHeader.shipToCountryCode.ToUpper(), 20);
                databaseQuery.addBoolParameter("@addShipToAddress", webCartHeader.addShipToAddress);
                databaseQuery.addStringParameter("@contactName", webCartHeader.contactName, 30);
                databaseQuery.addStringParameter("@phoneNo", webCartHeader.phoneNo, 20);
                databaseQuery.addStringParameter("@email", webCartHeader.email, 50);
                databaseQuery.addStringParameter("@customerOrderNo", webCartHeader.customerOrderNo.ToUpper(), 20);
                databaseQuery.addStringParameter("@noteOfGoods", webCartHeader.noteOfGoods, 100);
                databaseQuery.addStringParameter("@clientIpAddress", webCartHeader.clientIpAddress, 30);
                databaseQuery.addStringParameter("@clientUserAgent", webCartHeader.clientUserAgent, 250);
                databaseQuery.addDateTimeParameter("@orderCreatedDateTime", DateTime.Now);
                databaseQuery.addStringParameter("@languageCode", webCartHeader.languageCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@currencyCode", webCartHeader.currencyCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@webPaymentMethodCode", webCartHeader.webPaymentMethodCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@campaignCode", webCartHeader.campaignCode.ToUpper(), 20);
                databaseQuery.addDecimalParameter("@freightFee", webCartHeader.freightFee);
                databaseQuery.addDecimalParameter("@administrativeFee", webCartHeader.adminFee);
                databaseQuery.addStringParameter("@freightGLAccountNo", webCartHeader.webShipmentMethod.glAccountNo.ToUpper(), 20);
                databaseQuery.addStringParameter("@adminGLAccountNo", webCartHeader.webPaymentMethod.glAccountNo.ToUpper(), 20);
                databaseQuery.addStringParameter("@freightVATProdPostingGroup", webCartHeader.webShipmentMethod.vatProdPostingGroup.ToUpper(), 20);
                databaseQuery.addStringParameter("@adminVATProdPostingGroup", webCartHeader.webPaymentMethod.vatProdPostingGroup.ToUpper(), 20);
                databaseQuery.addStringParameter("@shippingAgentCode", webCartHeader.webShipmentMethod.shippingAgentCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shipmentMethodCode", webCartHeader.webShipmentMethod.shipmentMethodCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@webShipmentMethodCode", webCartHeader.webShipmentMethodCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shippingAgentServiceCode", webCartHeader.webShipmentMethod.shippingAgentServiceCode.ToUpper(), 20);

                int shippingAdvice = 0;
                if (webCartHeader.shippingAdvice == "1") shippingAdvice = 1;
                databaseQuery.addIntParameter("@shippingAdviceOption", shippingAdvice);


                databaseQuery.addDateTimeParameter("@shipmentDate", webCartHeader.shipmentDate);
                databaseQuery.addStringParameter("@messageText", webCartHeader.message, 250);

                int prepaymentExpected = 0;
                if (webCartHeader.webPaymentMethod.type > 0) prepaymentExpected = 1;
                databaseQuery.addIntParameter("@prepaymentExptected", prepaymentExpected);
                databaseQuery.addIntParameter("@deleted", prepaymentExpected);

                databaseQuery.execute();

            }
            catch (Exception e)
            {
                databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Header Extra Field") + "] WHERE [Document No_] = @no");
                databaseQuery.addStringParameter("@no", orderNo, 20);
                databaseQuery.execute();

                databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] WHERE [Document No_] = @no");
                databaseQuery.addStringParameter("@no", orderNo, 20);
                databaseQuery.execute();

                throw new Exception(e.Message);
            }
            infojetContext.systemDatabase.close();

        }


        /* Depricated */
        private string getNextOrderNo()
        {
            string nextNo = "";
            string startingNo = "";
            string lastNo = "";
            int lineNo = 0;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Starting No_], [Last No_ Used], [Line No_] FROM [" + infojetContext.systemDatabase.getTableName("No_ Series Line") + "] WHERE [Series Code] = @orderNoSeries AND ([Starting Date] < GETDATE() OR [Starting Date] = '1753-01-01 00:00:00') AND [Starting No_] <> '' ORDER BY [Starting Date] DESC");
            databaseQuery.addStringParameter("@orderNoSeries", infojetContext.webSite.orderNoSeries, 10);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                startingNo = dataReader.GetValue(0).ToString();
                lastNo = dataReader.GetValue(1).ToString();
                lineNo = dataReader.GetInt32(2);
            }

            dataReader.Close();

            if (lastNo == "") lastNo = startingNo;

            lastNo = incStr(lastNo);

            if (lineNo == 0) throw new Exception("No ordernumber found in series "+infojetContext.webSite.orderNoSeries);

            databaseQuery = infojetContext.systemDatabase.prepare("UPDATE [" + infojetContext.systemDatabase.getTableName("No_ Series Line") + "] SET [Last No_ Used] = @lastNoUsed WHERE [Series Code] = @seriesCode AND [Line No_] = @lineNo");
            databaseQuery.addStringParameter("@lastNoUsed", lastNo, 20);
            databaseQuery.addStringParameter("@seriesCode", infojetContext.webSite.orderNoSeries, 10);
            databaseQuery.addIntParameter("@lineNo", lineNo);
            databaseQuery.execute();


            return nextNo;
        }

        /* Depricated */
        private string incStr(string numberStr)
        {
            var prefix = System.Text.RegularExpressions.Regex.Match(numberStr, "^\\D+").Value;
            var number = System.Text.RegularExpressions.Regex.Replace(numberStr, "^\\D+", "");
            var i = int.Parse(number) + 1;
            var newString = prefix + i.ToString(new string('0', number.Length));
            return newString;
        }

        public bool checkMinAmount()
        {
            float minOrderAmount = 0;
            if (webCartHeader.webPaymentMethod != null)
            {
                minOrderAmount = webCartHeader.webPaymentMethod.minOrderAmount;
            }
            if (infojetContext.webSite.showPriceInclVAT)
            {
                if (minOrderAmount <= getTotalAmountInclVat()) return true;
            }
            else
            {
                if (minOrderAmount <= getTotalAmount()) return true;
            }

            
            return false;
        }


        public string webSiteCode { get { return _webSiteCode; } }
        public string code { get { return _code; } }
        public string description { get { return _description; } }
        public string step1WebPageCode { get { return _step1WebPageCode; } }
        public string step2WebPageCode { get { return _step2WebPageCode; } }
        public string step3WebPageCode { get { return _step3WebPageCode; } }
        public string step4WebPageCode { get { return _step4WebPageCode; } }
        public string step5WebPageCode { get { return _step5WebPageCode; } }
        public string orderConfirmationWebPageCode { get { return _orderConfirmationWebPageCode; } }
        public string loginRegistrationWebPageCode { get { return _loginRegistrationWebPageCode; } }
        public string paymentWebPageCode { get { return _paymentWebPageCode; } }
        public string customerSearchWebPageCode { get { return _customerSearchWebPageCode; } }
        public bool allowQuantityChange { get { return _allowQuantityChange; } }
        public bool showLineFieldExtra1 { get { return _showLineFieldExtra1; } }
        public bool showLineFieldExtra2 { get { return _showLineFieldExtra2; } }
        public bool showLineFieldExtra3 { get { return _showLineFieldExtra3; } }
        public bool showLineFieldExtra4 { get { return _showLineFieldExtra4; } }
        public bool showLineFieldExtra5 { get { return _showLineFieldExtra5; } }
        public bool allowLineReference { get { return _allowLineReference; } }

        public string defaultPaymentMethod { get { return _defaultPaymentMethod; } }
        public bool allowDiscountCode { get { return _allowDiscountCode; } }

        public string formCode { get { return _formCode; } }

        public string lastErrorMessage { get { return _lastErrorMessage; } }

        public WebCartHeader webCartHeader { get { return _webCartHeader; } }

	}
}
