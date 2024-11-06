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
    public partial class UserDefined_INFOSHOWCASE : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected float grandTotalQuantity;
        protected float grandTotalAmount;
        protected float totalProfit;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            SalesIDs salesIds = new SalesIDs();

            SalesIDCollection salesIdList = null;
            if (Request["salesId"] == "ALL")
            {
                salesIdList = salesIds.getSalesIdsPerStartDate(infojet, DateTime.Parse(Request["startDate"]));
            }
            else
            {
                salesIdList = new SalesIDCollection();
                SalesID salesId = new SalesID(infojet, Request["salesId"]);
                salesIdList.Add(salesId);
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