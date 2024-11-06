using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.KlarnaAPI.Wrapper
{
    public class InvoiceLine
    {
        public int quantity;
        public string itemNo;
        public string description;
        public double unitPriceInclVat;
        public double vat;
        public double discount;

    }
}
