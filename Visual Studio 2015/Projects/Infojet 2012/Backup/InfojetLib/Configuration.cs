using System;
using Microsoft.Win32;
using System.Configuration;

namespace Navipro.Infojet.Lib
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
        public string webSiteAddress;
        public bool fancyLinks;
        public bool nav2013mode;
        public bool debugMode;

		public Configuration()
		{
		}

		public bool init()
		{
			this.webSiteCode = ConfigurationSettings.AppSettings["WEB_WebSiteCode"];
			this.companyName = ConfigurationSettings.AppSettings["WEB_CompanyName"];

			this.smtpServer = ConfigurationSettings.AppSettings["SMTP_ServerName"];
			this.smtpPort = int.Parse(ConfigurationSettings.AppSettings["SMTP_ServerPort"]);
			this.smtpAuthenticate = int.Parse(ConfigurationSettings.AppSettings["SMTP_Authenticate"]);
			this.smtpUserName = ConfigurationSettings.AppSettings["SMTP_UserName"];
			this.smtpPassword = ConfigurationSettings.AppSettings["SMTP_Password"];

			this.smtpSender = ConfigurationSettings.AppSettings["SMTP_FromAddress"];

			this.wsAddress = ConfigurationSettings.AppSettings["WS_Address"];

            try
            {
                this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationSettings.AppSettings["DB_ConnectionStringName"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationSettings.AppSettings["DB_ConnectionStringName"] + " is misssing.");
            }

            this.webSiteAddress = ConfigurationSettings.AppSettings["WEB_Address"];

            if (ConfigurationSettings.AppSettings["WEB_FancyLinks"].ToString() == "true") fancyLinks = true;

            try
            {
                if (ConfigurationSettings.AppSettings["NAV_Version"].ToString() == "2013") nav2013mode = true;
            }
            catch (Exception) { }

            try
            {
                if (ConfigurationSettings.AppSettings["WEB_Debug"].ToString() == "true") debugMode = true;
            }
            catch (Exception) { }

			return true;
		}


	}
}
