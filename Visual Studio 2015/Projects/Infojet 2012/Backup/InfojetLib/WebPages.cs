using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPages.
	/// </summary>
	public class WebPages
	{
		private Database database;

		public WebPages(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getTopPages(string webSiteCode, string languageCode)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Window Mode], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = '" + languageCode + "' AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.[Protected] = '0' AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 AND p.[Parent Web Page Code] = '' ORDER BY p.[Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

		public DataSet getTopPages(string webSiteCode, string languageCode, string userNo)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Window Mode], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt, [" + database.getTableName("Web Page User Group") + "] wpg, [" + database.getTableName("Web User Account Group") + "] wug, [" + database.getTableName("Web User Account Relation") + "] wur WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = @languageCode AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.Code = wpg.[Web Page Code] AND p.[Web Site Code] = wpg.[Web Site Code] AND wpg.[Web User Group Code] = wug.[Web User Group Code] AND wug.[No_] = @userNo AND wug.[No_] = wur.[No_] AND wur.[Web Site Code] = @webSiteCode AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 AND p.[Parent Web Page Code] = '' ORDER BY p.[Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("userNo", userNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

		public DataSet getChildPages(string webSiteCode, string webPageCode, string languageCode)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Window Mode], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = @languageCode AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.[Protected] = '0' AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 AND p.[Parent Web Page Code] = @webPageCode ORDER BY p.[Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

		public DataSet getChildPages(string webSiteCode, string webPageCode, string languageCode, string userNo)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Window Mode], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt, [" + database.getTableName("Web Page User Group") + "] wpg, [" + database.getTableName("Web User Account Group") + "] wug, [" + database.getTableName("Web User Account Relation") + "] wur WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = @languageCode AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.Code = wpg.[Web Page Code] AND p.[Web Site Code] = wpg.[Web Site Code] AND wpg.[Web User Group Code] = wug.[Web User Group Code] AND wug.[No_] = @userNo AND wug.[No_] = wur.[No_] AND wur.[Web Site Code] = @webSiteCode AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 AND p.[Parent Web Page Code] = @webPageCode ORDER BY p.[Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addStringParameter("userNo", userNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

		public DataSet getAllPages(string webSiteCode, string languageCode, string userNo)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Published Version No_], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt, [" + database.getTableName("Web Page User Group") + "] wpg, [" + database.getTableName("Web User Account Group") + "] wug, [" + database.getTableName("Web User Account Relation") + "] wur WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = @languageCode AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.Code = wpg.[Web Page Code] AND p.[Web Site Code] = wpg.[Web Site Code] AND wpg.[Web User Group Code] = wug.[Web User Group Code] AND wug.[No_] = @userNo AND wug.[No_] = wur.[No_] AND wur.[Web Site Code] = @webSiteCode AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 ORDER BY p.[Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("userNo", userNo, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

		public DataSet getAllPages(string webSiteCode, string languageCode)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Published Version No_], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = @languageCode AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.[Protected] = '0' AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 ORDER BY p.[Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

        public DataSet getRSSPages(string webSiteCode, string languageCode)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Window Mode], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = @languageCode AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.[Protected] = '0' AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 AND p.[Parent Web Page Code] = '' AND p.[Inlude In RSS Feed] = 1 ORDER BY p.[Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }
	}
}
