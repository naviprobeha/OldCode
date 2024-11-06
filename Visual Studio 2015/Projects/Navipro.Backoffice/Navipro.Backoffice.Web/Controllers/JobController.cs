using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Navipro.Backoffice.Web.Models;
using System.Net;

namespace Navipro.Backoffice.Web.Controllers
{
    public class JobController : BaseController
    {
        // GET: Customer
        public ActionResult List(string dataView = "")
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            DataView dataViewObject = null;

            dataViewObject = User.profile.allowedDataViews[dataView];

            ViewBag.Title = "";
            if (dataViewObject != null) ViewBag.Title = dataViewObject.name;
            ViewBag.DataView = dataView;

            List<Job> jobList = Job.getList(database, dataViewObject);


            return View(jobList);
        }

        public ActionResult Details(string id)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            string jobNo = id;

            if (jobNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = Job.getEntry(database, jobNo);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

 

        public JsonResult getOpenCases(string id)
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            DataView dataView = new DataView();
            dataView.query = "[Case Type Code] = 'ÄRENDE'";
            dataView.orderBy = "[Received Date] DESC";

            List<Case> caseList = Case.getList(database, id, "", "", dataView);
            return Json(caseList, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getClosedCases(string id)
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            DataView dataView = new DataView();
            dataView.query = "[Case Type Code] = 'SLUTFÖRT'";
            dataView.orderBy = "[Received Date] DESC";

            List<Case> caseList = Case.getList(database, id, "", "", dataView);
            return Json(caseList, JsonRequestBehavior.AllowGet);


        }
    }

}