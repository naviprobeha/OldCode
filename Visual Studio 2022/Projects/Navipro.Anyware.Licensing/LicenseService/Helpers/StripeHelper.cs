using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LicenseService.Helpers
{
    public class StripeHelper
    {
        private string url = "https://api.stripe.com/v1/";
        //private string token = "sk_test_prktYMUMR6E9921fpmXPePO9002uoNt0xw";
        private string token = "rk_live_8uiiPpGiESUyNiFVTpRZTtpU0079ZtGe7j";

        public static string localToken = "Bearer 09943944-683d-4c2d-b8c6-e50b3eb18d12";

        public StripeHelper()
        {

        }


        public string makePostRequest(string method, string payload)
        {
            WebRequest webRequest = HttpWebRequest.Create(url + method);

            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.Write(payload);
            streamWriter.Flush();
            streamWriter.Close();

            log(url + method);
            log("Request: " + payload);

            try
            {
                WebResponse webResponse = webRequest.GetResponse();

                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = streamReader.ReadToEnd();
                streamReader.Close();

                log("Response: " + responseData);

                return responseData;
            }
            catch(WebException web)
            {

            }
            catch(Exception e)
            {

            }

            return "";
        }

        public string makeGetRequest(string method)
        {
            WebRequest webRequest = HttpWebRequest.Create(url + method);

            log(url + method);

            webRequest.Method = "GET";
            webRequest.Headers.Add("Authorization", "Bearer " + token);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            try
            {
                WebResponse webResponse = webRequest.GetResponse();

                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = streamReader.ReadToEnd();
                streamReader.Close();

                log("Response: "+responseData);

                return responseData;
            }
            catch (WebException web)
            {
                /*
                StreamReader streamReader = new StreamReader(web.Response.GetResponseStream());
                string data = streamReader.ReadToEnd();
                throw new Exception(data);
                */
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
            }

            return "";
        }


        private void log(string logMessage)
        {
            return; //No logging!
            StreamWriter writer = File.AppendText("C:\\temp\\license.log");
            writer.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]")+" "+logMessage);
            writer.Flush();
            writer.Close();

        }
    }
}
