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
                sqlConnection.ConnectionString = "Data Source = " + dbFileName;
                try
                {
                    sqlConnection.Open();

                    return true;
                }
                catch (Exception e)
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
            sqlEngine.LocalConnectionString = "Data Source = " + dbFileName;
            sqlEngine.CreateDatabase();

            init();
            nonQuery("CREATE TABLE pickHeader (entryNo integer PRIMARY KEY IDENTITY(1,1), no nvarchar(20), assignedTo nvarchar(30), noOfLines integer, description nvarchar(50))");
            //nonQuery("CREATE TABLE pickLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentNo nvarchar(20), docLineNo integer, binCode nvarchar(20), brand nvarchar(50), itemNo nvarchar(20), variantCode nvarchar(20), description nvarchar(50), description2 nvarchar(50), totalQty float, quantity float, pickedQty float, placeBinCode nvarchar(20), picked integer, placed integer, totalCount integer, action integer, inventory integer)");
            nonQuery("CREATE TABLE pickLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentNo nvarchar(20), docLineNo integer, binCode nvarchar(20), brand nvarchar(50), itemNo nvarchar(20), variantCode nvarchar(20), description nvarchar(50), description2 nvarchar(50), totalQty float, quantity float, pickedQty float, placeBinCode nvarchar(20), picked integer, placed integer, totalCount integer, action integer, inventory float)");
            nonQuery("CREATE TABLE itemCrossReference (entryNo integer PRIMARY KEY IDENTITY(1,1), documentNo nvarchar(20), itemNo nvarchar(20), variantCode nvarchar(20), unitOfMeasureCode nvarchar(20), crossReferenceCode nvarchar(50))");
            nonQuery("CREATE TABLE scanLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentType integer, documentNo nvarchar(20), docLineNo integer, quantity float)");

            nonQuery("CREATE TABLE storeHeader (entryNo integer PRIMARY KEY IDENTITY(1,1), no nvarchar(20), assignedTo nvarchar(20), noOfLines integer)");
            nonQuery("CREATE TABLE storeLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentNo nvarchar(20), docLineNo integer, binCode nvarchar(20), brand nvarchar(50), itemNo nvarchar(20), variantCode nvarchar(20), description nvarchar(50), description2 nvarchar(50), totalQty float, quantity float, pickedQty float, placeBinCode nvarchar(20), picked integer, placed integer, totalCount integer)");
            nonQuery("CREATE TABLE shipmentHeader (entryNo integer PRIMARY KEY IDENTITY(1,1), no nvarchar(20), name nvarchar(50), noOfLines integer)");
            nonQuery("CREATE TABLE itemBinContent (entryNo integer PRIMARY KEY IDENTITY(1,1), itemNo nvarchar(20), variantCode nvarchar(20), binCode nvarchar(20), quantity float)");
            nonQuery("CREATE TABLE pickItemLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentNo nvarchar(20), docLineNo integer, itemNo nvarchar(20), variantCode nvarchar(20), description nvarchar(50), description2 nvarchar(50))");
            //nonQuery("CREATE TABLE pickItemLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentNo nvarchar(20), itemNo nvarchar(20), variantCode nvarchar(20), description nvarchar(50), description2 nvarchar(50))");


            nonQuery("CREATE TABLE receiptHeader (entryNo integer PRIMARY KEY IDENTITY(1,1), no nvarchar(20), assignedTo nvarchar(20), noOfLines integer)");
            nonQuery("CREATE TABLE receiptLine (entryNo integer PRIMARY KEY IDENTITY(1,1), documentNo nvarchar(20), docLineNo integer, binCode nvarchar(20), brand nvarchar(50), itemNo nvarchar(20), variantCode nvarchar(20), description nvarchar(50), description2 nvarchar(50), quantity float, qtyToReceive float, weight float, length float, width float, height float)");

            nonQuery("CREATE TABLE purchaseOrder (entryNo integer PRIMARY KEY IDENTITY(1,1), itemNo nvarchar(20), variantCode nvarchar(20), documentNo nvarchar(20), description nvarchar(50), vendorName nvarchar(50), quantity float, outstandingQty float)");

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
            Exception inner = e.InnerException;

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

                System.Windows.Forms.MessageBox.Show(lastSqlStatement + ", " + bld.ToString());
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
