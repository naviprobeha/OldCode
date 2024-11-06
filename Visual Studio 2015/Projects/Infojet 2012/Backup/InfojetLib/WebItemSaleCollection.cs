using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebItemSaleCollection : CollectionBase
    {
        public WebItemSale this[int index]
        {
            get { return (WebItemSale)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebItemSale webItemSale)
        {
            return (List.Add(webItemSale));
        }
        public int IndexOf(WebItemSale webItemSale)
        {
            return (List.IndexOf(webItemSale));
        }
        public void Insert(int index, WebItemSale webItemSale)
        {
            List.Insert(index, webItemSale);
        }
        public void Remove(WebItemSale webItemSale)
        {
            List.Remove(webItemSale);
        }
        public bool Contains(WebItemSale webItemSale)
        {
            return (List.Contains(webItemSale));
        }


    }
}
