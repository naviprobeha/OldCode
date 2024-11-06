using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebService
{
    /// <summary>
    /// Summary description for WebPageService
    /// </summary>
    [WebService(Namespace = "http://infojet.navipro.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebPageService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public WebPage getStartPage(string webSiteCode, string webUserAccountNo)
        {
            Global.init(webSiteCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            if (webUserAccountNo == "") webUserAccountNo = null;

            System.Collections.ArrayList webPageList = infojetContext.webSite.getWebPagesByCategory(infojetContext.webSite.startPageCategoryCode, webUserAccountNo);

            infojetContext.systemDatabase.close();

            if (webPageList.Count > 0)
            {
                WebPage webPage = (WebPage)webPageList[0];
                return webPage;                
            }

            return null;

        }
    }
}
