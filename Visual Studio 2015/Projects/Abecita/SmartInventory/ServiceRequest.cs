using System;
using System.Xml;


namespace SmartInventory
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
			XmlElement serviceRequestElement = xmlDocumentContext.CreateElement("SERVICE_REQUEST");
			XmlElement serviceNameElement = xmlDocumentContext.CreateElement("SERVICE_NAME");
			XmlElement serviceArgumentElement = xmlDocumentContext.CreateElement("SERVICE_ARGUMENT");

			serviceNameElement.AppendChild(xmlDocumentContext.CreateTextNode(serviceName));

			serviceRequestElement.AppendChild(serviceNameElement);
			serviceRequestElement.AppendChild(serviceArgumentElement);

			if (serviceArgument != null)
			{
				serviceArgumentElement.AppendChild(serviceArgument.toDOM(xmlDocumentContext));
			}

			return serviceRequestElement;
		}

		public void postDOM()
		{
			if (serviceArgument != null) serviceArgument.postDOM();
		}
	}
}
