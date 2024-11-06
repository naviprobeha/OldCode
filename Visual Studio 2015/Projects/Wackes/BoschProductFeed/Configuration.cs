using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschProductFeed
{
    public class Configuration
    {
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string LocationCode { get; set; }

        public string connectionString
        {
            get
            {
                return "Database=" + DatabaseName + ";Server=" + ServerName + ";Connect Timeout=30;User ID=" + UserID + ";Password=" + Password + ";Max Pool Size=10000";
            }
        }

    }
}
