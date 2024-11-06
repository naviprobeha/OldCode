using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Navipro.TFSClient.Library.TFSWrapper wrapper = new Navipro.TFSClient.Library.TFSWrapper("http://moonraker:8080/tfs/DefaultCollection");
            Console.WriteLine("Creating project....");
            wrapper.createProjekt("Navipro", "Förvaltning2");
            Console.WriteLine("Done....");
            Console.ReadLine();
        }
    }
}
