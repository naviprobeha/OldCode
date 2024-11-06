using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class ItemPriceCollection : CollectionBase
    {
        public ItemPrice this[int index]
        {
            get { return (ItemPrice)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ItemPrice itemPrice)
        {
            return (List.Add(itemPrice));
        }
        public int IndexOf(ItemPrice itemPrice)
        {
            return (List.IndexOf(itemPrice));
        }
        public void Insert(int index, ItemPrice itemPrice)
        {
            List.Insert(index, itemPrice);
        }
        public void Remove(ItemPrice itemPrice)
        {
            List.Remove(itemPrice);
        }
        public bool Contains(ItemPrice itemPrice)
        {
            return (List.Contains(itemPrice));
        }


    }
}
