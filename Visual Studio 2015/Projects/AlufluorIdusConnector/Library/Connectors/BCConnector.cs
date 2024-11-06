using NaviPro.Alufluor.Idus.Library.Helpers;
using NaviPro.Alufluor.Idus.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NaviPro.Alufluor.Idus.Library.Connectors
{
    public class BCConnector
    {
        private Configuration configuration;
        private Logger logger;

        public BCConnector(Configuration configuration, Logger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public List<BCDimension> GetDimensions()
        {
            logger.write("INFO", "Requesting dimensions from BC...");
            string jsonContent = makeGetRequest("Dimensions");
            
            BCDimensionResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<BCDimensionResponse>(jsonContent);

            logger.write("INFO", "Dimension response: " + response.value.Count.ToString());
            return response.value;
        }

        public List<BCDimensionValue> GetDimensionValues()
        {
            logger.write("INFO", "Requesting dimension values from BC...");
            string jsonContent = makeGetRequest("DimensionValues");

            BCDimensionValueResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<BCDimensionValueResponse>(jsonContent);

            logger.write("INFO", "Dimension Value response from BC: " + response.value.Count.ToString());
            return response.value;
        }

        public List<BCGLAccount> GetGLAccounts()
        {
            logger.write("INFO", "Requesting gl accounts from BC...");
            string jsonContent = makeGetRequest("GLAccounts");

            BCGLAccountResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<BCGLAccountResponse>(jsonContent);

            logger.write("INFO", "GL Account response: " + response.value.Count.ToString());
            return response.value;
        }


        public void PushPurchaseList(List<BCPurchaseLine> bcList)
        {
            logger.write("INFO", "Pushing purchase list to BC...");

            string xmlResponse = makeSoapRequest("pushPurchaseList", Newtonsoft.Json.JsonConvert.SerializeObject(bcList));

            logger.write("INFO", "Push Purchase List response: " + xmlResponse);
        }




        public string makeGetRequest(string method)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(configuration.bcOdataUrl + "/" + method);
            webRequest.Credentials = new NetworkCredential(configuration.bcUserName, configuration.bcPassword);
            webRequest.Accept = "application/json";

            webRequest.Method = "GET";

            try
            {
                WebResponse response = webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string jsonContent = streamReader.ReadToEnd();
                return jsonContent;
            }
            catch(Exception e)
            {
                logger.write("ERROR", "BC MakeGetRequest: " + e.Message);
            }

            return "";
        }


        public string makePostRequest(string method, string payload)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(configuration.bcOdataUrl + "/" + method);
            webRequest.Credentials = new NetworkCredential(configuration.bcUserName, configuration.bcPassword);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Accept = "application/json";

            try
            {
                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.WriteLine(payload);
                streamWriter.Flush();
                streamWriter.Close();

                WebResponse response = webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string jsonContent = streamReader.ReadToEnd();
                return jsonContent;
            }
            catch (Exception e)
            {
                logger.write("ERROR", "BC MakePostRequest: " + e.Message);
            }

            return "";
        }

        public string makeSoapRequest(string method, string payload)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(configuration.bcSoapUrl);
            webRequest.Credentials = new NetworkCredential(configuration.bcUserName, configuration.bcPassword);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml; charset=utf-8";

            webRequest.Headers.Add("SOAPAction", "PerformService");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement bodyElement = XmlHelper.AddElement(ref docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement methodElement = XmlHelper.AddElement(ref bodyElement, "PerformService", "", "urn:microsoft-dynamics-schemas/codeunit/IdusServiceHandler");
            XmlHelper.AddElement(ref methodElement, "serviceName", method, "urn:microsoft-dynamics-schemas/codeunit/IdusServiceHandler");
            XmlHelper.AddElement(ref methodElement, "payload", payload, "urn:microsoft-dynamics-schemas/codeunit/IdusServiceHandler");

            try
            {
                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.WriteLine(xmlDoc.OuterXml);
                streamWriter.Flush();
                streamWriter.Close();

                WebResponse response = webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string jsonContent = streamReader.ReadToEnd();
                return jsonContent;
            }
            catch (WebException e)
            {
                logger.write("ERROR", "BC MakeSoapRequest: " + e.Message);

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
    }
}
