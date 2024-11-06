using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Navipro.Cashjet.PaymentModules.CardPayment
{
    class ComHandler
    {
        private TcpListener tcpListener;
        private bool running;
        private System.Threading.Thread thread;

        public ComHandler()
        {
            
            tcpListener = new TcpListener(System.Net.IPAddress.Any, 10001);
            
        }

        public void start()
        {
            running = true;
            tcpListener.Start();

            thread = new System.Threading.Thread(new System.Threading.ThreadStart(run));
            thread.Start();
        }

        public void run()
        {

            while (running)
            {
                System.Threading.Thread.Sleep(10);

                try
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    processConnection(tcpClient);
                }
                catch (Exception e) { }
            }
        }

        public void close()
        {
            running = false;
            System.Threading.Thread.Sleep(100);
            tcpListener.Stop();
        }

        private void processConnection(TcpClient tcpClient)
        {
            
            NetworkStream networkStream = tcpClient.GetStream();

            System.IO.StreamReader streamReader = new System.IO.StreamReader(networkStream);
            string input = streamReader.ReadLine();

            System.Threading.Thread.Sleep(5000);

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
            streamWriter.WriteLine(input);
            streamWriter.Flush();
            tcpClient.Close();


        }
            
    }
}
