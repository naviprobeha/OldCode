using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class SalesIDStatistics
    {
        private float _balance;
        private float _soldPackages;
        private float _profitAmount;

        public SalesIDStatistics() { }

        public float balance { get { return _balance; } set { _balance = value; } }
        public float soldPackages { get { return _soldPackages; } set { _soldPackages = value; } }
        public float profitAmount { get { return _profitAmount; } set { _profitAmount = value; } }

    }
}
