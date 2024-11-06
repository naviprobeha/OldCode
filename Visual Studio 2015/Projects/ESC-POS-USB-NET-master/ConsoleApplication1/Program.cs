using ESC_POS_USB_NET.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Printer printer = new Printer("TSP100");
            printer.TestPrinter();
            printer.PrintDocument();
            */

            TcpClient client = new TcpClient();
            client.Connect("192.168.120.105", 9100);
            NetworkStream nws = client.GetStream();

            Dictionary<string, byte[]> D = new Dictionary<string, byte[]>();

            D.Add("Esc", new byte[] { 0x1B });
            D.Add("@", new byte[] { 0x1B, 0x40 });
            D.Add("LF", new byte[] { 0x0A });
            D.Add("Cut", new byte[] { 0x1B, 0x69 });
            D.Add("Bold", new byte[] { 0x1B, 0x45, 0x1 });
            D.Add("Height2", new byte[] { 0x1D, 0x21, 0x1 });
            D.Add("Width2", new byte[] { 0x1D, 0x21, 0xA });
            D.Add("Print&Feed", new byte[] { 0x1B, 0x64, 0xA });



            nws.Write(D["@"], 0, 1);
            nws.Write(D["Bold"], 0, 3);
            nws.Write(D["Height2"], 0, 3);
            nws.Write(D["Width2"], 0, 3);
            nws.Write(System.Text.UTF8Encoding.Default.GetBytes("Hello World"), 0, System.Text.UTF8Encoding.Default.GetBytes("Hello World").Length);
            nws.Write(D["Print&Feed"], 0, 3);
            nws.Flush();

            nws.Write(D["@"], 0, 1);
            nws.Write(D["Cut"], 0, 2);
            nws.Flush();

            nws.Close();

            client.Close();


            Console.ReadLine();
        }
    }
}
