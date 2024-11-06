using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebPaymentMethod.
    /// </summary>
    public class WebShipmentMethod
    {

        private string _webSiteCode;
        private string _code;
        private string _description;
        private string _shipmentMethodCode;
        private string _shippingAgentCode;
        private string _shippingAgentServiceCode;
        private int _levelType;
        private bool _active;
        private string _text;
        private bool _defaultValue;
        private string _defaultText;
        private float _amount;
        private float _amountInclVat;
        private string _formatedAmount;
        private string _formatedAmountInclVat;
        private string _glAccountNo = "";
        private string _vatProdPostingGroup = "";
        private bool _specifyCheckouts;
        private bool _nightTimeDelivery = false;
        private bool _dayTimeDelivery = false;

        private Infojet infojetContext;

        public WebShipmentMethod() { }

        public WebShipmentMethod(Infojet infojetContext, string webSiteCode, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.webSiteCode = webSiteCode;
            this.code = code;

            this.infojetContext = infojetContext;

            getFromDatabase();
        }


        public WebShipmentMethod(Infojet infojetContext, DataRow dataRow)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            this.code = dataRow.ItemArray.GetValue(1).ToString();
            this.description = dataRow.ItemArray.GetValue(2).ToString();
            this.shipmentMethodCode = dataRow.ItemArray.GetValue(3).ToString();
            this.shippingAgentCode = dataRow.ItemArray.GetValue(4).ToString();
            this.shippingAgentServiceCode = dataRow.ItemArray.GetValue(5).ToString();
            this.levelType = int.Parse(dataRow.ItemArray.GetValue(6).ToString());

            this.active = false;
            if (dataRow.ItemArray.GetValue(7).ToString() == "1") this.active = true;

            this.specifyCheckouts = false;
            if (dataRow.ItemArray.GetValue(8).ToString() == "1") this.specifyCheckouts = true;
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Shipment Method Code], [Shipping Agent Code], [Shipping Agent Service Code], [Level Type], [Active] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment Method") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webSiteCode = dataReader.GetValue(0).ToString();
                code = dataReader.GetValue(1).ToString();
                description = dataReader.GetValue(2).ToString();
                shipmentMethodCode = dataReader.GetValue(3).ToString();
                shippingAgentCode = dataReader.GetValue(4).ToString();
                shippingAgentServiceCode = dataReader.GetValue(5).ToString();
                levelType = int.Parse(dataReader.GetValue(6).ToString());

                this.active = false;
                if (dataReader.GetValue(7).ToString() == "1") this.active = true;
            }

            dataReader.Close();
        }

        public DataSet getDetails(string languageCode, string countryCode, string currencyCode, string postCode)
        {
            if (currencyCode == infojetContext.generalLedgerSetup.lcyCode) currencyCode = "";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Shipment Method Code], [Language Code], [Web Shipment Zone], [Description], [Text], [From], [To], [Amount], [G_L Account No_], [VAT Prod_ Posting Group], [Currency Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment Method Detail") + "] d WHERE [Web Site Code] = @webSiteCode AND [Web Shipment Method Code] = @code AND [Language Code] = @languageCode AND [Currency Code] = @currencyCode AND d.[Web Shipment Zone] IN (SELECT [Web Shipment Zone Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment Zone Area") + "] WHERE [Country Code] = @countryCode AND (([From Post Code] <= @postCode OR [From Post Code] = '') AND ([To Post Code] > @postCode OR [To Post Code] = '')))");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
            databaseQuery.addStringParameter("countryCode", countryCode, 20);
            databaseQuery.addStringParameter("postCode", postCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

        public DataSet getDetails(string languageCode, string currencyCode)
        {
            if (currencyCode == infojetContext.generalLedgerSetup.lcyCode) currencyCode = "";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Shipment Method Code], [Language Code], [Web Shipment Zone], [Description], [Text], [From], [To], [Amount], [G_L Account No_], [VAT Prod_ Posting Group], [Currency Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment Method Detail") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Shipment Method Code] = @code AND [Language Code] = @languageCode AND [Currency Code] = @currencyCode AND [Web Shipment Zone] = ''");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

        public void applyDetails(WebShipmentMethodDetail webShipmentMethodDetail)
        {
            if ((webShipmentMethodDetail.description != "") && (webShipmentMethodDetail.description != null)) description = webShipmentMethodDetail.description;
            text = webShipmentMethodDetail.text;
            amount = webShipmentMethodDetail.amount;

            Customer currentCustomer = infojetContext.getCurrentCustomer();
            if (currentCustomer != null) 
            {
                amountInclVat = webShipmentMethodDetail.amount * webShipmentMethodDetail.getVatFactor(currentCustomer);
            }
            else
            {
                amountInclVat = (float)(webShipmentMethodDetail.amount * 1.25);
            }

            formatedAmount = infojetContext.systemDatabase.formatCurrency(amount);
            formatedAmountInclVat = infojetContext.systemDatabase.formatCurrency(amountInclVat);
            glAccountNo = webShipmentMethodDetail.glAccountNo;
            vatProdPostingGroup = webShipmentMethodDetail.vatProdPostingGroup;

            if (defaultValue)
            {
                WebTextConstantValue webTextConstantValue = new WebTextConstantValue(infojetContext.systemDatabase, this.webSiteCode, "DEFAULT SHP METHOD", webShipmentMethodDetail.languageCode);
                this.defaultText = webTextConstantValue.textValue;
            }
        }

        public bool checkShipmentMethodConnection(WebCheckout webCheckout)
        {
            if (webCheckout == null) return false;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Checkout Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment_Shipment Conn_") + "] WHERE [Web Site Code] = @webSiteCode AND [Type] = 1 AND [Code] = @code AND [Web Checkout Code] = @webCheckoutCode");
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

 
        public string webSiteCode { set { _webSiteCode = value; } get { return _webSiteCode; } }
        public string code { set { _code = value; } get { return _code; } }
        public string description { set { _description = value; } get { return _description; } }
        public string shipmentMethodCode { set { _shipmentMethodCode = value; } get { return _shipmentMethodCode; } }
        public string shippingAgentCode { set { _shippingAgentCode = value; } get { return _shippingAgentCode; } }
        public string shippingAgentServiceCode { set { _shippingAgentServiceCode = value; } get { return _shippingAgentServiceCode; } }
        public int levelType { set { _levelType = value; } get { return _levelType; } }
        public bool active { set { _active = value; } get { return _active; } }
        public string text { set { _text = value; } get { return _text; } }
        public bool defaultValue { set { _defaultValue = value; } get { return _defaultValue; } }
        public string defaultText { set { _defaultText = value; } get { return _defaultText; } }
        public string defaultRadioButtonValue { get { if (defaultValue) return "checked=\"checked\""; return ""; } }
        public string defaultDropDownValue { get { if (defaultValue) return "selected=\"selected\""; return ""; } }
        public float amount { set { _amount = value; } get { return _amount; } }
        public float amountInclVat { set { _amountInclVat = value; } get { return _amountInclVat; } }
        public string formatedAmount { set { _formatedAmount = value; } get { return _formatedAmount; } }
        public string formatedAmountInclVat { set { _formatedAmountInclVat = value; } get { return _formatedAmountInclVat; } }
        public string glAccountNo { set { _glAccountNo = value; } get { return _glAccountNo; } }
        public string vatProdPostingGroup { set { _vatProdPostingGroup = value; } get { return _vatProdPostingGroup; } }
        public bool specifyCheckouts { set { _specifyCheckouts = value; } get { return _specifyCheckouts; } }
        public string presentationText { get { return "<b>" + description + "</b><br/>" + text+"&nbsp;"; } }
    }
}
