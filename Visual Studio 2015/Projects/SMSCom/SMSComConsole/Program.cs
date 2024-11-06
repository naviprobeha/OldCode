using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace SMSComConsole
{
    class Program : Navipro.Base.Common.Logger
    {
        Navipro.SMSCom.Lib.SMSCom smsCom;

        public Program()
        {

            Navipro.Base.Common.Configuration config = new Navipro.Base.Common.Configuration();
            
            System.Configuration.AppSettingsReader appSettingsReader = new AppSettingsReader();
            config.setConfigValue("gsm_port", (string)appSettingsReader.GetValue("gsm_port", typeof(string)));
            config.setConfigValue("database", (string)appSettingsReader.GetValue("database", typeof(string)));
            config.setConfigValue("serverName", (string)appSettingsReader.GetValue("serverName", typeof(string)));
            config.setConfigValue("userName", (string)appSettingsReader.GetValue("userName", typeof(string)));
            config.setConfigValue("password", (string)appSettingsReader.GetValue("password", typeof(string)));


            smsCom = new Navipro.SMSCom.Lib.SMSCom(this, config);

            
        }

        public void stop()
        {
            smsCom.stop();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            Console.ReadLine();
            program.stop();
        }

        #region Logger Members

        public void write(string source, int level, string message)
        {
            Console.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " [" + source + "] " + message);
        }

        #endregion
    }
}
