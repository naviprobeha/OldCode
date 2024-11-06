using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for WebPageLines.
    /// </summary>
    public class WebUserAccountCustomerRelations
    {
        private Infojet infojetContext;

        public WebUserAccountCustomerRelations(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
        }

        public CustomerCollection getCustomers(string searchName)
        {
            CustomerCollection customerCollection = new CustomerCollection();


            string searchQuery = "";
            if (searchName != "") searchQuery = " AND UPPER(c.[Name]) LIKE '%" + searchName.ToUpper() + "%' ";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Web User Account Customer Rel") + "] r WHERE r.[Web User Account No_] = @webUserAccountNo AND r.[Web Site Code] = @webSiteCode AND r.[Type] = 0");
            databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet relDataSet = new DataSet();
            sqlDataAdapter.Fill(relDataSet);

            if (relDataSet.Tables[0].Rows.Count > 0)
            {
                databaseQuery = infojetContext.systemDatabase.prepare("SELECT c.[No_], c.[Name], c.[Name 2], c.[Address], c.[Address 2], c.[Post Code], c.[City], c.[Currency Code], c.[Customer Price Group], c.[Country_Region Code], c.[Location Code], c.[Customer Disc_ Group], c.[Shipping Agent Code], c.[Shipment Method Code], c.[Shipping Agent Service Code], c.[Contact], c.[Phone No_], c.[E-Mail], c.[VAT Bus_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Customer") + "] c WHERE UPPER(c.[Name]) LIKE '%" + searchName.ToUpper()+ "%'");

                sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                DataSet customerDataSet = new DataSet();
                sqlDataAdapter.Fill(customerDataSet);

                int i = 0;
                while (i < customerDataSet.Tables[0].Rows.Count)
                {
                    Customer customer = new Customer(infojetContext.systemDatabase, customerDataSet.Tables[0].Rows[i]);
                    customerCollection.Add(customer);

                    i++;
                }

            }
            else
            {
                databaseQuery = infojetContext.systemDatabase.prepare("SELECT c.[No_], c.[Name], c.[Name 2], c.[Address], c.[Address 2], c.[Post Code], c.[City], c.[Currency Code], c.[Customer Price Group], c.[Country_Region Code], c.[Location Code], c.[Customer Disc_ Group], c.[Shipping Agent Code], c.[Shipment Method Code], c.[Shipping Agent Service Code], c.[Contact], c.[Phone No_], c.[E-Mail], c.[VAT Bus_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Customer") + "] c, [" + infojetContext.systemDatabase.getTableName("Web User Account Customer Rel") + "] r WHERE r.[Web User Account No_] = @webUserAccountNo AND r.[Web Site Code] = @webSiteCode AND r.[Type] = 2 AND r.[No_] = c.[Customer Price Group] AND UPPER(c.[Name]) LIKE '%" + searchName.ToUpper() + "%'");
                databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);
                databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);

                sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                DataSet customerDataSet = new DataSet();
                sqlDataAdapter.Fill(customerDataSet);

                int i = 0;
                while (i < customerDataSet.Tables[0].Rows.Count)
                {
                    Customer customer = new Customer(infojetContext.systemDatabase, customerDataSet.Tables[0].Rows[i]);
                    customerCollection.Add(customer);

                    i++;
                }

                databaseQuery = infojetContext.systemDatabase.prepare("SELECT c.[No_], c.[Name], c.[Name 2], c.[Address], c.[Address 2], c.[Post Code], c.[City], c.[Currency Code], c.[Customer Price Group], c.[Country_Region Code], c.[Location Code], c.[Customer Disc_ Group], c.[Shipping Agent Code], c.[Shipment Method Code], c.[Shipping Agent Service Code], c.[Contact], c.[Phone No_], c.[E-Mail], c.[VAT Bus_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Customer") + "] c, [" + infojetContext.systemDatabase.getTableName("Web User Account Customer Rel") + "] r WHERE r.[Web User Account No_] = @webUserAccountNo AND r.[Web Site Code] = @webSiteCode AND r.[Type] = 1 AND r.[No_] = c.[No_] AND UPPER(c.[Name]) LIKE '%" + searchName.ToUpper() + "%'");
                databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);
                databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);

                sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
                customerDataSet = new DataSet();
                sqlDataAdapter.Fill(customerDataSet);

                i = 0;
                while (i < customerDataSet.Tables[0].Rows.Count)
                {
                    Customer customer = new Customer(infojetContext.systemDatabase, customerDataSet.Tables[0].Rows[i]);
                    customerCollection.Add(customer);

                    i++;
                }
            }

            return customerCollection;
        }


        public bool customerRelationsExists()
        {
            if (infojetContext.userSession == null) return false;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_] FROM [" + infojetContext.systemDatabase.getTableName("Web User Account Customer Rel") + "] r WHERE r.[Web User Account No_] = @webUserAccountNo AND r.[Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            bool exists = false;
            if (dataReader.Read())
            {
                exists = true;
            }
            dataReader.Close();

            return exists;

        }


    }
}
