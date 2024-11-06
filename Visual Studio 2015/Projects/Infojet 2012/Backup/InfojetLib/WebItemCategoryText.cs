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

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Item Category Code], [Language Code], [Line No_], [Text] FROM [" + database.getTableName("Web Item Category Text") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Item Category Code] = @webItemCategoryCode AND [Language Code] = @languageCode AND [Line No_] = @lineNo");
            databaseQuery.addStringParameter("webItemCategoryCode", webItemCategoryCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addIntParameter("lineNo", lineNo);


            SqlDataReader dataReader = databaseQuery.executeQuery();
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