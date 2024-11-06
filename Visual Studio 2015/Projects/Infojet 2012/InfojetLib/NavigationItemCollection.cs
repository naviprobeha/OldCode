using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    [Serializable]
    public class NavigationItemCollection : CollectionBase, IHierarchicalEnumerable
    {
        private DateTime _createdDateTime;

        public NavigationItem this[int index]
        {
            get { return (NavigationItem)List[index]; }
            set { List[index] = value; }
        }
        public int Add(NavigationItem navigationItem)
        {
            _createdDateTime = DateTime.Now;
            return (List.Add(navigationItem));
        }
        public int IndexOf(NavigationItem navigationItem)
        {
            return (List.IndexOf(navigationItem));
        }
        public void Insert(int index, NavigationItem navigationItem)
        {
            List.Insert(index, navigationItem);
        }
        public void Remove(NavigationItem navigationItem)
        {
            List.Remove(navigationItem);
        }
        public bool Contains(NavigationItem navigationItem)
        {
            return (List.Contains(navigationItem));
        }

        public void setSelectedCategory(string selectedCategory)
        {
            
            if ((selectedCategory != null) && (selectedCategory != ""))
            {
                int i = 0;
                while (i < Count)
                {

                    if (this[i].code.ToUpper() == selectedCategory.ToUpper())
                    {
                        this[i].selected = true;
                    }
                    else
                    {
                        this[i].selected = false;
                    }

                    if (this[i].subNavigationItems != null) this[i].subNavigationItems.setSelectedCategory(selectedCategory);

                    i++;
                }
            }
        }

        public void clearSelectedCategory()
        {
            int i = 0;
            while (i < Count)
            {
                this[i].selected = false;
                if (this[i].subNavigationItems != null) this[i].subNavigationItems.clearSelectedCategory();

                i++;
            }
        }

        public void setWebImageSize(int width, int height)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].webImage != null)
                {
                    this[i].webImageUrl = this[i].webImage.getUrl(width, height);
                }

                i++;
            }
        }

        public CacheableNavigationItemCollection getCacheableNavigationItemCollection(CacheableNavigationItem parentNavigationItem)
        {
            CacheableNavigationItemCollection cacheableNavigationItemCollection = new CacheableNavigationItemCollection();

            int i = 0;
            while (i < Count)
            {
                CacheableNavigationItem navigationItem = new CacheableNavigationItem(this[i]);
                navigationItem.parentItem = parentNavigationItem;

                if (this[i].subNavigationItems != null)
                {
                    navigationItem.subNavigationItems = this[i].subNavigationItems.getCacheableNavigationItemCollection(navigationItem);
                }

                cacheableNavigationItemCollection.Add(navigationItem);

                i++;
            }

            return cacheableNavigationItemCollection;
        }

        public DateTime createdDateTime { get { return _createdDateTime; } }

        #region IHierarchicalEnumerable Members

        public IHierarchyData GetHierarchyData(object enumeratedItem)
        {
            return (IHierarchyData)List[List.IndexOf(enumeratedItem)];
        }

        #endregion
    }
}
