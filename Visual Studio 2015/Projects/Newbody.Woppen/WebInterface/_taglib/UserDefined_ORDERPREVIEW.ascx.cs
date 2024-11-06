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
    public partial class UserDefined_ORDERPREVIEW : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected float totalQuantity;
        protected float totalAmount;
        protected string prevWebPageUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            SalesIDs salesIds = new SalesIDs();
            SalesID salesId = (SalesID)Session["currentSalesId"];
            if (salesId == null) infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());
            if (!salesId.isContactPerson(infojet.userSession.webUserAccount.no)) infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());



            WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
            prevWebPageUrl = webPage.getUrl();


            cartItemRepeater.DataSource = salesId.getSalesIdCartLines(out totalQuantity, out totalAmount);
            cartItemRepeater.DataBind();

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;

        }

        #endregion
    }
}