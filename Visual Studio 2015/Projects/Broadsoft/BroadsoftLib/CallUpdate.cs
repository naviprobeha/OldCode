using System;
using System.Xml;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for ResponseAuthentication.
    /// </summary>
    public class CallUpdate
    {

        public string id;
        public string userUid;
        public string monitoredUserUid;
        public string monitoredUserId;
        public string applicationId;

        public string callId;
        public string extTrackingId;
        public int state;
        public string remoteNumber;
        public string remoteName;

        public string callCenterUserId;
        public string appearance;
        public int personality;
        public string callType;
        public string remoteCountryCode;
        public string localAltType;
        public string linePort;
        public string localNumber;
        public string redirectToNum;
        public string redirectToReason;
        public string redirectFromCountryCode;
        public string redirectFromNumber;
        public string redirectFromName;
        public string redirectFromReason;
        public string redirectFromCounter;


        public CallUpdate(XmlDocument xmlDoc)
        {
            //
            // TODO: Add constructor logic here
            //

            this.state = 0;
            this.remoteName = "";
            this.remoteNumber = "";
            this.callId = "";
            this.extTrackingId = "";

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

            XmlElement callElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call");
            if (callElement != null)
            {
                if (callElement.GetAttribute("callId") != null) callId = callElement.GetAttribute("callId");
                if (callElement.GetAttribute("extTrackingId") != null) extTrackingId = callElement.GetAttribute("extTrackingId");
            }

            XmlElement stateElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/state");
            if (stateElement != null)
            {
                if (stateElement.FirstChild != null) state = int.Parse(stateElement.FirstChild.Value);
            }

            XmlElement remoteNumberElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/remoteNumber");
            if (remoteNumberElement != null)
            {
                if (remoteNumberElement.FirstChild != null) remoteNumber = remoteNumberElement.FirstChild.Value;
            }

            XmlElement remoteNameElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/remoteName");
            if (remoteNameElement != null)
            {
                if (remoteNameElement.FirstChild != null) remoteName = remoteNameElement.FirstChild.Value;
            }

            XmlElement callCenterUserIdElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/callCenterUserId");
            if (callCenterUserIdElement != null)
            {
                if (callCenterUserIdElement.FirstChild != null) callCenterUserId = callCenterUserIdElement.FirstChild.Value;
            }

            XmlElement appearanceElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/appearance");
            if (appearanceElement != null)
            {
                if (appearanceElement.FirstChild != null) appearance = appearanceElement.FirstChild.Value;
            }

            XmlElement personalityElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/personality");
            if (personalityElement != null)
            {
                if (personalityElement.FirstChild != null) personality = int.Parse(personalityElement.FirstChild.Value);
            }

            XmlElement callTypeElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/callType");
            if (callTypeElement != null)
            {
                if (callTypeElement.FirstChild != null) callType = callTypeElement.FirstChild.Value;
            }

            XmlElement remoteCountryCodeElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/remoteCountryCode");
            if (remoteCountryCodeElement != null)
            {
                if (remoteCountryCodeElement.FirstChild != null) remoteCountryCode = remoteCountryCodeElement.FirstChild.Value;
            }


            XmlElement localAltTypeElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/localAltType");
            if (localAltTypeElement != null)
            {
                if (localAltTypeElement.FirstChild != null) localAltType = localAltTypeElement.FirstChild.Value;
            }

            XmlElement linePortElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/linePort");
            if (linePortElement != null)
            {
                if (linePortElement.FirstChild != null) linePort = linePortElement.FirstChild.Value;
            }

            XmlElement localNumberElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/localNumber");
            if (localNumberElement != null)
            {
                if (localNumberElement.FirstChild != null) localNumber = localNumberElement.FirstChild.Value;
            }

            XmlElement redirectToNumElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/redirectToNum");
            if (redirectToNumElement != null)
            {
                if (redirectToNumElement.FirstChild != null) redirectToNum = redirectToNumElement.FirstChild.Value;
            }

            XmlElement redirectToReasonElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/redirectToReason");
            if (redirectToReasonElement != null)
            {
                if (redirectToReasonElement.FirstChild != null) redirectToReason = redirectToReasonElement.FirstChild.Value;
            }

            XmlElement redirectFromCountryCodeElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/redirectFromCountryCode");
            if (redirectFromCountryCodeElement != null)
            {
                if (redirectFromCountryCodeElement.FirstChild != null) redirectFromCountryCode = redirectFromCountryCodeElement.FirstChild.Value;
            }

            XmlElement redirectFromNumberElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/redirectFromNumber");
            if (redirectFromNumberElement != null)
            {
                if (redirectFromNumberElement.FirstChild != null) redirectFromNumber = redirectFromNumberElement.FirstChild.Value;
            }

            XmlElement redirectFromNameElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/redirectFromName");
            if (redirectFromNameElement != null)
            {
                if (redirectFromNameElement.FirstChild != null) redirectFromName = redirectFromNameElement.FirstChild.Value;
            }

            XmlElement redirectFromReasonElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/redirectFromReason");
            if (redirectFromReasonElement != null)
            {
                if (redirectFromReasonElement.FirstChild != null) redirectFromReason = redirectFromReasonElement.FirstChild.Value;
            }

            XmlElement redirectFromCounterElement = (XmlElement)docElement.SelectSingleNode("command/commandData/user/call/redirectFromCounter");
            if (redirectFromCounterElement != null)
            {
                if (redirectFromCounterElement.FirstChild != null) redirectFromCounter = redirectFromCounterElement.FirstChild.Value;
            }

        }
    }
}
