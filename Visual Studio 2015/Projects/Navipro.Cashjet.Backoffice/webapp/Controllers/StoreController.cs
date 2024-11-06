using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class StoreController : BaseController
    {
        // GET: Store
        public ActionResult Index()
        {
            AuthenticationCheck();
            List<Store> storeList = Store.getList(systemDatabase, CurrentEnvironment);

            return View(storeList);
        }

        public JsonResult getStoreList()
        {
            AuthenticationCheck();

            List<Store> storeList = Store.getList(systemDatabase, CurrentEnvironment);
            return Json(storeList, JsonRequestBehavior.AllowGet);
        }
    }
}