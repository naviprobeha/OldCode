using System;
using System.Data;
using System.Threading;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine

{
	/// <summary>
	/// Summary description for LineOrderManagement.
	/// </summary>
	public class LineOrderManagement
	{
		private Configuration configuration;
		private Logger logger;

		private Thread thread;
		private bool running;

		public LineOrderManagement(Logger logger, Configuration configuration)
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

			//this.recalculateArrivalTime(database, "104");

			while(running)
			{
				try
				{
					this.processUnHandledOrders();
					this.processUnhandledAwaitingOrders();
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


		public void processUnHandledOrders()
		{
			Database database = new Database(logger, configuration);

			if ((DateTime.Now.Hour >= 6) && (DateTime.Now.Hour < 8))
			{
				LineOrders lineOrders = new LineOrders();
				DataSet lineOrderDataSet = lineOrders.getUnhandledDataSet(database);

				if (lineOrderDataSet.Tables[0].Rows.Count > 0)
				{
					int i = 0;
					while (i < lineOrderDataSet.Tables[0].Rows.Count)
					{
						LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
						lineOrder.shipDate = DateTime.Today;

						if (lineOrder.shipTime.ToString("HH:mm") != "00:00")
						{
							DateTime shipDateTime = new DateTime(lineOrder.shipDate.Year, lineOrder.shipDate.Month, lineOrder.shipDate.Day, lineOrder.shipTime.Hour, lineOrder.shipTime.Minute, 0);

							if (shipDateTime > lineOrder.confirmedToDateTime)
							{
								lineOrder.shipTime = new DateTime(1754, 1, 1, 8, 0, 0);
							}
						}

						lineOrder.save(database);

						if (lineOrder.lineJournalEntryNo > 0)
						{
							LineJournal lineJournal = lineOrder.getJournal(database);
							if (lineJournal != null)
							{	
								if (lineJournal.shipDate < DateTime.Today)
								{
									lineJournal.shipDate = DateTime.Today;
									lineJournal.save(database);

								}

							}

						}

						i++;
					}
				}
			}

			database.close();
		}

		public void processUnhandledAwaitingOrders()
		{
			Database database = new Database(logger, configuration);


			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getUnhandledAwaitingDataSet(database);

			if (lineOrderDataSet.Tables[0].Rows.Count > 0)
			{
				int i = 0;
				while (i < lineOrderDataSet.Tables[0].Rows.Count)
				{
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
					lineOrder.status = 0;

					lineOrder.save(database);


					i++;
				}
			}
			
			database.close();
		}



		public void recalculateArrivalTime(Database database, string lineJournalNo)
		{

			LineJournals lineJournals = new LineJournals();
			LineJournal lineJournal = lineJournals.getEntry(database, lineJournalNo);

			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo);
			
			//Get departure time

			log("ArrivalTime - Get departure time", 0);

			DateTime departureDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			DateTime arrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);

			int i = 0;
			while(i < lineOrderDataSet.Tables[0].Rows.Count)
			{
				LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

				if (lineOrder.confirmedToDateTime > departureDateTime) departureDateTime = lineOrder.confirmedToDateTime;

				i++;
			}

			if (departureDateTime < DateTime.Now) departureDateTime = DateTime.Now;

			log("ArrivalTime - Departure time: "+departureDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 0);

			// Find container lead time

			log("ArrivalTime - Find container lead time", 0);
			
			Organizations organizations = new Organizations();
			Organization organization = organizations.getOrganization(database, lineJournal.organizationNo);

			int qtyContainers = lineJournal.countContainers(database);
			int containerLeadTime = qtyContainers * organization.containerLoadTime;

			log("ArrivalTime - Container lead time: "+containerLeadTime, 0);

			// Set arrival time

			log("ArrivalTime - Set arrival time", 0);

			arrivalDateTime = departureDateTime.AddMinutes(containerLeadTime + lineJournal.totalTime);

			log("ArrivalTime - Arrival time: "+arrivalDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 0);

			// Check loaded lineOrders

			log("ArrivalTime - Check loaded containers", 0);

			i = 0;
			bool prevPickedUp = true;

			DateTime calculatedArrivalDateTime = departureDateTime;

			if (lineOrderDataSet.Tables[0].Rows.Count > 0)
			{
				while(i < lineOrderDataSet.Tables[0].Rows.Count)
				{
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

					int lineOrderContainers = lineOrder.countContainers(database);

					if ((lineOrder.status == 7) || (lineOrder.status == 10))
					{

						if (prevPickedUp)
						{
							if (departureDateTime.AddMinutes(lineOrder.travelTime + (organization.containerLoadTime*lineOrderContainers)) > lineOrder.closedDateTime) departureDateTime = lineOrder.closedDateTime.AddMinutes((lineOrder.travelTime + (organization.containerLoadTime*lineOrderContainers)) * -1);
							calculatedArrivalDateTime = lineOrder.closedDateTime;
						}
						else
						{
							calculatedArrivalDateTime = calculatedArrivalDateTime.AddMinutes(lineOrder.travelTime + (lineOrderContainers*organization.containerLoadTime));
						}
					}
					else
					{
						prevPickedUp = false;

						calculatedArrivalDateTime = calculatedArrivalDateTime.AddMinutes(lineOrder.travelTime + (lineOrderContainers*organization.containerLoadTime));
					}


					i++;

					log("ArrivalTime - Loop Arrival time: "+calculatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 0);

				}

				calculatedArrivalDateTime = calculatedArrivalDateTime.AddMinutes(lineJournal.endingTravelTime);
			}

			log("ArrivalTime - Arrival time: "+calculatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 0);

			lineJournal.departureDateTime = departureDateTime;
			lineJournal.arrivalDateTime = calculatedArrivalDateTime;
			lineJournal.save(database);
		}


		private void log(string message, int type)
		{
			logger.write("[LineOrderManagement] "+message, type);
		}


	}
}
