using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebItemConfigDefValueCollection : CollectionBase
    {
        public WebItemConfigDefValue this[int index]
        {
            get { return (WebItemConfigDefValue)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebItemConfigDefValue webItemConfigDefValue)
        {
            return (List.Add(webItemConfigDefValue));
        }
        public int IndexOf(WebItemConfigDefValue webItemConfigDefValue)
        {
            return (List.IndexOf(webItemConfigDefValue));
        }
        public void Insert(int index, WebItemConfigDefValue webItemConfigDefValue)
        {
            List.Insert(index, webItemConfigDefValue);
        }
        public void Remove(WebItemConfigDefValue webItemConfigDefValue)
        {
            List.Remove(webItemConfigDefValue);
        }
        public bool Contains(WebItemConfigDefValue webItemConfigDefValue)
        {
            return (List.Contains(webItemConfigDefValue));
        }


    }
}
