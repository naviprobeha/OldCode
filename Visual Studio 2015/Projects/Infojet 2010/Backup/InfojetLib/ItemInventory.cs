using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for ItemInventory.
    /// </summary>
    public class ItemInventory
    {
        private Database database;

        public ItemInventory(Database database)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;
        }

        public void calcOfflineInventory(DataSet dataSet, string locationCode, ref Hashtable itemInfoTable)
        {

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


            DatabaseQuery databaseQuery = database.prepare("SELECT [Item No_], SUM([Remaining Quantity]) FROM [" + database.getTableName("Item Ledger Entry") + "] WITH (NOLOCK) WHERE (" + whereQuery + ") AND [Location Code] = @locationCode AND [Open] = '1' GROUP BY [Item No_]");
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet itemInventoryDataSet = new DataSet();
            dataAdapter.Fill(itemInventoryDataSet);

            i = 0;
            while (i < itemInventoryDataSet.Tables[0].Rows.Count)
            {
                string itemNo = itemInventoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                float inventory = 0;
                try
                {
                    inventory = float.Parse(itemInventoryDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                }
                catch (Exception) { }

                if (!itemInfoTable.Contains(itemNo))
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfoTable.Add(itemNo, itemInfo);
                }
                ((ItemInfo)itemInfoTable[itemNo]).inventory = inventory;

                i++;
            }

            databaseQuery = database.prepare("SELECT [No_], SUM([Outstanding Quantity]) FROM [" + database.getTableName("Sales Line") + "] WITH (NOLOCK) WHERE (" + whereQuery2 + ") AND [Location Code] = @locationCode AND [Document Type] = '1' GROUP BY [No_]");
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet salesDataSet = new DataSet();
            dataAdapter.Fill(salesDataSet);

            i = 0;
            while (i < salesDataSet.Tables[0].Rows.Count)
            {
                string itemNo = salesDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                float outstandingQty = 0;
                try
                {
                    outstandingQty = float.Parse(salesDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                }
                catch (Exception) { }

                if (!itemInfoTable.Contains(itemNo))
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfoTable.Add(itemNo, itemInfo);
                }
                ((ItemInfo)itemInfoTable[itemNo]).inventory = ((ItemInfo)itemInfoTable[itemNo]).inventory - outstandingQty;

                i++;
            }


            databaseQuery = database.prepare("SELECT l.[Item No_], SUM(l.[Quantity]) FROM [" + database.getTableName("Web Sales Line") + "] l, [" + database.getTableName("Web Sales Header") + "] h, [" + database.getTableName("Customer") + "] c WITH (NOLOCK) WHERE l.[Document Type] = '1' AND (" + whereQuery + ") AND h.[Document Type] = l.[Document Type] AND h.[No_] = l.[Document No_] AND c.[No_] = h.[Sell-to Customer No_] AND c.[Location Code] = @locationCode AND h.[Transfered] = 0 GROUP BY [Item No_]");
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet webSalesDataSet = new DataSet();
            dataAdapter.Fill(webSalesDataSet);

            i = 0;
            while (i < webSalesDataSet.Tables[0].Rows.Count)
            {
                string itemNo = webSalesDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                float outstandingQty = 0;
                try
                {
                    outstandingQty = float.Parse(webSalesDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                }
                catch (Exception) { }

                if (!itemInfoTable.Contains(itemNo))
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfoTable.Add(itemNo, itemInfo);
                }
                ((ItemInfo)itemInfoTable[itemNo]).inventory = ((ItemInfo)itemInfoTable[itemNo]).inventory - outstandingQty;

                i++;
            }


        }


        public float calcOfflineInventory(Item item, string locationCode)
        {

            float inventory = 0;

            DatabaseQuery databaseQuery = database.prepare("SELECT SUM([Remaining Quantity]) FROM [" + database.getTableName("Item Ledger Entry") + "] WITH (NOLOCK) WHERE [Item No_] = @itemNo AND [Location Code] = @locationCode AND [Open] = '1'");
            databaseQuery.addStringParameter("itemNo", item.no, 20);
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) inventory = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            databaseQuery = database.prepare("SELECT SUM([Outstanding Quantity]) FROM [" + database.getTableName("Sales Line") + "] WITH (NOLOCK) WHERE [Document Type] = '1' AND [No_] = @itemNo AND [Location Code] = @locationCode");
            databaseQuery.addStringParameter("itemNo", item.no, 20);
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) inventory = inventory - float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            databaseQuery = database.prepare("SELECT SUM(l.[Quantity]) FROM [" + database.getTableName("Web Sales Line") + "] l, [" + database.getTableName("Web Sales Header") + "] h, [" + database.getTableName("Customer") + "] c WHERE l.[Document Type] = '1' AND l.[Item No_] = @itemNo AND h.[Document Type] = l.[Document Type] AND h.[No_] = l.[Document No_] AND c.[No_] = h.[Sell-to Customer No_] AND c.[Location Code] = @locationCode AND h.[Transfered] = 0");
            databaseQuery.addStringParameter("itemNo", item.no, 20);
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) inventory = inventory - float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            return inventory;

        }

        public float calcOfflineInventory(WebModel webModel, string locationCode)
        {

            float inventory = 0;

            DatabaseQuery databaseQuery = database.prepare("SELECT SUM(ile.[Remaining Quantity]) FROM [" + database.getTableName("Item Ledger Entry") + "] ile WITH (NOLOCK), [" + database.getTableName("Web Model Variant") + "] wmv WHERE ile.[Item No_] = wmv.[Item No_] AND wmv.[Web Model No_] = @webModelNo AND ile.[Location Code] = @locationCode AND ile.[Open] = '1'");
            databaseQuery.addStringParameter("webModelNo", webModel.no, 20);
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) inventory = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            databaseQuery = database.prepare("SELECT SUM(sl.[Outstanding Quantity]) FROM [" + database.getTableName("Sales Line") + "] sl WITH (NOLOCK), [" + database.getTableName("Web Model Variant") + "] wmv WHERE sl.[Document Type] = '1' AND sl.[No_] = wmv.[Item No_] AND wmv.[Web Model No_] = @webModelNo AND sl.[Location Code] = @locationCode");
            databaseQuery.addStringParameter("webModelNo", webModel.no, 20);
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) inventory = inventory - float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            databaseQuery = database.prepare("SELECT SUM(l.[Quantity]) FROM [" + database.getTableName("Web Sales Line") + "] l, [" + database.getTableName("Web Sales Header") + "] h, [" + database.getTableName("Customer") + "] c, [" + database.getTableName("Web Model Variant") + "] wmv WHERE l.[Document Type] = '1' AND l.[Item No_] = wmv.[Item No_] AND wmv.[Web Model No_] = @webModelNo AND h.[Document Type] = l.[Document Type] AND h.[No_] = l.[Document No_] AND c.[No_] = h.[Sell-to Customer No_] AND c.[Location Code] = @locationCode AND h.[Transfered] = 0");
            databaseQuery.addStringParameter("webModelNo", webModel.no, 20);
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) inventory = inventory - float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            return inventory;

        }



        public void getNextPlannedReceipt(DataSet dataSet, string locationCode, ref Hashtable itemInfoTable)
        {
            string whereQuery = "";

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                if (whereQuery != "")
                {
                    whereQuery = whereQuery + " OR ";
                }
                whereQuery = whereQuery + "[No_] = '" + itemNo + "'";

                i++;
            }



            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Expected Receipt Date], SUM([Outstanding Quantity]) FROM [" + database.getTableName("Purchase Line") + "] WITH (NOLOCK) WHERE [Document Type] = 1 AND [Type] = 2 AND (" + whereQuery + ") AND [Outstanding Quantity] > 0 AND [Expected Receipt Date] >= GETDATE() AND [Location Code] = @locationCode GROUP BY [No_], [Expected Receipt Date] ORDER BY [No_]");
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                string itemNo = dataReader.GetValue(0).ToString();

                ItemReceiptInfo itemReceiptInfo = new ItemReceiptInfo();

                if (!dataReader.IsDBNull(0)) itemReceiptInfo.nextPlannedReceiptDate = dataReader.GetDateTime(1);
                if (!dataReader.IsDBNull(1)) itemReceiptInfo.nextPlannedReceiptQty = float.Parse(dataReader.GetValue(2).ToString());

                if (!itemInfoTable.Contains(itemNo))
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfoTable.Add(itemNo, itemInfo);
                }
                if (((ItemInfo)itemInfoTable[itemNo]).itemReceiptInfoCollection == null)
                {
                    ((ItemInfo)itemInfoTable[itemNo]).itemReceiptInfoCollection = new ItemReceiptInfoCollection();
                }
                ((ItemInfo)itemInfoTable[itemNo]).itemReceiptInfoCollection.Add(itemReceiptInfo);

            }

            dataReader.Close();


        }



        public ItemReceiptInfoCollection getNextPlannedReceipt(Item item, string locationCode)
        {

            ItemReceiptInfoCollection itemReceiptInfoCollection = new ItemReceiptInfoCollection();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Expected Receipt Date], [Outstanding Quantity] FROM [" + database.getTableName("Purchase Line") + "] WITH (NOLOCK) WHERE [Document Type] = 1 AND [Type] = 2 AND [No_] = @itemNo AND [Outstanding Quantity] > 0 AND [Expected Receipt Date] >= GETDATE() AND [Location Code] = @locationCode ORDER BY [Expected Receipt Date]");
            databaseQuery.addStringParameter("itemNo", item.no, 20);
            databaseQuery.addStringParameter("locationCode", locationCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                ItemReceiptInfo itemReceiptInfo = new ItemReceiptInfo();


                if (!dataReader.IsDBNull(0)) itemReceiptInfo.nextPlannedReceiptDate = dataReader.GetDateTime(0);
                if (!dataReader.IsDBNull(1)) itemReceiptInfo.nextPlannedReceiptQty = float.Parse(dataReader.GetValue(1).ToString());

                itemReceiptInfoCollection.Add(itemReceiptInfo);
            }

            dataReader.Close();

            return itemReceiptInfoCollection;

        }


    }
}
