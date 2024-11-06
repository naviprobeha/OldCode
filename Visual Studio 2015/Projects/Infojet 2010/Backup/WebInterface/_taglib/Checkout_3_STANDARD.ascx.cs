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
    public partial class Checkout_3_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected string errorMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            webCheckout.checkUserAuthorization();

            goBackButton.Text = infojet.translate("BACK");
            nextButton.Text = infojet.translate("NEXT");
            if (webCheckout.sendOrderIsNext()) nextButton.Text = infojet.translate("SEND ORDER");

            goBackButton.Click += new EventHandler(goBackButton_Click);
            nextButton.Click += new EventHandler(nextButton_Click);

            salesTermsValidator.ErrorMessage = infojet.translate("MUST CONFIRM TERMS");
        }

        void nextButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            webCheckout.webCartHeader.salesTermsConfirmed = true;
            webCheckout.webCartHeader.save();

            webCheckout.goNext();

            errorMessage = webCheckout.lastErrorMessage;
        }

        void goBackButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            webCheckout.goBack();
        }


        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}