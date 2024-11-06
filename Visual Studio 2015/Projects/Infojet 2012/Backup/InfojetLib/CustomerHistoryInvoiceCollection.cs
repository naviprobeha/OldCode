using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class CustomerHistoryInvoiceCollection : CollectionBase
    {
        public CustomerHistoryInvoice this[int index]
        {
            get { return (CustomerHistoryInvoice)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CustomerHistoryInvoice customerHistoryInvoice)
        {
            return (List.Add(customerHistoryInvoice));
        }
        public int IndexOf(CustomerHistoryInvoice customerHistoryInvoice)
        {
            return (List.IndexOf(customerHistoryInvoice));
        }
        public void Insert(int index, CustomerHistoryInvoice customerHistoryInvoice)
        {
            List.Insert(index, customerHistoryInvoice);
        }
        public void Remove(CustomerHistoryInvoice customerHistoryInvoice)
        {
            List.Remove(customerHistoryInvoice);
        }
        public bool Contains(CustomerHistoryInvoice customerHistoryInvoice)
        {
            return (List.Contains(customerHistoryInvoice));
        }


    }
}
