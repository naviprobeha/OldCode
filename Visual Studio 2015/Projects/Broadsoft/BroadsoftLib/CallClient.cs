using System.Collections;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for AttendantConsole.
    /// </summary>
    public class CallClient : CAPListener
    {

        private CAPClient capClient;
        private ArrayList listeners;
        private Logger logger;
        private string userUid;
        private string applicationId;

        public CallClient(string serverName, int port, string userName, string password, Logger logger, string applicationId)
        {

            this.applicationId = applicationId;

            listeners = new ArrayList();
            capClient = new CAPClient(serverName, port, CAPClient.CALL_CLIENT, userName, password, applicationId);
            capClient.registerListener(this);
            capClient.connect();

            this.logger = logger;
        }

        public void login()
        {
            log("Logging on to BroadWorks...");
            capClient.authenticationRequest();
            notifyListeners_connect(1);
        }

        public void logoff()
        {
            capClient.close();
            notifyListeners_connect(0);
        }

        public void registerListener(CallClientListener callClientListener)
        {
            listeners.Add(callClientListener);
        }

        #region CAPListener Members

        public void notify(string docType, System.Xml.XmlDocument xmlDocument)
        {
            // TODO:  Add AttendantConsole.notify implementation


            if (docType == "responseAuthentication")
            {
                ResponseAuthentication responseAuthentication = new ResponseAuthentication(xmlDocument);
                log("Authenticating...");
                capClient.registerRequest(responseAuthentication);
            }

            if (docType == "registerResponse")
            {
                RegisterResponse registerResponse = new RegisterResponse(xmlDocument);
                if (registerResponse.failed)
                {
                    log("Authentication failed with cause: " + registerResponse.failureCause);
                }
                else
                {
                    log("Authenticated. UserNo: " + capClient.userUid);
                    this.userUid = capClient.userUid;
                    capClient.acknowledgeMessage("registerResponse");
                    notifyListeners_connect(2);
                }
            }

            if (docType == "sessionUpdate")
            {
                SessionUpdate sessionUpdate = new SessionUpdate(xmlDocument);
                log("SessionUpdate for " + sessionUpdate.id + " (" + sessionUpdate.userUid + "). OffHook: " + sessionUpdate.offHook + ", NumCalls: " + sessionUpdate.numCalls);
                notifyListeners_sessionUpdate(sessionUpdate);
            }

            if (docType == "profileUpdate")
            {
                ProfileUpdate profileUpdate = new ProfileUpdate(xmlDocument);
                log("ProfileUpdate for " + profileUpdate.id + " (" + profileUpdate.userUid + ")");
                notifyListeners_profileUpdate(profileUpdate);
            }

            if (docType == "callUpdate")
            {
                CallUpdate callUpdate = new CallUpdate(xmlDocument);
                log("CallUpdate for " + callUpdate.id + " (" + callUpdate.userUid + ")");
                notifyListeners_callUpdate(callUpdate);
            }

        }

        #endregion


        private void log(string message)
        {
            if (logger != null) logger.write("[Call Client] " + message);
        }

        private void notifyListeners_connect(int status)
        {
            int i = 0;
            while (i < listeners.Count)
            {
                ((CallClientListener)listeners[i]).callClient_connect(status);
                i++;
            }

        }

        private void notifyListeners_profileUpdate(ProfileUpdate profileUpdate)
        {
            int i = 0;
            while (i < listeners.Count)
            {
                ((CallClientListener)listeners[i]).callClient_profileUpdate(profileUpdate);
                i++;
            }

        }

        private void notifyListeners_sessionUpdate(SessionUpdate sessionUpdate)
        {
            int i = 0;
            while (i < listeners.Count)
            {
                ((CallClientListener)listeners[i]).callClient_sessionUpdate(sessionUpdate);
                i++;
            }

        }

        private void notifyListeners_callUpdate(CallUpdate callUpdate)
        {
            int i = 0;
            while (i < listeners.Count)
            {
                ((CallClientListener)listeners[i]).callClient_callUpdate(callUpdate);
                i++;
            }

        }

        public void actionDial(string number)
        {
            CallAction callAction = capClient.createCallAction("Dial");
            callAction.addActionParameter("Number", number);

            capClient.sendMessage(callAction);
        }

        public void actionAnswer(string callId)
        {
            CallAction callAction = capClient.createCallAction("Answer");
            callAction.addActionParameter("CallId", callId);

            capClient.sendMessage(callAction);
        }

        public void actionTransfer(string callId, string number)
        {
            CallAction callAction = capClient.createCallAction("Xfer");
            callAction.addActionParameter("CallId", callId);
            callAction.addActionParameter("Number", number);

            capClient.sendMessage(callAction);
        }

        public void actionTransferConsultant(string callId1, string callId2)
        {
            CallAction callAction = capClient.createCallAction("XferConsult");
            callAction.addActionParameter("CallId", callId1);
            callAction.addActionParameter("CallId", callId2);

            capClient.sendMessage(callAction);
        }


        public void actionHold(string callId)
        {
            CallAction callAction = capClient.createCallAction("Hold");
            callAction.addActionParameter("CallId", callId);

            capClient.sendMessage(callAction);
        }

        public void actionRelease(string callId)
        {
            CallAction callAction = capClient.createCallAction("Release");
            callAction.addActionParameter("CallId", callId);

            capClient.sendMessage(callAction);
        }

        public void actionConference(string[] callId)
        {
            CallAction callAction = capClient.createCallAction("ConfStart");
            int i = 0;
            while (i < callId.Length)
            {
                callAction.addActionParameter("CallId", callId[i]);
                i++;
            }
            capClient.sendMessage(callAction);

        }
    }
}
