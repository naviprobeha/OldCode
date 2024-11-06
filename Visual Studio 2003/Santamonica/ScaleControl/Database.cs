using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.ScaleControl
{
	/// <summary>
	/// Summary description for Database.
	/// </summary>
	public class Database
	{
		private SqlConnection sqlConnection;
		private Logger logger;
		private Configuration configuration;
		private string lastSQLCommand;
		public string prefix;

		public Database(Logger logger, Configuration configuration)
		{
			this.configuration = configuration;
			this.logger = logger;
			sqlConnection = new SqlConnection("Database="+configuration.dataSource+";Server="+configuration.serverName+";Connect Timeout=30;User ID="+configuration.userName+";Password="+configuration.password);
			sqlConnection.Open();

			this.prefix = configuration.viktoriaPrefix;
		}

		public Database newConnection()
		{
			Database newDatabase = new Database(logger, configuration);
			return newDatabase;
		}

		public SqlDataReader query(string queryStr)
		{
			lastSQLCommand = queryStr;

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = queryStr;
			
			return sqlCommand.ExecuteReader();
		}

		public SqlDataAdapter dataAdapterQuery(string queryStr)
		{
			lastSQLCommand = queryStr;

			return new SqlDataAdapter(queryStr, sqlConnection);
		}

		public void nonQuery(string queryStr)
		{
			lastSQLCommand = queryStr;

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = queryStr;

			sqlCommand.ExecuteNonQuery();
		}

		public void nonQuery(string queryStr, bool log)
		{
			lastSQLCommand = queryStr;

			if (log) logger.write("[SQL] "+queryStr, 1);
			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = queryStr;

			sqlCommand.ExecuteNonQuery();
		}

		public object scalarQuery(string queryStr)
		{
			lastSQLCommand = queryStr;

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = queryStr;

			return sqlCommand.ExecuteScalar();
		}

		public void showErrors(string message)
		{
			if (logger != null) logger.write("SQL Error: "+message, 0);
		}

		public long getInsertedSeqNo()
		{
			string seqNo = "";

			SqlCommand sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandText = "SELECT @@identity as seqNo";

			SqlDataReader dataReader = sqlCommand.ExecuteReader();
			if (dataReader.Read()) seqNo = dataReader.GetValue(0).ToString();
			dataReader.Close();
			return long.Parse(seqNo);
		}

		public void close()
		{
			sqlConnection.Close();
			sqlConnection.Dispose();
		}

		public string getLastSQLCommand()
		{
			return lastSQLCommand;
		}

		public Configuration getConfiguration()
		{
			return configuration;
		}
	}
}
