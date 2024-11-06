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

namespace Navipro.Infojet.WebInterface
{
    public partial class order_sp : System.Web.UI.Page
    {
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();
            infojet.init();
            infojet.loadPage();

            try
            {
                //searchButton.Text = infojet.translate("SEARCH");
            }
            catch (Exception) { }
        }

    }
}
