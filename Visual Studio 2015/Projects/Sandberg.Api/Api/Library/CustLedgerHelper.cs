using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Library
{
    public class CustLedgerHelper
    {
        public static List<CustomerLedgerEntry> GetLedgerHistory(string customerNo)
        {

            List<CustomerLedgerEntry> customerLedgerEntryList = new List<CustomerLedgerEntry>();



            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            List<CustomerLedgerEntry> amountList = getAmountEntriesList(database, customerNo);
            List<CustomerLedgerEntry> remainingAmountList = getRemainingAmountEntriesList(database, customerNo);
            
            DatabaseQuery databaseQuery = database.prepare("SELECT [Entry No_], [Document Type], [Document No_], [Posting Date], [Due Date], [Currency Code] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] WHERE [Customer No_] = @customerNo AND [Open] = 1");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                CustomerLedgerEntry customerLedgerEntry = new CustomerLedgerEntry(dataReader, amountList, remainingAmountList);
                customerLedgerEntryList.Add(customerLedgerEntry);
            }

            dataReader.Close();


            database.close();

            return customerLedgerEntryList;
        }

        public static List<CustomerLedgerEntry> getAmountEntriesList(Database database, string customerNo)
        {

            List<CustomerLedgerEntry> amountList = new List<CustomerLedgerEntry>();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Cust_ Ledger Entry No_], SUM([Amount]) as amount, SUM([Amount (LCY)]) as amountLcy FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] d, [" + database.getTableName("Cust_ Ledger Entry") + "] l WHERE d.[Customer No_] = @customerNo AND d.[Cust_ Ledger Entry No_] = l.[Entry No_] AND l.[Open] = 1 AND (d.[Entry Type] <> 2 AND d.[Entry Type] <> 10 AND d.[Entry Type] <> 11) GROUP BY [Cust_ Ledger Entry No_]");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                CustomerLedgerEntry customerLedgerEntry = new CustomerLedgerEntry();
                customerLedgerEntry.entry_no = dataReader.GetInt32(dataReader.GetOrdinal("Cust_ Ledger Entry No_"));
                customerLedgerEntry.amount = dataReader.GetDecimal(dataReader.GetOrdinal("amount"));
                customerLedgerEntry.amount_lcy = dataReader.GetDecimal(dataReader.GetOrdinal("amountLcy"));
                amountList.Add(customerLedgerEntry);
            }

            dataReader.Close();

            return amountList;
        }

        public static List<CustomerLedgerEntry> getRemainingAmountEntriesList(Database database, string customerNo)
        {

            List<CustomerLedgerEntry> amountList = new List<CustomerLedgerEntry>();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Cust_ Ledger Entry No_], SUM([Amount]) as amount, SUM([Amount (LCY)]) as amountLcy FROM [" + database.getTableName("Detailed Cust_ Ledg_ Entry") + "] d, [" + database.getTableName("Cust_ Ledger Entry") + "] l WHERE d.[Customer No_] = @customerNo AND d.[Cust_ Ledger Entry No_] = l.[Entry No_] AND l.[Open] = 1 GROUP BY [Cust_ Ledger Entry No_]");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                CustomerLedgerEntry customerLedgerEntry = new CustomerLedgerEntry();
                customerLedgerEntry.entry_no = dataReader.GetInt32(dataReader.GetOrdinal("Cust_ Ledger Entry No_"));
                customerLedgerEntry.remaining_amount = dataReader.GetDecimal(dataReader.GetOrdinal("amount"));
                customerLedgerEntry.remaining_amount_lcy = dataReader.GetDecimal(dataReader.GetOrdinal("amountLcy"));
                amountList.Add(customerLedgerEntry);
            }

            dataReader.Close();

            return amountList;
        }
    }
}