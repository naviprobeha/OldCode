using System;
using Navipro.SantaMonica.Goldfinger;
using Navipro.SantaMonica.Common;

namespace GoldfingerConsoleApp
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class GoldfingerConsoleApp : Logger
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
			GoldfingerConsoleApp app = new GoldfingerConsoleApp();
			app.run();
		}
		#region Logger Members

		public void run()
		{
			//Goldfinger goldfinger = new Goldfinger(this);
			//if (goldfinger.init())
			//{
			//	goldfinger.start();

			//	Console.ReadLine();

			//	goldfinger.stop();

			//}
			
			Console.ReadLine();
		}

		public void write(string message, int type)
		{
			Console.Out.WriteLine(message);
		}

		#endregion
	}
}
