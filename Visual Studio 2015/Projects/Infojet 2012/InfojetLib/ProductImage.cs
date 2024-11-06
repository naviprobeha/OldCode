using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class ProductImage
    {
        private string _code;
        private string _description;
        private WebItemImage _webItemImage;
        private string _url;
        private string _changeUrl;

        public ProductImage(WebItemImage webItemImage, string description)
        {
            this._webItemImage = webItemImage;
            this._url = webItemImage.image.getUrl();
            this._description = description;
            this._code = webItemImage.webImageCode;
        }

        public void setSize(int width, int height)
        {
            this._url = webItemImage.image.getUrl(width, height);
        }

        public string getUrlFromSize(int width, int height)
        {
            setSize(width, height);
            return this._url;
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

        public WebItemImage webItemImage
        {
            get
            {
                return _webItemImage;
            }
            set
            {
                _webItemImage = value;
            }
        }

        public string url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }

        public string changeUrl
        {
            get
            {
                return _changeUrl;
            }
            set
            {
                _changeUrl = value;
            }
        }

    }
}
