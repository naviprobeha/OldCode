using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace VoucherService.Library
{
    public class Configuration
    {

        public Configuration()
        {
            init();
        }

        public string connectionString { get; set; }

        public string companyName { get; set; }

        public string authorizationKey { get; set; }

        public bool init()
        {
            this.companyName = ConfigurationManager.AppSettings["CompanyName"];
            this.authorizationKey = ConfigurationManager.AppSettings["AuthorizationKey"];


            try
            {
                this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["ConnectionStringName"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationManager.AppSettings["ConnectionStringName"] + " is misssing.");
            }

            return true;
        }


    }
}