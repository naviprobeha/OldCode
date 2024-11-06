using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebSiteLanguage
	{
		public string webSiteCode;
        public string code;
		public string languageCode;
		public string description;
		public string languageText;
		public string currencyCode;
		public string recPriceGroupCode;
		public string cultureValue;
		public string specificCultureValue;
        public string marketCountryCode;
	
		private Database database;

		public WebSiteLanguage(Database database, string webSiteCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.webSiteCode = webSiteCode;
			this.code = code;

			getFromDatabase();
		}

		public WebSiteLanguage(Database database, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			
			webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            code = dataRow.ItemArray.GetValue(1).ToString();
            languageCode = dataRow.ItemArray.GetValue(2).ToString();
			description = dataRow.ItemArray.GetValue(3).ToString();
			languageText = dataRow.ItemArray.GetValue(4).ToString();
			currencyCode = dataRow.ItemArray.GetValue(5).ToString();	
			recPriceGroupCode = dataRow.ItemArray.GetValue(6).ToString();
			cultureValue = dataRow.ItemArray.GetValue(7).ToString();
			specificCultureValue = dataRow.ItemArray.GetValue(8).ToString();
            marketCountryCode = dataRow.ItemArray.GetValue(9).ToString();
		}


		private void getFromDatabase()
		{

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Code], [Language Code], [Description], [Language Text], [Currency Code], [Rec_ Price Group Code], [Culture Value], [Specific Culture Value], [Market Country Code] FROM [" + database.getTableName("Web Site Language") + "] WHERE [Web Site Code] = @webSiteCode AND ([Code] = @code OR [Culture Value] = @code OR [Specific Culture Value] = @code)");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{
				webSiteCode = dataReader.GetValue(0).ToString();
                code = dataReader.GetValue(1).ToString();
				languageCode = dataReader.GetValue(2).ToString();
				description = dataReader.GetValue(3).ToString();
				languageText = dataReader.GetValue(4).ToString();
				currencyCode = dataReader.GetValue(5).ToString();	
				recPriceGroupCode = dataReader.GetValue(6).ToString();
				cultureValue = dataReader.GetValue(7).ToString();
				specificCultureValue = dataReader.GetValue(8).ToString();
                marketCountryCode = dataReader.GetValue(9).ToString();
                     
			}
			dataReader.Close();


		}


	}
}