using System;
using Microsoft.Win32;
using System.Configuration;

namespace Navipro.Cashjet.Library
{
    /// <summary>
    /// Summary description for Configuration.
    /// </summary>
    public class Configuration
    {
        public string webSiteCode;
        public string connectionString;
        public string dataSource;
        public string userName;
        public string password;
        public string smtpServer;
        public int smtpPort;
        public int smtpAuthenticate;
        public string smtpUserName;
        public string smtpPassword;
        public string smtpSender;
        public string companyName;
        public string wsAddress;
        public string language = "";
        public string version = "";
        public string allowZeroVat = "";

        public Configuration()
        {
        }

        public bool init()
        {
            this.webSiteCode = ConfigurationSettings.AppSettings["WEB_WebSiteCode"];
            this.companyName = ConfigurationSettings.AppSettings["WEB_CompanyName"];
            this.version = ConfigurationSettings.AppSettings["WEB_Version"];

            this.smtpServer = ConfigurationSettings.AppSettings["SMTP_ServerName"];
            this.smtpPort = int.Parse(ConfigurationSettings.AppSettings["SMTP_ServerPort"]);
            this.smtpAuthenticate = int.Parse(ConfigurationSettings.AppSettings["SMTP_Authenticate"]);
            this.smtpUserName = ConfigurationSettings.AppSettings["SMTP_UserName"];
            this.smtpPassword = ConfigurationSettings.AppSettings["SMTP_Password"];

            this.smtpSender = ConfigurationSettings.AppSettings["SMTP_FromAddress"];

            this.wsAddress = ConfigurationSettings.AppSettings["WS_Address"];


            try
            {
                this.language = ConfigurationSettings.AppSettings["Language"];
            }
            catch (Exception) { }

            try
            {
                this.allowZeroVat = ConfigurationSettings.AppSettings["AllowZeroVat"];
            }
            catch (Exception) { }
            try
            {
                this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationSettings.AppSettings["DB_ConnectionStringName"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationSettings.AppSettings["DB_ConnectionStringName"] + " is misssing.");
            }


            return true;
        }


    }
}
