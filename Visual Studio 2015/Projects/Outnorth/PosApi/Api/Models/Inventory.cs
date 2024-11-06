using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Inventory
    {
        public string id { get; set; }
        public string sku { get; set; }

        public string itemNo { get; set; }
        public string variantCode { get; set; }
        public string locationCode { get; set; }

        public decimal quantity { get; set; }
        public decimal nextReceiptQty { get; set; }
        public DateTime nextReceiptDate { get; set; }

    }
}