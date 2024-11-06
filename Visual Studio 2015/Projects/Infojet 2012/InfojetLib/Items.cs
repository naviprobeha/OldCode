using System;
using System.Xml;
using System.Data;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class Items
	{
        private string campaignCode;

		public Items()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public void setCampaignCode(string campaignCode)
        {
            this.campaignCode = campaignCode;
        }

        public Hashtable getItemInfo(Item item, Infojet infojetContext, bool calcPrices, bool calcInventory)
        {

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            DataColumn itemNoCol = new DataColumn("Item No_", Type.GetType("System.String"));
            dataTable.Columns.Add(itemNoCol);

            DataRow dataRow = dataTable.NewRow();
            dataRow["Item No_"] = item.no;

            dataTable.Rows.Add(dataRow);
            dataSet.Tables.Add(dataTable);

            //Allways calc prices.
            //return getItemInfo(dataSet, infojetContext, calcPrices, calcInventory);
            return getItemInfo(dataSet, infojetContext, true, calcInventory);
        }

        public Hashtable getItemInfo(DataSet itemDataSet, Infojet infojetContext, bool calcPrices, bool calcInventory)
        {
            string customerNo = "";
            if (infojetContext.userSession != null) customerNo = infojetContext.userSession.customer.no;

            //Allways calc prices.
            //return getItemInfo(itemDataSet, infojetContext, customerNo, calcPrices, calcInventory);
            return getItemInfo(itemDataSet, infojetContext, customerNo, true, calcInventory, infojetContext.currencyCode);
        }

		public Hashtable getItemInfo(DataSet itemDataSet, Infojet infojetContext, string customerNo, bool calcPrices, bool calcInventory, string currencyCode)
		{
			//itemDataSet is a DataSet with itemNo as first column.

			if ((infojetContext.webSite.priceInventoryCalcMethod == 0) && (customerNo != ""))
			{
                Customer customer = new Customer(infojetContext, customerNo);

				ItemListServiceArgument itemListServiceArgument = new ItemListServiceArgument();

				itemListServiceArgument.setLocationCode(infojetContext.webSite.locationCode);
				itemListServiceArgument.setCustomerNo(customerNo);
				itemListServiceArgument.setCurrencyCode(infojetContext.currencyCode);

				if (customer.locationCode != "") itemListServiceArgument.setLocationCode(customer.locationCode);

				itemListServiceArgument.setItemDataSet(itemDataSet);
				return appServerRequest(infojetContext, new ServiceRequest(infojetContext, "getItemInfo", itemListServiceArgument));
			}
			else
			{
                //Allways calc prices.
				//return offlinePriceInventoryCalculation(itemDataSet, infojetContext, customerNo, calcPrices, calcInventory);
                return offlinePriceInventoryCalculation(itemDataSet, infojetContext, customerNo, true, calcInventory, currencyCode);// 
			}
		}

		private Hashtable appServerRequest(Infojet infojetContext, ServiceRequest serviceRequest)
		{
			Hashtable itemInfoTable = new Hashtable();

			ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, serviceRequest);
			if (appServerConnection.processRequest())
			{
				//Parse serviceResponse into HashTable
				itemInfoTable = parseItemInfoResponse(infojetContext, appServerConnection.serviceResponse);
			}

			return itemInfoTable;
		}

 

        private Hashtable offlinePriceInventoryCalculation(DataSet dataSet, Infojet infojetContext, string customerNo, bool calcPrices, bool calcInventory, string currencyCode)
        {
            Hashtable itemInfoTable = new Hashtable();

            if (dataSet.Tables[0].Rows.Count == 0) return itemInfoTable;

            if (calcInventory)
            {
                ItemInventory itemInventory = new ItemInventory(infojetContext);
                itemInventory.calcOfflineInventory(dataSet, infojetContext.webSite.locationCode, ref itemInfoTable);
                itemInventory.getNextPlannedReceipt(dataSet, infojetContext.webSite.locationCode, ref itemInfoTable);

            }


            if (calcPrices)
            {
                SalesPrices salesPrices = new SalesPrices(infojetContext.systemDatabase, infojetContext);
                salesPrices.setCampaignCode(campaignCode);

                SalesLineDiscounts salesLineDiscounts = new SalesLineDiscounts(infojetContext.systemDatabase, infojetContext);
                salesLineDiscounts.setCampaignCode(campaignCode);

                WebSiteLanguage webSiteLanguage = new WebSiteLanguage(infojetContext.systemDatabase, infojetContext.webSite.code, infojetContext.marketCode);

                Hashtable salesPriceTable = salesPrices.getItemPrice(dataSet, webSiteLanguage.recPriceGroupCode, currencyCode);
                Hashtable discountTable = salesLineDiscounts.getItemDiscount(dataSet, null, infojetContext.currencyCode);

                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                    if (salesPriceTable.Contains(itemNo))
                    {
                        ItemInfo itemInfo = (ItemInfo)salesPriceTable[itemNo];

                        if (!itemInfoTable.Contains(itemNo))
                        {
                            itemInfoTable.Add(itemNo, itemInfo);
                        }
                        ((ItemInfo)itemInfoTable[itemNo]).unitListPrice = itemInfo.unitPrice;
                        ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection = itemInfo.itemInfoPriceCollection;


                        if ((((ItemInfo)itemInfoTable[itemNo]).unitPrice == ((ItemInfo)itemInfoTable[itemNo]).unitListPrice) || (!infojetContext.webSite.dontAllowCustPriceAndDisc))
                        {
                            float baseUnitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitPrice;
                            
                            if (discountTable.Contains(itemNo))
                            {
                                float lastUnitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitPrice;
                                ItemInfo discountInfo = (ItemInfo)discountTable[itemNo];
                                //((ItemInfo)itemInfoTable[itemNo]).unitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitPrice - (((ItemInfo)itemInfoTable[itemNo]).unitPrice * (discountInfo.lineDiscount / 100));
                                //Decimal amount = new Decimal(((ItemInfo)itemInfoTable[itemNo]).unitPrice);
                                //amount = Math.Round(amount, 2);
                                //((ItemInfo)itemInfoTable[itemNo]).unitPrice = Convert.ToSingle(amount);
                               
                                int j = 0;
                                while (j < ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Count)
                                {
                                    ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].lineDiscount = discountInfo.lineDiscount;

                                    j++;
                                }

                                j = 0;
                                while (j < discountInfo.itemInfoPriceCollection.Count)
                                {
                                    bool match = false;
                                    int k = 0;
                                    while (k < ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Count)
                                    {
                                        if (((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].minQuantity == discountInfo.itemInfoPriceCollection[j].minQuantity) match = true;

                                        if (((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].minQuantity >= discountInfo.itemInfoPriceCollection[j].minQuantity)
                                        {
                                            ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].lineDiscount = discountInfo.itemInfoPriceCollection[j].lineDiscount;
                                        }
                                        if ((((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].minQuantity > discountInfo.itemInfoPriceCollection[j].minQuantity) && (!match))
                                        {
                                            ItemInfoPrice itemInfoPrice = new ItemInfoPrice();
                                            itemInfoPrice.minQuantity = discountInfo.itemInfoPriceCollection[j].minQuantity;
                                            itemInfoPrice.unitPrice = baseUnitPrice;
                                            itemInfoPrice.lineDiscount = discountInfo.itemInfoPriceCollection[j].lineDiscount;
                                            ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Insert(k, itemInfoPrice);
                                            match = true;
                                        }

                                        lastUnitPrice = ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].unitPrice;

                                        k++;
                                    }
                                    if (!match)
                                    {
                                        ItemInfoPrice itemInfoPrice = new ItemInfoPrice();
                                        itemInfoPrice.minQuantity = discountInfo.itemInfoPriceCollection[j].minQuantity;
                                        itemInfoPrice.unitPrice = baseUnitPrice;
                                        itemInfoPrice.lineDiscount = discountInfo.itemInfoPriceCollection[j].lineDiscount;
                                        ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Add(itemInfoPrice);
                                    }

                                    j++;
                                }

                                j = 0;
                                while (j < ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Count)
                                {
                                    ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice = ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice - (((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice * (((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].lineDiscount / 100));
                                    Decimal unitPrice = new Decimal(((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice);
                                    unitPrice = Math.Round(unitPrice, 2);
                                    ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice = Convert.ToSingle(unitPrice);

                                    j++;
                                }

                            }
                        }

                    }


                    i++;
                }


                if ((customerNo != null) && (customerNo != ""))
                {
                    Customer customer = new Customer(infojetContext, customerNo);

                    Hashtable salesPriceTable2 = salesPrices.getItemPrice(dataSet, customer, infojetContext.currencyCode);

                    discountTable = salesLineDiscounts.getItemDiscount(dataSet, customer, infojetContext.currencyCode);

                    i = 0;
                    while (i < dataSet.Tables[0].Rows.Count)
                    {
                        string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                        if (!itemInfoTable.Contains(itemNo))
                        {
                            ItemInfo itemInfo = new ItemInfo();
                            itemInfo.no = itemNo;
                            itemInfoTable.Add(itemNo, itemInfo);
                        }

                        if (salesPriceTable2.Contains(itemNo))
                        {
                            ItemInfo salesInfo = (ItemInfo)salesPriceTable2[itemNo];
                            ((ItemInfo)itemInfoTable[itemNo]).unitPrice = salesInfo.unitPrice;
                            ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection = salesInfo.itemInfoPriceCollection;
                        }

                        if ((((ItemInfo)itemInfoTable[itemNo]).unitPrice == ((ItemInfo)itemInfoTable[itemNo]).unitListPrice) || (!infojetContext.webSite.dontAllowCustPriceAndDisc))
                        {
                            float baseUnitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitPrice;

                            if (discountTable.Contains(itemNo))
                            {
                                float lastUnitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitPrice;
                                ItemInfo discountInfo = (ItemInfo)discountTable[itemNo];

                                if (infojetContext.configuration.debugMode)
                                {
                                    CartHandler.log(infojetContext, customerNo, currencyCode, 0, itemNo, 1, baseUnitPrice, discountInfo.lineDiscount, 0, "Disc. calc");
                                }				

                                //((ItemInfo)itemInfoTable[itemNo]).unitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitPrice - (((ItemInfo)itemInfoTable[itemNo]).unitPrice * (discountInfo.lineDiscount / 100));
                                //Decimal amount = new Decimal(((ItemInfo)itemInfoTable[itemNo]).unitPrice);
                                //amount = Math.Round(amount, 2);
                                //((ItemInfo)itemInfoTable[itemNo]).unitPrice = Convert.ToSingle(amount);

                                //Reset information
                                int j = 0;
                                while (j < ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Count)
                                {
                                    ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].lineDiscount = discountInfo.lineDiscount;
                                    ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice = baseUnitPrice; 
                                
                                    j++;
                                }

                                j = 0;
                                while (j < discountInfo.itemInfoPriceCollection.Count)
                                {
                                    bool match = false;
                                    int k = 0;
                                    while (k < ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Count)
                                    {
                                        if (((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].minQuantity == discountInfo.itemInfoPriceCollection[j].minQuantity) match = true;

                                        if (((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].minQuantity >= discountInfo.itemInfoPriceCollection[j].minQuantity)
                                        {
                                            ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].lineDiscount = discountInfo.itemInfoPriceCollection[j].lineDiscount;
                                        }
                                        if ((((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].minQuantity > discountInfo.itemInfoPriceCollection[j].minQuantity) && (!match))
                                        {
                                            ItemInfoPrice itemInfoPrice = new ItemInfoPrice();
                                            itemInfoPrice.minQuantity = discountInfo.itemInfoPriceCollection[j].minQuantity;
                                            itemInfoPrice.unitPrice = baseUnitPrice;
                                            itemInfoPrice.lineDiscount = discountInfo.itemInfoPriceCollection[j].lineDiscount;
                                            ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Insert(k, itemInfoPrice);
                                            match = true;

                                            if (infojetContext.configuration.debugMode)
                                            {
                                                CartHandler.log(infojetContext, customerNo, currencyCode, 0, itemNo, itemInfoPrice.minQuantity, baseUnitPrice, itemInfoPrice.lineDiscount, 0, "Disc. calc line");
                                            }				

                                        }

                                        lastUnitPrice = ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[k].unitPrice;

                                        k++;
                                    }
                                    if (!match)
                                    {
                                        ItemInfoPrice itemInfoPrice = new ItemInfoPrice();
                                        itemInfoPrice.minQuantity = discountInfo.itemInfoPriceCollection[j].minQuantity;
                                        itemInfoPrice.unitPrice = baseUnitPrice;
                                        itemInfoPrice.lineDiscount = discountInfo.itemInfoPriceCollection[j].lineDiscount;
                                        ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Add(itemInfoPrice);

                                        if (infojetContext.configuration.debugMode)
                                        {
                                            CartHandler.log(infojetContext, customerNo, currencyCode, 0, itemNo, itemInfoPrice.minQuantity, baseUnitPrice, itemInfoPrice.lineDiscount, 0, "Disc. calc line");
                                        }				

                                    }

                                    j++;
                                }


                                j = 0;
                                while (j < ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Count)
                                {
                                    float unitPriceFloat = ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice - (((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice * (((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].lineDiscount / 100));
                                    Decimal unitPrice = new Decimal(unitPriceFloat);
                                    unitPrice = Math.Round(unitPrice, 2);

                                    if (infojetContext.configuration.debugMode)
                                    {
                                        CartHandler.log(infojetContext, customerNo, currencyCode, 0, itemNo, ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].minQuantity, ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice, ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].lineDiscount, (float)unitPrice, "Final disc. line");
                                    }

                                    ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[j].unitPrice = Convert.ToSingle(unitPrice);

                                    j++;
                                }

                            }

                        }

                        int l = 0;
                        while (l < ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection.Count)
                        {
                            if ((((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[l].minQuantity <= 1) && ((((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[l].unitPrice < ((ItemInfo)itemInfoTable[itemNo]).unitPrice)))
                            {
                                ((ItemInfo)itemInfoTable[itemNo]).unitPrice = ((ItemInfo)itemInfoTable[itemNo]).itemInfoPriceCollection[l].unitPrice;
                            }

                            l++;
                        }


                        i++;
                    }


                }
                else
                {
                    i = 0;
                    while (i < dataSet.Tables[0].Rows.Count)
                    {
                        string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                        if (itemInfoTable.Contains(itemNo))
                        {

                            ((ItemInfo)itemInfoTable[itemNo]).unitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitListPrice;

                            i++;
                        }
                    }
                }


            }

            int z = 0;
            while (z < dataSet.Tables[0].Rows.Count)
            {
                string itemNo = dataSet.Tables[0].Rows[z].ItemArray.GetValue(0).ToString();
                if (itemInfoTable[itemNo] == null)
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;

                    itemInfoTable.Add(itemNo, itemInfo);

                }

                z++;
            }


            return itemInfoTable;

        }



		public Hashtable parseItemInfoResponse(Infojet infojetContext, ServiceResponse serviceResponse)
		{
			Hashtable hashtable = new Hashtable();

			XmlDocument responseDocument = new XmlDocument();
			responseDocument.LoadXml(serviceResponse.xml);
			XmlElement documentElement = responseDocument.DocumentElement;

			XmlNodeList itemNodeList = documentElement.SelectNodes("serviceResponse/itemInfo/items/item");

			int i = 0;

			WebSiteLanguage webSiteLanguage = new WebSiteLanguage(infojetContext.systemDatabase, infojetContext.webSite.code, infojetContext.languageCode);
			

			while (i < itemNodeList.Count)
			{
				XmlElement itemElement = (XmlElement)itemNodeList.Item(i);

				ItemInfo itemInfo = new ItemInfo();

				itemInfo.no = itemElement.SelectSingleNode("no").FirstChild.Value;
				itemInfo.inventory = int.Parse(itemElement.SelectSingleNode("inventory").FirstChild.Value);
				
				//Item item = new Item(infojetContext, itemInfo.no);
                Item item = Item.get(infojetContext, itemInfo.no);

				string unitPrice = itemElement.SelectSingleNode("unitPrice").FirstChild.Value;
				unitPrice = unitPrice.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
				itemInfo.unitPrice = float.Parse(unitPrice);

                itemInfo.unitListPrice = itemInfo.unitPrice;

				if (hashtable[itemInfo.no] == null)
				{
					hashtable.Add(itemInfo.no, itemInfo);
				}
				i++;
			}

			return hashtable;
		}


	}
}
