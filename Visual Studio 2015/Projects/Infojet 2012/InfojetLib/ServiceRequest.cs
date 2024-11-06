using System;
using System.Xml;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ServiceRequest.
	/// </summary>
	public class ServiceRequest
	{
		private XmlDocument xmlDoc;

		public ServiceRequest(Infojet infojetContext, string serviceName, ServiceArgument serviceArgument)
		{
			//
			// TODO: Add constructor logic here
			//
            WebUserAccount webUserAccount = null;
            if (infojetContext.userSession != null) webUserAccount = infojetContext.userSession.webUserAccount;
            
            init(infojetContext, serviceName, serviceArgument, webUserAccount);
		}

        public ServiceRequest(Infojet infojetContext, string serviceName, ServiceArgument serviceArgument, WebUserAccount webUserAccount)
        {
            //
            // TODO: Add constructor logic here
            //

            init(infojetContext, serviceName, serviceArgument, webUserAccount);
        }


        private void init(Infojet infojetContext, string serviceName, ServiceArgument serviceArgument, WebUserAccount webUserAccount)
        {
            xmlDoc = new XmlDocument();

            if ((infojetContext.configuration.nav2013mode) || (infojetContext.webSite.orderSubmitMethod == 1))
            {
                xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><infojet/>");
            }
            else
            {
                xmlDoc.LoadXml("<infojet/>");
            }

            XmlElement documentElement = xmlDoc.DocumentElement;

            XmlElement serviceRequestElement = xmlDoc.CreateElement("serviceRequest");
            documentElement.AppendChild(serviceRequestElement);

            if (infojetContext.webSite != null)
            {
                XmlElement webSiteElement = xmlDoc.CreateElement("webSite");
                webSiteElement.AppendChild(xmlDoc.CreateTextNode(infojetContext.webSite.code));
                serviceRequestElement.AppendChild(webSiteElement);
            }

            XmlElement languageElement = xmlDoc.CreateElement("languageCode");
            languageElement.AppendChild(xmlDoc.CreateTextNode(infojetContext.languageCode));
            serviceRequestElement.AppendChild(languageElement);

            if (webUserAccount != null)
            {
                XmlElement userAccountNoElement = xmlDoc.CreateElement("webUserAccountNo");
                userAccountNoElement.AppendChild(xmlDoc.CreateTextNode(webUserAccount.no));
                serviceRequestElement.AppendChild(userAccountNoElement);
            }

            XmlElement serviceNameElement = xmlDoc.CreateElement("serviceName");
            serviceNameElement.AppendChild(xmlDoc.CreateTextNode(serviceName));
            serviceRequestElement.AppendChild(serviceNameElement);

            XmlElement serviceArgumentElement = xmlDoc.CreateElement("serviceArgument");
            serviceArgumentElement.AppendChild(serviceArgument.toDOM(xmlDoc));
            serviceRequestElement.AppendChild(serviceArgumentElement);

        }

		public XmlDocument getDocument()
		{
			return xmlDoc;
		}
	}
}
