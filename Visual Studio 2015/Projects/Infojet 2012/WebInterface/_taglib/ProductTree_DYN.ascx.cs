using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class ProductTree_DYN : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine; 

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
    }
}