using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klarna.Core;

namespace Navipro.KlarnaAPI.Wrapper
{
    public class Address
    {
        public string email;
        public string phoneNo;
        public string firstName;
        public string lastName;
        public string name2;
        public string address;
        public string postCode;
        public string city;
        public string countryCode;
        public string cellPhoneNo;
        public string houseNo;
        public string houseExt;

        public API.Country country
        {
            get
            {
                if (countryCode == "SE") return API.Country.Sweden;
                if (countryCode == "DK") return API.Country.Denmark;
                if (countryCode == "DE") return API.Country.Germany;
                if (countryCode == "FI") return API.Country.Finland;
                if (countryCode == "NL") return API.Country.Netherlands;
                if (countryCode == "NO") return API.Country.Norway;
                return API.Country.Sweden;
            }
        }


    }
}
