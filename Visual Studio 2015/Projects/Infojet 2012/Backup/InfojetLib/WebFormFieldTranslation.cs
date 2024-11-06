using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebFormFieldTranslation
	{
		private Database database;

        public string webSiteCode;
		public string webFormCode;
		public string fieldCode;
		public int type;
		public string valueCode;
		public string languageCode;
		public string text;

		public WebFormFieldTranslation(Database database, string webSiteCode, string webFormCode, string fieldCode, int type, string valueCode, string languageCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.webFormCode = webFormCode;
			this.fieldCode = fieldCode;
			this.type = type;
			this.valueCode = valueCode;
			this.languageCode = languageCode;
            this.webSiteCode = webSiteCode;


			getFromDatabase();
		}

		public WebFormFieldTranslation(Database database, DataRow dataRow)
		{
			this.database = database;

			this.webFormCode = dataRow.ItemArray.GetValue(0).ToString();
			this.fieldCode = dataRow.ItemArray.GetValue(1).ToString();
			this.type = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.valueCode = dataRow.ItemArray.GetValue(3).ToString();
			this.languageCode = dataRow.ItemArray.GetValue(4).ToString();
			this.text = dataRow.ItemArray.GetValue(5).ToString();
            this.webSiteCode = dataRow.ItemArray.GetValue(6).ToString();
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Form Code], [Field Code], [Type], [Value Code], [Language Code], [Text], [Web Site Code] FROM [" + database.getTableName("Web Form Field Translation") + "] WHERE [Web Form Code] = @webFormCode AND ([Field Code] = @fieldCode OR [Field Code] = @fieldCode2) AND [Type] = @type AND [Value Code] = @valueCode AND [Language Code] = @languageCode AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("fieldCode", fieldCode, 20);
            databaseQuery.addStringParameter("fieldCode2", fieldCode.Replace(" ", "_"), 20);
            databaseQuery.addIntParameter("type", type);
            databaseQuery.addStringParameter("valueCode", valueCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webFormCode = dataReader.GetValue(0).ToString();
				fieldCode = dataReader.GetValue(1).ToString();
				type = int.Parse(dataReader.GetValue(2).ToString());
				valueCode = dataReader.GetValue(3).ToString();
				languageCode = dataReader.GetValue(4).ToString();
				text = dataReader.GetValue(5).ToString();
                webSiteCode = dataReader.GetValue(6).ToString();

			}

			dataReader.Close();


		}

        public static DataSet getWebFormFieldTranslations(Infojet infojetContext, string webSiteCode, string webFormCode, string languageCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Form Code], [Field Code], [Type], [Value Code], [Language Code], [Text], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field Translation") + "] WHERE [Web Form Code] = @webFormCode AND [Language Code] = @languageCode AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return dataSet;

        }


        public static WebFormFieldTranslation[] getWebFormFieldTranslationArray(Infojet infojetContext, string webSiteCode, string webFormCode, string languageCode)
        {
            DataSet dataSet = getWebFormFieldTranslations(infojetContext, webSiteCode, webFormCode, languageCode);
            WebFormFieldTranslation[] webFormFieldTranslationArray = new WebFormFieldTranslation[dataSet.Tables[0].Rows.Count];

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                WebFormFieldTranslation webFormFieldTranslation = new WebFormFieldTranslation(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);

                webFormFieldTranslationArray[i] = webFormFieldTranslation;

                i++;
            }

            return webFormFieldTranslationArray;

        }

	}
}
