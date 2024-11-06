using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Navipro.Backoffice.Web.Models;

namespace Navipro.Backoffice.Web.Controllers
{
    public class TimeReportController : BaseController
    {
        // GET: TimeReport
        public ActionResult Index()
        {
            if (!base.authenticationCheck()) return base.getLoginView();
            //if (base.User.user.resourceNo == "") return base.getLoginView();


            ViewBag.Title = "Tidrapportering";

            DataView myCasesDataView = User.profile.allowedDataViews["myCases"];

            ViewBag.timeReportList = TimeReportEntry.getList(database, base.User.user.resourceNo, DateTime.Today);
            ViewBag.caseList = Case.getList(database, "", "", "", myCasesDataView);
            ViewBag.activityTemplateList = ActivityTemplate.getList(database, User.user.resourceNo);

            return View();
        }

        public JsonResult getTimeReportList(string date)
        {
            if (!base.authenticationCheck()) return Json(new List<TimeReportEntry>(), JsonRequestBehavior.AllowGet);


            List<TimeReportEntry> timeReportList = TimeReportEntry.getList(database, this.User.user.resourceNo, DateTime.Parse(date));
            return Json(timeReportList, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getTimeReportTotals(string date)
        {
            if (!base.authenticationCheck()) return Json(new List<TimeReportEntry>(), JsonRequestBehavior.AllowGet);

            string[] totals = new string[3];

            totals[0] = TimeReportEntry.getReportedTotals(database, User.user.resourceNo, DateTime.Parse(date));
            totals[1] = TimeReportEntry.getVerifiedTotals(database, User.user.resourceNo, DateTime.Parse(date));
            totals[2] = TimeReportEntry.getPostedTotals(database, User.user.resourceNo, DateTime.Parse(date));

            return Json(totals, JsonRequestBehavior.AllowGet); ;
        }


        public JsonResult saveTimeReportQuantity(string date, int entryNo, decimal quantity, decimal realQuantity, decimal unitPrice)
        {
            if (!base.authenticationCheck()) return Json("Autentisering misslyckades.", JsonRequestBehavior.AllowGet);

            try
            {
                TimeReportEntry timeReportEntry = TimeReportEntry.getEntry(database, User.user.resourceNo, DateTime.Parse(date), entryNo);
                if (timeReportEntry != null)
                {
                    timeReportEntry.quantity = quantity;
                    timeReportEntry.realQuantity = realQuantity;
                    timeReportEntry.unitPrice = unitPrice;

                    timeReportEntry.submitQuanties(database);

                    return Json("OK", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e)
            {
                return Json("Felmeddelande: "+e.Message, JsonRequestBehavior.AllowGet);
            }

            return Json("Felaktig tidrapport.");

        }

        public JsonResult addTemplateToTimeReport(string date, string templateCode, decimal quantity, decimal realQuantity)
        {
            if (!base.authenticationCheck()) return Json("Autentisering misslyckades.", JsonRequestBehavior.AllowGet);

            try
            {
                TimeReportEntry timeReportEntry = new TimeReportEntry();
                timeReportEntry.resourceNo = User.user.resourceNo;
                timeReportEntry.date = DateTime.Parse(date);
                timeReportEntry.no = templateCode;
                timeReportEntry.quantity = quantity;
                timeReportEntry.realQuantity = realQuantity;

                timeReportEntry.submitTemplate(database);

                return Json("OK", JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception e)
            {
                return Json("Felmeddelande: " + e.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult addCaseToTimeReport(string date, string caseNo, string comment)
        {
            if (!base.authenticationCheck()) return Json("Autentisering misslyckades.", JsonRequestBehavior.AllowGet);

            try
            {
                Case caseItem = Case.getEntry(database, caseNo);
                caseItem.submitCreateComment(database, User.user, comment, caseItem.caseStatusCode, DateTime.Parse(date), true);

                return Json("OK", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("Felmeddelande: " + e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult verifyTimeReport(string date)
        {
            if (!base.authenticationCheck()) return Json("Autentisering misslyckades.", JsonRequestBehavior.AllowGet);

            try
            {
                TimeReportEntry timeReportEntry = new TimeReportEntry();
                timeReportEntry.resourceNo = User.user.resourceNo;
                timeReportEntry.date = DateTime.Parse(date);

                timeReportEntry.submitVerify(database);

                return Json(TimeReportEntry.getVerifyMessage(database, timeReportEntry.resourceNo, timeReportEntry.date), JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("Felmeddelande: " + e.Message, JsonRequestBehavior.AllowGet);
            }

        }
    }
}