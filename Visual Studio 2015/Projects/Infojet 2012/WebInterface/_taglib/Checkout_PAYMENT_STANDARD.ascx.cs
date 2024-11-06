using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class Checkout_PAYMENT_STANDARD : System.Web.UI.UserControl, Navipro.Infojet.Lib.InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected PaymentModule paymentModule;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            if (webCheckout.webCartHeader.webPaymentMethod != null)
            {
                paymentModule = webCheckout.webCartHeader.webPaymentMethod.getPaymentModule(webCheckout);
            }
        }

        #region InfojetUserControl Members

        public void setWebPageLine(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}