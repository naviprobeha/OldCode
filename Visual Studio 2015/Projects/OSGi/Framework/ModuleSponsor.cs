using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Lifetime;

namespace Navipro.OSGi.Framework
{
    public class ModuleSponsor : MarshalByRefObject, ISponsor
    {
        public TimeSpan Renewal(ILease lease)
        {
            return lease.InitialLeaseTime;
        }
    }

}
