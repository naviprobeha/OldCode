using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.OSGi.Interfaces
{
    public interface OSGiModule
    {
        void load(Configuration configuration, OSGiFramework framework, OSGiLogger logger);
        void start();
        void stop();
        string getStatus();
        string sendMessage(string message);            
    }
}
