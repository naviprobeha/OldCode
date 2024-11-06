//------------------------------------------------------------------------------
/// <copyright from='1997' to='2005' company='Microsoft Corporation'>
///		Copyright (c) Microsoft Corporation. All Rights Reserved.
///
///   This source code is intended only as a supplement to Microsoft
///   Development Tools and/or on-line documentation.  See these other
///   materials for detailed information regarding Microsoft code samples.
/// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Globalization;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// This is the helper class to parse NMEA sentences.
	/// </summary>
	public class NmeaHelper
	{
		public NmeaHelper()
		{
		}

    #region Event delegates and handlers

    #region DBT
    public delegate void DbtEventHandler(object sender, DbtEventArgs e);
    public event DbtEventHandler Dbt;
    protected virtual void OnDbt(DbtEventArgs e) 
    {
      if (Dbt != null) Dbt(this, e);
    }
    #endregion

    #region GLL
    public delegate void GllEventHandler(object sender, GllEventArgs e);
    public event GllEventHandler Gll;
    protected virtual void OnGll(GllEventArgs e) 
    {
      if (Gll != null) Gll(this, e);
    }
    #endregion

    #region MTW
    public delegate void MtwEventHandler(object sender, MtwEventArgs e);
    public event MtwEventHandler Mtw;
    protected virtual void OnMtw(MtwEventArgs e) 
    {
      if (Mtw != null) Mtw(this, e);
    }
    #endregion

    #region MWV
    public delegate void MwvEventHandler(object sender, MwvEventArgs e);
    public event MwvEventHandler Mwv;
    protected virtual void OnMwv(MwvEventArgs e) 
    {
      if (Mwv != null) Mwv(this, e);
    }
    #endregion

    #region RMC
    public delegate void RmcEventHandler(object sender, RmcEventArgs e);
    public event RmcEventHandler Rmc;
    protected virtual void OnRmc(RmcEventArgs e) 
    {
      if (Rmc != null) Rmc(this, e);
    }
    #endregion

    #endregion

    #region Parse
    public void ParseSentence(string nmeaSentence)
    {

      // NMEA sentence must include talker and identifier, first must be '$', and max 82 chars
      if(nmeaSentence.Length < 6) return;
      if(nmeaSentence[0] != '$') return;
      if(nmeaSentence.Length > 82) return;

      // Remove leading '$'
      string sentence = nmeaSentence.Substring(1);

      // Checksum control (if '*' found)
      int starpos = sentence.IndexOf('*');
      if(starpos >= 0)
        if(!checksum(sentence.Substring(0, starpos), sentence.Substring(starpos + 1)))
          return;


      // Get talker
      //string talker = sentence.Substring(0, 2);

      // Get identifier
      string identifier = sentence.Substring(2, 3);

      // Get fields
      string[] fields = sentence.Substring(6).Split(',');

      switch (identifier)
      {
        case "DBT": // Depth below transducer 
          dbt(fields);
          break;

        case "GLL": // Depth below transducer 
          gll(fields);
          break;

        case "GGA": // Depth below transducer 
		  gga(fields);
		  break;

        case "MTW": // Water Temperature 
          mtw(fields);
          break;

        case "MWV": // Wind Speed and Angle 
          mwv(fields);
          break;

        case "RMC": // Water speed and heading
          rmc(fields);
          break;

        default:
          break;
      }
    }
    #endregion

    #region DBT - Depth below transducer
    /// <summary>
    /// DBT - Depth below transducer
    ///
    ///        1   2 3   4 5   6 7
    ///        |   | |   | |   | |
    /// $--DBT,x.x,f,x.x,M,x.x,F*hh<CR><LF>
    ///
    /// 1) Depth, feet
    /// 2) f = feet
    /// 3) Depth, meters
    /// 4) M = meters
    /// 5) Depth, Fathoms
    /// 6) F = Fathoms
    /// 7) Checksum
    /// </summary>
    private void dbt(string[] fields)
    {
      // Get field values
      decimal depthFeet = Convert.ToDecimal(fields[0]);
      decimal depthMeters = Convert.ToDecimal(fields[2]);
      decimal depthFathoms = Convert.ToDecimal(fields[4]);

      // Raise event
      OnDbt(new DbtEventArgs(depthFeet, depthMeters, depthFathoms));
    }
    #endregion

    #region GLL - Geographic Position - Latitude/Longitude
    /// <summary>
    /// GLL - Geographic Position - Latitude/Longitude
    ///  
    ///	       1       2 3        4 5         6 7
    ///	       |       | |        | |         | |
    /// $--GLL,llll.ll,a,yyyyy.yy,a,hhmmss.ss,A*hh<CR><LF>
    ///
    /// Field Number: 
    /// 1) Latitude
    /// 2) N or S (North or South)
    /// 3) Longitude
    /// 4) E or W (East or West)
    /// 5) Universal Time Coordinated (UTC)
    /// 6) Status A - Data Valid, V - Data Invalid
    /// 7) Checksum
    /// </summary>
    /// <param name="field"></param>
    private void gll(string[] fields)
    {
      // Check if data OK
      if(fields[5][0] != 'A')
        return;

      // Get field values
      string latitudeText = formatFractionalDegrees(fields[0], fields[1]);
      string longitudeText = formatFractionalDegrees(fields[2], fields[3]);
      decimal latitude = fractionalToDecimalDegrees(Convert.ToDecimal(fields[0]));
      if(fields[1][0] == 'S')
        latitude = -latitude;
      decimal longitude = fractionalToDecimalDegrees(Convert.ToDecimal(fields[2]));
      if(fields[3][0] == 'W')
        longitude = -longitude;
      //string utc = fields[4];

      // Raise event
      OnGll(new GllEventArgs(latitude, longitude, latitudeText, longitudeText));
    }
    #endregion

		#region GGA - Geographic Position - Latitude/Longitude
		/// <summary>
		/// GLL - Global Positioning System - Fix Data
		///  
		///	       1      2        3 4         5 6 7  8   9     10 11   12 13 14 15
		///	       |      |        | |         | | |  |   |     |  |    |  |  |  |
		/// $--GGA,123519,4807.038,N,01131.324,E,1,08,0.9,545.4,M, 46.9,M,  ,    *hh<CR><LF>
		///
		/// Field Number: 
		/// 1) Fix taken at 12:35:19 UTC
		/// 2) Latitude 48 deg 07.38' N
		/// 3) N or S (North or South)
		/// 4) Longitude 11 deg 31.324' E
		/// 5) E or W (East or West)
		/// 6) Fix quality (0 = invalid, 1 = GPS fix, 2 = DGPS fix)
		/// 7) Number of satelites being tracked
		/// 8) Horizontal dilution of position
		/// 9) Altitude
		/// 10) Unit of altitude
		/// 11) Height of geiod above WGS84 ellipsoid
		/// 12) Unit of altitude of geoid
		/// 13) Time in seconds since last DGPS update
		/// 14) DGPS station ID
		/// 15) Checksum
		/// </summary>
		/// <param name="field"></param>
		private void gga(string[] fields)
		{
			// Get field values

			if (fields[5][0] == '0') return;

			string latitudeText = formatFractionalDegrees(fields[1], fields[2]);
			string longitudeText = formatFractionalDegrees(fields[3], fields[4]);
			decimal latitude = fractionalToDecimalDegrees(Convert.ToDecimal(fields[1]));
			if(fields[2][0] == 'S')
				latitude = -latitude;
			decimal longitude = fractionalToDecimalDegrees(Convert.ToDecimal(fields[3]));
			if(fields[4][0] == 'W')
				longitude = -longitude;

			// Raise event
			OnGll(new GllEventArgs(latitude, longitude, latitudeText, longitudeText));
		}
		#endregion

    #region MTW - Water Temperature
    /// <summary>
    /// MTW - Water Temperature
    ///
    ///        1   2 3
    ///        |   | | 
    /// $--MTW,x.x,C*hh<CR><LF>
    ///
    /// Field Number: 
    /// 1) Degrees
    /// 2) Unit of Measurement, Celcius
    /// 3) Checksum
    /// </summary>
    /// <param name="field"></param>
    private void mtw(string[] fields)
    {
      // Get field values
      decimal celcius = Convert.ToDecimal(fields[0]);
      decimal farenheit = celciusToFahrenheit(celcius);

      // Raise event
      OnMtw(new MtwEventArgs(celcius, farenheit));
    }
    #endregion

    #region MWV - Wind Speed and Angle
    /// <summary>
    ///  MWV - Wind Speed and Angle
    ///
    ///        1   2 3   4 5
    ///        |   | |   | |
    /// $--MWV,x.x,a,x.x,a*hh<CR><LF>
    ///
    /// Field Number: 
    /// 1) Wind Angle, 0 to 360 degrees
    /// 2) Reference, R = Relative, T = True
    /// 3) Wind Speed
    /// 4) Wind Speed Units, K/M/N (K = kmph [km/h], M = mps [m/s], N = kt)
    /// 5) Status, A = Data Valid
    /// 6) Checksum
    /// </summary>
    /// <param name="field"></param>
    private void mwv(string[] fields)
    {
      // Check if data OK
      if(fields[4][0] != 'A')
        return;

      // Get field values
      decimal windAngle = Convert.ToDecimal(fields[0]);
      bool referenceRelative = fields[1][0] == 'R';
      decimal windSpeed = Convert.ToDecimal(fields[2]);
      string speedUnits = fields[3];

      // Raise event
      OnMwv(new MwvEventArgs(windAngle, referenceRelative, windSpeed, speedUnits));
    }
    #endregion

    #region RMC - Water speed and heading
    /// <summary>
    ///  RMC - Recommended minimum specific
    ///
    ///        1 2 3   4 5   6 7   8   9      10  11
    ///        | | |   | |   | |   |   |      |   |
    /// $--RMC,x,A,x.x,N,x.x,E,x.x,x.x,DDMMYY,x.x,E*hh<CR><LF>
    ///
    /// Field Number: 
    /// 1) UTC Position
    /// 2) A = Valid, V = Not valid
    /// 3) Latitude 
    /// 4) N/S Indicator
    /// 5) Longitude
    /// 6) E/W Indicator
    /// 7) Speed over ground
    /// 8) Course over ground
    /// 9) Date
    /// 10) Magnetic variation
    /// 11) E/W variation indicator
    /// </summary>
    /// <param name="field"></param>
    private void rmc(string[] fields)
    {
		// Get field values
		if (fields[1] != "A") 
			return;

		decimal speed = Convert.ToDecimal(fields[6]);
		decimal degreesTrue = Convert.ToDecimal(fields[7]);

		// Raise event
        OnRmc(new RmcEventArgs(degreesTrue, speed));
    }
    #endregion

    #region Helpers
    /// <summary>
    /// Checksum control.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private bool checksum(string s, string checksum)
    {
      int sum = 0;
      for(int i = 0; i < s.Length; i++)
        sum = sum ^ (int)(s[i]);
      return (checksum == string.Format("{0:X2}", sum));
    }

    ///summary>
    /// Converts fractional degrees to decimal degrees.
    /// </summary>
    /// <param name="factionalDegrees">Fractional degrees.</param>
    /// <returns>Decimal degrees.</returns>
    public static decimal fractionalToDecimalDegrees(decimal factionalDegrees) 
    {
      bool positve = factionalDegrees > 0;
      string factionalDegreesString = Math.Abs(factionalDegrees).ToString("00000.0000");
      int delimeterPosition = factionalDegreesString.IndexOf(
        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

      // Get the fractional part of minutes
      decimal fractionalMinutes = Convert.ToDecimal("0" + factionalDegreesString.Substring(delimeterPosition));

      // Get the minutes
      decimal minutes = Convert.ToDecimal(factionalDegreesString.Substring(delimeterPosition - 2, 2));

      // Degrees
      decimal degrees= Convert.ToDecimal(factionalDegreesString.Substring(0, delimeterPosition - 2));

      decimal result = degrees + (minutes + fractionalMinutes) / 60;

      if(positve)
        return result;
      else
        return -result;
    }

    ///summary>
    /// Format fractional degrees.
    /// </summary>
    /// <param name="factionalDegrees">Fractional degrees.</param>
    /// <returns>Decimal degrees.</returns>
    public static string formatFractionalDegrees(string factionalDegrees, string direction) 
    {
      string factionalDegreesString = Math.Abs(Convert.ToDecimal(factionalDegrees)).ToString("000.000");

      int delimeterPosition = factionalDegreesString.IndexOf(
        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

      // Get minutes with fractions
      string minutes = factionalDegreesString.Substring(delimeterPosition - 2);

      // Degrees
      string degrees = factionalDegreesString.Substring(0, delimeterPosition - 2).Trim();

      return degrees + "°" + minutes + "\"" + direction;
    }

    /// <summary>
    /// Convert from Celcius to Fahrenheit.
    /// </summary>
    /// <param name="celcius">Degress Celcius</param>
    /// <returns>Degress Fahrenheit</returns>
    public decimal celciusToFahrenheit(decimal celcius)
    {
      // °F = (°C * 9/5) + 32
      return (celcius * 9 / 5) + 32;
    }
    #endregion
	}

  #region Event argument classes

  #region DbtEventArgs
  /// <summary>
  /// Event arguments for DBT - Depth below transducer
  /// </summary>
  public class DbtEventArgs : EventArgs
  {
    /// <summary>
    /// Event arguments for DBT.
    /// </summary>
    /// <param name="depthFeet">Depth in feet.</param>
    /// <param name="depthMeters">Depth in meters.</param>
    /// <param name="depthFathoms">Depth in Fathoms.</param>
    public DbtEventArgs(decimal depthFeet, decimal depthMeters, decimal depthFathoms)
    {
      this.depthFeet = depthFeet;
      this.depthMeters = depthMeters;
      this.depthFathoms = depthFathoms;
    } 
    private decimal depthFeet;
    public decimal DepthFeet
    {
      get { return depthFeet; }
    }
    private decimal depthMeters;
    public decimal DepthMeters
    {
      get { return depthMeters; }
    }
    private decimal depthFathoms;
    public decimal DepthFathoms
    {
      get { return depthFathoms; }
    }
  }
  #endregion

  #region GllEventArgs
  /// <summary>
  /// Event arguments for Geographic Position - Latitude/Longitude
  /// </summary>
  public class GllEventArgs : EventArgs
  {
    /// <summary>
    /// Event arguments for GLL.
    /// </summary>
    /// <param name="depthFeet">Depth in feet.</param>
    /// <param name="depthMeters">Depth in meters.</param>
    /// <param name="depthFathoms">Depth in Fathoms.</param>
    public GllEventArgs(decimal latitude, decimal longitude, string latitudeText, string longitudeText) //, string utc)
    {
      this.latitude = latitude;
      this.longitude = longitude;
      this.latitudeText = latitudeText;
      this.longitudeText = longitudeText;
      //this.utc = utc;
    } 
    private decimal latitude;
    public decimal Latitude
    {
      get { return latitude; }
    }
    private string latitudeText;
    public string LatitudeText
    {
      get { return latitudeText; }
    }
    private decimal longitude;
    public decimal Longitude
    {
      get { return longitude; }
    }
    private string longitudeText;
    public string LongitudeText
    {
      get { return longitudeText; }
    }
    //private string utc;
    //public string Utc
    //{
    //  get { return utc; }
    //}
  }
  #endregion

  #region MtwEventArgs
  /// <summary>
  /// Event arguments for Water Temperature
  /// </summary>
  public class MtwEventArgs : EventArgs
  {
    /// <summary>
    /// Event arguments for MTW.
    /// </summary>
    /// <param name="celcius">Degrees Celcius.</param>
    /// <param name="fahrenheit">Degrees Fahrenheit.</param>
    public MtwEventArgs(decimal celcius, decimal fahrenheit)
    {
      this.celcius = celcius;
      this.fahrenheit = fahrenheit;
    } 
    private decimal celcius;
    public decimal Celcius
    {
      get { return celcius; }
    }
    private decimal fahrenheit;
    public decimal Fahrenheit
    {
      get { return fahrenheit; }
    }
  }
  #endregion

  #region MwvEventArgs
  /// <summary>
  /// Event arguments for Wind Speed and Angle
  /// </summary>
  public class MwvEventArgs : EventArgs
  {
    /// <summary>
    /// Event arguments for MWV.
    /// </summary>
    /// <param name="windAngle">Wind angle.</param>
    /// <param name="referenceRelative">Is reference "Relative" (or "True")?</param>
    /// <param name="windSpeed">Wind speed.</param>
    /// <param name="speedUnits">Speed units.</param>
    public MwvEventArgs(decimal windAngle, bool referenceRelative, decimal windSpeed, string speedUnits)
    {
      this.windAngle = windAngle;
      this.referenceRelative = referenceRelative;
      this.windSpeed = windSpeed;
      this.speedUnits = speedUnits;
    } 
    private decimal windAngle;
    public decimal WindAngle
    {
      get { return windAngle; }
    }
    private bool referenceRelative;
    public bool ReferenceRelative
    {
      get { return referenceRelative; }
    }
    private decimal windSpeed;
    public decimal WindSpeed
    {
      get { return windSpeed; }
    }
    private string speedUnits;
    public string SpeedUnits
    {
      get { return speedUnits; }
    }
  }
  #endregion

  #region RmcEventArgs
  /// <summary>
  /// Event arguments for Water speed and heading
  /// </summary>
  public class RmcEventArgs : EventArgs
  {
    /// <summary>
    /// Event arguments for VHW.
    /// </summary>
    /// <param name="degreesTrue">Heading degrees (true).</param>
    /// <param name="degreesMagnetic">Heading degrees (magnetic)</param>
    /// <param name="knots">Speed in knots.</param>
    /// <param name="kilometers">Speed in kilometers/hour.</param>
    public RmcEventArgs(decimal heading, decimal speed)
    {
      this.heading = heading;
      this.speed = speed;
    } 
    private decimal heading;
    public decimal Heading
    {
      get { return heading; }
    }
    private decimal speed;
    public decimal Speed
    {
      get { return speed; }
    }
  }
  #endregion

  #endregion
}
