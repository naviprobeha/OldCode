using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartAdminMvc
{
    public static class LanguageHelper
    {
        private static Dictionary<string, Language> languages;

        public static void Init()
        {
            if (languages == null)
            {
                languages = new Dictionary<string, Language>();
                languages.Add("sv", new Language("Swedish", "sv", "se"));
                languages.Add("en", new Language("English", "en", "us"));
            }
        }
        public static MvcHtmlString LangSwitcher(this UrlHelper url, RouteData routeData, int languageNo)
        {
            Init();

            Language language = languages.Values.ToList<Language>()[languageNo];

            var liTagBuilder = new TagBuilder("li");
            var aTagBuilder = new TagBuilder("a");
            var imgTagBuilder = new TagBuilder("img");
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            if (routeValueDictionary.ContainsKey("lang"))
            {
                if (routeData.Values["lang"] as string == language.language)
                {
                    liTagBuilder.AddCssClass("active");
                }
                else
                {
                    routeValueDictionary["lang"] = language.language;
                }
            }


            aTagBuilder.MergeAttribute("href", url.RouteUrl(routeValueDictionary));

            imgTagBuilder.MergeAttribute("src", "/content/img/blank.gif");
            imgTagBuilder.AddCssClass("flag flag-" + language.flag);
            imgTagBuilder.MergeAttribute("alt", language.name);

            aTagBuilder.InnerHtml = imgTagBuilder.ToString() + " " + language.name;

            liTagBuilder.InnerHtml = aTagBuilder.ToString();
            return new MvcHtmlString(liTagBuilder.ToString());
        }

        public static MvcHtmlString GetCurrentLanguage(this UrlHelper url, RouteData routeData)
        {
            Init();

            var routeValueDictionary = new RouteValueDictionary(routeData.Values);

            string currentLang = "en";
            if (routeValueDictionary.ContainsKey("lang"))
            {
                currentLang = (routeData.Values["lang"] as string);
            }

            Language currentLanguage = languages[currentLang];

            var imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", "/content/img/blank.gif");
            imgTagBuilder.AddCssClass("flag flag-" + currentLanguage.flag);
            imgTagBuilder.MergeAttribute("alt", currentLanguage.name);

            return new MvcHtmlString(imgTagBuilder.ToString() + " <span>" + currentLanguage.name + "</span>");

        }
    }
}