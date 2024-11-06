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

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class UserDefined_SENDORDERS_SALESID : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected string prevWebPageUrl;
        protected string sendUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDs salesIds = new SalesIDs();
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);


            SalesIDCollection releasedCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);
            SalesIDCollection openCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);

            SalesIDCollection salesIdList = (SalesIDCollection)Session["salesIdList"];
            if (salesIdList == null) salesIdList = new SalesIDCollection();


            int i = 0;
            while (i < releasedCollection.Count)
            {
                SalesID salesId = releasedCollection[i];
                if (!salesId.checkAllReleased(infojet))
                {
                    releasedCollection.Remove(salesId);
                    i--;
                }
                else
                {
                    if (!salesId.checkShowCaseCalculationMethod())
                    {
                        releasedCollection.Remove(salesId);
                        i--;
                    }
                    else
                    {
                        if (salesIdList.Contains(salesId)) salesId.setSelected(true);
                    }
                }

                i++;
            }
            i = 0;
            while (i < openCollection.Count)
            {
                SalesID salesId = openCollection[i];
                if (salesId.checkAllReleased(infojet))
                {
                    if (salesId.checkShowCaseCalculationMethod())
                    {
                        //throw new Exception("Hepp: "+salesId.code);
                        openCollection.Remove(salesId);
                        i--;
                    }
                    else
                    {
                        salesId.salesConcept = infojet.translate("NO SHOWCASE METHOD");
                        openCollection[i] = salesId;
                    }
                }
                else
                {
                    salesId.salesConcept = infojet.translate("NOT RELEASED");
                    openCollection[i] = salesId;
                }

                i++;
            }

            prevWebPageUrl = infojet.webSite.getAuthenticatedStartPageUrl();
            sendUrl = infojet.webPage.getUrl() + "&command=next";

            releasedSalesIdRepeater.DataSource = releasedCollection;
            releasedSalesIdRepeater.DataBind();

            openSalesIdRepeater.DataSource = openCollection;
            openSalesIdRepeater.DataBind();

            if (Request["command"] == "next")
            {
                salesIdList = new SalesIDCollection();

                i = 0;
                while (i < releasedCollection.Count)
                {
                    SalesID salesId = releasedCollection[i];

                    if (Request[salesId.code] == "on") salesIdList.Add(salesId);

                    i++;
                }

                Session["salesIdList"] = salesIdList;

                if (salesIdList.Count > 0)
                {
                    WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders2);
                    infojet.redirect(nextWebPage.getUrl());
                }
                else
                {
                    infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());
                }

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