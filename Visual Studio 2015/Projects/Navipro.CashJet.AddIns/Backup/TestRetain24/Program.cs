using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Navipro.CashJet.AddIns;

namespace TestRetain24
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://buy.playground.klarna.com/v1/";
            KlarnaOfflineHandler handler = new KlarnaOfflineHandler(url, "6923", "navipro", "znKeXm62GlbhlLe");
            handler.addTransactionLine("123", "Kalsonger", 1, 10000, 2500);

            //handler.cancelTransaction("R10004");
            //return;

            Console.WriteLine("Creating transaction...");
            string url = handler.createTransaction("POS1", "+46730698914", "R10010", "SEK", "SE", "sv-SE");


            System.Threading.Thread.Sleep(10000);

         
            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Checking status: "+url);
                KlarnaTransStatus status = handler.checkTransaction(url);
                if (status != null)
                {
                    Console.WriteLine("Message Code: " + status.message_code);
                    Console.WriteLine("Message: " + status.message);
                    Console.WriteLine("Invoice ID: " + status.invoice_id);
                    Console.WriteLine("");
                }
                System.Threading.Thread.Sleep(500);
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter) cont = false;
            }
        }
    }
}
