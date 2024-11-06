using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Navipro.Sharepoint.FileRetriever
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Retriever");
            // replace this string with your Sharepoint content DB connection string
            string DBConnString = "Server=APOLLO3;Database=satish.mysharepoint.se;user id=super;password=b0bbaf3tt";

            // create a DB connection
            SqlConnection con = new SqlConnection(DBConnString);
            con.Open();

            // the query to grab all the files.
            // Note: Feel free to alter the LeafName like ‘%.extension’ arguments to suit your purpose
            SqlCommand com = con.CreateCommand();
            com.CommandText = "SELECT AllDocStreams.Id, AllDocStreams.[Content], AllDocs.TimeLastModified, AllDocs.CheckoutUserId,AllDocs.CheckoutDate, AllDocs.IsCurrentVersion, AllDocs.DirName, AllDocs.LeafName FROM AllDocs INNER JOIN AllDocStreams ON AllDocStreams.Id = AllDocs.Id WHERE (UPPER(AllDocs.LeafName) like 'Projects Branding 2012-10-15%')";
            //com.CommandText = "select DirName, LeafName from AllDocs where (LeafName LIKE '%.xlsx')";

            // execute query
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("Document: " + reader.GetValue(7).ToString() + ", " + reader.GetValue(2).ToString() + ", " + reader.GetValue(5).ToString());
                // grab the file’s directory and name
                string DirName = (string)reader["DirName"];
                string LeafName = (string)reader["LeafName"];

                /*

                // create directory for the file if it doesn’t yet exist
                if (!Directory.Exists(DirName))
                {
                    Directory.CreateDirectory(DirName);
                    Console.WriteLine("Creating directory: " + DirName);
                }
                */
                // create a filestream to spit out the file
                FileStream fs = new FileStream("C:\\temp\\" + LeafName, FileMode.Create, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(fs);

                
                  
                // depending on the speed of your network, you may want to change the buffer size (it’s in bytes)
                int bufferSize = 1000000;
                long startIndex = 0;
                long retval = 0;
                byte[] outByte = new byte[bufferSize];

                // grab the file out of the db one chunk (of size bufferSize) at a time
                do
                {
                    retval = reader.GetBytes(1, startIndex, outByte, 0, bufferSize);
                    startIndex += bufferSize;

                    writer.Write(outByte, 0, (int)retval);
                    writer.Flush();
                } while (retval == bufferSize);

                // finish writing the file
                writer.Close();
                fs.Close();

                Console.WriteLine("Finished writing file: " + LeafName);
                

            }
            Console.WriteLine("Done...");
            Console.ReadKey();

            // close the DB connection and whatnots
            reader.Close();
            con.Close();


        }
    }
}
