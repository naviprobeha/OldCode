using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            X509Certificate2 cert = new X509Certificate2("C:\\temp\\cashjet.pfx", "cashjet2013r2", X509KeyStorageFlags.Exportable);

            RSACryptoServiceProvider provider = (RSACryptoServiceProvider)cert.PrivateKey;

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(provider.ToXmlString(true));
            xmlDoc.Save("C:\\temp\\privatekey.xml");             

            Console.ReadLine();
        }


    }
}
