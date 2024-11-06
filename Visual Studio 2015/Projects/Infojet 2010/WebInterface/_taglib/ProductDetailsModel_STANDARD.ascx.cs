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
    public partial class ProductDetailsModel_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        private WebModel webModel;
        protected WebImage productWebImage;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebItemList webItemList = new WebItemList(infojet, webPageLine.code);

            webModel = new WebModel(infojet, webItemList.getRequestedWebModelNo());

            updateView();

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

            if (infojet.userSession != null)
            {
                buyPanel.Visible = true;
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

        private void updateView()
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            dimensionRepeater.DataSource = webModel.getDimensions();
            dimensionRepeater.DataBind();

            setDimensionValues();

            WebModelVariant webModelVariant = webModel.getVariant();

            string itemNo = "";
            if (!this.IsPostBack) itemNo = Request["itemNo"];

            if ((itemNo == "") || (itemNo == null))
            {
                if (webModelVariant != null) itemNo = webModelVariant.itemNo;
            }

            Item item = new Item(infojet.systemDatabase, itemNo);

            ProductItem productItem = item.getProductItem(infojet, webPageLine);

            productHeader.Text = productItem.description;

            if (productItem.productImage != null)
            {
                productWebImage = productItem.productImage.webItemImage.image;
                noProductImage.Visible = false;
                productImage.Visible = true;
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

        }

        private void setDimensionValues()
        {
            RepeaterItemCollection repeaterItemCol = dimensionRepeater.Items;

            int i = 0;
            while (i < repeaterItemCol.Count)
            {
                HiddenField dimCodeField = (HiddenField)repeaterItemCol[i].FindControl("code");
                DropDownList dropDownList = (DropDownList)repeaterItemCol[i].FindControl("dimensionDropDown");

                dropDownList.Text = webModel.getDimensionValue(dimCodeField.Value);

                i++;
            }
        }

        protected void dimensionDropDown_selectedIndexChanged(object sender, EventArgs e)
        {
            HiddenField code = (HiddenField)((DropDownList)sender).Parent.FindControl("code");
            DropDownList dimensionDropDown = ((DropDownList)sender);

            webModel.setDimension(code.Value, dimensionDropDown.Text);

            updateView();
        }
    }
}