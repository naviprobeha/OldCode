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
    public partial class UserDefined_SALESIDUSERS : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected string customerNoFilter;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            SalesIDs salesIds = new SalesIDs();
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            if (Request["scommand"] == "login")
            {
                WebSite webSite = new WebSite(infojet, "NEWBODY");
                infojet.redirect(webSite.location+"adminLogin.aspx?pageCode=STARTPAGE&scommand=login&userNo="+Request["userNo"]);
            }

            searchButton.Text = infojet.translate("SEARCH");
            searchButton.Click += new EventHandler(searchButton_Click);

            //SalesIDCollection salesIdCollection = salesIds.searchSalesIdCollection(infojet, "");

            //salesIdRepeater.DataSource = salesIdCollection;
            //salesIdRepeater.DataBind();

        }

        void searchButton_Click(object sender, EventArgs e)
        {
            SalesIDs salesIds = new SalesIDs();
            SalesIDCollection salesIdCollection = salesIds.searchSalesIdCollection(infojet, searchCustomerNo.Text);



            salesIdRepeater.DataSource = salesIdCollection;
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