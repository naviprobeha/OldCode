using NaviPro.Alufluor.Idus.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program : Logger
    {
        public Program()
        {
            NaviPro.Alufluor.Idus.Library.IdusIntegration integration = new NaviPro.Alufluor.Idus.Library.IdusIntegration(this);
            integration.start();

            Console.ReadLine();

            integration.stop();

        }


        static void Main(string[] args)
        {
            Program program = new Program();
        }

        void Logger.write(string type, string message)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd") + " " + type + " " + message);
        }
    }
}
