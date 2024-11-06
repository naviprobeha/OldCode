using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class FieldValue
    {
        private string _code;
        private string _description;
        private bool _defaultValue;

        public FieldValue()
        {

        }

        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public bool defaultValue { get { return _defaultValue; } set { _defaultValue = value; } }
    }
}
