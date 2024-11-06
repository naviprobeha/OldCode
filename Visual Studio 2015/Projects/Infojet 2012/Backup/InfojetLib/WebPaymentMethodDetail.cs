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
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Payment Method Code], [Language Code], [Available To], [Code], [Description], [Text], [Starting Date], [Ending Date], [Currency Code], [Amount], [Enabled], [G_L Account No_], [VAT Prod_ Posting Group] FROM [" + database.getTableName("Web Payment Method Detail") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Payment Method Code] = @webPaymentMethodCode AND [Language Code] = @languageCode AND [Available To] = @availableTo AND [Code] = @code AND [Starting Date] = @startingDate AND [Ending Date] = @endingDate AND [Currency Code] = @currencyCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webPaymentMethodCode", webPaymentMethodCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addIntParameter("availableTo", availableTo);
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addDateTimeParameter("startingDate", startingDate);
            databaseQuery.addDateTimeParameter("endingDate", endingDate);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
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

            DatabaseQuery databaseQuery = database.prepare("SELECT "+database.convertField("[VAT %]")+", [VAT Calculation Type] FROM [" + database.getTableName("VAT Posting Setup") + "] WHERE [VAT Bus_ Posting Group] = @vatBusPostingGroup AND [VAT Prod_ Posting Group] = @vatProdPostingGroup");
            databaseQuery.addStringParameter("vatBusPostingGroup", customer.vatBusPostingGroup, 20);
            databaseQuery.addStringParameter("vatProdPostingGroup", vatProdPostingGroup, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                vatFactor = float.Parse(dataReader.GetValue(0).ToString());
                if (dataReader.GetValue(1).ToString() == "1") vatFactor = 0;
            }

            dataReader.Close();

            vatFactor = (vatFactor / 100) + 1;

            return vatFactor;
        }

	}
}
