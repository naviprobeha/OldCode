using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{
    public class ShipOrderDistance
    {

        private string _no;
        private int _distance;
        private DateTime _postingDate;

        public ShipOrderDistance() { }

        public ShipOrderDistance(System.Data.DataRow dataRow)
        {
            _no = dataRow.ItemArray.GetValue(0).ToString();
            _distance = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            _postingDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());

        }

        public string no { get { return _no; } set { _no = value; } }
        public int distance { get { return _distance; } set { _distance = value; } }
        public DateTime postingDate { get { return _postingDate; } set { _postingDate = value; } }
    }
}
