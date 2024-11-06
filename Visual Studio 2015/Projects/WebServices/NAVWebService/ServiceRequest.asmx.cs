using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Services;

namespace Navipro.WebServices.NAVWebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://www.navipro.se/webservices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceRequest : System.Web.Services.WebService
    {
        [WebMethod]
        public string performService(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            ServiceDelegator serviceDelegator = new ServiceDelegator();
            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);

            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }
    }
}
