using System;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace Navipro.BroadSoft.Lib
{
    /// <summary>
    /// Summary description for MessageDigest.
    /// </summary>
    public class MessageDigest
    {
        public MessageDigest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string SHAEncode(string encString)
        {
            HashAlgorithm hash = new SHA1Managed();

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(encString);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);

            return serialize(hashBytes, 40);
        }

        public string MD5Encode(string encString)
        {
            HashAlgorithm hash = new MD5CryptoServiceProvider();

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(encString);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);

            return serialize(hashBytes, 32);
        }


        public string serialize(byte[] password, int outputLength)
        {

            char[] asciiDigestBytes = new char[outputLength];

            for (int i = 0; i < (outputLength / 2); i++)
            {
                int outputOffset = i * 2;
                byte thisByte = password[i];
                char upperNibble = toAsciiHexNibble((thisByte & 0xF0) >> 4);
                char lowerNibble = toAsciiHexNibble(thisByte & 0x0F);
                asciiDigestBytes[outputOffset] = upperNibble;
                asciiDigestBytes[outputOffset + 1] = lowerNibble;
            }
            return new String(asciiDigestBytes);

        }

        private char toAsciiHexNibble(int hexValue)
        {
            char returnValue = '!';   // default
            if ((hexValue >= 0) && (hexValue <= 9))
            {
                returnValue = (char)((int)'0' + hexValue);
            }
            else if ((hexValue >= 0x000A) && (hexValue <= 0x000F))
            {
                returnValue = (char)((int)'a' + (hexValue - 0x000A));
            }
            return returnValue;
        }

    }
}
