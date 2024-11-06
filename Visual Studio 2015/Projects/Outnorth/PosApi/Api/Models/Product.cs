using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Product
    {
        public string sku { get; set; }
        public string itemNo { get; set; }

        public string variantCode { get; set; }
        public string description { get; set; }
        public string description2 { get; set; }

        public string itemCategoryCode { get; set; }
        public string itemCategoryDescription { get; set; }
        public string productGroupCode { get; set; }
        public string productGroupDescription { get; set; }
        public string brandCode { get; set; }
        public string brandDescription { get; set; }
        public string baseUnitOfMeasureCode { get; set; }
        public string salesUnitOfMeasureCode { get; set; }
        public string seasonCode { get; set; }
        public string composition { get; set; }
        public string colorCode { get; set; }
        public string sizeCode { get; set; }
        public string colorName { get; set; }
        public string baseColorCode { get; set; }
        public string baseColorName { get; set; }

        public string folders { get; set; }
        public int sortOrder { get; set; }
        public string productText { get; set; }
        public string primaryCrossReferenceNo { get; set; }
        public string additionalCrossReferenceNo { get; set; }
        public string vatProductPostingGroup { get; set; }
        public string imageUrl { get; set; }
        public decimal primaryUnitPrice { get; set; }
        public decimal primarySalesPrice { get; set; }
        public decimal unitCost { get; set; }
        public string externalId { get; set; }
  

    }
}