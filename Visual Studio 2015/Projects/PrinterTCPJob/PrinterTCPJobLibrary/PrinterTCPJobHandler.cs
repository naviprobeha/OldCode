using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTCPJobLibrary
{


    public class PrinterTCPJobHandler
    {
        private List<string> printLine;
        private Logger logger;

        public PrinterTCPJobHandler(Logger logger)
        {
            printLine = new List<string>();
            this.logger = logger;
        }

        public PrinterTCPJobHandler()
        {
            printLine = new List<string>();
            this.logger = null;
        }

        public void AddLine(string line)
        {
            printLine.Add(line);

        }
    

        public void PrintJob(string ipAddress, int port)
        {

            TcpClient client = new TcpClient();

            if (logger != null) logger.write("Connecting to " + ipAddress + ", port " + port);
            client.Connect(ipAddress, port);


            NetworkStream netStream = client.GetStream();
            StreamWriter streamWriter = new StreamWriter(netStream, Encoding.UTF8);

            foreach (string line in printLine)
            {

                if (logger != null) logger.write(line);
                streamWriter.WriteLine(line);

            }

            streamWriter.Flush();
            streamWriter.Close();


            netStream.Flush();
            netStream.Close();

            
            client.Close();

            if (logger != null) logger.write("Done!");
        }
    }
}
