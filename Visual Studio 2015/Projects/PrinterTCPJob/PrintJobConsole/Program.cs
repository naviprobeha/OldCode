using PrinterTCPJobLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintJobConsole
{
    class Program : Logger
    {
        static void Main(string[] args)
        {

            string stringToEncrypt = "Hej! jag heter Håkan Bengtsson. Vad heter du?";
            Console.WriteLine("Encrypt: "+stringToEncrypt);

            string encryptedString = CryptoHandler.encrypt(stringToEncrypt);
            Console.WriteLine("Encrypted: " + encryptedString);

            string decryptedString = CryptoHandler.decrypt(encryptedString);
            Console.WriteLine("Decrypted: " + decryptedString);

            Console.ReadLine();
            return;



            Program programLogger = new Program();

            PrinterTCPJobLibrary.PrinterTCPJobHandler lib = new PrinterTCPJobLibrary.PrinterTCPJobHandler(programLogger);

            StreamReader streamReader = new StreamReader(args[0]);

            string line = streamReader.ReadToEnd();
            streamReader.Close();

            lib.AddLine(line);

            lib.PrintJob(args[1], int.Parse(args[2]));


        }

        public void write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
