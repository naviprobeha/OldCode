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
    public partial class UserDefined_KPSALESIDPRODUCTS : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected DataSet cartDataSet;
        protected SalesID salesId;
        protected string prevWebPageUrl;
        protected bool cartIsReleased;
        protected WebUserAccount salesPersonWebUserAccount;

        protected void Page_Load(object sender, EventArgs e)
        {
            prevWebPageUrl = "";
            infojet = new Navipro.Infojet.Lib.Infojet();
            WebCartLines webCartLines = new WebCartLines(infojet.systemDatabase);
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            SalesIDs salesIds = new SalesIDs();
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);

                if (!salesId.isContactPerson(infojet.userSession.webUserAccount.no)) Response.End();

                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
                prevWebPageUrl = webPage.getUrl() + "&salesId=" + salesId.code;
            }
            else
            {

                salesId = salesIds.getSalesPersonSalesId(infojet, infojet.userSession.webUserAccount);
                if (salesId == null) Response.End();

            }


            if (!salesId.isContactPerson(infojet.userSession.webUserAccount.no)) Response.End();

            if ((Request["salesPersonWebUserAccountNo"] == null) || (Request["salesPersonWebUserAccountNo"] == ""))
            {
                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
                infojet.redirect(webPage.getUrl() + "&salesId=" + salesId.code);
            }
            

            string webUserAccountNo = infojet.userSession.webUserAccount.no;
            if ((Request["salesPersonWebUserAccountNo"] != null) && (Request["salesPersonWebUserAccountNo"] != ""))
            {
                if (salesId.isContactPerson(infojet.userSession.webUserAccount.no))
                {
                    webUserAccountNo = Request["salesPersonWebUserAccountNo"];
                    salesPersonWebUserAccount = new WebUserAccount(infojet.systemDatabase, webUserAccountNo);
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
                        if (webCartLine.extra2 == salesId.code) webCartLine.delete();
                    }
                }
                catch (Exception)
                {
                }

                infojet.redirect(infojet.webPage.getUrl() + "&salesId=" + salesId.code + "&salesPersonWebUserAccountNo=" + webUserAccountNo);
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
                catch (Exception) {}

                infojet.redirect(infojet.webPage.getUrl() + "&salesId=" + salesId.code + "&salesPersonWebUserAccountNo=" + webUserAccountNo);

            }

            if (Request["cart_command"] == "releaseCart")
            {
                Navipro.Infojet.Lib.DatabaseQuery databaseQuery = infojet.systemDatabase.prepare("UPDATE [" + infojet.systemDatabase.getTableName("Web Cart Line") + "] SET [Extra 3] = '1' WHERE [Web User Account No] = @webUserAccountNo AND [Extra 2] = @salesId");
                databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
                databaseQuery.addStringParameter("@salesId", salesId.code, 20);
                databaseQuery.execute();

                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);

                
                infojet.redirect(webPage.getUrl() + "&salesId=" + salesId.code);
                return;
            }

            if (Request["cart_command"] == "openCart")
            {
                Navipro.Infojet.Lib.DatabaseQuery databaseQuery = infojet.systemDatabase.prepare("UPDATE [" + infojet.systemDatabase.getTableName("Web Cart Line") + "] SET [Extra 3] = '0' WHERE [Web User Account No] = @webUserAccountNo AND [Extra 2] = @salesId");
                databaseQuery.addStringParameter("@webUserAccountNo", webUserAccountNo, 20);
                databaseQuery.addStringParameter("@salesId", salesId.code, 20);
                databaseQuery.execute();
            }


            cartDataSet = salesId.getCartLines(webUserAccountNo);
            cartIsReleased = salesId.checkReleasedCart(webUserAccountNo);



        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

    }
}