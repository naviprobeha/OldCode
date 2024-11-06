using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class ShipmentHeader
    {
		private string _organizationNo;
        private string _no;
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
        private string _productionSite;
        private int _payment;
        private string _paymentText;
        private int _status;
        private string _dairyCode;
        private string _dairyNo;
        private string _reference;
        private string _containerNo;
        private string _userName;
        private int _shipOrderEntryNo;
        private int _lineOrderEntryNo;
        private string _agentCode;
        private int _positionX;
        private int _positionY;
        private string _customerShipAddressNo;
        private string _shipName;
        private string _shipAddress;
        private string _shipAddress2;
        private string _shipPostCode;
        private string _shipCity;
        private string _invoiceNo;
        private bool _surfaceNotCorrect;

        private ShipmentLineCollection _shipmentLineCollection;

        public ShipmentHeader()
        {
            _shipmentLineCollection = new ShipmentLineCollection();
        }


        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
        public string no { get { return _no; } set { _no = value; } }
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
        public string productionSite { get { return _productionSite; } set { _productionSite = value; } }
        public int payment { get { return _payment; } set { _payment = value; } }
        public string paymentText { get { return _paymentText; } set { _paymentText = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public string dairyCode { get { return _dairyCode; } set { _dairyCode = value; } }
        public string dairyNo { get { return _dairyNo; } set { _dairyNo = value; } }
        public string reference { get { return _reference; } set { _reference = value; } }
        public string containerNo { get { return _containerNo; } set { _containerNo = value; } }
        public string userName { get { return _userName; } set { _userName = value; } }
        public int shipOrderEntryNo { get { return _shipOrderEntryNo; } set { _shipOrderEntryNo = value; } }
        public int lineOrderEntryNo { get { return _lineOrderEntryNo; } set { _lineOrderEntryNo = value; } }
        public string agentCode { get { return _agentCode; } set { _agentCode = value; } }
        public int positionX { get { return _positionX; } set { _positionX = value; } }
        public int positionY { get { return _positionY; } set { _positionY = value; } }
        public string customerShipAddressNo { get { return _customerShipAddressNo; } set { _customerShipAddressNo = value; } }
        public string shipName { get { return _shipName; } set { _shipName = value; } }
        public string shipAddress { get { return _shipAddress; } set { _shipAddress = value; } }
        public string shipAddress2 { get { return _shipAddress2; } set { _shipAddress2 = value; } }
        public string shipPostCode { get { return _shipPostCode; } set { _shipPostCode = value; } }
        public string shipCity { get { return _shipCity; } set { _shipCity = value; } }
        public string invoiceNo { get { return _invoiceNo; } set { _invoiceNo = value; } }
        public bool surfaceNotCorrect { get { return _surfaceNotCorrect; } set { _surfaceNotCorrect = value; } }
        public ShipmentLineCollection shipmentLineCollection { get { return _shipmentLineCollection; } set { _shipmentLineCollection = value; } }

        public JObject toJsonObject()
        {
            JObject jObject = new JObject();

            if (customerShipAddressNo == "NEW") customerShipAddressNo = "-1";
            if (organizationNo == "SSAB") organizationNo = "JO";
            if (organizationNo == "DICKS") organizationNo = "DICK";

            jObject.Add("No", no);
            jObject.Add("CompanyCode", organizationNo);
            jObject.Add("ShipmentDate", shipDate.ToString("yyyy-MM-dd"));
            jObject.Add("CustomerNo", customerNo);
            jObject.Add("CustomerName", customerName);
            jObject.Add("Address", address);
            jObject.Add("Address2", address2);
            jObject.Add("PostCode", postCode);
            jObject.Add("City", city);
            jObject.Add("CountryCode", countryCode);
            jObject.Add("PhoneNo", phoneNo);
            jObject.Add("CellPhoneNo", cellPhoneNo);
            jObject.Add("UserName", userName);

            if (payment == 0) jObject.Add("PaymentTypeCode", "FAKTURA");
            if (payment == 1) jObject.Add("PaymentTypeCode", "KONTANT");
            if (payment == 2) jObject.Add("PaymentTypeCode", "KORT");
            if (payment == 3) jObject.Add("PaymentTypeCode", "INTERNET");

            string custShipAddressNo = customerShipAddressNo.ToString();
            if (custShipAddressNo == "") custShipAddressNo = "0";
            if (custShipAddressNo == null) custShipAddressNo = "0";
            jObject.Add("CustomerShipAddressNo", custShipAddressNo);

            jObject.Add("ShipName", shipName);
            jObject.Add("ShipAddress", shipAddress);
            jObject.Add("ShipAddress2", shipAddress2);
            jObject.Add("ShipPostCode", shipPostCode);
            jObject.Add("ShipCity", shipCity);
            jObject.Add("AgentCode", agentCode);
            jObject.Add("FacilityNo", productionSite);
            jObject.Add("PositionX", positionX);
            jObject.Add("PositionY", positionY);
            jObject.Add("Reference", reference);

            if (surfaceNotCorrect)
            {
                jObject.Add("IncorrectSurface", 1);
            }
            else
            {
                jObject.Add("IncorrectSurface", 0);
            }


            jObject.Add("ContainerNo", containerNo);
            jObject.Add("ShipOrderEntryNo", shipOrderEntryNo.ToString());
            jObject.Add("InvoiceNo", invoiceNo);

            JArray linesArray = new JArray();

            foreach(ShipmentLine line in shipmentLineCollection)
            {
                linesArray.Add(line.toJsonObject());
            }

            jObject.Add("lines", linesArray);

            return jObject;
        }

    }
}
