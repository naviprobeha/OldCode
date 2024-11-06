using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class WebModelDimension
    {
        private string _code;
        private string _description;

        private WebModelDimValueCollection _values;

        public WebModelDimension(WebItemVarDim webItemVarDim)
        {
            this._code = webItemVarDim.code;
            this._description = webItemVarDim.description;

        }

        public string code { get { return _code; } }
        public string description { get { return _description; } }
        public WebModelDimValueCollection values { get { return _values; } set { _values = value; } }


    }
}
