using System;
using System.Data;
using System.Collections;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for CallbackArgument.
    /// </summary>
    public class ProductItem
    {
        private string _no = "";
        private Item _item;
        private string _extendedText = null;
        private string _extendedTextFull = null;
        private string _extendedTextShort = null;
        private ProductImageCollection _productImages;
        private ProductImageCollection _campainImages;
        private ProductImage _productImage;
        private ProductImage _campainImage;
        private string _formatedUnitPrice = "";
        private string _formatedUnitListPrice = "";
        private float _inventory = 0;
        private string _link = "";
        private string _buyLink = "";
        private string _commandArgument = "";
        private DateTime _nextPlannedDelivery;
        private DateTime _secondPlannedDelivery;
        private ItemReceiptInfoCollection _itemReceiptInfoCollection;
        private ProductTranslation itemTranslation;
        private ItemAttributeCollection _itemAttributes;
        private WebItemSetting _webItemSetting;
        //private string _languageCode;
        private ItemInfoPriceCollection _quantityPriceCollection;

        private Infojet infojetContext;

        private string _noImageUrl;
        private string _thumbnailImageUrl;
        private WebModel _webModel;

        public ProductItem(Infojet infojetContext, Item item)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            if (item != null)
            {
                this._item = item;
                this._no = item.no;
                this._productImages = new ProductImageCollection();

                this._webItemSetting = new WebItemSetting(infojetContext, 0, this._no);

                this._commandArgument = "0:" + no;
            }


        }

        public ProductItem(Infojet infojetContext, WebModel webModel, Item item)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            if (webModel != null)
            {
                this._no = webModel.no;
                this._webModel = webModel;
                this.itemTranslation = webModel.getTranslation(infojetContext.languageCode);

                this._webItemSetting = new WebItemSetting(infojetContext, 1, this._no);

                this._commandArgument = "1:" + no;

            }
            if (item != null)
            {
                this._item = item;
                this._productImages = new ProductImageCollection();

            }



        }

        public void setExtendedTextLength(int maxLength)
        {
            if (extendedText != null)
            {
                if (maxLength == 0) return;
                if (maxLength > extendedText.Length) return;

                string extendedTextPart = extendedText;

                extendedTextPart = extendedText.Substring(0, maxLength);

                int i = 1;
                while ((extendedTextPart[maxLength - i] != ' ') && (i < maxLength - 1))
                {
                    i++;
                }

                this._extendedText = extendedTextPart.Substring(0, maxLength - i) + "...";
            }
        }

        public string description
        {
            get
            {
                if (itemTranslation == null)
                {
                    if (item != null)
                    {
                        this.itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                    }
                }

                if (itemTranslation != null)
                {
                    if (itemTranslation.description != "") return itemTranslation.description.Replace(" & ", " &amp; ");
                    return item.description.Replace(" & ", " &amp; ");
                }
                return "";
            }
        }

        public string description2
        {
            get
            {
                if (itemTranslation != null)
                {
                    if (itemTranslation.description2 != "") return itemTranslation.description2;
                    return item.description2;
                }
                return "";
            }
        }

        public string extendedText
        {
            get
            {
                if (_extendedText == null) _extendedText = extendedTextFull;
                return _extendedText;
            }
        }

        public string extendedTextFull
        {
            get
            {
                if (item != null)
                {
                    if (_extendedTextFull == null) _extendedTextFull = item.getItemText(infojetContext.languageCode, true);
                    if (_extendedTextFull == null) _extendedTextFull = "";
                }
                return _extendedTextFull;
            }
        }

        public string extendedTextShort
        {
            get
            {
                if (item != null)
                {
                    if (_extendedTextShort == null) _extendedTextShort = item.getItemText(infojetContext.languageCode, false);
                    if (_extendedTextShort == null) _extendedTextShort = "";
                }
                return _extendedTextShort;
            }
        }

        public string link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
            }
        }

        public string formatedUnitPrice
        {
            get
            {
                return _formatedUnitPrice;
            }
            set
            {
                _formatedUnitPrice = value;
            }
        }

        public string formatedUnitListPrice
        {
            get
            {
                return _formatedUnitListPrice;
            }
            set
            {
                _formatedUnitListPrice = value;
            }
        }

        public ProductImageCollection productImages
        {
            get
            {
                return _productImages;
            }
            set
            {
                _productImages = value;
            }
        }

        public ProductImageCollection campainImages
        {
            get
            {
                return _campainImages;
            }
            set
            {
                _campainImages = value;
            }
        }

        public ProductImage productImage
        {
            get
            {
                return _productImage;
            }
            set
            {
                _productImage = value;
            }
        }

        public ProductImage campainImage
        {
            get
            {
                return _campainImage;
            }
            set
            {
                _campainImage = value;
            }
        }


        public float inventory
        {
            get
            {
                return _inventory;
            }
        }

        public string inventoryText
        {
            get
            {
                return getInventoryText();
            }
        }

        public Item item
        {
            get
            {
                return _item;
            }
        }

        public string productImageUrl
        {
            get
            {
                if (_productImage != null) return this._productImage.url;
                return "";
            }
        }

        public string campainImageUrl
        {
            get
            {
                if (_campainImage != null) return this._campainImage.url;
                return "";
            }
        }

        public string thumbnailImageUrl
        {
            get
            {
                return _thumbnailImageUrl;
            }
            set
            {
                _thumbnailImageUrl = value;
            }
        }

        public string no
        {
            get
            {
                if ((_no != "") && (_no != null)) return _no;
                if (item != null) return this._item.no;
                return "";
            }
        }

        public string buyLink
        {
            get
            {
                return _buyLink;
            }
            set
            {
                _buyLink = value;
            }

        }

        public ItemAttributeCollection itemAttributes
        {
            get
            {
                return _itemAttributes;
            }
            set
            {
                _itemAttributes = value;
            }
        }

        public ItemInfoPriceCollection quantityPriceCollection
        {
            get
            {
                return _quantityPriceCollection;
            }
            set
            {
                _quantityPriceCollection = value;
            }
        }

        public string nextPlannedDelivery
        {
            get
            {
                if (_nextPlannedDelivery.Year > 2000) return _nextPlannedDelivery.ToString("yyyy-MM-dd");
                return "";
            }
            set
            {
                _nextPlannedDelivery = DateTime.Parse(value);
            }
        }

        public string secondPlannedDelivery
        {
            get
            {
                if (_secondPlannedDelivery.Year > 2000) return _nextPlannedDelivery.ToString("yyyy-MM-dd");
                return "";
            }
            set
            {
                _secondPlannedDelivery = DateTime.Parse(value);
            }
        }

        public ItemReceiptInfoCollection itemReceiptInfoCollection
        {
            get
            {
                return _itemReceiptInfoCollection;
            }
            set
            {
                _itemReceiptInfoCollection = value;
            }
        }

        public string manufacturer
        {
            get
            {
                if (item != null) return item.manufacturerCode;
                return "";
            }
        }

        public string leadTime
        {
            get
            {
                return getLeadTimeText();
            }
        }

        public string commandArgument
        {
            get
            {
                return _commandArgument;
            }
        }

        public WebItemSetting webItemSetting
        {
            get
            {
                return _webItemSetting;
            }
        }

        public WebItemRelationCollection itemRelations
        {
            get
            {
                return WebItemRelation.getWebItemRelationCollection(infojetContext, item.no);
            }
        }

        public WebItemSaleCollection itemSales
        {
            get
            {
                return WebItemSale.getWebItemSaleCollection(infojetContext, item.no);
            }
        }

        public WebItemDocumentCollection itemDocuments
        {
            get
            {
                return WebItemDocument.getWebItemDocumentCollection(infojetContext, item.no);
            }
        }

        public bool checkVisibility()
        {
            if (_webItemSetting != null)
            {
                if (_webItemSetting.availability == 1)
                {
                    if ((infojetContext.webSite.hideZeroInventoryItems) && (_inventory < _webItemSetting.safetyInventoryLevel)) return false;
                    return true;
                }
                if (_webItemSetting.availability == 2) return true;
                if (_webItemSetting.availability == 3) return false;
            }
            return true;
        }

        public void setInventory(float inventoryValue)
        {
            _inventory = inventoryValue;
            if (_webItemSetting.visibility == 6) _inventory = _webItemSetting.fixedInventoryValue;

            _nextPlannedDelivery = _webItemSetting.preOrderDeliveryDate;
        }

        private string getInventoryText()
        {
            Hashtable unitOfMeasureTable = UnitOfMeasureTranslations.getTranslations(infojetContext, infojetContext.languageCode);
            if (_inventory < 0) _inventory = 0;
            string inventoryUnit = inventory.ToString();
            if (unitOfMeasureTable[item.salesUnitOfMeasure] != null) inventoryUnit = inventoryUnit +" " + unitOfMeasureTable[item.salesUnitOfMeasure].ToString();

            if (_webItemSetting != null)
            {
                if (_webItemSetting.visibility == 1) return inventoryUnit;
                if (_webItemSetting.visibility == 2)
                {
                    if (inventory > 0)
                    {
                        return infojetContext.translate(infojetContext.webSite.inStockTextConstantCode);
                    }
                    else
                    {
                        return infojetContext.translate(infojetContext.webSite.noInStockTextConstantCode);
                    }
                }
                if (_webItemSetting.visibility == 3) return infojetContext.translate("INV ORDERABLE");
                if (_webItemSetting.visibility == 4) return infojetContext.translate("INV PRE-ORDER");
                if (_webItemSetting.visibility == 5) return infojetContext.translate("INV PHONE ORDER");
                if (_webItemSetting.visibility == 6) return inventoryUnit;
            }

            return "";
        }

        private string getLeadTimeText()
        {
            if (_webItemSetting.leadTimeDays > 0)
            {
                return _webItemSetting.leadTimeDays + " " + infojetContext.translate("DAYS");
            }
            return "";
        }

        public bool isAvailable(string webSiteCode)
        {
            if (webItemSetting.availability == 3) return false;

            DatabaseQuery databaseQuery;
            System.Data.SqlClient.SqlDataReader dataReader;
            bool requireItemTranslation = false;

            if (infojetContext.userSession != null)
            {
                //dataReader = infojetContext.systemDatabase.query("SELECT c.[Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] m, [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category User Group") + "] cg, [" + infojetContext.systemDatabase.getTableName("Web User Account Group") + "] g WHERE m.[Web Site Code] = '" + webSiteCode + "' AND m.[Type] = " + (webItemSetting.type + 1) + " AND m.[Code] = '" + this.webItemSetting.no + "' AND c.[Web Site Code] = m.[Web Site Code] AND c.[Code] = m.[Web Item Category Code] AND (((c.[Protected] = 1) OR (c.[View Only] = 1) AND cg.[Web Site Code] = c.[Web Site Code] AND cg.[Web Item Category Code] = c.[Code] AND cg.[Web User Group Code] = g.[Web User Group Code] AND g.[No_] = '" + infojetContext.userSession.webUserAccount.no + "'))");
                databaseQuery = infojetContext.systemDatabase.prepare("SELECT c.[Code], c.[Require Item Translation] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] m, [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category User Group") + "] cg, [" + infojetContext.systemDatabase.getTableName("Web User Account Group") + "] g WHERE m.[Web Site Code] = @webSiteCode AND m.[Type] = @type AND m.[Code] = @code AND c.[Web Site Code] = m.[Web Site Code] AND c.[Code] = m.[Web Item Category Code] AND (((c.[Protected] = 1) OR (c.[View Only] = 1) AND cg.[Web Site Code] = c.[Web Site Code] AND cg.[Web Item Category Code] = c.[Code] AND cg.[Web User Group Code] = g.[Web User Group Code] AND g.[No_] = @webUserAccountNo))");
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addIntParameter("type", webItemSetting.type + 1);
                databaseQuery.addStringParameter("code", this.webItemSetting.no, 20);
                databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);

                dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (int.Parse(dataReader.GetValue(1).ToString()) == 1) requireItemTranslation = true;
                    dataReader.Close();

                    if (requireItemTranslation)
                    {
                        if (ItemTranslation.getItemTranslation(infojetContext, item) == null) return false;
                    }
                    return true;
                }
                dataReader.Close();
            }

            //dataReader = infojetContext.systemDatabase.query("SELECT c.[Code], c.[View Only] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] m, [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c WHERE m.[Web Site Code] = '" + webSiteCode + "' AND m.[Type] = " + (webItemSetting.type + 1) + " AND m.[Code] = '" + this.webItemSetting.no + "' AND c.[Web Site Code] = m.[Web Site Code] AND c.[Code] = m.[Web Item Category Code] AND c.[Protected] = 0");
            databaseQuery = infojetContext.systemDatabase.prepare("SELECT c.[Code], c.[View Only], c.[Require Item Translation] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] m, [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c WHERE m.[Web Site Code] = @webSiteCode AND m.[Type] = @type AND m.[Code] = @code AND c.[Web Site Code] = m.[Web Site Code] AND c.[Code] = m.[Web Item Category Code] AND c.[Protected] = 0");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addIntParameter("type", webItemSetting.type + 1);
            databaseQuery.addStringParameter("code", this.webItemSetting.no, 20);

            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (dataReader.GetValue(1).ToString() == "1") webItemSetting.setViewOnly();
                requireItemTranslation = false;
                if (int.Parse(dataReader.GetValue(1).ToString()) == 1) requireItemTranslation = true;
                dataReader.Close();
                if (requireItemTranslation)
                {
                    if (ItemTranslation.getItemTranslation(infojetContext, item) == null) return false;
                }
                return true;
            }
            dataReader.Close();

            return false;

        }

        public bool isBuyable
        {
            get
            {
                if (webItemSetting.availability == 3) return false;
                if (webItemSetting.availability == 4) return false;
                if ((infojetContext.userSession != null) && (!infojetContext.userSession.webUserAccount.allowOrdering)) return false;
                if (infojetContext.webSite.allowPurchaseNotLoggedIn) return true;
                if (infojetContext.userSession == null) return false;
                return true;
            }
        }

        public string getAttributeValue(string attributeCode)
        {
            ItemAttributeValue itemAttributeValue = new ItemAttributeValue(infojetContext.systemDatabase, item.no, attributeCode, infojetContext.languageCode);
            return itemAttributeValue.attributeValue;
        }

        public string noImageUrl
        {
            get
            {
                return this._noImageUrl;
            }
            set
            {
                this._noImageUrl = value;
            }
        }

        public string imageUrl
        {
            get
            {
                if (productImage != null) return productImage.url;
                return _noImageUrl;
            }
        }

        public WebModelDimValueCollection matrixDimension
        {
            get
            {
                if (webItemSetting.type == 1)
                {
                    return _webModel.getMatrixDimensionValues();
                }
                WebModelDimValueCollection emptyCollection = new WebModelDimValueCollection();
                return emptyCollection;
            }
        }

        public WebModelDimValueCollection horizDimension
        {
            get
            {
                if (webItemSetting.type == 1)
                {
                    return _webModel.getHorizDimensionValues();
                }
                WebModelDimValueCollection emptyCollection = new WebModelDimValueCollection();
                return emptyCollection;
            }
        }

        public void setItemTexts(Hashtable fullItemTextTable, Hashtable shortItemTexts)
        {
            if (fullItemTextTable.Contains(item.no))
            {
                _extendedTextFull = (string)fullItemTextTable[item.no];
            }
            else
            {
                _extendedTextFull = null;
            }
            if (shortItemTexts.Contains(item.no))
            {
                _extendedTextShort = (string)shortItemTexts[item.no];
            }
            else
            {
                _extendedTextShort = null;
            }
        }
 

    }
}
