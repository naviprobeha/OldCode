using Navipro.Backoffice.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navipro.Backoffice.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            base.User.profile.updateIndicators(database);
            base.User.profile.updateChart(database);

            ViewBag.indicators = base.User.profile.indicators;
            ViewBag.todoCaption = base.User.profile.todoCaption;
            ViewBag.todoList = Navipro.Backoffice.Web.Models.Case.getList(database, "", "", "", base.User.profile.todoDataView);
            ViewBag.chart1 = base.User.profile.chart;
            ViewBag.table = base.User.profile.getTable(database);
            ViewBag.tableCaption = base.User.profile.tableCaption;


            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchQuery)
        {
            if (!base.authenticationCheck()) return base.getLoginView();

            DateTime startTime = DateTime.Now;

            ViewBag.searchQuery = searchQuery;

            List<SearchResult> searchResults = new List<SearchResult>();

            Customer.search(database, searchQuery, ref searchResults);
            Job.search(database, searchQuery, ref searchResults);
            Case.search(database, searchQuery, ref searchResults);

            DateTime endTime = DateTime.Now;

            TimeSpan duration = endTime.Subtract(startTime);
            ViewBag.duration = String.Format("{0}.{1}", duration.Seconds, duration.Milliseconds);

            return View(searchResults);
        }
    }
}