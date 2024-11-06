using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navipro.Backoffice.Web.Models
{
    public class Orderer
    {
        public Orderer(CaseMember caseMember)
        {
            email = caseMember.email;
        }
        public string email { get; set; }
    }
}