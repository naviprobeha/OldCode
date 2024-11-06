using System;
using Microsoft.Win32;
using System.Configuration;

namespace NyceMethodInvoker
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

            try
            {
                this.connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            }
            catch (Exception)
            {
                throw new Exception("Connection string DB is misssing.");
            }

            return true;
        }


    }
}
