using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintJobConsole
{
    public class CryptoHandler
    {
    

        public static string encrypt(string text)
        {
            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            var byteArray = System.Text.Encoding.UTF8.GetBytes(text);
            string base64Text = System.Convert.ToBase64String(byteArray);

            string cryptoKey = dateTime.Ticks.ToString();

            while (cryptoKey.Length < base64Text.Length)
            {
                cryptoKey = cryptoKey + cryptoKey;
            }

            string encryptedString = "";
            int i = 0;
            while (i < base64Text.Length)
            {
                string keyChar = cryptoKey[i].ToString();
                int keyInt = int.Parse(keyChar);

                encryptedString = encryptedString + ((Char)(base64Text[i] + keyInt)).ToString();
                i++;
            }

            encryptedString = encryptedString + ":" + dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            var byteArray2 = System.Text.Encoding.UTF8.GetBytes(encryptedString);
            return System.Convert.ToBase64String(byteArray2);
        }

        public static string decrypt(string text)
        {
            var byteArray = System.Convert.FromBase64String(text);
            string stringToDecode = System.Text.Encoding.UTF8.GetString(byteArray);


            string dateTimeString = stringToDecode.Substring(stringToDecode.Length - 19);

            stringToDecode = stringToDecode.Substring(0, stringToDecode.Length - 20);

            DateTime dateTime = DateTime.Parse(dateTimeString);

            string cryptoKey = dateTime.Ticks.ToString();

            while (cryptoKey.Length < stringToDecode.Length)
            {
                cryptoKey = cryptoKey + cryptoKey;
            }

            string decryptedString = "";
            int i = 0;
            while (i < stringToDecode.Length)
            {
                string keyChar = cryptoKey[i].ToString();
                int keyInt = int.Parse(keyChar);

                decryptedString = decryptedString + ((Char)(stringToDecode[i] - keyInt)).ToString();
                i++;
            }            


            var byteArray2 = System.Convert.FromBase64String(decryptedString);
            return System.Text.Encoding.UTF8.GetString(byteArray2);

     
        }
    }
}
