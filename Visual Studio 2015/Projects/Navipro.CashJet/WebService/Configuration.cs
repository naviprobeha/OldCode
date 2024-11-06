using System;
using Microsoft.Win32;
using System.Configuration;

namespace Navipro.CashJet.WebService
{
    /// <summary>
    /// Summary description for Configuration.
    /// </summary>
    public class Configuration
    {
        public string msmqInQueue;
        public string msmqOutQueue;
        public int msmqTimeout;
        public string dbConnection;
        public string companyName;

        public string connectionString;


        public Configuration()
        {

        }

        public bool init()
        {
            this.msmqInQueue = ConfigurationSettings.AppSettings["MSMQ_RequestQueue"];
            this.msmqOutQueue = ConfigurationSettings.AppSettings["MSMQ_ResponseQueue"];
            this.msmqTimeout = int.Parse(ConfigurationSettings.AppSettings["MSMQ_TimeOut"]);
            this.dbConnection = ConfigurationSettings.AppSettings["DB_Connection"];
            this.companyName = ConfigurationSettings.AppSettings["DB_CompanyName"].Replace("#o#", "ö");

            try
            {
                this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationSettings.AppSettings["DB_Connection"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationSettings.AppSettings["DB_Connection"] + " is misssing.");
            }

            return true;
        }

    }
}