using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Navipro.Cashjet.PaymentModules.CardPaymentListener
{
    class CommHandler
    {
        private TcpListener tcpListener;
        private bool running;
        private System.Threading.Thread thread;

        private Navipro.Cashjet.PaymentModules.CardPaymentHandler.PaymentHandler paymentHandler;

        public CommHandler()
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

            string output = "";
            try
            {
                output = processCommand(input);
            }
            catch (Exception e)
            {
                output = "<error>" + e.Message + "</error>";
            }

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
            streamWriter.WriteLine(output);
            streamWriter.Flush();
            tcpClient.Close();


        }

        private string processCommand(string xmlDoc)
        {
            if (paymentHandler == null)
            {
                //paymentHandler = new Navipro.Cashjet.PaymentModules.CardPaymentHandler.PaymentHandler("BPTI");
            }
            
            //int status = paymentHandler.performTransaction("HEPP", 1, 100, 20, 0);

            return "OK";
        }
    }
}
