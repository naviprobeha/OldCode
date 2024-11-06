using System;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for GpsListener.
	/// </summary>
	public interface GpsListener
	{
		void onRawDataReceive(string gpsString);
		void onPositionReceive(int x, int y, int height);
		void onHeadingReceive(int heading, int speed);
	}
}
