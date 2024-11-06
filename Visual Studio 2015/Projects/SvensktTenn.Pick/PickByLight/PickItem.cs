using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvensktTenn.Pick
{
    public class PickItem
    {
        public string itemNo { get; set; }
        public string description { get; set; }
        public string description2 { get; set; }
        public string description3 { get; set; }
        public string description4 { get; set; }

        public decimal totalQty { get; set; }
        public decimal pickedQty { get; set; }

        public List<PickItemOrder> orderList { get; set; }

        public PickItem()
        {
            orderList = new List<PickItemOrder>();
        }
    }
}
