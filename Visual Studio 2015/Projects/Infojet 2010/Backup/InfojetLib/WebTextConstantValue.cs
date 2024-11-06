using System;
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

		private void getFromDatabase()
		{


                SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Web Text Constant Code], [Language Code], [Value] FROM [" + database.getTableName("Web Text Constant Value") + "] WHERE [Web Site Code] = '" + this.webSiteCode + "' AND [Web Text Constant Code] = '" + this.webTextConstantCode + "' AND [Language Code] = '" + this.languageCode + "'");
                if (dataReader.Read())
                {

                    webSiteCode = dataReader.GetValue(0).ToString();
                    webTextConstantCode = dataReader.GetValue(1).ToString();
                    languageCode = dataReader.GetValue(2).ToString();
                    textValue = dataReader.GetValue(3).ToString();

                }

                dataReader.Close();
		}

	}
}
