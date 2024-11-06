using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Navipro.Backoffice.Web.Models;
using System.Web.Security;

namespace Navipro.Backoffice.Web.Lib
{
    public class AuthenticationHandler
    {

        public static bool authenticate(Database database, string email, string password, bool rememberMe)
        {
            Navipro.Backoffice.Web.Models.User user = Navipro.Backoffice.Web.Models.User.getUserByEmailAndPassword(database, email, password);
            if (user != null)
            {
                user.refreshCustomer(database);
                DateTime expiration = DateTime.Now.AddMinutes(30);
                if (rememberMe) expiration = expiration.AddMonths(3);

                string userData = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.email,
                DateTime.Now,
                expiration,
                rememberMe, //pass here true, if you want to implement remember me functionality
                userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                if (rememberMe)
                {
                    faCookie.Expires = DateTime.Now.AddMonths(3);
                }
                System.Web.HttpContext.Current.Response.Cookies.Add(faCookie);                   

                return true;

            }

            return false;
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie1);
        
        }
    }

}