using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLineText.
	/// </summary>
	public class WebPageLineText
	{
		public Database database;


		public string webSiteCode;
		public string webPageCode;
		public int webPageLineNo;
		public string languageCode;
		public int versionNo;
		public int lineNo;
		public string text;


		public WebPageLineText(Database database, string webSiteCode, string webPageCode, int webPageLineNo, string languageCode, int versionNo, int lineNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;

			this.webSiteCode = webSiteCode;
			this.webPageCode = webPageCode;
			this.webPageLineNo = webPageLineNo;
			this.languageCode = languageCode;
			this.versionNo = versionNo;
			this.lineNo = lineNo;

			getFromDatabase();
		}

		public WebPageLineText(Database database, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;

			this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
			this.webPageCode = dataRow.ItemArray.GetValue(1).ToString();
			this.webPageLineNo = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.languageCode = dataRow.ItemArray.GetValue(3).ToString();
    		this.versionNo = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.lineNo = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.text = dataRow.ItemArray.GetValue(6).ToString();
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Page Code], [Web Page Line No_], [Language Code], [Version No_], [Line No_], [Text] FROM [" + database.getTableName("Web Page Line Text") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Page Code] = @webPageCode AND [Web Page Line No_] = @webPageLineNo AND [Language Code] = @languageCode AND [Version No_] = @versionNo AND [Line No_] = @lineNo");
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addIntParameter("versionNo", versionNo);
            databaseQuery.addIntParameter("webPageLineNo", webPageLineNo);
            databaseQuery.addIntParameter("lineNo", lineNo);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				webPageCode = dataReader.GetValue(1).ToString();
				webPageLineNo = dataReader.GetInt32(2);
				languageCode = dataReader.GetValue(3).ToString();
				versionNo = dataReader.GetInt32(4);
				lineNo = dataReader.GetInt32(5);
				text = dataReader.GetValue(6).ToString();
			}

			dataReader.Close();
			
		}

	

	}
}
