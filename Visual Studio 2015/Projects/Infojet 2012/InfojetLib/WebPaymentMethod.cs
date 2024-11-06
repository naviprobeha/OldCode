using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPaymentMethod.
	/// </summary>
	public class WebPaymentMethod
	{

		private string _webSiteCode;
		private string _code;
        private string _description;
        private string _paymentMethodCode;
        private int _type;
        private string _serviceParameter;
        private string _serviceCode;
        private string _text;
        private float _amount;
        private float _amountInclVat;
        private string _formatedAmount;
        private string _formatedAmountInclVat;
        private string _glAccountNo;
        private string _vatProdPostingGroup;
        private bool _requireFreightFee;
        private float _upperOrderLimitAmount;
        private float _minOrderAmount;
        
        private bool _checkCreditLimit;
        private bool _checkDueInvoices;
        private bool _checkAllInvoices;
        private bool _checkOrders;
        private bool _checkCart;

        private bool _specifyCheckouts;

		private Infojet infojetContext;

        public WebPaymentMethod() { }

		public WebPaymentMethod(Infojet infojetContext, string webSiteCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this._webSiteCode = webSiteCode;
			this._code = code;

			this.infojetContext = infojetContext;

			getFromDatabase();
		}

		
		public WebPaymentMethod(Infojet infojetContext, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.infojetContext = infojetContext;

			this._webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
			this._code = dataRow.ItemArray.GetValue(1).ToString();
			this._description = dataRow.ItemArray.GetValue(2).ToString();
			this._paymentMethodCode = dataRow.ItemArray.GetValue(3).ToString();
			this._type = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this._serviceParameter = dataRow.ItemArray.GetValue(5).ToString();
            this._serviceCode = dataRow.ItemArray.GetValue(6).ToString();

            this._requireFreightFee = false;
            if (dataRow.ItemArray.GetValue(7).ToString() == "1") _requireFreightFee = true;

            this._checkCreditLimit = false;
            if (dataRow.ItemArray.GetValue(8).ToString() == "1") _checkCreditLimit = true;

            this._checkDueInvoices = false;
            if (dataRow.ItemArray.GetValue(9).ToString() == "1") this._checkDueInvoices = true;

            this._checkAllInvoices = false;
            if (dataRow.ItemArray.GetValue(10).ToString() == "1") this._checkAllInvoices = true;

            this._checkOrders = false;
            if (dataRow.ItemArray.GetValue(11).ToString() == "1") this._checkOrders = true;

            this._checkCart = false;
            if (dataRow.ItemArray.GetValue(12).ToString() == "1") this._checkCart = true;

            this._specifyCheckouts = false;
            if (dataRow.ItemArray.GetValue(13).ToString() == "1") this._specifyCheckouts = true;

		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Payment Method Code], [Type], [Service Parameter], [Service Code], [Require Freight Fee], [Check Credit Limit], [Check Due Invoices], [Check All Invoices], [Check Orders], [Check Cart], [Specify Checkouts] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{
				_webSiteCode = dataReader.GetValue(0).ToString();
				_code = dataReader.GetValue(1).ToString();
				_description = dataReader.GetValue(2).ToString();
				_paymentMethodCode = dataReader.GetValue(3).ToString();
				_type = int.Parse(dataReader.GetValue(4).ToString());
				_serviceParameter = dataReader.GetValue(5).ToString();
                _serviceCode = dataReader.GetValue(6).ToString();

                this._requireFreightFee = false;
                if (dataReader.GetValue(7).ToString() == "1") this._requireFreightFee = true;

                this._checkCreditLimit = false;
                if (dataReader.GetValue(8).ToString() == "1") this._checkCreditLimit = true;

                this._checkDueInvoices = false;
                if (dataReader.GetValue(9).ToString() == "1") this._checkDueInvoices = true;

                this._checkAllInvoices = false;
                if (dataReader.GetValue(10).ToString() == "1") this._checkAllInvoices = true;

                this._checkOrders = false;
                if (dataReader.GetValue(11).ToString() == "1") this._checkOrders = true;

                this._checkCart = false;
                if (dataReader.GetValue(12).ToString() == "1") this._checkCart = true;

                this._specifyCheckouts = false;
                if (dataReader.GetValue(13).ToString() == "1") this._specifyCheckouts = true;

			}

			dataReader.Close();
			
		}

        public DataSet getDetails(string languageCode, WebCartHeader webCartHeader, string currencyCode)
        {
            if (currencyCode == infojetContext.generalLedgerSetup.lcyCode) currencyCode = "";
            Customer customer = new Customer(infojetContext, webCartHeader.customerNo);

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method Detail") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Payment Method Code] = @code AND [Language Code] = @languageCode AND [Available To] = 2 AND [Code] = @customerNo AND ([Starting Date] <= @todayDate OR [Starting Date] = '1753-01-01') AND ([Ending Date] >= @todayDate OR [Ending Date] = '1753-01-01') AND [Currency Code] = @currencyCode AND [Enabled] = 1");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
            databaseQuery.addStringParameter("customerNo", customer.no, 20);
            databaseQuery.addDateTimeParameter("todayDate", DateTime.Today);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method Detail") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Payment Method Code] = @code AND [Language Code] = @languageCode AND [Available To] = 1 AND [Code] = @customerPriceGroupCode AND ([Starting Date] <= @todayDate OR [Starting Date] = '1753-01-01') AND ([Ending Date] >= @todayDate OR [Ending Date] = '1753-01-01') AND [Currency Code] = @currencyCode AND [Enabled] = 1");
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addStringParameter("code", code, 20);
                databaseQuery.addStringParameter("languageCode", languageCode, 20);
                databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
                databaseQuery.addStringParameter("customerPriceGroupCode", customer.customerPriceGroup, 20);
                databaseQuery.addDateTimeParameter("todayDate", DateTime.Today);

                sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {

                    databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method Detail") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Payment Method Code] = @code AND [Language Code] = @languageCode AND [Available To] = 3 AND [Code] IN (SELECT [Web Shipment Zone Code] FROM ["+infojetContext.systemDatabase.getTableName("Web Shipment Zone Area")+"] WHERE [Country Code] = @countryCode) AND ([Starting Date] <= @todayDate OR [Starting Date] = '1753-01-01') AND ([Ending Date] >= @todayDate OR [Ending Date] = '1753-01-01') AND [Currency Code] = @currencyCode AND [Enabled] = 1");
                    databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                    databaseQuery.addStringParameter("code", code, 20);
                    databaseQuery.addStringParameter("languageCode", languageCode, 20);
                    databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
                    databaseQuery.addStringParameter("countryCode", webCartHeader.shipToCountryCode, 20);
                    databaseQuery.addDateTimeParameter("todayDate", DateTime.Today);

                    sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                    dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                    if (dataSet.Tables[0].Rows.Count == 0)
                    {

                        if (infojetContext.userSession != null)
                        {
                            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method Detail") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Payment Method Code] = @code AND [Language Code] = @languageCode AND [Available To] = 4 AND [Code] IN (SELECT [Web User Group Code] FROM [" + infojetContext.systemDatabase.getTableName("Web User Account Group") + "] WHERE [No_] = @webUserAccountNo) AND ([Starting Date] <= @todayDate OR [Starting Date] = '1753-01-01') AND ([Ending Date] >= @todayDate OR [Ending Date] = '1753-01-01') AND [Currency Code] = @currencyCode AND [Enabled] = 1");
                            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                            databaseQuery.addStringParameter("code", code, 20);
                            databaseQuery.addStringParameter("languageCode", languageCode, 20);
                            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
                            databaseQuery.addStringParameter("countryCode", webCartHeader.shipToCountryCode, 20);
                            databaseQuery.addDateTimeParameter("todayDate", DateTime.Today);
                            databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);

                            sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                            dataSet = new DataSet();
                            sqlDataAdapter.Fill(dataSet);
                        }

                        if (dataSet.Tables[0].Rows.Count == 0)
                        {

                            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method Detail") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Payment Method Code] = @code AND [Language Code] = @languageCode AND [Available To] = 0 AND ([Starting Date] <= @todayDate OR [Starting Date] = '1753-01-01') AND ([Ending Date] >= @todayDate OR [Ending Date] = '1753-01-01') AND [Currency Code] = @currencyCode AND [Enabled] = 1");
                            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                            databaseQuery.addStringParameter("code", code, 20);
                            databaseQuery.addStringParameter("languageCode", languageCode, 20);
                            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
                            databaseQuery.addDateTimeParameter("todayDate", DateTime.Today);

                            sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                            dataSet = new DataSet();
                            sqlDataAdapter.Fill(dataSet);

                        }
                    }
                }
            }
            return (dataSet);

        }

        public void applyDetails(WebPaymentMethodDetail webPaymentMethodDetail)
        {
            if ((webPaymentMethodDetail.description != "") && (webPaymentMethodDetail.description != null)) _description = webPaymentMethodDetail.description;
            _text = webPaymentMethodDetail.text;
            _amount = webPaymentMethodDetail.amount;

            Customer currentCustomer = infojetContext.getCurrentCustomer();
            if (currentCustomer != null) 
            {
                _amountInclVat = webPaymentMethodDetail.amount * webPaymentMethodDetail.getVatFactor(currentCustomer);
            }
            else
            {
                _amountInclVat = (float)(webPaymentMethodDetail.amount * 1.25);
            }

            _formatedAmount = infojetContext.systemDatabase.formatCurrency(amount);
            _formatedAmountInclVat = infojetContext.systemDatabase.formatCurrency(amountInclVat);
            _glAccountNo = webPaymentMethodDetail.glAccountNo;
            _vatProdPostingGroup = webPaymentMethodDetail.vatProdPostingGroup;

            _upperOrderLimitAmount = getUpperOrderLimitAmount();
            _minOrderAmount = getMinAmountLimit();
        }

        public PaymentModule getPaymentModule(WebCheckout webCheckout)
        {
            if (_type == 0) return null;
            if (_type == 1) return new PaymentModuleDibs(infojetContext, webCheckout);

            return null;

        }

        public bool checkCredit(WebCheckout webCheckout)
        {
            if (infojetContext.userSession != null)
            {
                Customer customer = new Customer(infojetContext, webCheckout.webCartHeader.customerNo);
                float creditLimit = customer.creditLimitLcy;
                float amount = 0;

                if (_checkAllInvoices)
                {
                    DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Remaining Amount]) FROM [" + infojetContext.systemDatabase.getTableName("Cust_ Ledger Entry") + "] WHERE [Customer No_] = @customerNo AND [Open] = 1");
                    databaseQuery.addStringParameter("customerNo", webCheckout.webCartHeader.customerNo, 20);

                    
                    SqlDataReader dataReader = databaseQuery.executeQuery();
                    if (dataReader.Read())
                    {
                        if (!dataReader.IsDBNull(0)) amount = float.Parse(dataReader.GetValue(0).ToString());
                    }
                    dataReader.Close();

                    if (creditLimit < amount) return false;
                }
                else
                {
                    if (_checkDueInvoices)
                    {
                        DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Remaining Amount]) FROM [" + infojetContext.systemDatabase.getTableName("Cust_ Ledger Entry") + "] WHERE [Customer No_] = @customerNo AND [Open] = 1 AND [Due Date] < GETDATE()");
                        databaseQuery.addStringParameter("customerNo", webCheckout.webCartHeader.customerNo, 20);

                        
                        SqlDataReader dataReader = databaseQuery.executeQuery();
                        if (dataReader.Read())
                        {
                            if (!dataReader.IsDBNull(0)) amount = float.Parse(dataReader.GetValue(0).ToString());
                        }
                        dataReader.Close();

                        if (creditLimit < amount) return false;

                    }

                }

                if (_checkOrders)
                {
                    DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Outstanding Amount]) FROM [" + infojetContext.systemDatabase.getTableName("Sales Line") + "] WHERE [Sell-to Customer No_] = @customerNo AND [Document Type] = 1");
                    databaseQuery.addStringParameter("customerNo", webCheckout.webCartHeader.customerNo, 20);

                    SqlDataReader dataReader = databaseQuery.executeQuery();
                    if (dataReader.Read())
                    {
                        if (!dataReader.IsDBNull(0)) amount = amount + float.Parse(dataReader.GetValue(0).ToString());
                    }
                    dataReader.Close();

                    if (creditLimit < amount) return false;

                    databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM(l.[Amount]) FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Sales Header") + "] h WHERE l.[Document Type] = '1' AND h.[Document Type] = l.[Document Type] AND h.[No_] = l.[Document No_] AND h.[Transfered] = 0 AND h.[Deleted] = 0 AND h.[Sell-to Customer No_] = @customerNo");
                    databaseQuery.addStringParameter("customerNo", webCheckout.webCartHeader.customerNo, 20);

                    dataReader = databaseQuery.executeQuery();
                    if (dataReader.Read())
                    {
                        if (!dataReader.IsDBNull(0)) amount = amount + float.Parse(dataReader.GetValue(0).ToString());
                    }
                    dataReader.Close();


                    if (creditLimit < amount) return false;

                }

                if (_checkCart)
                {
                    amount = amount + infojetContext.cartHandler.getTotalCartAmount()+infojetContext.cartHandler.getTotalCartVatAmount();
                    if (creditLimit < amount) return false;
                }
                return true;
            }
            return false;
        }

        private float getUpperOrderLimitAmount()
        {
            float upperOrderAmountLimit = 0;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Order Amount Limit] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment_Payment Order Lmt") + "] WHERE [Web Site Code] = @webSiteCode AND [Type] = 1 AND [Code] = @code AND [Currency Code] = @currencyCode");
            databaseQuery.addStringParameter("webSiteCode", this.webSiteCode, 20);
            databaseQuery.addStringParameter("code", this.code, 20);
            databaseQuery.addStringParameter("currencyCode", infojetContext.currencyCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) upperOrderAmountLimit = float.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            return upperOrderAmountLimit;
        }

        private float getMinAmountLimit()
        {
            float minAmountLimit = 0;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Order Amount Limit] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment_Payment Order Lmt") + "] WHERE [Web Site Code] = @webSiteCode AND [Type] = 1 AND [Code] = @code AND [Currency Code] = @currencyCode AND [Deny Order] = 1");
            databaseQuery.addStringParameter("webSiteCode", this.webSiteCode, 20);
            databaseQuery.addStringParameter("code", this.code, 20);
            databaseQuery.addStringParameter("currencyCode", infojetContext.currencyCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) minAmountLimit = float.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            return minAmountLimit;
        }

        public bool checkPaymentMethodConnection(WebCheckout webCheckout)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Checkout Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment_Shipment Conn_") + "] WHERE [Web Site Code] = @webSiteCode AND [Type] = 0 AND [Code] = @code AND [Web Checkout Code] = @webCheckoutCode");
            databaseQuery.addStringParameter("webSiteCode", this.webSiteCode, 20);
            databaseQuery.addStringParameter("code", this.code, 20);
            databaseQuery.addStringParameter("webCheckoutCode", webCheckout.code, 20);

            bool found = false;

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                found = true;
            }
            dataReader.Close();

            return found;
        }



        public string webSiteCode { get { return _webSiteCode; } }
        public string code { get { return _code; } }
        public string description { get { return _description; } }
        public string paymentMethodCode { get { return _paymentMethodCode; } }
        public int type { get { return _type; } }
        public string serviceParameter { get { return _serviceParameter; } }
        public string text { get { return _text; } set { _text = value; } }
        public float amount { get { return _amount; } }
        public float amountInclVat { get { return _amountInclVat; } }
        public string formatedAmount { get { return _formatedAmount; } }
        public string formatedAmountInclVat { get { return _formatedAmountInclVat; } }
        public string glAccountNo { get { return _glAccountNo; } }
        public string vatProdPostingGroup { get { return _vatProdPostingGroup; } }
        public string serviceCode { get { return _serviceCode; } }
        public bool requireFreightFee { get { return _requireFreightFee; } }
        public bool checkCreditLimit { get { return _checkCreditLimit; } }
        public bool checkDueInvoices { get { return _checkDueInvoices; } }
        public bool checkAllInvoices { get { return _checkAllInvoices; } }
        public bool checkOrders { get { return _checkOrders; } }
        public bool checkCart { get { return _checkCart; } }
        public bool specifyCheckouts { get { return _specifyCheckouts; } }
        public float upperOrderAmountLimit { get { return _upperOrderLimitAmount; } }
        public float minOrderAmount { get { return _minOrderAmount; } }
        public string presentationText { get { return "<b>" + description + "</b><br/>" + text + "&nbsp;"; } }

    }
}
