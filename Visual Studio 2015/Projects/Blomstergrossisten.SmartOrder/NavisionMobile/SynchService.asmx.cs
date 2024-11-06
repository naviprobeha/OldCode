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


namespace NavisionMobile
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://infojet.workanywhere.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SynchService : System.Web.Services.WebService
    {

        [WebMethod]
        public void PerformService(string serviceRequest)
        {
            ServiceDelegator serviceDelegator = new ServiceDelegator();

            XmlDocument xmlDocIn = new XmlDocument();
            xmlDocIn.LoadXml(serviceRequest);
            XmlDocument xmlDocOut = serviceDelegator.transport(xmlDocIn);
            if (xmlDocOut != null)
            {
                XmlTextWriter xmlTextWriter = new XmlTextWriter(HttpContext.Current.Response.Output);
                xmlDocOut.Save(xmlTextWriter);

                //if (logOutFile != null) xmlServiceResponse.Save(logOutFile);

                xmlTextWriter.Flush();

            }
 

        }



    }
}
