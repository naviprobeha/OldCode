using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class DeviceController : BaseController
    {
        // GET: Store
        public ActionResult Index()
        {
            AuthenticationCheck();
            List<Device> deviceList = Device.getList(systemDatabase, CurrentEnvironment);

            return View(deviceList);
        }

        public JsonResult getDeviceList()
        {
            AuthenticationCheck();

            List<Device> deviceList = Device.getList(systemDatabase, CurrentEnvironment);
            return Json(deviceList, JsonRequestBehavior.AllowGet);
        }
    }
}