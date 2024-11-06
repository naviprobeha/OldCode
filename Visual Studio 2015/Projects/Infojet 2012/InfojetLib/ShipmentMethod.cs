using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for PostCode.
	/// </summary>
	public class ShipmentMethod
	{
		public string code;
		public string description;
		private Database database;

		public ShipmentMethod(Database database, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.code = code;

			getFromDatabase();
		}

		public ShipmentMethod(Database database, DataRow dataRow)
		{
			this.database = database;

			this.code = dataRow.ItemArray.GetValue(0).ToString();
			this.description = dataRow.ItemArray.GetValue(1).ToString();
		}

		private void getFromDatabase()
		{
			SqlDataReader dataReader = database.query("SELECT [Code], [Description] FROM ["+database.getTableName("Shipment Method")+"] WHERE [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{

				code = dataReader.GetValue(0).ToString();
				description = dataReader.GetValue(1).ToString();

			}

			dataReader.Close();


		}

	}
}
