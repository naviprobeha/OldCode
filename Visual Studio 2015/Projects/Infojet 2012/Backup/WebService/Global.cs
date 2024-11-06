using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebService
{
    public class Global
    {
        public static void init(string webSiteCode, string marketCode)
        {
            Navipro.Infojet.Lib.Configuration configuration = new Navipro.Infojet.Lib.Configuration();
            if (!configuration.init())
            {
                throw new Exception("Configuration faild to init.");
            }

            Navipro.Infojet.Lib.Database database = new Navipro.Infojet.Lib.Database(null, configuration);
            System.Web.HttpContext.Current.Session.Add("database", database);
            System.Web.HttpContext.Current.Session.Add("configuration", configuration);

            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            WebSite webSite = null;

            if (webSiteCode == "")
            {
                webSite = infojet.getWebSite(configuration);
                if (webSite == null)
                {
                    throw new Exception("No website could be found.");
                }
            }
            else
            {
                webSite = new WebSite(infojet, webSiteCode);
            }

            System.Web.HttpContext.Current.Session.Add("webSite", webSite);

            CartHandler cartHandler = new CartHandler(database, System.Web.HttpContext.Current.Session.SessionID, infojet);
            System.Web.HttpContext.Current.Session.Add("cartHandler", cartHandler);


            WebSiteLanguage webSiteLanguage = new WebSiteLanguage(database, webSite.code, marketCode);
            if (webSiteLanguage.cultureValue == null)
            {
                webSiteLanguage = new WebSiteLanguage(database, webSite.code, webSite.defaultLanguageCode); //Default market code
            }

            System.Web.HttpContext.Current.Session["currencyCode"] = webSiteLanguage.currencyCode;
            System.Web.HttpContext.Current.Session["languageCode"] = webSiteLanguage.languageCode;
            System.Web.HttpContext.Current.Session["marketCode"] = marketCode;
            System.Web.HttpContext.Current.Session["marketCountryCode"] = webSiteLanguage.marketCountryCode;

        }

        public static void init()
        {
            init("", "");
        }

        public static void init(string webSiteCode)
        {
            init(webSiteCode, "");
        }

    }
}
