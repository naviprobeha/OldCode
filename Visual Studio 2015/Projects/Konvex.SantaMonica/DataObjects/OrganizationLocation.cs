using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class OrganizationLocation
    {
        private string _shippingCustomerNo;
        private string _name;

        public OrganizationLocation()
        { }

        public OrganizationLocation(Navipro.SantaMonica.Common.OrganizationLocation orgLocation)
        {
            _shippingCustomerNo = orgLocation.shippingCustomerNo;
            _name = orgLocation.name;
        }

        public string shippingCustomerNo { get { return _shippingCustomerNo; } set { _shippingCustomerNo = value; } }
        public string name { get { return _name; } set { _name = value; } }

        public static OrganizationLocation fromJsonObject(JObject jsonObject)
        {
            OrganizationLocation entry = new OrganizationLocation();
            entry.shippingCustomerNo = (string)jsonObject["lineCustomerNo"];
            entry.name = (string)jsonObject["name"];


            return entry;

        }
    }
}
