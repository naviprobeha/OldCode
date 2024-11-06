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
using System.IO;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class WebUserControl1 : System.Web.UI.UserControl, Navipro.Infojet.Lib.InfojetUserControl    
    {
        private Navipro.Infojet.Lib.WebPageLine webPageLine; 

        protected void Page_Load(object sender, EventArgs e)
        {
            textLiteral.Text = webPageLine.getContent();
        }


        #region InfojetUserControl Members

        public void setWebPageLine(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}