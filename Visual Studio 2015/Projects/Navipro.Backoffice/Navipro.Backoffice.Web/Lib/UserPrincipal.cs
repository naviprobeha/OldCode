using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Navipro.Backoffice.Web.Lib;
using Navipro.Backoffice.Web.Models;

namespace Navipro.Backoffice.Web.Lib
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {

            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserPrincipal(string email, User user)
        {
            this.Identity = new GenericIdentity(email);
            this.user = user;

        }

        public User user { get; private set; }
        public string Role { get; set; }
        public Profile profile { get; set; }
        public string[] roles { get; set; }
    }

}