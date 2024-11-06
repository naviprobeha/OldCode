using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Navipro.MapServer.Lib;
using Navipro.Base.Common;


namespace TileCreator
{
    class TileCreator
    {
        private string sourceFolder;
        private string destinationFolder;
        private string destinationTable;
        private Database database;
        private int width;
        private int height;
        private int level;
        private bool thumbnailMode;

        static void Main(string[] args)
        {
            Console.WriteLine("\nTileCreator 1.0 (C) Håkan Bengtsson, Navipro AB\n");

            string sourceFolder = "";
            string destinationFolder = "";
            string serverName = "";
            string database = "";
            string userId = "";
            string password = "";
            string destinationTable = "";
            bool thumbNailMode = false;
            int width = 0;
            int height = 0;
            int level = 0;

            if (args.Count() > 0)
            {
                if (args[0] == "-?")
                {
                    Console.Write("Usage: TileCreator.exe -s[sourceFolder] -d[destinationFolder] -S[serverName] -D[database] -u[userId] -p[password] -t[table]\n");
                    return;
                }

                int i = 0;
                while (i < args.Count())
                {
                    if (args[i].Substring(0, 2) == "-s") sourceFolder = args[i].Substring(2);
                    if (args[i].Substring(0, 2) == "-d") destinationFolder = args[i].Substring(2);
                    if (args[i].Substring(0, 2) == "-S") serverName = args[i].Substring(2);
                    if (args[i].Substring(0, 2) == "-D") database = args[i].Substring(2);
                    if (args[i].Substring(0, 2) == "-u") userId = args[i].Substring(2);
                    if (args[i].Substring(0, 2) == "-p") password = args[i].Substring(2);
                    if (args[i].Substring(0, 2) == "-t") destinationTable = args[i].Substring(2);
                    if (args[i] == "-Thumbnail") thumbNailMode = true;
                    if (args[i].Substring(0, 2) == "-w") width = int.Parse(args[i].Substring(2));
                    if (args[i].Substring(0, 2) == "-h") height = int.Parse(args[i].Substring(2));
                    if (args[i].Substring(0, 2) == "-l") level = int.Parse(args[i].Substring(2));

                    i++;
                }
            }
            else
            {
                Console.Write("Usage: TileCreator.exe -s[sourceFolder] -d[destinationFolder] -S[serverName] -D[database] -u[userId] -p[password] -t[table]\n");
                return;
            }

            Console.WriteLine("Source folder.....: " + sourceFolder);
            Console.WriteLine("Destination folder: " + destinationFolder);
            Console.WriteLine("Server name.......: " + serverName);
            Console.WriteLine("Database..........: " + database);
            Console.WriteLine("User ID...........: " + userId);
            Console.WriteLine("Password..........: " + password);
            Console.WriteLine("Level.............: " + level);
            Console.WriteLine("");


            TileCreator tileCreator = new TileCreator(sourceFolder, destinationFolder, serverName, database, userId, password, destinationTable, width, height, level);
            if (thumbNailMode) tileCreator.setThumbnailMode(true);
            tileCreator.run();
        
        }

        public TileCreator(string sourceFolder, string destinationFolder, string serverName, string database, string userId, string password, string destinationTable, int width, int height, int level)
        {
            this.sourceFolder = sourceFolder;
            this.destinationFolder = destinationFolder;
            this.destinationTable = destinationTable;

            Configuration configuration = new Configuration();
            configuration.setConfigValue("serverName", serverName);
            configuration.setConfigValue("database", database);
            configuration.setConfigValue("userName", userId);
            configuration.setConfigValue("password", password);

            this.database = new Database(null, configuration);
            this.width = width;
            this.height = height;
            this.level = level;
        }

        public void setThumbnailMode(bool mode)
        {
            this.thumbnailMode = mode;
        }

        public void run()
        {

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(sourceFolder);
                FileInfo[] fileInfoList = dirInfo.GetFiles();
                
                Console.WriteLine("\nFound " + fileInfoList.Count() + " files in " + sourceFolder + "\n");

                int i = 0;
                while (i < fileInfoList.Count())
                {
                    FileInfo fileInfo = fileInfoList[i];

                    Console.WriteLine("Processing file " + fileInfo.Name + " (" + (i + 1) + "/" + fileInfoList.Count() + ")");

                    if (this.thumbnailMode)
                    {
                        createThumbnail(fileInfo);
                    }
                    else
                    {
                        createTiles(fileInfo);
                    }
        
                    i++;
                }

                Console.WriteLine("\nDone.");

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void createTiles(FileInfo fileInfo)
        {
            
            Maps maps = new Maps(database);
            Map map = maps.selectMap(fileInfo.Name);

            if (map != null)
            {
                map.loadMap(fileInfo.DirectoryName);

                int tileWidth = width;
                int tileHeight = height;
                if (tileWidth == 0) tileWidth = 250;
                if (tileHeight == 0) tileHeight = 250;

                if (map.mapImage.Width > tileWidth)
                {
                    int noOfTilesX = map.mapImage.Width / tileWidth;
                    int noOfTilesY = map.mapImage.Height / tileHeight;

                    double metersPerPixelY = ((double)map.positionY1 - (double)map.positionY2) / (double)map.mapImage.Width;
                    double metersPerPixelX = ((double)map.positionX2 - (double)map.positionX1) / (double)map.mapImage.Height;

                    int y = 0;
                    while (y < noOfTilesX)
                    {
                        int x = 0;
                        while (x < noOfTilesY)
                        {
                            string tileName = map.code + "_" + (y + 1) + "_" + (x + 1);
                            Console.WriteLine("Creating tile " + tileName + "...");

                            Bitmap tileBmp = new Bitmap(tileWidth, tileHeight);

                            Graphics grfxThumb = Graphics.FromImage(tileBmp);
                            grfxThumb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            grfxThumb.DrawImage(map.mapImage, new Rectangle(0 - (x * tileWidth), 0 - (y * tileHeight), map.mapImage.Width, map.mapImage.Height));
                            tileBmp.Save(destinationFolder + "\\" + level.ToString() + "_" + tileName + "_1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                            grfxThumb.Dispose();
                            tileBmp.Dispose();

                            MapTile mapTile = new MapTile(database, tileName, destinationTable);

                            mapTile.positionY2 = (int)(map.positionY2 + ((x * tileWidth) * metersPerPixelY));
                            mapTile.positionY1 = (int)(map.positionY2 + (((x + 1) * tileWidth) * metersPerPixelY));
                            mapTile.positionX2 = (int)(map.positionX2 - ((y * tileHeight) * metersPerPixelX));
                            mapTile.positionX1 = (int)(map.positionX2 - (((y + 1) * tileHeight) * metersPerPixelX));
                            mapTile.commit();

                            x++;
                        }
                        y++;
                    }
                }
                else
                {
                    string tileName = map.code + "_1_1";
                    map.mapImage.Save(destinationFolder + "\\" + level.ToString() + "_" + tileName + "_1.jpg");

                    MapTile mapTile = new MapTile(database, tileName, destinationTable);

                    mapTile.positionY2 = (int)(map.positionY2);
                    mapTile.positionY1 = (int)(map.positionY1);
                    mapTile.positionX2 = (int)(map.positionX2);
                    mapTile.positionX1 = (int)(map.positionX1);
                    mapTile.commit();

                }

                map.mapImage.Dispose();
            }
            else
            {
                throw new Exception("Map for file " + fileInfo.Name + " is missing in database...");
            }
        }

        private void createThumbnail(FileInfo fileInfo)
        {
            Maps maps = new Maps(database);
            Map map = maps.selectMap(fileInfo.Name);

            if (map != null)
            {
                string tileName = map.code;
                string destName = destinationFolder + "\\" + tileName + ".jpg";

                if (!System.IO.File.Exists(destName))
                {

                    map.loadMap(fileInfo.DirectoryName);

                    int tileWidth = width;
                    int tileHeight = height;
                    if (tileWidth == 0) tileWidth = 500;
                    if (tileHeight == 0) tileHeight = 500;

                    Console.WriteLine("Creating thumbnail " + tileName + "...");

                    Bitmap tileBmp = new Bitmap(tileWidth, tileHeight);

                    Graphics grfxThumb = Graphics.FromImage(tileBmp);
                    grfxThumb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    grfxThumb.DrawImage(map.mapImage, new Rectangle(0, 0, tileWidth, tileHeight));
                    tileBmp.Save(destinationFolder + "\\" + tileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    grfxThumb.Dispose();
                    tileBmp.Dispose();

                    map.mapImage.Dispose();
                }
            }
            else
            {
                throw new Exception("Map for file " + fileInfo.Name + " is missing in database...");
            }

        }

    }
}
