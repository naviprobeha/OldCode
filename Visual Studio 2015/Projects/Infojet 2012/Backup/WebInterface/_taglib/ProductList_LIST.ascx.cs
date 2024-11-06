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
    public partial class ProductList_LIST : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        private BoundField itemNoField;
        private BoundField descriptionField;
        private ImageField imageField;
        private BoundField manufacturerField;
        private BoundField inventoryField;
        private BoundField unitListPriceField;
        private BoundField unitPriceField;
        private ButtonField buyField;

        protected WebItemList webItemList;

        private ProductItemCollection productItemCollection;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            webItemList = new WebItemList(infojet, webPageLine.code);

            itemListGrid.Columns.Clear();

            
            itemNoField = new BoundField();
            itemNoField.DataField = "no";
            itemNoField.HeaderText = infojet.translate("ITEM NO");
            itemNoField.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

            descriptionField = new BoundField();
            descriptionField.DataField = "description";
            descriptionField.HeaderText = infojet.translate("DESCRIPTION");
            descriptionField.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

            imageField = new ImageField();
            imageField.HeaderText = "";
            imageField.DataImageUrlField = "productImageUrl";

            manufacturerField = new BoundField();
            manufacturerField.DataField = "manufacturer";
            manufacturerField.HeaderText = infojet.translate("MANUFACTURER");
            manufacturerField.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

            inventoryField = new BoundField();
            inventoryField.DataField = "inventoryText";
            inventoryField.HeaderText = infojet.translate("INVENTORY");
            inventoryField.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;


            unitListPriceField = new BoundField();
            unitListPriceField.DataField = "formatedUnitListPrice";
            unitListPriceField.HeaderText = infojet.translate("UNIT LIST PRICE");
            unitListPriceField.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            unitListPriceField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

            unitPriceField = new BoundField();
            unitPriceField.DataField = "formatedUnitPrice";
            unitPriceField.HeaderText = infojet.translate("UNIT PRICE");
            unitPriceField.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            unitPriceField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;


            buyField = new ButtonField();
            //buyField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            buyField.ButtonType = ButtonType.Link;
            buyField.Text = infojet.translate("BUY");
            buyField.CommandName = "BUY";
            buyField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //buyField.DataNavigateUrlFields = new string[] { "buyLink" };
            //buyField.DataNavigateUrlFormatString = "{0}";


            itemListGrid.Columns.Add(imageField);
            itemListGrid.Columns.Add(itemNoField);
            itemListGrid.Columns.Add(descriptionField);
            itemListGrid.Columns.Add(manufacturerField);
            itemListGrid.Columns.Add(inventoryField);
            itemListGrid.Columns.Add(unitListPriceField);
            itemListGrid.Columns.Add(unitPriceField);
            itemListGrid.Columns.Add(buyField);

            showImagesLabel.Text = infojet.translate("SHOW IMAGES");
            showItemNoLabel.Text = infojet.translate("SHOW ITEM NO");
            showInventoryOnlyLabel.Text = infojet.translate("SHOW INVENTORY ONLY");
            sorting1Label.Text = infojet.translate("SORTING");
            filterLabel.Text = infojet.translate("FILTER");

            if (!IsPostBack)
            {
                webItemList.itemListFilterForm.clearItemAttributeFilters();

                sorting1List.Items.Add(new ListItem(infojet.translate("NONE"), "0"));
                sorting1List.Items.Add(new ListItem(infojet.translate("DESCRIPTION"), "1"));
                sorting1List.Items.Add(new ListItem(infojet.translate("PRICE"), "2"));
                if (webItemList.itemListFilterForm.showManufacturer) sorting2List.Items.Add(new ListItem(infojet.translate("MANUFACTURER"), "3"));

                sorting2List.Items.Add(new ListItem(infojet.translate("ASCENDING"), "0"));
                sorting2List.Items.Add(new ListItem(infojet.translate("DESCENDING"), "1"));

                itemAttributeFilterRepeater.DataSource = webItemList.getFilteredItemAttributes();
                itemAttributeFilterRepeater.DataBind();
                if (((ItemAttributeCollection)itemAttributeFilterRepeater.DataSource).Count == 0) itemAttributeFilterRepeater.Visible = false;

            }

            filterButton.Text = infojet.translate("FILTER");


            itemFilterForm.Visible = webItemList.itemListFilterForm.showForm;
            itemNoField.Visible = webItemList.itemListFilterForm.showItemNo;
            inventoryField.Visible = webItemList.itemListFilterForm.showInventory;
            buyField.Visible = webItemList.itemListFilterForm.showBuy;
            manufacturerField.Visible = webItemList.itemListFilterForm.showManufacturer;
            unitListPriceField.Visible = webItemList.itemListFilterForm.showUnitListPrice;
            unitPriceField.Visible = webItemList.itemListFilterForm.showUnitPrice;


            this.PreRender += new EventHandler(ProductList_LIST_PreRender);
        }

        void ProductList_LIST_PreRender(object sender, EventArgs e)
        {

            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            //WebItemList webItemList = new WebItemList(infojet, webPageLine.code);
            
            if (this.IsPostBack)
            {
                webItemList.itemListFilterForm.showImages = this.showImages.Checked;
                webItemList.itemListFilterForm.showItemNo = this.showItemNo.Checked;
                webItemList.itemListFilterForm.showInventoryOnly = this.showInventoryOnly.Checked;
                webItemList.itemListFilterForm.sorting1 = int.Parse(this.sorting1List.Items[this.sorting1List.SelectedIndex].Value);
                webItemList.itemListFilterForm.sorting2 = int.Parse(this.sorting2List.Items[this.sorting2List.SelectedIndex].Value);
                webItemList.itemListFilterForm.filterText = this.filterBox.Text;
                webItemList.itemListFilterForm.save();
            }

            itemFilterForm.Visible = webItemList.itemListFilterForm.showForm;
            imageField.Visible = webItemList.itemListFilterForm.showImages;
            itemNoField.Visible = webItemList.itemListFilterForm.showItemNo;
            inventoryField.Visible = webItemList.itemListFilterForm.showInventory;
            buyField.Visible = webItemList.itemListFilterForm.showBuy;
            manufacturerField.Visible = webItemList.itemListFilterForm.showManufacturer;
            showInventoryOnly.Visible = webItemList.itemListFilterForm.showInventory;
            showInventoryOnlyLabel.Visible = webItemList.itemListFilterForm.showInventory;
            showItemNo.Visible = (!webItemList.showListItemNoByDefault);
            showItemNoLabel.Visible = (!webItemList.showListItemNoByDefault);

            if (productItemCollection == null) productItemCollection = webItemList.getItems(infojet, webPageLine);
            itemListGrid.DataSource = productItemCollection;
            itemListGrid.DataBind();


            this.showImages.Checked = webItemList.itemListFilterForm.showImages;
            this.showItemNo.Checked = webItemList.itemListFilterForm.showItemNo;
            this.showInventoryOnly.Checked = webItemList.itemListFilterForm.showInventoryOnly;

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

 
        protected void itemListGrid_DataBound(object sender, EventArgs e)
        {

        }

        protected void itemListGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                string CssClass = (e.Row.RowState == DataControlRowState.Normal ? ((GridView)sender).RowStyle.CssClass : ((GridView)sender).AlternatingRowStyle.CssClass);

                e.Row.Attributes["onmouseover"] = string.Format("javascript:OnHover(this, '{0}');", CssClass);

                e.Row.Attributes["onmouseout"] = "javascript:OffHover(this);";
                e.Row.Attributes["onselectstart"] = "javascript:return false;";
                e.Row.Attributes["onclick"] = string.Format("javascript:Click(this, '{0}');", ((ProductItem)e.Row.DataItem).link);

            }
            
        }

        protected void sorting1List_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }


        protected void attributeFilterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DropDownList dropDownList = ((DropDownList)sender);


            string itemAttributeCode = dropDownList.SelectedValue.Substring(0, dropDownList.SelectedValue.IndexOf("|"));
            string itemAttributeValue = dropDownList.SelectedValue.Substring(dropDownList.SelectedValue.IndexOf("|")+1);

            //Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            //WebItemList webItemList = new WebItemList(infojet, webPageLine.code);

            webItemList.itemListFilterForm.setItemAttributeFilterValue(itemAttributeCode, itemAttributeValue);
            webItemList.itemListFilterForm.save();
        }

        protected void productList_buy(object sender, GridViewCommandEventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            //infojet.init();

            int row = -1;
            int.TryParse(e.CommandArgument as string, out row);
            if (row == -1)
            {
                return;
            }

            GridViewRow gdrow = itemListGrid.Rows[row];

            if (productItemCollection == null) productItemCollection = webItemList.getItems(infojet, webPageLine);
            ProductItem productItem = productItemCollection[gdrow.DataItemIndex];

            if (e.CommandName == "BUY")
            {
                if ((productItem.webItemSetting.type == 1) || (productItem.webItemSetting.calendarBooking))
                {
                    infojet.redirect(productItem.link);
                }
                else
                {
                    infojet.cartHandler.addItemToCart(productItem.no, "1", false, "", "", "", "", "", "");
                    infojet.redirect(infojet.webPage.getUrl()+"&category="+Request["category"]);
                }
            }
        }

    }
}