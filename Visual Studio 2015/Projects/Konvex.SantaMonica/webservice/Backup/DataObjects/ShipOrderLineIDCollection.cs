using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class ShipOrderLineIDCollection : CollectionBase
    {
        public ShipOrderLineIDCollection()
        {
        }

        public ShipOrderLineID this[int index]
        {
            get { return (ShipOrderLineID)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ShipOrderLineID orderLineId)
        {
            return (List.Add(orderLineId));
        }
        public int IndexOf(ShipOrderLineID orderLineId)
        {
            return (List.IndexOf(orderLineId));
        }
        public void Insert(int index, ShipOrderLineID orderLineId)
        {
            List.Insert(index, orderLineId);
        }
        public void Remove(ShipOrderLineID orderLineId)
        {
            List.Remove(orderLineId);
        }
        public bool Contains(ShipOrderLineID orderLineId)
        {
            return (List.Contains(orderLineId));
        }


    }
}
