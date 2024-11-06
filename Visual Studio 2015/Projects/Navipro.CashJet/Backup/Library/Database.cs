using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Cashjet.Library
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class Database
    {
        private SqlConnection sqlConnection;
        private Logger logger;
        private Configuration configuration;

        public Database(Logger logger, Configuration configuration)
        {
            this.configuration = configuration;
            this.logger = logger;

            open();
        }

        public Database newConnection()
        {
            Database newDatabase = new Database(logger, configuration);
            return newDatabase;
        }

        private void open()
        {
            sqlConnection = new SqlConnection(configuration.connectionString);
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

            if (log) logger.write("[SQL] " + queryStr, 1);
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

        public void showErrors(string message)
        {
            if (logger != null) logger.write("SQL Error: " + message, 0);
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

        public string formatCurrency(float amount, string currencyCode)
        {
            return formatCurrency(amount) + " " + currencyCode;
        }

        public string formatCurrency(float amount)
        {
            System.Globalization.NumberFormatInfo nb = new System.Globalization.NumberFormatInfo();
            nb.CurrencyDecimalDigits = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalDigits;
            nb.CurrencyDecimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
            nb.CurrencyGroupSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator;
            nb.CurrencySymbol = "";

            return amount.ToString("C", nb);
        }

        public string transformName(string name)
        {
            name = name.Replace(".", "_");
            name = name.Replace("/", "_");

            return name;
        }

        public bool allowZeroVat()
        {
            return (configuration.allowZeroVat == "true");

        }

    }
}

