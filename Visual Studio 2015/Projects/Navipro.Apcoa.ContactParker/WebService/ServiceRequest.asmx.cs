using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Navipro.Apcoa.ContractParker.Library;
using Navipro.Apcoa.ContractParker.Library.Data;

namespace Navipro.Apcoa.ContractParker.WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://navipro.apcoa.contractparker.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceRequest : System.Web.Services.WebService
    {

        [WebMethod]
        public SalesInvoiceHeader getSalesInvoiceHeader(string no)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);

            SalesInvoiceHeader salesInvoiceHeader = null;

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);
                salesInvoiceHeader = SalesInvoiceHeader.getEntry(database, no);
                if (salesInvoiceHeader != null)
                {
                    database.close();
                    return salesInvoiceHeader;
                }
            }

            database.close();

            return salesInvoiceHeader;
        }

        [WebMethod]
        public SalesInvoiceHeader getSalesInvoiceHeaderByOcr(string ocrNo)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);

            SalesInvoiceHeader salesInvoiceHeader = null;

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);
                salesInvoiceHeader = SalesInvoiceHeader.getOcrEntry(database, ocrNo);
                if (salesInvoiceHeader != null)
                {
                    database.close();
                    return salesInvoiceHeader;
                }
            }

            database.close();

            return salesInvoiceHeader;
        }

        [WebMethod]
        public SalesInvoiceHeaderCollection getSalesInvoiceHeaders(string billToCustomerNo, DateTime fromDate, DateTime toDate, int page, int itemsPerPage)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);
            SalesInvoiceHeaderCollection salesInvoiceHeaderCollection = null;

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);
                salesInvoiceHeaderCollection = SalesInvoiceHeader.getEntries(database, billToCustomerNo, fromDate, toDate);
                if (salesInvoiceHeaderCollection.Count > 0)
                {
                    database.close();
                    salesInvoiceHeaderCollection.setPaging(page, itemsPerPage);
                    return salesInvoiceHeaderCollection;
                }
            }
            database.close();

            salesInvoiceHeaderCollection.setPaging(page, itemsPerPage);
            return salesInvoiceHeaderCollection;
        }

        [WebMethod]
        public CustomerLedgerEntryCollection getCustomerLedgerEntries(string customerNo, DateTime fromDate, DateTime toDate, int page, int itemsPerPage)
        {
            
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);
            CustomerLedgerEntryCollection customerLedgerEntryCollection = null;

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);
                customerLedgerEntryCollection = CustomerLedgerEntry.getCustomerEntries(database, customerNo, fromDate, toDate);
                if (customerLedgerEntryCollection.Count > 0)
                {
                    database.close();
                    customerLedgerEntryCollection.setPaging(page, itemsPerPage);
                    return customerLedgerEntryCollection;
                }
            }
            database.close();
            customerLedgerEntryCollection.setPaging(page, itemsPerPage);

            return customerLedgerEntryCollection;
            
            
        }

        [WebMethod]
        public CustomerLedgerEntryCollection getContractCustomerLedgerEntries(string contractNo, DateTime fromDate, DateTime toDate, int page, int itemsPerPage)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);
            CustomerLedgerEntryCollection customerLedgerEntryCollection = null;

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);
                customerLedgerEntryCollection = CustomerLedgerEntry.getContractEntries(database, contractNo, fromDate, toDate);
                if (customerLedgerEntryCollection.Count > 0)
                {
                    database.close();
                    customerLedgerEntryCollection.setPaging(page, itemsPerPage);
                    return customerLedgerEntryCollection;
                }
            }
            database.close();
            customerLedgerEntryCollection.setPaging(page, itemsPerPage);

            return customerLedgerEntryCollection;
        }

        [WebMethod]
        public CustomerLedgerEntryCollection getCustomerLedgerEntriesByDate(DateTime fromDate, DateTime toDate, int page, int itemsPerPage)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);
            CustomerLedgerEntryCollection customerLedgerEntryCollection = null;

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);
                customerLedgerEntryCollection = CustomerLedgerEntry.getEntriesByDate(database, fromDate, toDate);
                if (customerLedgerEntryCollection.Count > 0)
                {
                    database.close();
                    customerLedgerEntryCollection.setPaging(page, itemsPerPage);
                    return customerLedgerEntryCollection;
                }
            }
            database.close();
            customerLedgerEntryCollection.setPaging(page, itemsPerPage);

            return customerLedgerEntryCollection;
        }

        [WebMethod]
        public CustomerLedgerEntryCollection getDueCustomerLedgerEntries(DateTime fromDate, DateTime toDate, int page, int itemsPerPage)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);
            CustomerLedgerEntryCollection customerLedgerEntryCollection = null;

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);
                customerLedgerEntryCollection = CustomerLedgerEntry.getDueEntries(database, fromDate, toDate);
                if (customerLedgerEntryCollection.Count > 0)
                {
                    database.close();
                    customerLedgerEntryCollection.setPaging(page, itemsPerPage);
                    return customerLedgerEntryCollection;
                }
            }
            database.close();
            customerLedgerEntryCollection.setPaging(page, itemsPerPage);

            return customerLedgerEntryCollection;
        }

        [WebMethod]
        public string getInvoicePdfAsBase64(string invoiceNo)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);

                string base64 = PDFDocumentLog.getBase64Document(database, 1, invoiceNo);
                if (base64 != "") return base64;
            }

            return "";
        }

        [WebMethod]
        public string getCreditMemoPdfAsBase64(string creditMemoNo)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(null, configuration);

            foreach (string companyName in configuration.getCompanyNameArray())
            {
                database.setCompanyName(companyName);

                string base64 = PDFDocumentLog.getBase64Document(database, 2, creditMemoNo);
                if (base64 != "") return base64;
            }

            return "";
        }

    

        [WebMethod]
        public void reSendInvoice(string invoiceNo)
        {
            Configuration configuration = new Configuration();
            configuration.init();

            NavWebService.contractParkerWebService ws = new Navipro.Apcoa.ContractParker.WebService.NavWebService.contractParkerWebService();
            ws.Url = configuration.wsAddress;

            string base64String = "";
            ws.ResendInvoice(invoiceNo); ;

        }

    }
}
