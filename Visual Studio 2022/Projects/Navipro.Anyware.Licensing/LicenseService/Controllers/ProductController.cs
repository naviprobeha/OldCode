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
    public class ProductController : ControllerBase
    {

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            ProductHelper productHelper = new ProductHelper();

            var productObj = Newtonsoft.Json.JsonConvert.DeserializeObject(productHelper.getProducts());
            return new JsonResult(productObj);
        }



    }
}
