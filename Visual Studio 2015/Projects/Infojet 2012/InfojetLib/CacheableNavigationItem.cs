using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Navipro.Infojet.Lib
{
    [Serializable]
    public class CacheableNavigationItem
    {
        private string _code;
        private string _text;
        private string _helpText;
        private string _link;
        private bool _selected;
        private bool _hasChilds;
        private int _windowMode;
        private string _webImageCode;

        private string _description;
        private string _description2;

        private CacheableNavigationItemCollection _subNavigationItems;
        private CacheableNavigationItem _parentItem;

        public CacheableNavigationItem(NavigationItem navigationItem)
        {
            this._code = navigationItem.code;
            this._description = navigationItem.description;
            this._description2 = navigationItem.description2;
            this._text = navigationItem.text;
            this._helpText = navigationItem.helpText;
            this._link = navigationItem.link;
            this._selected = navigationItem.selected;
            this._windowMode = navigationItem.windowMode;
            if (navigationItem.webImage != null) this._webImageCode = navigationItem.webImage.code;

            _subNavigationItems = new CacheableNavigationItemCollection();

            //if (navigationItem.parentItem != null) this._parentItem = new CacheableNavigationItem(navigationItem.parentItem);
        }

        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public string helpText
        {
            get
            {
                return _helpText;
            }
            set
            {
                _helpText = value;
            }
        }

        public string link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
            }
        }

        public bool selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                if (value == true)
                {
                    if (_parentItem != null) _parentItem.selected = value;
                }
            }
        }

        public bool hasChilds
        {
            get
            {
                return _hasChilds;
            }
            set
            {
                _hasChilds = value;
            }
        }

        public int windowMode
        {
            get
            {
                return _windowMode;
            }
            set
            {
                _windowMode = value;
            }
        }

        public string target
        {
            get
            {
                if (windowMode == 1) return "_blank";
                return "";
            }

        }

        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public string description2
        {
            get
            {
                return _description2;
            }
            set
            {
                _description2 = value;
            }
        }

        public string webImageCode
        {
            get
            {
                return _webImageCode;
            }
            set
            {
                _webImageCode = value;
            }
        }

        public CacheableNavigationItemCollection subNavigationItems
        {
            get
            {
                return _subNavigationItems;
            }
            set
            {
                _subNavigationItems = value;
            }
        }

        public CacheableNavigationItem parentItem
        {
            get
            {
                return _parentItem;
            }
            set
            {
                _parentItem = value;
            }
        }

    }
}
