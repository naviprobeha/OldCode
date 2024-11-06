using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;
using System.Configuration;

namespace Navipro.NAVWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://navipro.navwebservice/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceRequest : System.Web.Services.WebService
    {

        [WebMethod]
        public string performService(string method, string xmlData)
        {

            string url = ConfigurationManager.AppSettings["webServiceUrl"];
            string username = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];


            System.Net.WebRequest httpWebRequest = System.Net.HttpWebRequest.Create(url);
            System.Net.NetworkCredential netCredentials = new System.Net.NetworkCredential(username, password);
            httpWebRequest.Credentials = netCredentials;
            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;

            System.Net.WebHeaderCollection webHeaderCollection = httpWebRequest.Headers;
            webHeaderCollection.Add("SOAPAction", "performService");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement bodyElement = null;
            XmlElement methodElement = null;
            XmlElement element = null;

            addElement(ref docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/", ref bodyElement);
            addElement(ref bodyElement, "PerformService", "", "urn:microsoft-dynamics-schemas/codeunit/WebServiceRequest", ref methodElement);
            addElement(ref methodElement, "method", method, "urn:microsoft-dynamics-schemas/codeunit/WebServiceRequest", ref element);
            addElement(ref methodElement, "xmlData", "", "urn:microsoft-dynamics-schemas/codeunit/WebServiceRequest", ref element);

            if (xmlData != "")
            {
                XmlCDataSection xmlCData = xmlDoc.CreateCDataSection(xmlData);
                element.AppendChild(xmlCData);
            }

            //throw new Exception (xmlDoc.OuterXml);

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(httpWebRequest.GetRequestStream());
            streamWriter.Write(xmlDoc.OuterXml);
            streamWriter.Close();


            try
            {
                System.Net.HttpWebResponse httpWebResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();

                System.IO.StreamReader streamReader = new System.IO.StreamReader(httpWebResponse.GetResponseStream());


                XmlDocument responseSOAPDoc = new XmlDocument();
                responseSOAPDoc.LoadXml(streamReader.ReadToEnd());

                httpWebResponse.Close();


                XmlNodeList nodeList = responseSOAPDoc.GetElementsByTagName("return_value");
                if (nodeList.Count > 0)
                {
                    element = (XmlElement)nodeList.Item(0);
                    if (element != null)
                    {
                        XmlText xmlText = (XmlText)element.FirstChild;
                        if (xmlText != null)
                        {
                            return xmlText.Value;

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("WebException: "+e.Message);
            }

            return "";
        }

        private void addElement(ref XmlElement parentElement, string nodeName, string nodeValue, string nameSpace, ref XmlElement childElement)
        {
            XmlDocument xmlDoc = parentElement.OwnerDocument;
            childElement = xmlDoc.CreateElement(nodeName, nameSpace);
            if (nodeValue != "")
            {
                XmlText xmlText = xmlDoc.CreateTextNode(nodeValue);
                childElement.AppendChild(xmlText);
            }

            parentElement.AppendChild(childElement);

        }

    }

}
