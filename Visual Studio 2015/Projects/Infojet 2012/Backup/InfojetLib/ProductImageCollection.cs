using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class ProductImageCollection : CollectionBase
    {
        public ProductImage this[int index]
        {
            get { return (ProductImage)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ProductImage productImage)
        {
            return (List.Add(productImage));
        }
        public int IndexOf(ProductImage productImage)
        {
            return (List.IndexOf(productImage));
        }
        public void Insert(int index, ProductImage productImage)
        {
            List.Insert(index, productImage);
        }
        public void Remove(ProductImage productImage)
        {
            List.Remove(productImage);
        }
        public bool Contains(ProductImage productImage)
        {
            return (List.Contains(productImage));
        }

        public void setSize(int width, int height)
        {
            int i = 0;
            while (i < List.Count)
            {
                ((ProductImage)List[i]).setSize(width, height);
                i++;
            }

        }

        public void setChangeUrl(string changeUrl)
        {
            int i = 0;
            while (i < List.Count)
            {
                ((ProductImage)List[i]).changeUrl = changeUrl + "&imageCode=" + ((ProductImage)List[i]).code;
                i++;
            }

        }

    }
}
