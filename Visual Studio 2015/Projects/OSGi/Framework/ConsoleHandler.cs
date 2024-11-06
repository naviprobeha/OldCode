using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Navipro.OSGi.Framework
{
    
    public class ConsoleHandler
    {
        private FrameworkFactory _loader;
        private TcpListener _tcpListener;
        private Thread _thread;
        private bool _running;
        private ConsoleCollection _consoleCollection;

        public ConsoleHandler(FrameworkFactory loader)
        {
            _loader = loader;

            _consoleCollection = new ConsoleCollection();
            _running = true;

            _tcpListener = new TcpListener(System.Net.IPAddress.Any, 8899);
            _tcpListener.Start();

            _thread = new Thread(new ThreadStart(run));
            _thread.Start();

        }

        public void run()
        {

            while (_running)
            {
                if (_tcpListener.Pending())
                {
                    TcpClient tcpClient = _tcpListener.AcceptTcpClient();
                    ConsoleConnection consoleConnection = new ConsoleConnection(_loader, tcpClient);
                    _consoleCollection.Add(consoleConnection);

                }
                Thread.Sleep(100);
            }

            _tcpListener.Stop();
        }

        public void stop()
        {
            _running = false;

            int i = 0;
            while (i < _consoleCollection.Count)
            {
                _consoleCollection[i].disconnect();
                _consoleCollection.Remove(_consoleCollection[i]);
                i++;
            }
        }

        public void logWrite(string moduleName, int level, string message)
        {
            int i = 0;
            while (i < _consoleCollection.Count)
            {
                try
                {
                    _consoleCollection[i].logWrite(moduleName, level, message);
                }
                catch (Exception)
                {
                    _consoleCollection.Remove(_consoleCollection[i]);
                }
                i++;
            }

        }
        
    }
}
