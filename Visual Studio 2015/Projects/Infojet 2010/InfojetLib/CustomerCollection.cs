using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class CustomerCollection : CollectionBase
    {
        public Customer this[int index]
        {
            get { return (Customer)List[index]; }
            set { List[index] = value; }
        }
        public int Add(Customer customer)
        {
            return (List.Add(customer));
        }
        public int IndexOf(Customer customer)
        {
            return (List.IndexOf(customer));
        }
        public void Insert(int index, Customer customer)
        {
            List.Insert(index, customer);
        }
        public void Remove(Customer customer)
        {
            List.Remove(customer);
        }
        public bool Contains(Customer customer)
        {
            return (List.Contains(customer));
        }

 
    }
}
