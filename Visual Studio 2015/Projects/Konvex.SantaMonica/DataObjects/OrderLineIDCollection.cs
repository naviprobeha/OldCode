using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class OrderLineIDCollection : CollectionBase
    {
        public OrderLineIDCollection()
        {
        }

        public OrderLineID this[int index]
        {
            get { return (OrderLineID)List[index]; }
            set { List[index] = value; }
        }
        public int Add(OrderLineID orderLineId)
        {
            return (List.Add(orderLineId));
        }
        public int IndexOf(OrderLineID orderLineId)
        {
            return (List.IndexOf(orderLineId));
        }
        public void Insert(int index, OrderLineID orderLineId)
        {
            List.Insert(index, orderLineId);
        }
        public void Remove(OrderLineID orderLineId)
        {
            List.Remove(orderLineId);
        }
        public bool Contains(OrderLineID orderLineId)
        {
            return (List.Contains(orderLineId));
        }


    }
}
