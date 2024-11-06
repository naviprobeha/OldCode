using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class CustomerHistoryLineCollection : CollectionBase
    {
        public CustomerHistoryLine this[int index]
        {
            get { return (CustomerHistoryLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CustomerHistoryLine customerHistoryLine)
        {
            return (List.Add(customerHistoryLine));
        }
        public int IndexOf(CustomerHistoryLine customerHistoryLine)
        {
            return (List.IndexOf(customerHistoryLine));
        }
        public void Insert(int index, CustomerHistoryLine customerHistoryLine)
        {
            List.Insert(index, customerHistoryLine);
        }
        public void Remove(CustomerHistoryLine customerHistoryLine)
        {
            List.Remove(customerHistoryLine);
        }
        public bool Contains(CustomerHistoryLine customerHistoryLine)
        {
            return (List.Contains(customerHistoryLine ));
        }


    }
}
