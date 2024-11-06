using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPaymentMethodAccounting.
	/// </summary>
	public class WebPaymentMethodDetail
	{
		public string webSiteCode;
		public string webPaymentMethodCode;
        public string languageCode;
    	public int availableTo;
		public string code;
        public string description;
        public string text;
		public DateTime startingDate;
		public DateTime endingDate;
		public string currencyCode;
		public float amount;
		public bool enabled;
		public string glAccountNo;
        public string vatProdPostingGroup;

		private Database database;

		public WebPaymentMethodDetail(Database database, string webSiteCode, string webPaymentMethodCode, string languageCode, int availableTo, string code, DateTime startingDate, DateTime endingDate, string currencyCode)
		{
			//
			// TODO: Add constructor logic here
			//
		
			this.database = database;

			this.webSiteCode = webSiteCode;
			this.webPaymentMethodCode = webPaymentMethodCode;
            this.languageCode = languageCode;
			this.availableTo = availableTo;
			this.code = code;
			this.startingDate = startingDate;
			this.endingDate = endingDate;
			this.currencyCode = currencyCode;
			
			getFromDatabase();
		}


		public WebPaymentMethodDetail(Database database, DataRow dataRow)
		{
			this.database = database;

			this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
			this.webPaymentMethodCode = dataRow.ItemArray.GetValue(1).ToString();
            this.languageCode = dataRow.ItemArray.GetValue(2).ToString();
			this.availableTo = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.code = dataRow.ItemArray.GetValue(4).ToString();
            this.description = dataRow.ItemArray.GetValue(5).ToString();
            this.text = dataRow.ItemArray.GetValue(6).ToString();
			this.startingDate = DateTime.Parse(dataRow.ItemArray.GetValue(7).ToString());
			this.endingDate = DateTime.Parse(dataRow.ItemArray.GetValue(8).ToString());
			this.currencyCode = dataRow.ItemArray.GetValue(9).ToString();
			this.amount = float.Parse(dataRow.ItemArray.GetValue(10).ToString());
		
			this.enabled = false;
			if (int.Parse(dataRow.ItemArray.GetValue(11).ToString()) == 1) this.enabled = true;

			this.glAccountNo = dataRow.ItemArray.GetValue(12).ToString();
            this.vatProdPostingGroup = dataRow.ItemArray.GetValue(13).ToString();
		}

		private void getFromDatabase()
		{

			SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM ["+database.getTableName("Web Payment Method Detail")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Web Payment Method Code] = '"+this.webPaymentMethodCode+"' AND [Language Code] = '"+languageCode+"' AND [Available To] = '"+this.availableTo+"' AND [Code] = '"+this.code+"' AND [Starting Date] = '"+this.startingDate.ToString("yyyy-MM-dd")+"' AND [Ending Date] = '"+this.endingDate.ToString("yyyy-MM-dd")+"' AND [Currency Code] = '"+this.currencyCode+"'");
			if (dataReader.Read())
			{
				webSiteCode = dataReader.GetValue(0).ToString();
				webPaymentMethodCode = dataReader.GetValue(1).ToString();
                languageCode = dataReader.GetValue(2).ToString();
				availableTo = dataReader.GetInt32(3);
				code = dataReader.GetValue(4).ToString();
                description = dataReader.GetValue(5).ToString();
                text = dataReader.GetValue(6).ToString();
				startingDate = dataReader.GetDateTime(7);
				endingDate = dataReader.GetDateTime(8);
				currencyCode = dataReader.GetValue(9).ToString();
				amount = float.Parse(dataReader.GetValue(10).ToString());

				enabled = false;
				if (int.Parse(dataReader.GetValue(11).ToString()) == 1) enabled = true;

				glAccountNo = dataReader.GetValue(12).ToString();
                vatProdPostingGroup = dataReader.GetValue(13).ToString();
			}

			dataReader.Close();
			
		}

        public float getVatFactor(Customer customer)
        {
            float vatFactor = 0;

            SqlDataReader dataReader = database.query("SELECT [VAT %] FROM [" + database.getTableName("VAT Posting Setup") + "] WHERE [VAT Bus_ Posting Group] = '" + customer.vatBusPostingGroup + "' AND [VAT Prod_ Posting Group] = '" + this.vatProdPostingGroup + "'");
            if (dataReader.Read())
            {
                vatFactor = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            vatFactor = (vatFactor / 100) + 1;

            return vatFactor;
        }

	}
}
