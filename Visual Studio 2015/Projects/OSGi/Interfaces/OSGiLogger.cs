using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.OSGi.Interfaces
{
    public interface OSGiLogger
    {
        void write(int level, string message);
        void updateHeartBeat();
    }
}
