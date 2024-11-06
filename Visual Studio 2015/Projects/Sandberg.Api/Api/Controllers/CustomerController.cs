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
    [RoutePrefix("api/customers")]
    public class CustomerController : ApiController
    {
        // GET api/customers
        [HeaderAuthorization]
        public List<Customer> Get(int offset = 0, int count = 100)
        {
            List<Customer> customerList = CustomerHelper.GetCustomers(offset, count);
            return customerList;
        }

        // GET api/customers/5
        [HeaderAuthorization]
        public Customer Get(string id)
        {
            return CustomerHelper.GetCustomer(id);
        }

        // POST api/customers
        [HeaderAuthorization]
        public void Post(Customer customer)
        {
            if (!CustomerHelper.SubmitCustomer(customer))
            {
                throw new Exception("Customer already applied.");
            }
        }

    }
}
