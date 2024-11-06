using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class ItemPriceExtendedCollection : CollectionBase
    {
        public ItemPriceExtended this[int index]
        {
            get { return (ItemPriceExtended)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ItemPriceExtended itemPriceExtended)
        {
            return (List.Add(itemPriceExtended));
        }
        public int IndexOf(ItemPriceExtended itemPriceExtended)
        {
            return (List.IndexOf(itemPriceExtended));
        }
        public void Insert(int index, ItemPriceExtended itemPriceExtended)
        {
            List.Insert(index, itemPriceExtended);
        }
        public void Remove(ItemPriceExtended itemPriceExtended)
        {
            List.Remove(itemPriceExtended);
        }
        public bool Contains(ItemPriceExtended itemPriceExtended)
        {
            return (List.Contains(itemPriceExtended));
        }


    }
}
