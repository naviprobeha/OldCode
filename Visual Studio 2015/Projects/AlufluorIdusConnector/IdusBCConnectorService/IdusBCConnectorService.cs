using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NaviPro.Alufluor.Idus.Library;
using NaviPro.Alufluor.Idus.Library.Helpers;

namespace IdusBCConnectorService
{
    public partial class IdusBCConnectorService : ServiceBase, Logger
    {
        private IdusIntegration integration;
        
        public IdusBCConnectorService()
        {

            InitializeComponent();

            integration = new IdusIntegration(this);
        }

        public void write(string type, string message)
        {
            this.EventLog.WriteEntry(message);
        }

        protected override void OnStart(string[] args)
        {
            integration.start();
        }

        protected override void OnStop()
        {
            integration.stop();
        }


    }
}
