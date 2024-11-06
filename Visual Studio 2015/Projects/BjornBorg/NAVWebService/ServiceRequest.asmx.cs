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

namespace Navipro.BjornBorg.NAVWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.navipro.se/bjornborg")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceRequest : System.Web.Services.WebService
    {

        [WebMethod]
        public string getCustomers(string salesPersonCode, string changedDate)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();
            
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getCustomers");

            createElement(ref xmlDoc, ref serviceArgument, "salesPersonCode", salesPersonCode);
            createElement(ref xmlDoc, ref serviceArgument, "changedDate", changedDate);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getModels(string changedDate)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();
            
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getModels");

            createElement(ref xmlDoc, ref serviceArgument, "changedDate", changedDate);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getVariants(string modelNo)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getVariants");

            createElement(ref xmlDoc, ref serviceArgument, "modelNo", modelNo);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getModelInventory(string modelCode)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();
            
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getModelInventory");

            createElement(ref xmlDoc, ref serviceArgument, "modelCode", modelCode);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getModelPackInventory(string modelCode)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getModelPackInventory");

            createElement(ref xmlDoc, ref serviceArgument, "modelCode", modelCode);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getColorInventory(string modelCode, string colorCode)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();
            
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getColorInventory");

            createElement(ref xmlDoc, ref serviceArgument, "modelCode", modelCode);
            createElement(ref xmlDoc, ref serviceArgument, "colorCode", colorCode);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getColorPackInventory(string modelCode, string colorCode)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getColorPackInventory");

            createElement(ref xmlDoc, ref serviceArgument, "modelCode", modelCode);
            createElement(ref xmlDoc, ref serviceArgument, "colorCode", colorCode);

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getStatistics(string customerNo, int year)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getStatistics");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "year", year.ToString());

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getTotalPreOrderLines(string customerNo, int year)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getTotalPreOrderLines");

            createElement(ref xmlDoc, ref serviceArgument, "customerNo", customerNo);
            createElement(ref xmlDoc, ref serviceArgument, "year", year.ToString());

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }


        [WebMethod]
        public string getOrderTypes()
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getOrderTypes");

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getPrepacks()
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getPrepacks");

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }

        [WebMethod]
        public string getSeasons()
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getSeasons");

            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }


        [WebMethod]
        public string createOrder(string xmlDocument)
        {

            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "createOrder");
            XmlElement docElement = xmlDoc.DocumentElement;

            xmlDoc.LoadXml(xmlDocument);
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
