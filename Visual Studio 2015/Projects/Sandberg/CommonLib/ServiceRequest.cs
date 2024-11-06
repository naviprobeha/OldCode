using System;
using System.Xml;

namespace Navipro.Sandberg.Common
{
    /// <summary>
    /// Summary description for ServiceRequest.
    /// </summary>
    public class ServiceRequest
    {
        private XmlDocument xmlDoc;

        public ServiceRequest(string webSite, string serviceName, ServiceArgument serviceArgument)
        {
            //
            // TODO: Add constructor logic here
            //
            xmlDoc = new XmlDocument();

            xmlDoc.LoadXml("<infojet/>");

            XmlElement documentElement = xmlDoc.DocumentElement;

            XmlElement serviceRequestElement = xmlDoc.CreateElement("serviceRequest");
            documentElement.AppendChild(serviceRequestElement);

            XmlElement webSiteElement = xmlDoc.CreateElement("webSite");
            webSiteElement.AppendChild(xmlDoc.CreateTextNode(webSite));
            serviceRequestElement.AppendChild(webSiteElement);

            XmlElement serviceNameElement = xmlDoc.CreateElement("serviceName");
            serviceNameElement.AppendChild(xmlDoc.CreateTextNode(serviceName));
            serviceRequestElement.AppendChild(serviceNameElement);

            XmlElement serviceArgumentElement = xmlDoc.CreateElement("serviceArgument");            
            if (serviceArgument != null) serviceArgumentElement.AppendChild(serviceArgument.toDOM(xmlDoc));
            serviceRequestElement.AppendChild(serviceArgumentElement);

        }

        public XmlDocument getDocument()
        {
            return xmlDoc;
        }
    }
}
