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
        public static List<Item> GetItems(bool includeInventory, bool includeTranslations, int offset, int count)
        {
            List<Item> itemList = new List<Item>();

            if (count == 0) count = 100;

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

            DatabaseQuery databaseQuery = database.prepare("SELECT i1.[No_], i1.[Description], i1.[Description 2], i1.[Item Category Code], i3.[NPX Web Category] as [Product Group Code], i3.[NPX Image Url] as [Image Url], i3.[NPX Points] as [Points] FROM [WACKES$Item$437dbf0e-84ff-417a-965d-ed2bb9650972] i1 LEFT JOIN [WACKES$Item$8d5849cc-8eb0-4cc6-8822-f4837ec99fea] i2 ON i1.[No_] = i2.[No_] LEFT JOIN [WACKES$Item$756c86b4-ab87-47c3-b1f2-abd5bf4f88ae] i3 ON i1.[No_] = i3.[No_] LEFT JOIN [WACKES$Item$71a251e5-89f0-4020-bc26-e5525c5633a6] i4 ON i1.[No_] = i4.[No_] WHERE i4.[Web Shop] = 'BOSCH' ORDER BY i1.[No_] " + offsetString);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                Item item = new Item(dataReader);
                itemList.Add(item);
            }

            dataReader.Close();

            if (includeInventory) itemList = ApplyVariants(database, itemList, "");
            if (includeInventory) itemList = ApplyInventory(database, itemList, "");
            if (includeTranslations) itemList = ApplyTranslations(database, itemList, "");

            database.close();

            return itemList;
        }

        public static int CountItems()
        {

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT COUNT(*) as count FROM [WACKES$Item$71a251e5-89f0-4020-bc26-e5525c5633a6] i WHERE i.[Web Shop] = 'BOSCH'");

            int count = 0;

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) count = int.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();


            database.close();


            return count;
        }

        public static Item GetItem(string no)
        {
            List<Item> itemList = new List<Item>();

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT i1.[No_], i1.[Description], i1.[Description 2], i1.[Item Category Code], i3.[NPX Web Category] as [Product Group Code], i3.[NPX Image Url] as [Image Url], i3.[NPX Points] as [Points] FROM [WACKES$Item$437dbf0e-84ff-417a-965d-ed2bb9650972] i1 LEFT JOIN [WACKES$Item$8d5849cc-8eb0-4cc6-8822-f4837ec99fea] i2 ON i1.[No_] = i2.[No_] LEFT JOIN [WACKES$Item$756c86b4-ab87-47c3-b1f2-abd5bf4f88ae] i3 ON i1.[No_] = i3.[No_] LEFT JOIN [WACKES$Item$71a251e5-89f0-4020-bc26-e5525c5633a6] i4 ON i1.[No_] = i4.[No_] WHERE i4.[Web Shop] = 'BOSCH' AND i1.[No_] = @no ORDER BY i1.[No_] ");
            databaseQuery.addStringParameter("no", no, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                Item item = new Item(dataReader);
                itemList.Add(item);
            }

            dataReader.Close();

            itemList = ApplyVariants(database, itemList, no);
            itemList = ApplyInventory(database, itemList, no);
            itemList = ApplyTranslations(database, itemList, no);



            database.close();


            if (itemList.Count == 1) return itemList[0];
            return null;
        }


        public static List<string> GetItemSkuList(List<Item> itemList)
        {
            return itemList.Select(l => l.no).ToList();
        }


        public static List<Item> ApplyInventory(Database database, List<Item> itemList, string itemNo)
        {
            List<ItemVariant> invList = new List<ItemVariant>();

            string itemNoFilter = "";
            if (itemNo != "") itemNoFilter = "AND i.[No_] = '" + itemNo + "'";

            DatabaseQuery databaseQuery = database.prepare("SELECT ile.[Item No_], ile.[Variant Code], SUM(ile.[Quantity]) FROM [WACKES$Item Ledger Entry$437dbf0e-84ff-417a-965d-ed2bb9650972] ile, [WACKES$Item$71a251e5-89f0-4020-bc26-e5525c5633a6] i WHERE ile.[Item No_] = i.[No_] AND i.[Web Shop] = 'BOSCH' " + itemNoFilter+" GROUP BY ile.[Item No_], ile.[Variant Code]");

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                ItemVariant itemVariant = ItemVariant.FromInventory(dataReader);
                invList.Add(itemVariant);
            }

            dataReader.Close();


            List<ItemVariant> salesList = new List<ItemVariant>();

            databaseQuery = database.prepare("SELECT sl.[No_], sl.[Variant Code], SUM(sl.[Outstanding Quantity]) FROM [WACKES$Sales Line$437dbf0e-84ff-417a-965d-ed2bb9650972] sl, [WACKES$Item$71a251e5-89f0-4020-bc26-e5525c5633a6] i WHERE sl.[Document Type] = 1 AND sl.[No_] = i.[No_] AND i.[Web Shop] = 'BOSCH' " + itemNoFilter + " GROUP BY sl.[No_], sl.[Variant Code]");

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                ItemVariant itemVariant = ItemVariant.FromInventory(dataReader);
                salesList.Add(itemVariant);
            }

            dataReader.Close();


            int i = 0;
            while (i < itemList.Count)
            {
                decimal inventory = invList.Where(v => v.itemNo == itemList[i].no).Sum(v => v.inventory);
                decimal qtyOnSales = salesList.Where(v => v.itemNo == itemList[i].no).Sum(v => v.inventory);
                itemList[i].inventory = inventory - qtyOnSales;
                
                int j = 0;
                while (j < itemList[i].itemVariants.Count)
                {
                    decimal varInv = invList.Where(v => v.itemNo == itemList[i].no && v.code == itemList[i].itemVariants[j].code).Sum(v => v.inventory);
                    decimal varQtySales = salesList.Where(v => v.itemNo == itemList[i].no && v.code == itemList[i].itemVariants[j].code).Sum(v => v.inventory);
                    itemList[i].itemVariants[j].inventory = varInv - varQtySales;

                    j++;
                }

                i++;
            }



            return itemList;
        }

        public static List<Item> ApplyVariants(Database database, List<Item> itemList, string itemNo)
        {
            List<ItemVariant> variantList = new List<ItemVariant>();

            string itemNoFilter = "";
            if (itemNo != "") itemNoFilter = "AND i.[No_] = '" + itemNo + "'";

            DatabaseQuery databaseQuery = database.prepare("SELECT var.[Item No_], var.[Code], var.[Description], var2.[NPX Size Code] FROM [WACKES$Item Variant$437dbf0e-84ff-417a-965d-ed2bb9650972] var LEFT JOIN [WACKES$Item Variant$8d5849cc-8eb0-4cc6-8822-f4837ec99fea] var2 ON var.[Item No_] = var2.[Item No_] AND var.[Code] = var2.[Code], [WACKES$Item$71a251e5-89f0-4020-bc26-e5525c5633a6] i WHERE var.[Item No_] = i.[No_] AND i.[Web Shop] = 'BOSCH' " + itemNoFilter);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                ItemVariant itemVariant = ItemVariant.FromVariant(dataReader);

                variantList.Add(itemVariant);

            }

            dataReader.Close();



            int i = 0;
            while (i < itemList.Count)
            {
                itemList[i].itemVariants = variantList.Where(l => l.itemNo == itemList[i].no).ToList();
                i++;
            }



            return itemList;
        }

        public static List<Item> ApplyTranslations(Database database, List<Item> itemList, string itemNo)
        {
            Dictionary<string, ItemTranslation> productTextList = new Dictionary<string, ItemTranslation>();

            string itemNoFilter = "";
            if (itemNo != "") itemNoFilter = "AND i.[No_] = '" + itemNo + "'";

            DatabaseQuery databaseQuery = database.prepare("SELECT trans.[Item No_], trans.[Language Code], trans.[Description], trans.[Description 2] FROM [WACKES$Item Translation$437dbf0e-84ff-417a-965d-ed2bb9650972] trans, [WACKES$Item$71a251e5-89f0-4020-bc26-e5525c5633a6] i WHERE trans.[Item No_] = i.[No_] AND i.[Web Shop] = 'BOSCH' AND trans.[Variant Code] = '' " + itemNoFilter);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                ItemTranslation itemTranslation = new ItemTranslation(dataReader);

                productTextList.Add(dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString(), itemTranslation);

            }

            dataReader.Close();



            databaseQuery = database.prepare("SELECT tl.[No_], tl.[Language Code], tl.[Text] FROM [WACKES$Extended Text Line$437dbf0e-84ff-417a-965d-ed2bb9650972] tl, [WACKES$Item$71a251e5-89f0-4020-bc26-e5525c5633a6] i WHERE tl.[Table Name] = 2 AND tl.[No_] = i.[No_] AND i.[Web Shop] = 'BOSCH' " + itemNoFilter);

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                if (productTextList.ContainsKey(dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString()))
                {
                    ItemTranslation productText = productTextList[dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString()];
                    if (productText.productText != "") productText.productText = productText.productText + " ";
                    productText.productText = productText.productText + dataReader.GetValue(2);
                    productTextList[dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString()] = productText;
                }
            }

            dataReader.Close();


            int i = 0;
            while (i < itemList.Count)
            {
                itemList[i].itemTranslations = productTextList.Values.Where(l => l.itemNo == itemList[i].no).ToList();
                i++;
            }



            return itemList;
        }



    }
}