using LicenseService.Data;
using LicenseService.Helpers;
using LicenseService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LicenseService.Controllers
{
    public class APPApiController : Controller
    {
        private SystemDatabase systemDatabase;
        private const string localToken = "09943944-683d-4c2d-b8c6-e50b3eb18d12";

        public APPApiController(SystemDatabase systemDatabase)
        {
            this.systemDatabase = systemDatabase;

        }

        [HttpGet]
        public JsonResult Customer(string id)
        {
            if (Request.Headers["Authorization"] != localToken)
            {
                return new JsonResult("");
            }

            Customer customer = systemDatabase.Customers.FirstOrDefault(c => c.id == id);
            
            return new JsonResult(customer);

        }

        [HttpGet]
        public JsonResult FindCustomer(string email)
        {
            if (Request.Headers["Authorization"] != localToken)
            {
                return new JsonResult("");
            }

            Customer customer = systemDatabase.Customers.FirstOrDefault(c => c.email.ToUpper() == email.ToUpper());

            return new JsonResult(customer);

        }


        [HttpPost]
        public JsonResult Customer()
        {
            if (Request.Headers["Authorization"] != localToken)
            {
                return new JsonResult("");
            }


            StreamReader streamReader = new StreamReader(Request.Body);
            string jsonContent = streamReader.ReadToEnd();

            Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(jsonContent);
            if ((customer.id == "") || (customer.id == null))
            {
                customer.id = Guid.NewGuid().ToString();
                customer.no = "";

                systemDatabase.Customers.Add(customer);

                CustomerHistory history = new CustomerHistory();
                history.FromCustomer(customer);

                systemDatabase.CustomerHistory.Add(history);
            }
            else
            {
                Customer dbCustomer = systemDatabase.Customers.FirstOrDefault(c => c.id == customer.id);
                if (dbCustomer != null)
                {
                    if (customer.Changed(dbCustomer))
                    {
                        dbCustomer.FromCustomer(customer);

                        systemDatabase.Entry(dbCustomer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        CustomerHistory history = new CustomerHistory();
                        history.FromCustomer(customer);

                        systemDatabase.CustomerHistory.Add(history);

                    }
                }
            }

            systemDatabase.SaveChanges();

            return new JsonResult(customer);
            
        }



        [HttpGet]
        public JsonResult Product(string id)
        {
            if (Request.Headers["Authorization"] != localToken)
            {
                return new JsonResult("");
            }

            Product product = systemDatabase.Products.FirstOrDefault(c => c.id == id);

            return new JsonResult(product);

        }

        [HttpGet]
        public JsonResult Subscriptions(string id)
        {
            if (Request.Headers["Authorization"] != localToken)
            {
                return new JsonResult("");
            }

            string customerId = id;

            List<Subscription> subscriptionList = systemDatabase.Subscriptions.Where(c => c.customerId == customerId).ToList();
            List<Product> productList = systemDatabase.Products.ToList();

            int i = 0;
            while (i < subscriptionList.Count)
            {
                subscriptionList[i].applyProduct(productList);
                i++;
            }

            return new JsonResult(subscriptionList);

        }

        [HttpGet]
        public JsonResult Subscription(string id, string productId)
        {
            if (Request.Headers["Authorization"] != localToken)
            {
                return new JsonResult("");
            }

            string customerId = id;

            Subscription subscription = systemDatabase.Subscriptions.FirstOrDefault(c => c.customerId == customerId && c.productId == productId);

            return new JsonResult(subscription);

        }

        [HttpPost]
        public JsonResult Subscription()
        {
            if (Request.Headers["Authorization"] != localToken)
            {
                return new JsonResult("");
            }

            StreamReader streamReader = new StreamReader(Request.Body);
            string jsonContent = streamReader.ReadToEnd();

            Subscription subscription = Newtonsoft.Json.JsonConvert.DeserializeObject<Subscription>(jsonContent);
            subscription.id = subscription.customerId+"-"+subscription.productId+"-"+subscription.environmentName+"-"+subscription.companyName;

            Product product = systemDatabase.Products.FirstOrDefault(p => p.id == subscription.productId);
            if (product != null)
            {
                if (subscription.type == 0) subscription.unitPrice = product.unitPricePerMonth;
                if (subscription.type == 1) subscription.unitPrice = product.unitPricePerYear;

                Subscription dbSubscription = systemDatabase.Subscriptions.FirstOrDefault(s => s.id == subscription.id);
                if (dbSubscription == null)
                {

                    systemDatabase.Subscriptions.Add(subscription);
                    systemDatabase.SaveChanges();

                }
            }

            return new JsonResult(subscription);

        }

        [HttpPost]
        public JsonResult Usage(string id)
        {
            if (Request.Headers["Authorization"] != localToken)
            {
                return new JsonResult("");
            }

            StreamReader streamReader = new StreamReader(Request.Body);
            string jsonContent = streamReader.ReadToEnd();

            List<Usage> usageList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usage>>(jsonContent);

            foreach (Usage usage in usageList)
            {
                usage.customerId = id;
                usage.id = usage.customerId + "-" + usage.environmentName + "-" + usage.productId + "-" + usage.userId + "-" + usage.applicationAreaCode;

                Usage usageDb = systemDatabase.Usage.FirstOrDefault(u => u.id == usage.id);
                if (usageDb != null)
                {
                    usageDb.FromUsage(usage);
                    systemDatabase.Entry(usageDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    systemDatabase.Usage.Add(usage);
                }

            }



            systemDatabase.SaveChanges();



            return new JsonResult("OK");

        }

        private void logToFile(string message)
        {
            StreamWriter writer = new StreamWriter("C:\\temp\\license.log", true);
            writer.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + message);
            writer.Flush();
            writer.Close();
        }
    }
}
