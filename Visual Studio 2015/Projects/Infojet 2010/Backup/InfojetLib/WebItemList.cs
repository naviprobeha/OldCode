using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebItemList
	{
		public string webSiteCode;
		public string code;
		public string description;
		public string defaultWebItemCategoryCode;
		public bool showFilterForm;
		public bool showInventory;
		public bool showRecPrice;
		public bool showManufacturer;
		public bool loggedInShowInventory;
		public bool loggedInShowRecPrice;
		public bool loggedInShowCustPrice;
		public bool showListThumbsByDefault;
		public bool showListItemNoByDefault;

        public ItemListFilterForm itemListFilterForm;

	
		private Database database;

		public WebItemList(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.database = infojetContext.systemDatabase;
            this.webSiteCode = infojetContext.webSite.code;
			this.code = code;

			getFromDatabase();

            this.itemListFilterForm = new ItemListFilterForm(this, showFilterForm);
            this.itemListFilterForm.setStartupValues(showListThumbsByDefault, showListItemNoByDefault);

            this.itemListFilterForm.showManufacturer = this.showManufacturer;

            if (System.Web.HttpContext.Current.Request.IsAuthenticated)
            {
                itemListFilterForm.showUnitPrice = this.loggedInShowCustPrice;
                itemListFilterForm.showUnitListPrice = this.loggedInShowRecPrice;
                itemListFilterForm.showInventory = this.loggedInShowInventory;
                itemListFilterForm.showBuy = true;

            }
            else
            {
                itemListFilterForm.showUnitPrice = false;
                itemListFilterForm.showUnitListPrice = this.showRecPrice;
                itemListFilterForm.showInventory = this.showInventory;
                itemListFilterForm.showBuy = infojetContext.webSite.allowPurchaseNotLoggedIn;
            }
          
            this.itemListFilterForm.init();
		}


		private void getFromDatabase()
		{

			SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Code], [Description], [Default Web Item Category Code], [Show Filter Form], [Show Inventory], [Show Rec_ Price], [Show Manufacturer], [Logged In Show Inventory], [Logged In Show Rec_ Price], [Logged In Show Cust_ Price], [Show List Thumbs By Default], [Show List Item No By Default] FROM ["+database.getTableName("Web Item List_Info")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				description = dataReader.GetValue(2).ToString();
				defaultWebItemCategoryCode = dataReader.GetValue(3).ToString();

				showFilterForm = false;
				if (int.Parse(dataReader.GetValue(4).ToString()) == 1) showFilterForm = true;

				showInventory = false;
				if (int.Parse(dataReader.GetValue(5).ToString()) == 1) showInventory = true;

				showRecPrice = false;
				if (int.Parse(dataReader.GetValue(6).ToString()) == 1) showRecPrice = true;

				showManufacturer = false;
				if (int.Parse(dataReader.GetValue(7).ToString()) == 1) showManufacturer = true;

				loggedInShowInventory = false;
				if (int.Parse(dataReader.GetValue(8).ToString()) == 1) loggedInShowInventory = true;

				loggedInShowRecPrice = false;
				if (int.Parse(dataReader.GetValue(9).ToString()) == 1) loggedInShowRecPrice = true;

				loggedInShowCustPrice = false;
				if (int.Parse(dataReader.GetValue(10).ToString()) == 1) loggedInShowCustPrice = true;

				showListThumbsByDefault = false;
				if (int.Parse(dataReader.GetValue(11).ToString()) == 1) showListThumbsByDefault = true;

				showListItemNoByDefault = false;
				if (int.Parse(dataReader.GetValue(12).ToString()) == 1) showListItemNoByDefault = true;

			}

			dataReader.Close();
			
		}

        public bool checkShowInventory(Infojet infojetContext)
        {
            if (infojetContext.userSession != null) return this.loggedInShowInventory;
            return showInventory;
        }

        public bool checkShowRecPrice(Infojet infojetContext)
        {
            if (infojetContext.userSession != null) return this.loggedInShowRecPrice;
            return showRecPrice;
        }

        public bool checkShowPrice(Infojet infojetContext)
        {
            if (infojetContext.userSession != null) return this.loggedInShowCustPrice;
            return false;
        }

        public bool checkShowManufacturer(Infojet infojetContext)
        {
            return showManufacturer;
        }


        public ProductItemCollection getItems(Infojet infojetContext, WebPageLine webPageLine)
        {
            Items items = new Items();

            WebItemCategory webItemCategory;

            if ((System.Web.HttpContext.Current.Request["category"] != null) && (System.Web.HttpContext.Current.Request["category"] != ""))
            {
                webItemCategory = new WebItemCategory(infojetContext, System.Web.HttpContext.Current.Request["category"]);
            }
            else
            {
                webItemCategory = new WebItemCategory(infojetContext, this.defaultWebItemCategoryCode);
            }


            DataSet categoryMemberDataSet = webItemCategory.getItems();
            Hashtable inventoryTable = items.getItemInfo(categoryMemberDataSet, infojetContext, (itemListFilterForm.showUnitPrice || itemListFilterForm.showUnitListPrice), itemListFilterForm.showInventory);

            int i = 0;
            while (i < categoryMemberDataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(categoryMemberDataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString());

                if (type == 1)
                {
                    Item item = new Item(database, categoryMemberDataSet.Tables[0].Rows[i]);
                    ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);

                    categoryMemberDataSet.Tables[0].Rows[i][1] = itemTranslation.description;
                    categoryMemberDataSet.Tables[0].Rows[i][2] = itemTranslation.description2;
                    categoryMemberDataSet.Tables[0].Rows[i][3] = ((ItemInfo)inventoryTable[item.no]).unitPrice;

                }
                if (type == 2)
                {

                    WebModel webModel = new WebModel(infojetContext, categoryMemberDataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());
                    WebModelTranslation webModelTranslation = webModel.getTranslation(infojetContext.languageCode);

                    Item item = new Item(database, webModel.getDefaultItemNo());

                    categoryMemberDataSet.Tables[0].Rows[i][1] = webModelTranslation.description;
                    categoryMemberDataSet.Tables[0].Rows[i][2] = webModelTranslation.description2;
                    categoryMemberDataSet.Tables[0].Rows[i][3] = ((ItemInfo)inventoryTable[item.no]).unitPrice;

                    categoryMemberDataSet.Tables[0].Rows[i][4] = item.salesUnitOfMeasure;
                    categoryMemberDataSet.Tables[0].Rows[i][5] = item.manufacturerCode;
                    categoryMemberDataSet.Tables[0].Rows[i][6] = item.leadTimeCalculation;
                    categoryMemberDataSet.Tables[0].Rows[i][7] = item.unitListPrice;
                    categoryMemberDataSet.Tables[0].Rows[i][8] = item.itemDiscGroup;

                }

                i++;
            }


            categoryMemberDataSet.Tables[0].CaseSensitive = false;
            DataRow[] categoryMemberRows = categoryMemberDataSet.Tables[0].Select(itemListFilterForm.getFilter(), itemListFilterForm.getSorting());


            ProductItemCollection productItemCollection = new ProductItemCollection();

            i = 0;
            while (i < categoryMemberRows.Length)
            {
                int type = int.Parse(categoryMemberRows[i].ItemArray.GetValue(10).ToString());

                ProductItem productItem = null;

                WebPage itemInfoWebPage;
                if (categoryMemberRows[i].ItemArray.GetValue(9).ToString() != "")
                {
                    itemInfoWebPage = new WebPage(infojetContext, infojetContext.webSite.code, categoryMemberRows[i].ItemArray.GetValue(7).ToString());
                }
                else
                {
                    if (webItemCategory.itemInfoWebPageCode != "")
                    {
                        itemInfoWebPage = new WebPage(infojetContext, infojetContext.webSite.code, webItemCategory.itemInfoWebPageCode);
                    }
                    else
                    {
                        itemInfoWebPage = infojetContext.webPage;
                    }
                }


                if (type == 1)
                {
                    Item item = new Item(database, categoryMemberRows[i]);

                    productItem = new ProductItem(item, infojetContext.languageCode);

                    if (inventoryTable[productItem.item.no] != null)
                    {
                        productItem.inventory = ((ItemInfo)inventoryTable[productItem.item.no]).inventory;
                        productItem.inventoryText = items.getInventoryText(productItem.item, ((ItemInfo)inventoryTable[productItem.item.no]).inventory, infojetContext, true);
                    }

                    productItem.link = itemInfoWebPage.getUrl() + "&category=" + webItemCategory.code + "&itemNo=" + productItem.item.no;
                    productItem.buyLink = infojetContext.cartHandler.renderAddLink(productItem.item.no, 1);

                }

                if (type == 2)
                {
                    WebModel webModel = new WebModel(infojetContext, categoryMemberRows[i].ItemArray.GetValue(11).ToString());

                    string exposedItemNo = categoryMemberRows[i].ItemArray.GetValue(0).ToString();
                   
                    Item item = new Item(database, exposedItemNo);

                    productItem = new ProductItem(webModel, item, infojetContext.languageCode);

                    productItem.inventory = webModel.calcInventory();
                    productItem.inventoryText = items.getInventoryText(productItem.item, productItem.inventory, infojetContext, true);

                    productItem.link = itemInfoWebPage.getUrl() + "&category=" + webItemCategory.code + "&itemNo=" + productItem.item.no + "&webModelNo="+ webModel.no;
                    productItem.buyLink = productItem.link;

                }


                WebItemImages webItemImages = new WebItemImages(infojetContext.systemDatabase);
                productItem.productImages = webItemImages.getWebItemImages(productItem.item.no, 0, infojetContext.webSite.code).toProductImageCollection(productItem.description);
                if (productItem.productImages.Count > 0)
                {
                    productItem.productImage = productItem.productImages[0];
                    productItem.productImage.setSize(50, 50);
                }



                if (inventoryTable[productItem.item.no] != null)
                {

                    productItem.item.unitPrice = ((ItemInfo)inventoryTable[productItem.item.no]).unitPrice;
                    productItem.item.unitListPrice = ((ItemInfo)inventoryTable[productItem.item.no]).unitListPrice;

                    productItem.formatedUnitPrice = productItem.item.formatUnitPrice(infojetContext.presentationCurrencyCode);
                    productItem.formatedUnitListPrice = productItem.item.formatUnitListPrice(infojetContext.presentationCurrencyCode);

                }



                //If only one (1) item exists in the cateogry, send visitor directly to the item info page.
                if (categoryMemberDataSet.Tables[0].Rows.Count == 1)
                {
                    infojetContext.redirect(itemInfoWebPage.getUrl() + "&category=" + webItemCategory.code + "&itemNo=" + productItem.item.no);
                }


                bool showItem = true;

                showItem = infojetContext.webSite.checkItemVisibility(productItem.inventory);
                if ((itemListFilterForm.showInventoryOnly) && (productItem.inventory <= infojetContext.webSite.zeroInventoryValue)) showItem = false;
                if (productItem.item.description == "") showItem = false;

                IEnumerator attributeEnumerator = itemListFilterForm.itemAttributeFilterTable.Keys.GetEnumerator();
                while (attributeEnumerator.MoveNext())
                {
                    if (itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString() != "")
                    {
                        //throw new Exception(attributeEnumerator.Current.ToString()+"! 1: " + item.getAttribute(attributeEnumerator.Current.ToString(), infojetContext.languageCode) + ". 2: " + itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString());
                        if (productItem.item.getAttribute(attributeEnumerator.Current.ToString(), infojetContext.languageCode) != itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString()) showItem = false;
                    }
                }
                

                if (showItem) productItemCollection.Add(productItem);


                i++;
            }

            if (productItemCollection.Count == 0)
            {
                productItemCollection.Add(new ProductItem(null, infojetContext.languageCode));
            }
            return productItemCollection;
        }

        public string getRequestedWebModelNo()
        {
            if ((System.Web.HttpContext.Current.Request["webModelNo"] != "") && (System.Web.HttpContext.Current.Request["webModelNo"] != null))
            {
                return System.Web.HttpContext.Current.Request["webModelNo"];

            }

            return "";
        }
	}
}