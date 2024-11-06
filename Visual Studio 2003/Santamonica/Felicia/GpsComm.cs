using System;
using System.Collections;
using SerialNET;
using System.Threading;
using Microsoft.WindowsMobile.Samples.Location;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for GpsComm.
	/// </summary>
	public class GpsComm
	{
		public delegate void positionUpdateEventHandler(object sender, EventArgs e, int x, int y, int height);
		public delegate void headingUpdateEventHandler(object sender, EventArgs e, int heading, int speed);
		public delegate void dataUpdateEventHandler(object sender, EventArgs e, string data);

		public event positionUpdateEventHandler onPositionUpdate;
		public event headingUpdateEventHandler onHeadingUpdate;
		public event dataUpdateEventHandler onDataUpdate;

		private string inputString;
		private Port gpsPort;
		private ArrayList listeners;

		private NmeaHelper nmea;
		private SmartDatabase smartDatabase;

		private Gps gpsIntermideateDriver;

		public GpsComm(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;


			//NMEA Helper
			nmea = new NmeaHelper();

			nmea.Dbt += new Navipro.SantaMonica.Felicia.NmeaHelper.DbtEventHandler(nmea_Dbt);
			nmea.Gll += new Navipro.SantaMonica.Felicia.NmeaHelper.GllEventHandler(nmea_Gll);
			nmea.Mtw += new Navipro.SantaMonica.Felicia.NmeaHelper.MtwEventHandler(nmea_Mtw);
			nmea.Mwv += new Navipro.SantaMonica.Felicia.NmeaHelper.MwvEventHandler(nmea_Mwv);
			nmea.Rmc += new Navipro.SantaMonica.Felicia.NmeaHelper.RmcEventHandler(nmea_Rmc);
			

			
			if (smartDatabase.getSetup().gpsComPort > 0)
			{
				//Direct Serial Communication
				SerialNET.License gpsLicense = new SerialNET.License();
				gpsLicense.LicenseKey = "f0FGYKKBhLaWA7I1G5KziVM9kfOhUruyfScd";

				listeners = new ArrayList();

				gpsPort = new Port();
				gpsPort.ComPort = smartDatabase.getSetup().gpsComPort;
				gpsPort.BaudRate = smartDatabase.getSetup().gpsBaudRate;
				gpsPort.ByteSize = 8;
				gpsPort.Parity = SerialNET.Parity.No;
				gpsPort.StopBits = SerialNET.StopBits.One;
				gpsPort.BufferSize = 10;

				gpsPort.OnRead +=new OnRead(gpsPort_OnRead);		

			}

			//GPS Intermideate Driver
			this.gpsIntermideateDriver = new Gps();
			this.gpsIntermideateDriver.DeviceStateChanged+=new DeviceStateChangedEventHandler(gpsIntermideateDriver_DeviceStateChanged);
			this.gpsIntermideateDriver.LocationChanged+=new LocationChangedEventHandler(gpsIntermideateDriver_LocationChanged);



			inputString = "";

			if (smartDatabase.getSetup().gpsComPort > 0)
			{
				gpsPort.Enabled = true;
			}
			else
			{
				this.gpsIntermideateDriver.Open();
			}
		}

		public void close()
		{

			gpsPort.Enabled = false;
			gpsPort.Dispose();

			this.gpsIntermideateDriver.Close();			

		}

		private void gpsPort_OnRead(string Data)
		{
			try
			{
				inputString = inputString + Data;

			
				char nl = (char)13;
			
				if (inputString.IndexOf(nl) > -1)
				{
					string gpsString = inputString.Substring(1, inputString.IndexOf(nl)-1);
					if (gpsString[0] == (char)10) gpsString = gpsString.Substring(1);
					inputString = inputString.Substring(inputString.IndexOf(nl)+1);

					//System.Windows.Forms.MessageBox.Show(gpsString);

				
					nmea.ParseSentence(gpsString);
					
					onDataUpdate(this, null, gpsString);
				}
				// display as text
			}
			catch(Exception e)
			{
				//System.Windows.Forms.MessageBox.Show("Hepp: "+e.Message);
				if (e.Message != "") {}

			}

		}

		private void nmea_Dbt(object sender, DbtEventArgs e)
		{
			
		}

		private void nmea_Gll(object sender, GllEventArgs e)
		{
			try
			{
			
				//GaussKrugerProjection proj = new GaussKrugerProjection("2.5V");
				NavGaussKruger proj = new NavGaussKruger("rt90_2.5_gon_v");
				int[] xy = proj.GetRT90(e.LatitudeText, e.LongitudeText);

				//notifyListenersPosition(xy[1], xy[0], 0);			
				onPositionUpdate(sender, e, xy[1], xy[0], 0);

			}
			catch(Exception)
			{}
		}

		private void nmea_Mtw(object sender, MtwEventArgs e)
		{
		 			
		}

		private void nmea_Mwv(object sender, MwvEventArgs e)
		{
			
		}

		private void nmea_Rmc(object sender, RmcEventArgs e)
		{
			//notifyListenersHeading((int)e.Heading, (int)e.Speed);
			onHeadingUpdate(sender, e, (int)e.Heading, (int)(((double)e.Speed)*1.852));
		}

		private void gpsIntermideateDriver_DeviceStateChanged(object sender, DeviceStateChangedEventArgs args)
		{
			
		}

		private void gpsIntermideateDriver_LocationChanged(object sender, LocationChangedEventArgs args)
		{
			string lat = args.Position.LatitudeInDegreesMinutesSeconds.Degrees.ToString()+"°"+(args.Position.LatitudeInDegreesMinutesSeconds.Minutes+(args.Position.LatitudeInDegreesMinutesSeconds.Seconds / 60)).ToString()+"\"";
			string lon = args.Position.LongitudeInDegreesMinutesSeconds.Degrees.ToString()+"°"+(args.Position.LongitudeInDegreesMinutesSeconds.Minutes+(args.Position.LongitudeInDegreesMinutesSeconds.Seconds / 60)).ToString()+"\"";


			//notifyListenersPosition(xy[1], xy[0], 0);			
			//onPositionUpdate(sender, e, xy[1], xy[0], 0);

			onDataUpdate(this, null, "Pos: "+lat+";"+lon);


			NavGaussKruger proj = new NavGaussKruger("rt90_2.5_gon_v");
			int[] xy = proj.GetRT90(lat, lon);

			if ((xy[1] > 0) && (xy[0] > 0)) onPositionUpdate(sender, args, xy[1], xy[0], 0);

				

			onHeadingUpdate(sender, args, (int)args.Position.Heading, (int)(((double)args.Position.Speed)*1.852));

		}
	}
}
