using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;


namespace Navipro.Infojet.WebServiceProxy
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://infojet.workanywhere.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InfojetWebServiceProxy : System.Web.Services.WebService
    {

        [WebMethod]
        public string performservice(string method, string secret, string xmlDoc)
        {

            string webServiceUrl = ConfigurationSettings.AppSettings["webServiceUrl"];

            Navipro.Infojet.ServiceRequest.InfojetServiceRequest infojetServiceRequest = new Navipro.Infojet.ServiceRequest.InfojetServiceRequest();
            infojetServiceRequest.Url = webServiceUrl;

            return infojetServiceRequest.performservice(method, secret, xmlDoc);
            
        }

    }
}
