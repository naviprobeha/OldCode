using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.CashJet.AddIns
{

    public class RowItemFieldCollection : CollectionBase
    {
        public RowItemField this[int index]
        {
            get { return (RowItemField)List[index]; }
            set { List[index] = value; }
        }
        public int Add(RowItemField rowItemField)
        {
            return (List.Add(rowItemField));
        }
        public int IndexOf(RowItemField rowItemField)
        {
            return (List.IndexOf(rowItemField));
        }
        public void Insert(int index, RowItemField rowItemField)
        {
            List.Insert(index, rowItemField);
        }
        public void Remove(RowItemField rowItemField)
        {
            List.Remove(rowItemField);
        }
        public bool Contains(RowItemField rowItemField)
        {
            return (List.Contains(rowItemField));
        }


    }
}
