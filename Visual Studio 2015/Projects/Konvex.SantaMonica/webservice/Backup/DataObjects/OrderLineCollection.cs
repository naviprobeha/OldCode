using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class OrderLineCollection : CollectionBase
    {
        public OrderLineCollection()
        {
        }

        public OrderLine this[int index]
        {
            get { return (OrderLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(OrderLine orderLine)
        {
            return (List.Add(orderLine));
        }
        public int IndexOf(OrderLine orderLine)
        {
            return (List.IndexOf(orderLine));
        }
        public void Insert(int index, OrderLine orderLine)
        {
            List.Insert(index, orderLine);
        }
        public void Remove(OrderLine orderLine)
        {
            List.Remove(orderLine);
        }
        public bool Contains(OrderLine orderLine)
        {
            return (List.Contains(orderLine));
        }


    }
}
