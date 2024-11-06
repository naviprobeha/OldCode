using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFeedConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BoschProductFeed.ProductFeed feed = new BoschProductFeed.ProductFeed("192.168.222.39", "WACKES2015", "super", "3v0lut10n", "WACKES2015", "CL");
            Console.WriteLine(feed.getJson("BOSCH"));
            Console.ReadLine();
        }
    }
}
