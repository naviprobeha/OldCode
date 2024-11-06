using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for ResponseAuthentication.
    /// </summary>
    public class ResponseAuthentication
    {

        public string id;
        public string applicationId;
        public string nonce;
        public string algorithm;

        public ResponseAuthentication(XmlDocument xmlDoc)
        {
            //
            // TODO: Add constructor logic here
            //
            fromDom(xmlDoc);
        }


        private void fromDom(XmlDocument xmlDoc)
        {
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement idElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/id");
            if (idElement != null)
            {
                if (idElement.FirstChild != null) id = idElement.FirstChild.Value;
            }

            XmlElement appIdElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/applicationId");
            if (appIdElement != null)
            {
                if (appIdElement.FirstChild != null) applicationId = appIdElement.FirstChild.Value;
            }

            XmlElement nonceElement = (XmlElement)docElement.SelectSingleNode("command/commandData/nonce");
            if (nonceElement != null)
            {
                if (nonceElement.FirstChild != null) nonce = nonceElement.FirstChild.Value;
            }

            XmlElement algorithmElement = (XmlElement)docElement.SelectSingleNode("command/commandData/algorithm");
            if (algorithmElement != null)
            {
                if (algorithmElement.FirstChild != null) algorithm = algorithmElement.FirstChild.Value;
            }

        }
    }
}
