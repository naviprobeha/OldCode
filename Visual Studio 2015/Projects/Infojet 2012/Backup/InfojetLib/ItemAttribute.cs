using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class ItemAttribute
    {
        private string _code;
        private string _webTextConstantCode;
        private string _text;
        private string _itemValue;

        private ItemAttributeCollection _itemAttributeValueCollection;

        public ItemAttribute(Infojet infojetContext, ItemAttributeVisibility itemAttributeVisibility)
        {
            this._code = itemAttributeVisibility.itemAttributeCode;
            this._webTextConstantCode = itemAttributeVisibility.webTextConstantCode;
            this._text = infojetContext.translate(this._webTextConstantCode);

        }

        public ItemAttribute(ItemAttribute itemAttribute, string itemValue)
        {
            this._code = itemAttribute.code;
            this._webTextConstantCode = itemAttribute.webTextConstantCode;
            this._text = itemAttribute.text;
            this.itemValue = itemValue;

        }
        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        public string webTextConstantCode
        {
            get
            {
                return _webTextConstantCode;
            }
            set
            {
                _webTextConstantCode = value;
            }
        }

        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public string itemValue
        {
            get
            {
                return _itemValue;
            }
            set
            {
                _itemValue = value;
            }
        }

        public ItemAttributeCollection itemAttributeValueCollection
        {
            get
            {
                return _itemAttributeValueCollection;
            }
            set
            {
                _itemAttributeValueCollection = value;
            }
        }

    }
}
