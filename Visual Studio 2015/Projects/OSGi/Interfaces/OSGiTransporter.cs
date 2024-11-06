using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.OSGi.Interfaces
{
    public interface OSGiTransporter
    {
        void load(Configuration configuration, OSGiFramework framework);
        void start();
        void stop();
        string sendMessage(string message);           
    }
}
