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
        private string _removeLink;

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
        }

        public int lineNo { get { return _lineNo; } }
        public string itemNo { get { return _itemNo; } }
        public string description { get { return _description; } set { _description = value; } }
        public string referenceNo { get { return _referenceNo; } }
        public string extra1 { get { return _extra1; } }
        public string extra2 { get { return _extra2; } }
        public string extra3 { get { return _extra3; } }
        public string extra4 { get { return _extra4; } }
        public string extra5 { get { return _extra5; } }
        public float unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public float quantity { get { return _quantity; } }
        public float qtyToShip { get { return _qtyToShip; } }
        public float outstandingQty { get { return _outstandingQty; } }
        public string nextReceiptDate { get { return _nextReceiptDate; } }
        public float amount { get { return _amount; } set { _amount = value; } }
        public string formatedAmount { get { return _formatedAmount; } set { _formatedAmount = value; } }
        public string removeLink { get { return _removeLink; } set { _removeLink = value; } }

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
