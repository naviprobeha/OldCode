using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class CustomerHistoryShipmentCollection : CollectionBase
    {
        public CustomerHistoryShipment this[int index]
        {
            get { return (CustomerHistoryShipment)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CustomerHistoryShipment customerHistoryShipment)
        {
            return (List.Add(customerHistoryShipment));
        }
        public int IndexOf(CustomerHistoryShipment customerHistoryShipment)
        {
            return (List.IndexOf(customerHistoryShipment));
        }
        public void Insert(int index, CustomerHistoryShipment customerHistoryShipment)
        {
            List.Insert(index, customerHistoryShipment);
        }
        public void Remove(CustomerHistoryShipment customerHistoryShipment)
        {
            List.Remove(customerHistoryShipment);
        }
        public bool Contains(CustomerHistoryShipment customerHistoryShipment)
        {
            return (List.Contains(customerHistoryShipment));
        }


    }
}
