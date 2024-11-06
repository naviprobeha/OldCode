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
    public partial class ProductList_CHERRY : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected WebItemList webItemList;
        protected ProductItemCollection productItemCollection;
        protected ItemAttributeCollection itemAttributeFilterCollection;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            webItemList = new WebItemList(infojet, webPageLine.code);

            showInventoryOnlyLabel.Text = infojet.translate("SHOW INVENTORY ONLY");
            sorting1Label.Text = infojet.translate("SORTING");
            filterButton.Text = infojet.translate("FILTER");

            if (!IsPostBack)
            {
                webItemList.itemListFilterForm.clearItemAttributeFilters();

                sorting1List.Items.Add(new ListItem(infojet.translate("ITEM NO"), "0"));
                sorting1List.Items.Add(new ListItem(infojet.translate("DESCRIPTION"), "1"));
                sorting1List.Items.Add(new ListItem(infojet.translate("PRICE"), "2"));

                sorting2List.Items.Add(new ListItem(infojet.translate("ASCENDING"), "0"));
                sorting2List.Items.Add(new ListItem(infojet.translate("DESCENDING"), "1"));

                sorting1List.SelectedIndex = webItemList.itemListFilterForm.sorting1;
                sorting2List.SelectedIndex = webItemList.itemListFilterForm.sorting2;
                webItemList.itemListFilterForm.clearFilterText();
            }

            if (this.IsPostBack)
            {
                webItemList.itemListFilterForm.showInventoryOnly = this.showInventoryOnly.Checked;
                webItemList.itemListFilterForm.sorting1 = int.Parse(this.sorting1List.Items[this.sorting1List.SelectedIndex].Value);
                webItemList.itemListFilterForm.sorting2 = int.Parse(this.sorting2List.Items[this.sorting2List.SelectedIndex].Value);
                webItemList.itemListFilterForm.filterText = this.filterBox.Text;
                webItemList.itemListFilterForm.save();
            }

            itemFilterForm.Visible = webItemList.itemListFilterForm.showForm;
            showInventoryOnly.Visible = webItemList.itemListFilterForm.showInventory;
            showInventoryOnlyLabel.Visible = webItemList.itemListFilterForm.showInventory;
            showInventoryOnly.Checked = webItemList.itemListFilterForm.showInventoryOnly;

            if (!IsPostBack)
            {
                productItemCollection = webItemList.getItems(infojet, webPageLine);

                productItemCollection.setSize(170, 170);
                productItemCollection.setExtendedTextLength(100);
                productItemCollection.setNoImageUrl("../../_assets/img/no_image_170_170.jpg");

                itemAttributeFilterCollection = webItemList.getFilteredItemAttributes();
                itemAttributeFilterRepeater.DataSource = itemAttributeFilterCollection;
                itemAttributeFilterRepeater.DataBind();
                if (((ItemAttributeCollection)itemAttributeFilterRepeater.DataSource).Count == 0) itemAttributeFilterRepeater.Visible = false;


                bindData();
            }

        }


        private void bindData()
        {
            productListRepeater.DataSource = productItemCollection;
            productListRepeater.DataBind();

            productGridRepeater.DataSource = productItemCollection;
            productGridRepeater.DataBind();

        }

        
 
        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion


        protected void updateFilter(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            webItemList.itemListFilterForm.showInventoryOnly = this.showInventoryOnly.Checked;
            webItemList.itemListFilterForm.sorting1 = int.Parse(this.sorting1List.Items[this.sorting1List.SelectedIndex].Value);
            webItemList.itemListFilterForm.sorting2 = int.Parse(this.sorting2List.Items[this.sorting2List.SelectedIndex].Value);
            webItemList.itemListFilterForm.filterText = this.filterBox.Text;
            webItemList.itemListFilterForm.save();

            productItemCollection = webItemList.getItems(infojet, webPageLine);

            productItemCollection.setSize(170, 170);
            productItemCollection.setExtendedTextLength(100);
            productItemCollection.setNoImageUrl("../../_assets/img/no_image_170_170.jpg");

            bindData();
        }

        protected void attributeFilterList_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList dropDownList = ((DropDownList)sender);


            string itemAttributeCode = dropDownList.SelectedValue.Substring(0, dropDownList.SelectedValue.IndexOf("|"));
            string itemAttributeValue = dropDownList.SelectedValue.Substring(dropDownList.SelectedValue.IndexOf("|") + 1);

            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            //WebItemList webItemList = new WebItemList(infojet, webPageLine.code);

            webItemList.itemListFilterForm.setItemAttributeFilterValue(itemAttributeCode, itemAttributeValue);
            webItemList.itemListFilterForm.save();

            productItemCollection = webItemList.getItems(infojet, webPageLine);
            
            productItemCollection.setExtendedTextLength(100);
            productItemCollection.setSize(170, 170);
            productItemCollection.setNoImageUrl("../../_assets/img/no_image_170_170.jpg");

            bindData();
        }

 

    }
}