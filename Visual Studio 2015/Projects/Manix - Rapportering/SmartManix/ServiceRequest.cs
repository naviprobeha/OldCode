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
        private string guid = "";
        private string agentId = "";



        public ServiceRequest(string serviceName)
        {
            this.serviceName = serviceName;
            this.guid = Guid.NewGuid().ToString();
        }

        public void setServiceArgument(ServiceArgument serviceArgument)
        {
            this.serviceArgument = serviceArgument;
        }

        public void setAgentId(string agentId)
        {
            this.agentId = agentId;
        }

        public XmlElement toDOM(XmlDocument xmlDocumentContext)
        {
            XmlElement serviceRequestElement = xmlDocumentContext.CreateElement("serviceRequest");
            serviceRequestElement.SetAttribute("messageId", guid);
            serviceRequestElement.SetAttribute("agentId", agentId);

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
