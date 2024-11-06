using System;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for CustomerHistoryOrder.
	/// </summary>
	public class CustomerHistoryOrder
	{

		private string _no;
        private bool _transfered;

        private string _customerNo;

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

        private string _orderDate;
        private string _yourReference;
        private string _externalDocumentNo;
        private string _paymentMethodCode;
        private string _shipmentMethodCode;
        private string _shipmentDescription;
        private string _paymentDescription;
        private string _shippingAgentCode;
        private string _shippingAgentServiceCode;
        private string _shippingAgentName;
        private string _shippingAgentServiceName;
        private string _sellToContact;
        private string _shipToContact;
        private string _billToContact;

        private string _phoneNo;
        private string _email;
        private string _billToPhoneNo;
        private string _shipToPhoneNo;

        private string _amount;
        private string _amountIncludingVat;

        private string _description;
        private string _link;

        private CustomerHistoryLineCollection _lines;
        private CustomerHistoryShipmentCollection _shipments;

		public CustomerHistoryOrder()
		{
			//
			// TODO: Add constructor logic here
			//
            _shipments = new CustomerHistoryShipmentCollection();
            _lines = new CustomerHistoryLineCollection();
		}

        public string no { get { return _no; } set { _no = value; } }
        public bool transfered { get { return _transfered; } set { _transfered = value; } }
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
        public string yourReference { get { return _yourReference; } set { _yourReference = value; } }
        public string externalDocumentNo { get { return _externalDocumentNo; } set { _externalDocumentNo = value; } }
        public string shipmentMethodCode { get { return _shipmentMethodCode; } set { _shipmentMethodCode = value; } }
        public string paymentMethodCode { get { return _paymentMethodCode; } set { _paymentMethodCode = value; } }

        public string shipmentDescription { get { return _shipmentDescription; } set { _shipmentDescription = value; } }
        public string paymentDescription { get { return _paymentDescription; } set { _paymentDescription = value; } }

        public string shippingAgentCode { get { return _shippingAgentCode; } set { _shippingAgentCode = value; } }
        public string shippingAgentServiceCode { get { return _shippingAgentServiceCode; } set { _shippingAgentServiceCode = value; } }
        public string shippingAgentName { get { return _shippingAgentName; } set { _shippingAgentName = value; } }
        public string shippingAgentServiceName { get { return _shippingAgentServiceName; } set { _shippingAgentServiceName = value; } }
        public string amount { get { return _amount; } set { _amount = value; } }
        public string amountIncludingVat { get { return _amountIncludingVat; } set { _amountIncludingVat = value; } }
        public string sellToContact { get { return _sellToContact; } set { _sellToContact = value; } }
        public string shipToContact { get { return _shipToContact; } set { _shipToContact = value; } }
        public string billToContact { get { return _billToContact; } set { _billToContact = value; } }

        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string email { get { return _email; } set { _email = value; } }
        public string billToPhoneNo { get { return _billToPhoneNo; } set { _billToPhoneNo = value; } }
        public string shipToPhoneNo { get { return _shipToPhoneNo; } set { _shipToPhoneNo = value; } }

        public string description { get { return _description; } set { _description = value; } }
        public string link { get { return _link; } set { _link = value; } }

        public CustomerHistoryShipmentCollection shipments { get { return _shipments; } set { _shipments = value; } }
        public CustomerHistoryLineCollection lines { get { return _lines; } set { _lines = value; } }

	}
}
