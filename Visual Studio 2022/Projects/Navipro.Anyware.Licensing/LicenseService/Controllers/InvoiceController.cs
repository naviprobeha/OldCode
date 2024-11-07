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
    public class InvoiceController : ControllerBase
    {

        // GET api/values
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            InvoiceHelper invoiceHelper = new InvoiceHelper();

            var invoiceObj = Newtonsoft.Json.JsonConvert.DeserializeObject(invoiceHelper.getInvoices(id));

            return new JsonResult(invoiceObj);
        }



    }
}
