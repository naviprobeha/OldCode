using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Apcoa.ContractParker.Library.Data
{
    public class SalesInvoiceHeaderCollection : CollectionBase
    {
        public SalesInvoiceHeader this[int index]
        {
            get { return (SalesInvoiceHeader)List[index]; }
            set { List[index] = value; }
        }
        public int Add(SalesInvoiceHeader salesInvoiceHeader)
        {
            return (List.Add(salesInvoiceHeader));
        }
        public int IndexOf(SalesInvoiceHeader salesInvoiceHeader)
        {
            return (List.IndexOf(salesInvoiceHeader));
        }
        public void Insert(int index, SalesInvoiceHeader salesInvoiceHeader)
        {
            List.Insert(index, salesInvoiceHeader);
        }
        public void Remove(SalesInvoiceHeader salesInvoiceHeader)
        {
            List.Remove(salesInvoiceHeader);
        }
        public bool Contains(SalesInvoiceHeader salesInvoiceHeader)
        {
            return (List.Contains(salesInvoiceHeader));
        }

        public void setPaging(int page, int itemsPerPage)
        {
            if (page == 0) return;

            if (page > 1)
            {
                for (int i = 0; i < (itemsPerPage * (page - 1)); i++)
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
