using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.PaymentModules.CardPaymentHandler
{
    public class PrintText
    {
        private string _dataType;
        private string _code;
        private string _description;
        private string _value;

        public PrintText(string dataType, string code, string description, string value)
        {
            _dataType = dataType;
            _code = code;
            _description = description;
            _value = value;
        }

        public string dataType { get { return _dataType; } set { _dataType = value; } }
        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _value = value; } }
        public string value { get { return _value; } set { _value = value; } }
    }
}
