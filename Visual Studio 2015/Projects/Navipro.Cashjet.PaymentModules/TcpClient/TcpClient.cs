using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Navipro.Cashjet.TcpClient
{
    public class TcpClient
    {
        private System.Net.Sockets.TcpClient tcpClient;        
        private System.Threading.Thread thread;
        private bool running;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private string buffer = "";

        public delegate void DataReceivedEventHandler(string text);
        public event DataReceivedEventHandler onDataReceivedEvent;
 
        public TcpClient(string ipAddress, int port)
        {
            tcpClient = new System.Net.Sockets.TcpClient();
            thread = new System.Threading.Thread(new System.Threading.ThreadStart(run));
            
            tcpClient.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));
            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter = new StreamWriter(tcpClient.GetStream());

            running = true;
            thread.Start();
        }



        public void close()
        {

            running = false;
            tcpClient.Close();
        }

        public void run()
        {

            while (running)
            {
                try
                {

                    int character = streamReader.Read();
                    if (character > -1)
                    {
                        buffer = buffer + (char)character;
                        string message = testXml(ref buffer);
                        if (message != "")
                        {
                            raiseOnDataReceivedEvent(message);
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    raiseOnDataReceivedEvent("<error>" + e.ToString() + "</error>");
                }

                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(50);
            }

        }

        public void send(string buffer)
        {
            streamWriter.WriteLine(buffer);
            streamWriter.Flush();

        }

        protected virtual void raiseOnDataReceivedEvent(string text)
        {
            DataReceivedEventHandler handler = onDataReceivedEvent;
            if (handler != null)
            {
                handler(text);
            }
        }

        private string testXml(ref string bufferString)
        {
            if (bufferString.IndexOf("</bpti>") > 0)
            {
                string message = bufferString.Substring(0, bufferString.IndexOf("</bpti>") + 7);
                bufferString = bufferString.Substring(bufferString.IndexOf("</bpti>") + 7);
                return message;
            }

            return "";
            
 
        }
    }
}
