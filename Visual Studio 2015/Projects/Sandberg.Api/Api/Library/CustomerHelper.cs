using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Library
{
    public class CustomerHelper
    {
        public static List<Customer> GetCustomers()
        {
            return GetCustomers(0, 0);
        }
        public static List<Customer> GetCustomers(int offset, int count)
        {
            List<Customer> customerList = new List<Customer>();

            string offsetString = "";
            if (offset > 0)
            {
                offsetString = "OFFSET "+offset+" ROWS";
            }
            if (count > 0)
            {
                if (offset == 0)
                {
                    offsetString = "OFFSET 0 ROWS";
                }
                offsetString = offsetString + " FETCH NEXT "+count+" ROWS ONLY";
            }

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            SqlDataReader dataReader = database.query("SELECT [No_], [Name], [Address], [Address 2], [Post Code], [City], [County], [Country_Region Code], [Currency Code], [Shipping Agent Code], [Shipping Agent Service Code], [Customer Price Group], [Blocked], [Kundkategori], [Customer Disc_ Group], [Payment Method Code], [Web Allow Modify Ship-to Addr_], [Consolidated Shipping], [Corporate Chain Code], [Payment Terms Code], [Credit Limit (LCY)], [Bill-to Customer No_], [Shipment Method Code], [E-Mail], [Phone No_], [VAT Registration No_], [VAT Bus_ Posting Group], (SELECT TOP 1 [Code] FROM [" + database.getTableName("Cust_ Sandberg Partner Program") + "] WHERE [Customer No_] = c.[No_]) as partnerCode FROM [" + database.getTableName("Customer") + "] c ORDER BY [No_] "+offsetString);
            while (dataReader.Read())
            {
                Customer customer = new Customer(dataReader);
                customerList.Add(customer);
            }

            dataReader.Close();

            database.close();

            return customerList;       
        }

        public static Customer GetCustomer(string no)
        {
            Customer customer = null;

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Name], [Address], [Address 2], [Post Code], [City], [County], [Country_Region Code], [Currency Code], [Shipping Agent Code], [Shipping Agent Service Code], [Customer Price Group], [Blocked], [Kundkategori], [Customer Disc_ Group], [Payment Terms Code], [Web Allow Modify Ship-to Addr_], [Consolidated Shipping], [Corporate Chain Code], [Credit Limit (LCY)], [Bill-to Customer No_], [Shipment Method Code], [E-Mail], [Phone No_], [VAT Registration No_], (SELECT TOP 1 [Code] FROM [" + database.getTableName("Cust_ Sandberg Partner Program") + "] WHERE [Customer No_] = c.[No_]) as partnerCode, (SELECT SUM([Outstanding Amount (LCY)]) FROM [" + database.getTableName("Sales Line") + "] WHERE [Document Type] = 1 AND [Bill-to Customer No_] = c.[No_]) as outstandingOrderAmountLcy, [VAT Bus_ Posting Group] FROM [" + database.getTableName("Customer") + "] c WHERE [No_] = @no");
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                customer = new Customer(dataReader);

                dataReader.Close();

                DatabaseQuery databaseQuery2 = database.prepare("SELECT [Code], [Name], [Address], [Address 2], [Post Code], [City], [County], [Country_Region Code], [Shipping Agent Code], [Shipping Agent Service Code], [Contact], [Allow Consolidation], [Phone No_] FROM [" + database.getTableName("Ship-to Address") + "] WHERE [Customer No_] = @customerNo");
                databaseQuery2.addStringParameter("customerNo", no, 20);

                dataReader = databaseQuery2.executeQuery();
                while (dataReader.Read())
                {
                    ShipToAddress shipToAddress = new ShipToAddress(dataReader);
                    customer.delivery_addresses.Add(shipToAddress);

                }

                
            }

            dataReader.Close();

            database.close();


            return customer;
        }

        public static bool SubmitCustomer(Customer customer)
        {


            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery2 = database.prepare("SELECT TOP 1 [Entry No_] FROM [" + database.getTableName("API Customer") + "] WHERE [E-Mail] = @email");
            databaseQuery2.addStringParameter("email", customer.email, 100);

            SqlDataReader dataReader = databaseQuery2.executeQuery();
            if (dataReader.Read())
            {
                dataReader.Close();
                database.close();
                return false;
            }
            dataReader.Close();


            databaseQuery2 = database.prepare("SELECT TOP 1 [Entry No_] FROM [" + database.getTableName("API Customer") + "] ORDER BY [Entry No_] DESC");

            int entryNo = 1;
            dataReader = databaseQuery2.executeQuery();
            if (dataReader.Read())
            {
                entryNo = dataReader.GetInt32(0);
                entryNo++;
            }
            dataReader.Close();



            DatabaseQuery databaseQuery = database.prepare("INSERT INTO [" + database.getTableName("API Customer") + "] ([Entry No_], [Name], [Name 2], [Address], [Address 2], [Post Code], [City], [County], [Country Code], [Phone No_], [E-Mail], [VAT Registration No_], [Partner Code], [Status]) VALUES (@entryNo, @name, @name2, @address, @address2, @postCode, @city, @county, @countryCode, @phoneNo, @email, @vatRegistrationNo, @partnerCode, 0)");
            databaseQuery.addIntParameter("entryNo", entryNo);
            databaseQuery.addStringParameter("name", customer.name, 50);
            databaseQuery.addStringParameter("name2", "", 50);
            databaseQuery.addStringParameter("address", customer.address, 50);
            databaseQuery.addStringParameter("address2", customer.address2, 50);
            databaseQuery.addStringParameter("postCode", customer.post_code, 20);
            databaseQuery.addStringParameter("city", customer.city, 50);
            databaseQuery.addStringParameter("county", customer.county, 30);
            databaseQuery.addStringParameter("countryCode", customer.country_code, 20);
            databaseQuery.addStringParameter("phoneNo", customer.phone_no, 30);
            databaseQuery.addStringParameter("email", customer.email, 100);
            databaseQuery.addStringParameter("vatRegistrationNo", customer.vat_registration_no, 30);
            databaseQuery.addStringParameter("partnerCode", customer.partner_code, 30);

            databaseQuery.execute();

 
 
            database.close();

            return true;
        }
    }
}