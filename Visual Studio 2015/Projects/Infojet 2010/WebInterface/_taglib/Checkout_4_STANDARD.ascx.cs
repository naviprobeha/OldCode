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
    public partial class Checkout_4_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected WebCartHeader webCartHeader;
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

            this.webCartHeader = webCheckout.webCartHeader;
            CartItemCollection cartItemCollection = webCheckout.getCartLines(infojet);
            cartItemCollection = webCheckout.addInventoryInfo(infojet, cartItemCollection);
            this.cartItemRepeater.DataSource = cartItemCollection;
            this.cartItemRepeater.DataBind();

            this.freightAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.freightFee, infojet.currencyCode);
            this.adminAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.adminFee, infojet.currencyCode);

            this.totalLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmount(), infojet.currencyCode);
            this.vatAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalVatAmount(), infojet.currencyCode);
            this.totalInclVatLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmountInclVat(), infojet.currencyCode);

        }

        void nextButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

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