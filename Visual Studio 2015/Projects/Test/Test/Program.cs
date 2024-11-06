using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string prodNo;
            string idNo;
            string controlNo;
            splitType1Id("SE 050174-1506-2", out prodNo, out idNo, out controlNo);

            Console.WriteLine("ID: " + prodNo + ":" + idNo + ":" + controlNo);
            Console.ReadLine();
        }

        public static void splitType1Id(string inputId, out string prodNo, out string idNo, out string controlNo)
        {
            prodNo = "";
            idNo = "";
            controlNo = "";

            if (inputId.Substring(0, 2).ToUpper() == "SE") inputId = inputId.Substring(2);
            if (inputId.Substring(0, 1) == " ") inputId = inputId.Substring(1);
            if (inputId.IndexOf("-") == -1)
            {
                prodNo = inputId;
                return;
            }
            if (inputId.IndexOf("-") > -1)
            {
                prodNo = inputId.Substring(0, inputId.IndexOf("-"));
                inputId = inputId.Substring(inputId.IndexOf("-") + 1);
            }
            if (inputId.IndexOf("-") == -1)
            {
                idNo = inputId;
                return;
            }
            if (inputId.IndexOf("-") > -1)
            {
                idNo = inputId.Substring(0, inputId.IndexOf("-"));
                inputId = inputId.Substring(inputId.IndexOf("-") + 1);
            }
            controlNo = inputId;
        }
    }
}
