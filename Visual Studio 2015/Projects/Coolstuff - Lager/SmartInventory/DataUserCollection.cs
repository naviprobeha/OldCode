using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.SmartInventory
{

    public class DataUserCollection : CollectionBase
    {
        public DataUser this[int index]
        {
            get { return (DataUser)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataUser dataUser)
        {
            return (List.Add(dataUser));
        }
        public int IndexOf(DataUser dataUser)
        {
            return (List.IndexOf(dataUser));
        }
        public void Insert(int index, DataUser dataUser)
        {
            List.Insert(index, dataUser);
        }
        public void Remove(DataUser dataUser)
        {
            List.Remove(dataUser);
        }
        public bool Contains(DataUser dataUser)
        {
            return (List.Contains(dataUser));
        }


    }
}
