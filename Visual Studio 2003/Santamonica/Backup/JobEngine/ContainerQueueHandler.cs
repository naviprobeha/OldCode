using System;
using System.Threading;
using Navipro.SantaMonica.Common;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.JobEngine
{
	/// <summary>
	/// Summary description for ContainerQueueHandler.
	/// </summary>
	public class ContainerQueueHandler
	{
		private Configuration configuration;
		private Logger logger;
		
		private Thread thread;
		private bool running;

		public ContainerQueueHandler(Logger logger, Configuration configuration)
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

			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			LineOrders lineOrders = new LineOrders();
			
			ShipmentContainerQueueEntries shipmentContaineQueueEntries = new ShipmentContainerQueueEntries();
			ShipmentContainerQueueEntry shipmentContainerQueueEntry = shipmentContaineQueueEntries.getFirstEntry(database);
			
			while(shipmentContainerQueueEntry != null)
			{


				log("Processing "+shipmentContainerQueueEntry.shipmentNo+", Container: "+shipmentContainerQueueEntry.containerNo, 0);

				
				ShipmentHeader shipmentHeader = shipmentHeaders.getEntry(database, shipmentContainerQueueEntry.shipmentNo);
				if (shipmentHeader != null)
				{
					LineOrder lineOrder = null;

					//Handle different
					if (shipmentHeader.shipDate.ToString("yyyy-MM-dd") == DateTime.Today.ToString("yyyy-MM-dd"))
					{
						lineOrder = lineOrders.getContainerLineOrder(database, shipmentContainerQueueEntry.containerNo);
						if (lineOrder == null) //Create LineOrder
						{
							int lineOrderEntryNo = shipmentHeaders.getLineOrderForShipment(database, shipmentContainerQueueEntry.containerNo, shipmentHeader.shipDate);
							if (lineOrderEntryNo > 0)
							{
								lineOrder = lineOrders.getEntry(database, lineOrderEntryNo.ToString());							
							}
							else
							{
								lineOrder = createLineOrder(shipmentHeader, database);
							}
						}
					}
					else
					{
						int lineOrderEntryNo = shipmentHeaders.getLineOrderForShipment(database, shipmentContainerQueueEntry.containerNo, shipmentHeader.shipDate);
						if (lineOrderEntryNo > 0)
						{
							lineOrder = lineOrders.getEntry(database, lineOrderEntryNo.ToString());							
						}
						else
						{
							lineOrder = createLineOrder(shipmentHeader, database);
						}

					}
					if (lineOrder != null)
					{
						assignShipmentToLineOrder(lineOrder, shipmentContainerQueueEntry, database);
					}
				}


				//Assign Shipment to active LineOrder
				// Add lineOrderShipment to active LineOrder and Container.


				shipmentContainerQueueEntry.delete(database);
				shipmentContainerQueueEntry = shipmentContaineQueueEntries.getFirstEntry(database); 


				//i++;
			}

			database.close();
		}

		private LineOrder createLineOrder(ShipmentHeader shipmentHeader, Database database)
		{
			Organizations organizations = new Organizations();
			ShippingCustomers shippingCustomers = new ShippingCustomers();

			Organization organization = organizations.getOrganization(database, shipmentHeader.organizationNo);	

			LineOrder lineOrder = null;

			if (organization.enableAutoPlan)
			{

				log("Creating lineorder...",0);
				if (organization.shippingCustomerNo != "")
				{
					ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database,organization.shippingCustomerNo);				
					if (shippingCustomer != null)
					{
						LineOrder newLineOrder = new LineOrder();
						newLineOrder.shippingCustomerNo = shippingCustomer.no;
						newLineOrder.shipDate = DateTime.Now.AddDays(organization.containerUsageLeadTimeDays - 1);
						newLineOrder.shippingCustomerName = shippingCustomer.name;
						newLineOrder.address = shippingCustomer.address;
						newLineOrder.address2 = shippingCustomer.address2;
						newLineOrder.postCode = shippingCustomer.postCode;
						newLineOrder.city = shippingCustomer.city;
						newLineOrder.countryCode = shippingCustomer.countryCode;
						newLineOrder.phoneNo = shippingCustomer.phoneNo;
						newLineOrder.cellPhoneNo = shippingCustomer.cellPhoneNo;
						newLineOrder.comments = "Uppsamlingsbil: "+shipmentHeader.agentCode+", Chaufför: "+shipmentHeader.userName;
						newLineOrder.directionComment = shippingCustomer.directionComment;
						newLineOrder.directionComment2 = shippingCustomer.directionComment2;
						newLineOrder.positionX = shippingCustomer.positionX;	
						newLineOrder.positionY = shippingCustomer.positionY;
						newLineOrder.status = 0;
						newLineOrder.enableAutoPlan = true;			
						newLineOrder.save(database);
				
				
				
						lineOrder = newLineOrder;

						//skapa LineOrderContainer.
						LineOrderContainer newLineOrderContainer = new LineOrderContainer(lineOrder);
						newLineOrderContainer.containerNo = shipmentHeader.containerNo;
						newLineOrderContainer.save(database);
					}
				}
							
			}
			else
			{
				log("Organization "+organization.no+" not enabled for auto plan.", 0);
			}

			return lineOrder;
		}
		
		private void assignShipmentToLineOrder(LineOrder lineOrder, ShipmentContainerQueueEntry shipmentContainerQueueEntry, Database database)
		{
			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();

			LineOrderShipments lineOrderShipments = new LineOrderShipments();
			if (lineOrderShipments.getEntry(database, shipmentContainerQueueEntry.shipmentNo) == null)
			{
				log("Assigning container...",0);

					
				LineOrderShipment lineOrderShipment = new LineOrderShipment(lineOrder);
				lineOrderShipment.containerNo = shipmentContainerQueueEntry.containerNo;
				lineOrderShipment.shipmentNo = shipmentContainerQueueEntry.shipmentNo;
				lineOrderShipment.save(database);

				ShipmentHeader shipmentHeader = shipmentHeaders.getEntry(database, shipmentContainerQueueEntry.shipmentNo);
				if (shipmentHeader != null)
				{
					shipmentHeader.lineOrderEntryNo = lineOrderShipment.lineOrderEntryNo;
					shipmentHeader.save(database);
				}

				//Om status > skickad så sätt status till tilldelad. Är lineordern mer än skickad så måste vi skicka om den.

				lineOrder.updateCategoryInformation(database);
				lineOrder.updateWeight(database);

				ContainerEntry arrivalReport = this.checkArrivalReport(lineOrderShipment.shipmentNo, lineOrderShipment.containerNo, database);
				if (arrivalReport != null)
				{
					lineOrder.confirmOrder(database, arrivalReport.estimatedArrivalDateTime, arrivalReport.locationCode); 
				}
			}

		}

		private void log(string message, int type)
		{
			logger.write("[ContainerQueueHandler] "+message, type);
		}

		private ContainerEntry checkArrivalReport(string shipmentNo, string containerNo, Database database)
		{
			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			ShipmentHeader shipmentHeader = shipmentHeaders.getEntry(database, shipmentNo);
			if (shipmentHeader != null)
			{
				ContainerEntries containerEntries = new ContainerEntries();
				DataSet containerEntryDataSet = containerEntries.getTypeDataSet(database, 2, containerNo, shipmentHeader.shipDate);
				if (containerEntryDataSet.Tables[0].Rows.Count > 0)
				{
					ContainerEntry containerEntry = new ContainerEntry(containerEntryDataSet.Tables[0].Rows[0]);
					log("Container-entry found: Customer no: "+containerEntry.locationCode, 1);
					return containerEntry;

				}

			}

			return null;

		}
	}
}
