using LicenseService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseService.Models
{
    public class CustomerHistory
    {
        public string id { get; set; }
        public string customerId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string postCode { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string contactName { get; set; }
        public string phoneNo { get; set; }
        public string email { get; set; }
        public string vatRegistrationNo { get; set; }
        public DateTime updatedDateTime { get; set; }


        internal void FromCustomer(Customer customer)
        {
            this.id = Guid.NewGuid().ToString();
            this.customerId = customer.id;
            this.name = customer.name;
            this.address = customer.address;
            this.address2 = customer.address2;
            this.postCode = customer.postCode;
            this.city = customer.city;
            this.countryCode = customer.countryCode;
            this.contactName = customer.contactName;
            this.phoneNo = customer.phoneNo;
            this.email = customer.email;
            this.vatRegistrationNo = customer.vatRegistrationNo;
            this.updatedDateTime = DateTime.Now;
        }
    }
}


