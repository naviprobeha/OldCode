using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting OSGi...");
            Navipro.OSGi.Framework.FrameworkFactory loader = new Navipro.OSGi.Framework.FrameworkFactory();
            
            Console.ReadLine();
            Console.WriteLine("Stopping...");
            loader.stop();
            
        }
    }
}
