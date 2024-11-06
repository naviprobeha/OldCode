using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class CustomerHistoryOrderCollection : CollectionBase
    {
        public CustomerHistoryOrder this[int index]
        {
            get { return (CustomerHistoryOrder)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CustomerHistoryOrder customerHistoryOrder)
        {
            return (List.Add(customerHistoryOrder));
        }
        public int IndexOf(CustomerHistoryOrder customerHistoryOrder)
        {
            return (List.IndexOf(customerHistoryOrder));
        }
        public void Insert(int index, CustomerHistoryOrder customerHistoryOrder)
        {
            List.Insert(index, customerHistoryOrder);
        }
        public void Remove(CustomerHistoryOrder customerHistoryOrder)
        {
            List.Remove(customerHistoryOrder);
        }
        public bool Contains(CustomerHistoryOrder customerHistoryOrder)
        {
            return (List.Contains(customerHistoryOrder));
        }


    }
}
