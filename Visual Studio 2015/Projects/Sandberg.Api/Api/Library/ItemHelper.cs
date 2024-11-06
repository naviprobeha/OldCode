using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Library
{
    public class ItemHelper
    {
        public static List<Item> GetItems(string customerNo)
        {
            return GetItems(customerNo, 0, 0);
        }
        public static List<Item> GetItems(string customerNo, int offset, int count)
        {
            Customer customer = CustomerHelper.GetCustomer(customerNo);
            if (customer == null) throw new Exception("Invalid customer: " + customerNo);

            if (count == 0) count = 100;

            List<Item> itemList = new List<Item>();

            string offsetString = "";
            if (offset > 0)
            {
                offsetString = "OFFSET " + offset + " ROWS";
            }
            if (count > 0)
            {
                if (offset == 0)
                {
                    offsetString = "OFFSET 0 ROWS";
                }
                offsetString = offsetString + " FETCH NEXT " + count + " ROWS ONLY";
            }

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT i.[No_], [Description], [Description 2], [Base Unit of Measure], [Item Category Code], [Alt Produktgrupp], [Stock Item Only], [End Date], [Match Code], [Roll Length Code], [Idiom Code], [Match Type Code], [Designer Code], [Pattern Width Code], [Gross Weight], (Stuff((SELECT '; ' + [Test Book Name] AS [text()] FROM (SELECT DISTINCT [Test Book Name] FROM [" + database.getTableName("Item Testbook") + "] b WHERE b.[Item No_] = i.[No_]) x For XML PATH ('')),1,1,'')) as CollectionName, (SELECT TOP 1 [Expected Receipt Date] FROM [" + database.getTableName("Purchase Line") + "] p WHERE p.[Document Type] = 1 AND p.[Type] = 2 AND p.[No_] = i.[No_] ORDER BY [Expected Receipt Date]) as EarliestStockDate, (SELECT TOP 1 s.[Unit Price] FROM ["+database.getTableName("Sales Price")+ "] s WHERE s.[Item No_] = i.[No_] AND ((s.[Sales Type] = 2) OR (s.[Sales Type] = 1 AND s.[Sales Code] = @customerPriceGroup)) AND s.[Currency Code] = @currencyCode AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as UnitPrice, (SELECT TOP 1 s.[Unit Price] FROM [" + database.getTableName("Sales Price") + "] s WHERE s.[Item No_] = i.[No_] AND s.[Sales Type] = 1 AND s.[Sales Code] = 'REK' AND s.[Currency Code] = @currencyCode AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as RetailPrice, (SELECT TOP 1 s.[Line Discount %] FROM [" + database.getTableName("Sales Line Discount") + "] s WHERE ((s.[Type] = 0 AND s.[Code] = i.[No_]) OR (s.[Type] = 1 AND s.[Code] = i.[Item Disc_ Group])) AND ((s.[Sales Type] = 0 AND s.[Sales Code] = @customerNo) OR (s.[Sales Type] = 1 AND s.[Sales Code] = @customerDiscGroup) OR (s.[Sales Type]=2)) AND (s.[Currency Code] = @currencyCode OR s.[Currency Code] = '') AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as LineDiscountProc, (SELECT TOP 1 c.[Valid until] FROM [" + database.getTableName("Testbook_Customer") + "] c, [" + database.getTableName("Item Testbook") + "] d WHERE d.[Item No_] = i.[No_] AND d.[Test Book Code] = c.[Testbook Code] AND c.[Customer No_] = @customerNo) as validUntil, [VAT Prod_ Posting Group] FROM [" + database.getTableName("Item") + "] i WHERE i.[Web Enabled] = 1 ORDER BY i.[No_] " + offsetString);
            databaseQuery.addStringParameter("customerPriceGroup", customer.customer_price_group, 20);
            databaseQuery.addStringParameter("customerDiscGroup", customer.customer_disc_group, 20);
            databaseQuery.addStringParameter("customerNo", customer.no, 20);
            databaseQuery.addDateTimeParameter("startingDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("endingDate", DateTime.Today);
            databaseQuery.addStringParameter("currencyCode", customer.currency_code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                Item item = new Item(dataReader);
                itemList.Add(item);
            }

            dataReader.Close();

            database.close();

            return itemList;
        }

        public static Item GetItem(string customerNo, string no)
        {
            Customer customer = CustomerHelper.GetCustomer(customerNo);
            if (customer == null) throw new Exception("Invalid customer: " + customerNo);

            Item item = new Item();

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);



            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Description], [Description 2], [Base Unit of Measure], [Item Category Code], [Alt Produktgrupp], [Stock Item Only], [End Date], [Match Code], [Roll Length Code], [Idiom Code], [Match Type Code], [Designer Code], [Pattern Width], [Pattern Width Code], [Gross Weight], (Stuff((SELECT '; ' + [Test Book Name] AS [text()] FROM (SELECT DISTINCT [Test Book Name] FROM [" + database.getTableName("Item Testbook")+ "] b WHERE b.[Item No_] = i.[No_]) x For XML PATH ('')),1,1,'')) as CollectionName, (SELECT TOP 1 [Expected Receipt Date] FROM [" + database.getTableName("Purchase Line")+ "] p WHERE p.[Document Type] = 1 AND p.[Type] = 2 AND p.[No_] = i.[No_] AND p.[Outstanding Quantity] > 0 ORDER BY [Expected Receipt Date]) as EarliestStockDate, (SELECT TOP 1 s.[Unit Price] FROM [" + database.getTableName("Sales Price") + "] s WHERE s.[Item No_] = i.[No_] AND ((s.[Sales Type] = 2) OR (s.[Sales Type] = 1 AND s.[Sales Code] = @customerPriceGroup)) AND s.[Currency Code] = @currencyCode AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as UnitPrice, (SELECT TOP 1 s.[Unit Price] FROM [" + database.getTableName("Sales Price") + "] s WHERE s.[Item No_] = i.[No_] AND s.[Sales Type] = 1 AND s.[Sales Code] = 'REK' AND s.[Currency Code] = @currencyCode AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as RetailPrice, (SELECT TOP 1 s.[Line Discount %] FROM [" + database.getTableName("Sales Line Discount") + "] s WHERE ((s.[Type] = 0 AND s.[Code] = i.[No_]) OR (s.[Type] = 1 AND s.[Code] = i.[Item Disc_ Group])) AND ((s.[Sales Type] = 0 AND s.[Sales Code] = @customerNo) OR (s.[Sales Type] = 1 AND s.[Sales Code] = @customerDiscGroup) OR (s.[Sales Type]=2)) AND (s.[Currency Code] = @currencyCode OR s.[Currency Code] = '') AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as LineDiscountProc, (SELECT TOP 1 c.[Valid until] FROM [" + database.getTableName("Testbook_Customer") + "] c, [" + database.getTableName("Item Testbook") + "] d WHERE d.[Item No_] = i.[No_] AND d.[Test Book Code] = c.[Testbook Code] AND c.[Customer No_] = @customerNo) as validUntil, [VAT Prod_ Posting Group] FROM [" + database.getTableName("Item") + "] i WHERE i.[No_] = @no");
            databaseQuery.addStringParameter("no", no, 20);
            databaseQuery.addStringParameter("customerPriceGroup", customer.customer_price_group, 20);
            databaseQuery.addStringParameter("customerDiscGroup", customer.customer_disc_group, 20);
            databaseQuery.addStringParameter("customerNo", customer.no, 20);
            databaseQuery.addDateTimeParameter("startingDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("endingDate", DateTime.Today);

            if (customer.currency_code != "SEK")
            {
                databaseQuery.addStringParameter("currencyCode", customer.currency_code, 20);
            }
            else
            {
                databaseQuery.addStringParameter("currencyCode", "", 20);

            }

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                item = new Item(dataReader);

                dataReader.Close();

                /*
                //Total availability
                DatabaseQuery databaseQuery4 = database.prepare("SELECT SUM([Remaining Quantity]) as Qty FROM [" + database.getTableName("Item Ledger Entry") + "] WHERE [Item No_] = @itemNo AND [Open] = 1");
                databaseQuery4.addStringParameter("itemNo", no, 20);

                dataReader = databaseQuery4.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        item.available_stock = dataReader.GetDecimal(0);
                    }

                }

                dataReader.Close();
                */

                DatabaseQuery databaseQuery5 = database.prepare("SELECT SUM([Outstanding Quantity]) as Qty FROM [" + database.getTableName("Sales Line") + "] WHERE [Document Type] = 1 AND [Type] = 2 AND [No_] = @itemNo");
                databaseQuery5.addStringParameter("itemNo", no, 20);

                dataReader = databaseQuery5.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        item.available_stock = item.available_stock - dataReader.GetDecimal(0);
                    }
                }

                dataReader.Close();
                //


                DatabaseQuery databaseQuery2 = database.prepare("SELECT [Item Lot No], SUM([Remaining Quantity]) as Qty FROM [" + database.getTableName("Item Ledger Entry") + "] WHERE [Item No_] = @itemNo AND [Open] = 1 GROUP BY [Item Lot No]");
                databaseQuery2.addStringParameter("itemNo", no, 20);

                dataReader = databaseQuery2.executeQuery();
                while (dataReader.Read())
                {
                    ItemBatch itemBatch = new ItemBatch(dataReader);
                    item.batches.Add(itemBatch);

                }

                dataReader.Close();

                Dictionary<string, ItemBatch> salesBatches = new Dictionary<string, ItemBatch>();

                DatabaseQuery databaseQuery3 = database.prepare("SELECT [Item Lot No_] as [Item Lot No], SUM([Outstanding Quantity]) as Qty FROM [" + database.getTableName("Sales Line") + "] WHERE [Document Type] = 1 AND [Type] = 2 AND [No_] = @itemNo GROUP BY [Item Lot No_]");
                databaseQuery3.addStringParameter("itemNo", no, 20);

                dataReader = databaseQuery3.executeQuery();
                while (dataReader.Read())
                {
                    ItemBatch itemBatch = new ItemBatch(dataReader);
                    salesBatches.Add(dataReader["Item Lot No"].ToString(), itemBatch);

                }

                dataReader.Close();

                item.available_stock = 0;
                for (int i = 0; i < item.batches.Count; i++)
                {
                    
                    if (salesBatches.ContainsKey(item.batches[i].no))
                    {
                        item.batches[i].stock_level = item.batches[i].stock_level - salesBatches[item.batches[i].no].stock_level;
                    }
                    if (item.available_stock == 0) item.available_stock = item.batches[i].stock_level;
                    if (item.batches[i].stock_level > item.available_stock) item.available_stock = item.batches[i].stock_level;
                }

            }

            if (!dataReader.IsClosed) dataReader.Close();

            if ((item != null) && (item.vat_prod_posting_group != null))
            {
                item.unit_price_incl_vat = item.unit_price;
                item.net_price_incl_vat = item.net_price;

                //VAT
                DatabaseQuery vatDatabaseQuery = database.prepare("SELECT [VAT %] FROM [" + database.getTableName("VAT Posting Setup") + "] WHERE [VAT Bus_ Posting Group] = @vatBusPostingGroup AND [VAT Prod_ Posting Group] = @vatProdPostingGroup AND [VAT Calculation Type] = 0");
                vatDatabaseQuery.addStringParameter("vatBusPostingGroup", customer.vat_bus_posting_group, 20);
                vatDatabaseQuery.addStringParameter("vatProdPostingGroup", item.vat_prod_posting_group, 20);

                SqlDataReader vatDataReader = vatDatabaseQuery.executeQuery();
                if (vatDataReader.Read())
                {
                    decimal vatProc = vatDataReader.GetDecimal(0);
                    item.unit_price_incl_vat = item.unit_price * (1 + (vatProc / 100));
                    item.net_price_incl_vat = item.net_price * (1 + (vatProc / 100));
                }
            }

            if (item != null)
            {
                item.sample_products = new List<SampleItem>();

                DatabaseQuery sampleQuery = database.prepare("SELECT [No_], [Description] FROM [" + database.getTableName("Item") + "] WHERE ([No_] = @no1 OR [No_] = @no2 OR [No_] = @no3) AND [Web Enabled] = 1");
                sampleQuery.addStringParameter("no1", "P" + no, 20);
                sampleQuery.addStringParameter("no2", "B" + no, 20);
                sampleQuery.addStringParameter("no3", "A" + no, 20);

                SqlDataReader sampleReader = sampleQuery.executeQuery();
                while (sampleReader.Read())
                {
                    SampleItem sampleItem = new SampleItem(sampleReader);
                    item.sample_products.Add(sampleItem);                   
                }

                dataReader.Close();
            }

            database.close();

            return item;
        }


        public static List<Item> SearchItems(string customerNo, string no, string description, string collectionName, string itemCategory, int offset, int count)
        {
            Customer customer = CustomerHelper.GetCustomer(customerNo);
            if (customer == null) throw new Exception("Invalid customer: " + customerNo);

            List<Item> itemList = new List<Item>();

            string offsetString = "";
            if (offset > 0)
            {
                offsetString = "OFFSET " + offset + " ROWS";
            }
            if (count > 0)
            {
                if (offset == 0)
                {
                    offsetString = "OFFSET 0 ROWS";
                }
                offsetString = offsetString + " FETCH NEXT " + count + " ROWS ONLY";
            }

            string query = "";
            if (no != "")
            {
                query = query + " AND i.[No_] = @no ";
            }
            if (description != "")
            {
                query = query + " AND UPPER(i.[Description]) LIKE @description ";
            }
            if (collectionName != "")
            {
                query = query + " AND UPPER(t.[Test Book Name]) LIKE @collectionName ";
            }
            if (itemCategory != "")
            {
                query = query + " AND UPPER(i.[Item Category Code]) LIKE @itemCategoryCode ";
            }
            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Description], [Description 2], [Base Unit of Measure], [Item Category Code], [Alt Produktgrupp], [Stock Item Only], [End Date], [Match Code], [Roll Length Code], [Idiom Code], [Match Type Code], [Designer Code], [Pattern Width Code], [Gross Weight], (Stuff((SELECT '; ' + [Test Book Name] AS [text()] FROM (SELECT TOP 1 [Test Book Name] FROM [" + database.getTableName("Item Testbook") + "] b WHERE b.[Item No_] = i.[No_]) x For XML PATH ('')),1,1,'')) as CollectionName, (SELECT TOP 1 [Expected Receipt Date] FROM [" + database.getTableName("Purchase Line") + "] p WHERE p.[Document Type] = 1 AND p.[Type] = 2 AND p.[No_] = i.[No_] AND p.[Outstanding Quantity] > 0 ORDER BY [Expected Receipt Date]) as EarliestStockDate, (SELECT TOP 1 s.[Unit Price] FROM [" + database.getTableName("Sales Price") + "] s WHERE s.[Item No_] = i.[No_] AND ((s.[Sales Type] = 2) OR (s.[Sales Type] = 1 AND s.[Sales Code] = @customerPriceGroup)) AND s.[Currency Code] = @currencyCode AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as UnitPrice, (SELECT TOP 1 s.[Unit Price] FROM [" + database.getTableName("Sales Price") + "] s WHERE s.[Item No_] = i.[No_] AND s.[Sales Type] = 1 AND s.[Sales Code] = 'REK' AND s.[Currency Code] = @currencyCode AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as RetailPrice, (SELECT TOP 1 s.[Line Discount %] FROM [" + database.getTableName("Sales Line Discount") + "] s WHERE ((s.[Type] = 0 AND s.[Code] = i.[No_]) OR (s.[Type] = 1 AND s.[Code] = i.[Item Disc_ Group])) AND ((s.[Sales Type] = 0 AND s.[Sales Code] = @customerNo) OR (s.[Sales Type] = 1 AND s.[Sales Code] = @customerDiscGroup) OR (s.[Sales Type]=2)) AND (s.[Currency Code] = @currencyCode OR s.[Currency Code] = '') AND s.[Starting Date] <= @startingDate AND (s.[Ending Date] = '1753-01-01' OR s.[Ending Date] >= @endingDate)) as LineDiscountProc, (SELECT TOP 1 c.[Valid until] FROM [" + database.getTableName("Testbook_Customer") + "] c, [" + database.getTableName("Item Testbook") + "] d WHERE d.[Item No_] = i.[No_] AND d.[Test Book Code] = c.[Testbook Code] AND c.[Customer No_] = @customerNo) as validUntil, [VAT Prod_ Posting Group] FROM [" + database.getTableName("Item") + "] i LEFT JOIN [" + database.getTableName("Item Testbook") + "] t ON i.[No_] = t.[Item No_] WHERE i.[Web Enabled] = 1 " + query+" ORDER BY [No_] "+offsetString);
            databaseQuery.addStringParameter("no", no.ToUpper(), 30);
            databaseQuery.addStringParameter("description", '%' + description.ToUpper() + '%', 60);
            databaseQuery.addStringParameter("collectionName", '%' + collectionName.ToUpper() + '%', 60);
            databaseQuery.addStringParameter("itemCategoryCode", '%' + itemCategory.ToUpper() + '%', 20);
            databaseQuery.addStringParameter("customerPriceGroup", customer.customer_price_group, 20);
            databaseQuery.addStringParameter("customerDiscGroup", customer.customer_disc_group, 20);
            databaseQuery.addStringParameter("customerNo", customer.no, 20);
            databaseQuery.addStringParameter("currencyCode", customer.currency_code, 20);
            databaseQuery.addDateTimeParameter("startingDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("endingDate", DateTime.Today);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                Item item = new Item(dataReader);
                if (itemList.FirstOrDefault(i => i.no == item.no) == null)
                {
                    itemList.Add(item);
                }
            }

            dataReader.Close();

            database.close();

            return itemList;
        }
    }
}