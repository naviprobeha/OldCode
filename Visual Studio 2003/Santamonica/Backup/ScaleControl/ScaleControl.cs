using System;
using System.Xml;
using System.Data;
using System.Threading;
using System.Collections;

namespace Navipro.SantaMonica.ScaleControl
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class ScaleControl
	{
		private Logger logger;

		private Configuration configuration;

		private LoadHandler loadHandler;
		private ScaleHandler scaleHandler;

		public ScaleControl(Logger logger)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;

			log("Starting ScaleControl...", 0);

		}


		public bool start()
		{
			log("Starting handlers...", 0);

			configuration = new Configuration();
			if (!configuration.init())
			{
				logger.write("Configuration failed to load:"+configuration.errorMessage, 2);
				return false;
			}



			loadHandler = new LoadHandler(configuration, logger);
			loadHandler.start();

			scaleHandler = new ScaleHandler(configuration, logger);
			scaleHandler.start();

			return true;
			
		}



		public void stop()
		{
			log("Stopping...", 0);

			loadHandler.stop();
			scaleHandler.stop();

		}

		public void log(string message, int type)
		{
			logger.write("[ScaleControl] "+message, type);
		}
	}
}
