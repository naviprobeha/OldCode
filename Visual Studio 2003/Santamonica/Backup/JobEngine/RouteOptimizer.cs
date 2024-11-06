using System;
using System.Data;
using System.Threading;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for RouteOptimizer.
	/// </summary>
	public class RouteOptimizer
	{
		private Logger logger;
		private Configuration configuration;

		private Thread thread;
		private bool running;

		public RouteOptimizer(Logger logger, Configuration configuration)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;
			this.configuration = configuration;
			running = true;

			thread = new Thread(new ThreadStart(run));
			thread.Start();

		}

		public RouteOptimizer(Logger logger, Configuration configuration, bool exportCustomers)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;
			this.configuration = configuration;

		}

		public void run()
		{
			log("Handler started.", 0);

			while(running)
			{
				try
				{
					process();
				}
				catch(Exception e)
				{
					log("Exception: "+e.Message, 2);
				}

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
			Database database = new Database(logger, configuration);

			//Optimize Routes
			Optimization optimization = new Optimization(database, configuration, logger);

			LineJournals lineJournals = new LineJournals();
			DataSet lineJournalsDataSet = lineJournals.getStatusDataSet(database, 1);
			int j = 0;
			while (j < lineJournalsDataSet.Tables[0].Rows.Count)
			{
				LineJournal lineJournal = new LineJournal(lineJournalsDataSet.Tables[0].Rows[j]);
				
				if (lineJournal.forcedAssignment)
				{
					optimization.optimizeSingleRoute(lineJournal);
				}
				else
				{
					optimization.optimizeMultiRoute(lineJournal);
				}

				j++;
			}

			removeEmptyRoutes(database);

			database.close();
		}

		private void log(string message, int type)
		{
			logger.write("[RouteOptimizer] "+message, type);
		}

		public void exportCustomers()
		{
			Database database = new Database(logger, configuration);

			Optimization optimization = new Optimization(database, configuration, logger);
			optimization.createCustomerFile();

			database.close();
		}

		private void removeEmptyRoutes(Database database)
		{
			LineJournals lineJournals = new LineJournals();
			DataSet lineJournalDataSet = lineJournals.getDataSet(database);

			int i = 0;
			while (i < lineJournalDataSet.Tables[0].Rows.Count)
			{
				LineJournal lineJournal = new LineJournal(lineJournalDataSet.Tables[0].Rows[i]);
				if (lineJournal.countOrders(database) == 0) lineJournal.delete(database);

				i++;
			}

		}
	}
}
