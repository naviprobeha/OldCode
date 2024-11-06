using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class CustomerHistoryLedgerCollection : CollectionBase
    {
        public CustomerHistoryLedger this[int index]
        {
            get { return (CustomerHistoryLedger)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CustomerHistoryLedger customerHistoryLedger)
        {
            return (List.Add(customerHistoryLedger));
        }
        public int IndexOf(CustomerHistoryLedger customerHistoryLedger)
        {
            return (List.IndexOf(customerHistoryLedger));
        }
        public void Insert(int index, CustomerHistoryLedger customerHistoryLedger)
        {
            List.Insert(index, customerHistoryLedger);
        }
        public void Remove(CustomerHistoryLedger customerHistoryLedger)
        {
            List.Remove(customerHistoryLedger);
        }
        public bool Contains(CustomerHistoryLedger customerHistoryLedger)
        {
            return (List.Contains(customerHistoryLedger));
        }


    }
}
