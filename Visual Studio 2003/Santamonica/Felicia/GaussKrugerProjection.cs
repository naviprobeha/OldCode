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

namespace Navipro.SantaMonica.Felicia
{
    /// <summary>
    /// A Class for SWEREF 99 to RT90 transform
    /// through Gauss-Krüger Projection.
    /// </summary>
    class GaussKrugerProjection
    {
        #region Fields
        //GRS 80 Ellipsoid Characteristics:
        //Semi Major axis
        private static double a = 6378137.0;
        //Flattening
        private static double f = (1.0 / 298.2572221010);

        //RT90 0 gon V 0:-15 fields (Use around Stockholm)
        // Centrum meridian
        private static string CM_0V = "18°03.2268\"E";
        // Scale factor
        private static double k0_0V = 1.000005400000;
        // False North
        private static double FN_0V = -668.844;
        // False East
        private static double FE_0V = 1500083.521;

        //RT90 2.5 gon V 0:-15 fields (Örebro)
        private static string CM_25V = "15°48.22624306\"E";
        private static double k0_25V = 1.00000561024;
        private static double FN_25V = -667.711;
        private static double FE_25V = 1500064.274;

        //RT90 5 gon V 0:-15 fields (Malmö)
        private static string CM_5V = "13°33.2256\"E";
        private static double k0_5V = 1.000005800000;
        private static double FN_5V = -667.130;
        private static double FE_5V = 1500044.695;

        //RT90 7.5 gon V 0:-15 fields (Göteborg)
        private static string CM_75V = "11°18.225\"E";
        private static double k0_75V = 1.000006000000;
        private static double FN_75V = -667.282;
        private static double FE_75V = 1500025.141;

        //RT90 2.5 gon O 0:-15 fields (Umeå)
        private static string CM_25O = "20°18.2274\"E";
        private static double k0_25O = 1.000005200000;
        private static double FN_25O = -670.706;
        private static double FE_25O = 1500102.765;

        //RT90 5 gon O 0:-15 fields (Luleå)
        private static string CM_5O = "22°33.228\"E";
        private static double k0_5O = 1.000004900000;
        private static double FN_5O = -672.557;
        private static double FE_5O = 1500121.846;

        //Variables
        private string CM;
        private double k0;
        private double FN;
        private double FE;
        private double lat; // Geodetic latitude 
        private double lon; // Geodetic longitude
        //Gauss-Krüger Projection variables
        private double A, B, C, D, Beta1, Beta2, Beta3, Beta4,
                e2, n, aHat;

        //RT90-coordinates
        private double x;
        private double y;

        //Make it international...(for numers in NMEA-sentences)
        private static CultureInfo enUSCultureInfo =
            new CultureInfo("en-US");
        #endregion

        #region Constructors
        /// <summary>
        /// Initiate a new instance of this class,
        /// Using the projection 'gon'.
        /// </summary>
        /// <param name="gon">7.5V, 5V, 2.5V, 0V, 2.5O or 5O</param>
        public GaussKrugerProjection(string gon)
        {
            if (gon != null)
            {
                switch (gon)
                {
                    case ("2.5V"):
                        CM = CM_25V;
                        k0 = k0_25V;
                        FN = FN_25V;
                        FE = FE_25V;
                        break;
                    case ("5V"):
                        CM = CM_5V;
                        k0 = k0_5V;
                        FN = FN_5V;
                        FE = FE_5V;
                        break;
                    case ("7.5V"):
                        CM = CM_75V;
                        k0 = k0_75V;
                        FN = FN_75V;
                        FE = FE_75V;
                        break;
                    case ("0V"):
                        CM = CM_0V;
                        k0 = k0_0V;
                        FN = FN_0V;
                        FE = FE_0V;
                        break;
                    case ("2.5O"):
                        CM = CM_25O;
                        k0 = k0_25O;
                        FN = FN_25O;
                        FE = FE_25O;
                        break;
                    case ("5O"):
                        CM = CM_5O;
                        k0 = k0_5O;
                        FN = FN_5O;
                        FE = FE_5O;
                        break;
                    default:
                        throw new InvalidOperationException("Specified Gon isn't recognized");
                }
            }
            else
            {
                throw new InvalidOperationException("Specified Gon isn't recognized");
            }
            this.Initialize();
        }

        /// <summary>
        /// Creates an instance of this
        /// class using 2.5 Gon 0:-15.
        /// Good for use in the Stockholm-region.
        /// </summary>
        public GaussKrugerProjection()
        {
            // USE 2.5 V 0:-15 as default
            CM = CM_25V;
            k0 = k0_25V;
            FN = FN_25V;
            FE = FE_25V;
            this.Initialize();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Calculates some useful constants
        /// </summary>
        private void Initialize()
        {
            e2 = f * (2.0 - f);
            n = f / (2.0 - f);
            aHat = (a / (1.0 + n)) * (1.0 +
                (0.25 * Math.Pow(n, 2)) +
                ((1.0 / 64.0) * Math.Pow(n, 4)));
            A = e2;
            //B = (1.0 / 6.0) * (5.0 * Math.Pow(A, 2) -
            //    6.0 * Math.Pow(A, 3));
			B = (1.0 / 6.0) * (5.0 * Math.Pow(A, 2) -
			    Math.Pow(A, 3));

            C = (1.0 / 120.0) * (104.0 * Math.Pow(A, 3) -
                45.0 * Math.Pow(A, 4));
            D = (1.0 / 1260.0) * (1237.0 * Math.Pow(A, 4));
            Beta1 = (0.5 * n) - ((2.0 / 3.0) * Math.Pow(n, 2)) +
                ((5.0 / 16.0) * Math.Pow(n, 3)) +
                ((41.0 / 180.0) * Math.Pow(n, 4));
            Beta2 = ((13.0 / 48.0) * Math.Pow(n, 2)) -
                ((3.0 / 5.0) * Math.Pow(n, 3)) +
                ((557.0 / 1440.0) * Math.Pow(n, 4));
            Beta3 = ((61.0 / 240.0) * Math.Pow(n, 3)) -
                ((103.0 / 140.0) * Math.Pow(n, 4));
            Beta4 = ((49561.0 / 161280.0) * Math.Pow(n, 4));
        }

        /// <summary>
        /// Parse NMEA-string
        /// </summary>
        /// <param name="LatorLong"></param>
        /// <param name="isLong"></param>
        /// <returns></returns>
        private double GetLatLong(string LatorLong, bool isLong)
        {
            //Get Hours (up to the '°')
            double deciLatLon = Convert.ToDouble(
                LatorLong.Substring(0, LatorLong.IndexOf("°"))
                );

            //Remove it once we've used it
            LatorLong = LatorLong.Substring(LatorLong.IndexOf("°") + 1);

            //Get Minutes (up to the '.') and divide by Minutes/Hour
            deciLatLon += (Convert.ToDouble(LatorLong.Substring(
                0, LatorLong.IndexOf(".")), enUSCultureInfo)
                ) / 60.0;

            //Remove it once we've used it
            LatorLong = LatorLong.Substring(LatorLong.IndexOf(".") + 1);

            //Get Seconds (up to the '"') and divide by Seconds/Hour
            string sec = LatorLong.Substring(0, LatorLong.IndexOf("\""));
            // Insert a dot to prevent the time from flying away...
            deciLatLon += (Convert.ToDouble(
                sec.Insert(2, "."), enUSCultureInfo)
                ) / 3600.0;

            //Get the Hemisphere string
            LatorLong = LatorLong.Substring(
                LatorLong.IndexOf("\"") + 1);
            if (isLong && LatorLong == "S" ||
                !isLong && LatorLong == "W")
            {
                // Set us right
                deciLatLon = -deciLatLon;
            }
            //And return (as radians)
            return deciLatLon * (Math.PI / 180.0);
        }

		private double atanh(double x)
		{
			return (0.5 * Math.Log((1+x) / (1-x)));
		}

		private double cosh(double x)
		{
			return (Math.Exp(x) + Math.Exp(-x))/2;
		}

		private double sinh(double x)
		{
			return (Math.Exp(x) - Math.Exp(-x))/2;
		}


        /// <summary>
        /// Projection method
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <seealso cref="http://www.lm.se"/>
        private void CalcGaussKrugerProjection(double lat, double lon)
        {
            // Compute the Conformal Latitude
            double phiStar = lat - (Math.Sin(lat) * Math.Cos(lat) * (
                A +
                B * Math.Pow(Math.Sin(lat), 2) +
                C * Math.Pow(Math.Sin(lat), 4) +
                D * Math.Pow(Math.Sin(lat), 6)));

            // Difference in longitude
            double dLon = lon - GetLatLong(CM, true);

            // Get Angles:
            double chi = Math.Atan(Math.Tan(phiStar) / Math.Cos(dLon));

            //Since Atanh isn't represented in the Math-class 
            //we'll use a simplification that holds for real z < 1
            //Ref: 
            //http://mathworld.wolfram.com/InverseHyperbolicTangent.html
            double z = Math.Cos(phiStar) * Math.Sin(dLon);
            double eta = 0.5 * Math.Log((1.0 + z) / (1.0 - z));

            // OK , we're finally ready to calculate the 
            // cartesian coordinates in RT90
            x = k0 * aHat * (chi +
                Beta1 * Math.Sin(2.0 * chi) * cosh(2.0 * eta) +
                Beta2 * Math.Sin(4.0 * chi) * cosh(4.0 * eta) +
                Beta3 * Math.Sin(6.0 * chi) * cosh(6.0 * eta) +
                Beta4 * Math.Sin(8.0 * chi) * cosh(8.0 * eta)) +
                FN;

            y = k0 * aHat * (eta +
                Beta1 * Math.Cos(2.0 * chi) * sinh(2.0 * eta) +
                Beta2 * Math.Cos(4.0 * chi) * sinh(4.0 * eta) +
                Beta3 * Math.Cos(6.0 * chi) * sinh(6.0 * eta) +
                Beta4 * Math.Cos(8.0 * chi) * sinh(8.0 * eta)) +
                FE;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns RT90 x and y coordinates
        /// for the given Gon.
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns>int[] with x and y coordinates
        /// with index 0 and 1 respectively</returns>
        public int[] GetRT90(string lat, string lon)
        {
            if (lat != "" && lon != "")
            {
                // Parse NMEA-strings
                this.lat = GetLatLong(lat, false);
                this.lon = GetLatLong(lon, true);

                // Calculate Projection on the RT90-grid
                this.CalcGaussKrugerProjection(this.lat, this.lon);

                // Return x,y vector
                int[] RT90Coordinates = 
                    {
                        Convert.ToInt32(Math.Round(x)), 
                        Convert.ToInt32(Math.Round(y)) 
                    };
                return RT90Coordinates;
            }
            else
                return null;
        }
        #endregion
    }
}
