using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class ShipOrderLineCollection : CollectionBase
    {
        public ShipOrderLineCollection()
        {
        }

        public ShipOrderLine this[int index]
        {
            get { return (ShipOrderLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ShipOrderLine orderLine)
        {
            return (List.Add(orderLine));
        }
        public int IndexOf(ShipOrderLine orderLine)
        {
            return (List.IndexOf(orderLine));
        }
        public void Insert(int index, ShipOrderLine orderLine)
        {
            List.Insert(index, orderLine);
        }
        public void Remove(ShipOrderLine orderLine)
        {
            List.Remove(orderLine);
        }
        public bool Contains(ShipOrderLine orderLine)
        {
            return (List.Contains(orderLine));
        }


    }
}
