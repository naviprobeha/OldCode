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
    public partial class UserDefined_SENDORDERS_SHOWCASE : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected string prevWebPageUrl;
        protected string sendUrl;



        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            SalesIDs salesIds = new SalesIDs();

            WebPage prevWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders1);
            prevWebPageUrl = prevWebPage.getUrl();
            sendUrl = infojet.webPage.getUrl() + "&command=submitShowcase";

            SalesIDCollection salesIdList = (SalesIDCollection)Session["salesIdList"];

            if (Request["command"] == "submitShowcase")
            {
                bool gotoNext = true;
                int h = 0;
                while (h < salesIdList.Count)
                {
                    SalesID salesId = salesIdList[h];
                    OrderItemCollection soldShowCaseItems = salesId.soldShowCaseItems;

                    int i = 0;
                    while (i < soldShowCaseItems.Count)
                    {
                        OrderItem orderItem = soldShowCaseItems[i];
                        try
                        {
                            if (Request[salesId.code + "_realQuantity_" + orderItem.itemNo] == "") orderItem.remainingQuantity = 0;
                            if (Request[salesId.code + "_realQuantity_" + orderItem.itemNo] != "") orderItem.remainingQuantity = float.Parse(Request[salesId.code + "_realQuantity_" + orderItem.itemNo]);
                        }
                        catch (Exception) { }

                        try
                        {
                            if (Request[salesId.code + "_qtyPackMtrl_" + orderItem.itemNo] == "") orderItem.quantity2 = 0;
                            if (Request[salesId.code + "_qtyPackMtrl_" + orderItem.itemNo] != "") orderItem.quantity2 = float.Parse(Request[salesId.code + "_qtyPackMtrl_" + orderItem.itemNo]);
                        }
                        catch (Exception) {}

                        try
                        {
                            if (Request[salesId.code + "_qtyPackSlips_" + orderItem.itemNo] == "") orderItem.quantity3 = 0;
                            if (Request[salesId.code+"_qtyPackSlips_" + orderItem.itemNo] != "") orderItem.quantity3 = float.Parse(Request[salesId.code+"_qtyPackSlips_" + orderItem.itemNo]);
                        }
                        catch (Exception) {}

                        soldShowCaseItems[i] = orderItem;

                        i++;
                    }

                    soldShowCaseItems.setSalesId(salesId.code);

                    ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "updateShowCase", soldShowCaseItems));
                    if (!appServerConnection.processRequest())
                    {
                        errorMessageLabel.Text = appServerConnection.getLastError();
                        gotoNext = false;
                    }

                    h++;
                }

                if (gotoNext)
                {
                    WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders3);
                    infojet.redirect(webPage.getUrl());
                }

            }

            salesIdRepeater.DataSource = salesIdList;
            salesIdRepeater.DataBind();

         }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;

        }

        #endregion
    }
}