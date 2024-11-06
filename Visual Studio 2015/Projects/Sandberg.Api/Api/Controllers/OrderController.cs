using Api.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Xml;
using Api.Models;

namespace Api.Controllers
{
    
    public class OrderController : ApiController
    {
        // GET api/order
        [HeaderAuthorization]
        public List<OrderHistory> Get(DateTime? fromDate = null, DateTime? toDate = null, string no = "", int offset = 0, int count = 0, string customerOrderNo = "", string marking = "")
        {
            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            List<OrderHistory> orderHistoryList = OrderHistoryHelper.GetOrderHistory(customerNo, no, fromDate.GetValueOrDefault(DateTime.MinValue), toDate.GetValueOrDefault(DateTime.MinValue), offset, count, customerOrderNo, marking);
            return orderHistoryList;
        }

        
        // GET api/customers/5
        [HeaderAuthorization]
        public OrderHistory Get(string id)
        {
            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            return OrderHistoryHelper.GetOrder(customerNo, id);
        }
        


        [HeaderAuthorization]
        // Post: Order
        [HttpPost]
        public OrderHistory Post()
        {
            string customerNo = "";
            if (Request.Headers.Contains("CustomerNo"))
            {
                customerNo = Request.Headers.GetValues("CustomerNo").First();
            }

            

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;            

            OrderHistory order = JsonConvert.DeserializeObject<OrderHistory>(jsonContent);
            if (order == null) throw new Exception("401: Illegal payload: " + jsonContent);

            if ((order.action == null) || (order.action == "") || (order.action.ToUpper() == "VALIDATE"))
            {
                int i = 0;
                while(i < order.lines.Count())
                {
                    OrderHistoryLine line = order.lines[i];

                    if (line.type == 2)
                    {
                        bool batchFound = false;
                        bool requireBatch = true;
                        Item item = ItemHelper.GetItem(customerNo, line.no);
                        if (item.batches == null) item.batches = new List<ItemBatch>();

                        if ((item == null) || (item.no == ""))
                        {
                            line.error_message = "461: Item " + line.no + " not accepted for customer " + customerNo + ".";
                        }
                        else
                        {
                            item.stock_item_only = item.stock_item_only;
                            
                            if (item.available_stock < line.quantity)
                            {
                                line.error_message = "464: Available stock not sufficient for " + line.no + ".";
                            }
                            if (((item.batches.Count == 0) || ((item.batches.Count == 1) && (item.batches[0].no == ""))) && ((line.batch_no == "") || (line.batch_no == null))) requireBatch = false;

                            if ((requireBatch) && (line.error_message == ""))
                            {
                                if (item.batches.Count == 0)
                                {
                                    line.error_message = "462: Illegal batch no (" + line.batch_no + ") on item " + line.no + ".";
                                }

                                foreach (ItemBatch batch in item.batches)
                                {
                                    if (batch.no == line.batch_no)
                                    {
                                        batchFound = true;
                                        if (batch.stock_level < line.quantity)
                                        {
                                            line.error_message = "463: Stock level not sufficient on item " + line.no + ".";
                                        }
                                        line.available_stock = batch.stock_level;
                                    }
                                    if (line.batch_no.ToUpper() == "AUTO")
                                    {
                                        if (batch.stock_level >= line.quantity) batchFound = true;
                                        line.available_stock = item.available_stock;
                                    }
                                }
                                if (!batchFound)
                                {
                                    line.error_message = "462: Illegal batch no (" + line.batch_no + ") on item " + line.no + ".";
                                }
                            }
                        }
                        
                    }
                    order.lines[i] = line;
                    i++;
                }

                OrderHistoryLine orderHistoryLine = order.lines.FirstOrDefault(l => l.error_message != "");
                if (orderHistoryLine != null)
                {
                    order.error_message = orderHistoryLine.error_message;
                    return order;
                }
            }

            if (order.action.ToUpper() == "VALIDATE")
            {
                return order;
            }

            if (order.error_message == "")
            {
                XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonContent, "order");

                OrderHelper.SubmitOrder(customerNo, order.marking, doc);
            }

            return order;
        }
    }
}