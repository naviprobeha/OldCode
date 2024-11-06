using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class ItemInfoPriceCollection : CollectionBase
    {
        public ItemInfoPrice this[int index]
        {
            get { return (ItemInfoPrice)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ItemInfoPrice itemInfoPrice)
        {
            return (List.Add(itemInfoPrice));
        }
        public int IndexOf(ItemInfoPrice itemInfoPrice)
        {
            return (List.IndexOf(itemInfoPrice));
        }
        public void Insert(int index, ItemInfoPrice itemInfoPrice)
        {
            List.Insert(index, itemInfoPrice);
        }
        public void Remove(ItemInfoPrice itemInfoPrice)
        {
            List.Remove(itemInfoPrice);
        }
        public bool Contains(ItemInfoPrice itemInfoPrice)
        {
            return (List.Contains(itemInfoPrice));
        }

        public int getQuantityPriceIndex(float minQuantity)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].minQuantity == minQuantity) return i;
                i++;
            }

            return -1;
        }

        public ItemInfoPrice getNearestQuantityPrice(float quantity)
        {
            ItemInfoPrice itemInfoPrice = null;

            int i = 0;
            while (i < Count)
            {

                if (this[i].minQuantity <= quantity)
                {
                    if (itemInfoPrice == null) itemInfoPrice = this[i];
                    if (itemInfoPrice.unitPrice > this[i].unitPrice) itemInfoPrice = this[i];
                }
                i++;
            }

            return itemInfoPrice;
        }
    }
}
