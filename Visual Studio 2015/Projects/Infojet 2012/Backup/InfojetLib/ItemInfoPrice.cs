using System;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for ItemInfo.
    /// </summary>
    public class ItemInfoPrice
    {
        private float _minQuantity;
        private float _unitPrice;
        private float _lineDiscount;
        private string _formatedUnitPrice;

        public ItemInfoPrice()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public float minQuantity { get { return _minQuantity; } set { _minQuantity = value; } }
        public float unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
        public float lineDiscount { get { return _lineDiscount; } set { _lineDiscount = value; } }
        public string formatedUnitPrice { get { return _formatedUnitPrice; } set { _formatedUnitPrice = value; } }
    }

}
