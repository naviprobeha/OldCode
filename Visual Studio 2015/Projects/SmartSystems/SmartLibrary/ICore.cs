using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.SmartSystems.SmartLibrary
{
    public interface ICore
    {
        void init(Configuration configuration);
        void start();
    }
}
