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
    public class SubscriptionController : ControllerBase
    {

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            SubscriptionHelper subscriptionHelper = new SubscriptionHelper();

            dynamic subscriptionArr = Newtonsoft.Json.JsonConvert.DeserializeObject(subscriptionHelper.getSubscriptions(id));

            int a = 0;
            while (a < subscriptionArr.Count)
            {
                dynamic subscriptionObj = subscriptionArr[a];


                int i = 0;
                while (i < subscriptionObj.list.Count)
                {
                    dynamic subscObj = subscriptionObj.list[i];

                    int j = 0;
                    while (j < subscObj.items.data.Count)
                    {
                        dynamic itemObj = subscObj.items.data[j];

                        int k = 0;
                        while (k < itemObj.plan.tiers.Count)
                        {
                            dynamic tierObj = itemObj.plan.tiers[k];
                            if (tierObj.up_to == null) tierObj.up_to = 99999;
                            itemObj.plan.tiers[k] = tierObj;

                            k++;
                        }
                        j++;
                    }

                    int k2 = 0;
                    while (k2 < subscObj.plan.tiers.Count)
                    {
                        dynamic tierObj = subscObj.plan.tiers[k2];
                        if (tierObj.up_to == null) tierObj.up_to = 99999;
                        subscObj.plan.tiers[k2] = tierObj;

                        k2++;
                    }

                    i++;
                }
                a++;
            }
            

            return new JsonResult(subscriptionArr);
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post(string customerId, string planId, string companyName, string countryCode)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            SubscriptionHelper subscriptionHelper = new SubscriptionHelper();
            var subscriptionObj = Newtonsoft.Json.JsonConvert.DeserializeObject(subscriptionHelper.createSubscription(customerId, planId, companyName, countryCode));

            return new JsonResult(subscriptionObj);
        }

        // POST api/values
        [HttpPost("{id}")]
        public JsonResult Post(string id, bool cancel)
        {
            if (Request.Headers["Authorization"] != StripeHelper.localToken)
            {
                return new JsonResult("");
            }

            if (cancel)
            {
                SubscriptionHelper subscriptionHelper = new SubscriptionHelper();

                var subscriptionObj = Newtonsoft.Json.JsonConvert.DeserializeObject(subscriptionHelper.cancelSubscription(id));

                return new JsonResult(subscriptionObj);
            }
            return new JsonResult("Aborted");

        }


    }
}
