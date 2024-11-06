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
    public partial class ProductCategory_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebItemCategory webItemCategory = new WebItemCategory(infojet);

            webItemCategory.doSecurityCheck();

            categoryName.Text = webItemCategory.getTranslation().description;
            categoryDescription.Text = webItemCategory.getDescription();

            WebImage webImage = webItemCategory.getTranslation().webImage;
            categoryImage.Visible = false;

            if ((webImage != null) && (webImage.code != null))
            {
                categoryImage.ImageUrl = webImage.getUrl(250, 250);
                categoryImage.Visible = true;
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