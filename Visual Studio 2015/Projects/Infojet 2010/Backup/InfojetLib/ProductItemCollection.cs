using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class ProductItemCollection : CollectionBase
    {
        public ProductItem this[int index]
        {
            get { return (ProductItem)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ProductItem productItem)
        {
            return (List.Add(productItem));
        }
        public int IndexOf(ProductItem productItem)
        {
            return (List.IndexOf(productItem));
        }
        public void Insert(int index, ProductItem productItem)
        {
            List.Insert(index, productItem);
        }
        public void Remove(ProductItem productItem)
        {
            List.Remove(productItem);
        }
        public bool Contains(ProductItem productItem)
        {
            return (List.Contains(productItem));
        }
        public void setSize(int width, int height)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].productImage != null) this[i].productImage.setSize(width, height);
                i++;
            }
        }
    }
}
