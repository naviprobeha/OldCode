using System;
using System.Xml;
using System.Collections;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for ResponseAuthentication.
    /// </summary>
    public class SessionUpdate
    {

        public string id;
        public string userUid;
        public bool offHook;
        public int numCalls;
        public string monitoredUserUid;
        public string monitoredUserId;
        public string applicationId;

        public bool conferenceMode;
        public int conferenceState;
        public ArrayList conferenceCallIds;
        public string conferenceAppearance;

        public SessionUpdate(XmlDocument xmlDoc)
        {
            //
            // TODO: Add constructor logic here
            //
            conferenceCallIds = new ArrayList();
            fromDom(xmlDoc);
        }


        private void fromDom(XmlDocument xmlDoc)
        {
            XmlElement docElement = xmlDoc.DocumentElement;

            offHook = false;

            XmlElement userElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user");
            if (userElement != null)
            {
                if (userElement.GetAttribute("userUid") != null) userUid = userElement.GetAttribute("userUid");
                if (userElement.GetAttribute("id") != null) id = userElement.GetAttribute("id");
            }

            XmlElement offHookElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/offHook");
            if (offHookElement != null)
            {
                if (offHookElement.FirstChild != null)
                {
                    if (offHookElement.FirstChild.Value == "True") offHook = true;
                }
            }

            XmlElement numCallsElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/numCalls");
            if (numCallsElement != null)
            {
                if (numCallsElement.FirstChild != null) numCalls = int.Parse(numCallsElement.FirstChild.Value);
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

            XmlElement conferenceElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/conference");
            if (conferenceElement != null)
            {
                this.conferenceMode = true;

                if (conferenceElement.GetAttribute("conterenceState") == "Active") conferenceState = 1;
                if (conferenceElement.GetAttribute("conterenceState") == "Released") conferenceState = 0;
                if (conferenceElement.GetAttribute("conterenceState") == "Held") conferenceState = 2;
            }

            XmlNodeList callInNodeList = docElement.SelectNodes("command/commandData/user/conference/callIn");
            int i = 0;
            while (i < callInNodeList.Count)
            {
                XmlElement callInElement = (XmlElement)callInNodeList.Item(i);
                if (callInElement.GetAttribute("callInId") != null) this.conferenceCallIds.Add(callInElement.GetAttribute("callInId"));

                i++;
            }

            XmlElement appearanceElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/conference/appearance");
            if (appearanceElement != null)
            {
                if (appearanceElement.FirstChild != null) this.conferenceAppearance = appearanceElement.FirstChild.Value;
            }

        }
    }
}
