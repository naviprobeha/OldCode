using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;


namespace Navipro.SmartInventory
{
	/// <summary>
	/// Summary description for SmartDatabase.
	/// </summary>
	public class SmartDatabase
	{
		private string dbFileName;
		private string lastSqlStatement;

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
                    updateTables();
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
			nonQuery("CREATE TABLE purchaseLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentType integer, documentNo nvarchar(20), orderLineNo integer, itemNo nvarchar(20), variantCode nvarchar(20), description nvarchar(50), unitOfMeasure nvarchar(20), quantity float, quantityReceived float, quantityToReceive float, ean nvarchar(50))");
            nonQuery("CREATE TABLE scanLine (entryNo integer PRIMARY KEY IDENTITY(1,1), type integer, documentType integer, documentNo nvarchar(20), orderLineNo integer, quantity float, cartonNo nvarchar(20))");

            nonQuery("CREATE TABLE salesHeader (entryNo integer PRIMARY KEY IDENTITY(1,1), documentType integer, no nvarchar(20), customerNo nvarchar(20), customerName nvarchar(50), address nvarchar(50), city nvarchar(50), countryCode nvarchar(20), orderDate dateTime, totalQty float)");
            nonQuery("CREATE TABLE salesLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentType integer, documentNo nvarchar(20), orderLineNo integer, itemNo nvarchar(20), variantCode nvarchar(20), description nvarchar(50), unitOfMeasure nvarchar(20), quantity float, quantityShipped float, quantityToShip float, ean nvarchar(50), carton nvarchar(20), originalLineNo integer)");

            nonQuery("CREATE TABLE salesLineCarton (entryNo integer PRIMARY KEY IDENTITY(1,1), documentType integer, documentNo nvarchar(20), orderLineNo integer, cartonNo nvarchar(20), splitOnQuantity float)");

		}

        private void updateTables()
        {
            try
            {
                SqlCeCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT carton FROM salesLine";
                SqlCeDataReader dataReader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                nonQuery("ALTER TABLE salesLine ADD carton nvarchar(20)");
                nonQuery("UPDATE salesLine SET carton = ''");
            }

            try
            {
                SqlCeCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT originalLineNo FROM salesLine";
                SqlCeDataReader dataReader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                nonQuery("ALTER TABLE salesLine ADD originalLineNo integer");
                nonQuery("UPDATE salesLine SET originalLineNo = 0");
            }

            try
            {
                SqlCeCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT cartonNo FROM scanLine";
                SqlCeDataReader dataReader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                nonQuery("ALTER TABLE scanLine ADD cartonNo nvarchar(20)");
                nonQuery("UPDATE scanLine SET cartonNo = ''");
            }

            try
            {
                SqlCeCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT cartonNo FROM salesLineCarton";
                SqlCeDataReader dataReader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                nonQuery("CREATE TABLE salesLineCarton (entryNo integer PRIMARY KEY IDENTITY(1,1), documentType integer, documentNo nvarchar(20), orderLineNo integer, cartonNo nvarchar(20), splitOnQuantity float)");
            }

        }
		
		public SqlCeDataReader query(string queryString)
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = queryString;
			lastSqlStatement = queryString;

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
			lastSqlStatement = queryString;
			return new SqlCeDataAdapter(queryString, sqlConnection);
		}

		public void nonQuery(string queryString)
		{
			lastSqlStatement = queryString;
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

            cmd.Dispose();
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

				System.Windows.Forms.MessageBox.Show(lastSqlStatement+", "+bld.ToString());
				bld.Remove(0, bld.Length);
			}
		}

		public void dispose()
		{
			this.sqlConnection.Close();
			this.sqlConnection = null;
		}

	}
}
