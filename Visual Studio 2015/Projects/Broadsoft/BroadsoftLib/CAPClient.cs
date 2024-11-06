using System;
using System.IO;
using System.Xml;
using System.Threading;
using System.Net.Sockets;
using System.Collections;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>

    public class CAPClient
    {
        public static int CALL_CLIENT = 0;
        public static int ATTENDANT_CONSOLE = 1;

        private string serverName;
        private int port;
        private string userName;
        private string password;
        private string userUidValue;

        private TcpClient tcpClient;
        private NetworkStream netStream;
        private Thread thread;
        private bool running;
        private string userType;
        private string applicationId;

        private ArrayList listeners;


        public CAPClient(string serverName, int port, int type, string userName, string password, string applicationId)
        {
            //
            // TODO: Add constructor logic here
            //
            this.serverName = serverName;
            this.port = port;
            this.userName = userName;
            this.password = password;

            if (type == 1)
            {
                userType = "AttendantConsole";
            }
            else
            {
                userType = "CallClient";
            }

            this.applicationId = applicationId;

            tcpClient = new TcpClient();

            listeners = new ArrayList();

        }

        public bool connect()
        {
            tcpClient.Connect(serverName, port);

            netStream = tcpClient.GetStream();

            running = true;

            thread = new Thread(new ThreadStart(run));
            thread.Start();


            return true;
        }

        public void run()
        {
            try
            {
                DateTime lastKeepAliveSent = DateTime.Now;

                StreamReader streamReader = new StreamReader(netStream);
                string receiveBuffer = "";

                while (running)
                {
                    if (netStream.DataAvailable)
                    {

                        int character = streamReader.Read();
                        while ((character > 0) && ((receiveBuffer + ((char)character)).IndexOf("</BroadsoftDocument>") == -1))
                        {
                            receiveBuffer = receiveBuffer + ((char)character);
                            character = streamReader.Read();
                        }
                        if (character > 0)
                        {
                            receiveBuffer = receiveBuffer + ((char)character);
                        }

                        receiveBuffer = receiveBuffer.Substring(receiveBuffer.IndexOf("<BroadsoftDocument"));

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(receiveBuffer);
                        notifyListeners(xmlDoc);

                        receiveBuffer = "";

                    }

                    Thread.Sleep(50);

                    if (lastKeepAliveSent.AddMinutes(5) < DateTime.Now)
                    {
                        sendKeepAlive();
                        lastKeepAliveSent = DateTime.Now;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + " (" + e.StackTrace + ")");
            }
        }

        public void sendMessage(BroadSoftMessage message)
        {
            StreamWriter streamWriter = new StreamWriter(netStream);
            streamWriter.Write(message.toDOM().OuterXml + "\r\n");
            streamWriter.Flush();

        }

        public void registerListener(CAPListener capListener)
        {
            listeners.Add(capListener);
        }

        private void notifyListeners(XmlDocument xmlDocument)
        {

            if (getMessageType(xmlDocument) == "registerResponse")
            {
                RegisterResponse registerResponse = new RegisterResponse(xmlDocument);
                this.userUidValue = registerResponse.userUid;
            }

            int i = 0;
            while (i < listeners.Count)
            {
                ((CAPListener)listeners[i]).notify(getMessageType(xmlDocument), xmlDocument);
                i++;
            }
        }

        public void close()
        {
            sendMessage(new UnRegister(userName, userType, this.applicationId));

            running = false;
            tcpClient.Close();

        }

        public string getMessageType(XmlDocument xmlDoc)
        {
            XmlElement docElement = xmlDoc.DocumentElement;
            XmlElement commandElement = (XmlElement)docElement.SelectSingleNode("command");
            if (commandElement != null)
            {
                if (commandElement.GetAttribute("commandType") != null)
                {
                    return commandElement.GetAttribute("commandType");
                }

            }

            return null;
        }

        public void authenticationRequest()
        {
            BroadSoftMessage message = new RegisterAuthentication(userName, userType, this.applicationId);
            sendMessage(message);
        }

        public void registerRequest(ResponseAuthentication responseAuthentication)
        {
            MessageDigest messageDigest = new MessageDigest();
            string hashedPassword = messageDigest.SHAEncode(password);
            string noncedPassword = messageDigest.MD5Encode(responseAuthentication.nonce + ":" + hashedPassword);

            BroadSoftMessage message = new RegisterRequest(responseAuthentication.id, noncedPassword, userType, this.applicationId);
            sendMessage(message);
        }

        public void acknowledgeMessage(string messageName)
        {
            BroadSoftMessage message = new Acknowledgement(userName, userType, messageName, this.applicationId);

            sendMessage(message);

        }

        public string userUid
        {
            get
            {
                return userUidValue;
            }
        }

        public CallAction createCallAction(string actionType)
        {
            return new CallAction(this.userName, this.userType, actionType, this.applicationId);
        }

        public void sendKeepAlive()
        {
            ServerStatusRequest serverStatusRequest = new ServerStatusRequest(this.userUid, this.userType, this.applicationId);
            sendMessage(serverStatusRequest);
        }

        public MonitoringUsersRequest createMonitoringUsersRequest()
        {
            return new MonitoringUsersRequest(userName, userType, applicationId);
        }
    }
}
