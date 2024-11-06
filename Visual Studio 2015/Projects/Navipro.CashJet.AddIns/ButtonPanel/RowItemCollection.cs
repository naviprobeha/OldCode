using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.CashJet.AddIns
{

    public class RowItemCollection : CollectionBase
    {
        public RowItem this[int index]
        {
            get { return (RowItem)List[index]; }
            set { List[index] = value; }
        }
        public int Add(RowItem rowItem)
        {
            return (List.Add(rowItem));
        }
        public int IndexOf(RowItem rowItem)
        {
            return (List.IndexOf(rowItem));
        }
        public void Insert(int index, RowItem rowItem)
        {
            List.Insert(index, rowItem);
        }
        public void Remove(RowItem rowItem)
        {
            List.Remove(rowItem);
        }
        public bool Contains(RowItem rowItem)
        {
            return (List.Contains(rowItem));
        }



    }
}
