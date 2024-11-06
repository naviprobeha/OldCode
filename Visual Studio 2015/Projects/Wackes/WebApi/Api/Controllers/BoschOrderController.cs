using Api.Library;
using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;

[RoutePrefix("api/boschorder")]
public class BoschOrderController : ApiController
{
    // POST api/items
    [HeaderAuthorization]
    public async Task<bool> Post()
    {
        string json = await Request.Content.ReadAsStringAsync();

        XmlDocument xmlDoc  = JsonConvert.DeserializeXmlNode(json, "OrderRoot");

        OrderHelper.SubmitOrder2("BOSCH", xmlDoc);

        return true;
    }
}