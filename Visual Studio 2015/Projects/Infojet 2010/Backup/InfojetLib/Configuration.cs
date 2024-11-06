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
		public string transportMethod;
		public string msmqInQueue;
		public string msmqOutQueue;
		public string webServiceUrl;
		public string companyName;
		public int msmqTimeout;
		public string tcpServerAddress;
		public int tcpServerPort;
		public string wsAddress;
		public bool debug;

		public Configuration()
		{
            msmqInQueue = "";
            msmqOutQueue = "";

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

			this.transportMethod = ConfigurationSettings.AppSettings["COM_TransportMethod"];

			this.msmqInQueue = ConfigurationSettings.AppSettings["MSMQ_RequestQueue"];
			this.msmqOutQueue = ConfigurationSettings.AppSettings["MSMQ_ResponseQueue"];
			this.msmqTimeout = int.Parse(ConfigurationSettings.AppSettings["MSMQ_TimeOut"]);

			this.tcpServerAddress = ConfigurationSettings.AppSettings["TCP_ServerAddress"];
			this.tcpServerPort = int.Parse(ConfigurationSettings.AppSettings["TCP_ServerPort"]);

			this.wsAddress = ConfigurationSettings.AppSettings["WS_Address"];

            this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationSettings.AppSettings["DB_ConnectionStringName"]].ConnectionString;

			return true;
		}


	}
}
