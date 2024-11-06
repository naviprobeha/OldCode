using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.SmartSystems.SmartLibrary
{
    public interface Logger
    {
        void write(string source, int level, string message);
    }
}
