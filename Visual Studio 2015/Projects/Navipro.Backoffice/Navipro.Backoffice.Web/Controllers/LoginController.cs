using Navipro.Backoffice.Web.Lib;
using Navipro.Backoffice.Web.Models;
using Navipro.Backoffice.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Navipro.Backoffice.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model, string returnUrl = "")
        {
            Database database = (Database)Session["database"];

            //Do login
            if (ModelState.IsValid)
            {
                if (AuthenticationHandler.authenticate(database, model.email, model.password, model.rememberMe))
                {                    
                    return RedirectToAction("Index", "Home");                    
                }
                else
                {
                    model.errorMessage = "Okänt användarnamn eller lösenord.";
                }
            }

            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Logout()
        {
            AuthenticationHandler.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}