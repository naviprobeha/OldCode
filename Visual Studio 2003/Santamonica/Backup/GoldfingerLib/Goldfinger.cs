using System;
using System.Data;
using System.Threading;
using Navipro.SantaMonica.Common;


namespace Navipro.SantaMonica.Goldfinger
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Goldfinger : Logger
	{
		private Configuration configuration;
		private Database database;

		public Goldfinger()
		{
		}

		public bool init()
		{
			configuration = new Configuration();
			if (!configuration.initWeb())
			{
				return false;
			}

			database = new Database(this, configuration);
			 
			return true;
		}

		public int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status)
		{

			Agents agents = new Agents();

			Agent agent = agents.getAgent(database, agentCode);
			if (agent != null)
			{
				AgentTransaction agentTrans = new AgentTransaction(agentCode, rt90x, rt90y, (decimal)heading, (decimal)speed, (decimal)height, status, "", 0);
				agentTrans.save(database);

				SynchronizationQueueEntries synchEntries = new SynchronizationQueueEntries(agentCode);
				return synchEntries.getCount(database);

				
			}

			return 0;
		}

		public int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName)
		{

			Agents agents = new Agents();

			Agent agent = agents.getAgent(database, agentCode);
			if (agent != null)
			{
				AgentTransaction agentTrans = new AgentTransaction(agentCode, rt90x, rt90y, (decimal)heading, (decimal)speed, (decimal)height, status, userName, 0);
				agentTrans.save(database);

				SynchronizationQueueEntries synchEntries = new SynchronizationQueueEntries(agentCode);
				return synchEntries.getCount(database);

				
			}

			return 0;
		}

		public int reportStatus(string agentCode, int rt90x, int rt90y, float heading, float speed, float height, int status, string userName, int tripMeter)
		{

			Agents agents = new Agents();

			Agent agent = agents.getAgent(database, agentCode);
			if (agent != null)
			{
				AgentTransaction agentTrans = new AgentTransaction(agentCode, rt90x, rt90y, (decimal)heading, (decimal)speed, (decimal)height, status, userName, tripMeter);
				agentTrans.save(database);

				SynchronizationQueueEntries synchEntries = new SynchronizationQueueEntries(agentCode);
				return synchEntries.getCount(database);

				
			}

			return 0;
		}

		public void createContainerEntry(string agentCode, DataSet containerEntryDataSet)
		{
			ServerLogging serverLogging = new ServerLogging(database);
			try
			{
				serverLogging.log(agentCode, "Create Container Entry");

				Containers containers = new Containers();
				Container container = containers.getEntry(database, containerEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());


				ContainerEntry containerEntry = new ContainerEntry(database, agentCode, containerEntryDataSet);
				if (containerEntry.type == 2) // Arrival Report
				{
					serverLogging.log(agentCode, "[ArrivalReport] Finding LineOrder for container "+containerEntry.containerNo);				

					LineOrders lineOrders = new LineOrders();
					LineOrder lineOrder = lineOrders.getContainerLineOrder(database, containerEntry.containerNo, agentCode);
					if (lineOrder != null)
					{
						serverLogging.log(agentCode, "[ArrivalReport] Setting confirmed datetime to "+containerEntry.estimatedArrivalDateTime.ToString("yyyy-MM-dd HH:mm")+", LineOrder: "+lineOrder.entryNo);				
						
						lineOrder.confirmOrder(database, containerEntry.estimatedArrivalDateTime, containerEntry.locationCode);
					}
					
				}
			}
			catch(Exception e)
			{
				serverLogging.log(agentCode, "[CreateContainerEntry] "+e.Message);				
			}
		}

		public DataSet getSynchRecord(string agentCode, ref int type, ref int action, ref string primaryKey, ref int synchEntryNo)
		{
			Agents agents = new Agents();

			Agent agent = agents.getAgent(database, agentCode);
			
			if (agent != null)
			{
				DataSet entryDataSet = null;
				SynchronizationQueueEntry synchEntry = null;
				bool dataSetReturn = false;


				if (!dataSetReturn)
				{
					entryDataSet = getSynchRecords(agent, ref synchEntry, SynchronizationQueueEntries.SYNC_SHIP_ORDER);
					if (entryDataSet != null) dataSetReturn = true;
				}

				if (!dataSetReturn)
				{
					entryDataSet = getSynchRecords(agent, ref synchEntry, SynchronizationQueueEntries.SYNC_SHIP_ORDER_LINE);
					if (entryDataSet != null) dataSetReturn = true;
				}

				if (!dataSetReturn)
				{
					entryDataSet = getSynchRecords(agent, ref synchEntry, SynchronizationQueueEntries.SYNC_SHIP_ORDER_LINE_ID);
					if (entryDataSet != null) dataSetReturn = true;
				}

				if (!dataSetReturn)
				{
					entryDataSet = getSynchRecords(agent, ref synchEntry, SynchronizationQueueEntries.SYNC_LINE_JOURNAL);
					if (entryDataSet != null) dataSetReturn = true;
				}

				if (!dataSetReturn)
				{
					entryDataSet = getSynchRecords(agent, ref synchEntry, SynchronizationQueueEntries.SYNC_LINE_ORDER);
					if (entryDataSet != null) dataSetReturn = true;
				}

				if (!dataSetReturn)
				{
					entryDataSet = getSynchRecords(agent, ref synchEntry, SynchronizationQueueEntries.SYNC_LINE_ORDER_CONTAINER);
					if (entryDataSet != null) dataSetReturn = true;
				}

				if (!dataSetReturn)
				{
					entryDataSet = getSynchRecords(agent, ref synchEntry, SynchronizationQueueEntries.SYNC_ALL);
					if (entryDataSet != null) dataSetReturn = true;
				}

				if (dataSetReturn)
				{
					type = synchEntry.type;
					action = synchEntry.action;
					primaryKey = synchEntry.primaryKey;
					synchEntryNo = synchEntry.entryNo;

					return entryDataSet;
				}
			}

			return null;
		}

		private DataSet getSynchRecords(Agent agent, ref SynchronizationQueueEntry synchEntry, int recordType)
		{

			SynchronizationQueueEntries synchEntries = new SynchronizationQueueEntries(agent.code);
			
			if (recordType == -1)
			{
				synchEntry = synchEntries.getFirstEntry(database);
			}
			else
			{
				synchEntry = synchEntries.getFirstEntry(database, recordType);
			}


			/*if (synchEntry.type == 20)
			{
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add();
				return dataSet;
			}*/

			while(synchEntry != null)
			{

				DataSet entryDataSet = composeDataSet(agent, synchEntry);
					
				if (synchEntry.action == 2) return entryDataSet;

				if (entryDataSet.Tables.Count > 0)
				{
					if (entryDataSet.Tables[0].Rows.Count > 0) 
					{
						return entryDataSet;
					}
					else
					{
						synchEntry.delete(database);
					}
				}
				else
				{
					synchEntry.delete(database);
				}


				if (recordType == -1)
				{
					synchEntry = synchEntries.getFirstEntry(database);
				}
				else
				{
					synchEntry = synchEntries.getFirstEntry(database, recordType);
				}

			}
			return null;

		}


		private DataSet composeDataSet(Agent agent, SynchronizationQueueEntry synchEntry)
		{
			DataSet entryDataSet = new DataSet();

			if (synchEntry.action == 2) return entryDataSet;

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_SHIP_ORDER) // ShipOrder
			{
				ShipOrders shipOrders = new ShipOrders(agent.code);
				shipOrders.setStatus(database, synchEntry.primaryKey, 4);
				entryDataSet = shipOrders.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_CUSTOMER) // Customer
			{
				Customers customers = new Customers();
				entryDataSet = customers.getDataSetEntry(database, agent.getOrganization(database), synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_ITEM) // Item
			{
				Items items = new Items();
				entryDataSet = items.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_ITEM_PRICE) // Item Price
			{
				ItemPrices itemPrices = new ItemPrices();
				entryDataSet = itemPrices.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_MAP) // Map
			{
				Maps maps = new Maps();
				entryDataSet = maps.getDataSetEntry(database, synchEntry.primaryKey);
			}
		
			if (synchEntry.type == SynchronizationQueueEntries.SYNC_MESSAGE) // Message
			{
				Messages messages = new Messages();
				entryDataSet = messages.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_ORGANIZATION) // Organization
			{
				Organizations organizations = new Organizations();
				entryDataSet = organizations.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_ITEM_PRICE_EXTENDED) // Item Price Extended
			{
				ItemPricesExtended itemPricesExtended = new ItemPricesExtended();
				entryDataSet = itemPricesExtended.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_MOBILE_USER) // Mobile User
			{
				MobileUsers mobileUsers = new MobileUsers();
				entryDataSet = mobileUsers.getDataSetEntry(database, int.Parse(synchEntry.primaryKey));
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_SHIP_ORDER_LINE) // Ship Order Line
			{
				ShipOrderLines shipOrderLines = new ShipOrderLines();
				entryDataSet = shipOrderLines.getDataSetEntry(database, int.Parse(synchEntry.primaryKey));
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_SHIP_ORDER_LINE_ID) // Ship Order Line ID
			{
				ShipOrderLineIds shipOrderLineIds = new ShipOrderLineIds();
				entryDataSet = shipOrderLineIds.getDataSetEntry(database, int.Parse(synchEntry.primaryKey));
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_AGENT) // Agents
			{
				Agents agents = new Agents();
				entryDataSet = agents.getDataSetEntry(database, agent.getOrganization(database), synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_CONTAINER) // Containers
			{
				Containers containers = new Containers();
				entryDataSet = containers.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_LINE_JOURNAL) // Line Journal
			{
				LineJournals lineJournals = new LineJournals();
				LineJournal lineJournal = lineJournals.getEntry(database, synchEntry.primaryKey);
				if (lineJournal != null)
				{
				
					if (agent.code == lineJournal.agentCode)
					{
						entryDataSet = lineJournals.getDataSetEntry(database, synchEntry.primaryKey);
					}
					else
					{
						Agents cloneAgents = new Agents();
						Agent cloneAgent = cloneAgents.getAgent(database, lineJournal.agentCode);
						if (cloneAgent.cloneToAgentCode == agent.code)
						{
							entryDataSet = lineJournals.getDataSetEntry(database, synchEntry.primaryKey);
						}
					}
				}

			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_LINE_ORDER) // Line Order
			{
				LineOrders lineOrders = new LineOrders();
				lineOrders.setStatus(database, synchEntry.primaryKey, 5);
				entryDataSet = lineOrders.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_LINE_ORDER_CONTAINER) // Line Order Container
			{
				LineOrderContainers lineOrderContainers = new LineOrderContainers();
				entryDataSet = lineOrderContainers.getDataSetEntry(database, int.Parse(synchEntry.primaryKey));
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_ORGANIZATION_LOCATION) // Organization Locations
			{
				OrganizationLocations organizationLocations = new OrganizationLocations();
				entryDataSet = organizationLocations.getDataSetEntry(database, agent.organizationNo, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_CATEGORY) // Organization Locations
			{
				Categories categories = new Categories();
				entryDataSet = categories.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_CUSTOMER_SHIP_ADDRESS) // Customer Ship Address
			{
				CustomerShipAddresses customerShipAddresses = new CustomerShipAddresses();
				entryDataSet = customerShipAddresses.getDataSetEntry(database, int.Parse(synchEntry.primaryKey));
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_FACTORY_ORDER) // Factory Order
			{
				FactoryOrders factoryOrders = new FactoryOrders();
				factoryOrders.setStatus(database, synchEntry.primaryKey, 2);
				entryDataSet = factoryOrders.getDataSetEntry(database, synchEntry.primaryKey);
			}

			if (synchEntry.type == SynchronizationQueueEntries.SYNC_CONSUMER_STATUS) // Consumer Status
			{
				ConsumerStatuses consumerStatuses = new ConsumerStatuses();
				entryDataSet = consumerStatuses.getDataSetEntry(database, synchEntry.primaryKey);
			}

			return entryDataSet;
		}

		public void ackSynchRecord(int synchEntryNo)
		{
			SynchronizationQueueEntries synchEntries = new SynchronizationQueueEntries();
			SynchronizationQueueEntry synchEntry = synchEntries.getEntry(database, synchEntryNo);
			if (synchEntry != null) 
			{	
				if (synchEntry.type == SynchronizationQueueEntries.SYNC_LINE_JOURNAL) // Line Journal
				{
					if (synchEntry.action < 2)
					{
						LineJournals lineJournals = new LineJournals();
						LineJournal lineJournal = lineJournals.getEntry(database, synchEntry.primaryKey);
						if (lineJournal != null)
						{
							lineJournals.setStatus(database, synchEntry.primaryKey, 5);
				
						}
					}
				}
			
				synchEntry.delete(database);
			}

			
		}

		public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY)
		{
			setShipOrderStatus(agentCode, no, status, positionX, positionY, DateTime.Now);
		}

		public void setShipOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime)
		{
			try
			{
				ServerLogging serverLogging = new ServerLogging(database);
				serverLogging.log(agentCode, "SetShipOrderStatus: [No] "+no+", [Status] "+status);

				ShipOrders shipOrders = new ShipOrders(agentCode);
				ShipOrder shipOrder = shipOrders.getAgentEntry(database, agentCode, no);
				if (shipOrder != null)
				{
					shipOrder.status = status;

					if (status == 6)
					{

						Customers customers = new Customers();
						Customer customer = customers.getEntry(database, shipOrder.organizationNo, shipOrder.customerNo);

						shipOrder.positionX = positionX;
						shipOrder.positionY = positionY;

						shipOrder.closedDate = shipTime;
						shipOrder.shipTime = shipTime;


					}
            
					shipOrder.save(database, false);
					shipOrder.log(database, agentCode, "Ändrat status till "+shipOrder.getStatusText());
				}
			}
			catch(Exception e)
			{
				ServerLogging serverLogging = new ServerLogging(database);
				serverLogging.log(agentCode, "SetShipOrderStatus: [No] "+no+", [Message] "+e.Message);
			}
		}

		public void setLineOrderStatus(string agentCode, string no, int status, int positionX, int positionY, DateTime shipTime, int loadWaitTime)
		{
			ServerLogging serverLogging = new ServerLogging(database);
			try
			{
				serverLogging.log(agentCode, "SetLineOrderStatus");

				LineOrders lineOrders = new LineOrders();
				LineOrder lineOrder = lineOrders.getEntry(database, no);
				if (lineOrder != null)
				{
					LineJournal lineJournal = lineOrder.getJournal(database);
					if ((lineJournal != null) && (lineJournal.agentCode == agentCode))
					{
						lineOrder.status = status;

						if (status == 7)
						{
							Agents agents = new Agents();
							Agent agent = agents.getAgent(database, agentCode);

							if (lineJournal.status == 5) lineJournal.status = 6; 

							if ((lineOrder.positionX == 0) && (lineOrder.positionY == 0))
							{
								lineOrder.positionX = positionX;
								lineOrder.positionY = positionY;
							}

							lineOrder.isLoaded = true;
							lineOrder.driverName = agent.userName;
							
							lineOrder.loadWaitTime = loadWaitTime;

							lineOrder.closedDateTime = new DateTime(shipTime.Year, shipTime.Month, shipTime.Day, shipTime.Hour, shipTime.Minute, shipTime.Second);
							lineOrder.save(database, false);

							//Uppdatera klockslag för framkomst.
							//lineOrder.shipTime = new DateTime(1754, 01, 01, shipTime.Hour, shipTime.Minute, shipTime.Second);


							ShippingCustomers shippingCustomers = new ShippingCustomers();
							ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, lineOrder.shippingCustomerNo);

							if (shippingCustomer != null)
							{
								if ((shippingCustomer.positionX == 0) && (shippingCustomer.positionY == 0))
								{
									shippingCustomer.positionX = positionX;
									shippingCustomer.positionY = positionY;
									shippingCustomer.save(database);
								}
							}

							lineJournal.save(database);
							serverLogging.log(agentCode, "SetLineOrderStatus: Status: "+lineOrder.status+", RecalcArrivalTime Begin");
							lineJournal.recalculateArrivalTime(database);
							serverLogging.log(agentCode, "SetLineOrderStatus: RecalcArrivalTime Done");
						}
						else
						{
							if (lineOrder.isLoaded) lineOrder.status = 7;

							lineOrder.save(database, false);
						}
            
						//lineOrder.log(database, agentCode, "Ändrat status till "+shipOrder.getStatusText());


					}
				}
			}
			catch(Exception e)
			{
				serverLogging.log(agentCode, "SetLineOrderStatus: [No] "+no+", [Message] "+e.Message);
			}
		}

		public void reportLineJournal(string agentCode, DataSet lineJournalDataSet)
		{
			try
			{
				if (lineJournalDataSet.Tables.Count > 0)
				{
					if (lineJournalDataSet.Tables[0].Rows.Count > 0)
					{
						LineJournals lineJournals = new LineJournals();
						lineJournals.reportJournal(database, agentCode, lineJournalDataSet);
					}
				}
			}
			catch(Exception e)
			{
				ServerLogging serverLogging = new ServerLogging(database);
				serverLogging.log(agentCode, "ReportLineJournal: "+e.Message);
			}
		}

		public void setFactoryOrderStatus(string agentCode, DataSet factoryOrderDataSet)
		{
			ServerLogging serverLogging = new ServerLogging(database);
			try
			{

				serverLogging.log(agentCode, "SetFactoryOrderStatus!");
				serverLogging.log(agentCode, "FactoryOrderEntryNo: "+factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

				FactoryOrders factoryOrders = new FactoryOrders();
				FactoryOrder factoryOrder = factoryOrders.getEntry(database, factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
				if (factoryOrder != null)
				{

					if (factoryOrder.status == int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(29).ToString()))
					{
						//Status redan satt.
						return;
					}

					Agents agents = new Agents();
					Agent agent = agents.getAgent(database, agentCode);

					factoryOrder.status = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(29).ToString());

					if (factoryOrder.status == 3)
					{
						serverLogging.log(agentCode, "Pickup");

						factoryOrder.driverName = agent.userName;

						DateTime shipDate = DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
						DateTime shipTime = DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(31).ToString());
						factoryOrder.pickupDateTime = new DateTime(shipDate.Year, shipDate.Month, shipDate.Day, shipTime.Hour, shipTime.Minute, shipTime.Second);

						factoryOrder.quantity = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());

						factoryOrder.loadDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(36).ToString());
						factoryOrder.loadWaitDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(37).ToString());
						factoryOrder.phValueShipping = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(40).ToString().Replace(".", ","));

						if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 41)
						{
							factoryOrder.loadReasonValue = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(41).ToString());
							factoryOrder.loadReasonText = factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(42).ToString();
						}

						if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 45)
						{
							factoryOrder.extraDist = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(45).ToString());
							factoryOrder.extraTime = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(46).ToString());
						}

						factoryOrder.save(database, false);

						if (factoryOrder.factoryType == 0)
						{
							// Dra ner lagernivån på fabriken.

						}
					}

					if (factoryOrder.status == 4)
					{
						serverLogging.log(agentCode, "Drop");

						factoryOrder.dropDriverName = agent.userName;

						if ((factoryOrder.consumerPositionX == 0) && (factoryOrder.consumerPositionY == 0))
						{
							factoryOrder.consumerPositionX = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(26).ToString());
							factoryOrder.consumerPositionY = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(27).ToString());
						}

						factoryOrder.shipDate = DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
						factoryOrder.shipTime = DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(31).ToString());
						factoryOrder.quantity = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());
						
						//factoryOrder.arrivalDateTime = DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(33).ToString());
						factoryOrder.closedDateTime = factoryOrder.arrivalDateTime;
						factoryOrder.realQuantity = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(23).ToString());
						factoryOrder.consumerLevel = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(35).ToString());

						factoryOrder.loadDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(36).ToString());
						factoryOrder.loadWaitDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(37).ToString());
						factoryOrder.dropDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(38).ToString());
						factoryOrder.dropWaitDuration = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(39).ToString());

						factoryOrder.phValueShipping = float.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(40).ToString().Replace(".", ","));

						if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 43)
						{
							factoryOrder.dropReasonValue = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(43).ToString());
							factoryOrder.dropReasonText = factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(44).ToString();
						}

						if (factoryOrderDataSet.Tables[0].Rows[0].ItemArray.Length > 45)
						{
							factoryOrder.extraDist = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(45).ToString());
							factoryOrder.extraTime = int.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(46).ToString());
						}

						factoryOrder.navisionStatus = 1;

						factoryOrder.save(database, false, false);

						serverLogging.log(agentCode, "Setting arrivaltime");

						factoryOrder.setArrivalTime(database, DateTime.Parse(factoryOrderDataSet.Tables[0].Rows[0].ItemArray.GetValue(33).ToString()));

						serverLogging.log(agentCode, "Done setting arrivaltime");

						Consumers consumers = new Consumers();
						Consumer consumer = consumers.getEntry(database, factoryOrder.consumerNo);

						if (consumer != null)
						{
							if ((consumer.positionX == 0) && (consumer.positionY == 0))
							{
								consumer.positionX = factoryOrder.consumerPositionX;
								consumer.positionY = factoryOrder.consumerPositionY;
								consumer.save(database);
							}
						}

						serverLogging.log(agentCode, "SetFactoryOrderStatus: No: "+factoryOrder.entryNo+", Status: "+factoryOrder.status);

						ConsumerInventories consumerInventories = new ConsumerInventories();
						consumerInventories.setActualInventory(database, factoryOrder);

						serverLogging.log(agentCode, "SetFactoryOrderStatus: Recalculating inventory");

						factoryOrder.save(database, false);


					}
            
					//lineOrder.log(database, agentCode, "Ändrat status till "+shipOrder.getStatusText());


				}
				
			}
			catch(Exception e)
			{
				serverLogging.log(agentCode, "SetFactoryOrderStatus: [Agent] "+agentCode+", [Message] "+e.Message);
				//throw new Exception("SetFactoryOrderStatus failed: "+e.Message);
			}
		}

		public void createShipment(string agentCode, DataSet shipmentHeaderDataSet, DataSet shipmentLinesDataSet, DataSet shipmentLineIdsDataSet)
		{
			if (shipmentHeaderDataSet.Tables[0].Rows.Count == 0)
			{
				ServerLogging serverLogging = new ServerLogging(database);
				serverLogging.log(agentCode, "CreateShipment: Empty dataset... Deleting...");
				return;
				//throw new Exception("CreateShipment: Empty dataset...");
			}


			try
			{			

				ShipmentHeader shipmentHeader = new ShipmentHeader(database, agentCode, shipmentHeaderDataSet);
				ShipmentLines shipmentLines = new ShipmentLines(database, agentCode, shipmentLinesDataSet, shipmentLineIdsDataSet);

				shipmentHeader.setStatus(database, 1);

				//Enqueue shipment for system handling
				ShipmentContainerQueueEntries shipmentQueueEntries = new ShipmentContainerQueueEntries();
				shipmentQueueEntries.enqueue(database, shipmentHeader.no, shipmentHeader.containerNo);

				if (shipmentHeader.customerShipAddressNo == "NEW")
				{
					CustomerShipAddress customerShipAddress = new CustomerShipAddress();
					customerShipAddress.name = shipmentHeader.shipName;
					customerShipAddress.address = shipmentHeader.shipAddress;
					customerShipAddress.address2 = shipmentHeader.shipAddress2;
					customerShipAddress.postCode = shipmentHeader.shipPostCode;
					customerShipAddress.city = shipmentHeader.shipCity;
					customerShipAddress.productionSite = shipmentHeader.productionSite;
					customerShipAddress.organizationNo = shipmentHeader.organizationNo;
					customerShipAddress.customerNo = shipmentHeader.customerNo;
					customerShipAddress.positionX = shipmentHeader.positionX;
					customerShipAddress.positionY = shipmentHeader.positionY;
					customerShipAddress.save(database);

					shipmentHeader.customerShipAddressNo = customerShipAddress.entryNo;
					shipmentHeader.save(database);
				}

				

				//Position update
				if ((shipmentHeader.positionX > 0) && (shipmentHeader.positionY > 0))
				{
					if (shipmentHeader.customerShipAddressNo != "")
					{
						CustomerShipAddresses customerShipAddresses = new CustomerShipAddresses();
						CustomerShipAddress customerShipAddress = customerShipAddresses.getEntry(database, shipmentHeader.organizationNo, shipmentHeader.customerNo, shipmentHeader.customerShipAddressNo);
						if (customerShipAddress != null)
						{
							if (customerShipAddress.positionX == 0)
							{
								customerShipAddress.positionX = shipmentHeader.positionX;
								customerShipAddress.positionY = shipmentHeader.positionY;
								customerShipAddress.save(database);
							}
						}
					}
					else
					{
						Customers customers = new Customers();
						Customer customer = customers.getEntry(database, shipmentHeader.organizationNo, shipmentHeader.customerNo);

						if (customer != null)
						{
							if (customer.positionX == 0)
							{
								customer.positionX = shipmentHeader.positionX;
								customer.positionY = shipmentHeader.positionY;
								customer.save(database);
							}
						}

					}
				}
				

				//Ship Order Update
				if (shipmentHeader.shipOrderEntryNo > 0)
				{
					ShipOrders shipOrders = new ShipOrders();
					ShipOrder shipOrder = shipOrders.getEntry(database, shipmentHeader.organizationNo, shipmentHeader.shipOrderEntryNo.ToString());
					if (shipOrder != null)
					{
						if (shipOrder.status < 6)
						{
							shipOrder.status = 6;
							shipOrder.save(database, false);
						}
					}
				}

			}
			catch(Exception e)
			{
				//ServerLogging serverLogging = new ServerLogging(database);
				//serverLogging.log(agentCode, "CreateShipment: "+e.Message);
				throw new Exception(e.Message);
			}
		}
			
		public void createOrder(string agentCode, DataSet shipmentHeaderDataSet, DataSet shipmentLinesDataSet, DataSet shipmentLineIdsDataSet)
		{
			try
			{
				ShipOrder shipOrder = new ShipOrder(database, agentCode, shipmentHeaderDataSet);

				string assignToAgentCode = shipOrder.agentCode;
				
				ShipOrderLines shipOrderLines = new ShipOrderLines(database, shipOrder.entryNo, shipmentLinesDataSet, shipmentLineIdsDataSet);

				shipOrder.log(database, agentCode, "Körorder skapad.");

						
				
				shipOrder.updateDetails(database);
				shipOrder.agentCode = "";
				if (agentCode != "") shipOrder.assignOrder(database, assignToAgentCode, agentCode);

			}
			catch(Exception e)
			{
				ServerLogging serverLogging = new ServerLogging(database);
				serverLogging.log(agentCode, "CreateOrder: "+e.Message);
				throw new Exception(e.Message);
			}			
		}

		public void setMessageStatus(string agentCode, int messageEntryNo, int status)
		{
			Agents agents = new Agents();
			Agent agent = agents.getAgent(database, agentCode);
			if (agent != null)
			{
				MessageAgents messageAgents = new MessageAgents();
				MessageAgent messageAgent = messageAgents.getEntry(database, messageEntryNo, agentCode);
				if (messageAgent != null)
				{
					messageAgent.status = status;
					if (status == 2) messageAgent.ackDateTime = DateTime.Now;
					messageAgent.save(database);
				}
			}
		}

		public void assignShipOrder(string agentCode, string no, string newAgentCode)
		{
			try
			{
				ShipOrders shipOrders = new ShipOrders(agentCode);
				ShipOrder shipOrder = shipOrders.getEntry(database, no);
				if (shipOrder != null)
				{
					shipOrder.assignOrder(database, newAgentCode, agentCode);
				}
			}
			catch(Exception e)
			{
				ServerLogging serverLogging = new ServerLogging(database);
				serverLogging.log(agentCode, "AssignShipOrder: [No] "+no+", [Agent Code] "+newAgentCode+", [Message] "+e.Message);
			}


		}

		public void dispose()
		{
			database.close();
		}

		public void reportError(string agentCode, string message)
		{
			ServerLogging serverLogging = new ServerLogging(database);
			serverLogging.log(agentCode, "Agent Error: "+message);
			
		}

		public string getPriceUpdateItemNo(string agentCode)
		{
			AgentItemPriceUpdates agentItemPriceUpdates = new AgentItemPriceUpdates();
			DataSet priceUpdateDataSet = agentItemPriceUpdates.getDataSet(database, agentCode);
			if (priceUpdateDataSet.Tables[0].Rows.Count > 0)
			{
				AgentItemPriceUpdate agentItemPriceUpdate = new AgentItemPriceUpdate(priceUpdateDataSet.Tables[0].Rows[0]);
				agentItemPriceUpdate.enqueuePrices(database);
				return agentItemPriceUpdate.itemNo;
			}

			return "";

		}

		public void acknowledgePriceUpdate(string agentCode, string itemNo, float checksum)
		{
			AgentItemPriceUpdates agentItemPriceUpdates = new AgentItemPriceUpdates();
			agentItemPriceUpdates.acknowledge(database, agentCode, itemNo, checksum);
		}

		#region Logger Members

		public void write(string message, int type)
		{
			// TODO:  Add Goldfinger.write implementation
		}

		#endregion
	}
}
