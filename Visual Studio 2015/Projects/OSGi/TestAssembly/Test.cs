using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Navipro.OSGi.Interfaces;

namespace TestAssembly
{
    public class Test : MarshalByRefObject, OSGiModule
    {
        private OSGiLogger _logger;
        private OSGiFramework _framework;
        private Thread thread;
        private bool running;

        #region OSGiModule Members
        

        public void load(Configuration configuration, OSGiFramework framework, OSGiLogger logger)
        {
            _logger = logger;
            _framework = framework;
            _logger.write(1, "Loading...");

        }

        public void start()
        {
            running = true;
            _logger.write(1, "Starting...");
            thread = new Thread(new ThreadStart(run));
            thread.Start();
        }

        public void run()
        {
            while (running)
            {
                _logger.write(2, "Running");

                Thread.Sleep(5000);
            }
            _logger.write(1, "Stopped");
        }

        public void stop()
        {
            _logger.write(1, "Stopping...");
            running = false;
        }

        public string getStatus()
        {
            if (running) return "Started";
            return "Stopped";
        }

        public string sendMessage(string message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
