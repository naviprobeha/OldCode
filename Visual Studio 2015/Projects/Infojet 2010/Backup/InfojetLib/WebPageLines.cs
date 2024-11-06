using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLines.
	/// </summary>
	public class WebPageLines
	{
		private Database database;

		public WebPageLines(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getWebPageLines(string webSiteCode, string webPageCode, int versionNo)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Page Code], [Version No_], [Line No_], [Web Template Part Code], [Type], [Code], [Description], [Sort Order], [Language Code], [Web Type Code] FROM [" + database.getTableName("Web Page Line") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Page Code] = @webPageCode AND [Version No_] = @versionNo ORDER BY [Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addIntParameter("versionNo", versionNo);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

		public DataSet getWebPageLines(string webSiteCode, string webPageCode, int versionNo, string webTemplatePartCode)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Page Code], [Version No_], [Line No_], [Web Template Part Code], [Type], [Code], [Description], [Sort Order], [Language Code], [Web Type Code] FROM [" + database.getTableName("Web Page Line") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Page Code] = @webPageCode AND [Version No_] = @versionNo AND [Web Template Part Code] = @webTemplatePartCode ORDER BY [Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addIntParameter("versionNo", versionNo);
            databaseQuery.addStringParameter("webTemplatePartCode", webTemplatePartCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

	}
}
