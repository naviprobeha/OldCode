using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Drawing;
using Navipro.Base.Common;

namespace Navipro.MapServer.Lib
{
    public class CompiledMap
    {

        private Bitmap bmpMap;
        private double referenceX;
        private double referenceY;
        private int level;
        //private double metersPerPixelX;
        //private double metersPerPixelY;
        private Database database;
        private string mapPath;
        
        private int mapPointLeft;
        private int mapPointTop;
        private int mapPointRight;
        private int mapPointBottom;

        private Hashtable imageFileList;


        public CompiledMap(int width, int height, Hashtable imageFileList, Database database, string mapPath)
        {            
            bmpMap = new Bitmap(width, height);
            Graphics grfxThumb = Graphics.FromImage(bmpMap);
            grfxThumb.FillRectangle(Brushes.White, 0, 0, width, height);

            this.database = database;
            this.mapPath = mapPath;
            this.imageFileList = imageFileList;
        }

        public bool setReference(double x, double y, int level)
        {
            this.referenceX = x;
            this.referenceY = y;
            this.level = level;


            Maps maps = new Maps(database);
            Map referenceMap = maps.selectMap((int)x, (int)y);
            if (referenceMap != null)
            {
                referenceMap.loadMap(mapPath);
                referenceMap.setView(level);

                double metersPerPixelY = ((double)referenceMap.positionY1 - (double)referenceMap.positionY2) / (double)referenceMap.transformedMap.Width;
                double metersPerPixelX = ((double)referenceMap.positionX2 - (double)referenceMap.positionX1) / (double)referenceMap.transformedMap.Height;

                this.mapPointLeft = (int)(y - (bmpMap.Width * metersPerPixelY / 2));
                this.mapPointRight = (int)(y + (bmpMap.Width * metersPerPixelY / 2));
                this.mapPointBottom = (int)(x - (bmpMap.Height * metersPerPixelX / 2));
                this.mapPointTop = (int)(x + (bmpMap.Height * metersPerPixelX / 2));

                return true;
            }

            return false;
        }

        public void addMap(Map map)
        {
            double meterPerPixelY = ((double)map.positionY1 - (double)map.positionY2) / (double)map.transformedMap.Width;
            double meterPerPixelX = ((double)map.positionX2 - (double)map.positionX1) / (double)map.transformedMap.Height;

            int mapPositionY = (int)((bmpMap.Height / 2) + ((referenceX - map.positionX2) / meterPerPixelX));
            int mapPositionX = (int)((bmpMap.Width / 2) - ((referenceY - map.positionY2) / meterPerPixelY));

            
            Bitmap bmpSourceMap = map.transformedMap;

            Graphics grfxThumb = Graphics.FromImage(bmpMap);
            grfxThumb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            grfxThumb.DrawImage(bmpSourceMap, new Rectangle(mapPositionX, mapPositionY, bmpSourceMap.Width, bmpSourceMap.Height));
            
        }

        public void addMap(Map map, XmlElement layerElement)
        {
            double meterPerPixelY = ((double)map.positionY1 - (double)map.positionY2) / (double)map.transformedMap.Width;
            double meterPerPixelX = ((double)map.positionX2 - (double)map.positionX1) / (double)map.transformedMap.Height;

            int mapPositionY = (int)((bmpMap.Height / 2) + ((referenceX - map.positionX2) / meterPerPixelX));
            int mapPositionX = (int)((bmpMap.Width / 2) - ((referenceY - map.positionY2) / meterPerPixelY));


            layerElement.SetAttribute("x", mapPositionX.ToString());
            layerElement.SetAttribute("y", mapPositionY.ToString());
            layerElement.SetAttribute("width", map.transformedMap.Width.ToString());
            layerElement.SetAttribute("height", map.transformedMap.Height.ToString());

        }


        public void applyMaps()
        {
            Maps maps = new Maps(database);
            
            DataSet mapDataSet = maps.selectMaps(this.mapPointTop, this.mapPointLeft, this.mapPointBottom, this.mapPointRight);

            int i = 0;
            while (i < mapDataSet.Tables[0].Rows.Count)
            {
                Map map = new Map(database, mapDataSet.Tables[0].Rows[i]);
                
                map.loadMap(imageFileList, mapPath);
                map.setView(level);

                addMap(map);

                i++;
            }

        }

        public void transformMapsToXml(XmlDocument xmlDoc)
        {
            xmlDoc.LoadXml("<mapServer/>");
            XmlElement docElement = xmlDoc.DocumentElement;

            XmlElement boundariesElement = xmlDoc.CreateElement("boundaries");
            docElement.AppendChild(boundariesElement);


            Projection projection1 = new Projection();
            projection1.fromSweRef99(mapPointTop, mapPointLeft);

            Projection projection2 = new Projection();
            projection2.fromSweRef99(mapPointBottom, mapPointRight);


            boundariesElement.SetAttribute("left", ((int)projection1.rt90y).ToString());
            boundariesElement.SetAttribute("right", ((int)projection2.rt90y).ToString());
            boundariesElement.SetAttribute("top", ((int)projection1.rt90x).ToString());
            boundariesElement.SetAttribute("bottom", ((int)projection2.rt90x).ToString());


            XmlElement layersElement = xmlDoc.CreateElement("layers");
            docElement.AppendChild(layersElement);

            Maps maps = new Maps(database);

            DataSet mapDataSet = maps.selectMaps(this.mapPointTop, this.mapPointLeft, this.mapPointBottom, this.mapPointRight);

            int i = 0;
            while (i < mapDataSet.Tables[0].Rows.Count)
            {
                Map map = new Map(database, mapDataSet.Tables[0].Rows[i]);

                map.loadMap(imageFileList, mapPath);
                map.setView(level);

                XmlElement layerElement = xmlDoc.CreateElement("layer");
                layerElement.SetAttribute("address", System.Web.HttpContext.Current.Request.Url.Host + "/maps/" + map.code + "_" + level + ".jpg");

                addMap(map, layerElement);
                layersElement.AppendChild(layerElement);

                i++;
            }


        }

        public Image getCompiledMap()
        {
            return bmpMap;
        }

        public XmlDocument serializeXml(string grid)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<map/>");
            XmlElement documentElement = xmlDoc.DocumentElement;

            if (grid == "SWEREF99")
            {
                documentElement.SetAttribute("left", this.mapPointLeft.ToString());
                documentElement.SetAttribute("right", this.mapPointRight.ToString());
                documentElement.SetAttribute("top", this.mapPointTop.ToString());
                documentElement.SetAttribute("bottom", this.mapPointBottom.ToString());
            }

            if (grid == "RT90")
            {
                Projection projection1 = new Projection();
                projection1.fromSweRef99(mapPointTop, mapPointLeft);

                Projection projection2 = new Projection();
                projection2.fromSweRef99(mapPointBottom, mapPointRight);

                documentElement.SetAttribute("left", ((int)projection1.rt90y).ToString());
                documentElement.SetAttribute("right", ((int)projection2.rt90y).ToString());
                documentElement.SetAttribute("top", ((int)projection1.rt90x).ToString());
                documentElement.SetAttribute("bottom", ((int)projection2.rt90x).ToString());
            }

            return xmlDoc;
        }
    }
}
