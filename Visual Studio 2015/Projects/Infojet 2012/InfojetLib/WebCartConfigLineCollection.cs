using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebCartConfigLineCollection : CollectionBase
    {
        public WebCartConfigLine this[int index]
        {
            get { return (WebCartConfigLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebCartConfigLine webCartConfigLine)
        {
            return (List.Add(webCartConfigLine));
        }
        public int IndexOf(WebCartConfigLine webCartConfigLine)
        {
            return (List.IndexOf(webCartConfigLine));
        }
        public void Insert(int index, WebCartConfigLine webCartConfigLine)
        {
            List.Insert(index, webCartConfigLine);
        }
        public void Remove(WebCartConfigLine webCartConfigLine)
        {
            List.Remove(webCartConfigLine);
        }
        public bool Contains(WebCartConfigLine webCartConfigLine)
        {
            return (List.Contains(webCartConfigLine));
        }

    }
}
