using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebColor
	{
		private Database database;

		public string code;
		public string description;
		public string hexColor;

		public WebColor(Database database, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.code = code;

			getFromDatabase();
		}

		public WebColor(Database database, DataRow dataRow)
		{
			this.database = database;

			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.description = dataRow.ItemArray.GetValue(1).ToString();
			this.hexColor = dataRow.ItemArray.GetValue(2).ToString();
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Code], [Description], [Hex Color] FROM [" + database.getTableName("Web Color") + "] WHERE [Code] = @code");
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				code = dataReader.GetValue(0).ToString();
				description = dataReader.GetValue(1).ToString();
				hexColor = dataReader.GetValue(2).ToString();
			}

			dataReader.Close();


		}

	}
}
