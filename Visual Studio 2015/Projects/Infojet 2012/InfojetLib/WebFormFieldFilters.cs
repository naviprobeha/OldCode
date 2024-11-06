using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebLayoutRows.
    /// </summary>
    public class WebFormFieldFilters
    {
        private Database database;


        public WebFormFieldFilters(Database database)
        {
            //
            // TODO: Add constructor logic here
            //

            this.database = database;

        }

        public DataSet getWebFormFieldFilters(string webSiteCode, string webFormCode, string webFormFieldCode)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Form Code], [Web Form Field Code], [Value Table Field Name], [Filter Type], [Filter Value], [Web Site Code] FROM [" + database.getTableName("Web Form Field Filter") + "] WHERE [Web Form Code] = @webFormCode AND [Web Form Field Code] = @webFormFieldCode AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webFormFieldCode", webFormFieldCode.Replace("_", " "), 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

        public DataSet getConnectedFieldFilters(string webSiteCode, string webFormCode, string webFormFieldCode)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Form Code], [Web Form Field Code], [Value Table Field Name], [Filter Type], [Filter Value], [Web Site Code] FROM [" + database.getTableName("Web Form Field Filter") + "] WHERE [Web Form Code] = @webFormCode AND [Filter Type] = 0 AND [Filter Value] = @webFormFieldCode AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webFormFieldCode", webFormFieldCode.Replace("_", " "), 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

    }
}
