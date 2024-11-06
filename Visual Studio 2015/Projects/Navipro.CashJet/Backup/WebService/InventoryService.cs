using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Data.SqlClient;

namespace Navipro.CashJet.WebService
{
    public class InventoryService
    {
        private Configuration configuration;
        private Database database;

        public InventoryService(Configuration configuration)
        {
            this.configuration = configuration;
            this.database = new Database(configuration);
        }

        public XmlDocument performService(XmlDocument xmlDocument)
        {
            XmlDocument responseDoc = new XmlDocument();
            responseDoc.LoadXml("<nav/>");
            XmlElement responseDocElement = responseDoc.DocumentElement;
            XmlElement responseElement = addElement(responseDocElement, "serviceResponse", "", "");
            responseElement = addElement(responseElement, "inventory", "", "");

            DataSet locationDataSet = getLocations();

            XmlNodeList itemNodeList = xmlDocument.SelectNodes("serviceRequest/serviceArgument/inventory/item");
            int i = 0;
            while (i < itemNodeList.Count)
            {
                XmlElement xmlElement = (XmlElement)itemNodeList.Item(i);
                if (xmlElement.FirstChild != null)
                {
                    string itemNo = xmlElement.FirstChild.Value;

                    DataSet variantDataSet = getVariants(itemNo);
                    throw new Exception("ITem: " + itemNo);
                    int locationCounter = 0;
                    while (locationCounter < locationDataSet.Tables[0].Rows.Count)
                    {
                        string locationCode = locationDataSet.Tables[0].Rows[locationCounter].ItemArray.GetValue(0).ToString();


                        if (variantDataSet.Tables[0].Rows.Count > 0)
                        {
                            int variantCounter = 0;
                            while (variantCounter < variantDataSet.Tables[0].Rows.Count)
                            {
                                string variantCode = variantDataSet.Tables[0].Rows[locationCounter].ItemArray.GetValue(0).ToString();

                                XmlElement itemElement = addElement(responseElement, "item", "", "");
                                addAttribute(itemElement, "no", itemNo);
                                addAttribute(itemElement, "variantCode", variantCode);
                                addAttribute(itemElement, "locationCode", locationCode);
                                addAttribute(itemElement, "inventory", calcInventory(itemNo, variantCode, locationCode).ToString());

                                int purchaseQty = 0;
                                DateTime purchaseDate = DateTime.MinValue;
                                calcPurchaseQty(itemNo, variantCode, locationCode, out purchaseQty, out purchaseDate);

                                addAttribute(itemElement, "purchQuantity", purchaseQty.ToString());
                                addAttribute(itemElement, "purchDate", purchaseDate.ToString("yyyy-MM-dd"));
                                addAttribute(itemElement, "bomInventory", "");

                                variantCounter++;
                            }
                        }
                        else
                        {
                            XmlElement itemElement = addElement(responseElement, "item", "", "");
                            addAttribute(itemElement, "no", itemNo);
                            addAttribute(itemElement, "variantCode", "");
                            addAttribute(itemElement, "locationCode", locationCode);
                            addAttribute(itemElement, "inventory", calcInventory(itemNo, "", locationCode).ToString());

                            int purchaseQty = 0;
                            DateTime purchaseDate = DateTime.MinValue;
                            calcPurchaseQty(itemNo, "", locationCode, out purchaseQty, out purchaseDate);

                            addAttribute(itemElement, "purchQuantity", purchaseQty.ToString());
                            addAttribute(itemElement, "purchDate", purchaseDate.ToString("yyyy-MM-dd"));

                            if (isBOMItem(itemNo))
                            {
                                addAttribute(itemElement, "bomInventory", calcBOMInventory(itemNo, locationCode).ToString());
                            }
                            else
                            {
                                addAttribute(itemElement, "bomInventory", "");
                            }

                        }
                        locationCounter++;
                    }
                }

                i++;
            }

            return responseDoc;
        }

        private XmlElement addElement(XmlElement xmlElement, string name, string value, string nameSpace)
        {
            XmlElement newElement = xmlElement.OwnerDocument.CreateElement(name, nameSpace);
            if (value != "")
            {
                XmlText xmlText = xmlElement.OwnerDocument.CreateTextNode(value);
                newElement.AppendChild(xmlText);
            }
            xmlElement.AppendChild(newElement);

            return newElement;
        }

        private void addAttribute(XmlElement xmlElement, string name, string value)
        {
            XmlAttribute xmlAttribute = xmlElement.OwnerDocument.CreateAttribute(name);
            xmlAttribute.Value = value;
            xmlElement.AppendChild(xmlAttribute);
        }

        private DataSet getLocations()
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Code] FROM [" + database.getTableName("Location") + "]");
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            return dataSet;

        }

        private DataSet getVariants(string itemNo)
        {

            DatabaseQuery databaseQuery = database.prepare("SELECT [Code] FROM [" + database.getTableName("Item Variant") + "] WHERE [Item No_] = @itemNo");
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            return dataSet;

        }

        private int calcInventory(string itemNo, string variantCode, string locationCode)
        {
            string variantFilter = "";
            if (variantCode != "") variantFilter = "AND [Variant Code] = @variantCode";

            //Inventory
            DatabaseQuery databaseQuery = database.prepare("SELECT SUM([Quantity)) FROM [" + database.getTableName("Item Ledger Entry") + "] WHERE [Item No_] = @itemNo AND [Location Code] = @locationCode "+variantFilter);
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);
            databaseQuery.addStringParameter("@variantCode", variantCode, 20);
            databaseQuery.addStringParameter("@locationCode", locationCode, 20);

            int inventory = 0;
            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) inventory = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            //Qty. on sales orders
            databaseQuery = database.prepare("SELECT SUM([Outstanding Quantity)) FROM [" + database.getTableName("Sales Line") + "] WHERE [Document Type] = 1 AND [Type] = 2 AND [No_] = @itemNo AND [Location Code] = @locationCode "+variantFilter);
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);
            databaseQuery.addStringParameter("@variantCode", variantCode, 20);
            databaseQuery.addStringParameter("@locationCode", locationCode, 20);

            int qtyOnSales = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) qtyOnSales = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            //Open Receipts
            databaseQuery = database.prepare("SELECT SUM([Quantity)) FROM [" + database.getTableName("Cash Receipt Line") + "] WHERE [Line Type] = 0 AND [Sales Type] = 2 AND [No_] = @itemNo AND [Location Code] = @locationCode AND [Registered] = 1 AND [Void] = 0 "+variantFilter);
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);
            databaseQuery.addStringParameter("@variantCode", variantCode, 20);
            databaseQuery.addStringParameter("@locationCode", locationCode, 20);

            int qtyOnOpenReceipts = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) qtyOnOpenReceipts = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

            //Unposted receipts
            databaseQuery = database.prepare("SELECT SUM([Quantity)) FROM [" + database.getTableName("Posted Cash Receipt Line") + "] WHERE [Line Type] = 0 AND [Sales Type] = 2 AND [No_] = @itemNo AND [Location Code] = @locationCode AND [Registered] = 1 AND [Void] = 0 AND [Posted] = 0 "+variantFilter);
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);
            databaseQuery.addStringParameter("@variantCode", variantCode, 20);
            databaseQuery.addStringParameter("@locationCode", locationCode, 20);

            int qtyOnUnPostedReceipts = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) qtyOnUnPostedReceipts = int.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();

  
            return inventory - qtyOnSales - qtyOnOpenReceipts - qtyOnUnPostedReceipts;
        }

        private void calcPurchaseQty(string itemNo, string variantCode, string locationCode, out int purchaseQty, out DateTime purchaseDate)
        {
            purchaseQty = 0;
            purchaseDate = DateTime.MinValue;
        
            DatabaseQuery databaseQuery = database.prepare("SELECT TOP 1 [Outstanding Quantity], [Expected Receipt Date] FROM [" + database.getTableName("Purchase Line") + "] WHERE [Document Type] = 1 AND [Type] = 2 AND [No_] = @itemNo AND [Location Code] = @locationCode AND [Outstanding Quantity] > 1 AND [Expected Receipt Date] >= GETDATE()");
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);
            databaseQuery.addStringParameter("@variantCode", variantCode, 20);
            databaseQuery.addStringParameter("@locationCode", locationCode, 20);

            
            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) purchaseQty = int.Parse(dataReader.GetValue(0).ToString());
                if (!dataReader.IsDBNull(1)) purchaseDate = dataReader.GetDateTime(1);
            }
            dataReader.Close();
            
        }

  
        
        public bool isBOMItem(string itemNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT TOP 1 [Item No_] FROM [" + database.getTableName("BOM Component") + "] WHERE [Item No_] = @itemNo");
            databaseQuery.addStringParameter("@itemNo", itemNo, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            bool bomExists = false;

            if (dataReader.Read())
            {
                bomExists = true;
            }
            dataReader.Close();

            return bomExists;
        }

        private int calcBOMInventory(string bomItemNo, string locationCode)
        {
            int currentInventory = 99999;
            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Quantity per] FROM [" + database.getTableName("BOM Component") + "] WHERE [Parent Item No_] = @itemNo AND [Type] = 1");
            databaseQuery.addStringParameter("@itemNo", bomItemNo, 20);

            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                int quantityPer = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());

                int inventory = calcInventory(itemNo, "", locationCode);
                int bomInv = inventory / quantityPer;

                if (bomInv < currentInventory) currentInventory = bomInv;

                i++;
            }

            return currentInventory;
        }

    }
}
