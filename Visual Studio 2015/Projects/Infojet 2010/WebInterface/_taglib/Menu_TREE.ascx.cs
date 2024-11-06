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
    public partial class Menu_TREE : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            WebMenu webMenu = new WebMenu(infojet, webPageLine.code);

            menuTree.DataSource = webMenu.getMenuItems(infojet);
            menuTree.DataBind();

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine; 
        }

        #endregion

        protected void menuTree_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
        {
            if (((NavigationItem)e.Node.DataItem).selected)
            {
                e.Node.Selected = true;
                e.Node.Expand();
            }
            else
            {
                e.Node.Collapse();
            }

            if (((NavigationItem)e.Node.DataItem).windowMode == 1)
            {
                e.Node.Target = "_blank";
            }
        }
    }
}