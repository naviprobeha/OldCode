using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Base.Common
{
    public class Database
    {

		private SqlConnection sqlConnection;
		private Logger logger;
		private Configuration configuration;
		private string lastSQLCommand;

		public Database(Logger logger, Configuration configuration)
		{
			this.configuration = configuration;
			this.logger = logger;
			sqlConnection = new SqlConnection("Database="+configuration.getConfigValue("database")+";Server="+configuration.getConfigValue("serverName")+";Connect Timeout=30;User ID="+configuration.getConfigValue("userName")+";Password="+configuration.getConfigValue("password")+";Max Pool Size=1000;");
			sqlConnection.Open();
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

			if (log) logger.write("SQL", 1, queryStr);
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
			if (logger != null) logger.write("SQL", 1, "Error: "+message);
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

        public string getNavTableName(string tableName)
        {
            return configuration.getConfigValue("navCompanyName") + "$" + tableName;
        }
	}
}
