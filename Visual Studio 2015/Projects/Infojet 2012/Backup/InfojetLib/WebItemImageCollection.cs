using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebItemImageCollection : CollectionBase
    {
        public WebItemImage this[int index]
        {
            get { return (WebItemImage)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebItemImage webItemImage)
        {
            return (List.Add(webItemImage));
        }
        public int IndexOf(WebItemImage webItemImage)
        {
            return (List.IndexOf(webItemImage));
        }
        public void Insert(int index, WebItemImage webItemImage)
        {
            List.Insert(index, webItemImage);
        }
        public void Remove(WebItemImage webItemImage)
        {
            List.Remove(webItemImage);
        }
        public bool Contains(WebItemImage webItemImage)
        {
            return (List.Contains(webItemImage));
        }

        public ProductImageCollection toProductImageCollection(string description)
        {
            ProductImageCollection productImageCollection = new ProductImageCollection();

            int i = 0;
            while (i < List.Count)
            {
                productImageCollection.Add(new ProductImage((WebItemImage)List[i], description));
                i++;
            }

            return productImageCollection;
        }
    }
}
