using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Navipro.Anyware.CloudPrinting.ClientLibrary
{
    public class PrintingHandler
    {
        public Thread mainThread;
        public bool running;

        public PrintingHandler()
        {

        }

        public void start()
        {
            Console.WriteLine("Starting cloud printing handler.");
            running = true;
            mainThread = new Thread(new ThreadStart(run));
            mainThread.Start();
        }

        public void stop()
        {
            Console.WriteLine("Stopping cloud printing handler.");

            running = false;
        }

        public void run()
        {
            DateTime lastRequest = DateTime.Now.AddMinutes(-5);

            while(running)
            {
                if (lastRequest.AddSeconds(15) < DateTime.Now)
                {
                    PrintJob printJob = getNextPrintJob(false);
                    while (printJob != null)
                    {
                        Console.WriteLine("Printing job " + printJob.id);

                        printJob.print();

                        printJob = getNextPrintJob(true);
                    }
                    lastRequest = DateTime.Now;
                }
                Thread.Sleep(1000);
            }
        }

        private PrintJob getNextPrintJob(bool delete)
        {
            string url = "https://anyware.se:7201/api/cloudprinting/12233";
            if (delete) url = url + "?deleteFirst=true";

            WebRequest webRequest = HttpWebRequest.Create(url);
            webRequest.Method = "GET";

            try
            {


                WebResponse webResponse = webRequest.GetResponse();

                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                string jsonContent = streamReader.ReadToEnd();
                streamReader.Close();

                PrintJob printJob = Newtonsoft.Json.JsonConvert.DeserializeObject<PrintJob>(jsonContent);
                return printJob;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                
            }
            return null;
        }
    }

}
