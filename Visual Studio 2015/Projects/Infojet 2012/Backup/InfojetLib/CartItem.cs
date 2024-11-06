using System;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for CallbackArgument.
	/// </summary>
	public class CartItem
	{
		private int _lineNo;
        private string _itemNo;
        private string _description;
        private string _referenceNo;
        private string _extra1;
        private string _extra2;
        private string _extra3;
        private string _extra4;
        private string _extra5;
        private float _unitPrice;
        private float _quantity;
        private float _qtyToShip;
        private float _outstandingQty;
        private string _nextReceiptDate;
        private float _amount;
        private string _formatedAmount;
        private string _formatedUnitPrice;
        private string _removeLink;
        private float _inventory;
        private float _minOrderableQty;
        private bool _checkInventory;
        private string _inventoryText;
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _webModelNo;
        private WebCartConfigLineCollection _webCartConfigLines;
      
        public CartItem()
        { }

		public CartItem(WebCartLine webCartLine)
		{
			//
			// TODO: Add constructor logic here
			//
			_lineNo = webCartLine.entryNo;
			_itemNo = webCartLine.itemNo;
			_unitPrice = webCartLine.unitPrice;
			_quantity = webCartLine.quantity;
			_amount = webCartLine.amount;
            _referenceNo = webCartLine.referenceNo;
            _extra1 = webCartLine.extra1;
            _extra2 = webCartLine.extra2;
            _extra3 = webCartLine.extra3;
            _extra4 = webCartLine.extra4;
            _extra5 = webCartLine.extra5;
            _fromDate = webCartLine.fromDate;
            _toDate = webCartLine.toDate;

            _webCartConfigLines = webCartLine.getWebCartConfigLines();
        }

        public int lineNo { get { return _lineNo; } set { _lineNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public string referenceNo { get { return _referenceNo; } set { _referenceNo = value; } }
        public string extra1 { get { return _extra1; } set { _extra1 = value; } }
        public string extra2 { get { return _extra2; } set { _extra2 = value; } }
        public string extra3 { get { return _extra3; } set { _extra3 = value; } }
        public string extra4 { get { return _extra4; } set { _extra4 = value; } }
        public string extra5 { get { return _extra5; } set { _extra5 = value; } }
        public float unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public float quantity { get { return _quantity; } set { _quantity = value; } }
        public float qtyToShip { get { return _qtyToShip; } }
        public float outstandingQty { get { return _outstandingQty; } }
        public string nextReceiptDate { get { return _nextReceiptDate; } }
        public float amount { get { return _amount; } set { _amount = value; } }
        public string formatedAmount { get { return _formatedAmount; } set { _formatedAmount = value; } }
        public string formatedUnitPrice { get { return _formatedUnitPrice; } set { _formatedUnitPrice = value; } }
        public float inventory { get { return _inventory; } set { _inventory = value; } }
        public bool checkInventory { get { return _checkInventory; } set { _checkInventory = value; } }
        public string inventoryText { get { return _inventoryText; } set { _inventoryText = value; } }
        public string removeLink { get { return _removeLink; } set { _removeLink = value; } }
        public DateTime fromDate { get { return _fromDate; } }
        public DateTime toDate { get { return _toDate; } }
        public string fromDateText { get { if (_fromDate.Year > 1754) return _fromDate.ToString("yyyy-MM-dd HH:mm"); return ""; } }
        public string toDateText { get { if (_toDate.Year > 1754) return _toDate.ToString("yyyy-MM-dd HH:mm"); return ""; } }
        public string webModelNo { get { return _webModelNo; } set { _webModelNo = value; } }
        public float minOrderableQty { get { return _minOrderableQty; } set { _minOrderableQty = value; } }
        public WebCartConfigLineCollection webCartConfigLines { get { return _webCartConfigLines; } set { _webCartConfigLines = value; } }

        public void setTextLength(int length)
        {
           if (description.Length > length) description = description.Substring(0, length)+"...";
        }

        public void applyItemInfo(ItemInfo itemInfo)
        {
            _qtyToShip = _quantity;
            _outstandingQty = 0;

            if (itemInfo.inventory < _quantity)
            {
                if (itemInfo.inventory < 0)
                {
                    _qtyToShip = 0;
                }
                else
                {
                    _qtyToShip = itemInfo.inventory;
                }
                _outstandingQty = _quantity - _qtyToShip;

            }

            if (itemInfo.itemReceiptInfoCollection != null)
            {
                if (itemInfo.itemReceiptInfoCollection.Count > 0)
                {
                    _nextReceiptDate = itemInfo.itemReceiptInfoCollection[0].nextPlannedReceiptDate.ToString("yyyy-MM-dd");

                }
            }
        }
 
	}
}
