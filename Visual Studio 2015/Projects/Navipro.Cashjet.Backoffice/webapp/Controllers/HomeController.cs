#region Using

using System;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        // GET: home/index
        public ActionResult Index()
        {
            AuthenticationCheck();



            return View();
        }

        public ActionResult ChangeEnvironment(string environmentCode)
        {
            AuthenticationCheck();

            foreach(Environment env in EnvironmentList)
            {
                if (env.code == environmentCode)
                {
                    SetCurrentEnvironment(env);
                }
            }

            return Redirect("/");
        }

        public ActionResult Social()
        {
            return View();
        }

        // GET: home/inbox
        public ActionResult Inbox()
        {
            return View();
        }

        // GET: home/widgets
        public ActionResult Widgets()
        {
            return View();
        }

        // GET: home/chat
        public ActionResult Chat()
        {
            return View();
        }
    }
}