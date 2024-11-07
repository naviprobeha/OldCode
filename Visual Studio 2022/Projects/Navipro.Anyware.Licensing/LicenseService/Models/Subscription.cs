using LicenseService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseService.Models
{
    public class Subscription
    {
        public string id { get; set; }
        public string customerId { get; set; }
        public string productId { get; set; }
        public int type { get; set; }
        public decimal unitPrice { get; set; }
        public string statusCode { get; set; }
        public DateTime expirationDate { get; set; }
        public DateTime createdDate { get; set; }

        public string companyName { get; set; }
        public string environmentType { get; set; }
        public string environmentName { get; set; }


        [NotMapped]
        public Product product { get; set; }
        [NotMapped]
        public Customer customer { get; set; }

        public void applyProduct(List<Product> productList)
        {
            product = productList.FirstOrDefault(p => p.id == productId);
        }
    }
}
