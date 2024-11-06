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
    public partial class UserDefined_TOPLIST : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected SalesID salesId;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            SalesIDs salesIds = new SalesIDs();
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);
                if (salesId.contactWebUserAccountNo != infojet.userSession.webUserAccount.no) Response.End();

                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
            }
            else
            {
                salesId = salesIds.getSalesPersonSalesId(infojet, infojet.userSession.webUserAccount);
                if (salesId == null) Response.End();

            }

            salesPersonRepeater.DataSource = salesId.getTopList(infojet.userSession.webUserAccount.no);
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