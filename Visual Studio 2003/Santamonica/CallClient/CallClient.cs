using System;
using Navipro.BroadWorks.Lib;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.CallClient
{
	/// <summary>
	/// Summary description for CallClient.
	/// </summary>
	public class CallClient
	{

		private Database database;
		private Logger logger;
		private Configuration configuration;

		private Thread thread;
		private bool running;

		public CallClient(Logger logger, Configuration configuration)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;
			this.configuration = configuration;
			this.database = new Database(logger, configuration);

			running = true;

			thread = new Thread(new ThreadStart(run));
			thread.Start();

		}

		public void run()
		{
			log("Handler started.", 0);

			while(running)
			{
				process();

				Thread.Sleep(1000);
			}
			log("Handler stopped.", 0);
		}

		public void stop()
		{
			this.running = false;
		}


		public void process()
		{



		}
	}
}
