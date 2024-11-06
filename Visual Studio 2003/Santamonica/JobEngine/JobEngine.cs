using System;
using System.Threading;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class JobEngine
	{
		private Logger logger;
		private Configuration configuration;

		private ContainerQueueHandler containerQueueHandler;
		private LineOrderAssigner lineOrderAssigner;
		private LineOrderManagement lineOrderManagement;
		private RouteOptimizer routeOptimizer;
		private FactoryInventoryHandler factoryInventoryHandler;
		private FactoryOrderManagement factoryOrderManagement;
		private CallHandler callHandler;
		private ReportHandler reportHandler;
		private SMSHandler smsHandler;


		public JobEngine(Logger logger)
		{
			//
			// TODO: Add constructor logic here
			//

			this.logger = logger;

		}

		private bool init()
		{

			configuration = new Configuration();
			if (!configuration.init())
			{
				log("Configuration failed to load.", 2);
				return false;
			}

			return true;
		}


		public bool start()
		{

			log("Starting JobEngine...", 0);

			if (!init()) return false;

			containerQueueHandler = new ContainerQueueHandler(logger, configuration);
			lineOrderAssigner = new LineOrderAssigner(logger, configuration);
			lineOrderManagement = new LineOrderManagement(logger, configuration);
			routeOptimizer = new RouteOptimizer(logger, configuration);
			factoryInventoryHandler = new FactoryInventoryHandler(logger, configuration);
			factoryOrderManagement = new FactoryOrderManagement(logger, configuration);
			callHandler = new CallHandler(logger, configuration);
			reportHandler = new ReportHandler(logger, configuration);
			smsHandler = new SMSHandler(logger, configuration);

			log("JobEngine started.", 0);

			return true;
		}


		public void stop()
		{
			log("Stopping...", 0);

			containerQueueHandler.stop();
			lineOrderAssigner.stop();
			lineOrderManagement.stop();
			routeOptimizer.stop();
			factoryInventoryHandler.stop();
			factoryOrderManagement.stop();
			callHandler.stop();
			reportHandler.stop();
			smsHandler.stop();

		}

		private void log(string message, int type)
		{
			logger.write("[JobEngine] "+message, type);
		}

		public void exportCustomerFile()
		{
			if (!init()) return;
			
			RouteOptimizer routeOptimizer = new RouteOptimizer(logger, configuration, true);
			routeOptimizer.exportCustomers();
		}
	}
}
