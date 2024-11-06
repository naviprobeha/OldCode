using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class Organization
    {
        private string _no;
        private string _name;
        private string _address;
        private string _address2;
        private string _postCode;
        private string _city;
        private string _countryCode;
        private string _phoneNo;
        private string _faxNo;
        private string _eMail;
        private string _contactName;
        private decimal _stopFee;

        public Organization()
        {

        }

        public Organization(Navipro.SantaMonica.Common.Organization organization)
        {
            _no = organization.no;
            _name = organization.name;
            _address = organization.address;
            _address2 = organization.address2;
            _postCode = organization.postCode;
            _city = organization.city;
            _countryCode = organization.countryCode;
            _phoneNo = organization.phoneNo;
            _faxNo = organization.faxNo;
            _eMail = organization.email;
            _contactName = organization.contactName;
            _stopFee = organization.stopFee;
        }

        public string no { get { return _no; } set { _no = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public string address2 { get { return _address2; } set { _address2 = value; } }
        public string postCode { get { return _postCode; } set { _postCode = value; } }
        public string city { get { return _city; } set { _city = value; } }
        public string countryCode { get { return _countryCode; } set { _countryCode = value; } }
        public string phoneNo { get { return _phoneNo; } set { _phoneNo = value; } }
        public string faxNo { get { return _faxNo; } set { _faxNo = value; } }
        public string eMail { get { return _eMail; } set { _eMail = value; } }
        public string contactName { get { return _contactName; } set { _contactName = value; } }
        public decimal stopFee { get { return _stopFee; } set { _stopFee = value; } }


    }
}
