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

        public DataSet getWebFormFieldFilters(string webFormCode, string webFormFieldCode)
        {
            SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Web Form Code], [Web Form Field Code], [Value Table Field Name], [Filter Type], [Filter Value] FROM [" + database.getTableName("Web Form Field Filter") + "] WHERE [Web Form Code] = '" + webFormCode + "' AND [Web Form Field Code] = '" + webFormFieldCode.Replace("_", " ") + "'");
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }

    }
}
