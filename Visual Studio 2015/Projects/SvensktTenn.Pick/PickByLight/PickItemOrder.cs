using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvensktTenn.Pick
{
    public class PickItemOrder
    {
        public string orderNo { get; set; }
        public string itemNo { get; set; }
        public string fromBin { get; set; }
        public string toBin { get; set; }
        public decimal quantity { get; set; }

        public string ean { get; set; }
        public string lineRef { get; set; }

        public decimal quantityPicked { get; set; }

    }
}
