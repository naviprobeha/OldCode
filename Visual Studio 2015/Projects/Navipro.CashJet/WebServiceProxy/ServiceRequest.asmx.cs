using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;


namespace Navipro.CashJet.WebServiceProxy
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://cashjet.navipro.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceProxy : System.Web.Services.WebService
    {

        [WebMethod]
        public string performService(string xmlDoc)
        {

            string webServiceUrl = ConfigurationSettings.AppSettings["webServiceUrl"];
            
            CashJetWebService.ServiceRequest cashJetServiceRequest = new CashJetWebService.ServiceRequest();
            cashJetServiceRequest.Url = webServiceUrl;

            return cashJetServiceRequest.performService(xmlDoc);
        }
    }
}
