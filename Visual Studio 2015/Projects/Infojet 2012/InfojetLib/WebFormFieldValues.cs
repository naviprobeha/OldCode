using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebLayoutRows.
	/// </summary>
	public class WebFormFieldValues
	{
		private Database database;


		public WebFormFieldValues(Database database)
		{
			//
			// TODO: Add constructor logic here
			//

			this.database = database;

		}

		public DataSet getWebFormFieldValues(string webSiteCode, string webFormCode, string fieldCode)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Form Code], [Field Code], [Code], [Description], [Default], [Web Site Code] FROM [" + database.getTableName("Web Form Field Value") + "] WHERE [Web Form Code] = @webFormCode AND ([Field Code] = @fieldCode OR REPLACE([Field Code], ' ', '_') = @fieldCode) AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("fieldCode", fieldCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

        public WebFormFieldValue[] getWebFormFieldValueArray(Infojet infojetContext, string webSiteCode, string webFormCode, string fieldCode)
        {
            DataSet dataSet = getWebFormFieldValues(webSiteCode, webFormCode, fieldCode);
            WebFormFieldValue[] webFormFieldValueArray = new WebFormFieldValue[dataSet.Tables[0].Rows.Count];

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebFormFieldValue webFormFieldValue = new WebFormFieldValue(infojetContext, dataSet.Tables[0].Rows[i]);

                webFormFieldValueArray[i] = webFormFieldValue;

                i++;
            }

            return webFormFieldValueArray;

        }
	}
}
