using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Navipro.Apcoa.ContractParker.Library.Data
{
    public class CustomerLedgerEntry
    {

        private int _entryNo;
        private string _customerNo;
        private DateTime _postingDate;
        private int _documentType;
        private string _documentNo;
        private string _description;
        private decimal _amount;
        private decimal _remainingAmount;
        private string _globalDimension1Code;
        private string _globalDimension2Code;
        private int _appliesToDocType;
        private string _appliesToDocNo;
        private bool _open;
        private DateTime _dueDate;
        private int _closedByEntryNo;
        private DateTime _closedAtDate;
        private string _externalDocumentNo;
        private string _contractNo;
        private string _contractType;
        private int _reminderLevel;

        public CustomerLedgerEntry() { }

        public CustomerLedgerEntry(SqlDataReader dataReader)
        {
            _entryNo = dataReader.GetInt32(0);
            _customerNo = dataReader.GetValue(1).ToString();
            _postingDate = dataReader.GetDateTime(2);
            _documentType = dataReader.GetInt32(3);
            _documentNo = dataReader.GetValue(4).ToString();
            _description = dataReader.GetValue(5).ToString();
            _globalDimension1Code = dataReader.GetValue(6).ToString();
            _globalDimension2Code = dataReader.GetValue(7).ToString();
            _appliesToDocType = dataReader.GetInt32(8);
            _appliesToDocNo = dataReader.GetValue(9).ToString();
            
            _open = false;
            if (dataReader.GetValue(10).ToString() == "1") _open = true;

            _dueDate = dataReader.GetDateTime(11);
            _closedByEntryNo = dataReader.GetInt32(12);
            _closedAtDate = dataReader.GetDateTime(13);

            _externalDocumentNo = dataReader.GetValue(14).ToString();
            _contractNo = dataReader.GetValue(15).ToString();
            _contractType = dataReader.GetValue(18).ToString();
            _reminderLevel = dataReader.GetInt32(19);
        }

        public CustomerLedgerEntry(DataRow dataRow)
        {
            _entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            _customerNo = dataRow.ItemArray.GetValue(1).ToString();
            _postingDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
            _documentType = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            _documentNo = dataRow.ItemArray.GetValue(4).ToString();
            _description = dataRow.ItemArray.GetValue(5).ToString();
            _globalDimension1Code = dataRow.ItemArray.GetValue(6).ToString();
            _globalDimension2Code = dataRow.ItemArray.GetValue(7).ToString();
            _appliesToDocType = int.Parse(dataRow.ItemArray.GetValue(8).ToString());
            _appliesToDocNo = dataRow.ItemArray.GetValue(9).ToString();

            _open = false;
            if (dataRow.ItemArray.GetValue(10).ToString() == "1") _open = true;

            _dueDate = DateTime.Parse(dataRow.ItemArray.GetValue(11).ToString());
            _closedByEntryNo = int.Parse(dataRow.ItemArray.GetValue(12).ToString());
            _closedAtDate = DateTime.Parse(dataRow.ItemArray.GetValue(13).ToString());

            _externalDocumentNo = dataRow.ItemArray.GetValue(14).ToString();
            _contractNo = dataRow.ItemArray.GetValue(15).ToString();

            _amount = decimal.Parse(dataRow.ItemArray.GetValue(16).ToString());
            _remainingAmount = decimal.Parse(dataRow.ItemArray.GetValue(17).ToString());
            _contractType = dataRow.ItemArray.GetValue(18).ToString();
            _reminderLevel = int.Parse(dataRow.ItemArray.GetValue(19).ToString());

        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public string customerNo { get { return _customerNo; } set { _customerNo = value; } }
        public DateTime postingDate { get { return _postingDate; } set { _postingDate = value; } }
        public int documentType { get { return _documentType; } set { _documentType = value; } }
        public string documentNo { get { return _documentNo; } set { _documentNo = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string globalDimension1Code { get { return _globalDimension1Code; } set { _globalDimension1Code = value; } }
        public string globalDimension2Code { get { return _globalDimension2Code; } set { _globalDimension2Code = value; } }
        public int appliesToDocType { get { return _appliesToDocType; } set { _appliesToDocType = value; } }
        public string appliesToDocNo { get { return _appliesToDocNo; } set { _appliesToDocNo = value; } }
        public bool open { get { return _open; } set { _open = value; } }
        public DateTime dueDate { get { return _dueDate; } set { _dueDate = value; } }
        public int closedByEntryNo { get { return _closedByEntryNo; } set { _closedByEntryNo = value; } }
        public DateTime closedAtDate { get { return _closedAtDate; } set { _closedAtDate = value; } }
        public string externalDocumentNo { get { return _externalDocumentNo; } set { _externalDocumentNo = value; } }
        public string contractNo { get { return _contractNo; } set { _contractNo = value; } }
        public string contractType { get { return _contractType; } set { _contractType = value; } }
        public decimal amount { get { return _amount; } set { _amount = value; } }
        public decimal remainingAmount { get { return _remainingAmount; } set { _remainingAmount = value; } }
        public int reminderLevel { get { return _reminderLevel; } set { _reminderLevel = value; } }


        public static CustomerLedgerEntryCollection getCustomerEntries(Database database, string customerNo, DateTime fromDate, DateTime toDate)
        {
            if (database.getCompanyName() != "EuroPark Svenska AB") if (fromDate < DateTime.Parse("2017-04-01 00:00:00")) fromDate = DateTime.Parse("2017-04-01 00:00:00");

            CustomerLedgerEntryCollection customerLedgerEntryCollection = new CustomerLedgerEntryCollection();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Entry No_], [Customer No_], [Posting Date], [Document Type], [Document No_], [Description], [Global Dimension 1 Code], [Global Dimension 2 Code], [Applies-to Doc_ Type], [Applies-to Doc_ No_], "+
                "[Open], [Due Date], [Closed by Entry No_], [Closed at Date], [External Document No_], [Contract No_], (SELECT SUM(Amount) FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] WHERE [Cust_ Ledger Entry No_] = l.[Entry No_] AND [Entry Type] = 1) as amount, (SELECT SUM(Amount) FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] WHERE [Cust_ Ledger Entry No_] = l.[Entry No_]) as remainingAmount, (SELECT [Contract Type Description] FROM [" + database.getTableName("Contract Header") + "] WHERE [Contract No_] = l.[Contract No_]) as contractType, [Last Issued Reminder Level] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] l WHERE [Customer No_] = @customerNo AND [Posting Date] >= @fromDate AND [Posting Date] <= @toDate");

            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                CustomerLedgerEntry customerLedgerEntry = new CustomerLedgerEntry(dataSet.Tables[0].Rows[i]);
                customerLedgerEntryCollection.Add(customerLedgerEntry);

                i++;
            }

            return customerLedgerEntryCollection;
        }

        public static CustomerLedgerEntryCollection getContractEntries(Database database, string contractNo, DateTime fromDate, DateTime toDate)
        {
            if (database.getCompanyName() != "EuroPark Svenska AB") if (fromDate < DateTime.Parse("2017-04-01 00:00:00")) fromDate = DateTime.Parse("2017-04-01 00:00:00");

            CustomerLedgerEntryCollection customerLedgerEntryCollection = new CustomerLedgerEntryCollection();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Entry No_], [Customer No_], [Posting Date], [Document Type], [Document No_], [Description], [Global Dimension 1 Code], [Global Dimension 2 Code], [Applies-to Doc_ Type], [Applies-to Doc_ No_], " +
                "[Open], [Due Date], [Closed by Entry No_], [Closed at Date], [External Document No_], [Contract No_], (SELECT SUM(Amount) FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] WHERE [Cust_ Ledger Entry No_] = l.[Entry No_] AND [Entry Type] = 1) as amount, (SELECT SUM(Amount) FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] WHERE [Cust_ Ledger Entry No_] = l.[Entry No_]) as remainingAmount, (SELECT [Contract Type Description] FROM [" + database.getTableName("Contract Header") + "] WHERE [Contract No_] = l.[Contract No_]) as contractType, [Last Issued Reminder Level] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] l WHERE [Contract No_] = @contractNo AND [Posting Date] >= @fromDate AND [Posting Date] <= @toDate");

            databaseQuery.addStringParameter("contractNo", contractNo, 20);
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                CustomerLedgerEntry customerLedgerEntry = new CustomerLedgerEntry(dataSet.Tables[0].Rows[i]);
                customerLedgerEntryCollection.Add(customerLedgerEntry);

                i++;
            }

            return customerLedgerEntryCollection;
        }

        public static CustomerLedgerEntryCollection getEntriesByDate(Database database, DateTime fromDate, DateTime toDate)
        {
            if (database.getCompanyName() != "EuroPark Svenska AB") if (fromDate < DateTime.Parse("2017-04-01 00:00:00")) fromDate = DateTime.Parse("2017-04-01 00:00:00");

            CustomerLedgerEntryCollection customerLedgerEntryCollection = new CustomerLedgerEntryCollection();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Entry No_], [Customer No_], [Posting Date], [Document Type], [Document No_], [Description], [Global Dimension 1 Code], [Global Dimension 2 Code], [Applies-to Doc_ Type], [Applies-to Doc_ No_], " +
                "[Open], [Due Date], [Closed by Entry No_], [Closed at Date], [External Document No_], [Contract No_], (SELECT SUM(Amount) FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] WHERE [Cust_ Ledger Entry No_] = l.[Entry No_] AND [Entry Type] = 1) as amount, (SELECT SUM(Amount) FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] WHERE [Cust_ Ledger Entry No_] = l.[Entry No_]) as remainingAmount, (SELECT [Contract Type Description] FROM [" + database.getTableName("Contract Header") + "] WHERE [Contract No_] = l.[Contract No_]) as contractType, [Last Issued Reminder Level] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] l WHERE [Posting Date] >= @fromDate AND [Posting Date] <= @toDate");

            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                CustomerLedgerEntry customerLedgerEntry = new CustomerLedgerEntry(dataSet.Tables[0].Rows[i]);
                customerLedgerEntryCollection.Add(customerLedgerEntry);

                i++;
            }

            return customerLedgerEntryCollection;
        }

        public static CustomerLedgerEntryCollection getDueEntries(Database database, DateTime fromDate, DateTime toDate)
        {
            if (database.getCompanyName() != "EuroPark Svenska AB") if (fromDate < DateTime.Parse("2017-04-01 00:00:00")) fromDate = DateTime.Parse("2017-04-01 00:00:00");

            CustomerLedgerEntryCollection customerLedgerEntryCollection = new CustomerLedgerEntryCollection();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Entry No_], [Customer No_], [Posting Date], [Document Type], [Document No_], [Description], [Global Dimension 1 Code], [Global Dimension 2 Code], [Applies-to Doc_ Type], [Applies-to Doc_ No_], " +
                "[Open], [Due Date], [Closed by Entry No_], [Closed at Date], [External Document No_], [Contract No_], (SELECT SUM(Amount) FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] WHERE [Cust_ Ledger Entry No_] = l.[Entry No_] AND [Entry Type] = 1) as amount, (SELECT SUM(Amount) FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] WHERE [Cust_ Ledger Entry No_] = l.[Entry No_]) as remainingAmount, (SELECT [Contract Type Description] FROM [" + database.getTableName("Contract Header") + "] WHERE [Contract No_] = l.[Contract No_]) as contractType, [Last Issued Reminder Level] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] l WHERE [Open] = 1 AND [Due Date] >= @fromDate AND [Due Date] <= @toDate");

            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", fromDate);


            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                CustomerLedgerEntry customerLedgerEntry = new CustomerLedgerEntry(dataSet.Tables[0].Rows[i]);
                customerLedgerEntryCollection.Add(customerLedgerEntry);

                i++;
            }

            return customerLedgerEntryCollection;
        }


    }

}
