using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Xml;


namespace SmartOrder
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

        private DataSetup dataSetup;
        private Agent agent;

        private bool lastTransactionHadError;

        private int orderIdentitySeed;
        private int invoiceIdentitySeed;

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
                    this.dataSetup = new DataSetup(this);
                    this.agent = new Agent(this);

                    getConfiguration();

                    checkTables();

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
            getConfiguration();

            sqlEngine.LocalConnectionString = "Data Source = " + dbFileName;
            sqlEngine.CreateDatabase();

            init();
            nonQuery("CREATE TABLE activeCustomer (customerNo nvarchar(20) PRIMARY KEY)");
            nonQuery("CREATE TABLE customer (no nvarchar(20) PRIMARY KEY, name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(30), priceGroupCode nvarchar(20), blocked tinyint, customerDiscountGroup nvarchar(20), visible nvarchar(20), salesPersonCode nvarchar(20))");
            nonQuery("CREATE TABLE deliveryAddress (no integer PRIMARY KEY IDENTITY(1,1), customerNo nvarchar(20), code nvarchar(20), name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(50), contact nvarchar(50))");
            nonQuery("CREATE TABLE item (no nvarchar(20) PRIMARY KEY, description nvarchar(50), baseUnit nvarchar(20), price float, productGroupCode nvarchar(20), seasonCode nvarchar(20), eanCode nvarchar(100), defaultQuantity float, itemDiscountGroup nvarchar(20), lastUpdated datetime, searchDescription nvarchar(50), barcode1 nvarchar(30), barcode2 nvarchar(30), barcode3 nvarchar(30), barcode4 nvarchar(30), barcode5 nvarchar(30))");
            nonQuery("CREATE INDEX item_search on item (searchDescription)");

            if (orderIdentitySeed > 0)
            {
                nonQuery("CREATE TABLE salesHeader (no integer PRIMARY KEY IDENTITY(" + orderIdentitySeed + ",1), customerNo nvarchar(20), name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(30), deliveryCode nvarchar(20), deliveryName nvarchar(50), deliveryAddress nvarchar(50), deliveryAddress2 nvarchar(50), deliveryZipCode nvarchar(20), deliveryCity nvarchar(30), deliveryContact nvarchar(50), ready integer, contact nvarchar(50), phoneNo nvarchar(20), paymentMethod nvarchar(20), postingMethod integer, discount float, referenceCode nvarchar(20), salesPersonCode nvarchar(20), orderDate datetime)");
            }
            else
            {
                nonQuery("CREATE TABLE salesHeader (no integer PRIMARY KEY IDENTITY(100000,1), customerNo nvarchar(20), name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(30), deliveryCode nvarchar(20), deliveryName nvarchar(50), deliveryAddress nvarchar(50), deliveryAddress2 nvarchar(50), deliveryZipCode nvarchar(20), deliveryCity nvarchar(30), deliveryContact nvarchar(50), ready integer, contact nvarchar(50), phoneNo nvarchar(20), paymentMethod nvarchar(20), postingMethod integer, discount float, referenceCode nvarchar(20), salesPersonCode nvarchar(20), orderDate datetime)");
            }

            nonQuery("CREATE TABLE salesLine (no integer PRIMARY KEY IDENTITY(1,1), orderNo integer, itemNo nvarchar(20), colorCode nvarchar(20), sizeCode nvarchar(20), size2Code nvarchar(20), description nvarchar(50), baseUnit nvarchar(20), quantity float, discount float, deliveryDate nvarchar(20), unitPrice float, amount float, boxQuantity float)");
            nonQuery("CREATE TABLE lineDiscount (type integer, code nvarchar(20), salesType integer, salesCode nvarchar(20), startDate datetime, variantCode nvarchar(20), minimumQuantity float, discount float, endDate datetime)");
            nonQuery("CREATE TABLE setup (primaryKey integer PRIMARY KEY, host nvarchar(150), port integer, agentId nvarchar(20), userId nvarchar(20), password nvarchar(20), receiver nvarchar(30), spiraEnabled integer, spiraDelimiter nvarchar(10), itemScannerEnabled integer, startupForm integer, deliveryDateToday integer, itemSearchMethod integer, askSynchronization integer, askPostingMethod integer, showOrderItemsItemNo integer, showOrderItemsVariant integer, showOrderItemsBaseUnit integer, showOrderItemsDeliveryDate integer, useDynamicPrices integer, itemDeletionDays integer, printOnLocalPrinter integer)");
            nonQuery("INSERT INTO setup (primaryKey, host, port, agentId, userId, password, receiver, spiraEnabled, spiraDelimiter, itemScannerEnabled, startupForm, deliveryDateToday, itemSearchMethod, askSynchronization, askPostingMethod, showOrderItemsItemNo, showOrderItemsVariant, showOrderItemsBaseUnit, showOrderItemsDeliveryDate, useDynamicPrices, itemDeletionDays, printOnLocalPrinter) values (1, '', 0, '', '', '', '', '', '', 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 20, 0)");
            nonQuery("CREATE TABLE itemPrice (itemNo nvarchar(20), priceGroupCode nvarchar(20), startDate datetime, baseItemNo nvarchar(20), amount float, vatAmount float, discount float, discountAmount float, seasonCode nvarchar(20), type integer, variantCode nvarchar(20))");
            nonQuery("CREATE TABLE itemVariantDim (itemNo nvarchar(20), code nvarchar(20), description nvarchar(50), description2 nvarchar(50), boxQuantity float, unitPrice float)");
            nonQuery("CREATE TABLE itemCrossReference (itemNo nvarchar(20), variantDimCode nvarchar(20), unitCode nvarchar(20), type integer, no nvarchar(20), referenceNo nvarchar(100), description nvarchar(30))");
            nonQuery("CREATE TABLE productGroup (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
            nonQuery("CREATE TABLE paymentMethod (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
            nonQuery("CREATE TABLE salesPerson (code nvarchar(20) PRIMARY KEY)");
            nonQuery("CREATE TABLE userReference (code nvarchar(20) PRIMARY KEY)");

            // Spira Fashion for Pocket PC

            nonQuery("CREATE TABLE color (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
            nonQuery("CREATE TABLE itemColor (itemNo nvarchar(20), colorCode nvarchar(20))");
            nonQuery("CREATE TABLE itemSize (itemNo nvarchar(20), sizeCode nvarchar(20), sortOrder integer)");
            nonQuery("CREATE TABLE itemSize2 (itemNo nvarchar(20), size2Code nvarchar(20))");
            nonQuery("CREATE TABLE itemVariant (baseItemNo nvarchar(20), colorCode nvarchar(20), sizeCode nvarchar(20), size2Code nvarchar(20), valid integer, eanCode nvarchar(100))");
            nonQuery("CREATE TABLE size (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
            nonQuery("CREATE TABLE size2 (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");
            nonQuery("CREATE TABLE season (code nvarchar(20) PRIMARY KEY, description nvarchar(50))");

        }

        private void checkTables()
        {

            try
            {
                SqlCeDataReader dataReader = this.nonErrorQuery("SELECT barcode1 FROM item");
                dataReader.Close();
            }
            catch (Exception e)
            {
                nonQuery("ALTER TABLE item ADD barcode1 nvarchar(30)");
                nonQuery("ALTER TABLE item ADD barcode2 nvarchar(30)");
                nonQuery("ALTER TABLE item ADD barcode3 nvarchar(30)");
                nonQuery("ALTER TABLE item ADD barcode4 nvarchar(30)");
                nonQuery("ALTER TABLE item ADD barcode5 nvarchar(30)");
            }

            try
            {
                SqlCeDataReader dataReader = this.nonErrorQuery("SELECT * FROM salesPerson");
                dataReader.Close();
            }
            catch (Exception e)
            {
                nonQuery("CREATE TABLE userReference (code nvarchar(20) PRIMARY KEY)");
                nonQuery("CREATE TABLE salesPerson (code nvarchar(20) PRIMARY KEY)");
            }

            if (orderIdentitySeed > 0)
            {
                if (System.Windows.Forms.MessageBox.Show("Vill du återställa ordernrserien?", "Nrserie", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    nonQuery("DROP TABLE salesHeader");
                }
            }
            if (invoiceIdentitySeed > 0)
            {
                if (System.Windows.Forms.MessageBox.Show("Vill du återställa fakturanrserien?", "Nrserie", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    nonQuery("DROP TABLE invoiceNo");
                }
            }

            try
            {
                SqlCeDataReader dataReader = this.nonErrorQuery("SELECT * FROM salesHeader");
                dataReader.Close();
            }
            catch (Exception e)
            {
                if (orderIdentitySeed > 0)
                {
                    nonQuery("CREATE TABLE salesHeader (no integer PRIMARY KEY IDENTITY(" + orderIdentitySeed + ",1), customerNo nvarchar(20), name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(30), deliveryCode nvarchar(20), deliveryName nvarchar(50), deliveryAddress nvarchar(50), deliveryAddress2 nvarchar(50), deliveryZipCode nvarchar(20), deliveryCity nvarchar(30), deliveryContact nvarchar(50), ready integer, contact nvarchar(50), phoneNo nvarchar(20), paymentMethod nvarchar(20), postingMethod integer, discount float, referenceCode nvarchar(20), salesPersonCode nvarchar(20))");
                }
                else
                {
                    nonQuery("CREATE TABLE salesHeader (no integer PRIMARY KEY IDENTITY(100000,1), customerNo nvarchar(20), name nvarchar(50), address nvarchar(50), address2 nvarchar(50), zipCode nvarchar(20), city nvarchar(30), deliveryCode nvarchar(20), deliveryName nvarchar(50), deliveryAddress nvarchar(50), deliveryAddress2 nvarchar(50), deliveryZipCode nvarchar(20), deliveryCity nvarchar(30), deliveryContact nvarchar(50), ready integer, contact nvarchar(50), phoneNo nvarchar(20), paymentMethod nvarchar(20), postingMethod integer, discount float, referenceCode nvarchar(20), salesPersonCode nvarchar(20))");
                }
            }

            try
            {
                SqlCeDataReader dataReader = this.nonErrorQuery("SELECT * FROM invoiceNo");
                dataReader.Close();
            }
            catch (Exception e)
            {
                if (invoiceIdentitySeed > 0)
                {
                    nonQuery("CREATE TABLE invoiceNo (no integer PRIMARY KEY IDENTITY(" + invoiceIdentitySeed + ",1), orderNo nvarchar(20))");
                }
                else
                {
                    nonQuery("CREATE TABLE invoiceNo (no integer PRIMARY KEY IDENTITY(1,1), orderNo nvarchar(20))");
                }
            }

            try
            {
                SqlCeDataReader dataReader = this.nonErrorQuery("SELECT preInvoiceNo FROM salesHeader");
                dataReader.Close();
            }
            catch (Exception e)
            {
                nonQuery("ALTER TABLE salesHeader ADD preInvoiceNo nvarchar(20)");
            }

            try
            {
                SqlCeDataReader dataReader = this.nonErrorQuery("SELECT orderDate FROM salesHeader");
                dataReader.Close();
            }
            catch (Exception e)
            {
                nonQuery("ALTER TABLE salesHeader ADD orderDate datetime");
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

        public SqlCeDataReader nonErrorQuery(string queryString)
        {
            SqlCeCommand cmd = sqlConnection.CreateCommand();

            cmd.CommandText = queryString;
            lastSqlStatement = queryString;

            return cmd.ExecuteReader();
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
            lastTransactionHadError = true;

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

                System.Windows.Forms.MessageBox.Show(bld.ToString());
                bld.Remove(0, bld.Length);
            }
        }

        public void dispose()
        {
            this.sqlConnection.Close();
            this.sqlConnection = null;
        }

        public DataSetup getSetup()
        {
            return dataSetup;
        }

        public Agent getAgent()
        {
            return agent;
        }

        public void refresh()
        {
            dataSetup.refresh();
            agent.refresh();
        }

        public bool checkErrorFlag()
        {
            return lastTransactionHadError;
        }

        public void clearErrorFlag()
        {
            lastTransactionHadError = false;
        }

        private bool getConfiguration()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("\\smartorder.xml");

                XmlElement docElement = xmlDoc.DocumentElement;


                //OrderIdentitySeed
                XmlNodeList nodeList = docElement.GetElementsByTagName("orderIdentitySeed");
                if (nodeList.Count > 0)
                {
                    this.orderIdentitySeed = int.Parse(docElement.GetElementsByTagName("orderIdentitySeed").Item(0).FirstChild.Value);
                }

                //InvoiceIdentitySeed
                nodeList = docElement.GetElementsByTagName("invoiceIdentitySeed");
                if (nodeList.Count > 0)
                {
                    this.invoiceIdentitySeed = int.Parse(docElement.GetElementsByTagName("invoiceIdentitySeed").Item(0).FirstChild.Value);
                }

                return true;
            }
            catch (Exception e)
            {
                if (e.Message != "") { }

            }

            return false;
        }

    }
}
