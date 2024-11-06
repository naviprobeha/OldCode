using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for SalesPrices.
    /// </summary>
    public class SalesPricesStandard : SalesPricesCalculation
    {
        private Database database;
        private Infojet infojetContext;
        private string campaignCode;

        public SalesPricesStandard(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = infojetContext.systemDatabase;
            this.infojetContext = infojetContext;
        }

        public void setCampaignCode(string campaignCode)
        {
            this.campaignCode = campaignCode;
        }

        public Hashtable getItemPrice(DataSet dataSet, string currencyCode, string customerNo, string customerPriceGroupCode)
        {
            Hashtable itemUnitOfMeasureTable = ItemUnitOfMeasure.getItemSalesUnitTable(infojetContext, dataSet);

            Hashtable itemInfoTable = new Hashtable();

            string whereQuery = "";

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                if (whereQuery != "")
                {
                    whereQuery = whereQuery + " OR ";
                }
                whereQuery = whereQuery + "s.[Item No_] = '" + itemNo + "'";

                i++;
            }

            string currencyQuery = "s.[Currency Code] = '" + currencyCode + "'";
            if (currencyCode == infojetContext.generalLedgerSetup.lcyCode) currencyQuery = "s.[Currency Code] = ''";

            string customerNoFilter = "";
            if (customerNo != "") customerNoFilter = "(s.[Sales Type] = '0' AND s.[Sales Code] = '" + customerNo + "') OR ";

            //if (whereQuery.Contains("8825")) throw new Exception("SQL: " + "SELECT s.[Item No_], s.[Minimum Quantity], s.[Unit Price] FROM [" + database.getTableName("Sales Price") + "] s, [" + database.getTableName("Item") + "] i WHERE (" + whereQuery + ") AND (" + customerNoFilter + " (s.[Sales Type] = '1' AND s.[Sales Code] = '" + customerPriceGroupCode + "') OR s.[Sales Type] = '2' OR (s.[Sales Type] = '3' AND s.[Sales Code] = '" + campaignCode + "')) AND (s.[Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR s.[Starting Date] = '1753-01-01 00:00:00') AND (s.[Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR s.[Ending Date] = '1753-01-01 00:00:00') AND " + currencyQuery + " AND i.[No_] = s.[Item No_] AND ((i.[Sales Unit of Measure] = s.[Unit of Measure Code]) OR (s.[Unit of Measure Code] = '')) ORDER BY s.[Item No_], s.[Minimum Quantity], s.[Unit Price]");

            SqlDataReader dataReader = database.query("SELECT s.[Item No_], s.[Minimum Quantity], s.[Unit Price], s.[Unit of Measure Code], s.[Price Includes VAT], (SELECT " + database.convertField("[VAT %]") + " FROM [" + database.getTableName("VAT Posting Setup") + "] WHERE [VAT Bus_ Posting Group] = s.[VAT Bus_ Posting Gr_ (Price)] AND [VAT Prod_ Posting Group] = i.[VAT Prod_ Posting Group]) as vatProc, [Variant Code] FROM [" + database.getTableName("Sales Price") + "] s, [" + database.getTableName("Item") + "] i WHERE (" + whereQuery + ") AND (" + customerNoFilter + " (s.[Sales Type] = '1' AND s.[Sales Code] = '" + customerPriceGroupCode + "') OR s.[Sales Type] = '2' OR (s.[Sales Type] = '3' AND s.[Sales Code] = '" + campaignCode + "')) AND (s.[Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR s.[Starting Date] = '1753-01-01 00:00:00') AND (s.[Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR s.[Ending Date] = '1753-01-01 00:00:00') AND " + currencyQuery + " AND i.[No_] = s.[Item No_] AND ((i.[Sales Unit of Measure] = s.[Unit of Measure Code]) OR (s.[Unit of Measure Code] = '')) AND s.[Unit Price] > 0 ORDER BY s.[Item No_], s.[Minimum Quantity], s.[Unit Price]");
            while (dataReader.Read())
            {
                string itemNo = dataReader.GetValue(0).ToString();
                float minQuantity = float.Parse(dataReader.GetValue(1).ToString());
                float unitPrice = float.Parse(dataReader.GetValue(2).ToString());
                string unitOfMeasureCode = dataReader.GetValue(3).ToString();
                
                bool priceIncludesVat = false;
                if (dataReader.GetValue(4).ToString() == "1") priceIncludesVat = true;

                float vatProc = 0;
                if (!dataReader.IsDBNull(5)) vatProc = float.Parse(dataReader.GetValue(5).ToString());

                if (priceIncludesVat)
                {
                    unitPrice = unitPrice / (1+(vatProc/100));
                }

                ItemUnitOfMeasure itemUnitOfMeasure = (ItemUnitOfMeasure)itemUnitOfMeasureTable[itemNo];
                if (itemUnitOfMeasure == null) itemUnitOfMeasure = new ItemUnitOfMeasure();

                bool include = true;
                if ((unitOfMeasureCode != itemUnitOfMeasure.salesUnitOfMeasureCode) && (unitOfMeasureCode != itemUnitOfMeasure.baseUnitOfMeasureCode) && (unitOfMeasureCode != "")) include = false;

                if (include)
                {
                    if ((unitOfMeasureCode == itemUnitOfMeasure.baseUnitOfMeasureCode) || (unitOfMeasureCode == ""))
                    {
                        unitPrice = unitPrice * itemUnitOfMeasure.qtyPerUnitOfMeasure;
                    }

                    ItemInfo itemInfo = null;

                    if (!itemInfoTable.Contains(itemNo))
                    {
                        itemInfo = new ItemInfo();
                        itemInfo.no = itemNo;
                        itemInfo.itemInfoPriceCollection = new ItemInfoPriceCollection();
                        itemInfoTable.Add(itemNo, itemInfo);
                    }

                    itemInfo = (ItemInfo)itemInfoTable[itemNo];
                    if (minQuantity <= 1)
                    {
                        if (itemInfo.unitPrice == 0) itemInfo.unitPrice = unitPrice;
                        if (itemInfo.unitPrice > unitPrice) itemInfo.unitPrice = unitPrice;
                    }

                    if (minQuantity > 0)
                    {

                        int index = itemInfo.itemInfoPriceCollection.getQuantityPriceIndex(minQuantity);
                        if (index == -1)
                        {
                            ItemInfoPrice itemInfoPrice = new ItemInfoPrice();
                            itemInfoPrice.minQuantity = minQuantity;
                            itemInfoPrice.unitPrice = unitPrice;
                            itemInfo.itemInfoPriceCollection.Add(itemInfoPrice);
                        }
                        else
                        {
                            if (itemInfo.itemInfoPriceCollection[index].unitPrice > unitPrice) itemInfo.itemInfoPriceCollection[index].unitPrice = unitPrice;
                        }
                    }
                    itemInfoTable[itemNo] = itemInfo;
                }
            }
            dataReader.Close();



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
                if (((ItemInfo)itemInfoTable[itemNo]).unitPrice == 0)
                {                   
                    Item item = Item.get(infojetContext, itemNo);                    
                    ((ItemInfo)itemInfoTable[itemNo]).unitPrice = item.originalPrice;

                    ItemUnitOfMeasure itemUnitOfMeasure = (ItemUnitOfMeasure)itemUnitOfMeasureTable[itemNo];
                    if (itemUnitOfMeasure == null) itemUnitOfMeasure = new ItemUnitOfMeasure();
                    if (itemUnitOfMeasure.baseUnitOfMeasureCode != itemUnitOfMeasure.salesUnitOfMeasureCode)
                    {
                        ((ItemInfo)itemInfoTable[itemNo]).unitPrice = ((ItemInfo)itemInfoTable[itemNo]).unitPrice * itemUnitOfMeasure.qtyPerUnitOfMeasure;
                    }

                }

                i++;
            }

            return itemInfoTable;

        }

 
        public Hashtable getItemPrice(DataSet dataSet, string customerPriceGroupCode, string currencyCode)
        {
            return getItemPrice(dataSet, currencyCode, "", customerPriceGroupCode);
        }

        public Hashtable getItemPrice(DataSet dataSet, Customer customer, string currencyCode)
        {
            if (customer != null)
            {
                return getItemPrice(dataSet, currencyCode, customer.no, customer.customerPriceGroup);
            }
            else
            {
                return getItemPrice(dataSet, currencyCode, "", "");
            }

        }


    }
}
