using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class SearchBox_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

        }


        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion


        protected void searchButton_Click(object sender, EventArgs e)
        {
            infojet.performSearch(searchQueryBox.Text);
            
        }
    }
}