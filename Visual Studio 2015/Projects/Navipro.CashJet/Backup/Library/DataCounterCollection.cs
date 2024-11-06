using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.Library
{

    public class DataCounterCollection : CollectionBase
    {
        public DataCounter this[int index]
        {
            get { return (DataCounter)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataCounter dataCounter)
        {
            return (List.Add(dataCounter));
        }
        public int IndexOf(DataCounter dataCounter)
        {
            return (List.IndexOf(dataCounter));
        }
        public void Insert(int index, DataCounter dataCounter)
        {
            List.Insert(index, dataCounter);
        }
        public void Remove(DataCounter dataCounter)
        {
            List.Remove(dataCounter);
        }
        public bool Contains(DataCounter dataCounter)
        {
            return (List.Contains(dataCounter));
        }

    }
}
