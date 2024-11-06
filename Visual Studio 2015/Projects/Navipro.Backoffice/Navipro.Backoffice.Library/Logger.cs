using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navipro.Backoffice.Library
{
    public interface Logger
    {
        void write(string message, int level);
    }
}
