using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class CustomerHistoryCrMemoCollection : CollectionBase
    {
        public CustomerHistoryCrMemo this[int index]
        {
            get { return (CustomerHistoryCrMemo)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CustomerHistoryCrMemo customerCrMemoInvoice)
        {
            return (List.Add(customerCrMemoInvoice));
        }
        public int IndexOf(CustomerHistoryCrMemo customerCrMemoInvoice)
        {
            return (List.IndexOf(customerCrMemoInvoice));
        }
        public void Insert(int index, CustomerHistoryCrMemo customerCrMemoInvoice)
        {
            List.Insert(index, customerCrMemoInvoice);
        }
        public void Remove(CustomerHistoryCrMemo customerCrMemoInvoice)
        {
            List.Remove(customerCrMemoInvoice);
        }
        public bool Contains(CustomerHistoryCrMemo customerCrMemoInvoice)
        {
            return (List.Contains(customerCrMemoInvoice));
        }


    }
}
