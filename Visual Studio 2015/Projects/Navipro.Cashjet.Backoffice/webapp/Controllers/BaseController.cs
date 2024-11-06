using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartAdminMvc.Controllers
{
    public abstract class BaseController : Controller
    {
        private string CurrentLanguageCode { get; set; }
        protected Environment CurrentEnvironment { get; set; }
        protected List<Environment> EnvironmentList { get; set; }
        protected SystemDatabase systemDatabase { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLanguageCode = (string)requestContext.RouteData.Values["lang"];
                if (CurrentLanguageCode != null)
                {
                    
                    try
                    {
                        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguageCode);
                    }
                    catch (Exception)
                    {
                        throw new NotSupportedException($"Invalid language code '{CurrentLanguageCode}'.");
                    }
                    
                }

                
            }

            base.Initialize(requestContext);
        }

        protected void AuthenticationCheck()
        {
            systemDatabase = new SystemDatabase();

            
            if (Request.IsAuthenticated)
            {

                EnvironmentList = Environment.getList(systemDatabase, User.Identity.Name);
                ViewBag.EnvironmentList = EnvironmentList;

                if (EnvironmentList.Count > 0)
                {
                    SetCurrentEnvironment(EnvironmentList[0]);
                }
                
            }

        }

        protected void SetCurrentEnvironment(Environment env)
        {
            CurrentEnvironment = env;
            ViewBag.CurrentEnvironment = CurrentEnvironment;

            systemDatabase.checkEnvironmentTableStructure(CurrentEnvironment);
        }
    }
}