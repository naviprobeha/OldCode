using System;
using System.Data;
using System.Data.SqlClient;

namespace Api.Library
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class Database
    {
        private SqlConnection sqlConnection;
        public Configuration configuration { get; private set; }
        public string connectionString { get; private set; }

        public Database(Configuration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.connectionString;

            open();
        }

        public Database(Configuration configuration, string connectionString)
        {
            this.configuration = configuration;
            this.connectionString = connectionString;

            open();
        }

        public Database newConnection()
        {
            Database newDatabase = new Database(configuration);
            return newDatabase;
        }

        private void open()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public void reOpen()
        {


        }

        public SqlDataReader query(string queryStr)
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = queryStr;

            try
            {
                return sqlCommand.ExecuteReader();
            }
            catch (InvalidOperationException)
            {

                this.close();
                this.open();
                return query(queryStr);
            }
        }

        public DatabaseQuery prepare(string queryStr)
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = queryStr;

            return new DatabaseQuery(this, sqlCommand);

        }


        public SqlDataAdapter dataAdapterQuery(string queryStr)
        {
            return new SqlDataAdapter(queryStr, sqlConnection);
        }

        public void nonQuery(string queryStr)
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = queryStr;

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (InvalidOperationException)
            {
                this.close();
                this.open();
                nonQuery(queryStr);
            }
        }

        public void nonQuery(string queryStr, bool log)
        {

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = queryStr;

            sqlCommand.ExecuteNonQuery();
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }

        public object scalarQuery(string queryStr)
        {
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = queryStr;

            try
            {
                return sqlCommand.ExecuteScalar();
            }
            catch (InvalidOperationException)
            {
                this.close();
                this.open();
                return scalarQuery(queryStr);
            }
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

        }

        public string getTableName(string tableName)
        {
            if ((configuration.companyName != null) && (configuration.companyName != "")) return configuration.companyName + "$" + tableName;
            return transformName(tableName);
        }


        public string transformName(string name)
        {
            name = name.Replace(".", "_");
            name = name.Replace("/", "_");

            return name;
        }

    }
}

