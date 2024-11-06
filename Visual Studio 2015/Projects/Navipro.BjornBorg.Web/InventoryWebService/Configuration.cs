using System;
using Microsoft.Win32;
using System.Configuration;

namespace Navipro.BjornBorg.Web
{
    /// <summary>
    /// Summary description for Configuration.
    /// </summary>
    public class Configuration
    {
        public string connectionString;
        public string companyName;

        public Configuration()
        {
        }

        public bool init()
        {
            this.companyName = ConfigurationSettings.AppSettings["WEB_CompanyName"];

            try
            {
                this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[ConfigurationSettings.AppSettings["DB_ConnectionStringName"]].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string " + ConfigurationSettings.AppSettings["DB_ConnectionStringName"] + " is misssing.");
            }


            return true;
        }


    }
}
