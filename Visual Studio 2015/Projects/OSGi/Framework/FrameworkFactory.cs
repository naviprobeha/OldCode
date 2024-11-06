using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Navipro.OSGi.Interfaces;

namespace Navipro.OSGi.Framework
{
    public class FrameworkFactory : MarshalByRefObject, OSGiFramework
    {
        private bool _running;
        private Thread _thread;

        private ConsoleHandler _consoleHandler;
        private ModuleHandler _moduleHandler;
        private ConfigurationHandler _configurationHandler;
        private Configuration _configuration;

        public FrameworkFactory()
        {
            _consoleHandler = new ConsoleHandler(this);
            _moduleHandler = new ModuleHandler(this);
            _configurationHandler = new ConfigurationHandler(this);
            _configuration = _configurationHandler.load();

            _thread = new Thread(new ParameterizedThreadStart(run));
            _running = true;
            _thread.Start();
        }


        public void run(object parameters)
        {
            while (_running)
            {

                Thread.Sleep(1000);
            }

        }

        public void stop()
        {
            _running = false;

            _moduleHandler.stop();
            _consoleHandler.stop();
        }

        public void writeToLog(string moduleName, int level, string message)
        {
            _consoleHandler.logWrite(moduleName, level, message);
        }

        public ModuleHandler moduleHandler { get { return _moduleHandler; } }
        public Configuration getConfiguration()
        {
            return _configuration;
        }
    }
}
