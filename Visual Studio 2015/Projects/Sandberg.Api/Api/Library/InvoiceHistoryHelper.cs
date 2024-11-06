using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Library
{
    public class InvoiceHistoryHelper
    {
        public static List<InvoiceHistory> GetInvoiceHistory(string customerNo)
        {
            return GetInvoiceHistory(customerNo, DateTime.MinValue, DateTime.MinValue, 0, 0);
        }

        public static List<InvoiceHistory> GetInvoiceHistory(string customerNo, DateTime fromDate, DateTime toDate, int offset, int count, string customerOrderNo = "")
        {
            Customer customer = CustomerHelper.GetCustomer(customerNo);
            if (customer == null) throw new Exception("Invalid customer: " + customerNo);

            string query = "";
            if (fromDate == null) fromDate = DateTime.MinValue;
            if (toDate == null) toDate = DateTime.MinValue;
            if (fromDate.Year > 1990)
            {
                query = " AND [Document Date] >= '" + fromDate.ToString("yyyy-MM-dd") + "'";
            }
            if (toDate.Year > 1990)
            {
                query = query + " AND [Document Date] <= '" + toDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((customerOrderNo != "") && (customerOrderNo != null))
            {
                query = query + " AND [External Document No_] = @extDocNo";
            }

            if (count == 0) count = 100;

            List<InvoiceHistory> invoiceHistoryList = new List<InvoiceHistory>();

            string offsetString = "";
            if (offset > 0)
            {
                offsetString = "OFFSET " + offset + " ROWS";
            }
            if (count > 0)
            {
                if (offset == 0)
                {
                    offsetString = "OFFSET 0 ROWS";
                }
                offsetString = offsetString + " FETCH NEXT " + count + " ROWS ONLY";
            }

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT h.[No_], h.[Order No_], [Bill-to Customer No_], [Currency Code], [Bill-to Name], [Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country_Region Code], [Document Date], [Due Date], [External Document No_], [Note of Goods], (SELECT SUM([Amount]) FROM [" + database.getTableName("Sales Invoice Line") + "] l WHERE l.[Document No_] = h.[No_]) as Amount, (SELECT SUM([Amount Including VAT]) FROM [" + database.getTableName("Sales Invoice Line") + "] l WHERE l.[Document No_] = h.[No_]) as AmountIncludingVAT FROM [" + database.getTableName("Sales Invoice Header") + "] h WHERE [Bill-to Customer No_] = @customerNo "+query+" ORDER BY h.[Document Date] DESC "+offsetString);
            databaseQuery.addStringParameter("customerNo", customerNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                InvoiceHistory invoiceHistory = new InvoiceHistory(dataReader);
                invoiceHistoryList.Add(invoiceHistory);
            }

            dataReader.Close();


            database.close();

            return invoiceHistoryList;
        }


        public static InvoiceHistory GetInvoiceHistory(string customerNo, string no)
        {
            Customer customer = CustomerHelper.GetCustomer(customerNo);
            if (customer == null) throw new Exception("Invalid customer: " + customerNo);



            InvoiceHistory invoiceHistory = null;

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT h.[No_], h.[Order No_], [Bill-to Customer No_], [Currency Code], [Bill-to Name], [Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country_Region Code], [Document Date], [Due Date], [External Document No_], (SELECT SUM([Amount]) FROM [" + database.getTableName("Sales Invoice Line") + "] l WHERE l.[Document No_] = h.[No_]) as Amount, (SELECT SUM([Amount Including VAT]) FROM [" + database.getTableName("Sales Invoice Line") + "] l WHERE l.[Document No_] = h.[No_]) as AmountIncludingVAT FROM [" + database.getTableName("Sales Invoice Header") + "] h WHERE [Bill-to Customer No_] = @customerNo AND [No_] = @no");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                invoiceHistory = new InvoiceHistory(dataReader);
            }

            dataReader.Close();


            database.close();

            return invoiceHistory;
        }

    }
}