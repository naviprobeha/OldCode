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
    public class LayerBuilder
    {
        private int width;
        private int height;
        private double referenceX;
        private double referenceY;
        private int zoomLevel;
        private int level;
        private Database database;
        private string mapLocation;
        private string mapTable;

        private int mapPointLeft;
        private int mapPointTop;
        private int mapPointRight;
        private int mapPointBottom;

        private int baseWidth;
        private int baseHeight;

        private XmlDocument mapConfigXmlDoc;

        public LayerBuilder(int width, int height, Database database, XmlDocument mapConfigXmlDoc)
        {
            this.width = width;
            this.height = height;

            this.database = database;
            this.mapConfigXmlDoc = mapConfigXmlDoc;
        }

        public bool setReference(double x, double y, int level)
        {
            this.level = level;
            if (!readMapConfiguration(level)) return false;


            this.referenceX = x;
            this.referenceY = y;

            MapTiles maps = new MapTiles(database, mapTable);
            MapTile referenceMap = maps.selectMap((int)x, (int)y);
            if (referenceMap != null)
            {
                referenceMap.checkMap(System.Web.HttpContext.Current.Server.MapPath(mapLocation), level, zoomLevel);

                double metersPerPixelY = ((double)referenceMap.positionY1 - (double)referenceMap.positionY2) / (double)referenceMap.calcWidthHeight(zoomLevel, baseWidth);
                double metersPerPixelX = ((double)referenceMap.positionX2 - (double)referenceMap.positionX1) / (double)referenceMap.calcWidthHeight(zoomLevel, baseHeight);

                this.mapPointLeft = (int)(y - (width * metersPerPixelY / 2));
                this.mapPointRight = (int)(y + (width * metersPerPixelY / 2));
                this.mapPointBottom = (int)(x - (height * metersPerPixelX / 2));
                this.mapPointTop = (int)(x + (height * metersPerPixelX / 2));

                return true;
            }

            return false;
        }

        public void addMap(MapTile map, XmlDocument xmlDoc, XmlElement layersElement)
        {
            map.checkMap(System.Web.HttpContext.Current.Server.MapPath(mapLocation), level, zoomLevel);
            
            double meterPerPixelY = ((double)map.positionY1 - (double)map.positionY2) / (double)map.calcWidthHeight(zoomLevel, this.baseHeight);
            double meterPerPixelX = ((double)map.positionX2 - (double)map.positionX1) / (double)map.calcWidthHeight(zoomLevel, this.baseWidth);

            int mapPositionY = (int)((height / 2) + ((referenceX - map.positionX2) / meterPerPixelX));
            int mapPositionX = (int)((width / 2) - ((referenceY - map.positionY2) / meterPerPixelY));


            XmlElement layerElement = xmlDoc.CreateElement("tile");
            layerElement.SetAttribute("address", "http://"+System.Web.HttpContext.Current.Request.Url.Host+ this.getVirtualDirectory() +"/" + mapLocation+ "/" + level + "_" + map.code + "_" + zoomLevel + ".jpg");

            layerElement.SetAttribute("x", mapPositionX.ToString());
            layerElement.SetAttribute("y", mapPositionY.ToString());
            layerElement.SetAttribute("width", map.calcWidthHeight(zoomLevel, this.baseWidth).ToString());
            layerElement.SetAttribute("height", map.calcWidthHeight(zoomLevel, this.baseHeight).ToString());

            layersElement.AppendChild(layerElement);

        }


        public void transformMapsToXml(XmlDocument xmlDoc)
        {
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
            //boundariesElement.SetAttribute("width", width.ToString());
            //boundariesElement.SetAttribute("height", height.ToString());


            XmlElement layersElement = xmlDoc.CreateElement("tiles");
            docElement.AppendChild(layersElement);

            MapTiles maps = new MapTiles(database, mapTable);

            DataSet mapDataSet = maps.selectMaps(this.mapPointTop, this.mapPointLeft, this.mapPointBottom, this.mapPointRight);

            int i = 0;
            while (i < mapDataSet.Tables[0].Rows.Count)
            {
                MapTile map = new MapTile(database, mapDataSet.Tables[0].Rows[i], mapTable);

                addMap(map, xmlDoc, layersElement);

                i++;
            }


        }

        private bool readMapConfiguration(int level)
        {
            XmlElement docElement = mapConfigXmlDoc.DocumentElement;

            try
            {
                XmlNodeList nodeList = docElement.SelectNodes("map");
                int i = 0;
                while (i < nodeList.Count)
                {
                    XmlElement mapElement = (XmlElement)nodeList.Item(i);
                    if (level.ToString() == mapElement.GetAttribute("level"))
                    {
                        this.mapLocation = mapElement.GetAttribute("location");
                        this.baseWidth = int.Parse(mapElement.GetAttribute("width"));
                        this.baseHeight = int.Parse(mapElement.GetAttribute("height"));
                        this.mapTable = mapElement.GetAttribute("table");
                        this.zoomLevel = int.Parse(mapElement.GetAttribute("zoomLevel"));
                        return true;
                    }

                    i++;
                }

            }
            catch (Exception e)
            {
                if (e.Message != "") { }
                throw new Exception(e.Message);
            }

            return false;
        }

		private string getVirtualDirectory()
		{
			if (System.Web.HttpContext.Current.Request.ApplicationPath == "/") return "";
			return System.Web.HttpContext.Current.Request.ApplicationPath;

        }
    }
}
