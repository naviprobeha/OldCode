using System;
using Microsoft.Win32;
using System.Configuration;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	public class Configuration
	{
		public string serverName;
		public string dataSource;
		public string userName;
		public string password;
		public bool debug;
		public string smtpServer;
		public string smtpSender;
		public int serverPort;
		public string mapServerUrl;
		public string mapClientUrl;
		public string mapAccount;
		public string mapPassword;
		public string planExePath;
		public string planReturnFilePath;

		private string errorMessageValue;

		public Configuration()
		{
		}

		public Configuration(string serverNameValue, string dataSourceValue, string userNameValue, string passwordValue)
		{
			this.serverName = serverNameValue;
			this.dataSource = dataSourceValue;
			this.userName = userNameValue;
			this.password = passwordValue;

		}

		public bool initWeb()
		{
			dataSource = ConfigurationSettings.AppSettings["DataSource"];
			serverName = ConfigurationSettings.AppSettings["ServerName"];
			userName = ConfigurationSettings.AppSettings["UserName"];
			password = ConfigurationSettings.AppSettings["Password"];
			mapAccount = ConfigurationSettings.AppSettings["MapAccount"];
			mapClientUrl = ConfigurationSettings.AppSettings["MapClientUrl"];
			mapPassword = ConfigurationSettings.AppSettings["MapPassword"];
			mapServerUrl = ConfigurationSettings.AppSettings["MapServerUrl"];
			smtpSender = ConfigurationSettings.AppSettings["SmtpSender"];
			smtpServer = ConfigurationSettings.AppSettings["SmtpServer"];

			return true;
		}

		public bool init()
		{
			try
			{
				RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Navipro\\SantaMonica");
				if (regKey == null) throw new Exception("Registry not configured: "+Registry.LocalMachine.Name);

				serverPort = int.Parse(regKey.GetValue("ServerPort").ToString());
				if (int.Parse(regKey.GetValue("Debug").ToString()) == 1) debug = true;


				regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Navipro\\SantaMonica\\Database Connection");
				serverName = regKey.GetValue("Servername").ToString();
				dataSource = regKey.GetValue("Datasource").ToString();
				userName = regKey.GetValue("Username").ToString();
				password = regKey.GetValue("Password").ToString();

				regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Navipro\\SantaMonica\\SMTP");
				smtpServer = regKey.GetValue("SMTPServer").ToString();
				smtpSender = regKey.GetValue("SMTPSender").ToString();

				regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Navipro\\SantaMonica\\MapServer");
				mapServerUrl = regKey.GetValue("ServerUrl").ToString();
				mapClientUrl = regKey.GetValue("ClientUrl").ToString();
				mapAccount = regKey.GetValue("AccountNo").ToString();
				mapPassword = regKey.GetValue("Password").ToString();

				regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Navipro\\SantaMonica\\RoutePlanning");
				planExePath = regKey.GetValue("ExePath").ToString();
				planReturnFilePath = regKey.GetValue("ReturnFilePath").ToString();

				return true;
			}
			catch(Exception e)
			{
				errorMessageValue = e.Message + " ("+e.StackTrace+")";
				return false;
			}
		}

		public string getErrorMessage()
		{
			return this.errorMessageValue;
		}
	}
}
