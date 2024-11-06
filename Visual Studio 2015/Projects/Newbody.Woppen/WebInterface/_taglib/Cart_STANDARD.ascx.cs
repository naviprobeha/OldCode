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
    public partial class Cart_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PreRender += new EventHandler(Cart_STANDARD_PreRender);
        }

        void Cart_STANDARD_PreRender(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebCart webCart = new WebCart(infojet, webPageLine.code);

            cartLabel.Text = infojet.translate("CART");
            totalLabel.Text = infojet.translate("TOTAL");
            clearButton.Text = infojet.translate("CLEAR");
            checkOutButton.Text = infojet.translate("CHECKOUT");

            CartItemCollection cartItemCollection = webCart.getCartLines(webPageLine);
            cartItemCollection.setTextLength(10);
            cartList.DataSource = cartItemCollection;
            cartList.DataBind();

            total.Text = webCart.getFormatedTotalCartAmount();

            clearButton.Visible = webCart.showClearButton;
            cartPanel.Visible = webCart.showCart;

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

        protected void clearButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            infojet.cartHandler.emptyCart();
        }
    }
}