using Navipro.Backoffice.Web.Lib;
using Navipro.Backoffice.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Navipro.Backoffice.Web.Controllers
{
    public class PhoneLogController : BaseController
    {
        // GET: PhoneLog
        public ActionResult Index(string responseGroupUri)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today;
            if ((Request["fromDate"] != "") && (Request["fromDate"] != null)) fromDate = DateTime.Parse(Request["fromDate"]);
            if ((Request["toDate"] != "") && (Request["toDate"] != null)) toDate = DateTime.Parse(Request["toDate"]);

            ViewBag.responseGroups = User.profile.responseGroups;
            ViewBag.customerList = Customer.getSelectList(database);
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            ViewBag.responseGroupUri = responseGroupUri;

            return View();
        }

        
        public JsonResult ToggleHandled(string data)
        {
            if (!base.authenticationCheck()) return Json(new List<PhoneEntry>(), JsonRequestBehavior.AllowGet);

            string[] dataEntries = data.Split('|');
            DateTime alertDateTime = DateTime.Parse(dataEntries[0]);
            
            PhoneEntry phoneEntry = new PhoneEntry();
            phoneEntry.alertDateTime = alertDateTime;
            phoneEntry.fromUri = dataEntries[1];
            phoneEntry.responseGroupUri = dataEntries[2];
            phoneEntry.answeredByUri = base.User.user.email;

            phoneEntry.submitToggleHandled(database);

            return Json(new List<PhoneEntry>(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult getPhoneLog(string responseGroupUri, DateTime fromDate, DateTime toDate)
        {
            if (!base.authenticationCheck()) return Json(new List<PhoneEntry>(), JsonRequestBehavior.AllowGet);

            Database cdrDatabase = new Database(database.configuration, database.configuration.cdrConnectionString);

            List<PhoneEntry> phoneLogEntries = PhoneEntry.getPhoneCalls(database, cdrDatabase, responseGroupUri, fromDate, toDate);

            cdrDatabase.close();

            return Json(phoneLogEntries, JsonRequestBehavior.AllowGet);


        }

        public JsonResult countMissedCalls()
        {
            if (!base.authenticationCheck()) return Json(0, JsonRequestBehavior.AllowGet);

            //DateTime dateTime = new DateTime(2017, 4, 13);
            DateTime dateTime = DateTime.Today;

            Database cdrDatabase = new Database(database.configuration, database.configuration.cdrConnectionString);

            int count = 0;
            foreach (ResponseGroup responseGroup in User.profile.responseGroups)
            {
                if (responseGroup.primary == true)
                {
                    count = count + PhoneEntry.countMissedCalls(database, cdrDatabase, responseGroup.uri, dateTime, dateTime);
                }
            }

            cdrDatabase.close();

            return Json(count, JsonRequestBehavior.AllowGet);


        }
    }
}