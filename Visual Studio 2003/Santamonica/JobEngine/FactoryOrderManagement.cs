using System;
using System.Data;
using System.Threading;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine

{
	/// <summary>
	/// Summary description for LineOrderManagement.
	/// </summary>
	public class FactoryOrderManagement
	{
		private Database database;
		private Logger logger;

		private Thread thread;
		private bool running;

		public FactoryOrderManagement(Logger logger, Configuration configuration)
		{
			//
			// TODO: Add constructor logic here
			//
			this.logger = logger;
			this.database = new Database(logger, configuration);
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
				//this.checkExistingFactoryOrders();
				//this.produceScheduledFactoryOrders();
				//this.assignToAgents();				

				Thread.Sleep(10000);
			}
			log("Handler stopped.", 0);
		}

		public void stop()
		{
			this.running = false;
		}


		private void checkExistingFactoryOrders()
		{
			log("Checking existing factory orders...", 0);

			FactoryOrders factoryOrders = new FactoryOrders();
			DataSet factoryOrdersDataSet = factoryOrders.getPlannedDataSet(database);

			int i = 0;
			while (i < factoryOrdersDataSet.Tables[0].Rows.Count)
			{	
				FactoryOrder factoryOrder = new FactoryOrder(factoryOrdersDataSet.Tables[0].Rows[i]);
				DateTime shipDateTime = new DateTime(factoryOrder.shipDate.Year, factoryOrder.shipDate.Month, factoryOrder.shipDate.Day, factoryOrder.shipTime.Hour, factoryOrder.shipTime.Minute, factoryOrder.shipTime.Second);

				log("Checking factory order: "+factoryOrder.entryNo, 0);

				FactoryInventoryEntries factoryInventoryEntries = new FactoryInventoryEntries();
				float inventoryValue = factoryInventoryEntries.calcInventoryTotal(database, factoryOrder.factoryNo, shipDateTime);

				if (inventoryValue < factoryOrder.quantity)
				{
					// Move forward
					log("Moving forward", 0);
					DataSet preDataSet = factoryInventoryEntries.getPostOrderDataSet(database, factoryOrder.factoryNo, shipDateTime);
					int j = 0;
					while (j < preDataSet.Tables[0].Rows.Count)
					{
						FactoryInventoryEntry factoryInventoryEntry = new FactoryInventoryEntry(preDataSet.Tables[0].Rows[j]);
						float inventory = factoryInventoryEntries.calcInventoryTotal(database, factoryOrder.factoryNo, factoryInventoryEntry.createDateTime()) - factoryOrders.calcPlannedQuantity(database, factoryOrder.factoryNo, factoryInventoryEntry.createDateTime());
						if (inventory > (factoryOrder.quantity * 1000))
						{
							factoryOrder.shipDate = factoryInventoryEntry.date;
							factoryOrder.shipTime = factoryInventoryEntry.timeOfDay;
							factoryOrder.save(database);
						}
						j++;
					}
					
				}
				else
				{
					// Move backwards
					log("Moving bachward", 0);

					DataSet preDataSet = factoryInventoryEntries.getPreOrderDataSet(database, factoryOrder.factoryNo, shipDateTime);
					int j = 0;
					bool done = false;
					while ((j < preDataSet.Tables[0].Rows.Count) && (done == false))
					{
						FactoryInventoryEntry factoryInventoryEntry = new FactoryInventoryEntry(preDataSet.Tables[0].Rows[j]);
						float inventory = factoryInventoryEntries.calcInventoryTotal(database, factoryOrder.factoryNo, factoryInventoryEntry.createDateTime()) - factoryOrders.calcPlannedQuantity(database, factoryOrder.factoryNo, factoryInventoryEntry.createDateTime());
						if ((inventory < (factoryOrder.quantity * 1000)) || (factoryInventoryEntry.date < DateTime.Today))
						{
							if ((factoryInventoryEntry.date >= DateTime.Today) && (factoryInventoryEntry.timeOfDay.Hour >= DateTime.Now.Hour))
							{
								factoryOrder.shipDate = DateTime.Today;
								factoryOrder.shipTime = new DateTime(1754, 1, 1, DateTime.Now.Hour+1, 0, 0);
							}
							else
							{
								factoryOrder.shipDate = factoryInventoryEntry.date;
								factoryOrder.shipTime = factoryInventoryEntry.timeOfDay;
							}
							factoryOrder.save(database);
							done = true;
						}
						j++;
					}
				}

				i++;
			}
		}

		private void produceScheduledFactoryOrders()
		{
			log("Producing scheduled factory orders...", 0);

			ShippingCustomers shippingCustomers = new ShippingCustomers();
			ShippingCustomerOrganizations shippingCustomerOrganizations = new ShippingCustomerOrganizations();
			ConsumerRelations consumerRelations = new ConsumerRelations();	
			ConsumerInventories consumerInventories = new ConsumerInventories();
			ShippingCustomerSchedules shippingCustomerSchedules = new ShippingCustomerSchedules();
			FactoryOrders factoryOrders = new FactoryOrders();
			Categories categories = new Categories();

			DataSet factoryShippingCustomerDataSet = shippingCustomerOrganizations.getDataSet(database, 1);

			int i = 0;
			while (i < factoryShippingCustomerDataSet.Tables[0].Rows.Count)
			{
				ShippingCustomerOrganization shippingCustomerOrganization = new ShippingCustomerOrganization(factoryShippingCustomerDataSet.Tables[0].Rows[i]);
				
				FactoryOrder factoryOrder = factoryOrders.getLastEntry(database, shippingCustomerOrganization.shippingCustomerNo);
				DateTime lastDate = DateTime.Today;
				if (factoryOrder != null)
				{
					lastDate = factoryOrder.shipDate;
				}
				if (lastDate < DateTime.Today) lastDate = DateTime.Today;
				
				DateTime currentDate = lastDate;
				while (currentDate < DateTime.Today.AddDays(14))
				{
					if (shippingCustomerSchedules.checkSchedule(database, shippingCustomerOrganization.shippingCustomerNo, currentDate))
					{

						ShippingCustomerSchedule shippingCustomerSchedule = shippingCustomerSchedules.findSchedule(database, shippingCustomerOrganization.shippingCustomerNo, currentDate);
						
						if (!factoryOrders.orderExists(database, shippingCustomerSchedule.shippingCustomerNo, currentDate))
						{
							DataSet consumerRelationDataSet = consumerRelations.getDataSet(database, 0, shippingCustomerOrganization.shippingCustomerNo);

							int j = 0;
							bool orderCreated = false;
							while ((j < consumerRelationDataSet.Tables[0].Rows.Count) && (!orderCreated))
							{
								ConsumerRelation consumerRelation = new ConsumerRelation(consumerRelationDataSet.Tables[0].Rows[j]);


								Consumer consumer = consumerRelation.getConsumer(database);
								if (consumer != null)
								{
									DateTime consumerInventoryDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, shippingCustomerSchedule.time.Hour, 0, 0).AddMinutes(consumerRelation.travelTime);

									ConsumerInventory consumerInventory = consumerInventories.getEntry(database, consumerRelation.consumerNo, consumerInventoryDateTime);
									if (consumerInventory == null)
									{
										consumerInventory = new ConsumerInventory();
										consumerInventory.inventory = 0;
									}

									if (consumerInventory.inventory <= consumer.inventoryShipmentPoint)
									{
										ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, consumerRelation.no);

										if (shippingCustomer != null)
										{
											ConsumerCapacities consumerCapacities = new ConsumerCapacities();
											if (consumerCapacities.capacityExists(database, consumer.no, consumerInventoryDateTime)) 
											{

												FactoryOrder newOrder = new FactoryOrder();

												log("Creating new order for consumer: "+consumer.name+", Customer: "+shippingCustomer.name+", Organization: "+shippingCustomerOrganization.code, 0);
									
												if (shippingCustomerOrganization.type == 0)
												{
													newOrder.organizationNo = shippingCustomerOrganization.code;
												}
												else
												{
													newOrder.agentCode = shippingCustomerOrganization.code;
													Agents agents = new Agents();
													Agent agent = agents.getAgent(database, shippingCustomerOrganization.code);
													if (agent != null) newOrder.organizationNo = agent.organizationNo;
										
												}

												Category category = categories.getEntry(database, consumerRelation.categoryCode);
								

										
												newOrder.applyConsumer(consumer);
												newOrder.applyFactory(shippingCustomer);
												newOrder.shipDate = currentDate;
												newOrder.shipTime = shippingCustomerSchedule.time;
												newOrder.categoryCode = consumerRelation.categoryCode;
												if (category != null) newOrder.categoryDescription = category.description;
												newOrder.quantity = shippingCustomerSchedule.quantity;
												newOrder.createdByType = 0;
												newOrder.createdByCode = "AUTO";
												newOrder.type = 2;
												newOrder.save(database);

												orderCreated = true;
											}

										}
									}
								

								}
								j++;
							}
						}

					}

					currentDate = currentDate.AddDays(1);
				}

				i++;
			}

		}

		private void produceFloatingFactoryOrders()
		{
			log("Producing floating factory orders...", 0);

			Factories factories = new Factories();
			FactoryInventoryEntries factoryInventoryEntries = new FactoryInventoryEntries();
			FactoryOrganizations factoryOrganizations = new FactoryOrganizations();
			ConsumerRelations consumerRelations = new ConsumerRelations();	
			ConsumerInventories consumerInventories = new ConsumerInventories();
			FactoryOrders factoryOrders = new FactoryOrders();
			Categories categories = new Categories();

			DataSet factoryDataSet = factories.getDataSet(database);

			int i = 0;
			while (i < factoryDataSet.Tables[0].Rows.Count)
			{
				Factory factory = new Factory(factoryDataSet.Tables[0].Rows[i]);
							
				DateTime currentDate = DateTime.Today;
				while (currentDate < DateTime.Today.AddDays(14))
				{
					int hour = 0;
					while (hour < 24)
					{
						DateTime currentDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hour, 0, 0);

						float factoryInventory = factoryInventoryEntries.calcInventoryTotal(database, factory.no, currentDateTime);

						DataSet consumerRelationDataSet = consumerRelations.getDataSet(database, 1, factory.no);

						bool orderCreated = false;

						int j = 0;
						while ((j < consumerRelationDataSet.Tables[0].Rows.Count) && (!orderCreated))
						{
							ConsumerRelation consumerRelation = new ConsumerRelation(consumerRelationDataSet.Tables[0].Rows[j]);

							Consumer consumer = consumerRelation.getConsumer(database);
							if (consumer != null)
							{
								DateTime consumerInventoryDateTime = currentDateTime.AddMinutes(consumerRelation.travelTime);

								ConsumerInventory consumerInventory = consumerInventories.getEntry(database, consumerRelation.consumerNo, consumerInventoryDateTime);
								if (consumerInventory == null)
								{
									consumerInventory = new ConsumerInventory();
									consumerInventory.inventory = 0;
								}

								if ((consumerInventory.inventory <= consumer.inventoryShipmentPoint) && (factoryInventory > consumerRelation.quantity))
								{
									ConsumerCapacities consumerCapacities = new ConsumerCapacities();
									if (consumerCapacities.capacityExists(database, consumer.no, consumerInventoryDateTime)) 
									{
										Agent agent = factoryOrganizations.getFactoryAgent(database, factory.no);
										
										log("Creating new floating order for consumer: "+consumer.name+", Factory: "+factory.name+", Organization: ", 0);

										FactoryOrder newOrder = new FactoryOrder();
										newOrder.agentCode = agent.code;
										newOrder.organizationNo = agent.organizationNo;

										Category category = categories.getEntry(database, consumerRelation.categoryCode);
										
										newOrder.applyConsumer(consumer);
										newOrder.applyFactory(factory);
										newOrder.shipDate = currentDate;
										newOrder.shipTime = currentDateTime;
										newOrder.categoryCode = consumerRelation.categoryCode;
										if (category != null) newOrder.categoryDescription = category.description;
										newOrder.quantity = consumerRelation.quantity;
										newOrder.createdByType = 0;
										newOrder.createdByCode = "AUTO";
										newOrder.type = 2;
										newOrder.save(database);

										orderCreated = true;
										
									}

								}
								
							}
							j++;
						}

						hour++;
					}

					currentDate = currentDate.AddDays(1);
				}

				i++;
			}

		}

		private void assignToAgents()
		{
			FactoryOrders factoryOrders = new FactoryOrders();
			DataSet dataSet = factoryOrders.getUnAssignedDataSet(database);

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				FactoryOrder factoryOrder = new FactoryOrder(dataSet.Tables[0].Rows[i]);
				
				Agents agents = new Agents();
				DataSet agentDataSet = agents.getDataSet(database, factoryOrder.organizationNo, Agents.TYPE_TANK);
				
				int j = 0;
				string agentCode = "";
				int noOfOrders = 999;
				while (j < agentDataSet.Tables[0].Rows.Count)
				{
					Agent agent = new Agent(agentDataSet.Tables[0].Rows[j]);
					if (noOfOrders > agent.countFactoryOrders(database, factoryOrder.shipDate))
					{
						agentCode = agent.code;
						noOfOrders = agent.countFactoryOrders(database, factoryOrder.shipDate);
					}

					j++;
				}

				if (agentCode != "")
				{
					log("Assigning factory order "+factoryOrder.entryNo+" to agent "+agentCode, 0);

					factoryOrder.assignOrder(database, agentCode);
				}

				i++;
			}

		}

		private void log(string message, int type)
		{
			logger.write("[FactoryOrderManagement] "+message, type);
		}


	}
}
