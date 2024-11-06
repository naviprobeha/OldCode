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
    public class Map
    {

        public string code;
        public int positionX1;
        public int positionY1;
        public int positionX2;
        public int positionY2;

        public string fileName;
        public System.Drawing.Image mapImage;
        public Bitmap transformedMap;
        

        private double scaleFactor = 4;

        private Database database;

        public Map(Database database, string code)
        {
            //
            // TODO: Add constructor logic here
            //
            this.code = code;
            this.database = database;

            read();
        }

        public Map(Database database, DataRow dataRow)
        {
            this.database = database;

            this.code = dataRow.ItemArray.GetValue(0).ToString();
            this.positionX1 = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
            this.positionY1 = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
            this.positionX2 = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
            this.positionY2 = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
            this.fileName = dataRow.ItemArray.GetValue(5).ToString();
          
        }

        private bool read()
        {

            SqlDataReader dataReader = database.query("SELECT [Code], [Position X1], [Position Y1], [Position X2], [Position Y2], [Filename] FROM [Map] WHERE Code = '" + this.code + "'");

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
            SqlDataReader dataReader = database.query("SELECT [Code] FROM [Map] WHERE Code = '" + this.code + "'");
            if (dataReader.Read())
            {
                dataReader.Close();

                database.nonQuery("UPDATE [Map] SET [Position X1] = '" + this.positionX1 + "', [Position Y1] = '" + this.positionY1 + "', [Position X2] = '" + this.positionX2 + "', [Position Y2] = '" + this.positionY2 + "', [Filename] = '" + this.fileName + "' WHERE [Code] = '" + this.code + "'");
            }
            else
            {
                dataReader.Close();

                database.nonQuery("INSERT INTO [Map] ([Code], [Position X1], [Position Y1], [Position X2], [Position Y2], [Filename]) VALUES ('"+this.code+"', '" + this.positionX1 + "', '" + this.positionY1 + "', '" + this.positionX2 + "', '" + this.positionY2 + "', '" + this.fileName + "')");

            }


        }

        public void loadMap(string path)
        {
            mapImage = System.Drawing.Image.FromFile(path + "\\" + this.fileName);
        }


        public void loadMap(Hashtable imageFileList, string path)
        {
            if (imageFileList.ContainsKey(this.fileName))
            {
                mapImage = (Image)imageFileList[this.fileName];
            }
            else
            {
                mapImage = System.Drawing.Image.FromFile(path + "\\" + this.fileName);
                imageFileList.Add(this.fileName, mapImage);
            }
        }
        public void setView(int level)
        {
            if (mapImage != null)
            {
                Bitmap bmpSource = new Bitmap(mapImage);

                double scale = (level / scaleFactor);

                //throw new Exception("Scale: " + scale + ", scale2: " + scale2);

                this.transformedMap = new Bitmap((int)((bmpSource.Width) / scale) , (int)((bmpSource.Height) / scale), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics grfxThumb = Graphics.FromImage(transformedMap);

                grfxThumb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                grfxThumb.DrawImage(bmpSource, new Rectangle(-1, -1, transformedMap.Width+1, transformedMap.Height+1));


            }
        }

  
    }
}