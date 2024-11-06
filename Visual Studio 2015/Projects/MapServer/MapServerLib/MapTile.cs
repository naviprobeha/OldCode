using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Navipro.Base.Common;
using System.Collections;

/// <summary>
/// Summary description for Map
/// </summary>
/// 
namespace Navipro.MapServer.Lib
{
    public class MapTile
    {

        public string code;
        public int positionX1;
        public int positionY1;
        public int positionX2;
        public int positionY2;

        public string fileName;
        public System.Drawing.Image mapImage;


        private double scaleFactor = 4;

        private Database database;

        private string tableName;

        public MapTile(Database database, string code, string tableName)
        {
            //
            // TODO: Add constructor logic here
            //
            this.code = code;
            this.database = database;
            this.tableName = tableName;

            read();
        }

        public MapTile(Database database, DataRow dataRow, string tableName)
        {
            this.database = database;

            this.code = dataRow.ItemArray.GetValue(0).ToString();
            this.positionX1 = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this.positionY1 = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this.positionX2 = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.positionY2 = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
            this.fileName = dataRow.ItemArray.GetValue(5).ToString();

            this.tableName = tableName;

        }

        private bool read()
        {

            SqlDataReader dataReader = database.query("SELECT [Code], [Position X1], [Position Y1], [Position X2], [Position Y2], [Filename] FROM ["+tableName+"] WHERE Code = '" + this.code + "'");

            if (dataReader.Read())
            {
                this.code = dataReader.GetValue(0).ToString();
                this.positionX1 = dataReader.GetInt32(1);
                this.positionY1 = dataReader.GetInt32(2);
                this.positionX2 = dataReader.GetInt32(3);
                this.positionY2 = dataReader.GetInt32(4);

                this.fileName = dataReader.GetValue(5).ToString();

                dataReader.Close();
                return true;
            }
            dataReader.Close();
            return false;


        }

        public void commit()
        {
            SqlDataReader dataReader = database.query("SELECT [Code] FROM [" + tableName + "] WHERE Code = '" + this.code + "'");
            if (dataReader.Read())
            {
                dataReader.Close();

                database.nonQuery("UPDATE ["+tableName+"] SET [Position X1] = '" + this.positionX1 + "', [Position Y1] = '" + this.positionY1 + "', [Position X2] = '" + this.positionX2 + "', [Position Y2] = '" + this.positionY2 + "', [Filename] = '" + this.fileName + "' WHERE [Code] = '" + this.code + "'");
            }
            else
            {
                dataReader.Close();

                database.nonQuery("INSERT INTO ["+tableName+"] ([Code], [Position X1], [Position Y1], [Position X2], [Position Y2], [Filename]) VALUES ('" + this.code + "', '" + this.positionX1 + "', '" + this.positionY1 + "', '" + this.positionX2 + "', '" + this.positionY2 + "', '" + this.fileName + "')");

            }


        }

        public void checkMap(string path, int level, int zoomLevel)
        {
            string fileName = level+"_"+this.code + "_" + zoomLevel + ".jpg";

            if (!System.IO.File.Exists(path + "\\" + fileName))
            {
                mapImage = System.Drawing.Image.FromFile(path + "\\" + this.code + "_1.jpg");
                setView(level);
                mapImage.Save(path + "\\" + fileName);
                mapImage.Dispose();
            }

        }


        public void setView(int level)
        {
            if (mapImage != null)
            {
                Bitmap bmpSource = new Bitmap(mapImage);


                //throw new Exception("Scale: " + scale + ", scale2: " + scale2);

                Bitmap transformedMap = new Bitmap(calcWidthHeight(level, bmpSource.Width), calcWidthHeight(level, bmpSource.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics grfxThumb = Graphics.FromImage(transformedMap);

                grfxThumb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //grfxThumb.DrawImage(bmpSource, new Rectangle(-1, -1, transformedMap.Width + 1, transformedMap.Height + 1));
                grfxThumb.DrawImage(bmpSource, new Rectangle(1, 1, transformedMap.Width, transformedMap.Height));

                mapImage.Dispose();
                mapImage = transformedMap;
            }
        }


        public int calcWidthHeight(int level, int baseSize)
        {
            if (level == 1) return baseSize;

            double scale = (level / scaleFactor);
            return (int)Math.Round((baseSize / scale), 0);
        }


    }
}