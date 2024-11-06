using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class Translate : System.Web.UI.UserControl
    {
        private string _code;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            translationLiteral.Text = infojet.translate(_code);
            

        }

        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }
 
    }
}