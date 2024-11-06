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
    public partial class ProductDetails_STANDARD : System.Web.UI.UserControl, InfojetUserControl    
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            Item item = new Item(infojet.systemDatabase, Request["itemNo"]);

            WebItemList webItemList = new WebItemList(infojet, webPageLine.code);

            ProductItem productItem = item.getProductItem(infojet, webPageLine);
            productHeader.Text = productItem.description;

            if (productItem.productImage != null)
            {
                productImage.ImageUrl = productItem.productImage.webItemImage.image.getUrl(250, 250);
                noProductImage.Visible = false;
            }
            else
            {
                noProductImage.Visible = true;
                productImage.Visible = false;
            }

            manufacturerLabel.Text = infojet.translate("MANUFACTURER");
            productNoLabel.Text = infojet.translate("ITEM NO");
            inventoryLabel.Text = infojet.translate("INVENTORY STATUS");
            leadTimeLabel.Text = infojet.translate(infojet.webSite.leadTimeTextConstantCode);
            unitListPriceLabel.Text = infojet.translate("UNIT LIST PRICE");
            quantityLabel.Text = infojet.translate("QUANTITY");
            buyButton.Text = infojet.translate("BUY");
            quantityBox.Text = "1";
            
            manufacturer.Text = productItem.item.manufacturerCode;
            productNo.Text = productItem.no;
            inventory.Text = productItem.inventoryText;
            unitListPrice.Text = productItem.formatedUnitListPrice;
            leadTime.Text = productItem.item.leadTimeCalculation;
            unitPrice.Text = productItem.formatedUnitPrice;
            productDescription.Text = productItem.extendedText;

            itemAttributeRepeater.DataSource = productItem.itemAttributes;
            itemAttributeRepeater.DataBind();

            productItem.productImages.setSize(50, 50);
            productImageRepeater.DataSource = productItem.productImages;
            productImageRepeater.DataBind();

            if (!webItemList.checkShowInventory(infojet))
            {
                inventoryLabel.Visible = false;
                inventory.Visible = false;
                leadTimeLabel.Visible = false;
                leadTime.Visible = false;
            }

            if (!webItemList.checkShowManufacturer(infojet))
            {
                manufacturer.Visible = false;
                manufacturerLabel.Visible = false;
            }

            if (!webItemList.checkShowRecPrice(infojet))
            {
                unitListPrice.Visible = false;
                unitListPriceLabel.Visible = false;
            }

            if (infojet.webSite.showInventoryAs != 2)
            {
                leadTime.Visible = false;
                leadTimeLabel.Visible = false;
            }

            if (infojet.webSite.allowPurchaseNotLoggedIn)
            {
                buyPanel.Visible = true;
            }
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

        protected void buyButton_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            infojet.cartHandler.addItemToCart(Request["itemNo"], quantityBox.Text, false, "", "", "", "", "");
        }
    }
}