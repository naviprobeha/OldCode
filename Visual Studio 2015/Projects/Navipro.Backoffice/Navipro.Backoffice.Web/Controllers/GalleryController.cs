using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Controllers
{
    public class GalleryController : BaseController
    {

        public ActionResult BasicGallery()
        {
            return View();
        }

        public ActionResult BootstrapCarusela()
        {
            return View();
        }

        public ActionResult  SlickCarusela()
        {
            return View();
        }
	}
}