﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Controllers
{
    public class LayoutsController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OffCanvas()
        {
            return View();
        }
	}
}