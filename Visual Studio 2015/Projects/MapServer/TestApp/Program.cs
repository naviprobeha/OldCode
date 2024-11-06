using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Navipro.MapServer.Lib;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*            
            Navipro.Base.Common.Configuration configuration = new Navipro.Base.Common.Configuration();
            configuration.setConfigValue("serverName", "APOLLO2");
            configuration.setConfigValue("database", "MapServer");
            configuration.setConfigValue("userName", "super");
            configuration.setConfigValue("password", "b0bbaf3tt");

            Navipro.Base.Common.Database database = new Navipro.Base.Common.Database(null, configuration);

            Maps maps = new Maps(database);

            Projection proj = new Projection("2.5V");

            DataSet dataSet = maps.getDataSet();
            int i = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                Map map = new Map(database, dataSet.Tables[0].Rows[i]);

                int[] xy = proj.SweRef99ToRT90(map.positionY1, map.positionX1);
                map.rt90x1 = xy[1];
                map.rt90y1 = xy[0];

                xy = proj.SweRef99ToRT90(map.positionY2, map.positionX2);
                map.rt90x2 = xy[1];
                map.rt90y2 = xy[0];

                map.commit();

                Console.WriteLine("[" + map.code + "]");
                i++;
            }
            */

            GpsToolsNET.Position position = new GpsToolsNET.Position();
            GpsToolsNET.License lic = new GpsToolsNET.License();
            lic.LicenseKey = "psJhO0jSmOhnUEDfYXrJSsW8ckeQNfq7pDj8";

            position.Datum = GpsToolsNET.Datum.WGS_84;
            position.Grid = GpsToolsNET.Grid.SWEDISH_GRID;

            position.Easting = 1397089;
            position.Northing = 6211029;

            Console.WriteLine("Old position:");
            Console.WriteLine("Lon: " + position.Longitude);
            Console.WriteLine("Lat: " + position.Latitude);
            Console.WriteLine("X: " + position.Easting);
            Console.WriteLine("Y: " + position.Northing);

            //position.Grid = GpsToolsNET.Grid.UTM_MGA_94;

            GpsToolsNET.CustomDatum customDatum = new GpsToolsNET.CustomDatum();
            customDatum.SemiMajorAxis = 6378137;
            customDatum.E2 = 0.00669438002290079;
            customDatum.DeltaX = 0;
            customDatum.DeltaY = 0;
            customDatum.DeltaZ = 0;
            customDatum.RotX = 0;
            customDatum.RotY = 0;
            customDatum.RotZ = 0;
            customDatum.ScaleFactor = 0;
            
            GpsToolsNET.CustomGrid customGrid = new GpsToolsNET.CustomGrid();
            customGrid.CustomDatum = customDatum;
            customGrid.Algorithm = GpsToolsNET.Algorithm.TRANSVERSE_MERCATOR;
            customGrid.FalseEasting = 500000;
            customGrid.FalseNorthing = 0;
            customGrid.LongitudeOfOrigin = 15;
            customGrid.LatitudeOfOrigin = 0;
            customGrid.ScaleFactor = 0.9996;

            //position.CustomDatum = customDatum;
            position.CustomGrid = customGrid;
            //position.Grid = GpsToolsNET.Grid.CUSTOM_GRID;
            //position.Datum = GpsToolsNET.Datum.CUSTOM_DATUM;


            Console.WriteLine("New position:");
            Console.WriteLine("Lon: " + position.Longitude);
            Console.WriteLine("Lat: " + position.Latitude);
            Console.WriteLine("X: " + position.Easting);
            Console.WriteLine("Y: " + position.Northing);
            Console.ReadLine();

        }
    }
}
