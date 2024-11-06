using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.BjornBorg.Web
{
    public class ItemInventoryCollection : CollectionBase
    {
        public ItemInventory this[int index]
        {
            get { return (ItemInventory)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ItemInventory itemInventory)
        {
            return (List.Add(itemInventory));
        }
        public int IndexOf(ItemInventory itemInventory)
        {
            return (List.IndexOf(itemInventory));
        }
        public void Insert(int index, ItemInventory itemInventory)
        {
            List.Insert(index, itemInventory);
        }
        public void Remove(ItemInventory itemInventory)
        {
            List.Remove(itemInventory);
        }
        public bool Contains(ItemInventory itemInventory)
        {
            return (List.Contains(itemInventory));
        }


    }
}
