using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.SmartInventory
{

    public class DataWagonCollection : CollectionBase
    {
        public DataWagon this[int index]
        {
            get { return (DataWagon)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataWagon dataWagon)
        {
            return (List.Add(dataWagon));
        }
        public int IndexOf(DataWagon dataWagon)
        {
            return (List.IndexOf(dataWagon));
        }
        public void Insert(int index, DataWagon dataWagon)
        {
            List.Insert(index, dataWagon);
        }
        public void Remove(DataWagon dataWagon)
        {
            List.Remove(dataWagon);
        }
        public bool Contains(DataWagon dataWagon)
        {
            return (List.Contains(dataWagon));
        }


    }
}
