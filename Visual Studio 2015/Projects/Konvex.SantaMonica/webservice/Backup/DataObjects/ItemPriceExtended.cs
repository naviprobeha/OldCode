using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class ItemPriceExtended
    {
        private int _entryNo;
        private string _itemNo;
        private DateTime _startingDate;
        private DateTime _endingDate;
        private string _customerPriceGroup;
        private string _unitOfMeasureCode;
        private decimal _quantityFrom;
        private decimal _quantityTo;
        private decimal _lineAmount;

        public ItemPriceExtended() { }

        public ItemPriceExtended(Navipro.SantaMonica.Common.ItemPriceExtended itemPriceExtended)
        {
            _entryNo = itemPriceExtended.entryNo;
            _itemNo = itemPriceExtended.itemNo;
            _startingDate = itemPriceExtended.startingDate;
            _endingDate = itemPriceExtended.endingDate;
            _customerPriceGroup = itemPriceExtended.customerPriceGroup;
            _unitOfMeasureCode = itemPriceExtended.unitOfMeasureCode;
            _quantityFrom = itemPriceExtended.quantityFrom;
            _quantityTo = itemPriceExtended.quantityTo;
            _lineAmount = itemPriceExtended.lineAmount;
        }

        public ItemPriceExtended(System.Data.DataRow dataRow)
        {
            _entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            _itemNo = dataRow.ItemArray.GetValue(1).ToString();
            _startingDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
            _endingDate = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
            _customerPriceGroup = dataRow.ItemArray.GetValue(4).ToString();
            _unitOfMeasureCode = dataRow.ItemArray.GetValue(5).ToString();
            _quantityFrom = Decimal.Parse(dataRow.ItemArray.GetValue(6).ToString());
            _quantityTo = Decimal.Parse(dataRow.ItemArray.GetValue(7).ToString());
            _lineAmount = Decimal.Parse(dataRow.ItemArray.GetValue(8).ToString());
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public DateTime startingDate { get { return _startingDate; } set { _startingDate = value; } }
        public DateTime endingDate { get { return _endingDate; } set { _endingDate = value; } }
        public string customerPriceGroup { get { return _customerPriceGroup; } set { _customerPriceGroup = value; } }
        public string unitOfMeasureCode { get { return _unitOfMeasureCode; } set { _unitOfMeasureCode = value; } }
        public decimal quantityFrom { get { return _quantityFrom; } set { _quantityFrom = value; } }
        public decimal quantityTo { get { return _quantityTo; } set { _quantityTo = value; } }
        public decimal lineAmount { get { return _lineAmount; } set { _lineAmount = value; } }
            
    }
}
