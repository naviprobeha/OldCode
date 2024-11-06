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
    public partial class HistoryCrMemo_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected CustomerHistoryCrMemo customerHistoryCrMemo;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            CustomerHistory customerHistory = new CustomerHistory(infojet);
            customerHistoryCrMemo = customerHistory.getCrMemoInfo(customerHistory.getRequestedDocumentNo());

            if (customerHistoryCrMemo != null)
            {
                crMemoLineRepeater.DataSource = customerHistoryCrMemo.lines;
                crMemoLineRepeater.DataBind();
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