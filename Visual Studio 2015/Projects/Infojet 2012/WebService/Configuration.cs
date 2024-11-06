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
        public string connectionString;
        public string companyName;
        public string wsAddress;
        public string version;

		public Configuration()
		{

		}

		public bool init()
		{
			this.msmqInQueue = ConfigurationSettings.AppSettings["MSMQ_RequestQueue"];
			this.msmqOutQueue = ConfigurationSettings.AppSettings["MSMQ_ResponseQueue"];
			this.msmqTimeout = int.Parse(ConfigurationSettings.AppSettings["MSMQ_TimeOut"]);

            try
            {
                this.version = ConfigurationSettings.AppSettings["NAV_Version"];

                if (version == "2013")
                {
                    try
                    {

                        this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationSettings.AppSettings["DB_ConnectionStringName"]].ConnectionString;
                    }
                    catch (Exception)
                    {
                        throw new Exception("Connection string " + ConfigurationSettings.AppSettings["DB_ConnectionStringName"] + " is misssing.");
                    }
                    try
                    {

                        this.companyName = ConfigurationSettings.AppSettings["WEB_CompanyName"];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Companyname is misssing.");
                    }
                    try
                    {

                        this.wsAddress = ConfigurationSettings.AppSettings["WS_Address"];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Webservice address is misssing.");
                    }

                }

            }
            catch (Exception) { };

			return true;
		}

	}
}