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

		public string webFormCode;
		public string fieldCode;
		public int type;
		public string valueCode;
		public string languageCode;
		public string text;

		public WebFormFieldTranslation(Database database, string webFormCode, string fieldCode, int type, string valueCode, string languageCode)
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
		}

		private void getFromDatabase()
		{
			SqlDataReader dataReader = database.query("SELECT [Web Form Code], [Field Code], [Type], [Value Code], [Language Code], [Text] FROM ["+database.getTableName("Web Form Field Translation")+"] WHERE [Web Form Code] = '"+this.webFormCode+"' AND [Field Code] = '"+this.fieldCode+"' AND [Type] = '"+type+"' AND [Value Code] = '"+valueCode+"' AND [Language Code] = '"+languageCode+"'");
			if (dataReader.Read())
			{

				webFormCode = dataReader.GetValue(0).ToString();
				fieldCode = dataReader.GetValue(1).ToString();
				type = int.Parse(dataReader.GetValue(2).ToString());
				valueCode = dataReader.GetValue(3).ToString();
				languageCode = dataReader.GetValue(4).ToString();
				text = dataReader.GetValue(5).ToString();

			}

			dataReader.Close();


		}

	}
}
