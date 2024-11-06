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

        public SalesPricesStandard(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = infojetContext.systemDatabase;
            this.infojetContext = infojetContext;
        }

        public SalesPrice getItemPrice(Item item, string currencyCode, string customerNo, string customerPriceGroupCode, string variantCode, float quantity)
        {
            if (currencyCode == null) currencyCode = "";

            SalesPrice salesPrice = null;

            SqlDataReader dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Price") + "] WHERE [Item No_] = '" + item.no + "' AND (([Sales Type] = '0' AND [Sales Code] = '" + customerNo + "') OR ([Sales Type] = '1' AND [Sales Code] = '" + customerPriceGroupCode + "') OR [Sales Type] = '2') AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '" + currencyCode + "' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity + "' ORDER BY [Unit Price]");
            if (dataReader.Read())
            {
                salesPrice = new SalesPrice(database, dataReader);
                dataReader.Close();

            }
            else
            {
                dataReader.Close();


                if (currencyCode != "")
                {
                    dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Price") + "] WHERE [Item No_] = '" + item.no + "' AND (([Sales Type] = '0' AND [Sales Code] = '" + customerNo + "') OR ([Sales Type] = '1' AND [Sales Code] = '" + customerPriceGroupCode + "') OR [Sales Type] = '2') AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity + "' ORDER BY [Unit Price]");
                    if (dataReader.Read())
                    {
                        salesPrice = new SalesPrice(database, dataReader);
                    }
                    else
                    {
                        salesPrice = new SalesPrice(database, null);
                    }

                    dataReader.Close();

                }
                else
                {
                    salesPrice = new SalesPrice(database, null);
                }
            }

            if (salesPrice.unitPrice == 0)
            {
                salesPrice.itemNo = item.no;
                salesPrice.unitPrice = item.unitPrice;
            }

            return salesPrice;

        }

        public Hashtable getItemPrice(DataSet dataSet, string currencyCode, string customerNo, string customerPriceGroupCode, string variantCode, float quantity)
        {
            if (currencyCode == null) currencyCode = "";

            Hashtable itemInfoTable = new Hashtable();

            string whereQuery = "";
            string whereQuery2 = "";

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                if (whereQuery != "")
                {
                    whereQuery = whereQuery + " OR ";
                    whereQuery2 = whereQuery2 + " OR ";
                }
                whereQuery = whereQuery + "[Item No_] = '" + itemNo + "'";
                whereQuery2 = whereQuery2 + "[No_] = '" + itemNo + "'";

                i++;
            }

            string currencyQuery = "[Currency Code] = '" + currencyCode + "'";
            if (currencyCode == infojetContext.generalLedgerSetup.lcyCode) currencyQuery = "[Currency Code] = ''";

            SqlDataReader dataReader = database.query("SELECT [Item No_], [Unit Price] FROM [" + database.getTableName("Sales Price") + "] WITH (NOLOCK) WHERE (" + whereQuery + ") AND (([Sales Type] = '0' AND [Sales Code] = '" + customerNo + "') OR ([Sales Type] = '1' AND [Sales Code] = '" + customerPriceGroupCode + "') OR [Sales Type] = '2') AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND "+currencyQuery+" AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity + "' ORDER BY [Item No_], [Unit Price]");
            while (dataReader.Read())
            {
                string itemNo = dataReader.GetValue(0).ToString();
                float unitPrice = float.Parse(dataReader.GetValue(1).ToString());


                if (itemInfoTable[itemNo] == null)
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfo.unitPrice = unitPrice;
                    itemInfoTable.Add(itemNo, itemInfo);

                }

            }
            dataReader.Close();


            dataReader = database.query("SELECT [No_], [Unit Price] FROM [" + database.getTableName("Item") + "] WITH (NOLOCK) WHERE (" + whereQuery2 + ")");
            while (dataReader.Read())
            {
                string itemNo = dataReader.GetValue(0).ToString();
                float unitPrice = float.Parse(dataReader.GetValue(1).ToString());

                if (itemInfoTable[itemNo] == null)
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfo.unitPrice = unitPrice;
                    itemInfoTable.Add(itemNo, itemInfo);

                }

                if (((ItemInfo)itemInfoTable[itemNo]).unitPrice == 0)
                {
                    ((ItemInfo)itemInfoTable[itemNo]).unitPrice = unitPrice;
                }

            }
            dataReader.Close();

            return itemInfoTable;

        }

        public SalesPrice getItemPrice(Item item, string currencyCode, string customerPriceGroupCode, string variantCode, float quantity)
        {
            if (currencyCode == null) currencyCode = "";
            SalesPrice salesPrice = null;

            SqlDataReader dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Price") + "] WHERE [Item No_] = '" + item.no + "' AND ([Sales Type] = '1' AND [Sales Code] = '" + customerPriceGroupCode + "') AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '" + currencyCode + "' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity + "' ORDER BY [Unit Price]");
            if (dataReader.Read())
            {
                salesPrice = new SalesPrice(database, dataReader);
                dataReader.Close();

            }
            else
            {
                dataReader.Close();

                /*
				if (currencyCode != "")
				{
					dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM ["+database.getTableName("Sales Price")+"] WHERE [Item No_] = '"+item.no+"' AND ([Sales Type] = '1' AND [Sales Code] = '"+customerPriceGroupCode+"') AND ([Starting Date] <= '"+DateTime.Now.ToString("yyyy-MM-dd")+"' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '"+DateTime.Now.ToString("yyyy-MM-dd")+"' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '' AND [Variant Code] = '"+variantCode+"' AND [Minimum Quantity] <= '"+quantity+"' ORDER BY [Unit Price]");
					if (dataReader.Read())
					{
						salesPrice = new SalesPrice(database, dataReader);	
					}
					else
					{
						salesPrice = new SalesPrice(database, null);
					}

					dataReader.Close();

				}
				else
				{*/
                salesPrice = new SalesPrice(database, null);

            }

            if (salesPrice.unitPrice == 0)
            {
                salesPrice.itemNo = item.no;
                salesPrice.unitPrice = item.unitPrice;
            }

            return salesPrice;

        }

        public Hashtable getItemPrice(DataSet dataSet, string currencyCode, string customerPriceGroupCode, string variantCode, float quantity)
        {
            if (currencyCode == null) currencyCode = "";
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
                whereQuery = whereQuery + "[Item No_] = '" + itemNo + "'";

                i++;
            }

            SqlDataReader dataReader = database.query("SELECT [Item No_], [Unit Price] FROM [" + database.getTableName("Sales Price") + "] WHERE (" + whereQuery + ") AND ([Sales Type] = '1' AND [Sales Code] = '" + customerPriceGroupCode + "') AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '" + currencyCode + "' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity + "' ORDER BY [Item No_], [Unit Price]");
            while (dataReader.Read())
            {
                string itemNo = dataReader.GetValue(0).ToString();
                float unitPrice = float.Parse(dataReader.GetValue(1).ToString());

                if (!itemInfoTable.Contains(itemNo))
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfo.unitPrice = unitPrice;
                    itemInfoTable.Add(itemNo, itemInfo);
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
                    Item item = new Item(infojetContext.systemDatabase, itemNo);
                    ((ItemInfo)itemInfoTable[itemNo]).unitPrice = item.unitPrice;
                }

                i++;
            }


            return itemInfoTable;

        }


        public SalesPrice getItemPrice(Item item)
        {
            return getItemPrice(item, "", "", "", "", 1);
        }



        public SalesPrice getItemPrice(Item item, UserSession userSession, string currencyCode)
        {
            return getItemPrice(item, userSession, currencyCode, 1);
        }



        public SalesPrice getItemPrice(Item item, UserSession userSession, string currencyCode, float quantity)
        {
            SalesPrice salesPrice;
            if (userSession != null)
            {
                salesPrice = getItemPrice(item, currencyCode, userSession.customer.no, userSession.customer.customerPriceGroup, "", quantity);
            }
            else
            {
                salesPrice = getItemPrice(item);
            }


            if (currencyCode != salesPrice.currencyCode)
            {
                CurrencyExchangeRates currencyExchangeRates = new CurrencyExchangeRates(database);
                CurrencyExchangeRate currencyExchangeRate = currencyExchangeRates.getCurrentExchangeRate(currencyCode);

                if (currencyExchangeRate != null)
                {
                    salesPrice.unitPrice = (salesPrice.unitPrice / (currencyExchangeRate.relationalExchRateAmount / currencyExchangeRate.exchangeRateAmount));
                    salesPrice.currencyCode = currencyCode;
                }
            }

            return salesPrice;


        }


        public SalesPrice getItemGroupPrice(Item item, string customerPriceGroupCode, string currencyCode)
        {
            SalesPrice salesPrice;
            salesPrice = getItemPrice(item, currencyCode, customerPriceGroupCode, "", 1);

            return salesPrice;

        }

        public Hashtable getItemGroupPrice(DataSet dataSet, string customerPriceGroupCode, string currencyCode)
        {
            return getItemPrice(dataSet, currencyCode, customerPriceGroupCode, "", 1);

        }

        public Hashtable getItemPrice(DataSet dataSet, UserSession userSession, string currencyCode)
        {
            return getItemPrice(dataSet, userSession, currencyCode, 1);
        }

        public Hashtable getItemPrice(DataSet dataSet, UserSession userSession, string currencyCode, float quantity)
        {
            if (userSession != null)
            {
                return getItemPrice(dataSet, currencyCode, userSession.customer.no, userSession.customer.customerPriceGroup, "", quantity);
            }
            else
            {
                return getItemPrice(dataSet, "", "", "", 1);
            }

        }


    }
}
