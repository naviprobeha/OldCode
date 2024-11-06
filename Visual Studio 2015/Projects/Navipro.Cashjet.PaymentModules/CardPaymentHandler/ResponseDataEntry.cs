using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.PaymentModules.CardPaymentHandler
{
    public class ResponseDataEntry
    {
        private string _code;
        private string _value;

        public ResponseDataEntry(string code, string value)
        {
            _code = code;
            _value = value;
        }

        public string code { get { return _code; } set { _code = value; } }
        public string value { get { return _value; } set { _value = value; } }
    }
}
