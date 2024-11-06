using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Newbody.PartnerPortal.Library
{
    public class BankDetails
    {
        public string _bankAccountNo = "";
        public bool _swish = false;

        public BankDetails()
        { }

        public string bankAccountNo { get { return _bankAccountNo; } set { _bankAccountNo = value; } }
        public bool swish { get { return _swish; } set { _swish = value; } }
    }
}
