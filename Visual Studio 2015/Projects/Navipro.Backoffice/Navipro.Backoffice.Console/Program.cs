using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navipro.Backoffice.Library;

namespace Navipro.Backoffice.Console
{
    class Program : Logger
    {
        public Program()
        {
            Configuration config = new Configuration();
            config.mailServer = "mail.workanywhere.se";
            config.userName = "test_navipro";
            config.password = "@ccountManager1!!";
            config.connectionString = "Data Source=192.168.222.91; Initial Catalog=NAVIPRO2009R2JOCA;User ID=super;Password=b0bbaf3tt; MultipleActiveResultSets=True;";
            config.companyName = "NaviPro AB";
            config.wsAddress = "http://proxy.workanywhere.se/NAVIPROTEST/WS/NaviPro%20AB/Codeunit/CaseMasterWebService";
            config.wsUserName = "navservice";
            config.wsPassword = "Q%3t0808";
            config.emailAddress = "test@navipro.se";

            MailHandler mailHandler = new MailHandler(config, this);
            mailHandler.start();

            System.Console.ReadLine();

            mailHandler.stop();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
        }

        public void write(string message, int level)
        {
            System.Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + level + ": "+message);
        }
    }
}
