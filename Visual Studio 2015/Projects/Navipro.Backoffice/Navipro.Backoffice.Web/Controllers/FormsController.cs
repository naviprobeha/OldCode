﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Controllers
{
    public class FormsController : BaseController
    {

        public ActionResult BasicFroms()
        {
            return View();
        }

        public ActionResult Advanced()
        {
            return View();
        }
     
        public ActionResult Wizard()
        {
            return View();
        }
      
        public ActionResult FileUpload()
        {
            return View();
        }

        public ActionResult TextEditor()
        {
            return View();
        }

        public ActionResult Markdown()
        {
            return View();
        }

        public ActionResult Autocomplete()
        {
            return View();
        }

    }
}