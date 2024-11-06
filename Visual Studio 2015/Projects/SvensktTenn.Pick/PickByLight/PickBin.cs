using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvensktTenn.Pick
{
    public class PickBin
    {
        public string binCode { get; set; }
        public Dictionary<string, PickItem> itemDictionary { get; set; }

        public PickBin()
        {
            itemDictionary = new Dictionary<string, PickItem>();
        }
    }
}
