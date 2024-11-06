using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebPageLines.
    /// </summary>
    public class WebSiteNewUserMessageLines
    {
        private Infojet infojetContext;

        public WebSiteNewUserMessageLines(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
        }

        public DataSet getLines()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Line No_], [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web Site New User Message Line") + "] WHERE [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

        public string getSubject()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [New User Mail Subject] FROM [" + infojetContext.systemDatabase.getTableName("Web Site") + "] WHERE [Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);

            SqlDataReader sqlDataReader = databaseQuery.executeQuery();

            string subject = "";

            if (sqlDataReader.Read())
            {
                subject = sqlDataReader.GetValue(0).ToString();
            }

            sqlDataReader.Close();


            return subject;
        }
    }
}
