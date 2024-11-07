using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using LicenseService.Data;
using LicenseService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace LicenseService.Controllers
{
    
    public class ERPApiController : Controller
    {
        private SystemDatabase systemDatabase;

        public ERPApiController(SystemDatabase systemDatabase)
        {
            this.systemDatabase = systemDatabase;

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Products()
        {
            if (!CheckHeader())
            {
                Response.StatusCode = 403;
                HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Illegal token.";
                return Json("Unauthorized");
            }

            return Json(systemDatabase.Products.ToList());

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Product(string id)
        {
            if (!CheckHeader())
            {
                Response.StatusCode = 403;
                HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Illegal token.";
                return Json("Unauthorized");
            }

            return Json(systemDatabase.Products.FirstOrDefault(p => p.id == id));

        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Product()
        {
            if (!CheckHeader())
            {
                Response.StatusCode = 403;
                HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Illegal token.";
                return Json("Unauthorized");
            }

            StreamReader streamReader = new StreamReader(Request.Body);
            string jsonContent = streamReader.ReadToEnd();

            Product product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(jsonContent);

            Product dbProduct = systemDatabase.Products.FirstOrDefault(p => p.id == product.id);
            if (dbProduct == null)
            {
                systemDatabase.Products.Add(product);
            }
            else
            {
                dbProduct.FromProduct(product);

                systemDatabase.Entry(dbProduct).State = EntityState.Modified;
            }

            systemDatabase.SaveChanges();

            return Json(product);

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Customers()
        {
            if (!CheckHeader())
            {
                Response.StatusCode = 403;
                HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Illegal token.";
                return Json("Unauthorized");
            }

            return Json(systemDatabase.Customers.ToList());

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Customer(string id)
        {
            if (!CheckHeader())
            {
                Response.StatusCode = 403;
                HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Illegal token.";
                return Json("Unauthorized");
            }

            Customer customer = systemDatabase.Customers.FirstOrDefault(c => c.id == id);
            if (customer != null)
            {
                customer.applySubscriptions(systemDatabase);
            }

            return Json(customer);

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Subscription(string id)
        {
            if (!CheckHeader())
            {
                Response.StatusCode = 403;
                HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Illegal token.";
                return Json("Unauthorized");
            }

            Subscription subscription = systemDatabase.Subscriptions.FirstOrDefault(c => c.id == id);
            if (subscription != null)
            {

                subscription.customer = systemDatabase.Customers.FirstOrDefault(c => c.id == subscription.customerId);
                subscription.product = systemDatabase.Products.FirstOrDefault(c => c.id == subscription.productId);
            }

            return Json(subscription);

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult SubscriptionsByStatus(string statusCode)
        {
            if (!CheckHeader())
            {
                Response.StatusCode = 403;
                HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Illegal token.";
                return Json("Unauthorized");
            }

            List<Product> productList = systemDatabase.Products.ToList();
            List<Customer> customerList = systemDatabase.Customers.ToList();
            List<Subscription> subscriptionList = systemDatabase.Subscriptions.Where(c => c.statusCode == statusCode).ToList();

            int i = 0;
            while (i < subscriptionList.Count)
            {
                subscriptionList[i].customer = customerList.FirstOrDefault(c => c.id == subscriptionList[i].customerId);
                subscriptionList[i].product = productList.FirstOrDefault(c => c.id == subscriptionList[i].productId);

                i++;

            }

            return Json(subscriptionList);

        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SubscriptionStatus(string customerId, string productId, string environmentName, string companyName, string statusCode)
        {
            if (!CheckHeader())
            {
                Response.StatusCode = 403;
                HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Illegal token.";
                return Json("Unauthorized");
            }

            Subscription subscription = systemDatabase.Subscriptions.FirstOrDefault(c => c.customerId == customerId && c.productId == productId && c.environmentName == environmentName && c.companyName == companyName);
            if (subscription != null)
            {

                subscription.statusCode = statusCode;
                systemDatabase.Entry(subscription).State = EntityState.Modified;
                systemDatabase.SaveChanges();
            }

            return Json(subscription);

        }

        private bool CheckHeader()
        {
            string token = Request.Headers["Authorization"];
            if (token == "TheQu1ckBr0wnF0xJumpsOverTheLazyDoG!") return true;

            return false;
        }
    }
}