using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class FieldValueCollection : CollectionBase
    {
        public FieldValue this[int index]
        {
            get { return (FieldValue)List[index]; }
            set { List[index] = value; }
        }
        public int Add(FieldValue fieldValue)
        {
            return (List.Add(fieldValue));
        }
        public int IndexOf(FieldValue fieldValue)
        {
            return (List.IndexOf(fieldValue));
        }
        public void Insert(int index, FieldValue fieldValue)
        {
            List.Insert(index, fieldValue);
        }
        public void Remove(FieldValue fieldValue)
        {
            List.Remove(fieldValue);
        }
        public bool Contains(FieldValue fieldValue)
        {
            return (List.Contains(fieldValue));
        }

    }
}
