using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTCPJobLibrary
{
    public interface Logger
    {
        void write(string message);
    }
}
