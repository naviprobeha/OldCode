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
    public partial class UserDefined_SPSUB : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected string contactPageUrl;
        protected string contactPageTooltip;
        protected string contactPageText;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected Navipro.Newbody.Woppen.Library.SalesID salesId;
        protected WebUserAccount salesPersonWebUserAccount;

        protected void Page_Load(object sender, EventArgs e)
        {

            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDs salesIds = new SalesIDs();
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);
                if (salesId.contactWebUserAccountNo != infojet.userSession.webUserAccount.no) Response.End();
            }
            else
            {
                salesId = salesIds.getSalesPersonSalesId(infojet, infojet.userSession.webUserAccount);
                if (salesId == null)
                {
                    salesId = salesIds.getUserRegSalesId(infojet, infojet.userSession.webUserAccount);
                    if (salesId == null)
                    {
                        Response.End();
                    }
                }

            }

            salesPersonWebUserAccount = infojet.userSession.webUserAccount;
            if ((Request["salesPersonWebUserAccountNo"] != null) && (Request["salesPersonWebUserAccountNo"] != ""))
            {
                if (infojet.userSession.webUserAccount.no == salesId.contactWebUserAccountNo)
                {
                    salesPersonWebUserAccount = new WebUserAccount(infojet.systemDatabase, Request["salesPersonWebUserAccountNo"]);
                }
                else
                {
                    Response.End();
                }
            }

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            WebPage contactPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeContact);
            contactPageUrl = contactPage.getUrl();
            contactPageTooltip = infojet.translate("CONTACT CP TEXT");
            contactPageText = infojet.translate("CONTACT") +" "+ salesId.getContact().name;

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}