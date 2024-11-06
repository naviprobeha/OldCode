using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class WebFormFieldAssignment
    {
        private Infojet infojetContext;

        public string webSiteCode;
        public string webFormCode;
        public string webFormFieldCode;
        public string assignWebFormFieldCode;
        public int assignType;
        public string assignValue;

        public WebFormFieldAssignment(Infojet infojetContext, string webSiteCode, string webFormCode, string webFormFieldCode, string assignWebFormFieldCode)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            this.webFormCode = webFormCode;
            this.webFormFieldCode = webFormFieldCode;
            this.assignWebFormFieldCode = assignWebFormFieldCode;
            this.webSiteCode = webSiteCode;

            getFromDatabase();
        }

        public WebFormFieldAssignment(Infojet infojetContext, DataRow dataRow)
        {
            this.infojetContext = infojetContext;

            this.webFormCode = dataRow.ItemArray.GetValue(0).ToString();
            this.webFormFieldCode = dataRow.ItemArray.GetValue(1).ToString();
            this.assignWebFormFieldCode = dataRow.ItemArray.GetValue(2).ToString();
            this.assignType = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.assignValue = dataRow.ItemArray.GetValue(4).ToString();
            this.webSiteCode = dataRow.ItemArray.GetValue(5).ToString();
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Form Code], [Web Form Field Code], [Assign Web Form Field Code], [Assign Type], [Assign Value], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field Assignment") + "] WHERE [Web Form Code] = @webFormCode AND [Web Form Field Code] = @webFormFieldCode AND [Assign Web Form Field Code] = @assignWebFormFieldCode AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webFormFieldCode", webFormFieldCode, 20);
            databaseQuery.addStringParameter("assignWebFormFieldCode", assignWebFormFieldCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                webFormCode = dataReader.GetValue(0).ToString();
                webFormFieldCode = dataReader.GetValue(1).ToString();
                assignWebFormFieldCode = dataReader.GetValue(2).ToString();
                assignType = int.Parse(dataReader.GetValue(3).ToString());
                assignValue = dataReader.GetValue(4).ToString();
                webSiteCode = dataReader.GetValue(5).ToString();

            }

            dataReader.Close();


        }

    }
}
