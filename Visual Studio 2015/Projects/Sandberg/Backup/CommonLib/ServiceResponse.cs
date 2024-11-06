using System;
using System.Xml;

namespace Navipro.Sandberg.Common
{
    /// <summary>
    /// Summary description for ServiceResponse.
    /// </summary>
    public class ServiceResponse
    {
        private string statusValue;

        public string xml;

        public ServiceResponse(XmlDocument xmlDoc)
        {
            //
            // TODO: Add constructor logic here
            //

            xml = xmlDoc.OuterXml;

            XmlElement documentElement = xmlDoc.DocumentElement;

            XmlElement statusElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/status");
            if (statusElement != null)
            {
                statusValue = statusElement.FirstChild.Value;
            }

        }

        public string status
        {
            get
            {
                return statusValue;
            }
        }

    }
}
