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
    public partial class Campain_WIDE : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected WebItemCampain webItemCampain;

        protected void Page_Load(object sender, EventArgs e)
        {

            infojet = new Navipro.Infojet.Lib.Infojet();

            webItemCampain = new WebItemCampain(infojet, webPageLine.code);

            campainRepeater.DataSource = webItemCampain.getItemCampains(infojet, webPageLine);
            campainRepeater.DataBind();

            
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;    
        }

        #endregion
    }
}