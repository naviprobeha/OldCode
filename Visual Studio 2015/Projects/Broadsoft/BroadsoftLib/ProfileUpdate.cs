using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for ResponseAuthentication.
    /// </summary>
    public class ProfileUpdate
    {

        public string id;
        public string userUid;
        public string monitoredUserUid;
        public string monitoredUserId;
        public string applicationId;

        public string firstName;
        public string lastName;
        public string phone;
        public string extension;
        public string locationCode;
        public string countryCode;
        public string nationalPrefix;
        public string mobile;
        public string pager;
        public string email;
        public string department;
        public string title;
        public string voiceMessaging;
        public string voiceMessagingGroup;
        public string thirdPartyVoiceMessaging;
        public string thirdPartyVMGroup;
        public string dnd;
        public string cpe;
        public string acdState;
        public string cfa;
        public string cfaDestination;

        public ProfileUpdate(XmlDocument xmlDoc)
        {
            //
            // TODO: Add constructor logic here
            //
            this.acdState = "";
            this.applicationId = "";
            this.cfa = "";
            this.cfaDestination = "";
            this.countryCode = "";
            this.cpe = "";
            this.department = "";
            this.dnd = "";
            this.email = "";
            this.extension = "";
            this.firstName = "";
            this.id = "";
            this.lastName = "";
            this.locationCode = "";
            this.mobile = "";
            this.monitoredUserId = "";
            this.monitoredUserUid = "";
            this.nationalPrefix = "";
            this.pager = "";
            this.phone = "";
            this.thirdPartyVMGroup = "";
            this.thirdPartyVoiceMessaging = "";
            this.title = "";
            this.userUid = "";
            this.voiceMessaging = "";
            this.voiceMessagingGroup = "";

            fromDom(xmlDoc);
        }


        private void fromDom(XmlDocument xmlDoc)
        {

            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement userElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user");
            if (userElement != null)
            {
                if (userElement.GetAttribute("userUid") != null) userUid = userElement.GetAttribute("userUid");
                if (userElement.GetAttribute("id") != null) id = userElement.GetAttribute("id");
            }

            XmlElement appIdElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/applicationId");
            if (appIdElement != null)
            {
                if (appIdElement.FirstChild != null) applicationId = appIdElement.FirstChild.Value;
            }

            XmlElement monUidElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/monitoredUserUid");
            if (monUidElement != null)
            {
                if (monUidElement.FirstChild != null) monitoredUserUid = monUidElement.FirstChild.Value;
            }

            XmlElement monIdElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/monitoredUserId");
            if (monIdElement != null)
            {
                if (monIdElement.FirstChild != null) monitoredUserId = monIdElement.FirstChild.Value;
            }

            XmlElement firstNameElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/firstName");
            if (firstNameElement != null)
            {
                if (firstNameElement.FirstChild != null) firstName = firstNameElement.FirstChild.Value;
            }

            XmlElement lastNameElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/lastName");
            if (lastNameElement != null)
            {
                if (lastNameElement.FirstChild != null) lastName = lastNameElement.FirstChild.Value;
            }

            XmlElement phoneElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/phone");
            if (phoneElement != null)
            {
                if (phoneElement.FirstChild != null) phone = phoneElement.FirstChild.Value;
            }

            XmlElement extensionElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/extension");
            if (extensionElement != null)
            {
                if (extensionElement.FirstChild != null) extension = extensionElement.FirstChild.Value;
            }

            XmlElement locationCodeElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/locationCode");
            if (locationCodeElement != null)
            {
                if (locationCodeElement.FirstChild != null) locationCode = locationCodeElement.FirstChild.Value;
            }

            XmlElement countryCodeElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/countryCode");
            if (countryCodeElement != null)
            {
                if (countryCodeElement.FirstChild != null) countryCode = countryCodeElement.FirstChild.Value;
            }

            XmlElement nationalPrefixElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/nationalPrefix");
            if (nationalPrefixElement != null)
            {
                if (nationalPrefixElement.FirstChild != null) nationalPrefix = nationalPrefixElement.FirstChild.Value;
            }

            XmlElement mobileElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/mobile");
            if (mobileElement != null)
            {
                if (mobileElement.FirstChild != null) mobile = mobileElement.FirstChild.Value;
            }

            XmlElement pagerElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/pager");
            if (pagerElement != null)
            {
                if (pagerElement.FirstChild != null) pager = pagerElement.FirstChild.Value;
            }

            XmlElement emailElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/email");
            if (emailElement != null)
            {
                if (emailElement.FirstChild != null) email = emailElement.FirstChild.Value;
            }

            XmlElement departmentElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/department");
            if (departmentElement != null)
            {
                if (departmentElement.FirstChild != null) department = departmentElement.FirstChild.Value;
            }

            XmlElement titleElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/title");
            if (titleElement != null)
            {
                if (titleElement.FirstChild != null) title = titleElement.FirstChild.Value;
            }

            XmlElement voiceMessagingElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/voiceMessaging");
            if (voiceMessagingElement != null)
            {
                if (voiceMessagingElement.FirstChild != null) voiceMessaging = voiceMessagingElement.FirstChild.Value;
            }

            XmlElement voiceMessagingGroupElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/voiceMessagingGroup");
            if (voiceMessagingGroupElement != null)
            {
                if (voiceMessagingGroupElement.FirstChild != null) voiceMessagingGroup = voiceMessagingGroupElement.FirstChild.Value;
            }

            XmlElement thirdPartyVoiceMessagingElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/thirdPartyVoiceMessaging");
            if (thirdPartyVoiceMessagingElement != null)
            {
                if (thirdPartyVoiceMessagingElement.FirstChild != null) thirdPartyVoiceMessaging = thirdPartyVoiceMessagingElement.FirstChild.Value;
            }

            XmlElement thirdPartyVMGroupElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/thirdPartyVMGroup");
            if (thirdPartyVMGroupElement != null)
            {
                if (thirdPartyVMGroupElement.FirstChild != null) thirdPartyVMGroup = thirdPartyVMGroupElement.FirstChild.Value;
            }

            XmlElement dndElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/dnd");
            if (dndElement != null)
            {
                if (dndElement.FirstChild != null) dnd = dndElement.FirstChild.Value;
            }

            XmlElement cpeElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/cpe");
            if (cpeElement != null)
            {
                if (cpeElement.FirstChild != null) cpe = cpeElement.FirstChild.Value;
            }

            XmlElement acdStateElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/acdState");
            if (acdStateElement != null)
            {
                if (acdStateElement.FirstChild != null) acdState = acdStateElement.FirstChild.Value;
            }


            XmlElement cfaElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/cfa");
            if (cfaElement != null)
            {
                if (cfaElement.FirstChild != null) cfa = cfaElement.FirstChild.Value;
                if (cfaElement.GetAttribute("cfaDestination") != null) cfaDestination = cfaElement.GetAttribute("cfaDestination");
            }


        }
    }
}
