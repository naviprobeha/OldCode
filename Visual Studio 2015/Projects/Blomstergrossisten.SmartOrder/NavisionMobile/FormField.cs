using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Navipro.Infojet.WebService
{
    public class FormField
    {
        private string _webSiteCode;
        private string _webFormCode;
        private string _fieldCode;
        private string _value;

        public FormField()
        { }

        public string webSiteCode { get { return _webSiteCode; } set { _webSiteCode = value; } }
        public string webFormCode { get { return _webFormCode; } set { _webFormCode = value; } }
        public string fieldCode { get { return _fieldCode; } set { _fieldCode = value; } }
        public string value { get { return _value; } set { _value = value; } }


    }
}
