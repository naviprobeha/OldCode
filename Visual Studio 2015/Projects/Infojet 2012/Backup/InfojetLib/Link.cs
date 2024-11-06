using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Navipro.Infojet.Lib
{
    public class Link
    {
        private string _webPageCode = "";
        private string _webPageDescription = "";
        private string _templateFile = "";
        private string _languageCulture = "";
        private string _languageCode = "";
        private string _categoryCode = "";
        private string _categoryDescription = "";
        private string _itemModelNo = "";
        private string _itemNo = "";
        private string _itemDescription = "";
        private string _newsEntryNo = "";
        private string _newsEntryDescription = "";
        private Hashtable parameterTable;
        private Infojet infojetContext;

        public Link(Infojet infojetContext, WebPage webPage, string templateFile)
        {
            parameterTable = new Hashtable();
            this.infojetContext = infojetContext;
            this._webPageCode = webPage.code;
            this._webPageDescription = webPage.description;
            this._templateFile = templateFile;
            this._languageCulture = infojetContext.language.cultureValue;

            
        }

        public Link(Infojet infojetContext, string webPageCode, string webPageDescription, string templateFile)
        {
            parameterTable = new Hashtable();

            this.infojetContext = infojetContext;
            this._webPageCode = webPageCode;
            this._webPageDescription = webPageDescription;
            this._templateFile = templateFile;
            this._languageCulture = infojetContext.language.cultureValue;
        }

        public string languageCulture { get { return _languageCulture; } }

        public void setLanguageCulture(string languageCulture, string languageCode)
        {
            this._languageCulture = languageCulture;
            this._languageCode = languageCode;
        }

        public void setNewsEntry(string newsEntryNo, string newsEntryDescription)
        {
            this._newsEntryNo = newsEntryNo;
            this._newsEntryDescription = newsEntryDescription;
        }

        public void setCategory(string code, string description)
        {
            if (code != null)
            {
                this._categoryCode = code;
                this._categoryDescription = description;
            }
        }

        public void addParameter(string name, string value)
        {
            parameterTable.Add(name, value);
        }

 
        public void setItem(string modelNo, string no, string description)
        {
            this._itemNo = no;
            this._itemModelNo = modelNo;
            this._itemDescription = description;
        }

        public string toUrl()
        {
            if (infojetContext.webSite.fancyLinks) return toNamedUrl();
            return toParameterUrl();
        }

        private string toNamedUrl()
        {            
            string url = "";
            string parameters = "";
            string webSiteName = "";
            string languageCulture = _languageCulture;
            if (languageCulture != "") languageCulture = languageCulture + "/";

            foreach (DictionaryEntry pair in parameterTable)
            {
                parameters = parameters + "&"+pair.Key+"="+pair.Value.ToString();
            }
            if (parameters != "") parameters = "?" + parameters.Substring(1);

            if (infojetContext.configuration.webSiteCode == "") webSiteName = infojetContext.webSite.shortcutName + "/";

            if (_categoryDescription == "") _categoryDescription = _categoryCode;
            if (_itemDescription == "") _itemDescription = _itemNo;

            if (_categoryDescription != "")
            {
                url = infojetContext.webSite.siteLocation + webSiteName + languageCulture + urlEncode(_categoryDescription.ToLower());
                if (_itemDescription != "") url = url + "/" + urlEncode(_itemDescription.ToLower());
                return (url + parameters);
            }
            if (_itemDescription != "")
            {
                url = infojetContext.webSite.siteLocation + webSiteName + languageCulture + urlEncode(_itemDescription.ToLower());
                return (url + parameters);
            }
            if (_newsEntryDescription != "")
            {
                url = infojetContext.webSite.siteLocation + webSiteName + languageCulture + urlEncode(_newsEntryDescription.ToLower());
                return (url + parameters);
            }

            url = infojetContext.webSite.siteLocation + webSiteName + languageCulture + urlEncode(_webPageDescription.ToLower());
            return (url + parameters);    

        }

        private string toParameterUrl()
        {
            string url = "";
            if (infojetContext.configuration.webSiteCode == "")
            {
                url = infojetContext.webSite.location + _templateFile + "?webSiteCode=" + infojetContext.webSite.code.ToLower() + "&pageCode=" + _webPageCode.ToLower();
            }
            else
            {
                url = infojetContext.webSite.location + _templateFile + "?pageCode=" + _webPageCode.ToLower();
            }
            //if ((_categoryCode != "") && (_categoryCode != null)) url = url + "&category=" + urlEncode(_categoryCode.ToLower());
            if ((_categoryCode != "") && (_categoryCode != null)) url = url + "&category=" + _categoryCode.ToLower();
            if (_itemNo != "") url = url + "&itemNo=" + _itemNo;
            if (_itemModelNo != "") url = url + "&webModelNo=" + _itemModelNo;
            if (_newsEntryNo != "") url = url + "&newsEntryNo=" + _newsEntryNo;
            if (_languageCode != "") url = url + "&languageCode=" + _languageCode;

            string parameters = "";
            foreach (DictionaryEntry pair in parameterTable)
            {
                parameters = parameters + "&" + pair.Key + "=" + pair.Value;
            }

            return url + parameters;
        }

        private string urlEncode(string text)
        {
            text = text.Replace("å", "a");
            text = text.Replace("ä", "a");
            text = text.Replace("ö", "o");
            text = text.Replace(" ", "-");
            text = text.Replace("&amp;", "-");
            text = text.Replace("&", "-");
            text = text.Replace("/", "-");

            return text;
        }

 
    }
}
