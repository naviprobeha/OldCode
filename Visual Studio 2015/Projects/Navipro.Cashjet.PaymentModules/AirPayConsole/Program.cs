using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirPayConsole
{
    class Program : Navipro.Cashjet.PaymentModules.AirPay.Logger
    {
        static void Main(string[] args)
        {
            Program program = new Program();

            Navipro.Cashjet.PaymentModules.AirPay.Server server = new Navipro.Cashjet.PaymentModules.AirPay.Server(program);
            Console.WriteLine("Server starting...");
            server.start();
            Console.WriteLine("Server started...");
            Console.ReadLine();

            server.stop();


        }

        #region Logger Members

        void Navipro.Cashjet.PaymentModules.AirPay.Logger.log(string message)
        {
            Console.WriteLine(message);
        }

        #endregion
    }
}
