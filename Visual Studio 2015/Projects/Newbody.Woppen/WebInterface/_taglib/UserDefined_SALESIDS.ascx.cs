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
using Navipro.Newbody.Woppen.Library;

namespace WebInterface._taglib
{
    public partial class UserDefined_SALESIDS : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected string sendOrderLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDs salesIds = new SalesIDs();
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            if (infojet.userSession.webUserAccount.name == "")
            {
                WebPage profileWebPage = infojet.webSite.getWebPageByCategory(infojet.webSite.myProfileCategoryCode, infojet.userSession.webUserAccount.no);
                //WebPage profileWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeContProfile);
                infojet.redirect(profileWebPage.getUrl());
            }

            if (salesIds.checkIfNonAgreedFidsExists(infojet.systemDatabase, infojet.userSession.webUserAccount))
            {
                WebPage agreementWebPage = infojet.webSite.getWebPageByCategory("AGREEMENT", infojet.userSession.webUserAccount.no);
                //WebPage profileWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeContProfile);
                infojet.redirect(agreementWebPage.getUrl());

            }

            SalesIDCollection salesIdCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);
            
            WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);

            salesIdCollection.setPageUrl(webPage.getUrl());

            if (salesIdCollection.Count == 1)
            {
                infojet.redirect(((SalesID)salesIdCollection[0]).pageUrl);
            }

            salesIdRepeater.DataSource = salesIdCollection;
            salesIdRepeater.DataBind();

            WebPage orderWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders1);
            this.sendOrderLink = orderWebPage.getUrl();
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
           
        }

        #endregion
    }
}