using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class ItemAttributeCollection : CollectionBase
    {
        public ItemAttribute this[int index]
        {
            get { return (ItemAttribute)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ItemAttribute itemAttribute)
        {
            return (List.Add(itemAttribute));
        }
        public int IndexOf(ItemAttribute itemAttribute)
        {
            return (List.IndexOf(itemAttribute));
        }
        public void Insert(int index, ItemAttribute itemAttribute)
        {
            List.Insert(index, itemAttribute);
        }
        public void Remove(ItemAttribute itemAttribute)
        {
            List.Remove(itemAttribute);
        }
        public bool Contains(ItemAttribute itemAttribute)
        {
            return (List.Contains(itemAttribute));
        }

 
    }
}
