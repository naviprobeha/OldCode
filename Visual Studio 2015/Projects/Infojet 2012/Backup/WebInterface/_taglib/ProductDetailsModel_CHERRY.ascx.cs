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
    public partial class ProductDetailsModel_CHERRY : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;
        private WebModel webModel;
        private Item item;
        protected WebImage productWebImage;
        protected ProductImageCollection productWebImages;
        protected ProductItem productItem = null;
        protected WebItemList webItemList;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            webItemList = new WebItemList(infojet, webPageLine.code);

            if (webItemList.getRequestedType() == 1)
            {
                webModel = new WebModel(infojet, webItemList.getRequestedNo());
            }
            else
            {
                item = new Item(infojet, webItemList.getRequestedNo());
            }

            if (infojet.userSession != null)
            {
                buyPanel.Visible = true;
            }
            if (infojet.webSite.allowPurchaseNotLoggedIn)
            {
                buyPanel.Visible = true;
            }

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


            if (singleItemPanel.Visible == true)
            {
                quantityBox.Text = quantityBox.Text.Replace(",", ".");

                string referenceText = "";
                TextBox refBox = (TextBox)singleItemPanel.FindControl("referenceBox");
                if (refBox != null) referenceText = refBox.Text;

                bool allCheckOk = true;
                float quantity = 0;
                try
                {
                    quantity = float.Parse(quantityBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception)
                { }

                if (quantity > 0)
                {
                    //Check inventory
                    if (productItem.webItemSetting.availability == 1)
                    {
                        if (productItem.inventory < quantity)
                        {
                            allCheckOk = false;
                            errorMessageSingle.Text = infojet.translate("NOT ENOUGH IN STOCK");
                            errorMessageSingle.Visible = true;
                        }
                    }

                    if (productItem.webItemSetting.calendarBooking)
                    {
                        if (!checkCalendarBooking(quantity)) return;
                    }


                    if (allCheckOk)
                    {
                        WebUserAccount webUserAccount = null;
                        if (infojet.userSession != null) webUserAccount = infojet.userSession.webUserAccount;


                        DateTime fromDateTime = DateTime.MinValue;
                        DateTime toDateTime = DateTime.MinValue;
                        try
                        {
                            fromDateTime = DateTime.Parse(Request["fromDateBox"]);
                            toDateTime = DateTime.Parse(Request["toDateBox"]);
                        }
                        catch (Exception) { }

                        if (infojet.cartHandler.addItemToCart(productItem.no, quantity.ToString(), true, "", "", "", "", "", referenceText, webUserAccount, fromDateTime, toDateTime))
                        {
                            Navipro.Infojet.Lib.Link link = infojet.webPage.getUrlLink();
                            if (Request["category"] != null) link.setCategory(Request["category"], "");
                            link.setItem("", Request["itemNo"], "");
                            infojet.redirect(link.toUrl());
                        }                        
                        //infojet.redirect(infojet.webPage.getUrl() + "&category=" + Request["category"] + "&itemNo=" + Request["itemNo"] + "&webModelNo=" + Request["webModelNo"]);

                    }
                }
            }

            if (matrixPanel.Visible == true)
            {
                string referenceText = "";
                TextBox refBox = (TextBox)matrixPanel.FindControl("referenceBox");
                if (refBox != null) referenceText = refBox.Text;
                
                ArrayList arrayList = new ArrayList();
                bool allCheckOk = true;

                foreach (RepeaterItem repeaterItem in matrixBodyRepeater.Items)
                {

                    Repeater matrixCellRepeater = repeaterItem.FindControl("matrixCellRepeater") as Repeater;

                    foreach (RepeaterItem cellItem in matrixCellRepeater.Items)
                    {
                        HiddenField itemNoField = cellItem.FindControl("itemNo") as HiddenField;
                        HiddenField variantCodeField = cellItem.FindControl("itemVariantCode") as HiddenField;
                        TextBox matrixQuantityBox = cellItem.FindControl("matrixQuantityBox") as TextBox;
                        HiddenField inventoryField = cellItem.FindControl("inventory") as HiddenField;
                        Label inventoryLabel = cellItem.FindControl("inventoryLabel") as Label;

                        ItemInfo itemInfo = new ItemInfo();
                        itemInfo.no = itemNoField.Value;
                        itemInfo.variantCode = variantCodeField.Value;
                        try
                        {
                            itemInfo.quantity = float.Parse(matrixQuantityBox.Text);
                        }
                        catch (Exception)
                        { }

                        try
                        {
                            itemInfo.inventory = float.Parse(inventoryField.Value);
                        }
                        catch (Exception)
                        { }


                        arrayList.Add(itemInfo);


                        //Check inventory
                        if (productItem.webItemSetting.availability == 1)
                        {
                            if (itemInfo.quantity > 0)
                            {
                                if (itemInfo.inventory < itemInfo.quantity)
                                {
                                    allCheckOk = false;
                                    matrixQuantityBox.ForeColor = System.Drawing.Color.Red;
                                    errorMessage.Text = infojet.translate("NOT ENOUGH IN STOCK");
                                    errorMessage.Visible = true;
                                }
                            }
                        }
                    }
                }

                if (allCheckOk)
                {
                    int i = 0;
                    while (i < arrayList.Count)
                    {
                        ItemInfo itemInfo = (ItemInfo)arrayList[i];
                        if (itemInfo.quantity > 0)
                        {
                            infojet.cartHandler.addItemToCart(itemInfo.no, itemInfo.quantity.ToString(), false, itemInfo.variantCode, "", "", "", "", referenceText);
                        }
                        i++;
                    }

                    Navipro.Infojet.Lib.Link link = infojet.webPage.getUrlLink();
                    link.setCategory(Request["category"], "");
                    link.setItem(Request["webModelNo"], Request["itemNo"], "");
                    infojet.redirect(link.toUrl());

                    //infojet.redirect(infojet.webPage.getUrl() + "&category=" + Request["category"] + "&itemNo=" + Request["itemNo"] + "&webModelNo=" + Request["webModelNo"]);
                }

            }
        }

        private void updateView()
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            if (webModel != null)
            {
                //item = new Item(infojet, webModel.getDefaultItemNo());
                item = Item.get(infojet, webModel.getDefaultItemNo());

                matrixHeaderRepeater.DataSource = webModel.getHorizDimensionValues();
                matrixHeaderRepeater.DataBind();

                matrixBodyRepeater.DataSource = webModel.getMatrixDimensionValues();
                matrixBodyRepeater.DataBind();

                matrixPanel.Visible = true;
                singleItemPanel.Visible = false;

                productItem = webModel.getProductItem(infojet, webPageLine);

                if (productItem.webItemSetting.visibility == 1)
                {
                    //Dont show total inventory value.
                    inventoryLabel.Visible = false;
                    inventory.Visible = false;
                }
            }
            else
            {
                productItem = item.getProductItem(infojet, webPageLine);
            }

            if (!productItem.isAvailable(infojet.webSite.code))
            {
                infojet.redirect(infojet.webSite.getStartPageUrl());
            }

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
            //buyButton.Text = infojet.translate("BUY");
            //buyMatrixButton.Text = infojet.translate("BUY");
            nextDeliveryDateLabel.Text = infojet.translate("NEXT DELIVERY DATE");
            quantityBox.Text = "1";
            if ((Request["quantity"] != null) && (Request["quantity"] != ""))
            {
                quantityBox.Text = Request["quantity"];
            }

            if (productItem.leadTime == "")
            {
                leadTime.Visible = false;
                leadTimeLabel.Visible = false;
            }
            if (productItem.nextPlannedDelivery == "")
            {
                nextDeliveryDateLabel.Visible = false;
                nextDeliveryDate.Visible = false;
            }

            if (!productItem.isBuyable)
            {
                buyPanel.Visible = false;
            }

            calendarPanel.Visible = productItem.webItemSetting.calendarBooking;

            manufacturer.Text = productItem.item.manufacturerCode;
            productNo.Text = productItem.no;
            inventory.Text = productItem.inventoryText;
            unitListPrice.Text = productItem.formatedUnitListPrice;
            leadTime.Text = productItem.leadTime;
            nextDeliveryDate.Text = productItem.nextPlannedDelivery;
            unitPrice.Text = productItem.formatedUnitPrice;
            productDescription.Text = productItem.extendedText;

            itemAttributeRepeater.DataSource = productItem.itemAttributes;
            itemAttributeRepeater.DataBind();

            productItem.productImages.setSize(50, 50);
            productWebImages = productItem.productImages;
            productImageRepeater.DataSource = productWebImages;
            productImageRepeater.DataBind();
        }



        protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            float bookedQuantity = ItemCalendarBooking.calcBookedQuantity(infojet, productItem.item.no, "", e.Day.Date);

            Label label = new Label();
            label.Style.Add("font-size", "9px");
            if (productItem.inventory - bookedQuantity <= 0) label.Style.Add("color", "red");

            label.Text = "<br/>(" + (productItem.inventory - bookedQuantity) + ")";
            e.Cell.Controls.Add(label);
            //e.Cell.BackColor = Color.Firebrick;
        }

        private bool checkCalendarBooking(float quantity)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            DateTime fromDateTime = DateTime.MinValue;
            DateTime toDateTime = DateTime.MinValue;

            try
            {
                fromDateTime = DateTime.Parse(Request["fromDateBox"]);
                toDateTime = DateTime.Parse(Request["toDateBox"]);
            }
            catch (Exception)
            {
            }

            if (fromDateTime == DateTime.MinValue)
            {
                errorMessageSingle.Text = "Du måste välja från och med datum.";
                errorMessageSingle.Visible = true;
                return false;

            }

            if (toDateTime == DateTime.MinValue)
            {
                errorMessageSingle.Text = "Du måste välja till och med datum.";
                errorMessageSingle.Visible = true;
                return false;

            }


            if (fromDateTime > toDateTime)
            {
                errorMessageSingle.Text = "Från och med-datumet kan inte vara större än till och med-datumet.";
                errorMessageSingle.Visible = true;
                return false;
            }

            if (fromDateTime < DateTime.Today.AddDays(2))
            {
                errorMessageSingle.Text = "Det går inte att boka med mindre än 2 dagars varsel.";
                errorMessageSingle.Visible = true;
                return false;
            }

            DateTime currentDateTime = fromDateTime;
            while (currentDateTime < toDateTime)
            {
                float bookedQuantity = ItemCalendarBooking.calcBookedQuantity(infojet, productItem.item.no, "", currentDateTime);
                if ((productItem.inventory - bookedQuantity - quantity) < 0)
                {
                    errorMessageSingle.Text = "Lagret " + currentDateTime.ToString("yyyy-MM-dd") + " täcker inte behovet.";
                    errorMessageSingle.Visible = true;
                    return false;
                }

                currentDateTime = currentDateTime.AddDays(1);
            }

            return true;
        }


    }
}