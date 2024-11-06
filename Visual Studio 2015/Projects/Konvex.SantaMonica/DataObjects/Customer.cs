using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class Customer
    {

        private string _organizationNo;
        private string _no;
        private string _name;
        private string _address;
        private string _address2;
        private string _postCode;
        private string _city;
        private string _countryCode;
        private string _contactName;
        private string _email;
        private string _phoneNo;
        private string _cellPhoneNo;
        private string _faxNo;
        private string _productionSite;
        private string _registrationNo;
        private string _personNo;
        private string _dairyCode;
        private string _dairyNo;
        private bool _hide;
        private int _blocked;
        private bool _forceCashPayment;
        private int _positionX;
        private int _positionY;
        private string _directionComment;
        private string _directionComment2;
        private string _priceGroupCode;
        private string _billToCustomerNo;
        private bool _editable;

        public Customer() { }

        public Customer(Navipro.SantaMonica.Common.Customer customer)
        {
            _organizationNo = customer.organizationNo;
            _no = customer.no;
            _name = customer.name;
            _address = customer.address;
            _address2 = customer.address2;
            _postCode = customer.postCode;
            _city = customer.city;
            _countryCode = customer.countryCode;
            _email = customer.email;
            _phoneNo = customer.phoneNo;
            _cellPhoneNo = customer.cellPhoneNo;
            _faxNo = customer.faxNo;
            _productionSite = customer.productionSite;
            _registrationNo = customer.registrationNo;
            _personNo = customer.personNo;
            _dairyCode = customer.dairyCode;
            _dairyNo = customer.dairyNo;
            _hide = customer.hide;
            _blocked = customer.blocked;
            _forceCashPayment = customer.forceCashPayment;
            _positionX = customer.positionX;
            _positionY = customer.positionY;
            _directionComment = customer.directionComment;
            _directionComment2 = customer.directionComment2;
            _priceGroupCode = customer.priceGroupCode;
            _billToCustomerNo = customer.billToCustomerNo;
            _editable = customer.editable;

           
        }

        public string organizationNo { get { return _organizationNo; } set { _organizationNo = value; } }
        public string no { get { return _no; } set { _no = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string address2 { get { return _address2; } set { _address2 = value; } }
        public string postCode { get { return _postCode; } set { _postCode = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }
        public string email { get { return _email; } set { _email = value; } }
        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string cellPhoneNo { get { return _cellPhoneNo; } set { _cellPhoneNo = value; } }
        public string faxNo { get { return _faxNo; } set { _faxNo = value; } }
        public string productionSite { get { return _productionSite; } set { _productionSite = value; } }
        public string registrationNo { get { return _registrationNo; } set { _registrationNo = value; } }
        public string personNo { get { return _personNo; } set { _personNo = value; } }
        public string dairyCode { get { return _dairyCode; } set { _dairyCode = value; } }
        public bool hide { get { return _hide; } set { _hide = value; } }
        public int blocked { get { return _blocked; } set { _blocked = value; } }
        public bool forceCashPayment { get { return _forceCashPayment; } set { _forceCashPayment = value; } }
        public int positionX { get { return _positionX; } set { _positionX = value; } }
        public int positionY { get { return _positionY; } set { _positionY = value; } }
        public string directionComment { get { return _directionComment; } set { _directionComment = value; } }
        public string directionComment2 { get { return _directionComment2; } set { _directionComment2 = value; } }
        public string priceGroupCode { get { return _priceGroupCode; } set { _priceGroupCode = value; } }
        public string billToCustomerNo { get { return _billToCustomerNo; } set { _billToCustomerNo = value; } }
        public bool editable { get { return _editable; } set { _editable = value; } }

        public static Customer fromJsonObject(JObject jsonObject)
        {
            Customer entry = new Customer();

            entry.no = (string)jsonObject["no"];
            entry.name = (string)jsonObject["name"];
            entry.address = (string)jsonObject["address"];
            entry.address2 = (string)jsonObject["address2"];
            entry.postCode = (string)jsonObject["postCode"];
            entry.city = (string)jsonObject["city"];
            entry.countryCode = (string)jsonObject["countryCode"];
            entry.email = (string)jsonObject["email"];
            entry.phoneNo = (string)jsonObject["phoneNo"];
            entry.cellPhoneNo = (string)jsonObject["cellPhoneNo"];
            entry.faxNo = "";
            entry.productionSite = (string)jsonObject["facilityNo"];
            entry.registrationNo = (string)jsonObject["registrationNo"];
            entry.personNo = (string)jsonObject["personNo"];
            entry.dairyCode = "";
            entry.hide = (bool)jsonObject["hide"];
            entry.blocked = 0;
            if ((bool)jsonObject["blocked"] == true) entry.blocked = 1;
            entry.forceCashPayment = (bool)jsonObject["requireCashPayment"];
            entry.positionX = (int)jsonObject["positionX"];
            entry.positionY = (int)jsonObject["positionY"];
            if (jsonObject["directions"].HasValues) entry.directionComment = (string)jsonObject["directions"];
            entry.directionComment2 = "";
            entry.priceGroupCode = (string)jsonObject["priceGroupCode"];
            if (jsonObject["billToCustomerNo"].HasValues) entry.billToCustomerNo = (string)jsonObject["billToCustomerNo"];
            if (jsonObject["editable"].HasValues) entry.editable = (bool)jsonObject["editable"];

            if (entry.phoneNo.Length > 20) entry.phoneNo = entry.phoneNo.Substring(0, 20);
            if (entry.address2.Length > 30) entry.address2 = entry.address2.Substring(0, 30);
            if (entry.name.Length > 30) entry.name = entry.name.Substring(0, 30);

            return entry;
   
        }
    }
}
