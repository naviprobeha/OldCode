using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.Library
{

    public class DataHourCollection : CollectionBase
    {
        public DataHourEntry this[int index]
        {
            get { return (DataHourEntry)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataHourEntry dataHourEntry)
        {
            return (List.Add(dataHourEntry));
        }
        public int IndexOf(DataHourEntry dataHourEntry)
        {
            return (List.IndexOf(dataHourEntry));
        }
        public void Insert(int index, DataHourEntry dataHourEntry)
        {
            List.Insert(index, dataHourEntry);
        }
        public void Remove(DataHourEntry dataHourEntry)
        {
            List.Remove(dataHourEntry);
        }
        public bool Contains(DataHourEntry dataHourEntry)
        {
            return (List.Contains(dataHourEntry));
        }

    }
}
