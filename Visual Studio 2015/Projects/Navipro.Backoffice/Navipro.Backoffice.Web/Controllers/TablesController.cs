using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Controllers
{
    public class TablesController : BaseController
    {

        public ActionResult StaticTables()
        {
            return View();
        }

        public ActionResult DataTables()
        {
            return View();
        }

        public ActionResult FooTables()
        {
            return View();
        }

        public ActionResult jqGrid()
        {
            return View();
        }
	}
}