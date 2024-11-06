using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Apcoa.ContractParker.Library.Data
{
    public class SalesInvoiceLineCollection : CollectionBase
    {
        public SalesInvoiceLine this[int index]
        {
            get { return (SalesInvoiceLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(SalesInvoiceLine salesInvoiceLine)
        {
            return (List.Add(salesInvoiceLine));
        }
        public int IndexOf(SalesInvoiceLine salesInvoiceLine)
        {
            return (List.IndexOf(salesInvoiceLine));
        }
        public void Insert(int index, SalesInvoiceLine salesInvoiceLine)
        {
            List.Insert(index, salesInvoiceLine);
        }
        public void Remove(SalesInvoiceLine salesInvoiceLine)
        {
            List.Remove(salesInvoiceLine);
        }
        public bool Contains(SalesInvoiceLine salesInvoiceLine)
        {
            return (List.Contains(salesInvoiceLine));
        }


    }
}
