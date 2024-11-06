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

			SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Country Code], [Name], [Default] FROM ["+database.getTableName("Web Site Country")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Country Code] = '"+this.countryCode+"'");
			if (dataReader.Read())
			{
				webSiteCode = dataReader.GetValue(0).ToString();
				countryCode = dataReader.GetValue(1).ToString();
				name = dataReader.GetValue(2).ToString();

				defaultCountry = false;
				if (int.Parse(dataReader.GetValue(3).ToString()) == 1) defaultCountry = true;
			}

			dataReader.Close();
			
		}


	}
}