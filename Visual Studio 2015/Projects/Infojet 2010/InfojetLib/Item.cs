using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
    public class Item : ServiceArgument
    {
        private Database database;

        public string no = "";
        public string description = "";
        public string description2 = "";
        public float unitPrice;
        public string salesUnitOfMeasure = "";
        public string manufacturerCode = "";
        public string leadTimeCalculation = "";
        public float unitListPrice;
        public string itemDiscGroup = "";

        private string locationCode = "";
        private string customerNo = "";
        private string currencyCode = "";
        private float quantity;

        public string vatProductPostingGroup = "";

        public Item(Database database, string no)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;

            this.no = no;

            getFromDatabase();
        }

        public Item(Database database, DataRow dataRow)
        {
            this.database = database;

            this.no = dataRow.ItemArray.GetValue(0).ToString();
            this.description = dataRow.ItemArray.GetValue(1).ToString();
            this.description2 = dataRow.ItemArray.GetValue(2).ToString();
            this.unitPrice = 0;
            try
            {
                this.unitPrice = float.Parse(dataRow.ItemArray.GetValue(3).ToString());
            }
            catch (Exception) { }
            this.salesUnitOfMeasure = dataRow.ItemArray.GetValue(4).ToString();
            this.manufacturerCode = dataRow.ItemArray.GetValue(5).ToString();
            this.leadTimeCalculation = dataRow.ItemArray.GetValue(6).ToString();
            this.unitListPrice = 0;
            try
            {
                this.unitListPrice = float.Parse(dataRow.ItemArray.GetValue(7).ToString());
            }
            catch (Exception) { }
            this.itemDiscGroup = dataRow.ItemArray.GetValue(8).ToString();
            this.vatProductPostingGroup = dataRow.ItemArray.GetValue(9).ToString();
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [No_], [Description], [Description 2], [Unit Price], [Sales Unit of Measure], [Manufacturer Code], [Lead Time Calculation], [Unit List Price], [Item Disc_ Group], [VAT Prod_ Posting Group] FROM [" + database.getTableName("Item") + "] WTH (NOLOCK) WHERE [No_] = @no");
            databaseQuery.addStringParameter("no", no, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery(); 
            if (dataReader.Read())
            {

                no = dataReader.GetValue(0).ToString();
                description = dataReader.GetValue(1).ToString();
                description2 = dataReader.GetValue(2).ToString();
                unitPrice = float.Parse(dataReader.GetValue(3).ToString());
                salesUnitOfMeasure = dataReader.GetValue(4).ToString();
                manufacturerCode = dataReader.GetValue(5).ToString();
                leadTimeCalculation = dataReader.GetValue(6).ToString();
                unitListPrice = float.Parse(dataReader.GetValue(7).ToString());
                itemDiscGroup = dataReader.GetValue(8).ToString();
                vatProductPostingGroup = dataReader.GetValue(9).ToString();
            }
            else
            {
                no = "";
            }

            dataReader.Close();


        }


        public ItemTranslation getItemTranslation(string languageCode)
        {
            ItemTranslation itemTranslation = new ItemTranslation(database, no, "", languageCode);
            if (itemTranslation.description == null)
            {
                itemTranslation.description = description;
                itemTranslation.description2 = description2;
            }

            return itemTranslation;

        }

        private DataSet getItemTextDataSet(string languageCode)
        {
            ExtendedTextHeaders extendedTextHeaders = new ExtendedTextHeaders(database);
            ExtendedTextHeader extendedTextHeader = extendedTextHeaders.getItemText(this.no, languageCode);
            if (extendedTextHeader != null)
            {
                ExtendedTextLines extendedTextLines = new ExtendedTextLines(database);
                return extendedTextLines.getTextLines(extendedTextHeader);
            }
            else
            {

                extendedTextHeader = extendedTextHeaders.getItemText(this.no, "");
                if (extendedTextHeader != null)
                {
                    ExtendedTextLines extendedTextLines = new ExtendedTextLines(database);
                    return extendedTextLines.getTextLines(extendedTextHeader);
                }

            }
            return null;
        }

        public void setLocationCode(string locationCode)
        {
            this.locationCode = locationCode;
        }

        public void setCurrencyCode(string currencyCode)
        {
            this.currencyCode = currencyCode;
        }

        public void setCustomerNo(string customerNo)
        {
            this.customerNo = customerNo;
        }

        public void setQuantity(float quantity)
        {
            this.quantity = quantity;
        }

        #region ServiceArgument Members

        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
        {
            // TODO:  Add Item.toDOM implementation
            XmlElement itemsElement = xmlDoc.CreateElement("items");


            if (locationCode != "")
            {
                XmlAttribute locationAttribute = xmlDoc.CreateAttribute("locationCode");
                locationAttribute.Value = locationCode;

                itemsElement.Attributes.Append(locationAttribute);
            }

            if (customerNo != "")
            {
                XmlAttribute customerNoAttribute = xmlDoc.CreateAttribute("customerNo");
                customerNoAttribute.Value = customerNo;

                itemsElement.Attributes.Append(customerNoAttribute);
            }

            if (currencyCode != "")
            {
                XmlAttribute currencyAttribute = xmlDoc.CreateAttribute("currencyCode");
                currencyAttribute.Value = currencyCode;

                itemsElement.Attributes.Append(currencyAttribute);
            }

            XmlElement itemElement = xmlDoc.CreateElement("item");
            XmlElement itemNoElement = xmlDoc.CreateElement("no");

            XmlText itemNoText = xmlDoc.CreateTextNode(this.no);

            itemNoElement.AppendChild(itemNoText);
            itemElement.AppendChild(itemNoElement);

            if (quantity > 0)
            {
                XmlAttribute quantityAttribute = xmlDoc.CreateAttribute("quantity");
                quantityAttribute.Value = String.Format("{0:0.##}", quantity);

                itemElement.Attributes.Append(quantityAttribute);
            }

            itemsElement.AppendChild(itemElement);


            return itemsElement;
        }


        #endregion

        public string formatUnitPrice(string currencyCode)
        {
            return database.formatCurrency(this.unitPrice, currencyCode);
        }

        public string formatUnitListPrice(string currencyCode)
        {
            return database.formatCurrency(this.unitListPrice, currencyCode);
        }

        public float getOfflineInventory(string locationCode)
        {
            ItemInventory itemInventory = new ItemInventory(database);
            return itemInventory.calcOfflineInventory(this, locationCode);

        }

        public ItemReceiptInfoCollection getNextPlannedReceipt(string locationCode)
        {
            ItemInventory itemInventory = new ItemInventory(database);
            return itemInventory.getNextPlannedReceipt(this, locationCode);
        }



        public string getAttribute(string itemAttributeCode, string languageCode)
        {
            ItemAttributeValue itemAttributeValue = new ItemAttributeValue(database, this.no, itemAttributeCode, languageCode);
            return itemAttributeValue.attributeValue;
        }

        public string getItemText(string languageCode)
        {
            string text = "";

            DataSet itemText = getItemTextDataSet(languageCode);
            if (itemText != null)
            {
                int j = 0;
                while (j < itemText.Tables[0].Rows.Count)
                {
                    ExtendedTextLine extendedTextLine = new ExtendedTextLine(database, itemText.Tables[0].Rows[j]);
                    text = text + " " + extendedTextLine.text;
                    j++;
                }
            }

            return text;

        }

        public ProductItem getProductItem(Infojet infojetContext, WebPageLine webPageLine)
        {
            WebItemList webItemList = new WebItemList(infojetContext, webPageLine.code);

            ProductItem productItem = new ProductItem(this, infojetContext.languageCode);

            /*
            if (infojet.userSession != null)
            {
                itemListFilterForm.showUnitPrice = webItemList.loggedInShowCustPrice;
                itemListFilterForm.showUnitListPrice = webItemList.loggedInShowRecPrice;
                itemListFilterForm.showInventory = webItemList.loggedInShowInventory;
                itemListFilterForm.showBuy = true;

            }
            else
            {
                itemListFilterForm.showUnitPrice = false;
                itemListFilterForm.showUnitListPrice = webItemList.showRecPrice;
                itemListFilterForm.showInventory = webItemList.showInventory;
                itemListFilterForm.showBuy = infojetContext.webSite.allowPurchaseNotLoggedIn;
            }
        */

            Items items = new Items();
            Hashtable itemInfoTable = items.getItemInfo(this, infojetContext, 1);

            WebItemImages webItemImages = new WebItemImages(database);
            productItem.productImages = webItemImages.getWebItemImages(no, 0, infojetContext.webSite.code).toProductImageCollection(productItem.description);
            productItem.productImages.setChangeUrl(infojetContext.webPage.getUrl()+"&itemNo="+this.no);
            if (System.Web.HttpContext.Current.Request["webModelNo"] != null)
                productItem.productImages.setChangeUrl(infojetContext.webPage.getUrl() + "&itemNo=" + this.no + "&webModelNo=" + System.Web.HttpContext.Current.Request["webModelNo"]);

            if (productItem.productImages.Count > 0)
            {
                productItem.productImage = productItem.productImages[0];
            }

            if (System.Web.HttpContext.Current.Request["imageCode"] != null)
            {
                int i = 0;
                while (i < productItem.productImages.Count)
                {
                    if (productItem.productImages[i].code == System.Web.HttpContext.Current.Request["imageCode"])
                    {
                        productItem.productImage = productItem.productImages[i];
                    }
                    i++;
                }
            }
            else
            {
                if (productItem.productImages.Count > 0) productItem.productImage = productItem.productImages[0];
            }

            if (itemInfoTable.Contains(productItem.item.no))
            {
                productItem.inventory = ((ItemInfo)itemInfoTable[productItem.item.no]).inventory;
                productItem.inventoryText = items.getInventoryText(this, ((ItemInfo)itemInfoTable[productItem.item.no]).inventory, infojetContext, false);

                productItem.item.unitPrice = ((ItemInfo)itemInfoTable[productItem.item.no]).unitPrice;
                productItem.item.unitListPrice = ((ItemInfo)itemInfoTable[productItem.item.no]).unitListPrice;

                productItem.formatedUnitPrice = productItem.item.formatUnitPrice(infojetContext.presentationCurrencyCode);
                productItem.formatedUnitListPrice = productItem.item.formatUnitListPrice(infojetContext.presentationCurrencyCode);


            }

            productItem.itemAttributes = ItemAttributeVisibilities.getItemAttributes(infojetContext, webItemList.code, this.no);

            return productItem;
        }

        public float getVatFactor(Customer customer)
        {
            float vatFactor = 0;

            DatabaseQuery databaseQuery = database.prepare("SELECT [VAT %] FROM [" + database.getTableName("VAT Posting Setup") + "] WHERE [VAT Bus_ Posting Group] = @vatBusPostingGroup AND [VAT Prod_ Posting Group] = @vatProductPostingGroup");
            databaseQuery.addStringParameter("vatBusPostingGroup", customer.vatBusPostingGroup, 20);
            databaseQuery.addStringParameter("vatProductPostingGroup", this.vatProductPostingGroup, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery(); 
            if (dataReader.Read())
            {
                vatFactor = float.Parse(dataReader.GetValue(0).ToString());
            }

            dataReader.Close();

            vatFactor = (vatFactor / 100) + 1;

            return vatFactor;
        }

    }
}
