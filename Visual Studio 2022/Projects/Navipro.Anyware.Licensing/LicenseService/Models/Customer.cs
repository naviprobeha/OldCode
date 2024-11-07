using LicenseService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseService.Models
{
    public class Customer
    {
        public string id { get; set; }
        public string no { get; set; }
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

        [NotMapped]
        public List<Subscription> subscriptions { get; set; }


        public void applySubscriptions(SystemDatabase systemDatabase)
        {
            List<Product> productList = systemDatabase.Products.ToList();
            subscriptions = systemDatabase.Subscriptions.Where(s => s.customerId == this.id).ToList();

            int i = 0;
            while (i < subscriptions.Count)
            {
                subscriptions[i].applyProduct(productList);

                i++;
            }
        }

        internal void FromCustomer(Customer customer)
        { 
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
        }

        internal bool Changed(Customer customer)
        {
            if ((this.name != customer.name) || (this.address != customer.address) || (this.address2 != customer.address2) || (this.postCode != customer.postCode) || (this.city != customer.city) || (this.countryCode != customer.countryCode) || (this.contactName != customer.contactName) || (this.phoneNo != customer.phoneNo) || (this.email != customer.email)) return true;
            return false;
        }                            
    }
}


