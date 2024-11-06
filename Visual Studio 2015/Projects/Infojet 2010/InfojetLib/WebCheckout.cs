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

		private string _lastErrorMessage = "";
		private string _lastOrderNoReceived = "";

        private string _formCode;

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
                if (infojetContext.userSession != null)
                {
                    _webCartHeader.setUserSession(infojetContext.userSession);
                    _webCartHeader.save();
                }

            }
            

			getFromDatabase();


		}


		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Step 1 Web Page Code], [Step 2 Web Page Code], [Step 3 Web Page Code], [Step 4 Web Page Code], [Step 5 Web Page Code], [Login Reg_ Web Page Code], [Allow Quantity Change], [Show Line Field Extra 1], [Show Line Field Extra 2], [Show Line Field Extra 3], [Show Line Field Extra 4], [Show Line Field Extra 5], [Payment Web Page Code], [Allow Line Reference], [Default Payment Method], [Form Code], [Order Confirm_ Web Page Code], [Customer Search Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Checkout") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
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
			}

			dataReader.Close();
			
		}

		public CartItemCollection getCartLines(Infojet infojetContext)
		{
            CartItemCollection cartItemCollection = new CartItemCollection();

			WebCartLines webCartLines = new WebCartLines(infojetContext.systemDatabase);
			DataSet webCartLinesDataSet = webCartLines.getCartLines(infojetContext.sessionId);

			int i = 0;
			while (i < webCartLinesDataSet.Tables[0].Rows.Count)
			{
                WebCartLine webCartLine = new WebCartLine(infojetContext, webCartLinesDataSet.Tables[0].Rows[i]);
              
                CartItem cartItem = new CartItem(webCartLine);

                Item item = new Item(infojetContext.systemDatabase, cartItem.itemNo);

                ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                cartItem.description = itemTranslation.description;

                cartItem.unitPrice = webCartLine.unitPrice;

                cartItem.amount = cartItem.unitPrice * cartItem.quantity;
                cartItem.formatedAmount = infojetContext.systemDatabase.formatCurrency(cartItem.amount, infojetContext.currencyCode);
                
                cartItem.removeLink = infojetContext.cartHandler.renderRemoveLink(cartItem.lineNo);

                cartItemCollection.Add(cartItem);

				i++;
			}


            return cartItemCollection;
		}

        public CartItemCollection addInventoryInfo(Infojet infojetContext, CartItemCollection cartItemCollection)
        {
            WebCartLines webCartLines = new WebCartLines(infojetContext.systemDatabase);
            DataSet webCartLinesDataSet = webCartLines.getCartLines(infojetContext.sessionId);

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
            WebShipmentMethods webShipmentMethods = new WebShipmentMethods(infojetContext);
            return webShipmentMethods.getWebShipmentMethodCollection(infojetContext.webSite.code, infojetContext.cartHandler.getTotalCartQuantity(), infojetContext.cartHandler.getTotalCartAmount(), 0, infojetContext.languageCode);

        }

        public WebPaymentMethodCollection getPaymentMethods()
        {
            WebPaymentMethods webPaymentMethods = new WebPaymentMethods(infojetContext);
            return webPaymentMethods.getWebPaymentMethodCollection(infojetContext.webSite.code, infojetContext.languageCode);

        }

        public void updateQuantity(int entryNo, int quantity)
        {
            Items items = new Items(); 
            WebCartLine webCartLine = new WebCartLine(infojetContext, entryNo);

            try
            {
                Item item = new Item(infojetContext.systemDatabase, webCartLine.itemNo);

                webCartLine.quantity = quantity;

                Hashtable itemInfoTable = items.getItemInfo(item, infojetContext, (int)webCartLine.quantity);

                if (itemInfoTable.Contains(item.no))
                {
                    webCartLine.unitPrice = ((ItemInfo)itemInfoTable[item.no]).unitPrice;
                }
                else
                {
                    SalesPrices salesPrices = new SalesPrices(infojetContext.systemDatabase, infojetContext);
                    SalesPrice salesPrice = salesPrices.getItemPrice(webCartLine.getItem(), infojetContext.userSession, infojetContext.currencyCode, (int)webCartLine.quantity);
                    webCartLine.unitPrice = salesPrice.unitPrice;
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
            if (infojetContext.userSession == null)
            {
                WebPage newUserWebPage = new WebPage(infojetContext, infojetContext.webSite.code, loginRegistrationWebPageCode);
                infojetContext.redirect(newUserWebPage.getUrl());
            }
        }

		private void sendOrder()
		{
            CartItemCollection cartItemCollection = this.getCartLines(infojetContext);
            if (cartItemCollection.Count > 0)
            {
				ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "createOrder", this.webCartHeader));
				if (!appServerConnection.processRequest())
				{
                    if (appServerConnection.serviceResponse != null) _lastErrorMessage = appServerConnection.serviceResponse.errorMessage;
                    this._lastOrderNoReceived = "";
				}
				else
				{
					this._lastOrderNoReceived = appServerConnection.serviceResponse.orderNo;

                    webCartHeader.deleteLines();
                    webCartHeader.delete();


                    WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, this.orderConfirmationWebPageCode);
                    infojetContext.redirect(webPage.getUrl() + "&orderNo=" + this._lastOrderNoReceived + "&docType=0&docNo=" + this._lastOrderNoReceived);
                }

				
			}
			else
			{
                WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, this.step2WebPageCode);
				infojetContext.redirect(webPage.getUrl());
			}
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
            Panel formPanel = null;

            WebForm webForm = new WebForm(infojetContext, this.formCode);

            webCartToForm(webCartHeader, ref webForm);

            formPanel = webForm.getFormPanel();

            if (this._lastErrorMessage != "")
            {
                Label errorLabel = new Label();
                errorLabel.Text = _lastErrorMessage;
                errorLabel.CssClass = "errorMessage";
                formPanel.Controls.Add(errorLabel);
            }

            Button nextButton = new Button();
            nextButton.Text = infojetContext.translate("NEXT");
            nextButton.ValidationGroup = webForm.code;
            nextButton.CssClass = "Button";
            nextButton.Click += new EventHandler(nextButton_Click);
            nextButton.ID = "btn_submit";

            formPanel.Controls.Add(nextButton);



            return formPanel;

        }

        void nextButton_Click(object sender, EventArgs e)
        {
            WebForm webForm = new WebForm(infojetContext, this.formCode);
            WebFormDocument webFormDocument = webForm.readForm((Panel)((Button)sender).Parent);

            formToWebCart(webFormDocument, ref _webCartHeader);
            webCartHeader.save();

            goNext();
        }

        private void formToWebCart(WebFormDocument webFormDocument, ref WebCartHeader webCartHeader)
        {
            int i = 0;

            while (i < webFormDocument.keyList.Count)
            {
                string key = (string)webFormDocument.keyList[i];

                WebFormField webFormField = new WebFormField(infojetContext, formCode, key);
                if (webFormField.connectionType == 0) webCartHeader.setExtraField(webFormField.code, webFormDocument.valueList[i].ToString());
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
                if (webFormField.connectionType == 13) webCartHeader.setExtraField(webFormField.code, webFormDocument.valueList[i].ToString());
                if (webFormField.connectionType == 14) webCartHeader.email = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 15) webCartHeader.phoneNo = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 16) webCartHeader.setExtraField(webFormField.code, webFormDocument.valueList[i].ToString());
                if (webFormField.connectionType == 17) webCartHeader.setExtraField(webFormField.code, webFormDocument.valueList[i].ToString());
                if (webFormField.connectionType == 18) webCartHeader.contactName = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 22) webCartHeader.customerOrderNo = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 23) webCartHeader.noteOfGoods = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 24) webCartHeader.message = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 25) webCartHeader.shippingAdvice = webFormDocument.valueList[i].ToString();
                if (webFormField.connectionType == 26) webCartHeader.shipmentDate = DateTime.Parse(webFormDocument.valueList[i].ToString());



                i++;
            }


        }

        public float getTotalAmount()
        {
            return infojetContext.cartHandler.getTotalCartAmount() + webCartHeader.freightFee + webCartHeader.adminFee;
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
                adminVat = webCartHeader.webPaymentMethod.amountInclVat - webCartHeader.webPaymentMethod.amount;
            }

            return infojetContext.cartHandler.getTotalCartVatAmount() + freightVat + adminVat;
        }

        public float getTotalAmountInclVat()
        {
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
                if (webCartHeader.webPaymentMethod.type > 0)
                {
                    WebPage paymentWebPage = new WebPage(infojetContext, infojetContext.webSite.code, this.paymentWebPageCode);
                    infojetContext.redirect(paymentWebPage.getUrl());
                }
                else
                {
                    sendOrder();
                }
            }

        }

        public string getUserControl()
        {
            string step = "";
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
                    if (webCartHeader.extraFields.Contains(webFormField.code)) valueTable.Add(webFormField.code, webCartHeader.extraFields[webFormField.code].ToString());
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
                    if (webCartHeader.extraFields.Contains(webFormField.code)) valueTable.Add(webFormField.code, webCartHeader.extraFields[webFormField.code].ToString());
                }
                if (webFormField.connectionType == 14) valueTable.Add(webFormField.code, webCartHeader.email);
                if (webFormField.connectionType == 15) valueTable.Add(webFormField.code, webCartHeader.phoneNo);
                if (webFormField.connectionType == 16)
                {
                    if (webCartHeader.extraFields.Contains(webFormField.code)) valueTable.Add(webFormField.code, webCartHeader.extraFields[webFormField.code].ToString());
                }
                if (webFormField.connectionType == 17)
                {
                    if (webCartHeader.extraFields.Contains(webFormField.code)) valueTable.Add(webFormField.code, webCartHeader.extraFields[webFormField.code].ToString());
                }

                if (webFormField.connectionType == 18) valueTable.Add(webFormField.code, webCartHeader.contactName);
                if (webFormField.connectionType == 21) valueTable.Add(webFormField.code, webCartHeader.customerNo);
                if (webFormField.connectionType == 22) valueTable.Add(webFormField.code, webCartHeader.customerOrderNo);
                if (webFormField.connectionType == 23) valueTable.Add(webFormField.code, webCartHeader.noteOfGoods);
                if (webFormField.connectionType == 24) valueTable.Add(webFormField.code, webCartHeader.message);
                if (webFormField.connectionType == 25) valueTable.Add(webFormField.code, webCartHeader.shippingAdvice);
                if (webFormField.connectionType == 26) valueTable.Add(webFormField.code, webCartHeader.shipmentDate.ToString("yyyy-MM-dd"));


                i++;
            }

            webForm.setFormValues(valueTable);

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

        public string formCode { get { return _formCode; } }

        public string lastErrorMessage { get { return _lastErrorMessage; } }

        public WebCartHeader webCartHeader { get { return _webCartHeader; } }

	}
}
