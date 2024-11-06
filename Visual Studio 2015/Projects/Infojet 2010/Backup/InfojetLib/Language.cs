using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class Language
    {
        private string _webSiteCode;
        private string _languageCode;
        private string _description;
        private string _languageText;
        private string _currencyCode;
        private string _recPriceGroupCode;
        private string _cultureValue;
        private string _specificCultureValue;
        private string _changeUrl;

        public Language(WebSiteLanguage webSiteLanguage)
        {
            this._webSiteCode = webSiteLanguage.webSiteCode;
            this._languageCode = webSiteLanguage.languageCode;
            this._description = webSiteLanguage.description;
            this._languageText = webSiteLanguage.languageText;
            this._currencyCode = webSiteLanguage.currencyCode;
            this._recPriceGroupCode = webSiteLanguage.recPriceGroupCode;
            this._cultureValue = webSiteLanguage.cultureValue;
            this._specificCultureValue = webSiteLanguage.specificCultureValue;

        }

        public string webSiteCode
        {
            get
            {
                return this._webSiteCode;
            }
        }

        public string languageCode
        {
            get
            {
                return this._languageCode;
            }
        }

        public string description
        {
            get
            {
                return this._description;
            }
        }

        public string languageText
        {
            get
            {
                return this._languageText;
            }
        }

        public string currencyCode
        {
            get
            {
                return this._currencyCode;
            }
        }

        public string recPriceGroupCode
        {
            get
            {
                return this._recPriceGroupCode;
            }
        }

        public string cultureValue
        {
            get
            {
                return this._cultureValue;
            }
        }

        public string specificCultureValue
        {
            get
            {
                return this._specificCultureValue;
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
