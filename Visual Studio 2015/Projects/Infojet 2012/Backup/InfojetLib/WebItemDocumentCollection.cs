using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebItemDocumentCollection : CollectionBase
    {
        public WebItemDocument this[int index]
        {
            get { return (WebItemDocument)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebItemDocument webItemDocument)
        {
            return (List.Add(webItemDocument));
        }
        public int IndexOf(WebItemDocument webItemDocument)
        {
            return (List.IndexOf(webItemDocument));
        }
        public void Insert(int index, WebItemDocument webItemDocument)
        {
            List.Insert(index, webItemDocument);
        }
        public void Remove(WebItemDocument webItemDocument)
        {
            List.Remove(webItemDocument);
        }
        public bool Contains(WebItemDocument webItemDocument)
        {
            return (List.Contains(webItemDocument));
        }


    }
}
