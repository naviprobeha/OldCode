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
using System.Collections.Generic;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class Checkout_1_KLARNA : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected WebCartHeader webCartHeader;
        protected WebCheckout webCheckout;
        protected CartItemCollection cartItemCollection;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();
            webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            webCheckout.checkUserAuthorization();


            this.webCartHeader = webCheckout.webCartHeader;

            cartItemCollection = webCheckout.getCartLines(infojet);
            this.cartItemRepeater.DataSource = cartItemCollection;
            this.cartItemRepeater.DataBind();

            webCheckout.updatePaymentAndFreight();

            applyDiscountButton.Click += new EventHandler(applyDiscountButton_Click);



            discountPanel.Visible = webCheckout.allowDiscountCode;

            updatePaymentShipmentInformation();

            applyKlarnaCheckout();
        }

        void applyDiscountButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            if (webCheckout.allowDiscountCode)
            {
                webCheckout.webCartHeader.applyCampaignCode(discountCodeBox.Text);
                

                discountCodeBox.Text = "";

                this.cartItemRepeater.DataSource = webCheckout.getCartLines(infojet);
                this.cartItemRepeater.DataBind();

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
            webCheckout.processForm((Panel)checkoutForm.Controls[0]);
            webCheckout.updatePaymentAndFreight();

            //if ((webCheckout.checkInventory()) && (webCheckout.checkMinQuantities())) webCheckout.goNext();
            if (webCheckout.checkInventory())
            {
                webCheckout.goNext();

            }
            else
            {
                errorMessage.Visible = true;
                errorMessage.Text = infojet.translate("INVENTORY ERROR");
            }
        }

        public void updateButton_Click(object sender, EventArgs e)
        {


            int i = 0;
            while (i < cartItemRepeater.Items.Count)
            {
                System.Web.UI.WebControls.HiddenField lineNoField = (System.Web.UI.WebControls.HiddenField)cartItemRepeater.Items[i].FindControl("lineNoField");
                System.Web.UI.WebControls.TextBox qtyBox = (System.Web.UI.WebControls.TextBox)cartItemRepeater.Items[i].FindControl("qtyBox");

                webCheckout.updateQuantity(int.Parse(lineNoField.Value), int.Parse(qtyBox.Text));

                i++;
            }

            infojet.cartHandler.reCalculateCart();
            infojet.cartHandler.setForced(true);

            cartItemCollection = webCheckout.getCartLines(infojet);
            this.cartItemRepeater.DataSource = cartItemCollection;
            this.cartItemRepeater.DataBind();

            webCheckout.updatePaymentAndFreight();
            webCheckout.forceAmountUpdate();

            applyKlarnaCheckout();
        }

        protected void updatePaymentShipmentInformation()
        {

            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            //WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            if (webCartHeader.webShipmentMethod != null)
            {
                //shipmentMethodText.Text = webCartHeader.webShipmentMethod.text;
            }

            if (webCartHeader.webPaymentMethod != null)
            {
                //paymentMethodText.Text = webCartHeader.webPaymentMethod.text;
            }

            this.freightAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.freightFee, infojet.currencyCode);
            this.adminAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCartHeader.adminFee, infojet.currencyCode);

            this.totalLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmount(), infojet.currencyCode);
            this.vatAmountLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalVatAmount(), infojet.currencyCode);
            this.totalInclVatLabel.Text = infojet.systemDatabase.formatCurrency(webCheckout.getTotalAmountInclVat(), infojet.currencyCode);

            //nextButton.CssClass = "submitButton";
            //nextButton.Enabled = true;
            errorMessage.Visible = false;

            //if (cartItemRepeater.Items.Count == 0) nextButton.Enabled = false;
            if (!webCheckout.checkMinAmount())
            {
                //nextButton.Enabled = false;
                errorMessage.Visible = true;
                errorMessage.Text = infojet.translate("MIN ORDER AMOUNT").Replace("%1", webCartHeader.webPaymentMethod.minOrderAmount.ToString());
            }

            //if (!nextButton.Enabled) nextButton.CssClass = "disabledBtn";

        }


        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        private void applyKlarnaCheckout()
        {
            WebPage checkoutPage = new WebPage(infojet, infojet.webSite.code, webCheckout.step1WebPageCode);
            WebPage confirmationPage = new WebPage(infojet, infojet.webSite.code, webCheckout.step2WebPageCode);

            var cartItems = new List<Dictionary<string, object>>();

            string klarnaUrl = ConfigurationSettings.AppSettings["KLARNA_Url"];
            string klarnaStoreId = ConfigurationSettings.AppSettings["KLARNA_StoreId"];
            string klarnaSecret = ConfigurationSettings.AppSettings["KLARNA_Secret"];


            int i = 0;
            while (i < cartItemCollection.Count)
            {
                
                var cartItemEntry = new Dictionary<string, object>
                            {
                                { "reference", cartItemCollection[i].itemNo },
                                { "name", cartItemCollection[i].description },
                                { "quantity", (int)cartItemCollection[i].quantity },
                                { "unit_price", (int)(Math.Round((cartItemCollection[i].unitPrice*1.25), 2)*100) },
                                { "discount_rate", 0 },
                                { "tax_rate", 2500 }
                            };

                cartItems.Add(cartItemEntry);
                i++;
            }

             
            var cart = new Dictionary<string, object> { { "items", cartItems } };

            var data = new Dictionary<string, object>
            {
                { "cart", cart }
            };

            

            var merchant = new Dictionary<string, object>
            {
                { "id", klarnaStoreId },
                { "terms_uri", infojet.webSite.location+"onecol.aspx?pageCode=sales terms" },
                {
                    "checkout_uri",
                    checkoutPage.getUrl()
                },
                {
                    "confirmation_uri",
                    confirmationPage.getUrl() +
                    "&klarna_order={checkout.order.uri}"
                },
                {
                    "push_uri",
                    infojet.webSite.siteLocation+"klarna_push.aspx?pageCode="+infojet.webPage.code+"&customerNo=" +webCartHeader.customerNo+"&sessionId="+infojet.sessionId+"&klarna_order={checkout.order.uri}"
                }
            };

            data.Add("purchase_country", "SE");
            data.Add("purchase_currency", "SEK");
            data.Add("locale", "sv-se");
            data.Add("merchant", merchant);


            const string ContentType = "application/vnd.klarna.checkout.aggregated-order-v2+json";

            var connector = Klarna.Checkout.Connector.Create(klarnaSecret);//secret

            Klarna.Checkout.Order order = null;

            if (System.Web.HttpContext.Current.Session["klarna_checkout_uri"] != null)
            {
                order = new Klarna.Checkout.Order(connector, (Uri)System.Web.HttpContext.Current.Session["klarna_checkout_uri"])
                {
                    BaseUri = new Uri(klarnaUrl),
                    ContentType = ContentType
                };
                order.Update(data);
            }
            else
            {
                order = new Klarna.Checkout.Order(connector)
                {
                    BaseUri = new Uri(klarnaUrl),
                    ContentType = ContentType
                };
                order.Create(data);
                
            }

            
            order.Fetch();

            // Store location of checkout session is session object.
            System.Web.HttpContext.Current.Session["klarna_checkout_uri"] = order.Location;

            // Display checkout
            var gui = order.GetValue("gui") as Newtonsoft.Json.Linq.JObject;
            var snippet = gui["snippet"];

            Literal litteral = new Literal();
            litteral.Text = string.Format("<div>{0}</div>", snippet);
            //throw new Exception(string.Format("<div>{0}</div>", snippet));
            checkoutForm.Controls.Add(litteral);


        }

        #endregion
    }
}