using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for ResponseAuthentication.
    /// </summary>
    public class RegisterResponse
    {

        public string id;
        public string applicationId;
        public string userUid;
        public bool failed;
        public string failureCause;

        public RegisterResponse(XmlDocument xmlDoc)
        {
            //
            // TODO: Add constructor logic here
            //
            fromDom(xmlDoc);
        }


        private void fromDom(XmlDocument xmlDoc)
        {
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement userElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user");
            if (userElement != null)
            {
                if (userElement.GetAttribute("userUid") != null) userUid = userElement.GetAttribute("userUid");
            }

            XmlElement idElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/id");
            if (idElement != null)
            {
                if (idElement.FirstChild != null) id = idElement.FirstChild.Value;
            }

            XmlElement failureElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/failure");
            if (failureElement != null)
            {
                failed = true;
                if (failureElement.GetAttribute("failureCause") != null) failureCause = failureElement.GetAttribute("failureCause");
            }

            XmlElement appIdElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/applicationId");
            if (appIdElement != null)
            {
                if (appIdElement.FirstChild != null) applicationId = appIdElement.FirstChild.Value;
            }

        }
    }
}
