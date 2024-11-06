using Navipro.Backoffice.Web.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Controllers
{
    [OutputCache(Duration = 0)]
    public class BaseController : Controller
    {
        public BaseController()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                ViewBag.user = User.user;
                ViewBag.menuItems = User.profile.menuItems;
            }
        }
        protected virtual new UserPrincipal User
        {
            get { return System.Web.HttpContext.Current.User as UserPrincipal; }
        }

        protected virtual Database database
        {
            get
            {
                return (Database)Session["database"];
            }
        }
        protected bool authenticationCheck()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                ViewBag.user = User.user;
                ViewBag.menuItems = User.profile.menuItems;

                return true;
            }
            return false;
        }

        protected ActionResult getLoginView()
        {
            return RedirectToAction("Index", "Login");

        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}