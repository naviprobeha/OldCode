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
            int nonce = 100;

            /*
            byte[] nonceBytes = System.BitConverter.GetBytes(nonce);
            string nonceStr = System.Convert.ToBase64String(nonceBytes);

            Console.WriteLine("Nonce: " + nonceStr + " (" + System.Text.Encoding.ASCII.GetString(nonceBytes) + ")");

            string created = "2017-01-01T12:00:00Z";
            Console.WriteLine("Created: "+created);

            string password = "vRAdLYcGPHzR+BpQaZyJ2ZM+oc8";
            string pwdStr = System.Text.Encoding.ASCII.GetString(nonceBytes) + created + password;

            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            Console.WriteLine("Password: "+System.Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(pwdStr))));
            */

            //DateTime today = DateTime.Today;
            DateTime today = new DateTime(2019, 1, 22);
            DateTime lastYear = Navipro.Cashjet.Library.CalendarHelper.getLastYearDate(today);
            DateTime lastYear2 = Navipro.Cashjet.Library.CalendarHelper.getLastYearDate(lastYear);

            Console.WriteLine("Last year: "+lastYear.ToString("yyyy-MM-dd"));
            Console.WriteLine("Last year 2: " + lastYear2.ToString("yyyy-MM-dd"));

            Console.ReadLine();

        }
    }
}
