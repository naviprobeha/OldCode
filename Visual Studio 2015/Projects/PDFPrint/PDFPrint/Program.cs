using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFPrint
{
    class Program
    {
        static void Main(string[] args)
        {
            Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
            pdf.LoadFromFile("C:\\temp\\PDF_GIFT_PACK3.pdf");
            pdf.PrintSettings.PrinterName = "Microsoft XPS Document Writer";
            pdf.PrintSettings.PrintToFile("c:\\temp\\PrintToXps.xps");
            pdf.Print();
        }
    }
}
