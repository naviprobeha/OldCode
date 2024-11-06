using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebSite.
    /// </summary>
    public class WebStyleProperty
    {
        public string webStyleCode;
        public string code;
        public string description;
        public int valueType;
        public string defaultValue;

        private Infojet infojetContext;

        public WebStyleProperty(Infojet infojetContext, string webStyleCode, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
            this.webStyleCode = webStyleCode;
            this.code = code;

            getFromDatabase();
        }


        public WebStyleProperty(Infojet infojetContext, DataRow dataRow)
        {
            //
            // TODO: Add constructor logic here
            //

            this.infojetContext = infojetContext;
            fromDataRow(dataRow);
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Style Code], [Code], [Description], [Value Type], [Default Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Style Property") + "] WHERE [Web Style Code] = @webStyleCode AND [Code] = @code");
            databaseQuery.addStringParameter("webStyleCode", webStyleCode, 20);
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {

                webStyleCode = dataReader.GetValue(0).ToString();
                code = dataReader.GetValue(1).ToString();
                description = dataReader.GetValue(2).ToString();
                valueType = int.Parse(dataReader.GetValue(3).ToString());
                defaultValue = dataReader.GetValue(4).ToString();

            }

            dataReader.Close();

        }

        private void fromDataRow(DataRow dataRow)
        {
            webStyleCode = dataRow.ItemArray.GetValue(0).ToString();
            code = dataRow.ItemArray.GetValue(1).ToString();
            description = dataRow.ItemArray.GetValue(2).ToString();
            valueType = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            defaultValue = dataRow.ItemArray.GetValue(4).ToString();
        }

        public static ArrayList getArrayList(Infojet infojetContext)
        {
            ArrayList arrayList = new ArrayList();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Style Code], [Code], [Description], [Value Type], [Default Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Style Property") + "] WHERE [Web Style Code] = @webStyleCode");
            databaseQuery.addStringParameter("webStyleCode", infojetContext.webSite.webStyleCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebStyleProperty webStyleProperty = new WebStyleProperty(infojetContext, dataSet.Tables[0].Rows[i]);
                arrayList.Add(webStyleProperty);

                i++;
            }

            return arrayList;
        }

    }
}
