#region Using

using System.Web.Mvc;

#endregion

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class TablesController : BaseController
    {
        // GET: tables/normal
        public ActionResult Normal()
        {
            AuthenticationCheck();

            return View();
        }

        // GET: tables/data-tables
        public ActionResult DataTables()
        {
            AuthenticationCheck();
            return View();
        }

        // GET: tables/jq-grid
        public ActionResult JQGrid()
        {
            AuthenticationCheck();
            return View();
        }
    }
}