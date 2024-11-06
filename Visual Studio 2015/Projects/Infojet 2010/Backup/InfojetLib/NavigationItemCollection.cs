using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    public class NavigationItemCollection : CollectionBase, IHierarchicalEnumerable
    {
        public NavigationItem this[int index]
        {
            get { return (NavigationItem)List[index]; }
            set { List[index] = value; }
        }
        public int Add(NavigationItem navigationItem)
        {
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

        public void setSelected(string selectedCode)
        {
            if ((selectedCode != null) && (selectedCode != ""))
            {
                int i = 0;
                while (i < Count)
                {
                    if (this[i].code == selectedCode)
                    {
                        this[i].selected = true;
                        return;
                    }
                    if (this[i].subNavigationItems != null) this[i].subNavigationItems.setSelected(selectedCode);

                    i++;
                }
            }
        }

        #region IHierarchicalEnumerable Members

        public IHierarchyData GetHierarchyData(object enumeratedItem)
        {
            return (IHierarchyData)List[List.IndexOf(enumeratedItem)];
        }

        #endregion
    }
}
