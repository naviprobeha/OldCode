using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Navipro.SMSCom.Lib;
using Navipro.Base.Common;

namespace SMSComService
{
    public partial class SMSComService : ServiceBase, Logger
    {
        private SMSCom smsCom;

        public SMSComService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Navipro.Base.Common.Configuration config = new Navipro.Base.Common.Configuration();

            System.Configuration.AppSettingsReader appSettingsReader = new AppSettingsReader();
            config.setConfigValue("gsm_port", (string)appSettingsReader.GetValue("gsm_port", typeof(string)));
            config.setConfigValue("database", (string)appSettingsReader.GetValue("database", typeof(string)));
            config.setConfigValue("serverName", (string)appSettingsReader.GetValue("serverName", typeof(string)));
            config.setConfigValue("userName", (string)appSettingsReader.GetValue("userName", typeof(string)));
            config.setConfigValue("password", (string)appSettingsReader.GetValue("password", typeof(string)));


            smsCom = new SMSCom(this, config);
            
        }

        protected override void OnStop()
        {
            smsCom.stop();
        }

        #region Logger Members

        public void write(string source, int level, string message)
        {
            if (level == 0)
            {
                this.EventLog.WriteEntry("["+source+"] "+message, EventLogEntryType.Information);
            }
            if (level == 1)
            {
                this.EventLog.WriteEntry("[" + source + "] " + message, EventLogEntryType.Warning);
            }
            if (level == 2)
            {
                this.EventLog.WriteEntry("[" + source + "] " + message, EventLogEntryType.Error);
            }
        }

        #endregion
    }
}
