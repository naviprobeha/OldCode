using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class WebFormFieldFilter
    {
        private Infojet infojetContext;

        public string webSiteCode;
        public string webFormCode;
        public string webFormFieldCode;
        public string valueTableFieldName;
        public int filterType;
        public string filterValue;

        public WebFormFieldFilter(Infojet infojetContext, string webSiteCode, string webFormCode, string webFormFieldCode, string valueTableFieldName)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this.webFormCode = webFormCode;
            this.webFormFieldCode = webFormFieldCode;
            this.valueTableFieldName = valueTableFieldName;
            this.webSiteCode = webSiteCode;

            getFromDatabase();
        }

        public WebFormFieldFilter(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this.webFormCode = dataRow.ItemArray.GetValue(0).ToString();
            this.webFormFieldCode = dataRow.ItemArray.GetValue(1).ToString();
            this.valueTableFieldName = dataRow.ItemArray.GetValue(2).ToString();
            this.filterType = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.filterValue = dataRow.ItemArray.GetValue(4).ToString();
            this.webSiteCode = dataRow.ItemArray.GetValue(5).ToString();

        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Form Code], [Web Form Field Code], [Value Table Field Name], [Filter Type], [Filter Value], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field Filter") + "] WHERE [Web Form Code] = @webFormCode AND [Web Form Field Code] = @webFormFieldCode AND [Value Table Field Name] = @valueTableFieldName AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webFormFieldCode", webFormFieldCode, 20);
            databaseQuery.addStringParameter("valueTableFieldName", valueTableFieldName, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                webFormCode = dataReader.GetValue(0).ToString();
                webFormFieldCode = dataReader.GetValue(1).ToString();
                valueTableFieldName = dataReader.GetValue(2).ToString();
                filterType = int.Parse(dataReader.GetValue(3).ToString());
                filterValue = dataReader.GetValue(4).ToString();
                webSiteCode = dataReader.GetValue(5).ToString();
            }

            dataReader.Close();


        }

    }
}
