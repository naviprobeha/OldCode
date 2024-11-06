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
using Klarna.Checkout;
 

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class Checkout_2_KLARNA : System.Web.UI.UserControl, InfojetUserControl
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

            string klarnaUrl = ConfigurationSettings.AppSettings["KLARNA_Url"];
            string klarnaStoreId = ConfigurationSettings.AppSettings["KLARNA_StoreId"];
            string klarnaSecret = ConfigurationSettings.AppSettings["KLARNA_Secret"];

            try
            {
                
                var connector = Connector.Create(klarnaSecret);

                // Retrieve location from session object.
                // Use following in ASP.NET.
                // var checkoutId = Session["klarna_checkout"] as Uri;
                // Just a placeholder in this example.
                WebPage checkoutPage = new WebPage(infojet, infojet.webSite.code, webCheckout.step1WebPageCode);

                if (Session["klarna_checkout_uri"] == null) infojet.redirect(infojet.webSite.getStartPageUrl());

                Uri checkoutId = (Uri)Session["klarna_checkout_uri"];               
                var order = new Order(connector, checkoutId)
                {
                    ContentType = "application/vnd.klarna.checkout.aggregated-order-v2+json"
                    
                };

                order.Fetch();

                if ((string)order.GetValue("status") == "checkout_incomplete")
                {
                    
                    infojet.redirect(checkoutPage.getUrl());
                }

                // Display thank you snippet
                var gui = order.GetValue("gui") as Newtonsoft.Json.Linq.JObject;
                var snippet = gui["snippet"];

                

                Literal litteral = new Literal();
                litteral.Text = string.Format("<div>{0}</div>", snippet);
                //throw new Exception(string.Format("<div>{0}</div>", snippet));
                checkoutForm.Controls.Add(litteral);

                // Clear session object.
                Session["klarna_checkout_uri"] = null;

                infojet.cartHandler.emptyCart();
            }
            catch (Exception ex)
            {

                throw new Exception("Klarna Exception: "+ex.Message);
            }

        }

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

    }
}