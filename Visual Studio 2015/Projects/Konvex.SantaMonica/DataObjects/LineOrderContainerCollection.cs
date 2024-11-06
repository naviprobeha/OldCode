using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Konvex.SmartShipping.DataObjects
{

    public class LineOrderContainerCollection : CollectionBase
    {
        public LineOrderContainer this[int index]
        {
            get { return (LineOrderContainer)List[index]; }
            set { List[index] = value; }
        }
        public int Add(LineOrderContainer lineOrderContainer)
        {
            return (List.Add(lineOrderContainer));
        }
        public int IndexOf(LineOrderContainer lineOrderContainer)
        {
            return (List.IndexOf(lineOrderContainer));
        }
        public void Insert(int index, LineOrderContainer lineOrderContainer)
        {
            List.Insert(index, lineOrderContainer);
        }
        public void Remove(LineOrderContainer lineOrderContainer)
        {
            List.Remove(lineOrderContainer);
        }
        public bool Contains(LineOrderContainer lineOrderContainer)
        {
            return (List.Contains(lineOrderContainer));
        }


    }
}
