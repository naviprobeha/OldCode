using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace Navipro.Baxi.NAVWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.navipro.se/baxi")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceRequest : System.Web.Services.WebService
    {

        [WebMethod]
        public string getOrders(string customerNo)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getOrders");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getOrder(string customerNo, string orderNo)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getOrder");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "orderNo", orderNo);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getWarranties(string installerCustomerNo)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getWarranties");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", installerCustomerNo);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getWarranty(string customerNo, int no)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getWarranty");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "no", no.ToString());

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

 
        [WebMethod]
        public string updateCustomer(string customerXmlDocument)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();          
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "updateCustomer");
            XmlElement docElement = xmlDoc.DocumentElement;

            xmlDoc.LoadXml(customerXmlDocument);
            XmlElement argumentElement = xmlDoc.DocumentElement;

            serviceArgument.AppendChild(argumentElement);

            xmlDoc.LoadXml(docElement.OuterXml);
            
            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string createOrder(string orderXmlDocument)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "createOrder");
            XmlElement docElement = xmlDoc.DocumentElement;

            xmlDoc.LoadXml(orderXmlDocument);
            XmlElement argumentElement = xmlDoc.DocumentElement;

            serviceArgument.AppendChild(argumentElement);

            xmlDoc.LoadXml(docElement.OuterXml);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            
            return null;
        }


        [WebMethod]
        public string createWarranty(string warrantyXmlDocument)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "createWarranty");
            XmlElement docElement = xmlDoc.DocumentElement;

            xmlDoc.LoadXml(warrantyXmlDocument);
            XmlElement argumentElement = xmlDoc.DocumentElement;

            serviceArgument.AppendChild(argumentElement);

            xmlDoc.LoadXml(docElement.OuterXml);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string createQuote(string quoteXmlDocument)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "createQuote");
            XmlElement docElement = xmlDoc.DocumentElement;

            xmlDoc.LoadXml(quoteXmlDocument);
            XmlElement argumentElement = xmlDoc.DocumentElement;

            serviceArgument.AppendChild(argumentElement);

            xmlDoc.LoadXml(docElement.OuterXml);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string copyQuote(string quoteXmlDocument)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "salesQuoteCopy");
            XmlElement docElement = xmlDoc.DocumentElement;

            xmlDoc.LoadXml(quoteXmlDocument);
            XmlElement argumentElement = xmlDoc.DocumentElement;

            serviceArgument.AppendChild(argumentElement);

            xmlDoc.LoadXml(docElement.OuterXml);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string salesQuoteChange(string quoteXmlDocument)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "salesQuoteChange");
            XmlElement docElement = xmlDoc.DocumentElement;

            xmlDoc.LoadXml(quoteXmlDocument);
            XmlElement argumentElement = xmlDoc.DocumentElement;

            serviceArgument.AppendChild(argumentElement);

            xmlDoc.LoadXml(docElement.OuterXml);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string callMethod(string methodName, string argumentXmlDocument)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, methodName);
            XmlElement docElement = xmlDoc.DocumentElement;

            xmlDoc.LoadXml(argumentXmlDocument);
            XmlElement argumentElement = xmlDoc.DocumentElement;

            serviceArgument.AppendChild(argumentElement);

            xmlDoc.LoadXml(docElement.OuterXml);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        private XmlElement createXmlHeader(ref XmlDocument xmlDoc, string serviceName)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<nav/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement serviceRequestElement = createElement(ref xmlDoc, ref docElement, "serviceRequest", "");
            createElement(ref xmlDoc, ref serviceRequestElement, "serviceName", serviceName);
            XmlElement serviceArgumentElement = createElement(ref xmlDoc, ref serviceRequestElement, "serviceArgument", "");

            return serviceArgumentElement;

        }

        private XmlElement createElement(ref XmlDocument xmlDoc, ref XmlElement parentElement, string elementName, string elementValue)
        {
            XmlElement element = xmlDoc.CreateElement(elementName);
            if (elementValue != "")
            {
                XmlText text = xmlDoc.CreateTextNode(elementValue);
                element.AppendChild(text);
            }
            parentElement.AppendChild(element);

            return element;

        }

    }
}
