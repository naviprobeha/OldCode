using System;
using Navipro.SantaMonica.Common;
using Navipro.SantaMonica.NavisionConnector;

namespace NavisionConnectorConsoleApp
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class NavConConsole : Logger
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
			NavConConsole console = new NavConConsole();
			console.start();
			
		}
		
		public void start()
		{
			NavisionConnector con = new NavisionConnector(this);
			con.start();
			Console.ReadLine();
			con.stop();
		}

		#region Logger Members

		public void write(string message, int type)
		{
			// TODO:  Add NavConConsole.write implementation
			Console.Out.WriteLine(message);
		}

		#endregion
	}
}
