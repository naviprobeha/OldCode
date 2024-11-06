using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebLayoutRows.
    /// </summary>
    public class WebNewsEntries
    {
        private Infojet infojetContext;


        public WebNewsEntries(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //

            this.infojetContext = infojetContext;

        }

        public WebNewsCollection getNews(string webNewsCategoryCode)
        {
            return getNews(webNewsCategoryCode, 999);
        }

        public WebNewsCollection getNews(string webNewsCategoryCode, int maxCount)
        {
            WebNewsCollection webNewsCollection = new WebNewsCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT TOP "+maxCount+" n.[No_], n.[Description], n.[Creation Date], n.[Published From Date], n.[Published To Date], n.[Intro Image Code], n.[Main Image Code], n.[Common Header] FROM [" + infojetContext.systemDatabase.getTableName("Web News Entry") + "] n, [" + infojetContext.systemDatabase.getTableName("Web News Publication") + "] p, [" + infojetContext.systemDatabase.getTableName("Web News Header") + "] h WHERE n.[No_] = p.[Web News Entry No_] AND p.[Web News Category Code] = @webNewsCategoryCode AND (n.[Published From Date] = '1753-01-01' OR n.[Published From Date] <= GETDATE()) AND (n.[Published To Date] = '1753-01-01' OR n.[Published To Date] >= GETDATE()) AND h.[Web News Entry No_] = n.[No_] AND h.[Language Code] = @languageCode ORDER BY n.[Creation Date] DESC");
            databaseQuery.addStringParameter("webNewsCategoryCode", webNewsCategoryCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebNewsEntry webNewsEntry = new WebNewsEntry(infojetContext, dataSet.Tables[0].Rows[i]);

                webNewsCollection.Add(webNewsEntry);

                i++;
            }

            return webNewsCollection;

        }

        public WebNewsEntry getPublishedNewsEntry(string webNewsEntryNo)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT n.[No_], n.[Description], n.[Creation Date], n.[Published From Date], n.[Published To Date], n.[Intro Image Code], n.[Main Image Code], n.[Common Header] FROM [" + infojetContext.systemDatabase.getTableName("Web News Entry") + "] n, [" + infojetContext.systemDatabase.getTableName("Web News Publication") + "] p, [" + infojetContext.systemDatabase.getTableName("Web News Header") + "] h WHERE n.[No_] = p.[Web News Entry No_] AND (n.[Published From Date] = '1753-01-01' OR n.[Published From Date] <= GETDATE()) AND (n.[Published To Date] = '1753-01-01' OR n.[Published To Date] >= GETDATE()) AND h.[Web News Entry No_] = n.[No_] AND n.[No_] = @webNewsEntryNo AND h.[Language Code] = @languageCode ORDER BY n.[Creation Date] DESC");
            databaseQuery.addStringParameter("webNewsEntryNo", webNewsEntryNo, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataReader sqlDataReader = databaseQuery.executeQuery();

            WebNewsEntry webNewsEntry = null;

            if (sqlDataReader.Read())
            {
                webNewsEntry = new WebNewsEntry(infojetContext, sqlDataReader);

            }

            sqlDataReader.Close();

            return webNewsEntry;

        }

    }
}
