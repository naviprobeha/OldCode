using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebItemRelationCollection : CollectionBase
    {
        public WebItemRelation this[int index]
        {
            get { return (WebItemRelation)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebItemRelation webItemRelation)
        {
            return (List.Add(webItemRelation));
        }
        public int IndexOf(WebItemRelation webItemRelation)
        {
            return (List.IndexOf(webItemRelation));
        }
        public void Insert(int index, WebItemRelation webItemRelation)
        {
            List.Insert(index, webItemRelation);
        }
        public void Remove(WebItemRelation webItemRelation)
        {
            List.Remove(webItemRelation);
        }
        public bool Contains(WebItemRelation webItemRelation)
        {
            return (List.Contains(webItemRelation));
        }


    }
}
