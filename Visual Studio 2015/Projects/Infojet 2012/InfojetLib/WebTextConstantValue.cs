using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebTextConstantValue.
	/// </summary>
	public class WebTextConstantValue
	{
		private Database database;

		public string webSiteCode;
		public string webTextConstantCode;
		public string languageCode;
		public string textValue;

        public WebTextConstantValue()
        {
        }

		public WebTextConstantValue(Database database, string webSiteCode, string webTextConstantCode, string languageCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.webSiteCode = webSiteCode;
			this.webTextConstantCode = webTextConstantCode;
			this.languageCode = languageCode;
			this.textValue = "";

			getFromDatabase();

		}

        public WebTextConstantValue(Database database, DataRow dataRow)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;

            this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            this.webTextConstantCode = dataRow.ItemArray.GetValue(1).ToString();
            this.languageCode = dataRow.ItemArray.GetValue(2).ToString();
            this.textValue = dataRow.ItemArray.GetValue(3).ToString();
           
        }

        private void getFromDatabase()
        {

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Text Constant Code], [Language Code], [Value] FROM [" + database.getTableName("Web Text Constant Value") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Text Constant Code] = @webTextConstantCode AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webTextConstantCode", webTextConstantCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();

            if (dataReader.Read())
            {

                webSiteCode = dataReader.GetValue(0).ToString();
                webTextConstantCode = dataReader.GetValue(1).ToString();
                languageCode = dataReader.GetValue(2).ToString();
                textValue = dataReader.GetValue(3).ToString();

            }

            dataReader.Close();
        }


        public static WebTextConstantValue[] getWebTextConstantValueArray(Infojet infojetContext, string webSiteCode, string languageCode)
        {
            Database database = infojetContext.systemDatabase;

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Text Constant Code], [Language Code], [Value] FROM [" + database.getTableName("Web Text Constant Value") + "] WHERE [Web Site Code] = @webSiteCode AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);


            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            WebTextConstantValue[] webTextConstantValueArray = new WebTextConstantValue[dataSet.Tables[0].Rows.Count];

            int i = 0;
            while(i < dataSet.Tables[0].Rows.Count)
            {
                WebTextConstantValue webTextConstantValue = new WebTextConstantValue(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                webTextConstantValueArray[i] = webTextConstantValue;

                i++;

            }

            return webTextConstantValueArray;
        }
	}
}
