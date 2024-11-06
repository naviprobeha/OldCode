using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.Library
{

    public class DataGenProdPostingGroupCollection : CollectionBase
    {
        public DataGenProdPostingGroupEntry this[int index]
        {
            get { return (DataGenProdPostingGroupEntry)List[index]; }
            set { List[index] = value; }
        }
        public int Add(DataGenProdPostingGroupEntry dataProductEntry)
        {
            return (List.Add(dataProductEntry));
        }
        public int IndexOf(DataGenProdPostingGroupEntry dataProductEntry)
        {
            return (List.IndexOf(dataProductEntry));
        }
        public void Insert(int index, DataGenProdPostingGroupEntry dataProductEntry)
        {
            List.Insert(index, dataProductEntry);
        }
        public void Remove(DataGenProdPostingGroupEntry dataProductEntry)
        {
            List.Remove(dataProductEntry);
        }
        public bool Contains(DataGenProdPostingGroupEntry dataProductEntry)
        {
            return (List.Contains(dataProductEntry));
        }

    }
}
