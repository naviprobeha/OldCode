using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Navipro.Sandberg.Common
{
    public class ProductionEntry
    {

        public DateTime date;
        public string itemNo;
        public string paperItemNo;
        public float kgOfPaper;
        public float mOfPrima;
        public float primaPercent;
        public string color1ItemNo;
        public float color1Kg;
        public string color2ItemNo;
        public float color2Kg;
        public string color3ItemNo;
        public float color3Kg;
        public string color4ItemNo;
        public float color4Kg;
        public string color5ItemNo;
        public float color5Kg;
        public string color6ItemNo;
        public float color6Kg;
        public string color7ItemNo;
        public float color7Kg;
        public string color8ItemNo;
        public float color8Kg;
        public string comment;
        public string variantCode;

        public ProductionEntry(DataRow dataRow)
        {
            this.date = DateTime.Parse(dataRow.ItemArray.GetValue(0).ToString());
            this.itemNo = dataRow.ItemArray.GetValue(1).ToString();
            this.paperItemNo = dataRow.ItemArray.GetValue(2).ToString();
            this.kgOfPaper = float.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.mOfPrima = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
            this.primaPercent = float.Parse(dataRow.ItemArray.GetValue(5).ToString());
            this.color1ItemNo = dataRow.ItemArray.GetValue(6).ToString();
            this.color1Kg = float.Parse(dataRow.ItemArray.GetValue(7).ToString());
            this.color2ItemNo = dataRow.ItemArray.GetValue(8).ToString();
            this.color2Kg = float.Parse(dataRow.ItemArray.GetValue(9).ToString());
            this.color3ItemNo = dataRow.ItemArray.GetValue(10).ToString();
            this.color3Kg = float.Parse(dataRow.ItemArray.GetValue(11).ToString());
            this.color4ItemNo = dataRow.ItemArray.GetValue(12).ToString();
            this.color4Kg = float.Parse(dataRow.ItemArray.GetValue(13).ToString());
            this.color5ItemNo = dataRow.ItemArray.GetValue(14).ToString();
            this.color5Kg = float.Parse(dataRow.ItemArray.GetValue(15).ToString());
            this.color6ItemNo = dataRow.ItemArray.GetValue(16).ToString();
            this.color6Kg = float.Parse(dataRow.ItemArray.GetValue(17).ToString());
            this.color7ItemNo = dataRow.ItemArray.GetValue(18).ToString();
            this.color7Kg = float.Parse(dataRow.ItemArray.GetValue(19).ToString());
            this.color8ItemNo = dataRow.ItemArray.GetValue(20).ToString();
            this.color8Kg = float.Parse(dataRow.ItemArray.GetValue(21).ToString());
            this.comment = dataRow.ItemArray.GetValue(22).ToString();
            this.variantCode = dataRow.ItemArray.GetValue(23).ToString();
        }
    }
}
