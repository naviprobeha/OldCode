using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class LinkFactory
    {

        public static string createPageLink(Infojet infojetContext, WebPage webPage, string languageCulture, string category, string itemModelNo, string itemNo)
        {
            if (infojetContext.webSite.fancyLinks) return createRewritePageLink(infojetContext, webPage, languageCulture, category, itemModelNo, itemNo);
            return createParameterPageLink(infojetContext, webPage, languageCulture, category, itemModelNo, itemNo);

        }

        private static string createRewritePageLink(Infojet infojetContext, WebPage webPage, string languageCulture, string category, string itemModelNo, string itemNo)
        {
            string url = webPage.getUrl();

            url = url.Replace("[languageCulture]", languageCulture);

            if (category != "") url = url + "/" + category;
            if ((itemModelNo != "") || (itemNo != "")) url = url + "/" + itemModelNo.ToLower() + "/" + itemNo.ToLower();
            return url;
        }

        private static string createParameterPageLink(Infojet infojetContext, WebPage webPage, string languageCulture, string category, string itemModelNo, string itemNo)
        {
            string url = webPage.getUrl();

            url = url + "&languageCode=" + languageCulture;
            if (category != "") url = url + "&category=" + category;
            if (itemModelNo != "") url = url + "&webModelNo=" + itemModelNo;
            if (itemNo != "") url = url + "&itemNo = " + itemNo;

            return url;
        }

    }
}
