using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for PostCode.
	/// </summary>
	public class ShippingAgent
	{
		public string code;
		public string name;
		public string internetAddress;
		private Database database;

		public ShippingAgent(Database database, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.code = code;

			getFromDatabase();
		}

		public ShippingAgent(Database database, DataRow dataRow)
		{
			this.database = database;

			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.name = dataRow.ItemArray.GetValue(1).ToString();
			this.internetAddress = dataRow.ItemArray.GetValue(2).ToString();
		}

		private void getFromDatabase()
		{
			SqlDataReader dataReader = database.query("SELECT [Code], [Name], [Internet Address] FROM ["+database.getTableName("Shipping Agent")+"] WHERE [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{

				code = dataReader.GetValue(0).ToString();
				name = dataReader.GetValue(1).ToString();
				internetAddress = dataReader.GetValue(2).ToString();

			}

			dataReader.Close();


		}

	}
}
