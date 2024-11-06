using System;
using Navipro.SantaMonica.ScaleControl;

namespace ScaleConsole
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class ScaleConsole : Logger
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
			ScaleConsole console = new ScaleConsole();
			console.start();
			
		}


		public void start()
		{
			ScaleControl con = new ScaleControl(this);
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
