using Api.Library;
using Api.Library.ShippingAgents;
using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/freight")]
    public class FreightController : ApiController
    {
        // GET api/customers
        [HeaderAuthorization]
        public List<ShippingAgentService> Get()
        {
            string output = "xml";
            try
            {
                output = Request.Headers.GetValues("Response").First();
            }
            catch (Exception) { }

            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;

            OrderHistory order = JsonConvert.DeserializeObject<OrderHistory>(jsonContent);
            if (order == null) throw new Exception("401: Illegal payload: " + jsonContent);

            if (order.shipping_agent_code == "UPS")
            {
                UPSHelper upsHelper = new UPSHelper();
                if (output == "json") return upsHelper.freightRequestJson(order);
                return upsHelper.freightRequest(order);
            }

            List<ShippingAgentService> emptyList = new List<ShippingAgentService>();
            return emptyList;

        }



    }
}
