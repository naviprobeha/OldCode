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

namespace WooComWrapper
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://woocommercewrapper.navipro.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WooComWrapper : System.Web.Services.WebService
    {

        [WebMethod]
        public string Execute(string apiUrl, string userName, string password, string xmlData, string requestFileName)
        {
            
            //WooCommerceWrapper.Rest rest = new WooCommerceWrapper.Rest(apiUrl, userName, password);
            
            XmlDocument xmlDoc = new XmlDocument();
            XmlDocument xmlResponse = new XmlDocument();

            if (requestFileName != "")
            {
                xmlDoc.Load(requestFileName); // Filename
            }
            else
            {
                xmlDoc.LoadXml(xmlData); // XML Content
            }

            //string errorMessage = "C:\\temp\\";
            string errorMessage = "";
            
            /*
            //rest.Execute(ref xmlDoc, ref errorMessage, ref xmlResponse);
            
            if (errorMessage != "")
            {

                xmlResponse.LoadXml("<error/>");
                XmlElement docElement = xmlResponse.DocumentElement;
                XmlText xmlText = xmlResponse.CreateTextNode(errorMessage);
                docElement.AppendChild(xmlText);
            }
            */
            xmlResponse.Save("C:\\temp\\woocomwrapper_response.xml");

            return xmlResponse.OuterXml;
        }
    }
}
