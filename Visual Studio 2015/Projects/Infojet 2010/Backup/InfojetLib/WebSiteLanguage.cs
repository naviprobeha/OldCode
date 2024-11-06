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
		public string languageCode;
		public string description;
		public string languageText;
		public string currencyCode;
		public string recPriceGroupCode;
		public string cultureValue;
		public string specificCultureValue;
	
		private Database database;

		public WebSiteLanguage(Database database, string webSiteCode, string languageCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.webSiteCode = webSiteCode;
			this.languageCode = languageCode;

			getFromDatabase();
		}

		public WebSiteLanguage(Database database, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			
			webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
			languageCode = dataRow.ItemArray.GetValue(1).ToString();
			description = dataRow.ItemArray.GetValue(2).ToString();
			languageText = dataRow.ItemArray.GetValue(3).ToString();
			currencyCode = dataRow.ItemArray.GetValue(4).ToString();	
			recPriceGroupCode = dataRow.ItemArray.GetValue(5).ToString();
			cultureValue = dataRow.ItemArray.GetValue(6).ToString();
			specificCultureValue = dataRow.ItemArray.GetValue(7).ToString();
		}


		private void getFromDatabase()
		{
            if (languageCode == null) languageCode = "";

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Language Code], [Description], [Language Text], [Currency Code], [Rec_ Price Group Code], [Culture Value], [Specific Culture Value] FROM [" + database.getTableName("Web Site Language") + "] WHERE [Web Site Code] = @webSiteCode AND ([Culture Value] = @code OR [Specific Culture Value] = @code)");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", languageCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
			{
				webSiteCode = dataReader.GetValue(0).ToString();
				languageCode = dataReader.GetValue(1).ToString();
				description = dataReader.GetValue(2).ToString();
				languageText = dataReader.GetValue(3).ToString();
				currencyCode = dataReader.GetValue(4).ToString();	
				recPriceGroupCode = dataReader.GetValue(5).ToString();
				cultureValue = dataReader.GetValue(6).ToString();
				specificCultureValue = dataReader.GetValue(7).ToString();
			}
			dataReader.Close();


		}


	}
}