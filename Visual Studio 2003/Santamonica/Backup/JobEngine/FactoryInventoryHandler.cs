using System;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using System.Collections;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for LineOrderAssigner.
	/// </summary>
	public class FactoryInventoryHandler
	{
		private Logger logger;
		private Configuration configuration;

		private Thread thread;
		private bool running;

		public FactoryInventoryHandler(Logger logger, Configuration configuration)
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

			FactoryInventoryQueueEntries factoryInventoryQueueEntries = new FactoryInventoryQueueEntries();
			FactoryInventoryQueueEntry factoryInventoryQueueEntry = factoryInventoryQueueEntries.getFirstEntry(database);
			
			while(factoryInventoryQueueEntry != null)
			{

				log("Processing "+factoryInventoryQueueEntry.lineJournalEntryNo.ToString(), 0);

				LineJournals lineJournals = new LineJournals();
				LineJournal lineJournal = lineJournals.getEntry(database, factoryInventoryQueueEntry.lineJournalEntryNo.ToString());
				if (lineJournal != null)
				{
					lineJournal.updateEstimatedInventory(database);
				}

				factoryInventoryQueueEntry.delete(database);
				factoryInventoryQueueEntry = factoryInventoryQueueEntries.getFirstEntry(database);
			}

			updateConsumerStatus(database);

			database.close();
		}

		private void updateConsumerStatus(Database database)
		{
			Consumers consumers = new Consumers();
			DataSet consumerDataSet = consumers.getDataSet(database);
			int i = 0;
			while (i < consumerDataSet.Tables[0].Rows.Count)
			{
				Consumer consumer = new Consumer(consumerDataSet.Tables[0].Rows[i]);
				
				DateTime currentHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);

				ConsumerStatuses consumerStatuses = new ConsumerStatuses();
				ConsumerStatus consumerStatus = consumerStatuses.getEntry(database, consumer.no);
				if (consumerStatus == null)
				{
					consumerStatus = new ConsumerStatus();
				}

			
				ConsumerInventories consumerInventories = new ConsumerInventories();
				ConsumerInventory consumerInventory = consumerInventories.getEntry(database, consumer.no, currentHour);

				if (consumerInventory != null)
				{
					if ((consumerStatus.updatedDateTime.ToString("yyyy-MM-dd HH:00:00") != DateTime.Now.ToString("yyyy-MM-dd HH:00:00")) || (consumerStatus.inventoryLevel != consumerInventory.inventory))
					{
						consumerStatuses.updateStatus(database, consumer.no, consumerInventory.inventory);
						log("Consumer status for "+consumer.no+" updated.", 0);
					}
				}
				else
				{
					if (consumerStatus.updatedDateTime.ToString("yyyy-MM-dd HH:00:00") != DateTime.Now.ToString("yyyy-MM-dd HH:00:00"))
					{
						consumerStatuses.updateStatus(database, consumer.no, 0);
						log("Consumer status for "+consumer.no+" updated.", 0);
					}
				}



				i++;
			}


		}



		private void log(string message, int type)
		{
			logger.write("[FactoryInventoryUpdater] "+message, type);
		}

	}
}
