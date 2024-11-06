using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class WebCartField
    {
        private string _webFormCode;
        private string _fieldCode;
        private string _description;
        private string _value;
        private bool _transferToOrderTextLine;

        public WebCartField() { }

        public WebCartField(WebFormField webFormField)
        {
            this._webFormCode = webFormField.webFormCode;
            this._fieldCode = webFormField.originalCode;
            this._description = webFormField.getCaption();
            this._transferToOrderTextLine = webFormField.transferToOrderTextLine;
        }

        public string webFormCode { get { return _webFormCode; } set { _webFormCode = value; } }
        public string fieldCode { get { return _fieldCode; } set { _fieldCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string value { get { return _value; } set { _value = value; } }
        public bool transferToOrderTextLine { get { return _transferToOrderTextLine; } set { _transferToOrderTextLine = value; } }
    }
}
