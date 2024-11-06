using NaviPro.Alufluor.Idus.Library.Helpers;
using NaviPro.Alufluor.Idus.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace NaviPro.Alufluor.Idus.Library.Connectors
{
    public class IdusConnector
    {
        private Configuration configuration;
        private Logger logger;
        private static string pragmaHeader = "";

        public IdusConnector(Configuration configuration, Logger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public List<IdusAccount> getAccounts(string accountType)
        {
            string jsonContent = makeGetRequest("EXT_GetAccounts/" + accountType);

            IdusGetAccountResult response = Newtonsoft.Json.JsonConvert.DeserializeObject<IdusGetAccountResult>(jsonContent);
            if (response != null)
            {

                if (response.result.Count > 0)
                {
                    if (response.result[0].fields.FItems.Count > 0)
                    {
                        logger.write("INFO", "No of accounts retrieved from Idus: " + response.result[0].fields.FItems.Count);
                        return response.result[0].fields.FItems;
                    }
                }
            }

            return null;
        }

        public IdusAccount getAccount(string accountNo)
        {
            string jsonContent = makeGetRequest("EXT_GetAccount/" + accountNo);
            

            IdusGetAccountResult response = Newtonsoft.Json.JsonConvert.DeserializeObject<IdusGetAccountResult>(jsonContent);
            if (response != null)
            {
                if (response.result.Count > 0)
                {
                    if (response.result[0].fields.FItems.Count > 0)
                    {
                        return response.result[0].fields.FItems[0];
                    }
                }
            }

            return null;
        }

        public void createAccount(IdusAccount account)
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(account, Newtonsoft.Json.Formatting.None, settings);

            makePostRequest("EXT_AddAccount", jsonContent);

        }

        public void updateAccount(IdusAccount account)
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(account, Newtonsoft.Json.Formatting.None, settings);

            makePostRequest("EXT_UpdateAccount", jsonContent);

        }

        public List<IdusPurchase> getPurchaseList(DateTime fromDate, DateTime toDate)
        {
            string jsonContent = makeGetRequest("EXT_GetPurchase/DATUM-INTERVAL=" + fromDate.ToString("yyyy-MM-dd")+"T00:00:00.000+"+toDate.ToString("yyyy-MM-dd")+"T00:00:00.000");
            if (jsonContent == "") return null;

            IdusGetPurchaseResult response = Newtonsoft.Json.JsonConvert.DeserializeObject<IdusGetPurchaseResult>(jsonContent);
            if (response.result.Count > 0)
            {
                return response.result[0].fields.FItems;
            }

            return null;
        }

        public string makeGetRequest(string method)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(configuration.idusUrl + "/" + method);
            webRequest.Method = "GET";
            webRequest.UseDefaultCredentials = false;
            webRequest.Headers.Add("IdusAuthorization", "Basic " + Base64Encode(configuration.idusUserName + ":" + configuration.idusPassword));
            webRequest.Headers.Add("Authorization", "Basic " + Base64Encode(configuration.idusApiUserName +":"+ ""));

            Console.WriteLine("Idus auth: " + Base64Encode(configuration.idusUserName + ":" + configuration.idusPassword));
            Console.WriteLine("Auth: " + Base64Encode(configuration.idusApiUserName + ":" + ""));
            //webRequest.Headers.Add("IdusAuthorization", "Basic QkNDT006RmFubnk5MiE=");
            //webRequest.Headers.Add("Authorization", "Basic QVBJVVNFUl9TWU9DX01JSUcxOg==");
            if (pragmaHeader != "") webRequest.Headers.Add("Pragma", pragmaHeader);


            try
            {
                WebResponse response = webRequest.GetResponse();
                pragmaHeader = response.Headers.Get("Pragma");
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string jsonContent = streamReader.ReadToEnd();
                streamReader.Close();
                return jsonContent;
            }
            catch (WebException e)
            {
                logger.write("ERROR", "Idus MakeGetRequest: " + e.Message);

                if (e.Response != null)
                {
                    StreamReader streamReader = new StreamReader(e.Response.GetResponseStream());
                    string errorBody = streamReader.ReadToEnd();
                    streamReader.Close();

                    logger.write("ERROR", errorBody);

                }
            }

            return "";
        }

        public string makePostRequest(string method, string payload)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(configuration.idusUrl + "/\"" + method+"\"");          
            webRequest.Method = "POST";
            webRequest.UseDefaultCredentials = false;
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("IdusAuthorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(configuration.idusUserName + ":" + configuration.idusPassword)));
            webRequest.Headers.Add("Authorization", "Basic " + Base64Encode(configuration.idusApiUserName + ":" + ""));
            if (pragmaHeader != "") webRequest.Headers.Add("Pragma", pragmaHeader);

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.Write(payload);
            streamWriter.Flush();
            streamWriter.Close();

            try
            {
                WebResponse response = webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string jsonContent = streamReader.ReadToEnd();
                return jsonContent;
            }
            catch (WebException e)
            {
                logger.write("ERROR", "Idus MakePostRequest: " + e.Message);

                if (e.Response != null)
                {
                    StreamReader streamReader = new StreamReader(e.Response.GetResponseStream());
                    string errorBody = streamReader.ReadToEnd();
                    streamReader.Close();

                    logger.write("ERROR", errorBody);

                }
            }

            return "";
        }

        public string Base64Encode(string textToEncode)
        {
            byte[] textAsBytes = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(textAsBytes);
        }
    }
}
