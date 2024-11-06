using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ExtendedTextHeader
	{
		private Database database;

		public int tableName;
		public string no;
		public string languageCode;
		public int textNo;
		public DateTime startingDate;
		public DateTime endingDate;
		public bool allLanguageCodes;

		public ExtendedTextHeader(Database database, int tableName, string no, string languageCode, int textNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.tableName = tableName;
			this.no = no;
			this.languageCode = languageCode;
			this.textNo = textNo;

			getFromDatabase();
		}

		public ExtendedTextHeader(Database database, DataRow dataRow)
		{
			this.database = database;
			fromDataRow(dataRow);
		}

		private void fromDataRow(DataRow dataRow)
		{
			this.tableName = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.no = dataRow.ItemArray.GetValue(1).ToString();
			this.languageCode = dataRow.ItemArray.GetValue(2).ToString();
			this.textNo = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.startingDate = DateTime.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.endingDate = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());

			this.allLanguageCodes = false;
			if (dataRow.ItemArray.GetValue(6).ToString() == "1") this.allLanguageCodes = true;

		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Table Name], [No_], [Language Code], [Text No_], [Starting Date], [Ending Date], [All Language Codes] FROM [" + database.getTableName("Extended Text Header") + "] WHERE [Table Name] = @tableName AND [No_] = @no AND [Language Code] = @languageCode AND [Text No_] = @textNo");
            databaseQuery.addIntParameter("tableName", tableName);
            databaseQuery.addStringParameter("no", no, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addIntParameter("textNo", textNo);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				tableName = dataReader.GetInt32(0);
				no = dataReader.GetValue(1).ToString();
				languageCode = dataReader.GetValue(2).ToString();
				textNo = dataReader.GetInt32(3);
				startingDate = dataReader.GetDateTime(4);
				endingDate = dataReader.GetDateTime(5);

				allLanguageCodes = false;
				if (dataReader.GetValue(6).ToString() == "1") allLanguageCodes = true;

			}

			dataReader.Close();


		}

	}
}
