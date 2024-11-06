using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class WebPaymentMethodCollection : CollectionBase
    {
        public WebPaymentMethod this[int index]
        {
            get { return (WebPaymentMethod)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebPaymentMethod webPaymentMethod)
        {
            return (List.Add(webPaymentMethod));
        }
        public int IndexOf(WebPaymentMethod webPaymentMethod)
        {
            return (List.IndexOf(webPaymentMethod));
        }
        public void Insert(int index, WebPaymentMethod webPaymentMethod)
        {
            List.Insert(index, webPaymentMethod);
        }
        public void Remove(WebPaymentMethod webPaymentMethod)
        {
            List.Remove(webPaymentMethod);
        }
        public bool Contains(WebPaymentMethod webPaymentMethod)
        {
            return (List.Contains(webPaymentMethod));
        }


    }
}
