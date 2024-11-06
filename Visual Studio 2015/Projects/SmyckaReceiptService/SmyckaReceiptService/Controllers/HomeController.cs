using SmyckaReceiptService.Handlers;
using SmyckaReceiptService.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmyckaReceiptService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public ActionResult Index(string id)
        {
            Configuration configuration = new Configuration();
            configuration.serverName = System.Configuration.ConfigurationManager.AppSettings["ServerName"];
            configuration.port = System.Configuration.ConfigurationManager.AppSettings["Port"];
            configuration.serverInstance = System.Configuration.ConfigurationManager.AppSettings["ServerInstance"];
            configuration.companyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            configuration.userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            configuration.password = System.Configuration.ConfigurationManager.AppSettings["Password"];


            POSTransactionHeader posTransactionHeader = RequestHandler.GetReceipt(configuration, id);

            ViewBag.ID = id;

            ViewBag.ShowRegistrationForm = true;
            if (!posTransactionHeader.newCustomer) ViewBag.ShowRegistrationForm = false;


            ViewBag.FormFieldList = createForm();
            ViewBag.PageTitle = "Kvitto";
            ViewBag.FormTitle = "Tack för ditt besök!";
            ViewBag.FormHelpText = "Genom att registrera dig nedan så erhåller du kommande köp samt vårt nyhetsbrev per e-post.";
            ViewBag.FormSubmitButton = "Spara mina uppgifter";

            if ((string)TempData["Registered"] == "TRUE") ViewBag.ShowRegistrationForm = false;


            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("{id}")]
        public ActionResult Index(string id, string name, string phoneNo, string email, string approve)
        {
            Configuration configuration = new Configuration();
            configuration.serverName = System.Configuration.ConfigurationManager.AppSettings["ServerName"];
            configuration.port = System.Configuration.ConfigurationManager.AppSettings["Port"];
            configuration.serverInstance = System.Configuration.ConfigurationManager.AppSettings["ServerInstance"];
            configuration.companyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            configuration.userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            configuration.password = System.Configuration.ConfigurationManager.AppSettings["Password"];

            RequestHandler.CreateCustomer(configuration, id, name, phoneNo, email);


            TempData["Registered"] = "TRUE";



            return RedirectToAction("index", new object[] { id });
        }

        [AllowAnonymous]
        [Route("{controller}/{action}/{id}")]
        public ActionResult ReceiptImage(string id)
        {
               

    

            return File("C:\\temp\\receipts\\" + id + ".jpg", "image/jpeg");
        }



        private List<FormField> createForm()
        {
            List<FormField> list = new List<FormField>();

            list.Add(new FormField {
                code = "name",
                description = "Namn",
                type = 0,
                required = true
            });
            list.Add(new FormField
            {
                code = "phoneNo",
                description = "Telefonnr",
                type = 0,
                required = true
            });
            list.Add(new FormField
            {
                code = "email",
                description = "E-post",
                type = 0,
                required = true,
                connectionField = "email"
                
            });
            list.Add(new FormField
            {
                code = "approve",
                description = "Jag godkänner integritetspolicyn och är införstådd med att Smycka lagrar mina personuppgifter.",
                type = 2,
                required = true
            });


            return list;
        }
    }
}