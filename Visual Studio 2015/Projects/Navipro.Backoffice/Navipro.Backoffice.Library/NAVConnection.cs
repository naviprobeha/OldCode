using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;

namespace Navipro.Backoffice.Library
{
    public class NAVConnection
    {

        public static void performService(Configuration configuration, string method, XmlDocument parameterDocument, out XmlElement xmlResponseElement)
        {
            string url = configuration.wsAddress;
            string userName = configuration.wsUserName;
            string password = configuration.wsPassword;

            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(url);

            NetworkCredential netCredentials = new NetworkCredential(userName, password);

            httpWebRequest.Credentials = netCredentials;
            httpWebRequest.ContentType = "text/xml; charset=utf-8";
            httpWebRequest.Method = "POST";

            httpWebRequest.Timeout = 40000;

            WebHeaderCollection collection = httpWebRequest.Headers;
            collection.Add("SOAPAction", "performService");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"/>");

            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement bodyElement = addElement(docElement, "soap:Body", "", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement methodElement = addElement(bodyElement, "PerformService", "", "urn:microsoft-dynamics-schemas/codeunit/CaseMasterWebService");

            addElement(methodElement, "method", method, "urn:microsoft-dynamics-schemas/codeunit/CaseMasterWebService");
            XmlElement dataElement = addElement(methodElement, "xmlRecordData", "", "urn:microsoft-dynamics-schemas/codeunit/CaseMasterWebService");

            if (parameterDocument != null)
            {
                XmlCDataSection cData = xmlDoc.CreateCDataSection(parameterDocument.OuterXml);
                dataElement.AppendChild(cData);
            }


            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            streamWriter.Write(xmlDoc.OuterXml);
            streamWriter.Close();


            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());

            XmlDocument responseSOAPDoc = new XmlDocument();
            responseSOAPDoc.LoadXml(streamReader.ReadToEnd());

            httpWebResponse.Close();


            XmlNodeList nodeList = responseSOAPDoc.GetElementsByTagName("return_value");
            if (nodeList.Count > 0)
            {
                XmlElement returnValueElement = (XmlElement)nodeList.Item(0);
                if (returnValueElement != null)
                {
                    XmlText returnValueText = (XmlText)returnValueElement.FirstChild;
                    if (returnValueText != null)
                    {
                        XmlDocument responseDoc = new XmlDocument();
                        responseDoc.LoadXml(returnValueText.Value);
                        XmlElement responseDocElement = responseDoc.DocumentElement;

                        xmlResponseElement = null;
                        XmlElement dataContentElement = (XmlElement)responseDocElement.SelectSingleNode("data");
                        if (dataContentElement != null) xmlResponseElement = (XmlElement)dataContentElement.FirstChild;

                        XmlElement statusElement = (XmlElement)responseDocElement.SelectSingleNode("status");
                        if (statusElement.FirstChild.Value != "OK")
                        {
                            throw new Exception(statusElement.FirstChild.Value);
                        }

                        return;
                    }
                }
            }


            throw new Exception("Unknown error!");

        }

        public static XmlElement addElement(XmlElement parentElement, string elementName, string value, string nameSpace)
        {
            XmlElement element = parentElement.OwnerDocument.CreateElement(elementName, nameSpace);
            if (value != "")
            {
                XmlText text = parentElement.OwnerDocument.CreateTextNode(value);
                element.AppendChild(text);
            }
            parentElement.AppendChild(element);

            return element;
        }
    }

}