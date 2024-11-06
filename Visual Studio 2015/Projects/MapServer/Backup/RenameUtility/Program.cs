using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace RenameUtility
{
    class RenameUtility
    {
        private string folder;
        private string addText;

        static void Main(string[] args)
        {
            Console.WriteLine("\nRenameUtility 1.0 (C) Håkan Bengtsson, Navipro AB\n");

            string folder = "";
            string addText = "";

            if (args.Count() > 0)
            {
                if (args[0] == "-?")
                {
                    Console.Write("Usage: RenameUtility.exe -s[sourceFolder] -t[text to add in front]\n");
                    return;
                }

                int i = 0;
                while (i < args.Count())
                {
                    if (args[i].Substring(0, 2) == "-s") folder = args[i].Substring(2);
                    if (args[i].Substring(0, 2) == "-t") addText = args[i].Substring(2);

                    i++;
                }
            }
            else
            {
                Console.Write("\nUsage: RenameUtility.exe -s[sourceFolder] -t[text to add in front]\n");
                return;
            }

            Console.WriteLine("Source folder.....: " + folder);
            Console.WriteLine("Text to add.......: " + addText);
            Console.WriteLine("");


            RenameUtility renameUtility = new RenameUtility(folder, addText);
            renameUtility.run();

        }

        public RenameUtility(string folder, string addText)
        {
            this.folder = folder;
            this.addText = addText;

        }

        public void run()
        {

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folder);
                FileInfo[] fileInfoList = dirInfo.GetFiles();

                Console.WriteLine("\nFound " + fileInfoList.Count() + " files in " + folder + "\n");

                int i = 0;
                while (i < fileInfoList.Count())
                {
                    FileInfo fileInfo = fileInfoList[i];

                    Console.WriteLine("Renaming file " + fileInfo.Name + " (" + (i + 1) + "/" + fileInfoList.Count() + ")");

                    fileInfo.MoveTo(folder+"\\"+addText+fileInfo.Name);
                    i++;
                }

                Console.WriteLine("\nDone.");

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }


    }
}
