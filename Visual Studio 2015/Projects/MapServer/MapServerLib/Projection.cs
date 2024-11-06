//****************************************************
// A Class for SWEREF 99 to RT90 transform           *
// through Gauss-Krüger Projection                   *
// (also know as Transverse Mercator).               *
//                                                   *
// RT90 is the Swedish National Grid, using the      *
// GRS 80 as reference Ellipsoid.                    *
// Ref. http://www.lantmäteriet.se                   *
//                                                   *
// Contact: Peder.Kock@gmail.com, Univerity of Lund. *
//****************************************************

using System;
using System.Globalization;

namespace Navipro.MapServer.Lib
{
    /// <summary>
    /// A Class for RT90 to SWEREF99 transform
    /// through Gauss-Krüger Projection.
    /// </summary>
    public class Projection
    {
        public double east;
        public double north;
        public double rt90x;
        public double rt90y;

        public Projection()
        {
        }

        public void fromRt90(int rt90x, int rt90y)
        {
            this.rt90x = rt90x;
            this.rt90y = rt90y;

            GpsToolsNET.Position position = new GpsToolsNET.Position();
            GpsToolsNET.License lic = new GpsToolsNET.License();
            lic.LicenseKey = "psJhO0jSmOhnUEDfYXrJSsW8ckeQNfq7pDj8";

            position.Datum = GpsToolsNET.Datum.WGS_84;
            position.Grid = GpsToolsNET.Grid.SWEDISH_GRID;

            position.Easting = rt90y;
            position.Northing = rt90x;

            position.CustomGrid = this.getSweRef99Grid();

            this.east = position.Easting;
            this.north = position.Northing;
        }

        public void fromSweRef99(double north, double east)
        {
            this.north = north;
            this.east = east;

            GpsToolsNET.Position position = new GpsToolsNET.Position();
            GpsToolsNET.License lic = new GpsToolsNET.License();
            lic.LicenseKey = "psJhO0jSmOhnUEDfYXrJSsW8ckeQNfq7pDj8";

            position.Datum = GpsToolsNET.Datum.WGS_84;
            position.CustomGrid = this.getSweRef99Grid();

            position.Easting = east;
            position.Northing = north;

            position.Grid = GpsToolsNET.Grid.SWEDISH_GRID;

            this.rt90y = position.Easting;
            this.rt90x = position.Northing;
        }

        private GpsToolsNET.CustomDatum getSweRef99Datum()
        {
            GpsToolsNET.CustomDatum sweref99Datum = new GpsToolsNET.CustomDatum();
            sweref99Datum.SemiMajorAxis = 6378137;
            sweref99Datum.E2 = 0.00669438002290079;
            sweref99Datum.DeltaX = 0;
            sweref99Datum.DeltaY = 0;
            sweref99Datum.DeltaZ = 0;
            sweref99Datum.RotX = 0;
            sweref99Datum.RotY = 0;
            sweref99Datum.RotZ = 0;
            sweref99Datum.ScaleFactor = 0;

            return sweref99Datum;
        }

        private GpsToolsNET.CustomGrid getSweRef99Grid()
        {
            GpsToolsNET.CustomGrid sweref99Grid = new GpsToolsNET.CustomGrid();
            sweref99Grid.CustomDatum = this.getSweRef99Datum();
            sweref99Grid.Algorithm = GpsToolsNET.Algorithm.TRANSVERSE_MERCATOR;
            sweref99Grid.FalseEasting = 500000;
            sweref99Grid.FalseNorthing = 0;
            sweref99Grid.LongitudeOfOrigin = 15;
            sweref99Grid.LatitudeOfOrigin = 0;
            sweref99Grid.ScaleFactor = 0.9996;


            return sweref99Grid;
        }
    }
}
