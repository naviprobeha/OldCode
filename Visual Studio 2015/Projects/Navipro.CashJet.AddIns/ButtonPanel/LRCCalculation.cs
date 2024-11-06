using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.CashJet.AddIns
{
    public class LRCCalculation
    {
        public static string calculate(string data)
        {
            int lrc = 0;
            foreach (byte b in System.Text.Encoding.Default.GetBytes(data))
            {
                lrc = lrc ^ b;
            }

            return lrc.ToString("X");
        }

        public static string getHexLength(string data)
        {
            int length = data.Length;
            return length.ToString("X");
        }
    }
}
