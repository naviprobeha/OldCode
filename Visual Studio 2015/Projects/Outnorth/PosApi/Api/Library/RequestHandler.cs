using Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Api.Library
{
    public class RequestHandler
    {

        public static void PushInventories(List<Inventory> inventoryList)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(inventoryList);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebRequest webRequest = HttpWebRequest.Create("https://node2.anyware.se/erpapi/inventories");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", "22EE6925D781848D392299871319BCBA5D247320B766197CEC24DCE4A5CEA78E");

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.WriteLine(json);
            streamWriter.Flush();
            streamWriter.Close();

            WebResponse response = webRequest.GetResponse();

        }

        public static void PushProducts(List<Product> productList)
        {

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(productList);

            WebRequest webRequest = HttpWebRequest.Create("https://node2.anyware.se/erpapi/products");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", "22EE6925D781848D392299871319BCBA5D247320B766197CEC24DCE4A5CEA78E");


            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.WriteLine(json);
            streamWriter.Flush();
            streamWriter.Close();

            WebResponse response = webRequest.GetResponse();

        }
    }
}