using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.Library
{

    public class DataProductCollection : CollectionBase
    {
        public DataProductEntry this[int index]
        {
            get { return (DataProductEntry)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataProductEntry dataProductEntry)
        {
            return (List.Add(dataProductEntry));
        }
        public int IndexOf(DataProductEntry dataProductEntry)
        {
            return (List.IndexOf(dataProductEntry));
        }
        public void Insert(int index, DataProductEntry dataProductEntry)
        {
            List.Insert(index, dataProductEntry);
        }
        public void Remove(DataProductEntry dataProductEntry)
        {
            List.Remove(dataProductEntry);
        }
        public bool Contains(DataProductEntry dataProductEntry)
        {
            return (List.Contains(dataProductEntry));
        }

    }
}
