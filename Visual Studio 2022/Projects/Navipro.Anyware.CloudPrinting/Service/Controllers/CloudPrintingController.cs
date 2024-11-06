using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Navipro.Anyware.CloudPrinting.Service.Data;
using Navipro.Anyware.CloudPrinting.Service.Models;
using Newtonsoft.Json.Linq;

namespace Navipro.Anyware.CloudPrinting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudPrintingController : ControllerBase
    {
        private DatabaseContext databaseContext;

        public CloudPrintingController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post()
        {
 

            StreamReader streamReader = new StreamReader(Request.Body);
            string jsonContent = streamReader.ReadToEnd();

            JObject jsonObject = JObject.Parse(jsonContent);

            PrintQueueEntry printQueueEntry = new PrintQueueEntry();
            printQueueEntry.id = Guid.NewGuid().ToString();
            printQueueEntry.printerName = jsonObject.GetValue("printerName").ToString();
            printQueueEntry.serviceId = jsonObject.GetValue("serviceId").ToString();
            printQueueEntry.uncPrinterPath = jsonObject.GetValue("uncPrinterPath").ToString();
            printQueueEntry.base64Document = jsonObject.GetValue("base64").ToString();

            databaseContext.PrintQueueEntries.Add(printQueueEntry);
            databaseContext.SaveChanges();


            return new JsonResult("OK");

        }



        [HttpGet("{id}")]
        public JsonResult Get(string id, bool deleteFirst = false)
        {
            PrintQueueEntry printQueueEntry = null;

            if (deleteFirst)
            {
                printQueueEntry = databaseContext.PrintQueueEntries.FirstOrDefault(p => p.serviceId == id);
                databaseContext.Entry(printQueueEntry).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                databaseContext.SaveChanges();
            }

            printQueueEntry = databaseContext.PrintQueueEntries.FirstOrDefault(p => p.serviceId == id);
            return new JsonResult(printQueueEntry);

        }
    }
}
