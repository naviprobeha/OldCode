using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class GenericControl : System.Web.UI.UserControl, Navipro.Infojet.Lib.InfojetUserControl
    {
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected Navipro.Infojet.Lib.WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();
        }

        #region InfojetUserControl Members

        public void setWebPageLine(Navipro.Infojet.Lib.WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}