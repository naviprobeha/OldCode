using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Newbody.Woppen.Library
{
    public class PackingImage
    {
        private string _webImageCode;
        private string _description;

        public string webImageCode { get { return _webImageCode; } set { _webImageCode = value; } }
        public string description { get { return _description; } set { _description = value; } }
    }
}
