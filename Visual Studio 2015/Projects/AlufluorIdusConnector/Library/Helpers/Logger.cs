using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviPro.Alufluor.Idus.Library.Helpers
{
    public interface Logger
    {
        void write(string type, string message);
    }
}
