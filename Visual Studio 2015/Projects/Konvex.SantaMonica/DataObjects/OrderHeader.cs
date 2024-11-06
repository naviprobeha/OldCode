using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Konvex.SmartShipping.DataObjects
{
    public class OrderHeader
    {
        private string _organizationNo;
        private int _entryNo;
        private DateTime _shipDate;
        private string _customerNo;
        private string _customerName;
        private string _address;
        private string _address2;
        private string _postCode;
        private string _city;
        private string _countryCode;
        private string _phoneNo;
        private string _cellPhoneNo;
        private string _details;
        private string _comments;
        private int _priority;
        private string _billToCustomerNo;
        private int _paymentType;
        private string _customerShipAddressNo;
        private string _shipName;
        private string _shipAddress;
        private string _shipAddress2;
        private string _shipPostCode;
        private string _shipCity;
        private string _directionComment;
        private string _directionComment2;
        private int _positionX;
        private int _positionY;
        private string _agentCode;
        private int _status;
        private DateTime _closedDate;
        private DateTime _shipTime;
        private DateTime _creationDate;
        private int _createdBy;
        private string _productionSite;

        private OrderLineCollection _orderLineCollection;

        public OrderHeader()
        {
            _orderLineCollection = new OrderLineCollection();
        }

        public OrderHeader(Navipro.SantaMonica.Common.ShipOrder shipOrder)
        {
            this.organizationNo = shipOrder.organizationNo;
            this.entryNo = shipOrder.entryNo;
            this.shipDate = shipOrder.shipDate;
            this.customerNo = shipOrder.customerNo;
            this.customerName = shipOrder.customerName;
            this.address = shipOrder.address;
            this.address2 = shipOrder.address2;
            this.postCode = shipOrder.postCode;
            this.city = shipOrder.city;
            this.phoneNo = shipOrder.phoneNo;
            this.cellPhoneNo = shipOrder.cellPhoneNo;
            this.details = shipOrder.details;
            this.comments = shipOrder.comments;
            this.priority = shipOrder.priority;
            this.status = shipOrder.status;
            this.positionX = shipOrder.positionX;
            this.positionY = shipOrder.positionY;
            this.billToCustomerNo = shipOrder.billToCustomerNo;
            this.customerShipAddressNo = shipOrder.customerShipAddressNo;
            this.shipName = shipOrder.shipName;
            this.shipAddress = shipOrder.shipAddress;
            this.shipAddress2 = shipOrder.shipAddress2;
            this.shipPostCode = shipOrder.shipPostCode;
            this.shipCity = shipOrder.shipCity;
            this.directionComment = shipOrder.directionComment;
            this.directionComment2 = shipOrder.directionComment2;
            this.paymentType = shipOrder.paymentType;
            this.creationDate = shipOrder.creationDate;
            this.productionSite = shipOrder.productionSite;

            _orderLineCollection = new OrderLineCollection();

        }

        public void applyLines(Navipro.SantaMonica.Common.Database database)
        {
            Navipro.SantaMonica.Common.ShipOrderLines shipOrderLines = new Navipro.SantaMonica.Common.ShipOrderLines();
            DataSet shipOrderLineDataSet = shipOrderLines.getDataSet(database, this.entryNo);

            
            int i = 0;
            while (i < shipOrderLineDataSet.Tables[0].Rows.Count)
            {
                Navipro.SantaMonica.Common.ShipOrderLine shipOrderLine = new Navipro.SantaMonica.Common.ShipOrderLine(shipOrderLineDataSet.Tables[0].Rows[i]);
                OrderLine orderLine = new OrderLine(shipOrderLine);

                
                Navipro.SantaMonica.Common.ShipOrderLineIds shipOrderLineIds = new Navipro.SantaMonica.Common.ShipOrderLineIds();
                DataSet shipOrderLineIdDataSet = shipOrderLineIds.getDataSet(database, entryNo, orderLine.entryNo);
                int j = 0;
                while (j < shipOrderLineIdDataSet.Tables[0].Rows.Count)
                {
                    OrderLineID orderLineID = new OrderLineID(shipOrderLineIdDataSet.Tables[0].Rows[j]);
                    orderLine.orderLineIdCollection.Add(orderLineID);

                    j++;
                }
                 

                _orderLineCollection.Add(orderLine);

                i++;
            }
            
        }

        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public DateTime shipDate { get { return _shipDate; } set { _shipDate = value; } }
        public string customerNo { get { return _customerNo; } set { _customerNo = value; } }
        public string customerName { get { return _customerName; } set { _customerName = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string address2 { get { return _address2; } set { _address2 = value; } }
        public string postCode { get { return _postCode; } set { _postCode = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }
        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string cellPhoneNo { get { return _cellPhoneNo; } set { _cellPhoneNo = value; } }
        public string details { get { return _details; } set { _details = value; } }
        public string comments { get { return _comments; } set { _comments = value; } }
        public int priority { get { return _priority; } set { _priority = value; } }
        public string billToCustomerNo { get { return _billToCustomerNo; } set { _billToCustomerNo = value; } }
        public int paymentType { get { return _paymentType; } set { _paymentType = value; } }
        public string customerShipAddressNo { get { return _customerShipAddressNo; } set { _customerShipAddressNo = value; } }
        public string shipName { get { return _shipName; } set { _shipName = value; } }
        public string shipAddress { get { return _shipAddress; } set { _shipAddress = value; } }
        public string shipAddress2 { get { return _shipAddress2; } set { _shipAddress2 = value; } }
        public string shipPostCode { get { return _shipPostCode; } set { _shipPostCode = value; } }
        public string shipCity { get { return _shipCity; } set { _shipCity = value; } }
        public string directionComment { get { return _directionComment; } set { _directionComment = value; } }
        public string directionComment2 { get { return _directionComment2; } set { _directionComment2 = value; } }
        public int positionX { get { return _positionX; } set { _positionX = value; } }
        public int positionY { get { return _positionY; } set { _positionY = value; } }
        public string agentCode { get { return _agentCode; } set { _agentCode = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public DateTime closedDate { get { return _closedDate; } set { _closedDate = value; } }
        public DateTime shipTime { get { return _shipTime; } set { _shipTime = value; } }
        public DateTime creationDate { get { return _creationDate; } set { _creationDate = value; } }
        public int createdBy { get { return _createdBy; } set { _createdBy = value; } }
        public string productionSite { get { return _productionSite; } set { _productionSite = value; } }

        public OrderLineCollection orderLineCollection { get { return _orderLineCollection; } set { _orderLineCollection = value; } }

        public JObject toJsonObject()
        {

            JObject jObject = new JObject();

            if (shipDate == null) shipDate = DateTime.Today;
            if (billToCustomerNo == null) billToCustomerNo = "";

            if (organizationNo == "SSAB") organizationNo = "JO";

            jObject.Add("CompanyCode", organizationNo);
            jObject.Add("ShipMentDate", shipDate.ToString("yyyy-MM-dd"));
            jObject.Add("CustomerNo", customerNo);
            jObject.Add("BilltoCustomerNo", billToCustomerNo);
            jObject.Add("CustomerName", customerName);
            jObject.Add("Address", address);
            jObject.Add("Address2", address2);
            jObject.Add("PostCode", postCode);
            jObject.Add("City", city);
            jObject.Add("CountryCode", countryCode);
            jObject.Add("PhoneNo", phoneNo);
            jObject.Add("CellPhoneNo", cellPhoneNo);
            jObject.Add("Comments", comments);
            jObject.Add("Priority", priority);


            if (paymentType == 0) jObject.Add("PaymentTypeCode", "FAKTURA");
            if (paymentType == 1) jObject.Add("PaymentTypeCode", "KONTANT");
            if (paymentType == 2) jObject.Add("PaymentTypeCode", "KORT");
            if (paymentType == 3) jObject.Add("PaymentTypeCode", "INTERNET");

            if (customerShipAddressNo == null) customerShipAddressNo = "0";
            if (customerShipAddressNo == "NEW") customerShipAddressNo = "-1";
            if (customerShipAddressNo == "") customerShipAddressNo = "0";

            jObject.Add("CustomerShipAddressNo", customerShipAddressNo);

            jObject.Add("ShipName", shipName);
            jObject.Add("ShipAddress", shipAddress);
            jObject.Add("ShipAddress2", shipAddress2);
            jObject.Add("ShipPostCode", shipPostCode);
            jObject.Add("ShipCity", shipCity);
            jObject.Add("AgentCode", agentCode);
            jObject.Add("FacilityNo", productionSite);
            jObject.Add("PositionX", positionX);
            jObject.Add("PositionY", positionY);

            if (directionComment == null) directionComment = "";
            if (directionComment2 == null) directionComment2 = "";

            jObject.Add("Directions", directionComment + " " + directionComment2);
            jObject.Add("CreatedBy", createdBy);


            JArray linesArray = new JArray();

            foreach (OrderLine line in orderLineCollection)
            {
                linesArray.Add(line.toJsonObject());
            }

            jObject.Add("lines", linesArray);



            return jObject;
        }
    }
}
