using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Navipro.Anyware.CloudPrinting.ClientLibrary
{
    public class PrintJob
    {
            
        public string id { get; set; }

        public string printerName { get; set; }
        public string uncPrinterPath { get; set; }
        public string serviceId { get; set; }

        public string base64Document { get; set; }

        public void print()
        {
            string execPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\PdfToPrinter.exe";

            byte[] byteArray = Convert.FromBase64String(base64Document);
            string tempFile = Path.GetTempPath()+id+".pdf";

            File.WriteAllBytes(tempFile, byteArray);

            ProcessStartInfo printProcessInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                FileName = execPath,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true,
                Arguments = "\"" + tempFile + "\""

            };

            if (uncPrinterPath != "")
            {
                printProcessInfo.Arguments = printProcessInfo.Arguments + " \"" + uncPrinterPath + "\"";
            }

            Process printProcess = new Process();

            printProcess.ErrorDataReceived += PrintProcess_ErrorDataReceived;

            printProcess.StartInfo = printProcessInfo;
            printProcess.Start();

            printProcess.WaitForExit();

            Thread.Sleep(3000);

   
            File.Delete(tempFile);
        }

        public void printAlt()
        {
            byte[] byteArray = Convert.FromBase64String(base64Document);

            Stream stream = new MemoryStream(byteArray);
            
            DevExpress.XtraPdfViewer.PdfViewer pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer();
            pdfViewer.LoadDocument(stream);


        }

        private void PrintProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("Error: " + e.Data.ToString());
        }
    }
}
