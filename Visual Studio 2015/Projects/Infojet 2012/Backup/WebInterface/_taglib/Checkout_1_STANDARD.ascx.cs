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
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class Checkout_1_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            webCheckout.checkUserAuthorization();

            checkoutForm.Controls.Add(webCheckout.getFormPanel());



        }


        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}