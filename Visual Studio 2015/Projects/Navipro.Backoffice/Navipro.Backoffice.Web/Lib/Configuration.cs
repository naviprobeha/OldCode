using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Navipro.Backoffice.Web.Lib
{
    public class Configuration
    {

        public Configuration()
        {
        }

        public string connectionString { get; set; }
        public string cdrConnectionString { get; set; }

        public string resourcesConnectionString { get; set; }

        public string dataSource { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string smtpDomain { get; set; }
        public string smtpKey { get; set; }
        public string smtpFromName { get; set; }
        public string smtpFromAddress { get; set; }
        public string companyName { get; set; }
        public string wsAddress { get; set; }
        public string wsUserName { get; set; }
        public string wsPassword { get; set; }
        public string language { get; set; }
        
        public string internalNetworks { get; set; }

        public bool init()
        {
            this.companyName = ConfigurationManager.AppSettings["WEB_CompanyName"];

            this.smtpDomain = ConfigurationManager.AppSettings["SMTP_Domain"];
            this.smtpKey = ConfigurationManager.AppSettings["SMTP_Key"];
            this.smtpFromName = ConfigurationManager.AppSettings["SMTP_FromName"];
            this.smtpFromAddress = ConfigurationManager.AppSettings["SMTP_FromAddress"];

            this.wsAddress = ConfigurationManager.AppSettings["WS_Address"];
            this.wsUserName = ConfigurationManager.AppSettings["WS_UserName"];
            this.wsPassword = ConfigurationManager.AppSettings["WS_Password"];
            this.internalNetworks = ConfigurationManager.AppSettings["NET_InternalIPNetworks"];

            try
            {
                this.language = ConfigurationManager.AppSettings["Language"];
            }
            catch (Exception) { }
            try
            {
                this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["DB_ConnectionStringName"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationManager.AppSettings["DB_ConnectionStringName"] + " is misssing.");
            }
            try
            {
                this.cdrConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["CDR_ConnectionStringName"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationManager.AppSettings["CDR_ConnectionStringName"] + " is misssing.");
            }
            try
            {
                this.resourcesConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Resource_ConnectionStringName"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationManager.AppSettings["Resource_ConnectionStringName"] + " is misssing.");
            }

            return true;
        }

        public bool checkInternalNetwork(string userIpAddress)
        {
            int index = userIpAddress.IndexOf('.', userIpAddress.IndexOf('.') + 1);
            string userIpNetwork = userIpAddress.Substring(0, index);

            if (this.internalNetworks.Contains(userIpNetwork)) return true;
            return false;
        }

    }
}