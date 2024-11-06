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
			xmlDoc = new XmlDocument();

			xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><infojet/>");

			XmlElement documentElement = xmlDoc.DocumentElement;

			XmlElement serviceRequestElement = xmlDoc.CreateElement("serviceRequest");
			documentElement.AppendChild(serviceRequestElement);
			
			XmlElement webSiteElement = xmlDoc.CreateElement("webSite");
			webSiteElement.AppendChild(xmlDoc.CreateTextNode(infojetContext.webSite.code));
			serviceRequestElement.AppendChild(webSiteElement);

            if (infojetContext.userSession != null)
            {
                if (infojetContext.userSession.webUserAccount != null)
                {
                    XmlElement userAccountNoElement = xmlDoc.CreateElement("webUserAccountNo");
                    userAccountNoElement.AppendChild(xmlDoc.CreateTextNode(infojetContext.userSession.webUserAccount.no));
                    serviceRequestElement.AppendChild(userAccountNoElement);
                }
            }

			XmlElement serviceNameElement = xmlDoc.CreateElement("serviceName");
			serviceNameElement.AppendChild(xmlDoc.CreateTextNode(serviceName));
			serviceRequestElement.AppendChild(serviceNameElement);

            if (serviceArgument != null)
            {
                XmlElement serviceArgumentElement = xmlDoc.CreateElement("serviceArgument");
                serviceArgumentElement.AppendChild(serviceArgument.toDOM(xmlDoc));
                serviceRequestElement.AppendChild(serviceArgumentElement);
            }
		}

		public XmlDocument getDocument()
		{
			return xmlDoc;
		}
	}
}
