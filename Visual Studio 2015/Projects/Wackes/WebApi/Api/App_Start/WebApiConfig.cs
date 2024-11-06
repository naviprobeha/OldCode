using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "BoschApi",
                routeTemplate: "bosch/availableproducts/{id}",
                defaults: new { controller = "boschavailableproduct", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "BoschApi2",
                routeTemplate: "bosch/products/{id}",
                defaults: new { controller = "boschproduct", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "BoschApi3",
                routeTemplate: "bosch/order",
                defaults: new { controller = "boschorder", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
