using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Api.Library
{
    public class Configuration
    {

        public Configuration()
        {
        }

        public string connectionString { get; set; }
        
        public string companyName { get; set; }
        public string apiToken { get; set; }

        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string domain { get; set; }

        public bool init()
        {
            this.companyName = ConfigurationManager.AppSettings["WEB_CompanyName"];
            this.apiToken = ConfigurationManager.AppSettings["WEB_ApiToken"];

            this.clientId = ConfigurationManager.AppSettings["OAUTH_ClientId"];
            this.clientSecret = ConfigurationManager.AppSettings["OAUTH_ClientSecret"];
            this.domain = ConfigurationManager.AppSettings["OAUTH_Domain"];

            try
            {
                this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["DB_ConnectionStringName"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationManager.AppSettings["DB_ConnectionStringName"] + " is misssing.");
            }

            return true;
        }

 
    }
}