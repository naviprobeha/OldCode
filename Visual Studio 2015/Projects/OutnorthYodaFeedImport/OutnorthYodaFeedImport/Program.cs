using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutnorthYodaFeedImport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting importer...");

            Importer importer = new Importer();
            importer.process();


            Console.WriteLine("Importer done...");
            

        }
    }
}
