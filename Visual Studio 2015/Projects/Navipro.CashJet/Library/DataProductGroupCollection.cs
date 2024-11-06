using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.Library
{

    public class DataProductGroupCollection : CollectionBase
    {
        public DataProductGroupEntry this[int index]
        {
            get { return (DataProductGroupEntry)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataProductGroupEntry dataProductEntry)
        {
            return (List.Add(dataProductEntry));
        }
        public int IndexOf(DataProductGroupEntry dataProductEntry)
        {
            return (List.IndexOf(dataProductEntry));
        }
        public void Insert(int index, DataProductGroupEntry dataProductEntry)
        {
            List.Insert(index, dataProductEntry);
        }
        public void Remove(DataProductGroupEntry dataProductEntry)
        {
            List.Remove(dataProductEntry);
        }
        public bool Contains(DataProductGroupEntry dataProductEntry)
        {
            return (List.Contains(dataProductEntry));
        }

    }
}
