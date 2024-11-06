using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.Goldfinger
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


    }
}
