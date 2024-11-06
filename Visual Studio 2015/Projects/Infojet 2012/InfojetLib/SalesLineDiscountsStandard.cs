using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for SalesPrices.
    /// </summary>
    public class SalesLineDiscountsStandard : SalesLineDiscountsCalculation
    {
        private Infojet infojetContext;
        private string campaignCode = "";

        public SalesLineDiscountsStandard(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
        }

        public void setCampaignCode(string campaignCode)
        {
            this.campaignCode = campaignCode;
        }


        public Hashtable getItemDiscount(DataSet dataSet, string currencyCode, string customerNo, string customerDiscGroupCode)
        {
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
                whereQuery = whereQuery + "i.[No_] = '" + itemNo + "'";

                i++;
            }

            string customerNoFilter = "";
            if (customerNo != "") customerNoFilter = "([Sales Type] = '0' AND [Sales Code] = @customerNo) OR ";

            if (currencyCode == infojetContext.generalLedgerSetup.lcyCode) currencyCode = "";

            string campaignFilter = "";
            if ((campaignCode != "") && (campaignCode != null)) campaignFilter = "OR ([Sales Type] = '3' AND [Sales Code] = @campaignCode)";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT i.[No_], d.[Minimum Quantity], "+infojetContext.systemDatabase.convertField("d.[Line Discount %]")+" FROM [" + infojetContext.systemDatabase.getTableName("Sales Line Discount") + "] d, [" + infojetContext.systemDatabase.getTableName("Item") + "] i WHERE (" + whereQuery + ") AND ((d.[Code] = i.[No_] AND d.[Type] = '0') OR (d.[Code] = i.[Item Disc_ Group] AND d.[Type] = '1')) AND (" + customerNoFilter + " ([Sales Type] = '1' AND [Sales Code] = @customerDiscGroupCode) OR [Sales Type] = '2' " + campaignFilter + ") AND ([Starting Date] <= @startingDate OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= @endingDate OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = @currencyCode AND ((i.[Sales Unit of Measure] = d.[Unit of Measure Code]) OR (d.[Unit of Measure Code] = '')) ORDER BY i.[No_], d.[Minimum Quantity], "+infojetContext.systemDatabase.convertField("d.[Line Discount %]")+" DESC");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("customerDiscGroupCode", customerDiscGroupCode, 20);
            databaseQuery.addDateTimeParameter("startingDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("endingDate", DateTime.Today);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
            if ((campaignCode != "") && (campaignCode != null)) databaseQuery.addStringParameter("campaignCode", campaignCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            while (dataReader.Read())
            {
                string itemNo = dataReader.GetValue(0).ToString();
                float minQuantity = float.Parse(dataReader.GetValue(1).ToString());
                float lineDiscount = float.Parse(dataReader.GetValue(2).ToString());

                ItemInfo itemInfo = null;

                if (!itemInfoTable.Contains(itemNo))
                {
                    itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfo.itemInfoPriceCollection = new ItemInfoPriceCollection();
                    itemInfoTable.Add(itemNo, itemInfo);
                }

                itemInfo = (ItemInfo)itemInfoTable[itemNo];
                //if (minQuantity <= 1)
                //{
                //    if (itemInfo.lineDiscount == 0) itemInfo.lineDiscount = lineDiscount;
                //    if (itemInfo.lineDiscount < lineDiscount) itemInfo.lineDiscount = lineDiscount;
                //}
                if (minQuantity == 0) minQuantity = 1;

                int index = itemInfo.itemInfoPriceCollection.getQuantityPriceIndex(minQuantity);
                if (index == 0)
                {
                    ItemInfoPrice itemInfoPrice = new ItemInfoPrice();
                    itemInfoPrice.minQuantity = minQuantity;
                    itemInfoPrice.lineDiscount = lineDiscount;
                    itemInfo.itemInfoPriceCollection.Add(itemInfoPrice);
                }
                else
                {
                    if (itemInfo.itemInfoPriceCollection[index].lineDiscount < lineDiscount) itemInfo.itemInfoPriceCollection[index].lineDiscount = lineDiscount;
                }
                itemInfoTable[itemNo] = itemInfo;

            }

            dataReader.Close();


            return itemInfoTable;

        }

 
         public Hashtable getItemDiscount(DataSet dataSet, Customer customer, string currencyCode)
        {
            if (customer != null)
            {
                return getItemDiscount(dataSet, currencyCode, customer.no, customer.customerDiscGroup);
            }
            else
            {
                return getItemDiscount(dataSet, currencyCode, "", "");
            }


        }

 
    }
}
