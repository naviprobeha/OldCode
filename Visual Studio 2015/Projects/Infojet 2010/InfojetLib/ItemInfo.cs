using System;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ItemInfo.
	/// </summary>
	public class ItemInfo
	{
		private string _no;
		private float _unitPrice;
        private float _unitListPrice;
        private float _lineDiscount;
        private float _inventory;
        private ItemReceiptInfoCollection _itemReceiptInfoCollection;

		public ItemInfo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public string no { get { return _no; } set { _no = value; } }
        public float unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public float unitListPrice { get { return _unitListPrice; } set { _unitListPrice = value; } }
        public float lineDiscount { get { return _lineDiscount; } set { _lineDiscount = value; } }
        public float inventory { get { return _inventory; } set { _inventory = value; } }
        public ItemReceiptInfoCollection itemReceiptInfoCollection { get { return _itemReceiptInfoCollection; } set { _itemReceiptInfoCollection = value; } }
	}

}
