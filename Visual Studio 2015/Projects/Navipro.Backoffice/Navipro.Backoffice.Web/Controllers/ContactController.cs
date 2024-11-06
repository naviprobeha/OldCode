using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Navipro.Backoffice.Web.Models;
using System.Net;

namespace Navipro.Backoffice.Web.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Contact
        public ActionResult List(string dataView = "")
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            if (dataView == "") dataView = base.User.profile.defaultDataViews["contact"].code;

            DataView dataViewObject = null;

            dataViewObject = User.profile.allowedDataViews[dataView];

            ViewBag.Title = "";
            if (dataViewObject != null) ViewBag.Title = dataViewObject.name;
            ViewBag.DataView = dataView;

            ViewBag.customerList = Customer.getList(database, null);

            return View();
        }

        public ActionResult Details(string email)
        {
            if (!base.authenticationCheck()) return base.getLoginView();


            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseMember caseMember = CaseMember.getEntry(database, email);
            if (caseMember == null)
            {
                return HttpNotFound();
            }

            Customer customer = Customer.getEntry(database, caseMember.customerNo);
            if (customer != null)
            {
                caseMember.customerName = customer.name;
            }
            Job job = Job.getEntry(database, caseMember.defaultJobNo);
            if (job != null)
            {
                caseMember.defaultJobDescription = job.description;
            }

            return View(caseMember);
        }

        [HttpPost]
        public ActionResult Details(CaseMember caseMember)
        {
            if (!base.authenticationCheck()) return base.getLoginView();


            if (caseMember == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Request["mode"] == "deleteCaseMember")
            {
                CaseMember dbCaseMember = CaseMember.getEntry(database, caseMember.email);
                dbCaseMember.submitDelete(database, base.User.user);
                return RedirectToAction("List", "Contact");
            }

            return View(caseMember);
        }

        public ActionResult Edit(string email)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CaseMember caseMember = CaseMember.getEntry(database, email);
            if (caseMember == null)
            {
                return HttpNotFound();
            }

            ViewBag.jobList = Job.getSelectList(database);
            ViewBag.customerList = Customer.getSelectList(database);

            return View(caseMember);
        }

        [HttpPost]
        public ActionResult Edit(CaseMember caseMember)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            CaseMember dbCaseMember = CaseMember.getEntry(database, caseMember.email);

            try
            {
                dbCaseMember.name = caseMember.name;
                dbCaseMember.phoneNo = caseMember.phoneNo;
                dbCaseMember.cellPhoneNo = caseMember.cellPhoneNo;
                dbCaseMember.customerNo = caseMember.customerNo;
                dbCaseMember.defaultJobNo = caseMember.defaultJobNo;

                dbCaseMember.submitUpdate(database, base.User.user);
                return RedirectToAction("Details", "Contact", new { email = dbCaseMember.email });
            }
            catch (Exception e)
            {
                ViewBag.errorMessage = e.Message;

                ViewBag.jobList = Job.getSelectList(database);
                ViewBag.customerList = Customer.getSelectList(database);

            }
            return View(dbCaseMember);
        }


        public JsonResult getContactList(string customerNoFilter, string dataView)
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            DataView dataViewObject = User.profile.allowedDataViews[dataView];

            Dictionary<string, Customer> customerTable = Customer.getDictionary(database);
            List<CaseMember> caseMemberList = CaseMember.getList(database, customerNoFilter, dataViewObject);

            caseMemberList = CaseMember.applyCustomerInfo(caseMemberList, customerTable);

            return Json(caseMemberList, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getOpenCases(string email)
        {
            if (!base.authenticationCheck()) return Json(new List<Case>(), JsonRequestBehavior.AllowGet);

            DataView dataView = new DataView();
            dataView.query = "[Case Type Code] = 'ÄRENDE' AND [Orderer E-mail] = '"+email.Replace("'", "")+"'";
            dataView.orderBy = "[Received Date] DESC";

            List<Case> caseList = Case.getList(database, "", "", "", dataView);
            return Json(caseList, JsonRequestBehavior.AllowGet);


        }

        public JsonResult createContact(string email, string name, string phoneNo, string cellPhoneNo, string customerNo)
        {
            if (!base.authenticationCheck()) return Json(false, JsonRequestBehavior.AllowGet);

            CaseMember caseMember = CaseMember.getEntry(database, email);
            if (caseMember != null) return Json(false, JsonRequestBehavior.AllowGet);

            caseMember = new CaseMember();
            caseMember.email = email;
            caseMember.name = name;
            caseMember.phoneNo = phoneNo;
            caseMember.cellPhoneNo = cellPhoneNo;
            caseMember.customerNo = customerNo;


            caseMember.submitCreate(database, base.User.user);

            return Json(true, JsonRequestBehavior.AllowGet);

        }
    }
}