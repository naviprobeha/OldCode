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
        private string _no;
        private Item _item;
        private string _extendedText = null;
        private string _extendedTextFull = null;
        private ProductImageCollection _productImages;
        private ProductImageCollection _campainImages;
        private ProductImage _productImage;
        private ProductImage _campainImage;
        private string _formatedUnitPrice;
        private string _formatedUnitListPrice;
        private float _inventory;
        private string _inventoryText;
        private string _link;
        private string _buyLink;
        private string _nextPlannedReceipt;
        private string _secondPlannedReceipt;
        private ItemReceiptInfoCollection _itemReceiptInfoCollection;
        private ProductTranslation itemTranslation;
        private ItemAttributeCollection _itemAttributes;
        private string _languageCode;

        public ProductItem(Item item, string languageCode)
        {
            //
            // TODO: Add constructor logic here
            //
            if (item != null)
            {
                this._item = item;
                this._no = item.no;
                this._productImages = new ProductImageCollection();

            }
            this._languageCode = languageCode;
        }

        public ProductItem(WebModel webModel, Item item, string languageCode)
        {
            //
            // TODO: Add constructor logic here
            //
            if (webModel != null)
            {
                this._no = webModel.no;
                this.itemTranslation = webModel.getTranslation(languageCode);

            }
            if (item != null)
            {
                this._item = item;
                this._productImages = new ProductImageCollection();

            }
            this._languageCode = languageCode;

        }

        public void setExtendedTextLength(int maxLength)
        {
            if (_extendedText == null) _extendedText = extendedTextFull;
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

        public string description
        {
            get
            {
                if (itemTranslation == null)
                {
                    if (item != null)
                    {
                        this.itemTranslation = item.getItemTranslation(_languageCode);
                    }
                }

                if (itemTranslation != null)
                {
                    if (itemTranslation.description != "") return itemTranslation.description;
                    return item.description;
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
                    if (_extendedTextFull == null) _extendedTextFull = item.getItemText(this._languageCode);
                    if (_extendedTextFull == null) _extendedTextFull = "";
                }
                return _extendedTextFull;
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
            set
            {
                _inventory = value;
            }
        }

        public string inventoryText
        {
            get
            {
                return _inventoryText;
            }
            set
            {
                _inventoryText = value;
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

        public string nextPlannedReceipt
        {
            get
            {
                return _nextPlannedReceipt;
            }
            set
            {
                _nextPlannedReceipt = value;
            }
        }

        public string secondPlannedReceipt
        {
            get
            {
                return _secondPlannedReceipt;
            }
            set
            {
                _secondPlannedReceipt = value;
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


    }
}
