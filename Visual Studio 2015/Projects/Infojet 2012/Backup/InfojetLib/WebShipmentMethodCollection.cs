using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class WebShipmentMethodCollection : CollectionBase
    {
        public WebShipmentMethod this[int index]
        {
            get { return (WebShipmentMethod)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebShipmentMethod webShipmentMethod)
        {
            return (List.Add(webShipmentMethod));
        }
        public int IndexOf(WebShipmentMethod webShipmentMethod)
        {
            return (List.IndexOf(webShipmentMethod));
        }
        public void Insert(int index, WebShipmentMethod webShipmentMethod)
        {
            List.Insert(index, webShipmentMethod);
        }
        public void Remove(WebShipmentMethod webShipmentMethod)
        {
            List.Remove(webShipmentMethod);
        }
        public bool Contains(WebShipmentMethod webShipmentMethod)
        {
            int i = 0;
            while (i < this.Count)
            {
                if (this[i].code == webShipmentMethod.code) return true;
                i++;
            }
            return false;
        }

    }
}
