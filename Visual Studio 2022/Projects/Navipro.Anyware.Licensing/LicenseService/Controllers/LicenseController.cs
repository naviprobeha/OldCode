using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LicenseService.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LicenseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {

        // GET api/values/5
        [HttpGet]
        public JsonResult Get(string customerId, string productName)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            CustomerHelper customerHelper = new CustomerHelper();
            ProductHelper productHelper = new ProductHelper();

            string productId = "";
            string subscriptionStatus = "FAILED";

            dynamic customerObject = JObject.Parse(customerHelper.getCustomer(customerId));
            dynamic productsObject = JObject.Parse(productHelper.getProducts());

            foreach(dynamic product in productsObject.data)
            {
                if (((string)product.name).ToUpper() == productName.ToUpper()) productId = product.id;
            }

            if (productId != "")
            {

                if (customerObject.subscriptions != null)
                {
                    foreach (dynamic subscription in customerObject.subscriptions.data)
                    {
                        if (subscription.items != null)
                        {
                            foreach (dynamic subItem in subscription.items.data)
                            {
                                if (subItem.plan.product == productId)
                                {
                                    if (((string)subscription.status).ToUpper() == "ACTIVE") subscriptionStatus = "SUCCESS";
                                }

                            }
                        }
                    }
                }
            }

            var statusObject = new
            {
                subscriptionStatus = subscriptionStatus,
                customer = customerObject
            };

            return new JsonResult(statusObject);
        }

        // POST api/values
        [HttpPost("{id}")]
        public JsonResult Post(string id, int users = 0)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            CustomerHelper customerHelper = new CustomerHelper();
            customerHelper.pushUsage(id, users);

            return new JsonResult("OK");
        }
    }
}
