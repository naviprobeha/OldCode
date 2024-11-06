using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Navipro.WebServices.NAVWebService
{
    public class Configuration
    {
        public string msmqInQueue;
        public string msmqOutQueue;
        public int msmqTimeout;


        public Configuration()
        {

        }

        public bool init()
        {
            this.msmqInQueue = ConfigurationSettings.AppSettings["MSMQ_RequestQueue"];
            this.msmqOutQueue = ConfigurationSettings.AppSettings["MSMQ_ResponseQueue"];
            this.msmqTimeout = int.Parse(ConfigurationSettings.AppSettings["MSMQ_TimeOut"]);

            return true;
        }

    }
}
