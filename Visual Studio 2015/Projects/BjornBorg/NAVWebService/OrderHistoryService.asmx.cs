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

namespace Navipro.BjornBorg.NAVWebService
{
    /// <summary>
    /// Summary description for OrderHistoryService
    /// </summary>
    [WebService(Namespace = "http://www.navipro.se/bjornborg")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class OrderHistoryService : System.Web.Services.WebService
    {

        [WebMethod]
        public string getOrderHistory(string customerNo, string startingDate)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getOrderHistory");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "startingDate", startingDate);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getShipmentHistory(string customerNo, string startingDate)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getShipmentHistory");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "startingDate", startingDate);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getInvoiceHistory(string customerNo, string startingDate)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getInvoiceHistory");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "startingDate", startingDate);

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
        public string getShipment(string customerNo, string shipmentNo)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getShipment");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "shipmentNo", shipmentNo);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getInvoice(string customerNo, string invoiceNo)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getInvoice");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "invoiceNo", invoiceNo);

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
