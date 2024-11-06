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

		public Items()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public Hashtable getItemInfo(DataSet itemDataSet, Infojet infojetContext, bool calcPrices, bool calcInventory)
		{
			//itemDataSet is a DataSet with itemNo as first column.

			if ((infojetContext.webSite.priceInventoryCalcMethod == 0) && (infojetContext.userSession != null))
			{

				ItemListServiceArgument itemListServiceArgument = new ItemListServiceArgument();

				itemListServiceArgument.setLocationCode(infojetContext.webSite.locationCode);
				itemListServiceArgument.setCustomerNo(infojetContext.userSession.customer.no);
				itemListServiceArgument.setCurrencyCode(infojetContext.currencyCode);

				if (infojetContext.userSession.customer.locationCode != "") itemListServiceArgument.setLocationCode(infojetContext.userSession.customer.locationCode);

				itemListServiceArgument.setItemDataSet(itemDataSet);
				return appServerRequest(infojetContext, new ServiceRequest(infojetContext, "getItemInfo", itemListServiceArgument));
			}
			else
			{
				return offlinePriceInventoryCalculation(itemDataSet, infojetContext, calcPrices, calcInventory);
			}
		}

        public Hashtable getItemInfo(Item item, Infojet infojetContext, float quantity)
        {
            return getItemInfo(item, infojetContext, quantity, true, true);
        }

		public Hashtable getItemInfo(Item item, Infojet infojetContext, float quantity, bool calcPrices, bool calcInventory)
		{
			if ((infojetContext.webSite.priceInventoryCalcMethod == 0) && (infojetContext.userSession != null))
			{
				item.setLocationCode(infojetContext.webSite.locationCode);
				item.setCustomerNo(infojetContext.userSession.customer.no);
				item.setCurrencyCode(infojetContext.currencyCode);
				item.setQuantity(quantity);

				if (infojetContext.userSession.customer.locationCode != "") item.setLocationCode(infojetContext.userSession.customer.locationCode);

				return appServerRequest(infojetContext, new ServiceRequest(infojetContext, "getItemInfo", item));
			
			}
			else
			{
				
				Hashtable itemInfoTable = new Hashtable();
				itemInfoTable.Add(item.no, offlinePriceInventoryCalculation(item, infojetContext, quantity, calcPrices, calcInventory));
				
                return itemInfoTable;
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

        private Hashtable offlinePriceInventoryCalculation(DataSet dataSet, Infojet infojetContext, bool calcPrices, bool calcInventory)
		{
			Hashtable itemInfoTable = new Hashtable();
            if (dataSet.Tables[0].Rows.Count == 0) return itemInfoTable;

            if (calcInventory)
            {
                ItemInventory itemInventory = new ItemInventory(infojetContext.systemDatabase);
                itemInventory.calcOfflineInventory(dataSet, infojetContext.webSite.locationCode, ref itemInfoTable);
                itemInventory.getNextPlannedReceipt(dataSet, infojetContext.webSite.locationCode, ref itemInfoTable);


            }

            if (calcPrices)
            {
                SalesPrices salesPrices = new SalesPrices(infojetContext.systemDatabase, infojetContext);

                WebSiteLanguage webSiteLanguage = new WebSiteLanguage(infojetContext.systemDatabase, infojetContext.webSite.code, infojetContext.languageCode);

                Hashtable salesPriceTable = salesPrices.getItemGroupPrice(dataSet, webSiteLanguage.recPriceGroupCode, infojetContext.currencyCode);
                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                    if (salesPriceTable.Contains(itemNo))
                    {
                        ItemInfo itemInfo = (ItemInfo)salesPriceTable[itemNo];

                        if (itemInfoTable[itemNo] == null)
                        {
                            itemInfoTable.Add(itemNo, itemInfo);
                        }
                        ((ItemInfo)itemInfoTable[itemNo]).unitListPrice = itemInfo.unitPrice;

                    }

                    i++;
                }


                if (infojetContext.userSession != null)
                {
                    Hashtable salesPriceTable2 = salesPrices.getItemPrice(dataSet, infojetContext.userSession, infojetContext.currencyCode, 1);

                    SalesLineDiscounts salesLineDiscounts = new SalesLineDiscounts(infojetContext.systemDatabase, infojetContext);
                    Hashtable discountTable = salesLineDiscounts.getItemDiscount(dataSet, infojetContext.userSession, infojetContext.currencyCode, 1);
                    
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


                        }

                        if ((((ItemInfo)itemInfoTable[itemNo]).unitPrice == ((ItemInfo)itemInfoTable[itemNo]).unitListPrice) || (!infojetContext.webSite.dontAllowCustPriceAndDisc))
                        {
                            if (discountTable.Contains(itemNo))
                            {
                                ItemInfo discountInfo = (ItemInfo)discountTable[itemNo];
                                ((ItemInfo)itemInfoTable[itemNo]).unitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitPrice - (((ItemInfo)itemInfoTable[itemNo]).unitPrice * (discountInfo.lineDiscount / 100));
                                Decimal amount = new Decimal(((ItemInfo)itemInfoTable[itemNo]).unitPrice);
                                amount = Math.Round(amount, 2);
                                ((ItemInfo)itemInfoTable[itemNo]).unitPrice = Convert.ToSingle(amount);

                            }
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



			return itemInfoTable;

		}

        private ItemInfo offlinePriceInventoryCalculation(Item item, Infojet infojetContext, float quantity, bool calcPrices, bool calcInventory)
		{
			ItemInfo itemInfo = new ItemInfo();

			itemInfo.no = item.no;

            if (calcPrices)
            {
                SalesPrices salesPrices = new SalesPrices(infojetContext.systemDatabase, infojetContext);

                WebSiteLanguage webSiteLanguage = new WebSiteLanguage(infojetContext.systemDatabase, infojetContext.webSite.code, infojetContext.languageCode);

                SalesPrice recPrice = salesPrices.getItemGroupPrice(item, webSiteLanguage.recPriceGroupCode, infojetContext.currencyCode);
                itemInfo.unitListPrice = recPrice.unitPrice;

                if (infojetContext.userSession != null)
                {
                    SalesPrice salesPrice = salesPrices.getItemPrice(item, infojetContext.userSession, infojetContext.currencyCode, quantity);
                    itemInfo.unitPrice = salesPrice.unitPrice;

                    if ((itemInfo.unitPrice == itemInfo.unitListPrice) || (!infojetContext.webSite.dontAllowCustPriceAndDisc))
                    {
                        SalesLineDiscounts salesLineDiscounts = new SalesLineDiscounts(infojetContext.systemDatabase, infojetContext);
                        SalesLineDiscount salesLineDiscount = salesLineDiscounts.getItemDiscount(item, infojetContext.userSession, infojetContext.currencyCode, quantity);
                        if (salesLineDiscount != null)
                        {
                            itemInfo.unitPrice = itemInfo.unitPrice - (itemInfo.unitPrice * (salesLineDiscount.lineDiscount / 100));
                            Decimal amount = new Decimal(itemInfo.unitPrice);
                            amount = Math.Round(amount, 2);
                            itemInfo.unitPrice = Convert.ToSingle(amount);
                        }
                    }
                }
                else
                {
                    itemInfo.unitPrice = itemInfo.unitListPrice;
                }

            }

            itemInfo.itemReceiptInfoCollection = new ItemReceiptInfoCollection();

            if (calcInventory)
            {
                itemInfo.inventory = item.getOfflineInventory(infojetContext.webSite.locationCode);

                itemInfo.itemReceiptInfoCollection = item.getNextPlannedReceipt(infojetContext.webSite.locationCode);
            }

            return itemInfo;
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
				
				Item item = new Item(infojetContext.systemDatabase, itemInfo.no);

				string unitPrice = itemElement.SelectSingleNode("unitPrice").FirstChild.Value;
				unitPrice = unitPrice.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
				itemInfo.unitPrice = float.Parse(unitPrice);
			
				SalesPrices salesPrices = new SalesPrices(infojetContext.systemDatabase, infojetContext);
				SalesPrice recPrice = salesPrices.getItemGroupPrice(item, webSiteLanguage.recPriceGroupCode, infojetContext.currencyCode);							
				itemInfo.unitListPrice = recPrice.unitPrice;

				if (hashtable[itemInfo.no] == null)
				{
					hashtable.Add(itemInfo.no, itemInfo);
				}
				i++;
			}

			return hashtable;
		}

		public string getInventoryText(Item item, float inventory, Infojet infojetContext, bool showLeadTime)
		{
			if ((infojetContext.webSite.showInventoryAs == 0) || (infojetContext.webSite.showInventoryAs == 2)) return inventory.ToString();
			if (infojetContext.webSite.showInventoryAs == 1)
			{
				if (inventory > 0) 
				{
					return infojetContext.translate(infojetContext.webSite.inStockTextConstantCode);
				}
				else
				{
					return infojetContext.translate(infojetContext.webSite.noInStockTextConstantCode);
				}
			}
			if (infojetContext.webSite.showInventoryAs == 3)
			{
				if (inventory > 0) 
				{
					return infojetContext.translate(infojetContext.webSite.inStockTextConstantCode);
				}
				else
				{
                    if (showLeadTime)
                    {
                        if (item.leadTimeCalculation != "")
                        {
                            return infojetContext.translate(infojetContext.webSite.leadTimeTextConstantCode) + ": " + item.leadTimeCalculation;
                        }
                        else
                        {
                            return infojetContext.translate(infojetContext.webSite.noInStockTextConstantCode);
                        }
                    }
                    else
                    {
                        return infojetContext.translate(infojetContext.webSite.noInStockTextConstantCode);
                    }
				}
			}
            if ((infojetContext.webSite.showInventoryAs == 4) && (inventory < 0)) return infojetContext.translate(infojetContext.webSite.noInStockTextConstantCode);

			return inventory.ToString();
		}


	}
}
