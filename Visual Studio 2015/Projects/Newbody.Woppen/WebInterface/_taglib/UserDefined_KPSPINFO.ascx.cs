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
    public partial class UserDefined_KPSPINFO : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected WebUserAccount salesPersonWebUserAccount;
        protected SalesID salesId;
        protected int noOfContactSalesIds;
        protected bool allowDelete;

        protected void Page_Load(object sender, EventArgs e)
        {
 
            infojet = new Navipro.Infojet.Lib.Infojet();
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            SalesIDs salesIds = new SalesIDs();
            salesId = null;
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);
                if (!salesId.isContactPerson(infojet.userSession.webUserAccount.no)) Response.End();

                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
            }
            else
            {
                salesId = salesIds.getSalesPersonSalesId(infojet, infojet.userSession.webUserAccount);
                if (salesId == null) Response.End();

            }

            if (!salesId.isContactPerson(infojet.userSession.webUserAccount.no)) Response.End();

            string webUserAccountNo = infojet.userSession.webUserAccount.no;
            if ((Request["salesPersonWebUserAccountNo"] != null) && (Request["salesPersonWebUserAccountNo"] != ""))
            {
                if (salesId.isContactPerson(infojet.userSession.webUserAccount.no))
                {
                    webUserAccountNo = Request["salesPersonWebUserAccountNo"];
                }
                else
                {
                    Response.End();
                }
            }

            SalesIDCollection salesIdCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);
            noOfContactSalesIds = salesIdCollection.Count;

            salesPersonWebUserAccount = new WebUserAccount(infojet.systemDatabase, webUserAccountNo);

            if (Request["infoCommand"] == "updateSPName")
            {
                salesPersonWebUserAccount.name = Request["salesPersonName"];

                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "updateSPName", salesPersonWebUserAccount));
                if (!appServerConnection.processRequest())
                {
                    //errorMessageLabel.Text = appServerConnection.getLastError();
                }

                infojet.redirect(infojet.webPage.getUrl() + "&salesId="+salesId.code+"&salesPersonWebUserAccountNo="+webUserAccountNo);
            }

            if (Request["infoCommand"] == "deleteSP")
            {
                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "deleteSP", salesPersonWebUserAccount));
                if (!appServerConnection.processRequest())
                {
                    //errorMessageLabel.Text = appServerConnection.getLastError();
                }

                salesPersonWebUserAccount = new WebUserAccount(infojet.systemDatabase, webUserAccountNo);

                WebPage combinedWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
                infojet.redirect(combinedWebPage.getUrl() + "&salesId=" + salesId.code);

            }

            if (Request["infoCommand"] == "promoteSP")
            {
                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "promoteSP", salesPersonWebUserAccount));
                if (!appServerConnection.processRequest())
                {
                    //errorMessageLabel.Text = appServerConnection.getLastError();
                }

                salesPersonWebUserAccount = new WebUserAccount(infojet.systemDatabase, webUserAccountNo);
                salesId = new SalesID(infojet, salesId.code);
            }

            if (Request["infoCommand"] == "demoteSP")
            {
                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "demoteSP", salesPersonWebUserAccount));
                if (!appServerConnection.processRequest())
                {
                    //errorMessageLabel.Text = appServerConnection.getLastError();
                }

                salesPersonWebUserAccount = new WebUserAccount(infojet.systemDatabase, webUserAccountNo);
                salesId = new SalesID(infojet, salesId.code);

            }

            DataSet cartDataSet = salesId.getCartLines(salesPersonWebUserAccount.no);
            if (cartDataSet.Tables[0].Rows.Count == 0) allowDelete = true;


        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

    }
}