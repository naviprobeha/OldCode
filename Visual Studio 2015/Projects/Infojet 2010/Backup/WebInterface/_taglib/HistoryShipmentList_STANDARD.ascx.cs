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
    public partial class HistoryShipmentList_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            CustomerHistory customerHistory = new CustomerHistory(infojet);

            int pageNo = 1;
            if ((Request["pageNo"] != null) && (Request["pageNo"] != "")) pageNo = int.Parse(Request["pageNo"]);

            historyShipmentRepeater.DataSource = customerHistory.getShipmentHistory(pageNo);
            historyShipmentRepeater.DataBind();

            pageNav.Text = customerHistory.composePageNavigator();

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}