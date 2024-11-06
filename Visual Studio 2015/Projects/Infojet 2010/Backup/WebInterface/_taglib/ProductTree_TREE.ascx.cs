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
    public partial class ProductTree_TREE : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            WebItemCategory webItemCategory = new WebItemCategory(infojet, webPageLine.code);

            productTree.DataSource = webItemCategory.getProductCategoryTree();
            productTree.DataBind();

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

        protected void productTree_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
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

        }
    }
}