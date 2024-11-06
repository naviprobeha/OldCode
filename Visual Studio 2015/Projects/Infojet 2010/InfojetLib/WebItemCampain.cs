using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebItemCampain.
	/// </summary>
	public class WebItemCampain
	{
		private Database database;

		public string webSiteCode;
		public string code;
		public string description;
		public string displayWebPageCode;
		public bool showPrice;
		public bool markItemsWithZeroInventory;
        public bool loggedInShowPrice;


		public WebItemCampain(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.database = infojetContext.systemDatabase;
            this.webSiteCode = infojetContext.webSite.code;
			this.code = code;

			getFromDatabase();
		}

		private void getFromDatabase()
		{

			SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Code], [Description], [Display Web Page Code], [Show Price], [Mark Items With Zero Inventory], [Logged In Show Price] FROM ["+database.getTableName("Web Item Campain")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				description = dataReader.GetValue(2).ToString();
				displayWebPageCode = dataReader.GetValue(3).ToString();

				showPrice = false;
				if (dataReader.GetValue(4).ToString() == "1") showPrice = true;

				markItemsWithZeroInventory = false;
				if (dataReader.GetValue(5).ToString() == "1") markItemsWithZeroInventory = true;

                loggedInShowPrice = false;
                if (dataReader.GetValue(6).ToString() == "1") loggedInShowPrice = true;

			}

			dataReader.Close();
			
		}


        public ProductItemCollection getItemCampains(Infojet infojetContext, WebPageLine webPageLine)
        {
            Items items = new Items();

            WebItemCampainMembers webItemCampainMembers = new WebItemCampainMembers(infojetContext);
            DataSet webItemCampainMembersDataSet = webItemCampainMembers.getCampainMembers(infojetContext.webSite.code, code);

            bool calcPrices = this.showPrice;
            if (System.Web.HttpContext.Current.Request.IsAuthenticated) calcPrices = loggedInShowPrice;

            Hashtable inventoryTable = items.getItemInfo(webItemCampainMembersDataSet, infojetContext, calcPrices, this.markItemsWithZeroInventory);
            
            ProductItemCollection productItemCollection = new ProductItemCollection();

            int i = 0;
            while (i < webItemCampainMembersDataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                string no = webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                WebPage itemInfoWebPage;
                if (this.displayWebPageCode != "")
                {
                    itemInfoWebPage = new WebPage(infojetContext, infojetContext.webSite.code, this.displayWebPageCode);
                }
                else
                {
                    itemInfoWebPage = infojetContext.webPage;
                }


                ProductItem productItem = null;

                if (type == 0)
                {
                    Item item = new Item(infojetContext.systemDatabase, no);
                    productItem = new ProductItem(item, infojetContext.languageCode);

                    if (inventoryTable[productItem.item.no] != null)
                    {
                        productItem.inventory = ((ItemInfo)inventoryTable[productItem.item.no]).inventory;
                        productItem.inventoryText = items.getInventoryText(productItem.item, ((ItemInfo)inventoryTable[productItem.item.no]).inventory, infojetContext, true);
                    }

                    productItem.link = itemInfoWebPage.getUrl() + "&itemNo=" + productItem.item.no + "&campainCode=" + this.code;

                }
                if (type == 1)
                {
                    string webModelNo = webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                    WebModel webModel = new WebModel(infojetContext, webModelNo);

                    string exposedItemNo = webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    Item item = new Item(infojetContext.systemDatabase, exposedItemNo);
                    productItem = new ProductItem(webModel, item, infojetContext.languageCode);

                    productItem.inventory = webModel.calcInventory();
                    productItem.inventoryText = items.getInventoryText(productItem.item, productItem.inventory, infojetContext, true);

                    productItem.link = itemInfoWebPage.getUrl() + "&itemNo=" + productItem.item.no + "&campainCode=" + this.code + "&webModelNo="+productItem.no;
                   
                }
                


                WebItemImages webItemImages = new WebItemImages(infojetContext.systemDatabase);
                ProductImageCollection productImageCollection = webItemImages.getWebItemImages(productItem.item.no, 0, infojetContext.webSite.code).toProductImageCollection(productItem.description);
                if (productImageCollection.Count > 0)
                {
                    productItem.productImage = productImageCollection[0];
                }

                webItemImages = new WebItemImages(infojetContext.systemDatabase);
                ProductImageCollection campainImageCollection = webItemImages.getItemCampainImages(productItem.item.no, infojetContext.webSite.code, code).toProductImageCollection(productItem.description);
                if (campainImageCollection.Count > 0)
                {
                    productItem.campainImage = campainImageCollection[0];
                }

                productItem.productImages = productImageCollection;
                productItem.campainImages = campainImageCollection;
               

                if (inventoryTable[productItem.item.no] != null)
                {
                    productItem.item.unitPrice = ((ItemInfo)inventoryTable[productItem.item.no]).unitPrice;
                    productItem.item.unitListPrice = ((ItemInfo)inventoryTable[productItem.item.no]).unitListPrice;

                    bool showPrices = false;
                    if (infojetContext.userSession != null) showPrices = loggedInShowPrice;
                    if (infojetContext.userSession == null) showPrices = showPrice;

                    if (showPrices)
                    {
                        productItem.formatedUnitPrice = productItem.item.formatUnitPrice(infojetContext.presentationCurrencyCode);
                        productItem.formatedUnitListPrice = productItem.item.formatUnitListPrice(infojetContext.presentationCurrencyCode);
                    }

                }

                productItem.buyLink = infojetContext.cartHandler.renderAddLink(productItem.item.no, 1);

                bool showItem = true;

                showItem = infojetContext.webSite.checkItemVisibility(productItem.inventory);

                productItem.setExtendedTextLength(50);

                if (showItem) productItemCollection.Add(productItem);


                i++;
            }

            return productItemCollection;
        }
	}
}
