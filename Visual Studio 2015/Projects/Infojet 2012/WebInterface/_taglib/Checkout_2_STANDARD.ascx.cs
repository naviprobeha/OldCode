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
        protected WebCheckout webCheckout;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();
            webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

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
            
            webCheckout.updatePaymentAndFreight();

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
            applyDiscountButton.Click += new EventHandler(applyDiscountButton_Click);



            discountPanel.Visible = webCheckout.allowDiscountCode;
        }

        void applyDiscountButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            if (webCheckout.allowDiscountCode)
            {
                webCheckout.webCartHeader.applyCampaignCode(discountCodeBox.Text);
                updatePaymentShipmentInformation();

                discountCodeBox.Text = "";

                this.cartItemRepeater.DataSource = webCheckout.getCartLines(infojet);
                this.cartItemRepeater.DataBind();

            }
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
            //WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            if (webCartHeader.webShipmentMethod != null)
            {
                shipmentMethodText.Text = webCartHeader.webShipmentMethod.text;
            }

            if (webCartHeader.webPaymentMethod != null)
            {
                paymentMethodText.Text = webCartHeader.webPaymentMethod.text;
            }

            if (webCartHeader.pricesInclVat)
            {
                this.freightAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.freightFeeInclVat, infojet.currencyCode);
                this.adminAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.adminFeeInclVat, infojet.currencyCode);
            }
            else
            {
                this.freightAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.freightFee, infojet.currencyCode);
                this.adminAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.adminFee, infojet.currencyCode);
            }

            this.totalLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmount(), infojet.currencyCode);
            this.vatAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalVatAmount(), infojet.currencyCode);
            this.totalInclVatLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmountInclVat(), infojet.currencyCode);


            nextButton.Visible = true;
            errorMessage.Visible = false;

            if (cartItemRepeater.Items.Count == 0) nextButton.Visible = false;
            if (!webCheckout.checkMinAmount())
            {
                nextButton.Visible = false;
                errorMessage.Visible = true;
                errorMessage.Text = infojet.translate("MIN ORDER AMOUNT").Replace("%1", webCartHeader.webPaymentMethod.minOrderAmount.ToString());
            }

        }


        protected void updateCart_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);
            int entryNo = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "update")
            {
                TextBox quantityValue = (TextBox)e.Item.FindControl("quantityBox");
                TextBox referenceValue = (TextBox)e.Item.FindControl("referenceBox");

                try
                {
                    webCheckout.updateQuantity(entryNo, int.Parse(quantityValue.Text));

                    if (referenceValue != null)
                    {
                        webCheckout.updateReference(entryNo, referenceValue.Text);
                    }

                }
                catch (Exception) { }
            }

            if (e.CommandName == "remove")
            {
                
                infojet.cartHandler.removeItemFromCart(entryNo);                

            }

            webCheckout.updatePaymentAndFreight();

            infojet.redirect(infojet.webPage.getUrl());
        }


        void goBackButton_Click(object sender, EventArgs e)
        {
            webCheckout.goBack();
        }

        void nextButton_Click(object sender, EventArgs e)
        {
            //if ((webCheckout.checkInventory()) && (webCheckout.checkMinQuantities())) webCheckout.goNext();
            bool inventoryCheck = webCheckout.checkInventory();
            if (!inventoryCheck)
            {
                errorMessage.Text = infojet.translate("INVCHECK ERROR");
                errorMessage.Visible = true;
            }
            else
            {
                webCheckout.goNext();
            }
 
       }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}