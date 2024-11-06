using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Navipro.Backoffice.Web.Models;
using System.Net;

namespace Navipro.Backoffice.Web.Controllers
{
    public class CustomerController : BaseController
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

            List<Customer> customerList = Customer.getList(database, dataViewObject);

            
            return View(customerList);
        }

        public ActionResult Details(string id)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            string customerNo = id;

            if (customerNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = Customer.getEntry(database, customerNo);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public JsonResult getAssetSiteComments(string id)
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            List<AssetSiteComment> assetSiteCommentList = AssetSiteComment.getList(database, id, Request.UserHostAddress);
            return Json(assetSiteCommentList, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getCaseMembers(string id)
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            List<CaseMember> caseMemberList = CaseMember.getList(database, id);
            return Json(caseMemberList, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getJobs(string id)
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            List<Job> jobList = Job.getList(database, id);
            return Json(jobList, JsonRequestBehavior.AllowGet);


        }
    }

}