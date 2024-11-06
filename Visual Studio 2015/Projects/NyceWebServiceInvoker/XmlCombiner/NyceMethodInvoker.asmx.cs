using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Net;
using System.Xml;
using System.Data.SqlClient;

namespace NyceMethodInvoker
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NyceMethodInvoker : System.Web.Services.WebService
    {

        [WebMethod]
        public bool Invoke(int logEntryNo, string url, string username, string password, string connectionString)
        {
            string xmlData = getLogEntryFromDatabase(connectionString, logEntryNo);
            string token = logon(url, username, password);
            if ((token == "") || (token == null)) return false;
            return sendData(url, token, xmlData, logEntryNo);
        }

        [WebMethod]
        public bool TryPausOrder(string url, string username, string password, string connectionString, string customerOrderNo)
        {
            string token = pausOrderLogon(url, username, password);
            if ((token == "") || (token == null)) return false;
            return pausOrder(url, token, customerOrderNo);
        }

        public bool sendData(string url, string token, string xmlData, int logEntryNo)
        {
            WebRequest httpWebRequest = HttpWebRequest.Create(url);

            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 400000;

            WebHeaderCollection webHeaderCollection = httpWebRequest.Headers;
            webHeaderCollection.Add("SOAPAction", "http://tempuri.org/WSINTEGRATION/Import");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\" />");
            XmlElement docElement = xmlDoc.DocumentElement;
            XmlElement bodyElement = null;
            XmlElement methodElement = null;
            XmlElement element = null;
            
            addElement(ref docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/", ref bodyElement);
            addElement(ref bodyElement, "tem:Import", "", "http://tempuri.org/", ref methodElement);
            addElement(ref methodElement, "tem:ticket", token, "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:File", "", "http://tempuri.org/", ref element);
            
            if (xmlData != "")
            {
                XmlCDataSection xmlCData = xmlDoc.CreateCDataSection(xmlData);
                element.AppendChild(xmlCData);
            }

            addElement(ref methodElement, "tem:Identifier", "1", "http://tempuri.org/", ref element);


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

                //responseSOAPDoc.Save("C:\\temp\\nyce_general_response.xml");

                XmlNamespaceManager nsMgr = new XmlNamespaceManager(responseSOAPDoc.NameTable);
                nsMgr.AddNamespace("a", "http://schemas.datacontract.org/2004/07/Nyce.Logic.Middleware.WebServices");

                XmlNodeList responseNodeList = responseSOAPDoc.GetElementsByTagName("WsIntegrationMessageLog", "http://schemas.datacontract.org/2004/07/Nyce.Logic.Middleware.WebServices");

                if (responseNodeList.Count == 0) throw new Exception("Oväntat fel");
                
                foreach (XmlNode log in responseNodeList)
                {
                    XmlElement severity = (XmlElement)((XmlElement)log).SelectSingleNode("a:Severity", nsMgr);

                    if (severity != null)
                    {
                        if (severity.FirstChild != null)
                        {
                            if (severity.FirstChild.Value == "0")
                            {
                                responseSOAPDoc.Save("C:\\temp\\nyce_log_entry_"+logEntryNo+".xml");
                                return false;
                            }
                        }
                    }

                }

                return true;
            }
            catch (WebException e)
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(e.Response.GetResponseStream());
                XmlDocument responseSOAPDoc = new XmlDocument();
                responseSOAPDoc.LoadXml(streamReader.ReadToEnd());

                HttpContext.Current.Response.Write(responseSOAPDoc.OuterXml);
                HttpContext.Current.Response.End();

            }

            return false;
        }

        public string logon(string url, string username, string password)
        {
            WebRequest httpWebRequest = HttpWebRequest.Create(url);

            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;

            WebHeaderCollection webHeaderCollection = httpWebRequest.Headers;
            webHeaderCollection.Add("SOAPAction", "http://tempuri.org/WSINTEGRATION/Logon");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\" />");
            XmlElement docElement = xmlDoc.DocumentElement;
            XmlElement bodyElement = null;
            XmlElement methodElement = null;
            XmlElement element = null;

            addElement(ref docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/", ref bodyElement);
            addElement(ref bodyElement, "tem:Logon", "", "http://tempuri.org/", ref methodElement);
            addElement(ref methodElement, "tem:userName", username, "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:password", password, "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:language", "SWE", "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:clientGroupId", "0", "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:clientId", "1000", "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:warehouseId", "1000", "http://tempuri.org/", ref element);


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

                //responseSOAPDoc.Save("C:\\temp\\nyce_logon2.xml");

                XmlNodeList nodeList = responseSOAPDoc.GetElementsByTagName("LogonResult");
                XmlElement resultElement = (XmlElement)nodeList[0];

                return resultElement.FirstChild.Value;
            }
            catch (Exception e)
            {
                throw new Exception("Inloggningen mot Nyce misslyckades.");
            }

            return "";
        }

        public string pausOrderLogon(string url, string username, string password)
        {
            WebRequest httpWebRequest = HttpWebRequest.Create(url);

            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;

            WebHeaderCollection webHeaderCollection = httpWebRequest.Headers;
            webHeaderCollection.Add("SOAPAction", "http://tempuri.org/WSPAUSORDER/Logon");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\" />");
            XmlElement docElement = xmlDoc.DocumentElement;
            XmlElement bodyElement = null;
            XmlElement methodElement = null;
            XmlElement element = null;

            addElement(ref docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/", ref bodyElement);
            addElement(ref bodyElement, "tem:Logon", "", "http://tempuri.org/", ref methodElement);
            addElement(ref methodElement, "tem:userName", username, "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:password", password, "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:language", "SWE", "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:clientGroupId", "0", "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:clientId", "1000", "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:warehouseId", "1000", "http://tempuri.org/", ref element);


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

                XmlNodeList nodeList = responseSOAPDoc.GetElementsByTagName("LogonResult");
                XmlElement resultElement = (XmlElement)nodeList[0];

                return resultElement.FirstChild.Value;
            }
            catch (Exception e)
            {
                throw new Exception("Inloggningen mot Nyce misslyckades.");
            }

            return "";
        }

        public bool pausOrder(string url, string token, string customerOrderNo)
        {
            WebRequest httpWebRequest = HttpWebRequest.Create(url);

            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 360000;

            WebHeaderCollection webHeaderCollection = httpWebRequest.Headers;
            webHeaderCollection.Add("SOAPAction", "http://tempuri.org/WSPAUSORDER/PausOrder");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\" />");
            XmlElement docElement = xmlDoc.DocumentElement;
            XmlElement bodyElement = null;
            XmlElement methodElement = null;
            XmlElement element = null;

            addElement(ref docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/", ref bodyElement);
            addElement(ref bodyElement, "tem:PausOrder", "", "http://tempuri.org/", ref methodElement);
            addElement(ref methodElement, "tem:ticket", token, "http://tempuri.org/", ref element);
            addElement(ref methodElement, "tem:OrderNumber", customerOrderNo, "http://tempuri.org/", ref element);


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

                XmlNamespaceManager nsMgr = new XmlNamespaceManager(responseSOAPDoc.NameTable);
                nsMgr.AddNamespace("a", "http://schemas.datacontract.org/2004/07/Nyce.Logic.Middleware.WebServices");

                XmlNodeList responseNodeList = responseSOAPDoc.GetElementsByTagName("PausOrderResponse", "http://tempuri.org/");

                if (responseNodeList.Count == 0) throw new Exception("Oväntat fel");

                foreach (XmlNode response in responseNodeList)
                {
                    if (response.InnerText == "true") return true;

                }

                return false;
            }
            catch (WebException e)
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(e.Response.GetResponseStream());
                XmlDocument responseSOAPDoc = new XmlDocument();
                responseSOAPDoc.LoadXml(streamReader.ReadToEnd());

                HttpContext.Current.Response.Write(responseSOAPDoc.OuterXml);
                HttpContext.Current.Response.End();

            }

            return false;
        }



        private void addElement(ref XmlElement parentElement, string nodeName, string nodeValue, string nameSpace, ref XmlElement element)
        {
            element = parentElement.OwnerDocument.CreateElement(nodeName, nameSpace);
            if (nodeValue != "")
            {
                XmlText text = parentElement.OwnerDocument.CreateTextNode(nodeValue);
                element.AppendChild(text);
            }
            
            parentElement.AppendChild(element);
        }

        private string getLogEntryFromDatabase(string connectionString, int logEntryNo)
        {
            Configuration configuration = new Configuration();
            configuration.connectionString = connectionString;

            string companyName = "Outnorth AB";
            if (connectionString.Contains("PIONEER")) companyName = "TEST Outnorth AB TEST";
            
            Database database = new Database(configuration);

            DatabaseQuery query = database.prepare("SELECT [Document] FROM ["+companyName+"$WH Log Entry] WHERE [Entry No_] = @entryNo");
            query.addIntParameter("entryNo", logEntryNo);

            byte[] data = (byte[])query.executeScalar();
            
            /*
            string xmlData = "";
            SqlDataReader dataReader = query.executeQuery();
            if (dataReader.Read())
            {
                xmlData = dataReader.GetValue(0).ToString();
            }
            dataReader.Close();
            */
            if (data == null) return "";

            string xmlData = System.Text.Encoding.Default.GetString(data);
            return xmlData;
        }
    }
}
