using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebItemConfigLineCollection : CollectionBase
    {
        public WebItemConfigLine this[int index]
        {
            get { return (WebItemConfigLine)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebItemConfigLine webItemConfigLine)
        {
            return (List.Add(webItemConfigLine));
        }
        public int IndexOf(WebItemConfigLine webItemConfigLine)
        {
            return (List.IndexOf(webItemConfigLine));
        }
        public void Insert(int index, WebItemConfigLine webItemConfigLine)
        {
            List.Insert(index, webItemConfigLine);
        }
        public void Remove(WebItemConfigLine webItemConfigLine)
        {
            List.Remove(webItemConfigLine);
        }
        public bool Contains(WebItemConfigLine webItemConfigLine)
        {
            return (List.Contains(webItemConfigLine));
        }

        public void applyValues(WebItemConfigDefValueCollection webItemConfigDefValueCollection)
        {
            int i = 0;
            while (i < Count)
            {
                this[i].applyValues(webItemConfigDefValueCollection);
                i++;
            }
        }

        public void updateLineAmount(WebItemConfigHeader webItemConfigHeader)
        {
            int i = 0;
            while (i < Count)
            {
                this[i].updateLineAmount(webItemConfigHeader);
                i++;
            }
        }

        public WebItemConfigLine getWebItemConfigLineFromControl(string controlId)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].controlId == controlId) return this[i];
                i++;
            }

            return null;
        }

        public WebItemConfigLine getWebItemConfigLineFromOption(string optionCode)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].optionCode == optionCode) return this[i];
                i++;
            }

            return null;
        }
    }
}
