using System;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ItemInfo.
	/// </summary>
	public class ItemInfo
	{
		private string _no;
        private string _variantCode;
		private float _unitPrice;
        private float _unitListPrice;
        private float _lineDiscount;
        private float _inventory;
        private float _quantity;
        private ItemReceiptInfoCollection _itemReceiptInfoCollection;
        private ItemInfoPriceCollection _itemInfoPriceCollection;

		public ItemInfo()
		{
			//
			// TODO: Add constructor logic here
			//
			// 
            itemInfoPriceCollection = new ItemInfoPriceCollection();
		}

        public string no { get { return _no; } set { _no = value; } }
        public string variantCode { get { return _variantCode; } set { _variantCode = value; } }
        public float unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public float unitListPrice { get { return _unitListPrice; } set { _unitListPrice = value; } }
        public float lineDiscount { get { return _lineDiscount; } set { _lineDiscount = value; } }
        public float inventory { get { return _inventory; } set { _inventory = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public ItemReceiptInfoCollection itemReceiptInfoCollection { get { return _itemReceiptInfoCollection; } set { _itemReceiptInfoCollection = value; } }
        public ItemInfoPriceCollection itemInfoPriceCollection { get { return _itemInfoPriceCollection; } set { _itemInfoPriceCollection = value; } }
    }

}
