using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Net;
using System.Xml;

namespace Navipro.Cashjet.PaymentModules.AirPay
{
    public class Server : rti.IRTI_1Events
    {
        private rti.RTI_1Class rti;
        private UCOMIConnectionPointContainer icpc;
        private UCOMIConnectionPoint icp;
        private int cookie;

        private string navUrl;
        private string navUserName;
        private string navPassword;
        private Logger _logger;


        public Server(Logger logger)
        {
            _logger = logger;
            readConfiguration();

            rti = new rti.RTI_1Class();
            Guid IID_ICoBpTiEvents = typeof(rti.IRTI_1Events).GUID;

            icpc = (UCOMIConnectionPointContainer)rti;
            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            icp.Advise(this, out cookie);

            rti.init("");

        }

        public void start()
        {
            rti.start();

        }

        public void stop()
        {
            rti.stop();
        }

        public string performService(string method, string xmlData)
        {
            string url = navUrl;
            string username = navUserName;
            string password = navPassword;


            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            NetworkCredential netCredentials = new NetworkCredential(username, password);
            httpWebRequest.Credentials = netCredentials;
            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            httpWebRequest.Method = "POST";
            //httpWebRequest.Timeout = 20;

            WebHeaderCollection webHeaderCollection = httpWebRequest.Headers;
            webHeaderCollection.Add("SOAPAction", "performService");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement bodyElement = null;
            XmlElement methodElement = null;
            XmlElement element = null;

            addElement(ref docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/", ref bodyElement);
            addElement(ref bodyElement, "PerformService", "", "urn:microsoft-dynamics-schemas/codeunit/POSClientWebService", ref methodElement);
            addElement(ref methodElement, "method", method, "urn:microsoft-dynamics-schemas/codeunit/POSClientWebService", ref element);
            addElement(ref methodElement, "xmlRecordData", "", "urn:microsoft-dynamics-schemas/codeunit/POSClientWebService", ref element);

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
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

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
                            XmlDocument responseDoc = new XmlDocument();

                            responseDoc.LoadXml(xmlText.Value);
                            docElement = responseDoc.DocumentElement;

                            element = (XmlElement)docElement.SelectSingleNode("data");
                            if (element != null)
                            {
                                if (element.FirstChild != null)
                                {
                                    XmlElement responseElement = (XmlElement)element.FirstChild;
                                    return responseElement.OuterXml;
                                }
                            }

                            element = (XmlElement)docElement.SelectSingleNode("status");

                            if (element.FirstChild.Value != "OK")
                            {
                                //throw error
                                return "<error>"+element.FirstChild.Value+"</error>";
                                
                            }
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                using (var stream = webException.Response.GetResponseStream())
                using (var reader = new System.IO.StreamReader(stream))
                {
                                return "<error>"+reader.ReadToEnd()+"</error>";               
                }

            }

            return "";
        }

        private void readConfiguration()
        {
            System.Xml.XmlDocument configDoc = new System.Xml.XmlDocument();
            configDoc.Load("C:\\CardPayment\\airpay.xml");

            System.Xml.XmlElement docElement = configDoc.DocumentElement;

            navUrl = getNodeValue(docElement, "navWebService");
            navUserName = getNodeValue(docElement, "navUserName");
            navPassword = getNodeValue(docElement, "navPassword");


        }

        public static void addElement(ref XmlElement parentElement, string nodeName, string nodeValue, string nameSpace, ref XmlElement childElement)
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

        public static string getNodeValue(XmlElement xmlElement, string nodeName)
        {
            XmlElement element = (XmlElement)xmlElement.SelectSingleNode(nodeName);
            if (element != null)
            {
                if (element.FirstChild != null)
                {
                    return element.FirstChild.Value;
                }
            }
            return "";
        }


        #region IRTI_1Events Members

        void rti.IRTI_1Events.requestEvent(ref string xml)
        {
            _logger.log("Incoming message: "+xml);
            string responseXml = performService("doAirPayCommand", xml);

            _logger.log("Response from NAV: " + responseXml);

            rti.response(responseXml);
        }

        #endregion
    }
}
