using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klarna.Rest;
using Klarna.Rest.Models;
using Klarna.Rest.Transport;
using Klarna.Rest.OrderManagement;

namespace Navipro.Klarna.Checkout
{
    public class KlarnaCheckoutHelper
    {
        private List<OrderLine> orderLineList;
        private bool testMode;
        private string captureId;

        public KlarnaCheckoutHelper()
        {
        }

        public void addArticle(int quantity, string itemNo, string description, double unitPriceInclVat, double vat, double discount)
        {
            if (orderLineList == null) orderLineList = new List<OrderLine>();
            OrderLine orderLine = new OrderLine();
            orderLine.Quantity = quantity;
            orderLine.Reference = itemNo;
            orderLine.Name = description;
            orderLine.UnitPrice = (long)(unitPriceInclVat*100);
            orderLine.TaxRate = (int)(vat*100);
            orderLine.TotalDiscountAmount = (long)discount*100;
            orderLine.TotalAmount = (long)(((unitPriceInclVat * quantity) - discount)*100);

            orderLineList.Add(orderLine);

        }


        public void setTestMode(bool testMode)
        {
            this.testMode = testMode;
        }

        public bool activateReservation(string merchantId, string secret, string orderNo)
        {

            Uri url = Client.EuBaseUrl;
            if (testMode) url = Client.EuTestBaseUrl;

            IConnector connector = ConnectorFactory.Create(merchantId, secret, url);

            Client client = new Client(connector);

            IOrder order = client.NewOrder(orderNo);
            ICapture capture = client.NewCapture(order.Location);

            int totalAmount = 0;
            foreach(OrderLine orderLine in orderLineList)
            {
                totalAmount = totalAmount + (int)orderLine.TotalAmount;
            }

            CaptureData captureData = new CaptureData()
            {
                CapturedAmount = totalAmount, 
                Description = "Shipped part of the order",
                OrderLines = orderLineList,                               
            };

            capture.Create(captureData);

            if (captureData.CaptureId == "") return false;
            captureId = captureData.CaptureId;
            return true;
        }

        public string getCaptureId()
        {
            return captureId;
        }
    }
}
