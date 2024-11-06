using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.OSGi.Interfaces
{
    public interface OSGiFramework
    {
        Configuration getConfiguration();
        void writeToLog(string moduleName, int level, string message);
    }
}
