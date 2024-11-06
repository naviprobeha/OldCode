using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebService
{
    /// <summary>
    /// Summary description for LoginService
    /// </summary>
    [WebService(Namespace = "http://infojet.navipro.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebUserProfileService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public UserSession authenticate(string webSiteCode, string userId, string password)
        {
            Global.init(webSiteCode);

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            SystemHandler systemHandler = new SystemHandler(infojetContext);
            if (systemHandler.authenticate(infojetContext, userId, password))
            {

                UserSession userSession = infojetContext.userSession;

                System.Collections.ArrayList webPageList = infojetContext.webSite.getWebPagesByCategory(infojetContext.webSite.startPageCategoryCode, userSession.webUserAccount.no);

                infojetContext.systemDatabase.close();

                if (webPageList.Count > 0)
                {
                    WebPage webPage = (WebPage)webPageList[0];
                    userSession.startPageCode = webPage.code;
                }


                return userSession;
            }

            return null;

        }
    }
}
