using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class ItemPrice
    {
        private int _entryNo;
        private string _itemNo;
        private int _salesType;
        private string _salesCode;
        private DateTime _startingDate;
        private decimal _minimumQuantity;
        private DateTime _endingDate;
        private decimal _unitPrice;

        public ItemPrice() { }

        public ItemPrice(Navipro.SantaMonica.Common.ItemPrice itemPrice)
        {
            _entryNo = itemPrice.entryNo;
            _itemNo = itemPrice.itemNo;
            _salesType = itemPrice.salesType;
            _salesCode = itemPrice.salesCode;
            _startingDate = itemPrice.startingDate;
            _minimumQuantity = itemPrice.minimumQuantity;
            _endingDate = itemPrice.endingDate;
            _unitPrice = itemPrice.unitPrice;
        }

        public ItemPrice(System.Data.DataRow dataRow)
        {
            _entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
            _itemNo = dataRow.ItemArray.GetValue(1).ToString();
            _salesType = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
            _salesCode = dataRow.ItemArray.GetValue(3).ToString();
            _startingDate = DateTime.Parse(dataRow.ItemArray.GetValue(4).ToString());
            _minimumQuantity = Decimal.Parse(dataRow.ItemArray.GetValue(5).ToString());
            _endingDate = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());
            _unitPrice = Decimal.Parse(dataRow.ItemArray.GetValue(7).ToString());
        }

        public int entryNo { get { return _entryNo; } set { _entryNo = value; } }
        public string itemNo { get { return _itemNo; } set { _itemNo = value; } }
        public int salesType { get { return _salesType; } set { _salesType = value; } }
        public string salesCode { get { return _salesCode; } set { _salesCode = value; } }
        public DateTime startingDate { get { return _startingDate; } set { _startingDate = value; } }
        public decimal minimumQuantity { get { return _minimumQuantity; } set { _minimumQuantity = value; } }
        public DateTime endingDate { get { return _endingDate; } set { _endingDate = value; } }
        public decimal unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }

        public static ItemPrice fromJsonObject(JObject jsonObject)
        {
            ItemPrice entry = new ItemPrice();
            entry.entryNo = (int)jsonObject["entryNo"];
            entry._itemNo = (string)jsonObject["itemNo"];
            if ((jsonObject["customerPriceGroup"] != null) && ((string)jsonObject["customerPriceGroup"] != ""))
            {
                entry.salesType = 1;
                entry.salesCode = (string)jsonObject["customerPriceGroup"];
            }
            else
            {
                entry.salesType = 2;
                entry.salesCode = "";
            }

            entry.startingDate = DateTime.Parse("2001-01-01");
            entry.endingDate = DateTime.Parse("2099-01-01");

            if (jsonObject["startingDate"].HasValues)
            {
                entry.startingDate = DateTime.Parse(((string)jsonObject["startingDate"]).Substring(0, 10));
            }
            if (jsonObject["endingDate"].HasValues)
            {
                if (jsonObject["endingDate"] != null)
                {
                    entry.endingDate = DateTime.Parse(((string)jsonObject["endingDate"]).Substring(0, 10));
                }
            }
            entry.minimumQuantity = (decimal)jsonObject["quantityFrom"];
            entry.unitPrice = (Decimal)jsonObject["price"];


            return entry;
        }
    }
}
