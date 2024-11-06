using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace Navipro.Infojet.Lib
{
    public class WebModel
    {
        private string _no;
        private string _description;
        private string _description2;
        private string _variantDimension1Code;
        private string _variantDimension2Code;
        private string _variantDimension3Code;
        private string _variantDimension4Code;

        private Infojet infojetContext;
        private Hashtable dimensionTable;
        private Hashtable variantTable;

        public WebModel(Infojet infojetContext, string no)
        {
            this._no = no;
            this.infojetContext = infojetContext;

            this.dimensionTable = new Hashtable();
            getFromDatabase();

            //getPrepopulatedDimensions();
        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [No_], [Description], [Description 2], [Variant Dimension 1 Code], [Variant Dimension 2 Code], [Variant Dimension 3 Code], [Variant Dimension 4 Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Model") + "] WHERE [No_] = @no");
            databaseQuery.addStringParameter("no", _no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                _no = dataReader.GetValue(0).ToString();
                _description = dataReader.GetValue(1).ToString();
                _description2 = dataReader.GetValue(2).ToString();
                _variantDimension1Code = dataReader.GetValue(3).ToString();
                _variantDimension2Code = dataReader.GetValue(4).ToString();
                _variantDimension3Code = dataReader.GetValue(5).ToString();
                _variantDimension4Code = dataReader.GetValue(6).ToString();
            }
            else
            {
                _no = "";
            }
            
            dataReader.Close();


        }

        public WebModelTranslation getTranslation(string languageCode)
        {
            WebModelTranslation webModelTranslation = new WebModelTranslation(infojetContext, no, languageCode);
            if (webModelTranslation.description == null)
            {
                webModelTranslation.description = description;
                webModelTranslation.description2 = description2;
            }

            return webModelTranslation;

        }

        public float calcInventory()
        {
            ItemInventory itemInventory = new ItemInventory(infojetContext.systemDatabase);
            return itemInventory.calcOfflineInventory(this, infojetContext.webSite.locationCode);
        }

        public WebModelDimensionCollection getDimensions()
        {
            WebModelDimensionCollection webModelDimCollection = new WebModelDimensionCollection();

            if (this.variantDimension1Code != "") addDimension(webModelDimCollection, this.variantDimension1Code);
            if (this.variantDimension2Code != "") addDimension(webModelDimCollection, this.variantDimension2Code);
            if (this.variantDimension3Code != "") addDimension(webModelDimCollection, this.variantDimension3Code);
            if (this.variantDimension4Code != "") addDimension(webModelDimCollection, this.variantDimension4Code);

            return webModelDimCollection;
        }

        public WebModelDimValueCollection getMatrixDimensionValues()
        {
            WebItemVarDim vertWebItemVarDim = new WebItemVarDim(infojetContext, this._variantDimension1Code);
            WebModelDimValueCollection vertMatrixDimensionValues = vertWebItemVarDim.getDimValues(no);

            WebItemVarDim horizWebItemVarDim = new WebItemVarDim(infojetContext, this._variantDimension2Code);
            WebModelDimValueCollection horizMatrixDimensionValues = horizWebItemVarDim.getDimValues(no);


            int i = 0;
            while (i < vertMatrixDimensionValues.Count)
            {
                WebModelDimValueCollection webModelDimValueCollection = horizMatrixDimensionValues.clone();

                vertMatrixDimensionValues[i].subLevelValues = webModelDimValueCollection;
                
                i++;

            }

            vertMatrixDimensionValues.bindItems(this);

            return vertMatrixDimensionValues;
        }

        public WebModelDimValueCollection getHorizDimensionValues()
        {
            WebItemVarDim horizWebItemVarDim = new WebItemVarDim(infojetContext, this._variantDimension2Code);
            WebModelDimValueCollection horizMatrixDimensionValues = horizWebItemVarDim.getDimValues(no);

            return horizMatrixDimensionValues;
        }

        private void addDimension(WebModelDimensionCollection webModelDimCollection, string dimensionCode)
        {
            WebItemVarDim webItemVarDim = new WebItemVarDim(infojetContext, dimensionCode);
            WebModelDimension webModelDim = new WebModelDimension(webItemVarDim);
            WebModelDimValueCollection valueCollection = webItemVarDim.getDimValues(no);

            if (webModelDimCollection.Count > 0)
            {
                int i = 0;
                while (i < valueCollection.Count)
                {
                    string dimValue1 = "";
                    string dimValue2 = "";
                    string dimValue3 = "";
                    string dimValue4 = "";
                    if ((webModelDimCollection.Count >= 1) && (dimensionTable[this._variantDimension1Code] != null)) dimValue1 = dimensionTable[this._variantDimension1Code].ToString();
                    if ((webModelDimCollection.Count >= 2) && (dimensionTable[this._variantDimension2Code] != null)) dimValue2 = dimensionTable[this._variantDimension2Code].ToString();
                    if ((webModelDimCollection.Count >= 3) && (dimensionTable[this._variantDimension3Code] != null)) dimValue3 = dimensionTable[this._variantDimension3Code].ToString();
                    if ((webModelDimCollection.Count >= 4) && (dimensionTable[this._variantDimension4Code] != null)) dimValue4 = dimensionTable[this._variantDimension4Code].ToString();
                    if (webModelDimCollection.Count == 1) dimValue2 = valueCollection[i].code;
                    if (webModelDimCollection.Count == 2) dimValue3 = valueCollection[i].code;
                    if (webModelDimCollection.Count == 3) dimValue4 = valueCollection[i].code;
                    
                    WebModelVariants webModelVariants = new WebModelVariants(infojetContext);
                    if (webModelVariants.getVariant(this.no, dimValue1, dimValue2, dimValue3, dimValue4) == null)
                    {
                        valueCollection.RemoveAt(i);
                        i--;
                    }
                    i++;
                }
            }

            if (valueCollection.Count > 0)
            {
                if (!dimensionTable.Contains(webModelDim.code))
                {
                    dimensionTable.Add(webModelDim.code, valueCollection[0].code);
                }
                else
                {
                    bool acceptedValue = false;
                    int j = 0;
                    while (j < valueCollection.Count)
                    {
                        if (valueCollection[j].code == dimensionTable[webModelDim.code].ToString()) acceptedValue = true;

                        j++;
                    }
                    if (!acceptedValue) dimensionTable[webModelDim.code] = valueCollection[0].code;

                }
            }

            webModelDim.values = valueCollection;
            webModelDimCollection.Add(webModelDim);
        }

        public string getDimensionValue(string dimCode)
        {
            if (dimensionTable.Contains(dimCode)) return dimensionTable[dimCode].ToString();
            return "";
        }

        public WebModelVariant getVariant()
        {
            if (variantTable == null)
            {

                WebModelVariants webModelVariants = new WebModelVariants(infojetContext);
                variantTable = webModelVariants.getTable(this.no);
            }

            string varDimValue1 = "";
            if (dimensionTable[this.variantDimension1Code] != null) varDimValue1 = dimensionTable[this.variantDimension1Code].ToString();
            string varDimValue2 = "";
            if (dimensionTable[this.variantDimension2Code] != null) varDimValue2 = dimensionTable[this.variantDimension2Code].ToString();
            string varDimValue3 = "";
            if (dimensionTable[this.variantDimension3Code] != null) varDimValue3 = dimensionTable[this.variantDimension3Code].ToString();
            string varDimValue4 = "";
            if (dimensionTable[this.variantDimension4Code] != null) varDimValue4 = dimensionTable[this.variantDimension4Code].ToString();

            //WebModelVariant webModelVariant = webModelVariants.getVariant(this.no, varDimValue1, varDimValue2, varDimValue3, varDimValue4);
            //if (webModelVariant != null) return webModelVariant;
            WebModelVariant webModelVariant = (WebModelVariant)variantTable[varDimValue1 + "-" + varDimValue2 + "-" + varDimValue3 + "-" + varDimValue4];
            if (webModelVariant != null) return webModelVariant;
            return null;
        }

        public void setDimension(string code, string value)
        {
            if (dimensionTable.Contains(code))
            {
                dimensionTable[code] = value;
            }
            else
            {
                dimensionTable.Add(code, value);
            }
        }

        public string getDefaultItemNo()
        {
            string itemNo = "";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] WHERE [Web Model No_] = @no AND [Primary] = 1");
            databaseQuery.addStringParameter("no", _no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                itemNo = dataReader.GetValue(0).ToString();

            }
            dataReader.Close();

            return itemNo;
        }

        public ProductItem getProductItem(Infojet infojetContext, WebPageLine webPageLine)
        {
            WebItemList webItemList = new WebItemList(infojetContext, webPageLine.code);

            Item item = new Item(infojetContext.systemDatabase, getDefaultItemNo());
            ProductItem productItem = new ProductItem(this, item, infojetContext.languageCode);


            Items items = new Items();
            Hashtable itemInfoTable = items.getItemInfo(item, infojetContext, 1);

            WebItemImages webItemImages = new WebItemImages(infojetContext.systemDatabase);
            productItem.productImages = webItemImages.getWebItemImages(item.no, 0, infojetContext.webSite.code).toProductImageCollection(productItem.description);
            productItem.productImages.setChangeUrl(infojetContext.webPage.getUrl() + "&itemNo=" + this.no+"&webModelNo="+no);
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
                productItem.inventoryText = items.getInventoryText(item, ((ItemInfo)itemInfoTable[productItem.item.no]).inventory, infojetContext, false);

                productItem.item.unitPrice = ((ItemInfo)itemInfoTable[productItem.item.no]).unitPrice;
                productItem.item.unitListPrice = ((ItemInfo)itemInfoTable[productItem.item.no]).unitListPrice;

                productItem.formatedUnitPrice = productItem.item.formatUnitPrice(infojetContext.presentationCurrencyCode);
                productItem.formatedUnitListPrice = productItem.item.formatUnitListPrice(infojetContext.presentationCurrencyCode);


            }

            productItem.itemAttributes = ItemAttributeVisibilities.getItemAttributes(infojetContext, webItemList.code, item.no);

            return productItem;
        }

        public DataSet getVariants()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT i.[No_], i.[Description], i.[Description 2], i.[Unit Price], i.[Sales Unit of Measure], i.[Manufacturer Code], i.[Lead Time Calculation], i.[Unit List Price], i.[Item Disc_ Group], i.[VAT Prod_ Posting Group] FROM [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v, [" + infojetContext.systemDatabase.getTableName("Item") + "] i WHERE i.[No_] = v.[Item No_] AND v.[Web Model No_] = @webModelNo");
            databaseQuery.addStringParameter("webModelNo", no, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

        public ProductItemCollection getModelVariantInfo(string locationCode, bool calcPrices, bool calcInventory)
        {
            ProductItemCollection productItemCollection = new ProductItemCollection();

            DataSet dataSet = this.getVariants();
            Items items = new Items();
            Hashtable itemInfoTable = items.getItemInfo(dataSet, infojetContext, calcPrices, calcInventory);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                Item item = new Item(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                ProductItem productItem = new ProductItem(item, infojetContext.languageCode);

                if (itemInfoTable[item.no] != null)
                {
                    ItemInfo itemInfo = (ItemInfo)itemInfoTable[item.no];
                    productItem.inventory = itemInfo.inventory;
                    productItem.inventoryText = items.getInventoryText(productItem.item, productItem.inventory, infojetContext, true);

                    //if (item.no == "12008-R-S") throw new Exception("Inv: "+itemInfo.inventory);

                    productItem.itemReceiptInfoCollection = itemInfo.itemReceiptInfoCollection;

                    if (itemInfo.itemReceiptInfoCollection != null)
                    {

                    int z = 0;
                    while (z < itemInfo.itemReceiptInfoCollection.Count)
                    {
                        string nextPlannedStr = "";

                        ItemReceiptInfo itemReceiptInfo = itemInfo.itemReceiptInfoCollection[z];
                        if (itemReceiptInfo.nextPlannedReceiptDate != DateTime.MinValue)
                        {
                            //nextPlannedStr = itemReceiptInfo.nextPlannedReceiptDate.ToString("yyyy-MM-dd") + " (" + itemReceiptInfo.nextPlannedReceiptQty + ")";
                            nextPlannedStr = itemReceiptInfo.nextPlannedReceiptDate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            if (z == 1)
                            {
                                nextPlannedStr = infojetContext.translate("UNCONF RECEIPT DATE");
                            }
                        }

                        if (z == 0) productItem.nextPlannedReceipt = nextPlannedStr;
                        if (z == 1) productItem.secondPlannedReceipt = nextPlannedStr;

                        z++;
                    }

                    if (itemInfo.itemReceiptInfoCollection.Count == 0)
                    {
                        productItem.nextPlannedReceipt = infojetContext.translate("UNCONF RECEIPT DATE");
                    }

                    }
                    productItem.item.unitPrice = itemInfo.unitPrice;
                    productItem.item.unitListPrice = itemInfo.unitListPrice;

                    productItem.formatedUnitPrice = productItem.item.formatUnitPrice(infojetContext.presentationCurrencyCode);
                    productItem.formatedUnitListPrice = productItem.item.formatUnitListPrice(infojetContext.presentationCurrencyCode);
                }

                productItemCollection.Add(productItem);

                i++;
            }

            return productItemCollection;
        }

        public string no { get { return _no; } }
        public string description { get { return _description; } }
        public string description2 { get { return _description2; } }
        public string variantDimension1Code { get { return _variantDimension1Code; } }
        public string variantDimension2Code { get { return _variantDimension2Code; } }
        public string variantDimension3Code { get { return _variantDimension3Code; } }
        public string variantDimension4Code { get { return _variantDimension4Code; } }

    }
}
