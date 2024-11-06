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
    public partial class UserDefined_SHOWCASE : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected string prevWebPageUrl;
        protected string sendUrl;
        protected string nextBtn;

        protected string method1Check;
        protected string method2Check;



        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            SalesIDs salesIds = new SalesIDs();
            SalesID salesId = null;
            if (Session["currentSalesId"] != null)
            {
                salesId = (SalesID)Session["currentSalesId"];
                if (!salesId.isContactPerson(infojet.userSession.webUserAccount.no)) Response.End();

                WebPage prevWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeReOrders);
                prevWebPageUrl = prevWebPage.getUrl();
                sendUrl = infojet.webPage.getUrl() + "&command=submitShowcase";

                SalesIDCollection salesIdCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);
                if ((salesId.showCase == "") || (salesId.nextOrderType >= 3))
                {
                    if (salesIdCollection.Count > 1)
                    {
                        infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());
                    }
                    else
                    {
                        SalesIDCollection salesIdList = new SalesIDCollection();
                        salesIdList.Add(salesId);
                        Session["salesIdList"] = salesIdList;

                        WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders2);
                        infojet.redirect(webPage.getUrl());
                    }
                }
                else
                {
                    if (salesIdCollection.Count > 1)
                    {
                        prevWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
                        prevWebPageUrl = prevWebPage.getUrl();
                        nextBtn = "IMG MY GROUPS BTN";
                    }
                    else
                    {
                        prevWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeReOrders);
                        prevWebPageUrl = prevWebPage.getUrl();
                        nextBtn = "IMG NEXT BTN";
                    }
                }

            }

            OrderItemCollection soldShowCaseItems = salesId.getShowCaseItemForReporting();
            
            if (soldShowCaseItems.Count > 0)
            {
                if (soldShowCaseItems[0].method == 1) method1Check = "checked=\"checked\"";
                if (soldShowCaseItems[0].method == 2) method2Check = "checked=\"checked\"";
            }


            if (Request["command"] == "submitShowcase")
            {
                bool cancelUpdate = false;

                int i = 0;
                while (i < soldShowCaseItems.Count)
                {
                    OrderItem orderItem = soldShowCaseItems[i];
                    try
                    {
                        if (Request["realQuantity" + orderItem.itemNo] == "") orderItem.remainingQuantity = 0;
                        if (Request["realQuantity" + orderItem.itemNo] != "") orderItem.remainingQuantity = float.Parse(Request["realQuantity" + orderItem.itemNo]);
                    }
                    catch (Exception ex) { cancelUpdate = true; }

                    try
                    {
                        if (Request["qtyPackMtrl" + orderItem.itemNo] == "") orderItem.quantity2 = 0;
                        if (Request["qtyPackMtrl" + orderItem.itemNo] != "") orderItem.quantity2 = float.Parse(Request["qtyPackMtrl" + orderItem.itemNo]);
                    }
                    catch (Exception ex) { cancelUpdate = true; }

                    try
                    {
                        if (Request["qtyPackSlips" + orderItem.itemNo] == "") orderItem.quantity3 = 0;
                        if (Request["qtyPackSlips" + orderItem.itemNo] != "") orderItem.quantity3 = float.Parse(Request["qtyPackSlips" + orderItem.itemNo]);
                    }
                    catch (Exception ex) { cancelUpdate = true; }

                    orderItem.method = int.Parse(Request["showCaseCalculationMethod"]);
                    soldShowCaseItems[i] = orderItem;

                    i++;
                }

                soldShowCaseItems.setSalesId(salesId.code);

                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "updateShowCase", soldShowCaseItems));
                if (!appServerConnection.processRequest())
                {
                    errorMessageLabel.Text = appServerConnection.getLastError();
                }
                else
                {
                    if (!cancelUpdate)
                    {
                        SalesIDCollection salesIdCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);
                        if (salesIdCollection.Count > 1)
                        {
                            infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());
                        }
                        else
                        {
                            if (salesId.subContWebUserAccountNo == infojet.userSession.webUserAccount.no)
                            {
                                infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());
                            }
                            else
                            {
                                SalesIDCollection salesIdList = new SalesIDCollection();
                                salesIdList.Add(salesId);
                                Session["salesIdList"] = salesIdList;

                                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders2);
                                infojet.redirect(webPage.getUrl());
                            }
                        }

                    }
                }

            }

            cartItemRepeater.DataSource = soldShowCaseItems;
            cartItemRepeater.DataBind();

            quantityRepeater.DataSource = soldShowCaseItems;
            quantityRepeater.DataBind();

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;

        }

        #endregion
    }
}