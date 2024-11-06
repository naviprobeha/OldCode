using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace Navipro.SvensktTenn.Nav
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://navipro.svenskttenn.nav/stwebservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class STWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string performService(string documentStr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(documentStr);
            
            ServiceDelegator serviceDelegator = new ServiceDelegator();
            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDoc);

            return xmlDocOut.OuterXml;
        }
    }
}
