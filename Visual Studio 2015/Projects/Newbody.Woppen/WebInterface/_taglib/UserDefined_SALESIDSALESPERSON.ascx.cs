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
    public partial class UserDefined_SALESIDSALESPERSON : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected bool allReleased;
        protected int countReleased;
        protected string sendOrderUrl;
        protected string nextWebPageUrl;
        protected string nextWebPageBtn;
        protected string nextWebPageText;
        protected bool allSalesPersonsRegistered;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDs salesIds = new SalesIDs();
            SalesID salesId = (SalesID)Session["currentSalesId"];
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);
                if (!salesId.isContactPerson(infojet.userSession.webUserAccount.no)) Response.End();
            }

            if (Session["currentSalesId"] == null)
            {
                Session.Add("currentSalesId", salesId);
            }
            else
            {
                Session["currentSalesId"] = salesId;
            }

            SalesIDSalesPersonCollection completeSalesIdSalesPersonCollection = salesId.getSalesPersons(infojet);
            SalesIDSalesPersonCollection salesIdSalesPersonCollection = salesId.getActiveSalesPersons(infojet);
            if (completeSalesIdSalesPersonCollection.Count == salesIdSalesPersonCollection.Count) allSalesPersonsRegistered = true;

           

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            WebPage salesPersonDetailWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeSalesPersonDetail);
            WebPage sendOrderWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeReOrders);

            salesIdSalesPersonCollection.setPageUrl(salesPersonDetailWebPage.getUrl());

            countReleased = salesId.countReleased(infojet);
            if (countReleased == salesIdSalesPersonCollection.Count) allReleased = true;
           
            
            sendOrderUrl = sendOrderWebPage.getUrl();

            SalesIDCollection salesIdCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);
            if ((salesId.showCase == "") || (salesId.nextOrderType > 2))
            {
                if (salesIdCollection.Count > 1) 
                {
                    nextWebPageUrl = infojet.webSite.getAuthenticatedStartPageUrl();
                    nextWebPageBtn = "IMG MY GROUPS BTN";
                }
                else
                {
                    SalesIDCollection salesIdList = new SalesIDCollection();
                    salesIdList.Add(salesId);
                    Session["salesIdList"] = salesIdList;

                    //WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders2);
                    WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeReOrders);
                    nextWebPageUrl = webPage.getUrl();
                    nextWebPageBtn = "IMG BIG ORDER BTN";
                }

                if (salesId.isSubContactPerson(infojet.userSession.webUserAccount.no))
                {
                    nextWebPageUrl = "";
                    nextWebPageBtn = "";
                }
            }
            else
            {
                if (salesIdCollection.Count > 1)
                {
                    //WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeShowCase);
                    WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeShowCase);
                    nextWebPageUrl = nextWebPage.getUrl();
                }
                else
                {
                    if (salesId.isSubContactPerson(infojet.userSession.webUserAccount.no))
                    {
                        //WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeShowCase);
                        WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeShowCase);
                        nextWebPageUrl = nextWebPage.getUrl();
                    }
                    else
                    {
                        //WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeShowCase);
                        WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeReOrders);
                        nextWebPageUrl = nextWebPage.getUrl();
                    }
                }

                if (salesId.nextOrderType < 3)
                {
                    if (salesIdCollection.Count > 1)
                    {
                        nextWebPageBtn = "IMG SHOWCASE BTN";
                    }
                    else
                    {
                        nextWebPageBtn = "IMG BIG ORDER BTN";
                        nextWebPageText = "BEGIN ORDER TEXT";
                    }
                }
                else
                {
                    nextWebPageBtn = "IMG BIG ORDER BTN";
                    nextWebPageText = "BEGIN ORDER TEXT";
                }

                if (salesId.isSubContactPerson(infojet.userSession.webUserAccount.no))
                {
                    nextWebPageBtn = "IMG SHOWCASE BTN";
                }

            }

            if (Request["command"] == "addNewSP")
            {

                SalesIDSalesPerson salesPerson = salesId.getFirstAvailableSalesPerson(infojet);
                if (salesPerson != null)
                {
                    WebUserAccount salesPersonWebUserAccount = new WebUserAccount(infojet.systemDatabase, salesPerson.webUserAccountNo);

                    ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "addSP", salesPersonWebUserAccount));
                    if (!appServerConnection.processRequest())
                    {
                        //errorMessageLabel.Text = appServerConnection.getLastError();
                    }

                    infojet.redirect(salesPersonDetailWebPage.getUrl() + "&salesId=" + salesId.code + "&salesPersonWebUserAccountNo=" + salesPerson.webUserAccountNo);
                    
                }
                
                infojet.redirect(infojet.webPage.getUrl()+"&salesId="+salesId.code);
            }


            salesPersonRepeater.DataSource = salesIdSalesPersonCollection;
            salesPersonRepeater.DataBind();
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;

        }

        #endregion
    }
}