using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.KlarnaAPI.Wrapper
{

    public class InvoiceLineCollection : CollectionBase
    {
        public InvoiceLine this[int index]
        {
            get { return (InvoiceLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(InvoiceLine invoiceLine)
        {
            return (List.Add(invoiceLine));
        }
        public int IndexOf(InvoiceLine invoiceLine)
        {
            return (List.IndexOf(invoiceLine));
        }
        public void Insert(int index, InvoiceLine invoiceLine)
        {
            List.Insert(index, invoiceLine);
        }
        public void Remove(InvoiceLine invoiceLine)
        {
            List.Remove(invoiceLine);
        }
        public bool Contains(InvoiceLine invoiceLine)
        {
            return (List.Contains(invoiceLine));
        }


    }
}
