using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Lib;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Models
{
    public class Customer
    {
        public Customer()
        {
            name = "";

        }

        public Customer(SqlDataReader dataReader)
        {
            name = "";
            fromDataReader(dataReader);

        }

        private void fromDataReader(SqlDataReader dataReader)
        {
            no = dataReader.GetValue(0).ToString();
            name = dataReader.GetValue(1).ToString();
            blocked = int.Parse(dataReader.GetValue(2).ToString());
            if (!dataReader.IsDBNull(3)) firstTransactionDate = dataReader.GetDateTime(3);

            address = dataReader.GetValue(4).ToString();
            address2 = dataReader.GetValue(5).ToString();
            postCode = dataReader.GetValue(6).ToString();
            city = dataReader.GetValue(7).ToString();
            countryCode = dataReader.GetValue(8).ToString();

            if (countryCode == "") countryCode = "SE";
        }


        [Required]
        public String no { get; set; }

        [Required]
        public String name { get; set; }

        public String address { get; set; }

        public String address2 { get; set; }

        public String postCode { get; set; }

        public String city { get; set; }

        public String countryCode { get; set; }


        public int blocked { get; set; }

        public DateTime firstTransactionDate { get; private set; }

        public double noOfCases { get; private set; }

        public int caseProcent { get; private set; }
        public int totalNoOfCases { get; private set; }

        public string status { get { if (blocked == 0) return "Aktiv"; return "Spärrad"; } }
        public string statusIcon { get { if (blocked == 0) return "label-primary"; return "label-error"; } }
        public static Customer getEntry(Database database, string no)
        {
            DatabaseQuery query = database.prepare("SELECT [No_], [Name], [Blocked], (SELECT TOP 1 [Posting Date] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] WITH (NOLOCK) WHERE [Customer No_] = @no ORDER BY [Posting Date]) as [First Transaction Date], [Address], [Address 2], [Post Code], [City], [Country_Region Code] FROM [" + database.getTableName("Customer") + "] WHERE [No_] = @no");
            query.addStringParameter("no", no, 20);

            Customer customer = new Customer();

            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                customer = new Customer(dataReader);

            }
            dataReader.Close();

            return customer;
        }

        public static Dictionary<string, Customer> getDictionary(Database database)
        {
            Dictionary<string, Customer> customerTable = new Dictionary<string, Customer>();

            DatabaseQuery query = database.prepare("SELECT [No_], [Name], [Blocked], (SELECT TOP 1 [Posting Date] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] WITH (NOLOCK) WHERE [Customer No_] = c.[No_] ORDER BY [Posting Date]) as [First Transaction Date], [Address], [Address 2], [Post Code], [City], [Country_Region Code] FROM [" + database.getTableName("Customer") + "] c");          

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                Customer customer = new Customer(dataReader);
                customerTable.Add(customer.no, customer);

            }
            dataReader.Close();

            return customerTable;
        }

        public static void search(Database database, string searchQuery, ref List<SearchResult> searchResults)
        {
            DatabaseQuery query = database.prepare("SELECT [No_], [Name], [Blocked], (SELECT TOP 1 [Posting Date] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] WITH (NOLOCK) WHERE [Customer No_] = c.[No_] ORDER BY [Posting Date]) as [First Transaction Date], [Address], [Address 2], [Post Code], [City], [Country_Region Code] FROM [" + database.getTableName("Customer") + "] c WHERE [Name] LIKE @searchQuery");
            query.addStringParameter("searchQuery", "%"+searchQuery+"%", 100);

            

            SqlDataReader dataReader = query.executeQuery();
            while(dataReader.Read())
            {

                SearchResult searchResult = new SearchResult();
                searchResult.caption = "Kund - " + dataReader.GetValue(0).ToString() + " - " + dataReader.GetValue(1).ToString();
                searchResult.url = "/Customer/Details/" + dataReader.GetValue(0).ToString();

                searchResults.Add(searchResult);
            }
            dataReader.Close();
           
        }

        public static List<Customer> getList(Database database, DataView dataView)
        {
            List<Customer> customerList = new List<Customer>();

            string filterString = "";
            string noOfRecords = "";
            string orderByString = "";
            if (dataView != null)
            {
                if ((dataView.query != "") && (filterString != "")) filterString = filterString + " AND ";
                filterString = filterString + dataView.query;
                if (dataView.noOfRecords > 0) noOfRecords = "TOP " + dataView.noOfRecords;
                if (dataView.orderBy != "") orderByString = dataView.orderBy;
            }


            DatabaseQuery query = database.prepare("SELECT "+noOfRecords+ " [No_], [Name], [Blocked], (SELECT TOP 1 [Posting Date] FROM [" + database.getTableName("Cust_ Ledger Entry") + "] WITH (NOLOCK) WHERE [Customer No_] = c.[No_] ORDER BY [Posting Date]) as [First Transaction Date], [Address], [Address 2], [Post Code], [City], [Country_Region Code] FROM [" + database.getTableName("Customer") + "] c "+filterString+" "+orderByString);

            SqlDataReader dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                Customer customer = new Customer(dataReader);
                customerList.Add(customer);

            }
            dataReader.Close();


            DataView lastYearCases = new DataView();
            lastYearCases.query = "[Received Date] >= '" + DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd") + "'";
            double totalCases = (double)Case.countView(database, lastYearCases);


            query = database.prepare("SELECT [Customer No_], COUNT(*) FROM [" + database.getTableName("Case") + "] WITH (NOLOCK) WHERE [Received Date] >= '" + DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd") + "' GROUP BY [Customer No_]");

            Dictionary<string, int> statsTable = new Dictionary<string, int>();

            dataReader = query.executeQuery();
            while (dataReader.Read())
            {
                statsTable.Add(dataReader.GetValue(0).ToString(), int.Parse(dataReader.GetValue(1).ToString()));
            }

            dataReader.Close();

            foreach (Customer customer in customerList)
            {
                if (statsTable.ContainsKey(customer.no)) customer.noOfCases = statsTable[customer.no];
                customer.caseProcent = (int)((customer.noOfCases / totalCases) * 100);
                customer.totalNoOfCases = (int)totalCases;
            }

            return customerList;
        }

        public static List<SelectListItem> getSelectList(Database database)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<Customer> customerList = getList(database, null);

            foreach (Customer item in customerList)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Value = item.no;
                selectItem.Text = item.no + " - " + item.name;
                selectList.Add(selectItem);

            }

            return selectList;
        }
    }



}