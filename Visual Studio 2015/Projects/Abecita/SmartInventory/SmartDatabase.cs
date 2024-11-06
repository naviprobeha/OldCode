using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;


namespace SmartInventory
{
	/// <summary>
	/// Summary description for SmartDatabase.
	/// </summary>
	public class SmartDatabase
	{
		private string dbFileName;

		private SqlCeConnection sqlConnection;
		private SqlCeEngine sqlEngine;

        private bool lastTransactionHadError;

		public SmartDatabase(string dbFileName)
		{
			this.dbFileName = dbFileName;
			sqlEngine = new SqlCeEngine();
			sqlConnection = new SqlCeConnection();
		}

		public bool init()
		{
			if (File.Exists(dbFileName))
			{
				sqlConnection.ConnectionString = "Data Source = "+dbFileName;
				try
				{
					sqlConnection.Open();
					return true;
				}
				catch(Exception e)
				{
					System.Windows.Forms.MessageBox.Show(e.Message);
					return false;
				}
			}
			else
				return false;
		
		}

		public void createDatabase()
		{
			sqlEngine.LocalConnectionString = "Data Source = "+dbFileName;
			sqlEngine.CreateDatabase();

			init();
			nonQuery("CREATE TABLE item (no nvarchar(20) PRIMARY KEY, description nvarchar(50), baseUnit nvarchar(20), price float, productGroupCode nvarchar(20), seasonCode nvarchar(20), eanCode nvarchar(100), defaultQuantity float)");
			nonQuery("CREATE TABLE setup (primaryKey integer PRIMARY KEY, host nvarchar(150), port integer, agentId nvarchar(20), userId nvarchar(20), password nvarchar(20), receiver nvarchar(30), synchMethod integer, locationCode nvarchar(20), allowDecimal integer)");
			nonQuery("INSERT INTO setup (primaryKey, host, port, agentId, userId, password, receiver, synchMethod, locationCode, allowDecimal) values (1, '', 0, '', '', '', '', 0, '', 0)");
			nonQuery("CREATE TABLE itemCrossReference (itemNo nvarchar(20), variantDimCode nvarchar(20), unitCode nvarchar(20), type integer, no nvarchar(20), referenceNo nvarchar(100), description nvarchar(30))");

			nonQuery("CREATE TABLE bin (code nvarchar(20) PRIMARY KEY, locationCode nvarchar(20), zoneCode nvarchar(20), blocking integer, empty integer, inComplete integer)");
			nonQuery("CREATE TABLE itemUnit (code nvarchar(20) PRIMARY KEY, itemNo nvarchar(20), quantity float)");
			nonQuery("CREATE TABLE whseActivityHeader (no nvarchar(20), type integer, noOfLines integer)");
			nonQuery("CREATE TABLE whseActivityLine (seqNo integer PRIMARY KEY IDENTITY(1,1), whseActivityNo nvarchar(20), whseActivityType integer, lineEntryNo integer, zone nvarchar(20), locationCode nvarchar(20), binCode nvarchar(20), itemNo nvarchar(20), quantity float, handleUnitId nvarchar(20), freq integer, action integer, status integer, linkedToLineNo integer)");
			nonQuery("CREATE TABLE whseItemStore (seqNo integer PRIMARY KEY IDENTITY(1,1), locationCode nvarchar(20), binCode nvarchar(20), handleUnitId nvarchar(20), itemNo nvarchar(20), quantity float, toBinCode nvarchar(20))");


		}
		
		public SqlCeDataReader query(string queryString)
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = queryString;
			try
			{
				return cmd.ExecuteReader();
			}
			catch (SqlCeException e) 
			{
				ShowErrors(e);
			}
			
			return null;
		}

		public SqlCeDataAdapter dataAdapterQuery(string queryString)
		{
			return new SqlCeDataAdapter(queryString, sqlConnection);
		}

		public void nonQuery(string queryString)
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = queryString;
			
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlCeException e) 
			{
				ShowErrors(e);
			}
		}

		public int getInsertedId()
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = "SELECT @@IDENTITY as ID";

			SqlCeDataReader dataReader = cmd.ExecuteReader();
			if (dataReader.Read())
			{
				try
				{
					return dataReader.GetInt32(0);
				}
				catch (SqlCeException e) 
				{
					ShowErrors(e);
				}
			}
			return 0;
		}

		public void ShowErrors(SqlCeException e) 
		{
			SqlCeErrorCollection errorCollection = e.Errors;

			System.Text.StringBuilder bld = new System.Text.StringBuilder();
			Exception   inner = e.InnerException;

			foreach (SqlCeError err in errorCollection) 
			{
				bld.Append("\n Error Code: " + err.HResult.ToString("X"));
				bld.Append("\n Message   : " + err.Message);
				bld.Append("\n Minor Err.: " + err.NativeError);
				bld.Append("\n Source    : " + err.Source);
                
				foreach (int numPar in err.NumericErrorParameters) 
				{
					if (0 != numPar) bld.Append("\n Num. Par. : " + numPar);
				}
                
				foreach (string errPar in err.ErrorParameters) 
				{
					if (String.Empty != errPar) bld.Append("\n Err. Par. : " + errPar);
				}

				System.Windows.Forms.MessageBox.Show(bld.ToString());
				bld.Remove(0, bld.Length);
			}
		}

        public bool checkErrorFlag()
        {
            return lastTransactionHadError;
        }

        public void clearErrorFlag()
        {
            lastTransactionHadError = false;
        }

	}
}
