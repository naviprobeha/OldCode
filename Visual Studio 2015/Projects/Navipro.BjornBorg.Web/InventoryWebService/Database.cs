using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.BjornBorg.Web
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Serializable]
    public class Database
    {
        private SqlConnection sqlConnection;
        private Configuration configuration;

        public Database(Configuration configuration)
        {
            this.configuration = configuration;

            open();
        }

        public Database newConnection()
        {
            Database newDatabase = new Database(configuration);
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

        public string getReplacementChars(string fieldName)
        {
            return "REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(" + fieldName + ", 'å', 'a'), 'ä', 'a'), 'ö', 'o'), ' ', '-'), '/', '-'), '&', '-')";
        }

        public string convertField(string fieldName)
        {
            return fieldName;
        }

        public string convertTableName(string tableName)
        {

            return tableName;
        }
    }
}

