using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.SmartSystems.SmartLibrary
{
    public interface IModule
    {
        void init(Configuration configuration);
        void start();
    }
}
