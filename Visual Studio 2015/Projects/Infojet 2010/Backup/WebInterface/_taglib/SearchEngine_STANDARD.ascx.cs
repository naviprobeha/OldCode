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
    public partial class SearchEngine_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            string searchQuery = System.Web.HttpContext.Current.Request["searchQuery"];

            DefaultSearchEngine searchEngine = new DefaultSearchEngine();

            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();
            navigationItemCollection = searchEngine.getAllSearchResults(navigationItemCollection, infojet, searchQuery);

            searchResultRepeater.DataSource = navigationItemCollection;
            searchResultRepeater.DataBind();
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion




    }
}