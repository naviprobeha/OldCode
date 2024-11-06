using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for AttendantConsole.
    /// </summary>
    public class AttendantConsole : CAPListener
    {

        private CAPClient capClient;
        private ArrayList listeners;
        private ArrayList monitoringUsersList;
        private Logger logger;
        private string userUid;
        private string applicationId;

        public AttendantConsole(string serverName, int port, string userName, string password, Logger logger, string applicationId)
        {

            this.applicationId = applicationId;

            listeners = new ArrayList();
            monitoringUsersList = new ArrayList();
            capClient = new CAPClient(serverName, port, CAPClient.ATTENDANT_CONSOLE, userName, password, applicationId);
            capClient.registerListener(this);
            capClient.connect();

            this.logger = logger;
        }

        public void addMonitoringUsers(ArrayList userList)
        {
            this.monitoringUsersList = userList;
        }

        private void sendMonitoringUsersCommand()
        {
            MonitoringUsersRequest monitoringUsersRequest = capClient.createMonitoringUsersRequest();

            int i = 0;
            while (i < monitoringUsersList.Count)
            {
                monitoringUsersRequest.add(monitoringUsersList[i].ToString());

                i++;
            }

            capClient.sendMessage(monitoringUsersRequest);

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

        public void registerListener(AttendantConsoleListener attendantConsoleListener)
        {
            listeners.Add(attendantConsoleListener);
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
                log("SessionUpdate for " + sessionUpdate.monitoredUserId + " (" + sessionUpdate.monitoredUserUid + "). OffHook: " + sessionUpdate.offHook + ", NumCalls: " + sessionUpdate.numCalls);
                notifyListeners_sessionUpdate(sessionUpdate);
            }

            if (docType == "profileUpdate")
            {
                ProfileUpdate profileUpdate = new ProfileUpdate(xmlDocument);
                log("ProfileUpdate for " + profileUpdate.monitoredUserId + " (" + profileUpdate.monitoredUserUid + ")");
                notifyListeners_profileUpdate(profileUpdate);
            }

            if (docType == "callUpdate")
            {
                CallUpdate callUpdate = new CallUpdate(xmlDocument);
                log("CallUpdate for " + callUpdate.monitoredUserId + " (" + callUpdate.monitoredUserUid + ")");
                notifyListeners_callUpdate(callUpdate);
            }

            if (docType == "monitoringUsersResponse")
            {
                log("Monitor Response: " + xmlDocument.OuterXml);
            }

        }

        #endregion


        private void log(string message)
        {
            if (logger != null) logger.write("[Attendant Console] " + message);
        }

        private void notifyListeners_connect(int status)
        {
            int i = 0;
            while (i < listeners.Count)
            {
                ((AttendantConsoleListener)listeners[i]).attendantConsole_connect(status);
                i++;
            }

            if (this.monitoringUsersList.Count > 0) this.sendMonitoringUsersCommand();

        }

        private void notifyListeners_profileUpdate(ProfileUpdate profileUpdate)
        {
            int i = 0;
            while (i < listeners.Count)
            {
                ((AttendantConsoleListener)listeners[i]).attendantConsole_profileUpdate(profileUpdate);
                i++;
            }

        }

        private void notifyListeners_sessionUpdate(SessionUpdate sessionUpdate)
        {
            int i = 0;
            while (i < listeners.Count)
            {
                ((AttendantConsoleListener)listeners[i]).attendantConsole_sessionUpdate(sessionUpdate);
                i++;
            }

        }

        private void notifyListeners_callUpdate(CallUpdate callUpdate)
        {
            int i = 0;
            while (i < listeners.Count)
            {
                ((AttendantConsoleListener)listeners[i]).attendantConsole_callUpdate(callUpdate);
                i++;
            }

        }

    }
}
