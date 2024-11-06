﻿using System;
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
    public partial class ProductDetailsModel_MATRIX : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        private WebModel webModel;
        private WebItemList webItemList;
        protected WebImage productWebImage;
        protected ProductImageCollection productWebImages;


        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            webItemList = new WebItemList(infojet, webPageLine.code);

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

            if ((infojet.webSite.showInventoryAs != 2) && (infojet.webSite.showInventoryAs != 4))
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
            
            int i = 0;
            while (i < matrixBodyRepeater.Items.Count)
            {
                RepeaterItem vertComponent = matrixBodyRepeater.Items[i];

                Repeater horizRepeater = (Repeater)vertComponent.FindControl("matrixCellRepeater");
                int j = 0;
                while (j < horizRepeater.Items.Count)
                {
                    
                    RepeaterItem horizComponent = horizRepeater.Items[j];

                    HiddenField itemNoField = (HiddenField)horizComponent.FindControl("itemNo");
                    HiddenField itemVariantCodeField = (HiddenField)horizComponent.FindControl("itemVariantCode");

                    

                    TextBox matrixQuantityBox = (TextBox)horizComponent.FindControl("matrixQuantityBox");
                    int quantity = 0;
                    try
                    {
                        quantity = int.Parse(matrixQuantityBox.Text);
                    }
                    catch (Exception) { }
                    if (quantity > 0)
                    {
                        infojet.cartHandler.addItemToCart(itemNoField.Value, matrixQuantityBox.Text, false, itemVariantCodeField.Value, "", "", "", "");
                    }

                    matrixQuantityBox.Text = "";

                    j++;
                }
            
                i++;
            }

            infojet.redirect(infojet.webPage.getUrl() + "&webModelNo=" + webItemList.getRequestedWebModelNo());
        }

        private void updateView()
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            matrixHeaderRepeater.DataSource = webModel.getHorizDimensionValues();
            matrixHeaderRepeater.DataBind();

            matrixBodyRepeater.DataSource = webModel.getMatrixDimensionValues();
            matrixBodyRepeater.DataBind();

            if (webItemList.showInventory || webItemList.loggedInShowInventory)
            {
                itemInfoRepeater.DataSource = webModel.getModelVariantInfo(infojet.webSite.locationCode, (webItemList.itemListFilterForm.showUnitPrice || webItemList.itemListFilterForm.showUnitListPrice), webItemList.itemListFilterForm.showInventory);
                itemInfoRepeater.DataBind();
            }
            //setDimensionValues();

            ProductItem productItem = webModel.getProductItem(infojet, webPageLine);

            WebModelTranslation webModelTranslation = webModel.getTranslation(infojet.languageCode);
            productHeader.Text = webModelTranslation.description;

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
            productNoLabel.Text = infojet.translate("MODEL NO");
            inventoryLabel.Text = infojet.translate("INVENTORY STATUS");
            leadTimeLabel.Text = infojet.translate(infojet.webSite.leadTimeTextConstantCode);
            unitListPriceLabel.Text = infojet.translate("UNIT LIST PRICE");
            quantityLabel.Text = infojet.translate("QUANTITY");
            buyButton.Text = infojet.translate("BUY");

            manufacturer.Text = productItem.item.manufacturerCode;
            productNo.Text = webModel.no;
            inventory.Text = productItem.inventoryText;
            unitListPrice.Text = productItem.formatedUnitListPrice;
            leadTime.Text = productItem.item.leadTimeCalculation;
            unitPrice.Text = productItem.formatedUnitPrice;
            productDescription.Text = productItem.extendedText;

            itemAttributeRepeater.DataSource = productItem.itemAttributes;
            itemAttributeRepeater.DataBind();

            productItem.productImages.setSize(50, 50);
            productWebImages = productItem.productImages;
            //productImageRepeater.DataSource = productItem.productImages;
            //productImageRepeater.DataBind();

        }

        private void setDimensionValues()
        {
            RepeaterItemCollection repeaterItemCol = matrixBodyRepeater.Items;

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

        protected void matrixCellRepeater_ItemCreated(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            WebModelDimValue webModelDimValue = (Navipro.Infojet.Lib.WebModelDimValue)e.Item.DataItem;
            if (webModelDimValue != null)
            {
                TextBox qtyBox = (TextBox)e.Item.FindControl("matrixQuantityBox");
                if (qtyBox != null)
                {
                    qtyBox.Attributes.Add("onFocus", "showInventory('" + webModelDimValue.itemNo + "')");
                }
            }
        }

    }
}