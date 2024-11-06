using System;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for CustomerHistoryOrder.
	/// </summary>
	public class CustomerHistoryShipment
	{

		private string _no;
        private string _customerNo;
        private string _orderNo;

        private string _customerName;
        private string _customerName2;
        private string _address;
        private string _address2;
        private string _postCode;
        private string _city;
        private string _countryCode;

        private string _shipToName;
        private string _shipToName2;
        private string _shipToAddress;
        private string _shipToAddress2;
        private string _shipToPostCode;
        private string _shipToCity;
        private string _shipToCountryCode;

        private string _shipmentMethodCode;
        private string _shippingAgentCode;
        private string _shippingAgentName;
        private string _shippingAgentServiceCode;
        private string _shippingAgentServiceName;
        private string _shippingAgentInternetAddress;
        private string _sellToContact;
        private string _shipToContact;

        private string _orderDate;
        private string _shipmentDate;
        private string _yourReference;
        private string _externalDocumentNo;

        private string _description;
        private string _packageTrackingNo;
        private string _link;

        private ArrayList _lines;

		public CustomerHistoryShipment()
		{
			//
			// TODO: Add constructor logic here
			//

			lines = new ArrayList();
		}

        public string no { get { return _no; } set { _no = value; } }
        public string customerNo { get { return _customerNo; } set { _customerNo = value; } }

        public string customerName { get { return _customerName; } set { _customerName = value; } }
        public string customerName2 { get { return _customerName2; } set { _customerName2 = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string address2 { get { return _address2; } set { _address2 = value; } }
        public string postCode { get { return _postCode; } set { _postCode = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }

        public string shipToName { get { return _shipToName; } set { _shipToName = value; } }
        public string shipToName2 { get { return _shipToName2; } set { _shipToName2 = value; } }
        public string shipToAddress { get { return _shipToAddress; } set { _shipToAddress = value; } }
        public string shipToAddress2 { get { return _shipToAddress2; } set { _shipToAddress2 = value; } }
        public string shipToPostCode { get { return _shipToPostCode; } set { _shipToPostCode = value; } }
        public string shipToCity { get { return _shipToCity; } set { _shipToCity = value; } }
        public string shipToCountryCode { get { return _shipToCountryCode; } set { _shipToCountryCode = value; } }

        public string orderDate { get { return _orderDate; } set { _orderDate = value; } }
        public string orderNo { get { return _orderNo; } set { _orderNo = value; } }
        public string shipmentDate { get { return _shipmentDate; } set { _shipmentDate = value; } }
        public string yourReference { get { return _yourReference; } set { _yourReference = value; } }
        public string externalDocumentNo { get { return _externalDocumentNo; } set { _externalDocumentNo = value; } }
        public string shipmentMethodCode { get { return _shipmentMethodCode; } set { _shipmentMethodCode = value; } }
        public string shippingAgentCode { get { return _shippingAgentCode; } set { _shippingAgentCode = value; } }
        public string shippingAgentServiceCode { get { return _shippingAgentServiceCode; } set { _shippingAgentServiceCode = value; } }
        public string shippingAgentName { get { return _shippingAgentName; } set { _shippingAgentName = value; } }
        public string shippingAgentServiceName { get { return _shippingAgentServiceName; } set { _shippingAgentServiceName = value; } }
        public string shippingAgentInternetAddress { get { return _shippingAgentInternetAddress; } set { _shippingAgentInternetAddress = value; } }
        public string packageTrackingNo { get { return _packageTrackingNo; } set { _packageTrackingNo = value; } }
        public string sellToContact { get { return _sellToContact; } set { _sellToContact = value; } }
        public string shipToContact { get { return _shipToContact; } set { _shipToContact = value; } }

        public string description { get { return _description; } set { _description = value; } }
        public string link { get { return _link; } set { _link = value; } }

        public ArrayList lines { get { return _lines; } set { _lines = value; } }

	}
}
