using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NyceWebServiceInvoker
{
    public class NyceWebServiceInvoker
    {
        public NyceWebServiceInvoker()
        {

        }

        public string combine(string ticket, string cDataFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            
            xmlDoc.LoadXml("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\"/>");
            XmlNamespaceManager nsMgt = new XmlNamespaceManager(xmlDoc.NameTable);

            XmlElement docElement = xmlDoc.DocumentElement;

            addElement(docElement, "soapenv:Header", "", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement bodyElement = addElement(docElement, "soapenv:Body", "", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement importElement = addElement(bodyElement, "tem:Import", "", "http://tempuri.org/");
            addElement(importElement, "tem:ticket", ticket, "http://tempuri.org/");

            XmlElement fileElement = addElement(importElement, "tem:File", "", "http://tempuri.org/");
            
            System.IO.StreamReader reader = new System.IO.StreamReader(cDataFileName);
            string cDataContent = reader.ReadToEnd();
            reader.Close();

            xmlDoc.CreateCDataSection(cDataContent);


            addElement(importElement, "tem:Identifier", "1", "http://tempuri.org/");


            string outFileName = "C:\temp\nyce_combined_response_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            xmlDoc.Save(outFileName);

            return outFileName;
        }

        private XmlElement addElement(XmlElement parentElement, string nodeName, string value, string nameSpace)
        {
            XmlElement xmlElement = parentElement.OwnerDocument.CreateElement(nodeName, nameSpace);
            xmlElement.Value = value;
            parentElement.AppendChild(xmlElement);

            return xmlElement;
        }
    }
}
