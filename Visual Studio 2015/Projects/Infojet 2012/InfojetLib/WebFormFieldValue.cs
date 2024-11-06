using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebFormFieldValue
	{
		private Infojet infojetContext;

        public string webSiteCode;
		public string webFormCode;
		public string fieldCode;
		public string code;
		public string description;
		public bool defaultValue;
        public string translation;

        public WebFormFieldValue(Infojet infojetContext, string webSiteCode, string webFormCode, string fieldCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			
			this.webFormCode = webFormCode;
			this.fieldCode = fieldCode;
			this.code = code;
            this.webSiteCode = webSiteCode;

			getFromDatabase();
		}

        public WebFormFieldValue()
        { }


        public WebFormFieldValue(Infojet infojetContext, DataRow dataRow)
		{
            this.infojetContext = infojetContext;

			this.webFormCode = dataRow.ItemArray.GetValue(0).ToString();
			this.fieldCode = dataRow.ItemArray.GetValue(1).ToString();
			this.code = dataRow.ItemArray.GetValue(2).ToString();
			this.description = dataRow.ItemArray.GetValue(3).ToString();

			defaultValue = false;
			if (int.Parse(dataRow.ItemArray.GetValue(4).ToString()) == 1)
			{
				defaultValue = true;
			}

            this.webSiteCode = dataRow.ItemArray.GetValue(5).ToString();
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Form Code], [Field Code], [Code], [Description], [Default], [Web Site Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field Value") + "] WHERE [Web Form Code] = @webFormCode AND [Field Code] = @fieldCode AND [Code] = @code AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webFormCode", webFormCode, 20);
            databaseQuery.addStringParameter("fieldCode", fieldCode, 20);
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webFormCode = dataReader.GetValue(0).ToString();
				fieldCode = dataReader.GetValue(1).ToString();
				code = dataReader.GetValue(2).ToString();
				description = dataReader.GetValue(3).ToString();
				
				defaultValue = false;
				if (int.Parse(dataReader.GetValue(4).ToString()) == 1)
				{
					defaultValue = true;
				}

                webSiteCode = dataReader.GetValue(5).ToString();
			}

			dataReader.Close();


		}

		public string getCaption(string languageCode)
		{
			WebFormFieldTranslation webFormFieldTranslation = new WebFormFieldTranslation(infojetContext.systemDatabase, webSiteCode, this.webFormCode, this.fieldCode, 1, this.code, languageCode);
			if ((webFormFieldTranslation.text != null) && (webFormFieldTranslation.text != "")) return webFormFieldTranslation.text;

			return this.description;
		}

        public FieldValue getFieldValue()
        {
            FieldValue fieldValue = new FieldValue();
            fieldValue.code = this.code;
            fieldValue.description = getCaption(infojetContext.languageCode);
            fieldValue.defaultValue = this.defaultValue;

            return fieldValue;

        }
	}
}
