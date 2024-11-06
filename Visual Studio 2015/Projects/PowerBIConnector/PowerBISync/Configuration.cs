using System;
using Microsoft.Win32;
using System.Configuration;

namespace PowerBISync
{
    /// <summary>
    /// Summary description for Configuration.
    /// </summary>
    public class Configuration
    {
        public string connectionString { set; get; }
        public string companyName { set; get; }
        public string powerBiUserName { set; get; }
        public string powerBiPassword { set; get; }
        public string powerBiAppId { set; get; }

        public string module { get; set; }

        public Configuration()
        {
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            companyName = ConfigurationManager.AppSettings["companyName"];
            powerBiUserName = ConfigurationManager.AppSettings["powerBiUserName"];
            powerBiPassword = ConfigurationManager.AppSettings["powerBiPassword"];
            powerBiAppId = ConfigurationManager.AppSettings["powerBiAppId"];
            module = ConfigurationManager.AppSettings["module"];
        }

 

    }
}
