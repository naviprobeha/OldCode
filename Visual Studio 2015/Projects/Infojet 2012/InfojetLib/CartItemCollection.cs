using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class CartItemCollection : CollectionBase
    {
        public CartItem this[int index]
        {
            get { return (CartItem)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CartItem cartItem)
        {
            return (List.Add(cartItem));
        }
        public int IndexOf(CartItem cartItem)
        {
            return (List.IndexOf(cartItem));
        }
        public void Insert(int index, CartItem cartItem)
        {
            List.Insert(index, cartItem);
        }
        public void Remove(CartItem cartItem)
        {
            List.Remove(cartItem);
        }
        public bool Contains(CartItem cartItem)
        {
            return (List.Contains(cartItem));
        }

        public void setTextLength(int length)
        {
            int i = 0;
            while (i < List.Count)
            {
                ((CartItem)List[i]).setTextLength(length);
                i++;
            }

        }
    }
}
