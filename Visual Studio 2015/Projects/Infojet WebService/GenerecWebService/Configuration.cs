using System;
using Microsoft.Win32;
using System.Configuration;

namespace Navipro.Infojet.WebService
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	public class Configuration
	{
		public string msmqInQueue;
		public string msmqOutQueue;
		public int msmqTimeout;
        public string wsAddress;


		public Configuration()
		{

		}

		public bool init()
		{
			this.msmqInQueue = ConfigurationSettings.AppSettings["MSMQ_RequestQueue"];
			this.msmqOutQueue = ConfigurationSettings.AppSettings["MSMQ_ResponseQueue"];
			this.msmqTimeout = int.Parse(ConfigurationSettings.AppSettings["MSMQ_TimeOut"]);
            this.wsAddress = ConfigurationSettings.AppSettings["WS_Address"];

			return true;
		}

	}
}