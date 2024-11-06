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

        public Configuration()
        {
        }

 

    }
}
