using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class ProductItemCollection : CollectionBase
    {
        private int _noOfItemsPerPage;
        private int _currentPage;
        private int _totalItems;

        public ProductItem this[int index]
        {
            get { return (ProductItem)List[index]; }
            set { List[index] = value; }
        }
        public int Add(ProductItem productItem)
        {
            return (List.Add(productItem));
        }
        public int IndexOf(ProductItem productItem)
        {
            return (List.IndexOf(productItem));
        }
        public void Insert(int index, ProductItem productItem)
        {
            List.Insert(index, productItem);
        }
        public void Remove(ProductItem productItem)
        {
            List.Remove(productItem);
        }
        public bool Contains(ProductItem productItem)
        {
            return (List.Contains(productItem));
        }
        public void setSize(int width, int height)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].productImage != null) this[i].productImage.setSize(width, height);
                i++;
            }
        }

        public void setExtendedTextLength(int length)
        {
            int i = 0;
            while (i < Count)
            {
                this[i].setExtendedTextLength(length);
                i++;
            }
        }

        public void setNoImageUrl(string noImageUrl)
        {
            int i = 0;
            while (i < Count)
            {
                this[i].noImageUrl = noImageUrl;
                i++;
            }
        }

        public void setPageNavigation(int noOfItemsPerPage, int currentPage, int totalItems)
        {
            _noOfItemsPerPage = noOfItemsPerPage;
            _currentPage = currentPage;
            _totalItems = totalItems;
        }

        public NavigationItemCollection getPageNavigation()
        {
            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();
            if (_noOfItemsPerPage > 0)
            {
                int noOfPages = _totalItems / _noOfItemsPerPage;
                if (_totalItems % _noOfItemsPerPage > 0) noOfPages++;

                int i = 0;
                while (i < noOfPages)
                {
                    int startNo = (i * _noOfItemsPerPage) + 1;
                    int endNo = (i * _noOfItemsPerPage) + _noOfItemsPerPage;
                    if ((_totalItems / _noOfItemsPerPage) == i) endNo = _totalItems;

                    NavigationItem navigationItem = new NavigationItem();
                    navigationItem.description = startNo.ToString() + "-" + endNo.ToString();
                    navigationItem.helpText = startNo.ToString() + " - " + endNo.ToString();
                    navigationItem.description2 = (i+1).ToString();

                    if ((_currentPage-1) == i) navigationItem.selected = true;

                    navigationItemCollection.Add(navigationItem);

                    i++;
                }
            }
            return navigationItemCollection;
        }

    }
}
