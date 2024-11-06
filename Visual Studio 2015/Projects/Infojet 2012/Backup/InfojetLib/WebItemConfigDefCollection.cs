using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebItemConfigDefCollection : CollectionBase
    {
        public WebItemConfigDef this[int index]
        {
            get { return (WebItemConfigDef)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebItemConfigDef webItemConfigDef)
        {
            return (List.Add(webItemConfigDef));
        }
        public int IndexOf(WebItemConfigDef webItemConfigDef)
        {
            return (List.IndexOf(webItemConfigDef));
        }
        public void Insert(int index, WebItemConfigDef webItemConfigDef)
        {
            List.Insert(index, webItemConfigDef);
        }
        public void Remove(WebItemConfigDef webItemConfigDef)
        {
            List.Remove(webItemConfigDef);
        }
        public bool Contains(WebItemConfigDef webItemConfigDef)
        {
            return (List.Contains(webItemConfigDef));
        }

     }
}
