using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for GeneralLedgerSetup.
	/// </summary>
	public class GeneralLedgerSetup
	{
		private Database database;

		public string lcyCode;

		public GeneralLedgerSetup(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;	

			getFromDatabase();
		}


		private void getFromDatabase()
		{

			
			SqlDataReader dataReader = database.query("SELECT [LCY Code] FROM ["+database.getTableName("General Ledger Setup")+"]");
			if (dataReader.Read())
			{
				lcyCode = dataReader.GetValue(0).ToString();
			}

			dataReader.Close();
			
		}

	}
}
