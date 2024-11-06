using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace Navipro.OSGi.Framework
{
    public class ConsoleConnection
    {
        private CommandHandler _commandHandler;
        private FrameworkFactory _loader;
        private TcpClient _tcpClient;
        private NetworkStream _netStream;
        private Thread _thread;
        private System.IO.StreamWriter _streamWriter;

        public ConsoleConnection(FrameworkFactory loader, TcpClient tcpClient)
        {
            this._loader = loader;
            this._tcpClient = tcpClient;
            this._commandHandler = new CommandHandler(_loader, this);

            _netStream = _tcpClient.GetStream();

            _thread = new Thread(new ThreadStart(run));
            _thread.Start();

            _streamWriter = new System.IO.StreamWriter(_netStream);
            _streamWriter.WriteLine("Navipro.OSGi.Framework, 2013.");
            _streamWriter.WriteLine("");
            _streamWriter.Flush();
            writePrompt();
        }

        public void run()
        {
            try
            {
                string command = "";

                while (_tcpClient.Connected)
                {
                    if (_netStream.DataAvailable)
                    {
                        int byteData = _netStream.ReadByte();
                        if ((byteData != 13) && (byteData != 10))
                        {
                            command = command + (char)byteData;
                        }
                        else
                        {
                            if (byteData != 10)
                            {
                                _commandHandler.executeCommand(command);
                                command = "";
                                writePrompt();
                            }
                        }


                    }
                    Thread.Sleep(50);
                }
            }
            catch (Exception)
            {

            }
        }

        public void disconnect()
        {
            _tcpClient.Close();
        }

        public void write(string text)
        {
            try
            {
                _streamWriter.WriteLine(text);
                _streamWriter.Flush();
            }
            catch (Exception) { }
        }

        private void writePrompt()
        {
            try
            {
                _streamWriter.Write("OSGi> ");
                _streamWriter.Flush();
            }
            catch (Exception) { }
        }

        public void logWrite(string moduleName, int level, string message)
        {
            try
            {
                write("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "][" + moduleName + "][" + level + "] " + message);
            }
            catch (Exception) { }
        }
    }
}
