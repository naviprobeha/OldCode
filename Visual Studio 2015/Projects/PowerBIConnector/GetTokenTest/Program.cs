using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace GetTokenTest
{
    class Program
    {

        private static string resourceUri = "https://api.businesscentral.dynamics.com/.default";


        static void Main(string[] args)
        {
            Console.WriteLine("Trying get token....");

            //Console.WriteLine("Token: " + NaviPro.OAuthHelper.GetBCAccessToken("navipro.se", "4f198e50-6632-4ae8-bdd5-445db23691b6", "w8m8Q~eRi9.7N5IaWOWtBVbSdTACT17fjYitZc69"));

            string accessToken = NaviPro.OAuthHelper.GetBCAccessToken("matildadjerf.onmicrosoft.com", "2d216749-0b7a-4a44-9197-2f454cb4c000", "6Ms8Q~t.g4kg-2dXkoWvJTntFuFTqDa-HQrC1dxk");
            Console.WriteLine("Token: " + accessToken);

            Newtonsoft.Json.Linq.JObject jObject = new Newtonsoft.Json.Linq.JObject();
            jObject.Add("method", "DeliveryReport");
            jObject.Add("integrationCode", "SHIPMONK");
            jObject.Add("payload", "{ \"data\": \"Hepp\" }");


            string response = NaviPro.OAuthHelper.MakeBCPostJsonRequest("https://api.businesscentral.dynamics.com/v2.0/matildadjerf.onmicrosoft.com/Test/ODataV4/ShipMonkMgt_ProcessRequest?company=Production", accessToken, jObject.ToString());

            Console.WriteLine("Response: "+ response);
            Console.ReadLine();
        }



 
    }
}
