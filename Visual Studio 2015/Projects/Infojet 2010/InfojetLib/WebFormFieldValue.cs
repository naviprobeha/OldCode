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

		public string webFormCode;
		public string fieldCode;
		public string code;
		public string description;
		public bool defaultValue;

        public WebFormFieldValue(Infojet infojetContext, string webFormCode, string fieldCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			
			this.webFormCode = webFormCode;
			this.fieldCode = fieldCode;
			this.code = code;

			getFromDatabase();
		}

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

		}

		private void getFromDatabase()
		{
            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Web Form Code], [Field Code], [Code], [Description], [Default] FROM [" + infojetContext.systemDatabase.getTableName("Web Form Field Value") + "] WHERE [Web Form Code] = '" + this.webFormCode + "' AND [Field Code] = '" + this.fieldCode + "' AND [Code] = '" + this.code + "'");
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

			}

			dataReader.Close();


		}

		public string getCaption(string languageCode)
		{
			WebFormFieldTranslation webFormFieldTranslation = new WebFormFieldTranslation(infojetContext.systemDatabase, this.webFormCode, this.fieldCode, 1, this.code, languageCode);
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
