using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Xml;

namespace Navipro.SvensktTenn.NAV
{
    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-92F1-1319ADB3EF5F";
        public const string intfguid = "D030D214-C984-496a-87E7-41732C114F1E";
        public const string eventguid = "D030D214-C984-496a-87E7-41732C114F1F";

        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ISTWebServiceAPI
    {
        [DispId(1)]
        void init(string url);
        [DispId(2)]
        string getOrderList();
        [DispId(3)]
        string doPrepayment(string orderNo, float amount);
        [DispId(4)]
        string getOrderList2(string orderNoFilter, string customerNoFilter, string customerNameFilter);
    }

    [Guid(Guids.coclsguid), ProgId("Navipro.SvensktTenn.Nav.STWebServiceAPI"), ClassInterface(ClassInterfaceType.None)]
    public class STWebServiceAPI : ISTWebServiceAPI 
    {
        string webServiceUrl = "";

        #region ISTWebServiceAPI Members

        public void init(string url)
        {
            webServiceUrl = url;
        }

        public string getOrderList()
        {
            STWebService.STWebService stWebService = new STWebService.STWebService();
            stWebService.Url = this.webServiceUrl;

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getOrders");

            string returnXmlDoc = stWebService.performService(xmlDoc.OuterXml);

            XmlDocument xmlDocOut =  new XmlDocument();
            xmlDocOut.LoadXml(returnXmlDoc);

            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\orderList" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            xmlDocOut.Save(fileName);

            return fileName;
        }

        public string getOrderList2(string orderNoFilter, string customerNoFilter, string customerNameFilter)
        {
            STWebService.STWebService stWebService = new STWebService.STWebService();
            stWebService.Url = this.webServiceUrl;

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "getOrders");

            createElement(ref xmlDoc, ref serviceArgument, "orderNoFilter", orderNoFilter);
            createElement(ref xmlDoc, ref serviceArgument, "customerNoFilter", customerNoFilter);
            createElement(ref xmlDoc, ref serviceArgument, "customerNameFilter", customerNameFilter);

            string returnXmlDoc = stWebService.performService(xmlDoc.OuterXml);

            XmlDocument xmlDocOut = new XmlDocument();
            xmlDocOut.LoadXml(returnXmlDoc);

            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\orderList" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            xmlDocOut.Save(fileName);

            return fileName;
        }

        public string doPrepayment(string orderNo, float amount)
        {
            STWebService.STWebService stWebService = new STWebService.STWebService();
            stWebService.Url = this.webServiceUrl;

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement serviceArgument = this.createXmlHeader(ref xmlDoc, "doPayment");

            createElement(ref xmlDoc, ref serviceArgument, "orderNo", orderNo);
            createElement(ref xmlDoc, ref serviceArgument, "amount", amount.ToString());

            string returnXmlDoc = stWebService.performService(xmlDoc.OuterXml);

            XmlDocument xmlDocOut = new XmlDocument();
            xmlDocOut.LoadXml(returnXmlDoc);

            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\doPayment" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
            xmlDocOut.Save(fileName);

            return fileName;

        }

        #endregion


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
