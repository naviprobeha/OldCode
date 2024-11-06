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
    public partial class Checkout_2_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected WebCartHeader webCartHeader;
        protected WebShipmentMethodCollection webShipmentMethodCollection;
        protected WebPaymentMethodCollection webPaymentMethodCollection;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            webCheckout.checkUserAuthorization();

            this.webCartHeader = webCheckout.webCartHeader;
            this.cartItemRepeater.DataSource = webCheckout.getCartLines(infojet);
            this.cartItemRepeater.DataBind();

            webShipmentMethodCollection = webCheckout.getShipmentMethods();
            this.shipmentMethodList.DataSource = webShipmentMethodCollection;
            this.shipmentMethodList.DataBind();

            if (webCartHeader.webShipmentMethodCode == "")
            {
                if (webShipmentMethodCollection.Count > 0) webCartHeader.applyWebShipmentMethod(webShipmentMethodCollection[0]);
            }
            else
            {
                shipmentMethodList.Text = webCartHeader.webShipmentMethodCode;
            }

            webPaymentMethodCollection = webCheckout.getPaymentMethods();
            this.paymentMethodList.DataSource = webPaymentMethodCollection;
            this.paymentMethodList.DataBind();

            if (webCartHeader.webPaymentMethodCode == "")
            {
                if (webPaymentMethodCollection.Count > 0) webCartHeader.applyWebPaymentMethod(webPaymentMethodCollection[0]);
            }
            else
            {
                paymentMethodList.Text = webCartHeader.webPaymentMethodCode;
            }

            updatePaymentShipmentInformation();

            goBackButton.Text = infojet.translate("BACK");
            nextButton.Text = infojet.translate("NEXT");
            if (webCheckout.sendOrderIsNext()) nextButton.Text = infojet.translate("SEND ORDER");

            //if (webCheckout.step3WebPageCode == "") nextButton.Text = infojet.translate("SEND ORDER");

            goBackButton.Click += new EventHandler(goBackButton_Click);
            nextButton.Click += new EventHandler(nextButton_Click);

            if (cartItemRepeater.Items.Count == 0) nextButton.Visible = false;
        }

        protected void shipmentMethodList_SelectedIndexChanged(object sender, EventArgs e)
        {          
            int i = 0;
            while (i < webShipmentMethodCollection.Count)
            {
                if (((DropDownList)sender).Text == webShipmentMethodCollection[i].code)
                {
                    webCartHeader.applyWebShipmentMethod(webShipmentMethodCollection[i]);
                }
                i++;
            }

            updatePaymentShipmentInformation();

        }

        protected void paymentMethodList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            while (i < webPaymentMethodCollection.Count)
            {
                if (((DropDownList)sender).Text == webPaymentMethodCollection[i].code)
                {
                    webCartHeader.applyWebPaymentMethod(webPaymentMethodCollection[i]);
                }
                i++;
            }

            updatePaymentShipmentInformation();

        }

        protected void updatePaymentShipmentInformation()
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            if (webCartHeader.webShipmentMethod != null)
            {
                shipmentMethodText.Text = webCartHeader.webShipmentMethod.text;
            }

            if (webCartHeader.webPaymentMethod != null)
            {
                paymentMethodText.Text = webCartHeader.webPaymentMethod.text;
            }

            this.freightAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.freightFee, infojet.currencyCode);
            this.adminAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.adminFee, infojet.currencyCode);

            this.totalLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmount(), infojet.currencyCode);
            this.vatAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalVatAmount(), infojet.currencyCode);
            this.totalInclVatLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmountInclVat(), infojet.currencyCode);

        }


        protected void updateCart_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int entryNo = int.Parse(e.CommandArgument.ToString());
            TextBox quantityValue = (TextBox)e.Item.FindControl("quantityBox");
            TextBox referenceValue = (TextBox)e.Item.FindControl("referenceBox");

            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            try
            {
                webCheckout.updateQuantity(entryNo, int.Parse(quantityValue.Text));

                if (referenceValue != null)
                {
                    webCheckout.updateReference(entryNo, referenceValue.Text);
                }
            }
            catch (Exception) { }

            infojet.redirect(infojet.webPage.getUrl());
        }


        void goBackButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            webCheckout.goBack();
        }

        void nextButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            webCheckout.goNext();
            
        }

 
        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}