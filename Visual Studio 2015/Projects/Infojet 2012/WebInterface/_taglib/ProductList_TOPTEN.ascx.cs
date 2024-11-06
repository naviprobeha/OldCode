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
    public partial class ProductList_TOPTEN : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        protected ProductItemCollection productItemCollection;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            WebItemList webItemList = new WebItemList(infojet, webPageLine.code);
            productItemCollection = webItemList.getTopTenList(infojet, webPageLine);

            productItemCollection.setSize(150, 150);
            productItemCollection.setExtendedTextLength(100);
            productItemCollection.setNoImageUrl("../../_assets/img/no_image_150_150.jpg");

            itemRepeater.DataSource = productItemCollection;
            itemRepeater.DataBind();
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}