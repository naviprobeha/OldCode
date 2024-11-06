using System;
using Navipro.Anyware.CloudPrinting.ClientLibrary;

namespace Navipro.Anyware.CloudPrinting.ClientConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintingHandler printingHandler = new PrintingHandler();
            printingHandler.start();

            Console.ReadLine();

            printingHandler.stop();
        }
    }
}
