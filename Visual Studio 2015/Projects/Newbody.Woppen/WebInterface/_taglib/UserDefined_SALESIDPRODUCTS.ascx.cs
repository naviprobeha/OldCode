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
    public partial class UserDefined_SALESIDPRODUCTS : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected DataSet cartDataSet;
        protected SalesID salesId;
        protected string prevWebPageUrl;
        protected bool cartIsReleased;

        protected void Page_Load(object sender, EventArgs e)
        {
            prevWebPageUrl = "";
            infojet = new Navipro.Infojet.Lib.Infojet();
            WebCartLines webCartLines = new WebCartLines(infojet.systemDatabase);
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            if (infojet.userSession.webUserAccount.name == "")
            {
                WebPage profileWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeSalesPersonProfile);
                infojet.redirect(profileWebPage.getUrl());
            }

            SalesIDs salesIds = new SalesIDs();
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);
                if (salesId.contactWebUserAccountNo != infojet.userSession.webUserAccount.no) Response.End();

                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
                prevWebPageUrl = webPage.getUrl() + "&salesId=" + salesId.code;
            }
            else
            {
                salesId = salesIds.getSalesPersonSalesId(infojet, infojet.userSession.webUserAccount);
                if (salesId == null) Response.End();

                if (salesId.closingDate < DateTime.Today)
                {
                    productPanel.Visible = false;
                    messagePanel.Visible = true;
                    messageLabel.Text = infojet.translate("NO SALES ID FOUND");
                }

                if ((salesId.nextOrderType > 2) && (!salesId.additionalOrder)) //Order Sent
                //if (salesId.nextOrderType > 2) //Order Sent
                {
                    productPanel.Visible = false;
                    messagePanel.Visible = true;
                    messageLabel.Text = infojet.translate("ORDER DISALLOWED");
                }
            }

            if (salesId.checkPreliminaryCatalog())
            {
                WebPage prelInfoWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeSPPreSort);
                infojet.redirect(prelInfoWebPage.getUrl());
            }

            string webUserAccountNo = infojet.userSession.webUserAccount.no;
            if ((Request["salesPersonWebUserAccountNo"] != null) && (Request["salesPersonWebUserAccountNo"] != ""))
            {
                if (infojet.userSession.webUserAccount.no == salesId.contactWebUserAccountNo)
                {
                    webUserAccountNo = Request["salesPersonWebUserAccountNo"];
                }
                else
                {
                    Response.End();
                }
            }



            if (Request["cart_command"] == "deleteCartLine")
            {
                try
                {
                    int entryNo = int.Parse(Request["cart_entryNo"]);
                    WebCartLine webCartLine = new WebCartLine(infojet, entryNo);
                    if (webCartLine != null)
                    {
                        webCartLine.delete();
                    }
                }
                catch (Exception)
                {
                }

            }

            if (Request["cart_command"] == "updateCartLine")
            {
                try
                {
                    float quantity = 0;
                    int entryNo = int.Parse(Request["cart_entryNo"]);
                    if (Request["cartQuantity_" + entryNo] != "") quantity = float.Parse(Request["cartQuantity_" + entryNo]);

                    WebCartLine webCartLine = new WebCartLine(infojet, entryNo);
                    if (webCartLine != null)
                    {
                        if (quantity > 0)
                        {
                            webCartLine.quantity = quantity;
                            webCartLine.save();
                        }
                        else
                        {
                            webCartLine.delete();
                        }
                    }
                }
                catch (Exception) { }

            }

            if (Request["cart_command"] == "releaseCart")
            {
                Navipro.Infojet.Lib.DatabaseQuery databaseQuery = infojet.systemDatabase.prepare("UPDATE ["+infojet.systemDatabase.getTableName("Web Cart Line")+"] SET [Extra 3] = '1' WHERE [Web User Account No] = @webUserAccountNo AND [Extra 2] = @salesId");
                databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
                databaseQuery.addStringParameter("@salesId", salesId.code, 20);
                databaseQuery.execute();
            }


            
            
            cartDataSet = salesId.getCartLines(webUserAccountNo);
            if (infojet.userSession.webUserAccount.no != salesId.contactWebUserAccountNo) cartIsReleased = salesId.checkReleasedCart(webUserAccountNo);

            if (!productPanel.Visible) cartDataSet = salesId.getSentCartLines(webUserAccountNo);

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

    }
}