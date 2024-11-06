using System;
using Navipro.SantaMonica.Common;
using Navipro.SantaMonica.JobEngine;

namespace JobEngineConsole
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class JobEngineConsole : Logger
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 

		private int mode;

		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
		
			if (args.Length > 0)
			{
				if (args[0] == "/c")
				{
					JobEngineConsole console = new JobEngineConsole(1);
					console.start();
				}
			}
			else
			{
				JobEngineConsole console = new JobEngineConsole();
				console.start();
			}
		}

		
		public JobEngineConsole()
		{
			mode = 0;

		}

		public JobEngineConsole(int mode)
		{
			this.mode = mode;

		}

		public void start()
		{
			JobEngine con = new JobEngine(this);

			if (mode == 0)
			{
				con.start();
				Console.ReadLine();
				con.stop();
			}
			else
			{
				con.exportCustomerFile();
			}
		}

		#region Logger Members

		public void write(string message, int type)
		{
			// TODO:  Add NavConConsole.write implementation
			//Console.Out.WriteLine(message);

			string handlerName = "jobengine.log";
			if (message.IndexOf("]") > 0)
			{
				handlerName = message.Substring(1, message.IndexOf("]")-1);
			}

			System.IO.StreamWriter streamWriter = System.IO.File.AppendText(handlerName+".log");
			streamWriter.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]")+message);
			streamWriter.Close();
		}

		#endregion
	}
}
