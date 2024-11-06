using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Xml;

namespace Navipro.Cashjet.PaymentModules.CardPaymentHandler
{
    class CommHandler
    {
        private TcpListener tcpListener;
        private bool running;
        private System.Threading.Thread thread;

        private PaymentHandler paymentHandler;

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
                catch (Exception e) 
                {
                    
                }
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

        private string processCommand(string inputXml)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(inputXml);
            
            XmlElement docElement = xmlDoc.DocumentElement;
            string paymentModuleName = getXmlValue(docElement, "paymentModule");
            string transactionType = getXmlValue(docElement, "type");
            string transactionNo = getXmlValue(docElement, "transactionNo");
            string entryNoStr = getXmlValue(docElement, "entryNo");
            string amountStr = getXmlValue(docElement, "amount");
            string vatStr = getXmlValue(docElement, "vat");
            string cashbackStr = getXmlValue(docElement, "cashback");
            string manualType = getXmlValue(docElement, "manual");

            string reportType = getXmlValue(docElement, "reportType");
            string noOfTransactions = getXmlValue(docElement, "noOfTransactions");

            int entryNo = int.Parse(entryNoStr);
            decimal amount = (decimal)int.Parse(amountStr);
            decimal vat = (decimal)int.Parse(vatStr);
            decimal cashback = (decimal)int.Parse(cashbackStr);

            if (paymentHandler == null)
            {
                paymentHandler = new PaymentHandler(paymentModuleName);
            }


            if (transactionType == "endOfDay") paymentHandler.endOfDay(transactionNo, entryNo);
            if (transactionType == "transaction") paymentHandler.performTransaction(manualType, transactionNo, entryNo, amount / 100, vat / 100, cashback / 100);
            if (transactionType == "getStatus") return paymentHandler.getResultXml(transactionNo, entryNo);
            if (transactionType == "report") paymentHandler.transactionLog(transactionNo, entryNo, int.Parse(reportType), int.Parse(noOfTransactions));
            
            if (transactionType == "close") paymentHandler.close();

            return "<status>OK</status>";
        }

        private string getXmlValue(XmlElement xmlElement, string fieldName)
        {
            XmlElement fieldElement = (XmlElement)xmlElement.SelectSingleNode(fieldName);
            if (fieldElement != null)
            {
                if (fieldElement.FirstChild != null)
                {
                    XmlText xmlText = (XmlText)fieldElement.FirstChild;
                    return xmlText.Value;
                }
            }
            return "";
        }
            
    }
}
