using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebSiteCountry
	{
		public string webSiteCode;
		public string countryCode;
		public string name;
		public bool defaultCountry;
        public string vatBusPostingGroup = "";
	
		private Database database;

		public WebSiteCountry(Database database, string webSiteCode, string countryCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.webSiteCode = webSiteCode;
			this.countryCode = countryCode;

			getFromDatabase();
		}

		public WebSiteCountry(Database database, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;

			this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
			this.countryCode = dataRow.ItemArray.GetValue(1).ToString();
			this.name = dataRow.ItemArray.GetValue(2).ToString();

			this.defaultCountry = false;
			if (int.Parse(dataRow.ItemArray.GetValue(3).ToString()) == 1) this.defaultCountry = true;
		}

		private void getFromDatabase()
		{

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Country Code], [Name], [Default], [VAT Bus_ Posting Group] FROM [" + database.getTableName("Web Site Country") + "] WHERE [Web Site Code] = @webSiteCode AND [Country Code] = @countryCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("countryCode", countryCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{
				webSiteCode = dataReader.GetValue(0).ToString();
				countryCode = dataReader.GetValue(1).ToString();
				name = dataReader.GetValue(2).ToString();

				defaultCountry = false;
				if (int.Parse(dataReader.GetValue(3).ToString()) == 1) defaultCountry = true;

                vatBusPostingGroup = dataReader.GetValue(4).ToString();
			}

			dataReader.Close();
			
		}


	}
}