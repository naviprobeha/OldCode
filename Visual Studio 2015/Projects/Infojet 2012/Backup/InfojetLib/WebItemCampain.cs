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

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Code], [Description], [Display Web Page Code], [Show Price], [Mark Items With Zero Inventory], [Logged In Show Price] FROM [" + database.getTableName("Web Item Campain") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
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

            //bool calcPrices = this.showPrice;
            //if (System.Web.HttpContext.Current.Request.IsAuthenticated) calcPrices = loggedInShowPrice;

            Hashtable inventoryTable = items.getItemInfo(webItemCampainMembersDataSet, infojetContext, true, this.markItemsWithZeroInventory);
            
            ProductItemCollection productItemCollection = new ProductItemCollection();

            int i = 0;
            while (i < webItemCampainMembersDataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                string no = webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                string currentDisplayWebPageCode = this.displayWebPageCode;
                if (webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() != "") currentDisplayWebPageCode = webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();

                WebPage itemInfoWebPage;
                if (currentDisplayWebPageCode != "")
                {
                    itemInfoWebPage = new WebPage(infojetContext, infojetContext.webSite.code, currentDisplayWebPageCode);
                }
                else
                {
                    itemInfoWebPage = infojetContext.webPage;
                }


                ProductItem productItem = null;

                if (type == 0)
                {
                    //Item item = new Item(infojetContext, no);
                    Item item = Item.get(infojetContext, no);

                    productItem = new ProductItem(infojetContext, item);

                    if (inventoryTable[productItem.item.no] != null)
                    {
                        productItem.setInventory(((ItemInfo)inventoryTable[productItem.item.no]).inventory);
                    }

                    productItem.link = itemInfoWebPage.getUrl() + "&amp;itemNo=" + System.Web.HttpUtility.UrlEncode(productItem.item.no) + "&amp;campainCode=" + System.Web.HttpUtility.UrlEncode(this.code);

                }
                if (type == 1)
                {
                    string webModelNo = webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                    WebModel webModel = new WebModel(infojetContext, webModelNo);

                    string exposedItemNo = webItemCampainMembersDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    //Item item = new Item(infojetContext, exposedItemNo);
                    Item item = Item.get(infojetContext, exposedItemNo);

                    productItem = new ProductItem(infojetContext, webModel, item);

                    productItem.setInventory(webModel.calcInventory(inventoryTable));

                    productItem.link = itemInfoWebPage.getUrl() + "&amp;itemNo=" + System.Web.HttpUtility.UrlEncode(productItem.item.no) + "&amp;campainCode=" + System.Web.HttpUtility.UrlEncode(this.code) + "&amp;webModelNo="+System.Web.HttpUtility.UrlEncode(productItem.no);
                   
                }
                


                WebItemImages webItemImages = new WebItemImages(infojetContext);
                ProductImageCollection productImageCollection = webItemImages.getWebItemImages(productItem.item.no, 0, infojetContext.webSite.code).toProductImageCollection(productItem.description);
                if (productImageCollection.Count > 0)
                {
                    productItem.productImage = productImageCollection[0];
                }

                webItemImages = new WebItemImages(infojetContext);
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

                showItem = productItem.checkVisibility();

                productItem.setExtendedTextLength(50);

                if (showItem) productItemCollection.Add(productItem);


                i++;
            }

            return productItemCollection;
        }
	}
}
