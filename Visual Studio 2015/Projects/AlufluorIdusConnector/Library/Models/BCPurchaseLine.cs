using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro.Alufluor.Idus.Library.Models
{
    public class BCPurchaseLine
    {
        public string no { get; set; }
        public int lineNo { get; set; }
        public DateTime orderDate { get; set; }
        public string vendorNo { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorAddress2 { get; set; }
        public string vendorPostCode { get; set; }
        public string vendorCity { get; set; }
        
        public string orderer { get; set; }
        public string receiver { get; set; }
        public string description { get; set; }
        public decimal quantity { get; set; }
        public decimal unitPrice { get; set; }

        public string dimension1Value { get; set; }
        public string dimension2Value { get; set; }
        public string dimension3Value { get; set; }
        public string dimension4Value { get; set; }
        public string dimension5Value { get; set; }
        public string dimension6Value { get; set; }
        public string dimension7Value { get; set; }
        public string dimension8Value { get; set; }

    }
}
