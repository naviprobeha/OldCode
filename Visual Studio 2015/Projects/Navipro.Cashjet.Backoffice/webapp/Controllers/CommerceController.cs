#region Using

using System.Web.Mvc;

#endregion

namespace SmartAdminMvc.Controllers
{
    public class CommerceController : BaseController
    {
        // GET: /commerce/orders
        public ActionResult Orders()
        {
            AuthenticationCheck();

            return View();
        }

        // GET: /commerce/productview
        public ActionResult ProductView()
        {
            AuthenticationCheck();

            return View();
        }

        // GET: /commerce/detail
        public ActionResult Detail()
        {
            AuthenticationCheck();

            return View();
        }
    }
}