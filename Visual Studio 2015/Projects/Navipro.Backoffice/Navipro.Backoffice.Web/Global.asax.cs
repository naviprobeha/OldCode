using Navipro.Backoffice.Web.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Navipro.Backoffice.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

        }

        protected void Session_Start()
        {
            Configuration configuration = new Configuration();
            configuration.init();
            Database database = new Database(configuration);
            Session.Add("database", database);

            Database resourcesDatabase = new Database(configuration, configuration.resourcesConnectionString);
            Session.Add("resourcesDatabase", resourcesDatabase);

            
        }

        protected void Session_End()
        {
            Database database = (Database)Session["database"];
            database.close();

            Database resourcesDatabase = (Database)Session["resourcesDatabase"];
            resourcesDatabase.close();

        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                authCookie.Expires = authCookie.Expires.AddMonths(3);

                Navipro.Backoffice.Web.Models.User user = JsonConvert.DeserializeObject<Navipro.Backoffice.Web.Models.User>(authTicket.UserData);
                UserPrincipal newUser = new UserPrincipal(user.email, user);

                //Profile
                Dictionary<string, Navipro.Backoffice.Web.Models.DataView> dataViewTable = Navipro.Backoffice.Web.Models.DataView.load();
                //newUser.profile = Navipro.Backoffice.Web.Models.Profile.load(user.profileCode, dataViewTable);
                newUser.profile = Navipro.Backoffice.Web.Models.Profile.load("SUPPORTAGENT", dataViewTable, user);

                HttpContext.Current.User = newUser;
                

            }
        }
    }
}
