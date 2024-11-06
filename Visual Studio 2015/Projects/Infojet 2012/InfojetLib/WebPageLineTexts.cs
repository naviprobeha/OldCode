using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLines.
	/// </summary>
	public class WebPageLineTexts
	{
		private Database database;

		public WebPageLineTexts(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getDataSet(string webSiteCode, string webPageCode, int webPageLineNo, string languageCode, int versionNo)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Page Code], [Web Page Line No_], [Language Code], [Version No_], [Line No_], [Text] FROM [" + database.getTableName("Web Page Line Text") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Page Code] = @webPageCode AND [Web Page Line No_] = @webPageLineNo AND [Language Code] = @languageCode AND [Version No_] = @versionNo ORDER BY [Line No_]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addIntParameter("versionNo", versionNo);
            databaseQuery.addIntParameter("webPageLineNo", webPageLineNo);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}


        public DataSet getDataSet(WebPageLine webPageLine, string languageCode, int versionNo)
		{
			return getDataSet(webPageLine.webSiteCode, webPageLine.webPageCode, webPageLine.lineNo, languageCode, versionNo);
		}

 
	}
}
