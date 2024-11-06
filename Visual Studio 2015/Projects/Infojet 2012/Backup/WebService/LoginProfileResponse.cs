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
    public class LoginProfileResponse
    {
        private bool _result;
        private string _errorMessage;
        private string _responseWebPageCode;

        public LoginProfileResponse()
        { }

        public bool result { get { return _result; } set { _result = value; } }
        public string errorMessage { get { return _errorMessage; } set { _errorMessage = value; } }
        public string responseWebPageCode { get { return _responseWebPageCode; } set { _responseWebPageCode = value; } }

    
    }
}
