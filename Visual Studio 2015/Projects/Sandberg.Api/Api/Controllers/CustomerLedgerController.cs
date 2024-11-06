using Api.Library;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/customerLedger")]
    public class CustomerLedgerController : ApiController
    {
        // GET api/customerLedger
        [HeaderAuthorization]
        public List<CustomerLedgerEntry> Get(string id)
        {
            string customerNo = id;
            Customer customer = CustomerHelper.GetCustomer(customerNo);
            if (customer == null) return new List<CustomerLedgerEntry>();
            if (customer.bill_to_customer_no != "") customerNo = customer.bill_to_customer_no;

            List<CustomerLedgerEntry> customerLedgerEntryList = CustLedgerHelper.GetLedgerHistory(customerNo);
            return customerLedgerEntryList;
        }

 

    }
}
