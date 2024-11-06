using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Navipro.BjornBorg.Web
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.navipro.se/bjornborg/web/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InventoryWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public ItemInventoryCollection getItemInventory(string[] itemNoArray)
        {

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            ItemInventoryCollection itemInventoryList = queryItemInventory(database, itemNoArray); 

            database.close();

            return itemInventoryList;
        }

        private ItemInventoryCollection queryItemInventory(Database database, string[] itemNoArray)
        {
            ItemInventoryCollection itemInventoryList = new ItemInventoryCollection();
            Hashtable inventoryTable = new Hashtable();
            ArrayList keys = new ArrayList();
            string query1 = "";
            string query2 = "";
            string query3 = "";

            foreach (string bufferItemNo in itemNoArray)
            {
                string itemNo = bufferItemNo;
                string variantCode = "";
                if (bufferItemNo.Contains("_"))
                {
                    itemNo = bufferItemNo.Substring(0, bufferItemNo.IndexOf("_"));
                    variantCode = bufferItemNo.Substring(bufferItemNo.IndexOf("_") + 1);
                }
                
                
                if (query1 != "") query1 = query1 + " OR ";
                query1 = query1 + "([Item No_] = '" + itemNo.Replace("'", "") + "' AND [Variant Code] = '"+variantCode.Replace("'", "")+"')";

                if (query2 != "") query2 = query2 + " OR ";
                query2 = query2 + "([No_] = '" + itemNo.Replace("'", "") + "' AND [Variant Code] = '" + variantCode.Replace("'", "") + "')";

                if (query3 != "") query3 = query3 + " OR ";
                query3 = query3 + "([Sales No_] = '" + itemNo.Replace("'", "") + "' AND [Variant Code] = '" + variantCode.Replace("'", "") + "')";

            }
            if (query1 == "") return itemInventoryList;

            
            SqlDataReader dataReader = database.query("SELECT [Item No_], [Variant Code], [Location Code], SUM([Remaining Quantity]) FROM [" + database.getTableName("Item Ledger Entry") + "] WHERE (" + query1 + ") AND [Open] = 1 GROUP BY [Item No_], [Variant Code], [Location Code]");
            while (dataReader.Read())
            {
                
                string sku = dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString();
                string locationCode = dataReader.GetValue(2).ToString();
                ItemInventory itemInventory = null;
                if (inventoryTable[sku + "_" + locationCode] == null)
                {
                    itemInventory = new ItemInventory();
                    itemInventory.sku = sku;
                    itemInventory.locationCode = locationCode;

                    inventoryTable.Add(itemInventory.sku + "_" + itemInventory.locationCode, itemInventory);
                    keys.Add(sku + "_" + locationCode);
                }

                itemInventory = (ItemInventory)inventoryTable[sku + "_" + locationCode];

                itemInventory.inventory = (int)float.Parse(dataReader.GetValue(3).ToString());

                inventoryTable[sku + "_" + locationCode] = itemInventory;
            }
            dataReader.Close();

            dataReader = database.query("SELECT [No_], [Variant Code], [Location Code], SUM([Outstanding Quantity]) FROM [" + database.getTableName("Sales Line") + "] WHERE (" + query2 + ") AND [Document Type] = 1 AND [Type] = 2 AND [Outstanding Quantity] > 0 GROUP BY [No_], [Variant Code], [Location Code]");
            while (dataReader.Read())
            {
                string sku = dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString();
                string locationCode = dataReader.GetValue(2).ToString();
                ItemInventory itemInventory = null;
                if (inventoryTable[sku + "_" + locationCode] == null)
                {
                    itemInventory = new ItemInventory();
                    itemInventory.sku = sku;
                    itemInventory.locationCode = locationCode;

                    inventoryTable.Add(itemInventory.sku + "_" + itemInventory.locationCode, itemInventory);
                    keys.Add(sku + "_" + locationCode);

                }

                itemInventory = (ItemInventory)inventoryTable[sku + "_" + locationCode];

                itemInventory.inventory = itemInventory.inventory - (int)float.Parse(dataReader.GetValue(3).ToString());

                inventoryTable[sku + "_" + locationCode] = itemInventory;
            }
            dataReader.Close();


            dataReader = database.query("SELECT [Item No_], [Variant Code], [Transfer-from Code], SUM([Outstanding Quantity]) FROM [" + database.getTableName("Transfer Line") + "] WHERE (" + query1 + ") AND [Outstanding Quantity] > 0 GROUP BY [Item No_], [Variant Code], [Transfer-from Code]");
            while (dataReader.Read())
            {
                string sku = dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString();
                string locationCode = dataReader.GetValue(2).ToString();
                ItemInventory itemInventory = null;
                if (inventoryTable[sku + "_" + locationCode] == null)
                {
                    itemInventory = new ItemInventory();
                    itemInventory.sku = sku;
                    itemInventory.locationCode = locationCode;

                    inventoryTable.Add(itemInventory.sku + "_" + itemInventory.locationCode, itemInventory);
                    keys.Add(sku + "_" + locationCode);

                }

                itemInventory = (ItemInventory)inventoryTable[sku + "_" + locationCode];

                itemInventory.inventory = itemInventory.inventory - (int)float.Parse(dataReader.GetValue(3).ToString());

                inventoryTable[sku + "_" + locationCode] = itemInventory;
            }
            dataReader.Close();

            dataReader = database.query("SELECT [Sales No_], [Variant Code], [Location Code], SUM([Quantity]) FROM [" + database.getTableName("Posted Cash Receipt Line") + "] WHERE (" + query3 + ") AND [Line Type] = 0 AND [Sales Type] = 2 AND [Void] = 0 AND [Posted] = 0 GROUP BY [Sales No_], [Variant Code], [Location Code]");
            while (dataReader.Read())
            {
                string sku = dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString();
                string locationCode = dataReader.GetValue(2).ToString();
                ItemInventory itemInventory = null;
                if (inventoryTable[sku + "_" + locationCode] == null)
                {
                    itemInventory = new ItemInventory();
                    itemInventory.sku = sku;
                    itemInventory.locationCode = locationCode;

                    inventoryTable.Add(itemInventory.sku + "_" + itemInventory.locationCode, itemInventory);
                    keys.Add(sku + "_" + locationCode);

                }

                itemInventory = (ItemInventory)inventoryTable[sku + "_" + locationCode];

                itemInventory.inventory = itemInventory.inventory - (int)float.Parse(dataReader.GetValue(3).ToString());

                inventoryTable[sku + "_" + locationCode] = itemInventory;
            }
            dataReader.Close();

            dataReader = database.query("SELECT [Sales No_], [Variant Code], [Location Code], SUM([Quantity]) FROM [" + database.getTableName("Cash Receipt Line") + "] WHERE (" + query3 + ") AND [Line Type] = 0 AND [Sales Type] = 2 AND [Void] = 0 AND [Registered] = 1 GROUP BY [Sales No_], [Variant Code], [Location Code]");
            while (dataReader.Read())
            {
                string sku = dataReader.GetValue(0).ToString() + "_" + dataReader.GetValue(1).ToString();
                string locationCode = dataReader.GetValue(2).ToString();
                ItemInventory itemInventory = null;
                if (inventoryTable[sku + "_" + locationCode] == null)
                {
                    itemInventory = new ItemInventory();
                    itemInventory.sku = sku;
                    itemInventory.locationCode = locationCode;

                    inventoryTable.Add(itemInventory.sku + "_" + itemInventory.locationCode, itemInventory);
                    keys.Add(sku + "_" + locationCode);

                }

                itemInventory = (ItemInventory)inventoryTable[sku + "_" + locationCode];

                itemInventory.inventory = itemInventory.inventory - (int)float.Parse(dataReader.GetValue(3).ToString());

                inventoryTable[sku + "_" + locationCode] = itemInventory;
            }
            dataReader.Close();

            int i = 0;
            while (i < keys.Count)
            {
                ItemInventory itemInventory = (ItemInventory)inventoryTable[keys[i]];
                itemInventoryList.Add(itemInventory);
                i++;
            }

            return itemInventoryList;
        }
    }
}
