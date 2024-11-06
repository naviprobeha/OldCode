using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Cashjet.Library
{

    public class CashSiteCollection : CollectionBase
    {
        public CashSite this[int index]
        {
            get { return (CashSite)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CashSite cashSite)
        {
            return (List.Add(cashSite));
        }
        public int IndexOf(CashSite cashSite)
        {
            return (List.IndexOf(cashSite));
        }
        public void Insert(int index, CashSite cashSite)
        {
            List.Insert(index, cashSite);
        }
        public void Remove(CashSite cashSite)
        {
            List.Remove(cashSite);
        }
        public bool Contains(CashSite cashSite)
        {
            return (List.Contains(cashSite));
        }

    }
}
