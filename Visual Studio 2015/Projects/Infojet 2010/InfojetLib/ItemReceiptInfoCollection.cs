using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class ItemReceiptInfoCollection : CollectionBase
    {
        public ItemReceiptInfo this[int index]
        {
            get { return (ItemReceiptInfo)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ItemReceiptInfo itemReceiptInfo)
        {
            return (List.Add(itemReceiptInfo));
        }
        public int IndexOf(ItemReceiptInfo itemReceiptInfo)
        {
            return (List.IndexOf(itemReceiptInfo));
        }
        public void Insert(int index, ItemReceiptInfo itemReceiptInfo)
        {
            List.Insert(index, itemReceiptInfo);
        }
        public void Remove(ItemReceiptInfo itemReceiptInfo)
        {
            List.Remove(itemReceiptInfo);
        }
        public bool Contains(ItemReceiptInfo itemReceiptInfo)
        {
            return (List.Contains(itemReceiptInfo));
        }


    }
}
