using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;


namespace SmartOrder
{
	/// <summary>
	/// Summary description for SmartDatabase.
	/// </summary>
	public class SmartDatabase
	{
		private string dbFileName;

		private SqlCeConnection sqlConnection;
		private SqlCeEngine sqlEngine;

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
			nonQuery("CREATE TABLE activeCustomer (customerNo nvarchar(20) PRIMARY KEY)");
			nonQuery("CREATE TABLE customer (no nvarchar(20) PRIMARY KEY, name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(30))");
			nonQuery("CREATE TABLE deliveryAddress (no integer PRIMARY KEY IDENTITY(1,1), customerNo nvarchar(20), code nvarchar(20), name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(50), contact nvarchar(50))");
			nonQuery("CREATE TABLE item (no nvarchar(20) PRIMARY KEY, description nvarchar(50), baseUnit nvarchar(20), price float, productGroupCode nvarchar(20), seasonCode nvarchar(20))");
			nonQuery("CREATE TABLE salesHeader (no integer PRIMARY KEY IDENTITY(1,1), customerNo nvarchar(20), name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(30), deliveryCode nvarchar(20), deliveryName nvarchar(50), deliveryAddress nvarchar(50), deliveryAddress2 nvarchar(50), deliveryZipCode nvarchar(20), deliveryCity nvarchar(30), deliveryContact nvarchar(50), ready integer, contact nvarchar(50), phoneNo nvarchar(20))");
			nonQuery("CREATE TABLE salesLine (no integer PRIMARY KEY IDENTITY(1,1), orderNo integer, itemNo nvarchar(20), colorCode nvarchar(20), sizeCode nvarchar(20), size2Code nvarchar(20), description nvarchar(50), baseUnit nvarchar(20), quantity float)");
			nonQuery("CREATE TABLE setup (primaryKey integer PRIMARY KEY, host nvarchar(150), port integer, agentId nvarchar(20), userId nvarchar(20), password nvarchar(20), receiver nvarchar(30))");
			nonQuery("INSERT INTO setup (primaryKey, host, port, agentId, userId, password, receiver) values (1, '', 0, '', '', '', '')");

			// Spira Fashion for Pocket PC

			nonQuery("CREATE TABLE color (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
			nonQuery("CREATE TABLE itemColor (itemNo nvarchar(20), colorCode nvarchar(20))");
			nonQuery("CREATE TABLE itemSize (itemNo nvarchar(20), sizeCode nvarchar(20))");
			nonQuery("CREATE TABLE itemSize2 (itemNo nvarchar(20), size2Code nvarchar(20))");
			nonQuery("CREATE TABLE size (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
			nonQuery("CREATE TABLE size2 (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
			nonQuery("CREATE TABLE season (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
			nonQuery("CREATE TABLE productGroup (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");

		}
		
		public SqlCeDataReader query(string queryString)
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = queryString;
			
			return cmd.ExecuteReader();
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

	}
}
