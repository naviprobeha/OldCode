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
    public partial class UserDefined_REORDER : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected string prevWebPageUrl;
        protected string nextWebPageUrl;
        protected string sendUrl;


        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            SalesIDs salesIds = new SalesIDs();

            WebPage prevWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
            prevWebPageUrl = prevWebPage.getUrl();

            WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeShowCase);
            nextWebPageUrl = nextWebPage.getUrl();


            SalesID salesId = (SalesID)Session["currentSalesId"];
            if (salesId.nextOrderType > 2)
            {
                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders2);
                infojet.redirect(webPage.getUrl());
            }

            sendUrl = infojet.webPage.getUrl() + "&command=applyReorderSetup";


            if (Request["command"] == "applyReorderSetup")
            {
                OrderItemCollection orderItemCollection = new OrderItemCollection();

                if (Session["currentSalesId"] != null)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.salesId = salesId.code;
                    orderItem.method = int.Parse(Request["allowReOrder"]);
                    orderItemCollection.Add(orderItem);

                    orderItemCollection.setSalesId(salesId.code);

                    ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "reorderSetup", orderItemCollection));
                    if (!appServerConnection.processRequest())
                    {
                        errorMessageLabel.Text = appServerConnection.getLastError();
                    }
                    else
                    {
                        infojet.redirect(nextWebPageUrl);
                    }
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