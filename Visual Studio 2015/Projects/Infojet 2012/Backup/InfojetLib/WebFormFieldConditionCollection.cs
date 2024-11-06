using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebFormFieldConditionCollection : CollectionBase
    {
        public WebFormFieldCondition this[int index]
        {
            get { return (WebFormFieldCondition)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebFormFieldCondition webFormFieldCondition)
        {
            return (List.Add(webFormFieldCondition));
        }
        public int IndexOf(WebFormFieldCondition webFormFieldCondition)
        {
            return (List.IndexOf(webFormFieldCondition));
        }
        public void Insert(int index, WebFormFieldCondition webFormFieldCondition)
        {
            List.Insert(index, webFormFieldCondition);
        }
        public void Remove(WebFormFieldCondition webFormFieldCondition)
        {
            List.Remove(webFormFieldCondition);
        }
        public bool Contains(WebFormFieldCondition webFormFieldCondition)
        {
            return (List.Contains(webFormFieldCondition));
        }


    }
}
