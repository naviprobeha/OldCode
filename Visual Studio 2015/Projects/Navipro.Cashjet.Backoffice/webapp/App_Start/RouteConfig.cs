#region Using

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace SmartAdminMvc
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(
                        name: "Language",
                        url: "{lang}/{controller}/{action}/{id}",
                        defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                        constraints: new { lang = @"sv|en" }
                    );

            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional,
                lang = "en"
            }).RouteHandler = new DashRouteHandler();
        }
    }
}