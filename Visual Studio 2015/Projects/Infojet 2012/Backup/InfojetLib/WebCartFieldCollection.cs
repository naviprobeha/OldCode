using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebCartFieldCollection : CollectionBase
    {
        public WebCartField this[int index]
        {
            get { return (WebCartField)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebCartField webCartField)
        {
            return (List.Add(webCartField));
        }
        public int IndexOf(WebCartField webCartField)
        {
            return (List.IndexOf(webCartField));
        }
        public void Insert(int index, WebCartField webCartField)
        {
            List.Insert(index, webCartField);
        }
        public void Remove(WebCartField webCartField)
        {
            List.Remove(webCartField);
        }
        public bool Contains(WebCartField cartItem)
        {
            return (List.Contains(cartItem));
        }

        public string getFieldValue(string fieldCode)
        {
            int i = 0;
            while (i < Count)
            {
                if (fieldCode == this[i].fieldCode) return this[i].value;
                i++;
            }

            return null;
        }

        public bool setFieldValue(string fieldCode, string fieldValue)
        {
            int i = 0;
            while (i < Count)
            {
                if (fieldCode == this[i].fieldCode)
                {
                    this[i].value = fieldValue;
                    return true;
                }
                i++;
            }

            return false;

        }
     }
}
