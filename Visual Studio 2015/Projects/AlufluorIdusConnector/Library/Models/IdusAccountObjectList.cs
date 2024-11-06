using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro.Alufluor.Idus.Library.Models
{
    public class IdusAccountObjectList
    {
        public string type { get; set; }
        public string id { get; set; }
        public IdusAccountFields fields { get; set; }
    }
}
