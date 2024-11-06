using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebModelDimensionCollection : CollectionBase
    {
        public WebModelDimension this[int index]
        {
            get { return (WebModelDimension)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebModelDimension webModelDimension)
        {
            return (List.Add(webModelDimension));
        }
        public int IndexOf(WebModelDimension webModelDimension)
        {
            return (List.IndexOf(webModelDimension));
        }
        public void Insert(int index, WebModelDimension webModelDimension)
        {
            List.Insert(index, webModelDimension);
        }
        public void Remove(WebModelDimension webModelDimension)
        {
            List.Remove(webModelDimension);
        }
        public bool Contains(WebModelDimension webModelDimension)
        {
            return (List.Contains(webModelDimension));
        }

    }
}
