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
        public bool followSingleItem;

        public int defaultSortOrder;
        public bool usePageLimiter;
        public int noOfItemsPerPage;

        public ItemListFilterForm itemListFilterForm;

	
		private Database database;
        private Infojet infojetContext;
        private DataSet lastDataSetUsed;

		public WebItemList(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
            this.database = infojetContext.systemDatabase;
            this.webSiteCode = infojetContext.webSite.code;
			this.code = code;

			getFromDatabase();

            this.itemListFilterForm = new ItemListFilterForm(this, showFilterForm);
            this.itemListFilterForm.setStartupValues(showListThumbsByDefault, showListItemNoByDefault, this.noOfItemsPerPage);

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

            itemListFilterForm.defaultSorting = this.defaultSortOrder;
            this.itemListFilterForm.init();
		}


		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Code], [Description], [Default Web Item Category Code], [Show Filter Form], [Show Inventory], [Show Rec_ Price], [Show Manufacturer], [Logged In Show Inventory], [Logged In Show Rec_ Price], [Logged In Show Cust_ Price], [Show List Thumbs By Default], [Show List Item No By Default], [Default Sort Order], [Follow Single Item], [Use Page Limiter], [No_ Of Items Per Page] FROM [" + database.getTableName("Web Item List_Info") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
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

                defaultSortOrder = dataReader.GetInt32(13);

                followSingleItem = false;
                if (int.Parse(dataReader.GetValue(14).ToString()) == 1) followSingleItem = true;

                usePageLimiter = false;
                if (dataReader.GetValue(15).ToString() == "1") usePageLimiter = true;

                noOfItemsPerPage = dataReader.GetInt32(16);
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

            WebItemCategory webItemCategory;

            if ((System.Web.HttpContext.Current.Request["category"] != null) && (System.Web.HttpContext.Current.Request["category"] != ""))
            {
                webItemCategory = new WebItemCategory(infojetContext, System.Web.HttpContext.Current.Request["category"]);
            }
            else
            {
                webItemCategory = new WebItemCategory(infojetContext, this.defaultWebItemCategoryCode);
            }

            if (!webItemCategory.checkSecurity()) infojetContext.redirectToSignInPage(infojetContext.webPage);

            DataSet categoryMemberDataSet = webItemCategory.getItems();
            DataSet categoryModelVariantDataSet = webItemCategory.getItemsAndVariants();

            Items items = new Items();           
            Hashtable inventoryTable = items.getItemInfo(categoryModelVariantDataSet, infojetContext, (itemListFilterForm.showUnitPrice || itemListFilterForm.showUnitListPrice), itemListFilterForm.showInventory);

            return composeCollection(infojetContext, webPageLine, categoryMemberDataSet, webItemCategory, 0, true, this.followSingleItem, inventoryTable);

        }

        public ProductItemCollection getTopTenList(Infojet infojetContext, WebPageLine webPageLine)
        {
            WebItemRanking webItemRanking = new WebItemRanking();

            WebItemCategory webItemCategory = new WebItemCategory(infojetContext, this.defaultWebItemCategoryCode);

            if (!webItemCategory.checkSecurity()) infojetContext.redirectToSignInPage(infojetContext.webPage);

            DataSet topTenDataSet = webItemRanking.getTopTenDataSet(infojetContext, infojetContext.webSite.code);
 
            Items items = new Items();
            Hashtable inventoryTable = items.getItemInfo(topTenDataSet, infojetContext, (itemListFilterForm.showUnitPrice || itemListFilterForm.showUnitListPrice), itemListFilterForm.showInventory);

            return composeCollection(infojetContext, webPageLine, topTenDataSet, webItemCategory, 10, false, false, inventoryTable);

        }


        public ProductItemCollection composeCollection(Infojet infojetContext, WebPageLine webPageLine, DataSet dataSet, WebItemCategory webItemCategory, int maxCount, bool sort, bool followSingleItem, Hashtable inventoryTable)
        {
            lastDataSetUsed = dataSet;

            int permission = webItemCategory.checkSecurityPermission();

            ItemAttributeVisibilities itemAttributeVisibilities = new ItemAttributeVisibilities(infojetContext);
            Hashtable attributeTable = itemAttributeVisibilities.getItemListAttributes(dataSet, webPageLine.webSiteCode, this.code, infojetContext.languageCode);

            WebItemSetting.preCacheItemList(infojetContext, dataSet);
            WebModel.preloadWebModels(infojetContext, dataSet);
            //Hashtable fullItemTextTable = ExtendedTextHeader.getItemTexts(infojetContext, infojetContext.languageCode, true);
            //Hashtable shortItemTextTable = ExtendedTextHeader.getItemTexts(infojetContext, infojetContext.languageCode, false);


            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString());
                bool deleteItem = false;

                if (type == 1)
                {
                    
                    Item item = new Item(infojetContext, dataSet.Tables[0].Rows[i]);
                    
                    if (item.description != "")
                    {
                        
                        //ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);

                        //dataSet.Tables[0].Rows[i][1] = itemTranslation.description;
                        //dataSet.Tables[0].Rows[i][2] = itemTranslation.description2;



                        if ((dataSet.Tables[0].Rows[i][13].ToString() != "") && (dataSet.Tables[0].Rows[i][13].ToString() != null))
                        {
                            dataSet.Tables[0].Rows[i][1] = dataSet.Tables[0].Rows[i][13];
                            dataSet.Tables[0].Rows[i][2] = dataSet.Tables[0].Rows[i][14];
                        }
                        else
                        {
                            if (webItemCategory.requireItemTranslation)
                            {
                                deleteItem = true;
                            }
                        }


                        if (((ItemInfo)inventoryTable[item.no]) != null)
                        {
                            dataSet.Tables[0].Rows[i][3] = ((ItemInfo)inventoryTable[item.no]).unitPrice;
                        }
                        
                    }
 
                    IEnumerator attributeEnumerator = itemListFilterForm.itemAttributeFilterTable.Keys.GetEnumerator();
                    while (attributeEnumerator.MoveNext())
                    {
                        if (itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString() != "")
                        {
                            //throw new Exception(attributeEnumerator.Current.ToString()+"! 1: " + item.getAttribute(attributeEnumerator.Current.ToString(), infojetContext.languageCode) + ". 2: " + itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString());
                            if (item.getAttribute(attributeEnumerator.Current.ToString(), infojetContext.languageCode) != itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString())
                            {
                                deleteItem = true;
                            }
                        }
                    }

                }
                if (type == 2)
                {
                    //WebModel webModel = new WebModel(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());
                    WebModel webModel = WebModel.get(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());


                    if (webModel.description != "")
                    {
                        //WebModelTranslation webModelTranslation = webModel.getTranslation(infojetContext.languageCode);

                        //Item item = new Item(infojetContext, webModel.getDefaultItemNo());
                        //Item item = new Item(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
                        Item item = Item.get(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

                        if (dataSet.Tables[0].Rows[i][15].ToString() != "")
                        {
                            dataSet.Tables[0].Rows[i][1] = dataSet.Tables[0].Rows[i][15];
                            dataSet.Tables[0].Rows[i][2] = dataSet.Tables[0].Rows[i][16];
                        }
                        else
                        {
                            if (webItemCategory.requireItemTranslation)
                            {
                                deleteItem = true;
                            }
                        }


                        dataSet.Tables[0].Rows[i][4] = item.salesUnitOfMeasure;
                        dataSet.Tables[0].Rows[i][5] = item.manufacturerCode;
                        dataSet.Tables[0].Rows[i][6] = item.leadTimeCalculation;
                        dataSet.Tables[0].Rows[i][8] = item.itemDiscGroup;

                        dataSet.Tables[0].Rows[i][3] = ((ItemInfo)inventoryTable[item.no]).unitPrice;
                        dataSet.Tables[0].Rows[i][7] = ((ItemInfo)inventoryTable[item.no]).unitListPrice;

                        IEnumerator attributeEnumerator = itemListFilterForm.itemAttributeFilterTable.Keys.GetEnumerator();
                        while (attributeEnumerator.MoveNext())
                        {
                            if (itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString() != "")
                            {
                                //throw new Exception(attributeEnumerator.Current.ToString()+"! 1: " + item.getAttribute(attributeEnumerator.Current.ToString(), infojetContext.languageCode) + ". 2: " + itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString());
                                if (item.getAttribute(attributeEnumerator.Current.ToString(), infojetContext.languageCode) != itemListFilterForm.itemAttributeFilterTable[attributeEnumerator.Current.ToString()].ToString())
                                {
                                    deleteItem = true;
                                }
                            }
                        }


                    }


                }

                if (deleteItem)
                {
                    dataSet.Tables[0].Rows[i].Delete();
                }

                
                i++;
            }


            dataSet.Tables[0].AcceptChanges();

            dataSet.Tables[0].CaseSensitive = false;
            DataRow[] categoryMemberRows = null;

            if (sort)
            {
                categoryMemberRows = dataSet.Tables[0].Select(itemListFilterForm.getFilter(), itemListFilterForm.getSorting());
            }
            else
            {
                categoryMemberRows = dataSet.Tables[0].Select();
            }



            WebPage categoryItemInfoWebPage = infojetContext.webPage;
            if (webItemCategory.itemInfoWebPageCode != "")
            {
                categoryItemInfoWebPage = new WebPage(infojetContext, infojetContext.webSite.code, webItemCategory.itemInfoWebPageCode);
            }
            Link categoryItemInfoLink = categoryItemInfoWebPage.getUrlLink();
            //string categoryItemInfoWebPageUrl = categoryItemInfoWebPage.getUrl();

            ProductItemCollection productItemCollection = new ProductItemCollection();
            WebItemImages webItemImages = new WebItemImages(infojetContext);
            Hashtable webItemImageTable = webItemImages.getWebItemImages(0, infojetContext.webSite.code);

            if (maxCount == 0) maxCount = categoryMemberRows.Length;
            if (maxCount > categoryMemberRows.Length) maxCount = categoryMemberRows.Length;

            int itemCountPerPage = maxCount;
            int startNo = 0;
            if (usePageLimiter)
            {
                if (itemListFilterForm.currentPage == 0) itemListFilterForm.currentPage = 1;
                itemCountPerPage = itemListFilterForm.noOfItemsPerPage;
                startNo = (itemListFilterForm.currentPage - 1) * itemCountPerPage;
                if ((maxCount / itemCountPerPage) == (itemListFilterForm.currentPage-1)) itemCountPerPage = maxCount % itemListFilterForm.noOfItemsPerPage;

                productItemCollection.setPageNavigation(itemListFilterForm.noOfItemsPerPage, itemListFilterForm.currentPage, categoryMemberRows.Length);
            }

            i = startNo;

            

            while (i < (startNo+itemCountPerPage))
            {
                try
                {
                    int type = int.Parse(categoryMemberRows[i].ItemArray.GetValue(10).ToString());

                    ProductItem productItem = null;

                    //string itemInfoWebPageUrl = categoryItemInfoWebPageUrl;
                    Link itemInfoLink = categoryItemInfoLink;
                    if (categoryMemberRows[i].ItemArray.GetValue(9).ToString() != "")
                    {
                        WebPage itemInfoWebPage = new WebPage(infojetContext, infojetContext.webSite.code, categoryMemberRows[i].ItemArray.GetValue(9).ToString());
                        itemInfoLink = itemInfoWebPage.getUrlLink();
                    }


                    if (type == 1)
                    {
                        Item item = new Item(infojetContext, categoryMemberRows[i]);

                        productItem = new ProductItem(infojetContext, item);

                        if (inventoryTable[productItem.item.no] != null)
                        {
                            productItem.setInventory(((ItemInfo)inventoryTable[productItem.item.no]).inventory);
                        }

                        itemInfoLink.setCategory(webItemCategory.code, webItemCategory.description);
                        itemInfoLink.setItem("", productItem.item.no, productItem.description);
                        //productItem.link = itemInfoWebPageUrl + "&amp;category=" + System.Web.HttpUtility.UrlEncode(webItemCategory.code).ToLower() + "&amp;itemNo=" + System.Web.HttpUtility.UrlEncode(productItem.item.no);
                        productItem.link = itemInfoLink.toUrl();

                        productItem.buyLink = infojetContext.cartHandler.renderAddLink(productItem.item.no, 1);

                    }

                    if (type == 2)
                    {
                        WebModel webModel = new WebModel(infojetContext, categoryMemberRows[i].ItemArray.GetValue(11).ToString());

                        string exposedItemNo = categoryMemberRows[i].ItemArray.GetValue(0).ToString();

                        Item item = new Item(infojetContext, exposedItemNo);

                        productItem = new ProductItem(infojetContext, webModel, item);

                        itemInfoLink.setCategory(webItemCategory.code, webItemCategory.description);
                        itemInfoLink.setItem(webModel.no, item.no, productItem.description);

                        productItem.setInventory(webModel.calcInventory(inventoryTable));
                        //productItem.link = itemInfoWebPageUrl + "&amp;category=" + System.Web.HttpUtility.UrlEncode(webItemCategory.code).ToLower() + "&amp;itemNo=" + System.Web.HttpUtility.UrlEncode(productItem.item.no) + "&amp;webModelNo=" + System.Web.HttpUtility.UrlEncode(webModel.no);
                        productItem.link = itemInfoLink.toUrl();

                        productItem.buyLink = productItem.link;


                    }

                    //productItem.setItemTexts(fullItemTextTable, shortItemTextTable);

                    /*
                    WebItemImages webItemImages = new WebItemImages(infojetContext);
                    productItem.productImages = webItemImages.getWebItemImages(productItem.item.no, 0, infojetContext.webSite.code).toProductImageCollection(productItem.description);
                    if (productItem.productImages.Count > 0)
                    {
                        productItem.productImage = productItem.productImages[0];
                        productItem.productImage.setSize(120, 120);

                        productItem.thumbnailImageUrl = productItem.productImage.getUrlFromSize(50, 50);
                    }
                    */
                    if (webItemImageTable.Contains(productItem.item.no))
                    {
                        productItem.productImage = (ProductImage)webItemImageTable[productItem.item.no];
                        productItem.thumbnailImageUrl = productItem.productImage.getUrlFromSize(50, 50);
                    }

                    if (inventoryTable[productItem.item.no] != null)
                    {
                        productItem.item.unitPrice = ((ItemInfo)inventoryTable[productItem.item.no]).unitPrice;
                        productItem.item.unitListPrice = ((ItemInfo)inventoryTable[productItem.item.no]).unitListPrice;

                        productItem.formatedUnitPrice = productItem.item.formatUnitPrice(infojetContext.presentationCurrencyCode);
                        productItem.formatedUnitListPrice = productItem.item.formatUnitListPrice(infojetContext.presentationCurrencyCode);

                    }

                    if (attributeTable[productItem.item.no] != null)
                    {
                        productItem.itemAttributes = (ItemAttributeCollection)attributeTable[productItem.item.no];
                    }


                    if (followSingleItem)
                    {
                        //If only one (1) item exists in the cateogry, send visitor directly to the item info page.
                        if (dataSet.Tables[0].Rows.Count == 1)
                        {
                            infojetContext.redirect(productItem.link);
                        }
                    }



                    bool showItem = true;

                    showItem = productItem.checkVisibility();

                    //Check View Only
                    if (permission == 2) productItem.webItemSetting.setViewOnly();

                    if ((itemListFilterForm.showInventoryOnly) && (productItem.inventory <= infojetContext.webSite.zeroInventoryValue)) showItem = false;
                    if (productItem.item.description == "") showItem = false;


                    if (showItem) productItemCollection.Add(productItem);

                }
                //catch (Exception e) { throw new Exception("Err: " + e.Message); }
                catch (Exception) { }
                i++;
            }
            
 
            if (productItemCollection.Count == 0)
            {
                //productItemCollection.Add(new ProductItem(infojetContext, null));
            }
            return productItemCollection;
        }


        public int getRequestedType()
        {
            if ((System.Web.HttpContext.Current.Request["webModelNo"] != "") && (System.Web.HttpContext.Current.Request["webModelNo"] != null))
            {
                return 1;

            }

            return 0;
        }

        public string getRequestedNo()
        {
            if ((System.Web.HttpContext.Current.Request["webModelNo"] != "") && (System.Web.HttpContext.Current.Request["webModelNo"] != null))
            {
                return System.Web.HttpContext.Current.Request["webModelNo"];

            }

            if (System.Web.HttpContext.Current.Request["itemNo"] != null) return System.Web.HttpContext.Current.Request["itemNo"];
            return "";
        }

        public ItemAttributeCollection getFilteredItemAttributes()
        {
            return itemListFilterForm.getFilteredItemAttributes(infojetContext, lastDataSetUsed);
        }

	}
}