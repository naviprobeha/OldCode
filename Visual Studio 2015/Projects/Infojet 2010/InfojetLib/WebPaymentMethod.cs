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
        private string _text;
        private float _amount;
        private float _amountInclVat;
        private string _formatedAmount;
        private string _formatedAmountInclVat;
        private string _glAccountNo;

		private Infojet infojetContext;


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
		}

		private void getFromDatabase()
		{

			SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Web Site Code], [Code], [Description], [Payment Method Code], [Type], [Service Parameter] FROM ["+infojetContext.systemDatabase.getTableName("Web Payment Method")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{
				_webSiteCode = dataReader.GetValue(0).ToString();
				_code = dataReader.GetValue(1).ToString();
				_description = dataReader.GetValue(2).ToString();
				_paymentMethodCode = dataReader.GetValue(3).ToString();
				_type = int.Parse(dataReader.GetValue(4).ToString());
				_serviceParameter = dataReader.GetValue(5).ToString();
			}

			dataReader.Close();
			
		}

        public DataSet getDetails(string languageCode, Customer customer, string currencyCode)
        {
            if (currencyCode == infojetContext.generalLedgerSetup.lcyCode) currencyCode = "";

            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM ["+infojetContext.systemDatabase.getTableName("Web Payment Method Detail")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Web Payment Method Code] = '"+this.code+"' AND [Language Code] = '"+languageCode+"' AND [Available To] = 2 AND [Code] = '"+customer.no+"' AND ([Starting Date] <= '"+DateTime.Today.ToString("yyyy-MM-dd")+"' OR [Starting Date] = '1753-01-01') AND ([Ending Date] >= '"+DateTime.Today.ToString("yyyy-MM-dd")+"' OR [Ending Date] = '1753-01-01') AND [Currency Code] = '"+currencyCode+"' AND [Enabled] = 1");
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method Detail") + "] WHERE [Web Site Code] = '" + this.webSiteCode + "' AND [Web Payment Method Code] = '" + this.code + "' AND [Language Code] = '" + languageCode + "' AND [Available To] = 1 AND [Code] = '" + customer.customerPriceGroup + "' AND ([Starting Date] <= '" + DateTime.Today.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01') AND ([Ending Date] >= '" + DateTime.Today.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01') AND [Currency Code] = '" + currencyCode + "' AND [Enabled] = 1");
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Web Payment Method Detail") + "] WHERE [Web Site Code] = '" + this.webSiteCode + "' AND [Web Payment Method Code] = '" + this.code + "' AND [Language Code] = '" + languageCode + "' AND [Available To] = 0 AND ([Starting Date] <= '" + DateTime.Today.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01') AND ([Ending Date] >= '" + DateTime.Today.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01') AND [Currency Code] = '" + currencyCode + "' AND [Enabled] = 1");
                    dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                }

            }
            return (dataSet);

        }

        public void applyDetails(WebPaymentMethodDetail webPaymentMethodDetail)
        {
            if ((webPaymentMethodDetail.description != "") && (webPaymentMethodDetail.description != null)) _description = webPaymentMethodDetail.description;
            _text = webPaymentMethodDetail.text;
            _amount = webPaymentMethodDetail.amount;
            _amountInclVat = webPaymentMethodDetail.amount * webPaymentMethodDetail.getVatFactor(infojetContext.userSession.customer);
            _formatedAmount = infojetContext.systemDatabase.formatCurrency(amount);
            _formatedAmountInclVat = infojetContext.systemDatabase.formatCurrency(amountInclVat);
            _glAccountNo = webPaymentMethodDetail.glAccountNo;
 
        }

        public string webSiteCode { get { return _webSiteCode; } }
        public string code { get { return _code; } }
        public string description { get { return _description; } }
        public string paymentMethodCode { get { return _paymentMethodCode; } }
        public int type { get { return _type; } }
        public string serviceParameter { get { return _serviceParameter; } }
        public string text { get { return _text; } }
        public float amount { get { return _amount; } }
        public float amountInclVat { get { return _amountInclVat; } }
        public string formatedAmount { get { return _formatedAmount; } }
        public string formatedAmountInclVat { get { return _formatedAmountInclVat; } }
        public string glAccountNo { get { return _glAccountNo; } }


	}
}
