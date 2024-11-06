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


namespace Navipro.Infojet.WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://infojet.workanywhere.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InfojetServiceRequest : System.Web.Services.WebService
    {

        [WebMethod]
        public string performservice(string xmlDoc)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDocIn = new XmlDocument();
            xmlDocIn.LoadXml(xmlDoc);
            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDocIn);
            if (xmlDocOut != null) return xmlDocOut.OuterXml;

            return null;
        }



    }
}
