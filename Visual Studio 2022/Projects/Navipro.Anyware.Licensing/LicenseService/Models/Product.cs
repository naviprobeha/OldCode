using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseService.Models
{
    public class Product
    {
        public string id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public decimal unitPricePerMonth { get; set; }
        public decimal unitPricePerYear { get; set; }
        public int trialPeriodDays { get; set; }

        internal void FromProduct(Product product)
        {
            code = product.code;
            description = product.description;  
            unitPricePerMonth = product.unitPricePerMonth;
            unitPricePerYear = product.unitPricePerYear;
            trialPeriodDays = product.trialPeriodDays;
        }
    }
}


