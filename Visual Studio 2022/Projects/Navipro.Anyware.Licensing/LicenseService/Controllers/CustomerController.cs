using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LicenseService.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LicenseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            CustomerHelper customerHelper = new CustomerHelper();


            var customerObj = Newtonsoft.Json.JsonConvert.DeserializeObject(customerHelper.getCustomer(id));

            return new JsonResult(customerObj);

        }

        // GET api/values/5
        [HttpGet]
        public JsonResult Get(string id = "", string email = "")
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            CustomerHelper customerHelper = new CustomerHelper();

            var customerObj = Newtonsoft.Json.JsonConvert.DeserializeObject(customerHelper.findCustomerByEmail(email));
            return new JsonResult(customerObj);
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post()
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            StreamReader streamReader = new StreamReader(Request.Body);
            string jsonContent = streamReader.ReadToEnd();


            CustomerHelper customerHelper = new CustomerHelper();

            var customerObj = Newtonsoft.Json.JsonConvert.DeserializeObject(customerHelper.createCustomer(jsonContent));

            return new JsonResult(customerObj);

        }

        // POST api/values
        [HttpPost("{id}")]
        public JsonResult Post(string id, int users)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            StreamReader streamReader = new StreamReader(Request.Body);
            string jsonContent = streamReader.ReadToEnd();


            CustomerHelper customerHelper = new CustomerHelper();

            var customerObj = Newtonsoft.Json.JsonConvert.DeserializeObject(customerHelper.updateCustomer(id, jsonContent));

            try
            {
                if (users != 0)
                {
                    customerHelper.pushUsage(id, users);
                }
            }
            catch (Exception)
            { }

            return new JsonResult(customerObj);

        }

 


    }
}
