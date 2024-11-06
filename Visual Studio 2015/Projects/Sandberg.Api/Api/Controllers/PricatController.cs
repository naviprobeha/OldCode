using Api.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/pricats")]
    public class PricatController : ApiController
    {
        // GET api/items
        [HeaderAuthorization]
        public IHttpActionResult Get()
        {
            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            PricatHelper pricatHelper = new PricatHelper();
            byte[] byteArray = pricatHelper.GetPricatFile(customerNo);
            if (byteArray != null)
            {
                MemoryStream stream = new MemoryStream(byteArray);
                return new CSVFileResult(stream, this.Request, "pricat.csv");
            }

            return ResponseMessage(Request.CreateResponse(System.Net.HttpStatusCode.OK, ""));
        }

    }

    public class CSVFileResult : IHttpActionResult
    {
        MemoryStream bookStuff;
        string CsvFileName;
        HttpRequestMessage httpRequestMessage;
        HttpResponseMessage httpResponseMessage;
        public CSVFileResult(MemoryStream data, HttpRequestMessage request, string filename)
        {
            bookStuff = data;
            httpRequestMessage = request;
            CsvFileName = filename;
        }
        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            httpResponseMessage = httpRequestMessage.CreateResponse(System.Net.HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(bookStuff);
            //httpResponseMessage.Content = new ByteArrayContent(bookStuff.ToArray());  
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = CsvFileName;
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/CSV");

            return System.Threading.Tasks.Task.FromResult(httpResponseMessage);
        }
    }
}