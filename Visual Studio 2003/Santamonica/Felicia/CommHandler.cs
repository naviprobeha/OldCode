using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Threading;
using System.Collections;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for CommHandler.
	/// </summary>
	public class CommHandler
	{
		private SmartDatabase smartDatabase;
		private Status status;
		private DataSetup dataSetup;
		private NotifyForm notifyForm;


		private Thread thread;
		private bool running;
		private bool stopped;
		private DateTime lastRunTime;
		private int intervalMinutes;
		private DateTime lastSuccessfulConnection;
		private bool hangUpDone;

		public CommHandler(SmartDatabase smartDatabase, Status status, NotifyForm notifyForm)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
			this.status = status;
			this.dataSetup = smartDatabase.getSetup();
			this.notifyForm = notifyForm;
			this.lastSuccessfulConnection = DateTime.Now;
			this.hangUpDone = false;

			running = true;
			intervalMinutes = dataSetup.synchInterval;

			thread = new Thread(new ThreadStart(run));
			thread.Start();

		}

		public void run()
		{
			try
			{
				Thread.Sleep(5000);

				while (running)
				{
					if (DateTime.Now >= lastRunTime.AddMinutes(intervalMinutes))
					{
						synchronize();


						lastRunTime = DateTime.Now;
					}

				

					Thread.Sleep(100);
				}
			}
			catch(Exception)
			{

			}

			stopped = true;
			
		}

		public void stop()
		{
			running = false;
			
		}

		public void waitForTermination()
		{
			while(!stopped)
			{
				Thread.Sleep(1000);
			}
		}

		private void synchronize()
		{
			try
			{
				sendData();
				checkPrices();
				getData();
			}
			catch(Exception)
			{}
		}

		private void sendData()
		{
			Navipro.SantaMonica.Goldfinger.Goldfinger goldFinger = new Navipro.SantaMonica.Goldfinger.Goldfinger();
			goldFinger.Timeout = 30000;
			goldFinger.Url = dataSetup.host;

			notifyForm.invokeSetStatusText("Synkroniserar");

			string errorMessage = "";

			try
			{
				DataShipOrders dataShipOrders = new DataShipOrders(smartDatabase);
				dataShipOrders.cleanUp();

				DataLineOrders dataLineOrders = new DataLineOrders(smartDatabase);
				dataLineOrders.cleanUp();

				DataLineJournals dataLineJournals = new DataLineJournals(smartDatabase);
				dataLineJournals.cleanUp();

				DataFactoryOrders dataFactoryOrders = new DataFactoryOrders(smartDatabase);
				dataFactoryOrders.cleanUp();


				DataSyncActions syncActions = new DataSyncActions(smartDatabase);
				DataSyncAction syncAction = syncActions.getFirstSyncAction();

				while(syncAction != null)
				{

					errorMessage = "[Entry No: "+syncAction.entryNo+"] [Type: "+syncAction.type+"] [Primary Key: "+syncAction.primaryKey+"]";

					// ShipOrder status
					if (syncAction.type == 1)
					{
						DataShipOrder dataShipOrder = new DataShipOrder(smartDatabase, int.Parse(syncAction.primaryKey));
						if (dataShipOrder.entryNo > 0)
						{
							if (dataShipOrder.status == 7) dataShipOrder.status = 2; // Kanske
							if (dataShipOrder.status == 8) dataShipOrder.status = 1; // Nej tack
							goldFinger.setShipOrderStatusEx(dataSetup.agentId, syncAction.primaryKey, dataShipOrder.status, dataShipOrder.positionX, dataShipOrder.positionY, dataShipOrder.shipTime);
						}

					}

					// Shipment
					if (syncAction.type == 2)
					{
						DataShipmentHeaders dataShipmentHeaders = new DataShipmentHeaders(smartDatabase);
						DataShipmentLines dataShipmentLines = new DataShipmentLines(smartDatabase);
						DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
						goldFinger.createShipment(dataSetup.agentId, dataShipmentHeaders.getEntryDataSet(int.Parse(syncAction.primaryKey)), dataShipmentLines.getEntriesDataSet(int.Parse(syncAction.primaryKey)), dataShipmentLineIds.getEntriesDataSet(int.Parse(syncAction.primaryKey)));

						dataShipmentHeaders.setStatus(int.Parse(syncAction.primaryKey), 2);
						dataShipmentHeaders.cleanUp();
					}

					// Message
					if (syncAction.type == 3)
					{
						goldFinger.setMessageStatus(dataSetup.agentId, int.Parse(syncAction.primaryKey), 2);
					}

					// Order
					if (syncAction.type == 4)
					{
						DataOrderHeaders dataOrderHeaders = new DataOrderHeaders(smartDatabase);
						DataOrderLines dataOrderLines = new DataOrderLines(smartDatabase);
						DataOrderLineIds dataOrderLineIds = new DataOrderLineIds(smartDatabase);
						goldFinger.createOrder(dataSetup.agentId, dataOrderHeaders.getEntryDataSet(int.Parse(syncAction.primaryKey)), dataOrderLines.getEntriesDataSet(int.Parse(syncAction.primaryKey)), dataOrderLineIds.getEntriesDataSet(int.Parse(syncAction.primaryKey)));

						dataOrderHeaders.setStatus(int.Parse(syncAction.primaryKey), 2);
						dataOrderHeaders.cleanUp();
					}

					//Re-assign shiporder
					if (syncAction.type == 5)
					{
						DataShipOrder dataShipOrder = new DataShipOrder(smartDatabase, int.Parse(syncAction.primaryKey));
						if (dataShipOrder.entryNo > 0)
						{
							goldFinger.assignShipOrder(dataSetup.agentId, syncAction.primaryKey, dataShipOrder.agentCode);						
						}
					}

					//Container Entry
					if (syncAction.type == 6)
					{
						DataContainerEntries dataContainerEntries = new DataContainerEntries(smartDatabase);
						DataSet containerEntryDataSet = dataContainerEntries.getEntryDataSet(int.Parse(syncAction.primaryKey));

						if (containerEntryDataSet.Tables[0].Rows.Count > 0)
						{
							goldFinger.createContainerEntry(dataSetup.agentId, containerEntryDataSet);
							dataContainerEntries.deleteEntry(int.Parse(syncAction.primaryKey));
						}
					}

					//Lineorder status
					if (syncAction.type == 7)
					{
						DataLineOrder dataLineOrder = new DataLineOrder(smartDatabase, int.Parse(syncAction.primaryKey));
						if (dataLineOrder.entryNo > 0)
						{
							if (dataLineOrder.status == 8) dataLineOrder.status = 2; // Kanske
							if (dataLineOrder.status == 9) dataLineOrder.status = 1; // Nej tack
							goldFinger.setLineOrderStatusEx(dataSetup.agentId, syncAction.primaryKey, dataLineOrder.status, dataLineOrder.positionX, dataLineOrder.positionY, dataLineOrder.shipTime, dataLineOrder.loadWaitTime);
						}
					}

					//Linejournal 
					if (syncAction.type == 8)
					{
						DataSet lineJournalDataSet = dataLineJournals.getEntry(int.Parse(syncAction.primaryKey));
						goldFinger.reportLineJournal(dataSetup.agentId, lineJournalDataSet);
					}

					//FactoryOrder status
					if (syncAction.type == 9)
					{
						DataSet factoryOrderDataSet = dataFactoryOrders.getEntry(int.Parse(syncAction.primaryKey));
						goldFinger.setFactoryOrderStatus(dataSetup.agentId, factoryOrderDataSet);
					}


					syncAction.delete();
				
					syncAction = syncActions.getFirstSyncAction();

					System.Windows.Forms.Application.DoEvents();
					this.lastSuccessfulConnection = DateTime.Now;

				}
			}
			catch(Exception e)
			{
				if (smartDatabase.debug) System.Windows.Forms.MessageBox.Show(e.Message);
				notifyForm.invokeSetStatusText("Offline");
				
				try
				{
					goldFinger.reportError(dataSetup.agentId, errorMessage+" "+e.Message);
				}
				catch(Exception e2)
				{
					if (e2.Message != "") {}
				}
			}

		}

		private void checkPrices()
		{
			Navipro.SantaMonica.Goldfinger.Goldfinger goldFinger = new Navipro.SantaMonica.Goldfinger.Goldfinger();
			goldFinger.Timeout = 30000;
			goldFinger.Url = dataSetup.host;

			try
			{

				string itemNo = goldFinger.getPriceUpdateItemNo(dataSetup.agentId);
				while (itemNo != "")
				{
					DataItem dataItem = new DataItem(smartDatabase, itemNo);
					dataItem.deletePrices();

					getData();

					itemNo = goldFinger.getPriceUpdateItemNo(dataSetup.agentId);
				}

			}
			catch(Exception e)
			{
				if (smartDatabase.debug) System.Windows.Forms.MessageBox.Show(e.Message);
				notifyForm.invokeSetStatusText("Offline");
				
				try
				{
					goldFinger.reportError(dataSetup.agentId, e.Message);
				}
				catch(Exception e2)
				{
					if (e2.Message != "") {}
				}
			}
		}

		private void getData()
		{
			Navipro.SantaMonica.Goldfinger.Goldfinger goldFinger = new Navipro.SantaMonica.Goldfinger.Goldfinger();
			goldFinger.Timeout = 30000;
			goldFinger.Url = dataSetup.host;

			notifyForm.invokeSetStatusText("Synkroniserar");
			
			int synchEntryNo = 0;
			string primaryKey = "";

			try
			{
				//if (smartDatabase.debug) System.Windows.Forms.MessageBox.Show("Synkar...");

				int recordsToSync = goldFinger.reportStatusTrip(dataSetup.agentId, status.rt90x, status.rt90y, status.heading, status.speed, status.height, status.status, status.mobileUserName, status.tripMeter);

				if (recordsToSync > 0)
				{
					int i = 0;
					while (i < recordsToSync)
					{
						i++;

						int type = 0;
						int action = 0;
						primaryKey = "";

						DataSet dataset = goldFinger.getSynchEntry(dataSetup.agentId, ref type, ref action, ref primaryKey, ref synchEntryNo);

						if (type == 0) // ShipOrder
						{
							if (action == 2)
							{
								DataShipOrder dataShipOrder = new DataShipOrder(smartDatabase, int.Parse(primaryKey));
								dataShipOrder.delete();
							}
							else
							{
								DataShipOrder dataShipOrder = new DataShipOrder(dataset, smartDatabase);
							}
							//notifyForm.invokeUpdateGrid();
						}

						if (type == 1) // Customer
						{
							if (action == 2)
							{
								DataCustomer dataCustomer = new DataCustomer(smartDatabase, primaryKey);
								dataCustomer.delete();
							}
							else
							{
								DataCustomer dataCustomer = new DataCustomer(dataset, smartDatabase);
							}
						}

						if (type == 2) // Item
						{
							if (action == 2)
							{
								DataItem dataItem = new DataItem(smartDatabase, primaryKey);
								dataItem.delete();
							}
							else
							{
								DataItem dataItem = new DataItem(dataset, smartDatabase);
							}
						}

						if (type == 3) // Item Price
						{
							if (action == 2)
							{
								DataItemPrice dataItemPrice = new DataItemPrice(smartDatabase, int.Parse(primaryKey));

								if (primaryKey == "0")
								{
									dataItemPrice.deleteAll();
								}
								else
								{
									dataItemPrice.delete();
								}

								goldFinger.acknowledgePriceUpdate(dataSetup.agentId, dataItemPrice.itemNo, calcPriceChecksum(dataItemPrice.itemNo));
							}
							else
							{
								DataItemPrice dataItemPrice = new DataItemPrice(dataset, smartDatabase);
								goldFinger.acknowledgePriceUpdate(dataSetup.agentId, dataItemPrice.itemNo, calcPriceChecksum(dataItemPrice.itemNo));
							}


						}

						if (type == 4) // Map
						{
							if (action == 2)
							{
								DataMap dataMap = new DataMap(smartDatabase, primaryKey);
								dataMap.delete();
							}
							else
							{
								DataMap dataMap = new DataMap(dataset, smartDatabase);
								dataMap.getMapFromServer();
							}					
						}


						if (type == 5) // Message
						{
							if (action == 2)
							{
								DataMessage dataMessage = new DataMessage(smartDatabase, int.Parse(primaryKey), false);
								dataMessage.delete();
							}
							else
							{
								DataMessage dataMessage = new DataMessage(dataset, smartDatabase);
								goldFinger.setMessageStatus(dataSetup.agentId, dataMessage.entryNo, 1);
							}

						}


						if (type == 6) // Organization
						{
							if (action == 2)
							{
								DataOrganization dataOrganization = new DataOrganization(smartDatabase, primaryKey);
								dataOrganization.delete();
							}
							else
							{
								DataOrganization dataOrganization = new DataOrganization(dataset, smartDatabase);
							}					
						}

						if (type == 7) // Item Price Extended
						{
							if (action == 2)
							{
								DataItemPriceExtended dataItemPriceExtended = new DataItemPriceExtended(smartDatabase, int.Parse(primaryKey));

								if (primaryKey == "0")
								{
									dataItemPriceExtended.deleteAll();
								}
								else
								{
									dataItemPriceExtended.delete();
								}

								goldFinger.acknowledgePriceUpdate(dataSetup.agentId, dataItemPriceExtended.itemNo, calcPriceChecksum(dataItemPriceExtended.itemNo));
							}
							else
							{
								DataItemPriceExtended dataItemPriceExtended = new DataItemPriceExtended(dataset, smartDatabase);
								goldFinger.acknowledgePriceUpdate(dataSetup.agentId, dataItemPriceExtended.itemNo, calcPriceChecksum(dataItemPriceExtended.itemNo));
							}					
						}

						if (type == 8) // Mobile User
						{
							if (action == 2)
							{
								DataMobileUser dataMobileUser = new DataMobileUser(smartDatabase, int.Parse(primaryKey));
								dataMobileUser.delete();
							}
							else
							{
								DataMobileUser dataMobileUser = new DataMobileUser(dataset, smartDatabase);
							}
						}

						if (type == 9) // Ship Order Line
						{
							if (action == 2)
							{
								DataShipOrderLine dataShipOrderLine = new DataShipOrderLine(smartDatabase, int.Parse(primaryKey));
								dataShipOrderLine.delete();
							}
							else
							{
								DataShipOrderLine dataShipOrderLine = new DataShipOrderLine(dataset, smartDatabase);
							}
						}

						if (type == 10) // Ship Order Line Id
						{
							if (action == 2)
							{
								DataShipOrderLineId dataShipOrderLineId = new DataShipOrderLineId(smartDatabase, int.Parse(primaryKey));
								dataShipOrderLineId.delete();
							}
							else
							{
								DataShipOrderLineId dataShipOrderLineId = new DataShipOrderLineId(dataset, smartDatabase);
							}
						}

						if (type == 11) // Ship Order Line Id
						{
							if (action == 2)
							{
								DataAgent dataAgent = new DataAgent(smartDatabase, primaryKey);
								dataAgent.delete();
							}
							else
							{
								DataAgent dataAgent = new DataAgent(dataset, smartDatabase);
							}
						}

						if (type == 12) // Container
						{
							if (action == 2)
							{
								DataContainer dataContainer = new DataContainer(smartDatabase, primaryKey);
								dataContainer.delete();
							}
							else
							{
								DataContainer dataContainer = new DataContainer(dataset, smartDatabase);
							}
						}

						if (type == 13) // Line Journal
						{
							if (action == 2)
							{
								DataLineJournal dataLineJournal = new DataLineJournal(smartDatabase, int.Parse(primaryKey));
								dataLineJournal.delete();
							}
							else
							{
								DataLineJournal dataLineJournal = new DataLineJournal(dataset, smartDatabase);
							}
						}

						if (type == 14) // Line Order
						{
							if (action == 2)
							{
								DataLineOrder dataLineOrder = new DataLineOrder(smartDatabase, int.Parse(primaryKey));
								dataLineOrder.delete();
							}
							else
							{
								DataLineOrder dataLineOrder = new DataLineOrder(dataset, smartDatabase);
							}
							//notifyForm.invokeUpdateGrid();

						}

						if (type == 15) // Line Order Container
						{
							if (action == 2)
							{
								DataLineOrderContainer dataLineOrderContainer = new DataLineOrderContainer(smartDatabase, int.Parse(primaryKey));
								dataLineOrderContainer.delete();
							}
							else
							{
								DataLineOrderContainer dataLineOrderContainer = new DataLineOrderContainer(dataset, smartDatabase);
							}
							//notifyForm.invokeUpdateGrid();
						}

						if (type == 16) // Organization Location
						{
							if (action == 2)
							{
								DataOrganizationLocation dataOrganizationLocation = new DataOrganizationLocation(smartDatabase, primaryKey);
								dataOrganizationLocation.delete();
							}
							else
							{
								DataOrganizationLocation dataOrganizationLocation = new DataOrganizationLocation(dataset, smartDatabase);
							}
						}

						if (type == 17) // Category
						{
							if (action == 2)
							{
								DataCategory dataCategory = new DataCategory(smartDatabase, primaryKey);
								dataCategory.delete();
							}
							else
							{
								DataCategory dataCategory = new DataCategory(dataset, smartDatabase);
							}
						}

						if (type == 18) // Customer Ship Address
						{
							if (action == 2)
							{
								DataCustomerShipAddress dataCustomerShipAddress = new DataCustomerShipAddress(smartDatabase, int.Parse(primaryKey));

								if (primaryKey == "0")
								{
									dataCustomerShipAddress.deleteAll();
								}
								else
								{
									dataCustomerShipAddress.delete();
								}
								
							}
							else
							{
								DataCustomerShipAddress dataCustomerShipAddress = new DataCustomerShipAddress(dataset, smartDatabase);
							}
						}

						if (type == 19) // Factory Order
						{
							if (action == 2)
							{
								DataFactoryOrder dataFactoryOrder = new DataFactoryOrder(smartDatabase, int.Parse(primaryKey));
								dataFactoryOrder.delete();
							}
							else
							{
								DataFactoryOrder dataFactoryOrder = new DataFactoryOrder(dataset, smartDatabase);
							}

						}

						if (type == 21) // Consumer Status
						{
							if (action == 2)
							{
								DataConsumerStatus dataConsumerStatus = new DataConsumerStatus(smartDatabase, primaryKey);
								dataConsumerStatus.delete();
							}
							else
							{
								DataConsumerStatus dataConsumerStatus = new DataConsumerStatus(dataset, smartDatabase);
							}

						}

						goldFinger.ackSynchEntry(synchEntryNo);

						this.lastSuccessfulConnection = DateTime.Now;
						this.hangUpDone = false;

						System.Windows.Forms.Application.DoEvents();
					}

				}

				resendQueuedShipments();

				notifyForm.invokeSetStatusText("Online");

				this.lastSuccessfulConnection = DateTime.Now;

			}
			catch(Exception e)
			{
				if (smartDatabase.debug) System.Windows.Forms.MessageBox.Show("Sync to Server: "+e.Message);
				notifyForm.invokeSetStatusText("Offline");

				try
				{
					goldFinger.reportError(dataSetup.agentId, "SynchError ("+primaryKey+"): "+e.Message);
				}
				catch(Exception e2)
				{
					if (e2.Message != "") {}				
				}

			}

		}

		private void resendQueuedShipments()
		{
			DataShipmentHeaders dataShipmentHeaders = new DataShipmentHeaders(smartDatabase);
			DataSet shipmentDataSet = dataShipmentHeaders.getDataSet(1);

			DataSyncActions syncActions = new DataSyncActions(smartDatabase);

			int i = 0;
			while (i < shipmentDataSet.Tables[0].Rows.Count)
			{
				if (!syncActions.syncActionExists(2, 0, shipmentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()))
				{
					syncActions.addSyncAction(2, 0, shipmentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
				}
				i++;
			}

		}


		public void checkConnectionPerformHangup()
		{
			if (dataSetup.hangUpConnections)
			{
				if ((this.lastSuccessfulConnection.AddMinutes(60) < DateTime.Now) && (!hangUpDone))
				{
					try
					{
						WindowsCE.hangUpConnections();
					}
					catch(Exception e)
					{
						if (e.Message != "") {}
					}
					this.hangUpDone = true;

				}
			}
		}

		public bool checkConnection()
		{
			if (this.lastSuccessfulConnection.AddMinutes(15) < DateTime.Now)
			{
				lastSuccessfulConnection = DateTime.Now;
				return false;
			}			

			return true;

		}

		private float calcPriceChecksum(string itemNo)
		{
			float checksum1 = 0;
			SqlCeDataReader dataReader = smartDatabase.query("SELECT SUM(unitPrice) FROM itemPrice WHERE itemNo = '"+itemNo+"'");
			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) checksum1 = float.Parse(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();

			float checksum2 = 0;
			dataReader = smartDatabase.query("SELECT SUM(lineAmount) FROM itemPriceExtended WHERE itemNo = '"+itemNo+"'");
			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) checksum2 = float.Parse(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();
			
			return checksum1 + checksum2;
		}
	}
}
