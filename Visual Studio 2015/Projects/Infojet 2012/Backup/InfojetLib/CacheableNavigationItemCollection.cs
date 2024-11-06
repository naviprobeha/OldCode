using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    [Serializable]
    public class CacheableNavigationItemCollection : CollectionBase
    {
        private DateTime _createdDateTime;

        public CacheableNavigationItemCollection()
        {
            _createdDateTime = DateTime.Now;
        }

        public CacheableNavigationItem this[int index]
        {
            get { return (CacheableNavigationItem)List[index]; }
            set { List[index] = value; }
        }
        public int Add(CacheableNavigationItem navigationItem)
        {
            _createdDateTime = DateTime.Now;
            return (List.Add(navigationItem));
        }
        public int IndexOf(CacheableNavigationItem navigationItem)
        {
            return (List.IndexOf(navigationItem));
        }
        public void Insert(int index, CacheableNavigationItem navigationItem)
        {
            List.Insert(index, navigationItem);
        }
        public void Remove(CacheableNavigationItem navigationItem)
        {
            List.Remove(navigationItem);
        }
        public bool Contains(CacheableNavigationItem navigationItem)
        {
            return (List.Contains(navigationItem));
        }

        public DateTime createdDateTime { get { return _createdDateTime; } }

        public NavigationItemCollection getNavigationItemCollection(Infojet infojetContext, NavigationItem parentNavigationItem)
        {
            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();

            int i = 0;
            while (i < Count)
            {
                NavigationItem navigationItem = new NavigationItem(this[i]);
                navigationItem.parentItem = parentNavigationItem;
                if (this[i].webImageCode != null) navigationItem.webImage = new WebImage(infojetContext, this[i].webImageCode);

                if (this[i].subNavigationItems != null)
                {
                    navigationItem.subNavigationItems = this[i].subNavigationItems.getNavigationItemCollection(infojetContext, navigationItem);
                }

                navigationItemCollection.Add(navigationItem);

                i++;
            }

            return navigationItemCollection;
        }

    }
}
