using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.Library
{

    public class DataCollection : CollectionBase
    {
        public DataEntry this[int index]
        {
            get { return (DataEntry)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataEntry dataEntry)
        {
            return (List.Add(dataEntry));
        }
        public int IndexOf(DataEntry dataEntry)
        {
            return (List.IndexOf(dataEntry));
        }
        public void Insert(int index, DataEntry dataEntry)
        {
            List.Insert(index, dataEntry);
        }
        public void Remove(DataEntry dataEntry)
        {
            List.Remove(dataEntry);
        }
        public bool Contains(DataEntry dataEntry)
        {
            return (List.Contains(dataEntry));
        }

    }
}
