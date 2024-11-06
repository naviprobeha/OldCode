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
    [RoutePrefix("api/shippingagentservice")]
    public class ShippingAgentServiceController : ApiController
    {
        // GET api/customers
        [HeaderAuthorization]
        public List<ShippingAgentService> Get()
        {
            List<ShippingAgentService> shippingAgentServiceList = ShippingAgentHelper.GetServices();
            return shippingAgentServiceList;
        }



    }
}
