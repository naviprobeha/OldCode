using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for PostCode.
	/// </summary>
	public class PostCode
	{
		public string code;
		public string city;
		private Database database;

		public PostCode(Database database, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.code = code;

			getFromDatabase();
		}

		public PostCode(Database database, DataRow dataRow)
		{
			this.database = database;

			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.city = dataRow.ItemArray.GetValue(1).ToString();
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Code], [City] FROM [" + database.getTableName("Post Code") + "] WHERE [Code] = @code");
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				code = dataReader.GetValue(0).ToString();
				city = dataReader.GetValue(1).ToString();

			}

			dataReader.Close();


		}

	}
}
