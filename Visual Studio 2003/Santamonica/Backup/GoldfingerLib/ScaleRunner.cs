using System;
using System.Data;
using System.Collections;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.Goldfinger
{
	/// <summary>
	/// Summary description for ScaleRunner.
	/// </summary>
	public class ScaleRunner : Logger
	{
		private Configuration configuration;
		private Database database;


		public ScaleRunner()
		{
			//
			// TODO: Add constructor logic here
			//
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

		public DataSet getUnSentFactoryLineJournals(string factoryCode)
		{
			LineJournals lineJournals = new LineJournals();
			DataSet lineJournalDataSet = lineJournals.getUnSentFactoryLineJournals(database, factoryCode);

			return lineJournalDataSet;

		}

		public DataSet getLineJournalOrders(int lineJournalEntryNo)
		{
			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournalEntryNo);

			return lineOrderDataSet;
		}

		public DataSet getLineJournalEntries(int lineJournalEntryNo)
		{
			//ContainerEntries containerEntries = new ContainerEntries();
			//DataSet containerEntriesDataSet = containerEntries.getDocumentDataSet(database, 2, lineJournalEntryNo.ToString(), 1);
			//return containerEntriesDataSet;

			System.Data.SqlClient.SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[Entry No], c.[Container No], c.[Category Code], c.[Weight], c.[Real Weight], c.[Scaled Date], c.[Scaled Time], c.[Is Scaled], o.[Shipping Customer Name], o.[Confirmed To Date], o.[Confirmed To Time] FROM [Line Order Container] c, [Line Order] o WHERE c.[Line Order Entry No] = o.[Entry No] AND o.[Line Journal Entry No] = '"+lineJournalEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;

			//return null;
		}

		public DataSet getContainerLineOrder(int lineJournalEntryNo, string containerNo)
		{
			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournalEntryNo);

			int i = 0;
			while (i < lineOrderDataSet.Tables[0].Rows.Count)
			{
				LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
				
				ContainerEntries containerEntries = new ContainerEntries();
				DataSet containerEntriesDataSet = containerEntries.getDocumentDataSet(database, 1, lineOrder.entryNo.ToString(), 0);
				int j = 0;
				while (j < containerEntriesDataSet.Tables[0].Rows.Count)
				{
					ContainerEntry containerEntry = new ContainerEntry(containerEntriesDataSet.Tables[0].Rows[j]);
					if (containerEntry.containerNo == containerNo)
					{
						return lineOrders.getDataSetEntry(database, lineOrder.entryNo.ToString());
					}

					j++;
				}
		
				i++;
			}

			return null;
		}


		public DataSet getLineOrderContainers(int lineOrderEntryNo)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, lineOrderEntryNo);

			return lineOrderContainerDataSet;
		}

		public DataSet getContainer(string containerNo)
		{
			Containers containers = new Containers();
			DataSet containerDataSet = containers.getDataSetEntry(database, containerNo);

			return containerDataSet;
		}

		public DataSet getContainersToScale(string factoryCode)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet lineOrderContainerDataSet = lineOrderContainers.getContainersToScaleDataSet(database, factoryCode);

			return lineOrderContainerDataSet;
		}

		public DataSet getLineOrder(int lineOrderEntryNo)
		{
			LineOrders lineOrders = new LineOrders();
			return lineOrders.getDataSetEntry(database, lineOrderEntryNo.ToString());
		}

		public DataSet getLineJournal(int lineJournalEntryNo, string factoryCode)
		{
			LineJournals lineJournals = new LineJournals();
			return lineJournals.getFactoryDataSetEntry(database, lineJournalEntryNo.ToString(), factoryCode);
		}

		public void setLineJournalAsSent(int lineJournalEntryNo)
		{
			LineJournals lineJournals = new LineJournals();
			LineJournal lineJournal = lineJournals.getEntry(database, lineJournalEntryNo.ToString());
			if (lineJournal != null)
			{
				lineJournal.sentToFactory = true;
				lineJournal.save(database);
			}
		}

		public void setContainerAsSent(int lineOrderEntryNo, int lineOrderContainerEntryNo)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			LineOrderContainer lineOrderContainer = lineOrderContainers.getEntry(database, lineOrderEntryNo, lineOrderContainerEntryNo);
			if (lineOrderContainer != null)
			{
				lineOrderContainer.isSentToScaling = true;
				lineOrderContainer.save(database);

				LineOrders lineOrders = new LineOrders();
				LineOrder lineOrder = lineOrders.getEntry(database, lineOrderContainer.lineOrderEntryNo.ToString());
				if (lineOrder != null)
				{
					setLineJournalAsSent(lineOrder.lineJournalEntryNo);
				}
			}
		}

		public void setLineOrderContainerWeight(int lineOrderEntryNo, string containerNo, float weight, DateTime dateTime)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			LineOrderContainer lineOrderContainer = lineOrderContainers.getEntry(database, lineOrderEntryNo, containerNo);
			if (lineOrderContainer != null)
			{
				lineOrderContainer.realWeight = weight;
				lineOrderContainer.scaledDateTime = dateTime;
				lineOrderContainer.isScaled = true;
				lineOrderContainer.save(database);
			}
		}

		public void createScaleEntry(string factoryCode, int type, DataSet transactionEntryDataSet)
		{

			// Transnr, Container, Referens, Transtid, Kund, Leverantor, Artikel, Nettovikt, Slapcont, Vaglangd, Bil, Uppdrag, Status, Container2
			ServerLogging serverLogging = new ServerLogging(database);

			try
			{
				int transNo = int.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());	
				string containerNo = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();	
				string reference = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();	
				DateTime entryDateTime = DateTime.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());	
				string customerNo = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();	
				string vendorNo = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();	
				string categoryCode = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();	
				float weight = 0;
				try
				{
					weight = float.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());	
				}
				catch(Exception e)
				{
					if (e.Message == "") {}

				}
				string containerTypeCode = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();	
				
				int noOfContainers = 0;
				try
				{
					noOfContainers = int.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());	
				}
				catch(Exception e)
				{
					if (e.Message == "") {}
				}

				string agentCode = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();	
				string assignmentNo = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();	
				int status = int.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());	

				string shippingCustomerNo = "";
				if (type == 0) shippingCustomerNo = vendorNo;
				if (type == 1) shippingCustomerNo = customerNo;

				int lineOrderEntryNo = 0;
				if (assignmentNo.IndexOf("-") > 0) lineOrderEntryNo = int.Parse(assignmentNo.Substring(0, assignmentNo.IndexOf("-")));

				ScaleEntry scaleEntry = new ScaleEntry();
				scaleEntry.factoryCode = factoryCode;
				scaleEntry.type = type;
				scaleEntry.entryNo = transNo;
				scaleEntry.containerNo = containerNo;
				scaleEntry.reference = reference;
				scaleEntry.entryDateTime = entryDateTime;
				scaleEntry.shippingCustomerNo = shippingCustomerNo;
				scaleEntry.containerTypeCode = containerTypeCode;
				scaleEntry.agentCode = agentCode;
				scaleEntry.lineOrderEntryNo = lineOrderEntryNo;
				scaleEntry.weight = weight;
				scaleEntry.categoryCode = categoryCode;
				scaleEntry.status = status;
				scaleEntry.save(database);

				if (scaleEntry.lineOrderEntryNo > 0)
				{
					LineOrderContainers lineOrderContainers = new LineOrderContainers();
					LineOrderContainer lineOrderContainer = lineOrderContainers.getEntry(database, lineOrderEntryNo, scaleEntry.containerNo);
					if (lineOrderContainer != null)
					{
						if (scaleEntry.status == 2)
						{
							lineOrderContainer.realWeight = scaleEntry.weight;
							lineOrderContainer.scaledDateTime = scaleEntry.entryDateTime;
							lineOrderContainer.isScaled = true;
							lineOrderContainer.save(database);
						}
					}
				}

			}
			catch(Exception e)
			{
				serverLogging.log(factoryCode, "[Scale] DateTime: "+transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString()+", Weight: "+transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()+", Containers: "+transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()+", Status: "+transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
				serverLogging.log(factoryCode, "[Scale] Error: "+e.Message);
				throw new Exception(e.Message);
			}
		}


		public void createScaleEntry(string factoryCode, int type, DataSet transactionEntryDataSet, DataSet transactionSubEntryDataSet)
		{
			ServerLogging serverLogging = new ServerLogging(database);

			// Transnr, Container, Referens, Transtid, Kund, Leverantor, Artikel, Nettovikt, Slapcont, Vaglangd, Bil, Uppdrag, Status, Container2

			try
			{
				int transNo = int.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());	
				string containerNo = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();	
				string reference = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();	

				DateTime entryDateTime = DateTime.Now;
				try
				{
					entryDateTime = DateTime.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());	
				}
				catch(Exception e)
				{
					if (e.Message == "") {}
				}

				string customerNo = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();	
				string vendorNo = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();	
				string categoryCode = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();	

				float weight = 0;
				try
				{
					weight = float.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());	
				}
				catch(Exception e)
				{
					if (e.Message == "") {}

				}

				string containerTypeCode = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();	
				
				int noOfContainers = 0;
				try
				{
					noOfContainers = int.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());	
				}
				catch(Exception e)
				{
					if (e.Message == "") {}
				}

				string agentCode = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();	
				string assignmentNo = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();	
				int status = int.Parse(transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());	
				string containerNo2 = transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();	
				string comment = "";

				if (transactionSubEntryDataSet.Tables[0].Rows.Count > 0)
				{
					comment = transactionSubEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
				}

				containerNo = containerNo.Replace("'", "");
				containerNo2 = containerNo2.Replace("'", "");
				comment = comment.Replace("'", "");
				reference = reference.Replace("'", "");

				string shippingCustomerNo = "";
				if (type == 0) shippingCustomerNo = vendorNo;
				if (type == 1) shippingCustomerNo = customerNo;

				int lineOrderEntryNo = 0;
				if (assignmentNo.IndexOf("-") > 0) lineOrderEntryNo = int.Parse(assignmentNo.Substring(0, assignmentNo.IndexOf("-")));

				ScaleEntry scaleEntry = new ScaleEntry();
				scaleEntry.factoryCode = factoryCode;
				scaleEntry.type = type;
				scaleEntry.entryNo = transNo;
				scaleEntry.containerNo = containerNo;
				scaleEntry.reference = reference;
				scaleEntry.entryDateTime = entryDateTime;
				scaleEntry.shippingCustomerNo = shippingCustomerNo;
				scaleEntry.containerTypeCode = containerTypeCode;
				scaleEntry.agentCode = agentCode;
				scaleEntry.lineOrderEntryNo = lineOrderEntryNo;
				scaleEntry.weight = weight;
				scaleEntry.categoryCode = categoryCode;
				scaleEntry.status = status;
				scaleEntry.noOfContainers = noOfContainers;
				scaleEntry.containerNo2 = containerNo2;
				scaleEntry.comment = comment;
				scaleEntry.navisionStatus = 1;

				scaleEntry.save(database);


				if (scaleEntry.type == 0)
				{
					if (scaleEntry.lineOrderEntryNo > 0)
					{

						if (scaleEntry.status == 2)
						{

							LineOrderContainers lineOrderContainers = new LineOrderContainers();
							LineOrderContainer lineOrderContainer = lineOrderContainers.getEntry(database, lineOrderEntryNo, scaleEntry.containerNo);
							if (lineOrderContainer != null)
							{

								lineOrderContainer.realWeight = scaleEntry.weight;
								lineOrderContainer.scaledDateTime = scaleEntry.entryDateTime;
								lineOrderContainer.isScaled = true;

								lineOrderContainer.save(database);

														
								lineOrderContainer.updateInventory(database);
							}

							if (scaleEntry.noOfContainers > 1)
							{
								//Match with containers on same lineorder

								DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, lineOrderEntryNo);
								int i = 0;
								int noOfUpdatedContainers = 0;
								while ((i < lineOrderContainerDataSet.Tables[0].Rows.Count) && (noOfUpdatedContainers < scaleEntry.noOfContainers-1))
								{
									LineOrderContainer lineOrderContainer2 = new LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);
									if (lineOrderContainer2.containerNo != scaleEntry.containerNo)
									{
										lineOrderContainer2.isScaled = true;
										lineOrderContainer2.scaledDateTime = scaleEntry.entryDateTime;
										lineOrderContainer2.save(database);
										noOfUpdatedContainers++;

									}
									
									i++;
								}

								if (noOfUpdatedContainers < scaleEntry.noOfContainers-1)
								{
									//Match with containers on same linejournal

									LineOrders lineOrders = new LineOrders();
									LineOrder lineOrder = lineOrders.getEntry(database, lineOrderEntryNo.ToString());
									if (lineOrder != null)
									{
									
										lineOrderContainerDataSet = lineOrderContainers.getJournalDataSet(database, lineOrder.lineJournalEntryNo);
										i = 0;
										noOfUpdatedContainers = 0;
										while ((i < lineOrderContainerDataSet.Tables[0].Rows.Count) && (noOfUpdatedContainers < scaleEntry.noOfContainers-1))
										{
											LineOrderContainer lineOrderContainer2 = new LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);
											if (lineOrderContainer2.containerNo != scaleEntry.containerNo)
											{
												if (lineOrderContainer2.isScaled == false)
												{
													lineOrderContainer2.isScaled = true;
													lineOrderContainer2.scaledDateTime = scaleEntry.entryDateTime;
													lineOrderContainer2.save(database);
													noOfUpdatedContainers++;
												}
											}
									
											i++;
										}

									}
								}
							}
						}
					}
				}				

				// Temporary solution for output weights
				if (scaleEntry.type == 1)
				{
					// Create output inventory entry
					if (scaleEntry.status > 1)
					{
						FactoryInventoryEntry factoryInventoryEntry = new FactoryInventoryEntry();
						factoryInventoryEntry.factoryNo = scaleEntry.factoryCode;
						factoryInventoryEntry.type = 1;
						factoryInventoryEntry.status = 1;
						factoryInventoryEntry.date = scaleEntry.entryDateTime;
						factoryInventoryEntry.timeOfDay = new DateTime(1754, 1, 1, scaleEntry.entryDateTime.Hour, 0, 0);
						factoryInventoryEntry.weight = scaleEntry.weight* 1000;
						if (scaleEntry.status > 7) factoryInventoryEntry.weight = factoryInventoryEntry.weight * -1;
						factoryInventoryEntry.save(database);
					}
				}

			}
			catch(Exception e)
			{
				serverLogging.log(factoryCode, "[Scale] DateTime: "+transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString()+", Weight: "+transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()+", Containers: "+transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()+", Status: "+transactionEntryDataSet.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
				serverLogging.log(factoryCode, "[Scale] Error: "+e.Message);
				throw new Exception(e.Message);
			}
		}
	


		public DataSet getScaleEntries(string factoryCode, int status)
		{
			ScaleEntries scaleEntries = new ScaleEntries();
			DataSet dataSet = scaleEntries.getDataSet(database, factoryCode, status);

			return dataSet;

		}

		public string[] getMissingScaleEntries(string factoryCode)
		{
			ArrayList missingList = new ArrayList();
			
			ScaleEntries scaleEntries = new ScaleEntries();
			DataSet firstDataSet = scaleEntries.getTransactionsFromDate(database, factoryCode, DateTime.Today.AddMonths(-1));
			if (firstDataSet.Tables[0].Rows.Count > 0)
			{
				missingList = scaleEntries.findMissingEntries(database, factoryCode, int.Parse(firstDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()));
			}

			string[] array = new string[missingList.Count];
			int i = 0;
			while (i < missingList.Count)
			{
				array[i] = missingList[i].ToString();
				i++;
			}

			return array;
		}

		public string[] getUnfinishedScaleEntries(string factoryCode)
		{
		
			ScaleEntries scaleEntries = new ScaleEntries();
			DataSet firstDataSet = scaleEntries.getDataSet(database, factoryCode, 1);

			if (firstDataSet.Tables[0].Rows.Count > 0)
			{
				string[] array = new string[firstDataSet.Tables[0].Rows.Count];

				int i = 0;
				while (i < firstDataSet.Tables[0].Rows.Count)
				{
					array[i] = firstDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
					i++;
				}

				return array;
			}

			return null;
		}

		public void dispose()
		{
			database.close();
		}


		#region Logger Members

		public void write(string message, int type)
		{
			// TODO:  Add ScaleRunner.write implementation
		}

		#endregion
	}
}
