using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace VoucherService.Library
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HeaderAuthorizationAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            Configuration configuration = new Configuration();

            string authHeader = "";
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("Authorization", out values))
            {
                authHeader = values.First();
            }

            if (authHeader == "Bearer "+configuration.authorizationKey) return;

            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);

        }

    }
}