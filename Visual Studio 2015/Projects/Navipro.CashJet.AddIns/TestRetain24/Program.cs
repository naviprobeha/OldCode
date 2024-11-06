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
            KlarnaV3Handler handler = new KlarnaV3Handler("PK12286_b7a6af57f766", "IgDiqFKva2QIWwVF");
            handler.AddOrderLine("112233", "Kalsonger", "112233", 1, "PCS", 25, 100, 0, 100, 20, 0);

            //handler.cancelTransaction("R10004");
            //return;

            Console.WriteLine("Creating transaction...");
            string url = handler.CreateSession("FO0001", "POS1", 100, 20, "SEK");


            System.Threading.Thread.Sleep(10000);

   
        }
    }
}
