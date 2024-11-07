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
    public class ProductPlanController : ControllerBase
    {

        // GET api/values
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            string productName = id;

            ProductHelper productHelper = new ProductHelper();
            dynamic productPlanObj = Newtonsoft.Json.JsonConvert.DeserializeObject(productHelper.getProductPlans(productName));

            int i = 0;
            while (i < productPlanObj.data.Count)
            {
                dynamic planObj = productPlanObj.data[i];

                int j = 0;
                while (j < planObj.tiers.Count)
                {
                    dynamic tierObj = planObj.tiers[j];
                    if (tierObj.up_to == null) tierObj.up_to = 99999;
                    planObj.tiers[j] = tierObj;

                    j++;
                }

                i++;
            }


            return new JsonResult(productPlanObj);
        }



    }
}
