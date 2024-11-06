using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLine.
	/// </summary>
	public class LineJournal
	{

		public int entryNo;
		public string organizationNo;
		public DateTime shipDate;
		public string agentCode;
		public int status;
		public string departureFactoryCode;
		public string arrivalFactoryCode;
		public DateTime arrivalDateTime;
		public decimal calculatedDistance;
		public decimal measuredDistance;
		public decimal reportedDistance;
		public decimal reportedDistanceSingle;
		public decimal reportedDistanceTrailer;
		public bool forcedAssignment;
		public int endingTravelDistance;
		public int endingTravelTime;
		public int totalDistance;
		public int totalTime;
		public DateTime departureDateTime;
		public bool sentToFactory;
		public string agentStorageGroup;
		public bool invoiceReceived;
		public int parentJournalEntryNo;
		public int dropWaitTime;
		public string factoryConfirmedWaitTime;

		private string updateMethod;

		public LineJournal(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.organizationNo = dataReader.GetValue(1).ToString();
			this.shipDate = dataReader.GetDateTime(2);
			this.agentCode = dataReader.GetValue(3).ToString();
			this.status = dataReader.GetInt32(4);
			this.departureFactoryCode = dataReader.GetValue(5).ToString();
			this.arrivalFactoryCode = dataReader.GetValue(6).ToString();
			DateTime arrivalDate = dataReader.GetDateTime(7);
			DateTime arrivalTime = dataReader.GetDateTime(8);
			this.calculatedDistance = dataReader.GetDecimal(9);
			this.measuredDistance = dataReader.GetDecimal(10);
			this.reportedDistance = dataReader.GetDecimal(11);

			this.arrivalDateTime = new DateTime(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day, arrivalTime.Hour, arrivalTime.Minute, arrivalTime.Second);

			this.forcedAssignment = false;
			if (dataReader.GetValue(12).ToString() == "1") this.forcedAssignment = true;

			this.endingTravelDistance = dataReader.GetInt32(13);
			this.endingTravelTime = dataReader.GetInt32(14);
			this.totalDistance = dataReader.GetInt32(15);
			this.totalTime = dataReader.GetInt32(16);

			DateTime departureDate = dataReader.GetDateTime(17);
			DateTime departureTime = dataReader.GetDateTime(18);

			this.departureDateTime = new DateTime(departureDate.Year, departureDate.Month, departureDate.Day, departureTime.Hour, departureTime.Minute, departureTime.Second);

			this.sentToFactory = false;
			if (dataReader.GetValue(19).ToString() == "1") this.sentToFactory = true;

			this.reportedDistanceSingle = dataReader.GetDecimal(20);
			this.reportedDistanceTrailer = dataReader.GetDecimal(21);

			this.agentStorageGroup = dataReader.GetValue(22).ToString();

			this.invoiceReceived = false;
			if (dataReader.GetValue(23).ToString() == "1") this.invoiceReceived = true;

			this.parentJournalEntryNo = dataReader.GetInt32(24);
			this.dropWaitTime = dataReader.GetInt32(25);

			this.factoryConfirmedWaitTime = dataReader.GetValue(26).ToString();

		}


		public LineJournal(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.organizationNo = dataRow.ItemArray.GetValue(1).ToString();
			this.shipDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.agentCode = dataRow.ItemArray.GetValue(3).ToString();
			this.status = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.departureFactoryCode = dataRow.ItemArray.GetValue(5).ToString();
			this.arrivalFactoryCode = dataRow.ItemArray.GetValue(6).ToString();
			DateTime arrivalDate = DateTime.Parse(dataRow.ItemArray.GetValue(7).ToString());
			DateTime arrivalTime = DateTime.Parse(dataRow.ItemArray.GetValue(8).ToString());
			this.calculatedDistance = decimal.Parse(dataRow.ItemArray.GetValue(9).ToString());
			this.measuredDistance = decimal.Parse(dataRow.ItemArray.GetValue(10).ToString());
			this.reportedDistance = decimal.Parse(dataRow.ItemArray.GetValue(11).ToString());

			this.arrivalDateTime = new DateTime(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day, arrivalTime.Hour, arrivalTime.Minute, arrivalTime.Second);

			this.forcedAssignment = false;
			if (dataRow.ItemArray.GetValue(12).ToString() == "1") this.forcedAssignment = true;

			this.endingTravelDistance = int.Parse(dataRow.ItemArray.GetValue(13).ToString());
			this.endingTravelTime = int.Parse(dataRow.ItemArray.GetValue(14).ToString());
			this.totalDistance = int.Parse(dataRow.ItemArray.GetValue(15).ToString());
			this.totalTime = int.Parse(dataRow.ItemArray.GetValue(16).ToString());

			DateTime departureDate = DateTime.Parse(dataRow.ItemArray.GetValue(17).ToString());
			DateTime departureTime = DateTime.Parse(dataRow.ItemArray.GetValue(18).ToString());

			this.departureDateTime = new DateTime(departureDate.Year, departureDate.Month, departureDate.Day, departureTime.Hour, departureTime.Minute, departureTime.Second);

			this.sentToFactory = false;
			if (dataRow.ItemArray.GetValue(19).ToString() == "1") this.sentToFactory = true;

			this.reportedDistanceSingle = decimal.Parse(dataRow.ItemArray.GetValue(20).ToString());
			this.reportedDistanceTrailer = decimal.Parse(dataRow.ItemArray.GetValue(21).ToString());

			this.agentStorageGroup = dataRow.ItemArray.GetValue(22).ToString();

			this.invoiceReceived = false;
			if (dataRow.ItemArray.GetValue(23).ToString() == "1") this.invoiceReceived = true;

			this.parentJournalEntryNo = int.Parse(dataRow.ItemArray.GetValue(24).ToString());
			this.dropWaitTime = int.Parse(dataRow.ItemArray.GetValue(25).ToString());

			this.factoryConfirmedWaitTime = dataRow.ItemArray.GetValue(26).ToString();

		}

		public LineJournal()
		{
			arrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			departureDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
		}


		public void save(Database database)
		{
			
			int forcedAssignmentVal = 0;
			if (this.forcedAssignment) forcedAssignmentVal = 1;

			int sentToFactoryVal = 0;
			if (this.sentToFactory) sentToFactoryVal = 1;

			int invoiceReceivedVal = 0;
			if (this.invoiceReceived) invoiceReceivedVal = 1;

			if (this.arrivalDateTime.Year < 1753) this.arrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			if (this.departureDateTime.Year < 1753) this.departureDateTime = new DateTime(1753, 1, 1, 0, 0, 0);

			SqlDataReader dataReader = database.query("SELECT [Status] FROM [Line Journal] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				if (int.Parse(dataReader.GetValue(0).ToString()) >= 8)
				{
					//Do not ever EVER open up reported/finished line journals.
					this.status = 8;
				}
				dataReader.Close();

				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Line Journal] WHERE [Entry No] = '"+entryNo+"'");

					SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
					synchQueue.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_LINE_JOURNAL, entryNo.ToString(), 2);

				}

				else
				{

					database.nonQuery("UPDATE [Line Journal] SET [Organization No] = '"+this.organizationNo+"', [Ship Date] = '"+this.shipDate.ToString("yyyy-MM-dd")+"', [Agent Code] = '"+this.agentCode+"', [Status] = '"+this.status+"', [Departure Factory Code] = '"+this.departureFactoryCode+"', [Arrival Factory Code] = '"+this.arrivalFactoryCode+"', [Arrival Date] = '"+this.arrivalDateTime.ToString("yyyy-MM-dd")+"', [Arrival Time] = '"+this.arrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Calculated Distance] = '"+this.calculatedDistance.ToString().Replace(",", ".")+"', [Measured Distance] = '"+this.measuredDistance.ToString().Replace(",", ".")+"', [Reported Distance] = '"+this.reportedDistance.ToString().Replace(",", ".")+"', [Forced Assignment] = '"+forcedAssignmentVal+"', [Ending Travel Distance] = '"+this.endingTravelDistance+"', [Ending Travel Time] = '"+this.endingTravelTime+"', [Total Distance] = '"+this.totalDistance+"', [Total Time] = '"+this.totalTime+"', [Departure Date] = '"+departureDateTime.ToString("yyyy-MM-dd")+"', [Departure Time] = '"+departureDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Sent To Factory] = '"+sentToFactoryVal+"', [Reported Distance Single] = '"+this.reportedDistanceSingle.ToString().Replace(",", ".")+"', [Reported Distance Trailer] = '"+this.reportedDistanceTrailer.ToString().Replace(",", ".")+"', [Agent Storage Group] = '"+this.agentStorageGroup+"', [Invoice Received] = '"+invoiceReceivedVal+"', [Parent Journal Entry No] = '"+parentJournalEntryNo+"', [Drop Wait Time] = '"+dropWaitTime+"', [Factory Confirmed Wait Time] = '"+factoryConfirmedWaitTime+"' WHERE [Entry No] = '"+this.entryNo+"'");


				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Line Journal] ([Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time]) VALUES ('"+this.organizationNo+"','"+this.shipDate+"','"+this.agentCode+"','"+this.status+"','"+this.departureFactoryCode+"','"+this.arrivalFactoryCode+"', '"+this.arrivalDateTime.ToString("yyyy-MM-dd")+"', '"+this.arrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+this.calculatedDistance.ToString().Replace(",", ".")+"', '"+this.measuredDistance.ToString().Replace(",", ".")+"', '"+this.reportedDistance.ToString().Replace(",", ".")+"', '"+forcedAssignmentVal+"', '"+this.endingTravelDistance+"', '"+this.endingTravelTime+"', '"+this.totalDistance+"', '"+this.totalTime+"', '"+this.departureDateTime.ToString("yyyy-MM-dd")+"', '"+this.departureDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+sentToFactoryVal+"', '"+this.reportedDistanceSingle.ToString().Replace(",", ".")+"', '"+this.reportedDistanceTrailer.ToString().Replace(",", ".")+"', '"+this.agentStorageGroup+"', '"+invoiceReceivedVal+"', '"+parentJournalEntryNo+"', '"+dropWaitTime+"', '"+factoryConfirmedWaitTime+"')");
				entryNo = (int)database.getInsertedSeqNo();

			}

		}

		public int getOrderCount(Database database)
		{
			int orderCount = 0;
			
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] WHERE [Line Journal Entry No] = '"+this.entryNo+"'");
			if (dataReader.Read())
			{
				orderCount = dataReader.GetInt32(0);
			}
			
			dataReader.Close();
			return orderCount;


		}

		public DataSet getContainers(Database database)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			return lineOrderContainers.getJournalDataSet(database, this.entryNo);
		}


		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}

		public string getStatusText(Database database)
		{
			if (status == 0) 
			{
				return "";
			}
			if (status == 1)
			{
				return "Ändrad";
			}
			if (status == 2)
			{
				return "Optimering pågår";
			}
			if (status == 3)
			{
				return "Optimerad";
			}
			if (status == 4)
			{
				return "Tilldelad";
			}
			if (status == 5)
			{
				return "Skickad";
			}
			if (status == 6) 
			{
				string statusText = "Lastning påbörjad";
				if (this.allOrdersLoaded(database)) statusText = "Lastad";
				return statusText;
			}
			if (status == 7) 
			{
				return "Lossad";
			}
			if (status == 8) 
			{
				return "Återrapporterad";
			}
			if (status == 9) 
			{
				return "Stängd";
			}

			return "";


		}

		public string getStatusIcon()
		{
			if (status == 0) 
			{
				return "ind_white.gif";
			}
			if (status == 1) 
			{
				return "ind_yellow.gif";
			}
			if (status == 2)
			{
				return "ind_orange.gif";
			}
			if (status == 3)
			{
				return "ind_orange.gif";
			}
			if (status == 4)
			{
				return "ind_orange.gif";
			}
			if (status == 5) 
			{
				return "ind_green.gif";
			}
			if (status == 6) 
			{
				return "ind_green.gif";
			}
			if (status == 7) 
			{
				return "ind_purple.gif";
			}
			if (status == 8) 
			{
				return "ind_black.gif";
			}
			if (status == 9) 
			{
				return "ind_black.gif";
			}

			return "ind_white.gif";
		}

		public int countContainers(Database database)
		{
			return countContainers(database, "");
		}

		public int countContainers(Database database, int calculationType)
		{
			return countContainers(database, "", calculationType);
		}

		public int countContainers(Database database, string routeGroupCode, int calculationType)
		{
			int sum = 0;

			ContainerUnits containerUnits = new ContainerUnits();
			DataSet containerUnitDataSet = containerUnits.getDataSet(database, calculationType);
			int i = 0;
			while (i < containerUnitDataSet.Tables[0].Rows.Count)
			{
				ContainerUnit containerUnit = new ContainerUnit(containerUnitDataSet.Tables[0].Rows[i]);

				if (calculationType == 0) sum = sum + countUnitContainers(database, routeGroupCode, containerUnit.code);	
				if (calculationType == 1) sum = sum + (calcUnitContainers(database, routeGroupCode, containerUnit.code) * containerUnit.volumeFactor);	

				i++;
			}

			return sum;
		}

		private int countUnitContainers(Database database, string unitCode)
		{
			return countUnitContainers(database, "", unitCode);
		}


		public int countContainers(Database database, string routeGroupCode)
		{
			string routeGroupQuery = "";
			if (routeGroupCode != "") routeGroupQuery = "AND o.[Route Group Code] = '"+routeGroupCode+"' ";

			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] o, [Line Order Container] c WHERE o.[Line Journal Entry No] = '"+entryNo+"' AND o.[Entry No] = c.[Line Order Entry No] "+routeGroupQuery);
	
			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;

		}

		private int countUnitContainers(Database database, string routeGroupCode, string unitCode)
		{

			string routeGroupQuery = "";
			if (routeGroupCode != "") routeGroupQuery = "AND o.[Route Group Code] = '"+routeGroupCode+"' ";

			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] o, [Line Order Container] oc, [Container] c, [Container Type] ct WHERE o.[Line Journal Entry No] = '"+entryNo+"' AND o.[Entry No] = oc.[Line Order Entry No] AND oc.[Container No] = c.[No] AND c.[Container Type Code] = ct.[Code] AND ct.[Unit Code] = '"+unitCode+"' "+routeGroupQuery);
	
			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;

		}

		public int calcUnitContainers(Database database, string routeGroupCode, string unitCode)
		{
			string routeGroupQuery = "";
			if (routeGroupCode != "") routeGroupQuery = "AND o.[Route Group Code] = '"+routeGroupCode+"' ";
	
			ContainerTypes containerTypes = new ContainerTypes();
			DataSet containerTypeDataSet = containerTypes.getDataSet(database, unitCode);

			int volume = 0;

			int i = 0;
			while (i < containerTypeDataSet.Tables[0].Rows.Count)
			{

				SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] o, [Line Order Container] oc, [Container] c WHERE oc.[Line Order Entry No] = o.[Entry No] AND o.[Line Journal Entry No] = '"+this.entryNo+"' AND c.[No] = oc.[Container No] AND c.[Container Type Code] = '"+containerTypeDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"' "+routeGroupQuery);
	
				int count = 0;

				if (dataReader.Read())
				{
					count = int.Parse(dataReader.GetValue(0).ToString());
				}
				dataReader.Close();

				volume = volume + (int.Parse(containerTypeDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) * count);

				i++;
			}

			return volume;


		}


		public int countOrders(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] WHERE [Line Journal Entry No] = '"+this.entryNo+"'");

			int count = 0;
			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;
		}

		public bool includesOrder(Database database, int entryNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Line Order] WHERE [Entry No] = '"+entryNo+"' AND [Line Journal Entry No] = '"+this.entryNo+"'");
	
			bool exists = false;

			if (dataReader.Read())
			{
				exists = true;
			}
			dataReader.Close();

			return exists;

		}

		public bool allOrdersLoaded(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Line Order] WHERE [Line Journal Entry No] = '"+this.entryNo+"' AND [Is Loaded] = 0");
	
			bool nonLoadedOrdersExists = false;

			if (dataReader.Read())
			{
				nonLoadedOrdersExists = true;
			}
			dataReader.Close();

			if (nonLoadedOrdersExists) return false;
			return true;

		}

		public int countOrdersLoaded(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] WHERE [Line Journal Entry No] = '"+this.entryNo+"' AND [Is Loaded] = 1");
	
			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;

		}


		public void unassignOrder(Database database, int lineOrderEntryNo)
		{
			// Sets status to Re-plan on old route.

			this.status = 1; // Re-plan
			save(database);
		}

		public void assignOrder(Database database, int lineOrderEntryNo)
		{
			//Sets status to Re-plan on new route.

			this.status = 1; // Re-plan
			save(database);
		}

		public void updateJournal(Database database)
		{
			if (status >= 4)
			{

				SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
				synchQueue.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_LINE_JOURNAL, entryNo.ToString(), 0);
			
				LineOrders lineOrders = new LineOrders();
				DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, this.entryNo);
				int i = 0;
				while (i < lineOrderDataSet.Tables[0].Rows.Count)
				{
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
	
					synchQueue.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER, lineOrder.entryNo.ToString(), 0);

		

					LineOrderContainers lineOrderContainers = new LineOrderContainers();
					DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, lineOrder.entryNo);

					int j = 0;
					while (j < lineOrderContainerDataSet.Tables[0].Rows.Count)
					{
						synchQueue.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER_CONTAINER, lineOrderContainerDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), 0);
				
						j++;
					}

					i++;
				}

			}
		}

		public void removeJournal(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
			synchQueue.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_LINE_JOURNAL, entryNo.ToString(), 2);
			
			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, this.entryNo);
			int i = 0;
			while (i < lineOrderDataSet.Tables[0].Rows.Count)
			{
				LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
	
				synchQueue.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER, lineOrder.entryNo.ToString(), 2);

				if (lineOrder.status < 7)
				{
					lineOrder.status = 3;
					lineOrder.save(database, false);
				}

				LineOrderContainers lineOrderContainers = new LineOrderContainers();
				DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, lineOrder.entryNo);

				int j = 0;
				while (j < lineOrderContainerDataSet.Tables[0].Rows.Count)
				{
					synchQueue.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER_CONTAINER, lineOrderContainerDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), 2);
				
					j++;
				}

				i++;
			}

		}

		public void setOrdersAssigned(Database database)
		{

			if (status >= 4)
			{
				SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
				synchQueue.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_LINE_JOURNAL, entryNo.ToString(), 0);


				LineOrders lineOrders = new LineOrders();
				DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, this.entryNo);
				int i = 0;
				while (i < lineOrderDataSet.Tables[0].Rows.Count)
				{
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
	
					if (lineOrder.status < 5) lineOrder.status = 4;
					if (lineOrder.checkOrderLoaded(database)) lineOrder.status = 7;

					lineOrder.save(database, true);
					i++;
				}

			}
		}

		public void recalculateArrivalTime(Database database)
		{
			if (this.isReadyToSend(database))
			{
				LineOrders lineOrders = new LineOrders();
				DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, entryNo);
			
				//Get departure time

				DateTime departureDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
				DateTime firstLoadingDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
				DateTime arrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
				DateTime calculatedArrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);

				int i = 0;
				while(i < lineOrderDataSet.Tables[0].Rows.Count)
				{
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

					if (firstLoadingDateTime.Year == 1753) firstLoadingDateTime = lineOrder.confirmedToDateTime;
					if (lineOrder.confirmedToDateTime < firstLoadingDateTime) firstLoadingDateTime = lineOrder.confirmedToDateTime;

					i++;
				}

				if (firstLoadingDateTime < DateTime.Now) firstLoadingDateTime = DateTime.Now;


				// Find container lead time
			
				Organizations organizations = new Organizations();
				Organization organization = organizations.getOrganization(database, this.organizationNo);

				int qtyContainers = this.countContainers(database);
				int containerLeadTime = qtyContainers * organization.containerLoadTime;

				System.Collections.ArrayList lineJournalList = null;

				// Check loaded lineOrders

				i = 0;
				bool prevPickedUp = true;

				if (lineOrderDataSet.Tables[0].Rows.Count > 0)
				{
					// Set departure time
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[0]);
					int firstTravelTime = lineOrder.travelTime;
					departureDateTime = firstLoadingDateTime.AddMinutes(firstTravelTime*-1);

					//Console.WriteLine("Startdate: "+departureDateTime.ToString("yyyy-MM-dd HH:mm"));

					// Planning by Departure and Arrival time
					LineJournals lineJournals = new LineJournals();
					lineJournalList = lineJournals.getPlanningJournalsByArrivalTime(database, this.agentCode);

					//Console.WriteLine("Orders loaded: "+this.countOrdersLoaded(database));
					if (this.countOrdersLoaded(database) == 0)
					{
						int j = 0;
						while (j < lineJournalList.Count)
						{
							LineJournal planLineJournal = (LineJournal)lineJournalList[j];
							//Console.WriteLine("Comparing to route: "+planLineJournal.entryNo);
							if (((planLineJournal.entryNo < this.entryNo) && (planLineJournal.arrivalDateTime.Year >= 2000)) || (planLineJournal.countOrdersLoaded(database) > 0))
							{
								if (planLineJournal.entryNo > this.entryNo)
								{
									planLineJournal.recalculateArrivalTime(database);
								}

								//Console.WriteLine("Applying route: "+planLineJournal.entryNo);
								departureDateTime = planLineJournal.arrivalDateTime.AddMinutes(containerLeadTime);
							}
							j++;
						}
					}

					//Console.WriteLine("Startdate: "+departureDateTime.ToString("yyyy-MM-dd HH:mm"));

					// Set arrival time
					firstLoadingDateTime = departureDateTime.AddMinutes(firstTravelTime);
					arrivalDateTime = departureDateTime.AddMinutes(containerLeadTime + this.totalTime);
					calculatedArrivalDateTime = departureDateTime;


					while(i < lineOrderDataSet.Tables[0].Rows.Count)
					{
						lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

						int lineOrderContainers = lineOrder.countContainers(database);

						if ((lineOrder.status == 7) || (lineOrder.status == 10))
						{

							if (prevPickedUp)
							{
								if (lineOrder.closedDateTime.Year < 2000) lineOrder.closedDateTime = new DateTime(lineOrder.shipDate.Year, lineOrder.shipDate.Month, lineOrder.shipDate.Day, lineOrder.closedDateTime.Hour, lineOrder.closedDateTime.Minute, lineOrder.closedDateTime.Second);

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

					}

					calculatedArrivalDateTime = calculatedArrivalDateTime.AddMinutes(this.endingTravelTime);
				}
	
				//Console.WriteLine("Setting start: "+departureDateTime.ToString("yyyy-MM-dd HH:mm"));
				//Console.WriteLine("Setting end  : "+calculatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm"));


				this.departureDateTime = departureDateTime;
				this.arrivalDateTime = calculatedArrivalDateTime;
				save(database);

				//Console.WriteLine("Set start: "+departureDateTime.ToString("yyyy-MM-dd HH:mm"));
				//Console.WriteLine("Set end  : "+arrivalDateTime.ToString("yyyy-MM-dd HH:mm"));


				// Re-schedule other routes
				if (lineJournalList != null)
				{
					LineJournals lineJournals = new LineJournals();

					int k = 0;
					while (k < lineJournalList.Count)
					{
						LineJournal planLineJournal = (LineJournal)lineJournalList[k];
						planLineJournal = lineJournals.getEntry(database, planLineJournal.entryNo.ToString());

						if ((planLineJournal.entryNo > this.entryNo) && (planLineJournal.countOrdersLoaded(database) == 0))
						{							
							//Console.WriteLine("Planning route: "+planLineJournal.entryNo);
							planLineJournal.recalculateArrivalTime(database);
						}
						k++;
					}
				}

				enqueueInventoryUpdate(database);
				this.updateEstimatedInventory(database);

			}
			else
			{
				this.departureDateTime = new DateTime(1753, 1, 1, 0, 0,0);
				this.arrivalDateTime = new DateTime(1753, 1, 1, 0, 0,0);
				save(database);

			}
		}

		public void applyLineOrderAgent(Database database)
		{
			Organizations organizations = new Organizations();
			Organization organization = organizations.getOrganization(database, this.organizationNo);
			if (organization != null)
			{
				System.Collections.Hashtable agentHashTable = new System.Collections.Hashtable();			
				System.Collections.ArrayList agentList = new System.Collections.ArrayList();			

				Agents agents = new Agents();
			
				LineOrders lineOrders = new LineOrders();
				DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, this.entryNo);

				int i = 0;
				while (i < lineOrderDataSet.Tables[0].Rows.Count)
				{
					LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

					if (agentHashTable.Contains(lineOrder.planningAgentCode.ToString()))
					{
						agentHashTable[lineOrder.planningAgentCode.ToString()] = (int.Parse(agentHashTable[lineOrder.planningAgentCode.ToString()].ToString()) + 1).ToString();
					}
					else
					{
						agentHashTable.Add(lineOrder.planningAgentCode.ToString(), "1");
						agentList.Add(lineOrder.planningAgentCode.ToString());
					}

					i++;
				}

				if (agentList.Count == 0)
				{
					if (!organization.autoAssignJournals) return;
				
					DataSet agentDataSet = agents.getDataSet(database, organizationNo, this.agentStorageGroup);
					if (agentDataSet.Tables[0].Rows.Count > 0) 
					{
						System.Collections.Hashtable planningTable = new System.Collections.Hashtable();
						System.Collections.ArrayList planningList = new System.Collections.ArrayList();

						int k = 0;
						while (k < agentDataSet.Tables[0].Rows.Count)
						{
							LineJournals lineJournals = new LineJournals();
							System.Collections.ArrayList journalList = lineJournals.getPlanningJournalsByArrivalTime(database, agentDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString());

							planningTable.Add(agentDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString(), journalList.Count);
							planningList.Add(agentDataSet.Tables[0].Rows[k].ItemArray.GetValue(0).ToString());

							k++;
						}

						k=0;
						string foundAgentCode = "";
						int routeQuantity = 0;
						while(k < planningList.Count)
						{
							if (foundAgentCode == "") foundAgentCode = planningList[k].ToString();

							if (int.Parse(planningTable[planningList[k].ToString()].ToString()) > routeQuantity)
							{
								routeQuantity = int.Parse(planningTable[foundAgentCode].ToString());
								foundAgentCode = planningList[k].ToString();
							}

							k++;

						}


						agentList.Add(foundAgentCode);
						agentHashTable.Add(foundAgentCode, 1);
					}


				}

				int j = 0;
				string choosenAgentCode = "";
				int orderQuantity = 0;

				while (j < agentList.Count)
				{
					if (choosenAgentCode == "") choosenAgentCode = agentList[j].ToString();

					if (int.Parse(agentHashTable[agentList[j].ToString()].ToString()) > orderQuantity)
					{
						orderQuantity = int.Parse(agentHashTable[choosenAgentCode].ToString());
						choosenAgentCode = agentList[j].ToString();
					}

					j++;
				}

				Agent agent = agents.getAgent(database, choosenAgentCode);
				if (agent != null)
				{
					if (agent.containsMoreStorageThan(database, this.agentStorageGroup))
					{
						if (this.agentCode != choosenAgentCode)
						{
							this.removeJournal(database);
						}
						this.agentCode = choosenAgentCode;
						this.save(database);
					}
				}
			}
		}	


		public bool isReadyToSend(Database database)
		{
			Agents agents = new Agents();
			Agent agent = agents.getAgent(database, this.agentCode);
			if (agent != null)
			{
				if (forcedAssignment) return true;

				//Console.WriteLine("Debug: Containers plats på flaket: "+this.getNoOfContainers(database)+", på rutten: "+this.countUnitContainers(database, "ST"));
				//Console.WriteLine("Debug: Volym plats på flaket: "+this.getVolumeStorage(database)+", på rutten: "+this.countUnitContainers(database, "KUB"));
				if (this.getNoOfContainers(database) <= this.countContainers(database, 0))
				{
					if (this.agentCode != "")
					{
						return true;
					}
				}
			}

			return false;

		}


		public void setJournalUnloaded(Database database, int creatorType, string creatorNo)
		{
			if (this.allOrdersLoaded(database))
			{
				if (this.status < 8)
				{
					LineOrders lineOrders = new LineOrders();
					DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, this.entryNo);

					int j = 0;
					while (j < lineOrderDataSet.Tables[0].Rows.Count)
					{
						LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[j]);

						DataSet lineOrderContainerDataSet = lineOrder.getContainers(database);

						int i = 0;
						while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
						{
							LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);

							ContainerEntry containerEntry = new ContainerEntry();
							containerEntry.containerNo = lineOrderContainer.containerNo;
							containerEntry.type = 1;
							containerEntry.sourceType = 0;
							containerEntry.sourceCode = this.agentCode;
							containerEntry.creatorType = creatorType;
							containerEntry.creatorNo = creatorNo;
							containerEntry.documentType = 2;
							containerEntry.documentNo = this.entryNo.ToString();
							containerEntry.entryDateTime = DateTime.Now;
							containerEntry.locationType = 2;
							containerEntry.locationCode = this.arrivalFactoryCode;
							containerEntry.receivedDateTime = DateTime.Now;
							containerEntry.save(database);

							i++;
						}

						lineOrder.status = 7;
						lineOrder.save(database, false);

						j++;

					}

					this.status = 7;
					this.save(database);
				}
			}
		}

		public void loadAllOrders(Database database, int creatorType, string creatorNo)
		{
			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, this.entryNo);

			int i = 0;
			while (i < lineOrderDataSet.Tables[0].Rows.Count)
			{
				LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
				lineOrder.setOrderLoaded(database, creatorType, creatorNo);

				i++;
			}
		}

		public void setJournalReported(Database database)
		{
			if (this.status == 7)
			{
				this.status = 8;
				this.save(database);
			}
		}

		public void openJournal(Database database, bool sendToAgent)
		{
			if (this.status == 8)
			{
				if (sendToAgent)
				{
					this.status = 1;
				}
				else
				{
					this.status = 7;
				}

				database.nonQuery("UPDATE [Line Journal] SET [Status] = '"+this.status+"', [Sent To Factory] = 0 WHERE [Entry No] = '"+this.entryNo+"'");


			}
		}

		private void enqueueInventoryUpdate(Database database)
		{
			FactoryInventoryQueueEntries factoryInventoryQueueEntries = new FactoryInventoryQueueEntries();
			factoryInventoryQueueEntries.enqueue(database, this.entryNo);
		}

		public void updateEstimatedInventory(Database database)
		{
			this.deleteFactoryInventoryEntries(database);

			LineJournals lineJournals = new LineJournals();
			DataSet lineJournalDataSet = lineJournals.getDataSet(database, this.arrivalFactoryCode, this.arrivalDateTime);

			//Delete all inventory transactions for journals arriving after current journal
			int a = 0;
			while (a < lineJournalDataSet.Tables[0].Rows.Count)
			{
				LineJournal lineJournal = new LineJournal(lineJournalDataSet.Tables[0].Rows[a]);
				lineJournal.deleteFactoryInventoryEntries(database);

				a++;
			}


			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, this.entryNo);

			int i = 0;
			while (i < lineOrderDataSet.Tables[0].Rows.Count)
			{
				LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
				
				DataSet lineOrderContainers = lineOrder.getContainers(database);
				
				int j = 0;
				while (j < lineOrderContainers.Tables[0].Rows.Count)
				{
					LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrderContainers.Tables[0].Rows[j]);

					lineOrderContainer.updateInventory(database, this);

					j++;
				}

				i++;
			}

			
			//Re-update inventory for journals arriving after current journal
			a = 0;
			while (a < lineJournalDataSet.Tables[0].Rows.Count)
			{
				LineJournal lineJournal = new LineJournal(lineJournalDataSet.Tables[0].Rows[a]);
				lineJournal.updateEstimatedInventory(database);

				a++;
			}

		}

		private void deleteFactoryInventoryEntries(Database database)
		{

			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, this.entryNo);

			int i = 0;
			while (i < lineOrderDataSet.Tables[0].Rows.Count)
			{
				LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
				
				FactoryInventoryEntries factoryInventoryEntries = new FactoryInventoryEntries();
				factoryInventoryEntries.deleteLineOrderEntries(database, lineOrder.entryNo);

				i++;
			}



		}

		public int getNoOfContainers(Database database)
		{
			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
			AgentStorageGroup agentStorageGroup = agentStorageGroups.getEntry(database, this.agentStorageGroup);
			if (agentStorageGroup != null)
			{
				return agentStorageGroup.noOfContainers;
			}

			return 0;

		}

		public int getVolumeStorage(Database database)
		{
			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
			AgentStorageGroup agentStorageGroup = agentStorageGroups.getEntry(database, this.agentStorageGroup);
			if (agentStorageGroup != null)
			{
				return agentStorageGroup.volumeStorage;
			}

			return 0;

		}

		public string getAgentStorageGroupDescription(Database database)
		{
			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
			AgentStorageGroup agentStorageGroup = agentStorageGroups.getEntry(database, this.agentStorageGroup);
			if (agentStorageGroup != null)
			{
				return agentStorageGroup.description;
			}

			return "";
		}

		public string getScaleStatus()
		{
			if (this.status >= 7)
			{
				if (sentToFactory) return "Skickad";
				return "Köad";
			}
			return "";
		}

		public string getInvoiceStatus()
		{
			if (this.invoiceReceived) return "Ja";
			return "";
		}

		public void setInvoiceStatus(Database database, bool invoiceReceived)
		{
			this.invoiceReceived = invoiceReceived;
			this.save(database);
			
		}

		public string getFactoryConfirmedWaitTime()
		{
			if (this.factoryConfirmedWaitTime != "") return "Ja, "+this.factoryConfirmedWaitTime;
			return "";
		}

		public DataSet getNonContainerOrders(Database database)
		{
			LineOrders lineOrders = new LineOrders();
			return lineOrders.getNonContainerDataSet(database, this.entryNo);
		}

		public void clearScaleStatus(Database database)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet lineOrderContainerDataSet = lineOrderContainers.getJournalDataSet(database, this.entryNo);
			int i = 0;
			while(i < lineOrderContainerDataSet.Tables[0].Rows.Count)
			{
				LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);
				lineOrderContainer.isSentToScaling = false;
				lineOrderContainer.save(database);

				i++;
			}
			
			this.sentToFactory = false;
			save(database);
			
		}
	}
}
