using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class WebNewsCollection : CollectionBase
    {
        public WebNewsEntry this[int index]
        {
            get { return (WebNewsEntry)List[index]; }
            set { List[index] = value; }
        }
        public int Add(WebNewsEntry webNewsEntry)
        {
            return (List.Add(webNewsEntry));
        }
        public int IndexOf(WebNewsEntry webNewsEntry)
        {
            return (List.IndexOf(webNewsEntry));
        }
        public void Insert(int index, WebNewsEntry webNewsEntry)
        {
            List.Insert(index, webNewsEntry);
        }
        public void Remove(WebNewsEntry webNewsEntry)
        {
            List.Remove(webNewsEntry);
        }
        public bool Contains(WebNewsEntry webNewsEntry)
        {
            return (List.Contains(webNewsEntry));
        }

        public void setIntroImageSize(int width, int height)
        {
            int i = 0;
            while (i < List.Count)
            {
                ((WebNewsEntry)List[i]).setIntroImageSize(width, height);
                i++;
            }

        }

        public void setFullImageSize(int width, int height)
        {
            int i = 0;
            while (i < List.Count)
            {
                ((WebNewsEntry)List[i]).setMainImageSize(width, height);
                i++;
            }

        }

    }
}
