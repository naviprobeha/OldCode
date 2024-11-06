using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebItemCategoryText
	{
		public string webSiteCode;
		public string webItemCategoryCode;
		public string languageCode;
		public int lineNo;
		public string text;
	
		private Database database;

		public WebItemCategoryText(Database database, string webSiteCode, string webItemCategoryCode, string languageCode, int lineNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.webSiteCode = webSiteCode;
			this.webItemCategoryCode = webItemCategoryCode;
			this.languageCode = languageCode;
			this.lineNo = lineNo;

			getFromDatabase();
		}


		private void getFromDatabase()
		{

			SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Web Item Category Code], [Language Code], [Line No_], [Text] FROM ["+database.getTableName("Web Item Category Text")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Web Item Category Code] = '"+this.webItemCategoryCode+"' AND [Language Code] = '"+this.languageCode+"' AND [Line No_] = '"+lineNo+"'");
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				webItemCategoryCode = dataReader.GetValue(1).ToString();
				languageCode = dataReader.GetValue(2).ToString();
				lineNo = int.Parse(dataReader.GetValue(3).ToString());
				text = dataReader.GetValue(4).ToString();

			}

			dataReader.Close();
			
		}


	}
}