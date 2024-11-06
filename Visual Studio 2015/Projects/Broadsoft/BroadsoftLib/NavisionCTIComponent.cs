using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime;
using Navipro.BroadSoft.Lib;

namespace Navipro.WorkAnyWhere.Navision
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    /// 

    class Guids
    {
        public const string coclsguid = "E03D715B-A13F-4cff-92F1-0319ADB3EF5F";
        public const string intfguid = "D030D214-C984-496a-87E7-31732C114F1E";
        public const string eventguid = "D030D214-C984-496a-87E7-31732C114F1F";

        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ICTIComponent
    {
        [DispId(1)]
        bool init(string serverName, int port, string userName, string password);
        [DispId(2)]
        string checkForIncomingCall();
        [DispId(3)]
        void dial(string phoneNo);
        [DispId(4)]
        void monitorUser(string userName);
        [DispId(5)]
        void startMonitor();
        [DispId(6)]
        void getMonitoredUserStatus(string userName, out int state, out bool dnd, out string cpe, out bool cfa, out string cfaDestination, out bool offHook);
        [DispId(7)]
        void setDebug(bool debug);
    }

    [Guid(Guids.coclsguid), ProgId("Navipro.WorkAnywhere.Navision.CTIComponent"), ClassInterface(ClassInterfaceType.None)]
    public class CTIComponent : ICTIComponent, CallClientListener, AttendantConsoleListener, Logger 
    {

        private CallClient callClient;
        private AttendantConsole attendantConsole;

        private string currentPhoneNo;
        private bool newCall;
        private Hashtable monitoredUserInfo;
        private ArrayList monitoredUserList;

        private bool debug;

        public CTIComponent()
        {

            //
            // TODO: Add constructor logic here
            //
            monitoredUserInfo = new Hashtable();
            monitoredUserList = new ArrayList();
        }

        public bool init(string serverName, int port, string userName, string password)
        {
            callClient = new CallClient(serverName, port, userName, password, this, "DNAV-CC-" + userName);
            callClient.registerListener(this);
            callClient.login();

            attendantConsole = new AttendantConsole(serverName, port, userName, password, this, "DNAV-AC-" + userName);
            attendantConsole.registerListener(this);
            return true;
        }

        public void monitorUser(string userName)
        {
            this.monitoredUserInfo.Add(userName, new MonitoredUser(userName));
            this.monitoredUserList.Add(userName);
        }

        public void startMonitor()
        {
            attendantConsole.addMonitoringUsers(monitoredUserList);
            attendantConsole.login();
        }

        public void getMonitoredUserStatus(string userName, out int state, out bool dnd, out string cpe, out bool cfa, out string cfaDestination, out bool offHook)
        {
            state = 0;
            dnd = false;
            cpe = "";
            cfa = false;
            cfaDestination = "";
            offHook = false;

            MonitoredUser monitoredUser = (MonitoredUser)monitoredUserInfo[userName];
            if (monitoredUser != null)
            {
                state = monitoredUser.state;
                dnd = monitoredUser.dnd;
                cpe = monitoredUser.cpe;
                cfa = monitoredUser.cfa;
                cfaDestination = monitoredUser.cfaDestination;
                offHook = monitoredUser.offHook;
                
            }

        }

        public string checkForIncomingCall()
        {
            if (newCall)
            {
                newCall = false;
                return currentPhoneNo;
            }

            return "";
        }

        public void dial(string phoneNo)
        {
            callClient.actionDial(phoneNo);
        }

        public void setDebug(bool debug)
        {
            this.debug = debug;
        }

        #region CallClientListener Members

        public void callClient_callUpdate(CallUpdate callUpdate)
        {
            // TODO:  Add About.callClient_callUpdate implementation

            if (callUpdate.state == 2)
            {
                currentPhoneNo = callUpdate.remoteNumber;
                newCall = true;
            }

            if ((callUpdate.state == 5) || (callUpdate.state == 6))
            {
                currentPhoneNo = "";
                newCall = false;
            }


        }

        public void callClient_connect(int status)
        {
            // TODO:  Add About.callClient_connect implementation
        }

        public void callClient_profileUpdate(ProfileUpdate profileUpdate)
        {
            // TODO:  Add About.callClient_profileUpdate implementation


        }

        public void callClient_sessionUpdate(SessionUpdate sessionUpdate)
        {
            // TODO:  Add About.callClient_sessionUpdate implementation

        }

        #endregion

        #region AttendantConsoleListener Members

        public void attendantConsole_connect(int status)
        {
        }

        public void attendantConsole_callUpdate(CallUpdate callUpdate)
        {
            MonitoredUser monitoredUser = (MonitoredUser)monitoredUserInfo[callUpdate.monitoredUserId];

            monitoredUser.state = callUpdate.state;
        }

        public void attendantConsole_profileUpdate(ProfileUpdate profileUpdate)
        {
            MonitoredUser monitoredUser = (MonitoredUser)monitoredUserInfo[profileUpdate.monitoredUserId];
            
            monitoredUser.dnd = false;
            if (profileUpdate.dnd.ToUpper() == "ON") monitoredUser.dnd = true;
            monitoredUser.cpe = profileUpdate.cpe;

            monitoredUser.cfa = false;
            if (profileUpdate.cfa.ToUpper() == "ON") monitoredUser.cfa = true;
            monitoredUser.cfaDestination = profileUpdate.cfaDestination;
               
        }

        public void attendantConsole_sessionUpdate(SessionUpdate sessionUpdate)
        {
            MonitoredUser monitoredUser = (MonitoredUser)monitoredUserInfo[sessionUpdate.monitoredUserId];

            monitoredUser.offHook = sessionUpdate.offHook;
        }

        #endregion

        #region Logger Members

        public void write(string message)
        {
            if (this.debug)
            {
                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("C:\\callManager.log", true);
                streamWriter.WriteLine(message);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        #endregion
    }

    public class MonitoredUser
    {
        public string userName;
        public int state;
        public bool dnd;
        public string cpe;
        public bool offHook;
        public bool cfa;
        public string cfaDestination;

        public MonitoredUser(string userName)
        {
            this.userName = userName;
        }
    }
}
