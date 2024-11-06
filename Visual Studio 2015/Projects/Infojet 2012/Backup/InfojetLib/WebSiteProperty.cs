using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebSite.
    /// </summary>
    public class WebSiteProperty
    {
        public string webSiteCode;
        public string webStylePropertyCode;
        public string value;

        private Infojet infojetContext;

        public WebSiteProperty(Infojet infojetContext, string webSiteCode, string webStylePropertyCode)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
            this.webSiteCode = webSiteCode;
            this.webStylePropertyCode = webStylePropertyCode;
            this.value = "";

            getFromDatabase();
        }

        public WebSiteProperty(Infojet infojetContext, DataRow dataRow)
        {
            //
            // TODO: Add constructor logic here
            //

            this.infojetContext = infojetContext;
            fromDataRow(dataRow);
        }

        private void getFromDatabase()
        {

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Style Property Code], [Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Site Property") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Style Property Code] = @webStylePropertyCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webStylePropertyCode", webStylePropertyCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                webSiteCode = dataReader.GetValue(0).ToString();
                webStylePropertyCode = dataReader.GetValue(1).ToString();
                value = dataReader.GetValue(2).ToString();

            }

            dataReader.Close();

        }

        private void fromDataRow(DataRow dataRow)
        {
            webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            webStylePropertyCode = dataRow.ItemArray.GetValue(1).ToString();
            value = dataRow.ItemArray.GetValue(2).ToString();
        }

        public static ArrayList getArrayList(Infojet infojetContext, string webSiteCode)
        {
            ArrayList arrayList = new ArrayList();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Style Property Code], [Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Site Property") + "] WHERE [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebSiteProperty webSiteProperty = new WebSiteProperty(infojetContext, dataSet.Tables[0].Rows[i]);
                arrayList.Add(webSiteProperty);

                i++;
            }

            return arrayList;
        }
    }
}
