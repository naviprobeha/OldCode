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


namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class UserDefined_ORDERRESPONSE : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected string orderConfirmationPdfUrl;
        protected string pickListPdfUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            if ((Request["orderNo"] == null) || (Request["orderNo"] == ""))
            {
                infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());    
            }

            orderConfirmationPdfUrl = infojet.webPage.getUrl() + "&orderNo=" + Request["orderNo"] + "&docType=0&pdf=true";
            pickListPdfUrl = infojet.webPage.getUrl() + "&orderNo=" + Request["orderNo"] + "&docType=4&pdf=true";

            if (Request["pdf"] == "true")
            {
                CustomerHistory customerHistory = new CustomerHistory(infojet);
                customerHistory.requestPdfDocument(int.Parse(Request["docType"]), Request["orderNo"]);
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