using System;
using System.Xml;


namespace Navipro.SmartInventory
{
	/// <summary>
	/// Summary description for ServiceRequest.
	/// </summary>
	public class ServiceRequest
	{
		private string serviceName;
		private ServiceArgument serviceArgument;


		public ServiceRequest(string serviceName)
		{
			this.serviceName = serviceName;
	
		}

		public void setServiceArgument(ServiceArgument serviceArgument)
		{
			this.serviceArgument = serviceArgument;
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			XmlElement serviceRequestElement = xmlDocumentContext.CreateElement("serviceRequest");
			XmlElement serviceNameElement = xmlDocumentContext.CreateElement("serviceName");
			XmlElement serviceArgumentElement = xmlDocumentContext.CreateElement("serviceArgument");

			serviceNameElement.AppendChild(xmlDocumentContext.CreateTextNode(serviceName));

			serviceRequestElement.AppendChild(serviceNameElement);
			serviceRequestElement.AppendChild(serviceArgumentElement);

			if (serviceArgument != null)
			{
				serviceArgumentElement.AppendChild(serviceArgument.toDOM(xmlDocumentContext));
			}

			return serviceRequestElement;
		}
	}
}
