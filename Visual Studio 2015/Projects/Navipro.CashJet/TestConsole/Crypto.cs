﻿using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class Crypto
{
    // Generate a key k1 with password pwd1 and salt salt1.
    // Generate a key k2 with password pwd1 and salt salt1.
    // Encrypt data1 with key k1 using symmetric encryption, creating edata1.
    // Decrypt edata1 with key k2 using symmetric decryption, creating data2.
    // data2 should equal data1.

    private const string usageText = "Usage: RFC2898 <password>\nYou must specify the password for encryption.\n";
    public static void crypt(string password, string dataToEncrypt)
    {
        //If no file name is specified, write usage text.
        if (password.Length == 0)
        {
            Console.WriteLine(usageText);
        }
        else
        {
            string pwd1 = password;
            // Create a byte array to hold the random value.
            byte[] salt1 = System.Text.ASCIIEncoding.Default.GetBytes("AdyenNexoV1Salt");

            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
                // Fill the array with a random value.
                rngCsp.GetBytes(salt1);

            //data1 can be a string or contents of a file.
            string data1 = dataToEncrypt;
            //The default iteration count is 1000 so the two methods use the same iteration count.
            int myIterations = 4000;
            try
            {
                Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(pwd1, salt1,
myIterations);
                Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(pwd1, salt1);
                // Encrypt the data.
                Aes encAlg = Aes.Create();
                encAlg.Key = k1.GetBytes(80);
                MemoryStream encryptionStream = new MemoryStream();
                CryptoStream encrypt = new CryptoStream(encryptionStream,
encAlg.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] utfD1 = new System.Text.UTF8Encoding(false).GetBytes(
data1);

                encrypt.Write(utfD1, 0, utfD1.Length);
                encrypt.FlushFinalBlock();
                encrypt.Close();
                byte[] edata1 = encryptionStream.ToArray();
                k1.Reset();

                Console.WriteLine("Base64: " + System.Convert.ToBase64String(edata1));

                // Try to decrypt, thus showing it can be round-tripped.
                Aes decAlg = Aes.Create();
                decAlg.Key = k2.GetBytes(16);
                decAlg.IV = encAlg.IV;
                MemoryStream decryptionStreamBacking = new MemoryStream();
                CryptoStream decrypt = new CryptoStream(
decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
                decrypt.Write(edata1, 0, edata1.Length);
                decrypt.Flush();
                decrypt.Close();
                k2.Reset();
                string data2 = new UTF8Encoding(false).GetString(
decryptionStreamBacking.ToArray());

                if (!data1.Equals(data2))
                {
                    Console.WriteLine("Error: The two values are not equal.");
                }
                else
                {
                    Console.WriteLine("The two values are equal.");
                    Console.WriteLine("k1 iterations: {0}", k1.IterationCount);
                    Console.WriteLine("k2 iterations: {0}", k2.IterationCount);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
        }
    }
}