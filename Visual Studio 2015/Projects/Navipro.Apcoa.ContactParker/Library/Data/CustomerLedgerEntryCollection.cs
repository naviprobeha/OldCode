using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Apcoa.ContractParker.Library.Data
{
    public class CustomerLedgerEntryCollection : CollectionBase
    {
        public CustomerLedgerEntry this[int index]
        {
            get { return (CustomerLedgerEntry)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CustomerLedgerEntry customerLedgerEntry)
        {
            return (List.Add(customerLedgerEntry));
        }
        public int IndexOf(CustomerLedgerEntry customerLedgerEntry)
        {
            return (List.IndexOf(customerLedgerEntry));
        }
        public void Insert(int index, CustomerLedgerEntry customerLedgerEntry)
        {
            List.Insert(index, customerLedgerEntry);
        }
        public void Remove(CustomerLedgerEntry customerLedgerEntry)
        {
            List.Remove(customerLedgerEntry);
        }
        public bool Contains(CustomerLedgerEntry customerLedgerEntry)
        {
            return (List.Contains(customerLedgerEntry));
        }

        public void setPaging(int page, int itemsPerPage)
        {
            if (page == 0) return;

            if (page > 1)
            {
                for (int i=0; i < (itemsPerPage*(page-1)); i++)
                {
                    List.RemoveAt(0);
                }

            }

            if (Count > itemsPerPage)
            {
                int j = Count;

                for (int i = 0; i < (j - itemsPerPage); i++)
                {
                    List.RemoveAt(itemsPerPage);
                }
            }
        }
    }
}
