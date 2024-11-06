using Api.Library;
using Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/invoices")]
    public class InvoiceController : ApiController
    {
        // GET api/invoices
        [HeaderAuthorization]
        public List<InvoiceHistory> Get(DateTime? fromDate = null, DateTime? toDate = null, int offset = 0, int count = 0, string customerOrderNo = "")
        {
            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            return InvoiceHistoryHelper.GetInvoiceHistory(customerNo, fromDate.GetValueOrDefault(DateTime.MinValue), toDate.GetValueOrDefault(DateTime.MinValue), offset, count, customerOrderNo);
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            InvoiceHistory invoiceHistory = InvoiceHistoryHelper.GetInvoiceHistory(customerNo, id);
            if (invoiceHistory == null) throw new Exception("471: Invalid invoice no: " + id);

            string fileName = "Invoice " + id + ".pdf";
         
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(System.IO.File.ReadAllBytes(@"\\diskstation\gemensam\11 Arkiv\00 Fakturor\"+fileName))
            };
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                    FileName = fileName
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
    }
}
