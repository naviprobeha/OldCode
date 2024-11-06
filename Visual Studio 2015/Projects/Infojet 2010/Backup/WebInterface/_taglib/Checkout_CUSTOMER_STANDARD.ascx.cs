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
    public partial class Checkout_CUSTOMER_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            searchButton.Text = infojet.translate("SEARCH");
            searchButton.Click += new EventHandler(searchButton_Click);
        }

        void searchButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebUserAccountCustomerRelations webUserAccountCustomerRelations = new WebUserAccountCustomerRelations(infojet);
            CustomerCollection customerCollection = webUserAccountCustomerRelations.getCustomers(searchNameBox.Text);

            customerRepeater.DataSource = customerCollection;
            customerRepeater.DataBind();

        }

        protected void customerList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCheckout webCheckout = new WebCheckout(infojet, webPageLine.webSiteCode, webPageLine.code);

            string customerNo = (string)e.CommandArgument;
            
            Customer customer = new Customer(infojet.systemDatabase, customerNo);

            WebCartHeader webCartHeader = WebCartHeader.get();
            if (webCartHeader == null)
            {
                webCartHeader = new WebCartHeader(infojet, infojet.sessionId);
                webCartHeader.setUserSession(infojet.userSession);
            }
            webCartHeader.setCustomer(customer);
            
            WebPage webPage = new WebPage(infojet, infojet.webSite.code, webCheckout.step1WebPageCode);
            infojet.redirect(webPage.getUrl());
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}