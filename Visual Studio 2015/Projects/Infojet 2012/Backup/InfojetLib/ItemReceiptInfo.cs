using System;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for ItemInfo.
    /// </summary>
    public class ItemReceiptInfo
    {
        private DateTime _nextPlannedReceiptDate;
        private float _nextPlannedReceiptQty;

        public ItemReceiptInfo()
        {
            //
            // TODO: Add constructor logic here
            //

            _nextPlannedReceiptDate = DateTime.MinValue;
        }

        public DateTime nextPlannedReceiptDate { get { return _nextPlannedReceiptDate; } set { _nextPlannedReceiptDate = value; } }
        public float nextPlannedReceiptQty { get { return _nextPlannedReceiptQty; } set { _nextPlannedReceiptQty = value; } }
    }

}
