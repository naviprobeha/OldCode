using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{

    public class LanguageCollection : CollectionBase
    {
        public Language this[int index]
        {
            get { return (Language)List[index]; }
            set { List[index] = value; }
        }
        public int Add(Language language)
        {
            return (List.Add(language));
        }
        public int IndexOf(Language language)
        {
            return (List.IndexOf(language));
        }
        public void Insert(int index, Language language)
        {
            List.Insert(index, language);
        }
        public void Remove(Language language)
        {
            List.Remove(language);
        }
        public bool Contains(Language language)
        {
            return (List.Contains(language));
        }


    }
}
