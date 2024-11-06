using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Navipro.Sandberg.Common
{
    public class ItemJournalLine
    {

        public DateTime documentDate;
        public string itemNo;
        public string description;
        public float qtyOnOrder;
        public float inventory;
        public float disposable;
        public float saleQty;
        public bool expiringItem;
        public bool doubleBatch;
        public float qtyOnSampleTestBookOrder;

        public ItemJournalLine(DataRow dataRow)
        {
            this.documentDate = DateTime.Parse(dataRow.ItemArray.GetValue(0).ToString());
            this.itemNo = dataRow.ItemArray.GetValue(1).ToString();
            this.description = dataRow.ItemArray.GetValue(2).ToString();
            this.qtyOnOrder = float.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.inventory = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
            this.disposable = float.Parse(dataRow.ItemArray.GetValue(5).ToString());
            this.saleQty = float.Parse(dataRow.ItemArray.GetValue(6).ToString());
            
            this.expiringItem = false;
            if (dataRow.ItemArray.GetValue(7).ToString() == "1") this.expiringItem = true;

            this.doubleBatch = false;
            if (dataRow.ItemArray.GetValue(8).ToString() == "1") this.doubleBatch = true;

            this.qtyOnSampleTestBookOrder = float.Parse(dataRow.ItemArray.GetValue(9).ToString());

        }

        public string getExpiringItem()
        {
            if (this.expiringItem) return "Ja";
            return "";
        }

        public string getDoubleBatch()
        {
            if (this.doubleBatch) return "Ja";
            return "";
        }

    }
}
