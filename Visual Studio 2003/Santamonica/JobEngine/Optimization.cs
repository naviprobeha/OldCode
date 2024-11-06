using System;
using System.Collections;
using System.Data;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.JobEngine

{
	/// <summary>
	/// Summary description for Optimization.
	/// </summary>
	public class Optimization
	{
		private Database database;
		private Configuration configuration;
		private Logger logger;
		private ArrayList routeList;
		private ArrayList orderList;
		private Hashtable routeGroupList;
		private ArrayList optimizedRouteList;

		public Optimization(Database database, Configuration configuration, Logger logger)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.logger = logger;
			this.configuration = configuration;
			this.routeList = new ArrayList();

		}

		public void optimizeSingleRoute(LineJournal lineJournal)
		{

			bool success = true;

			log("Optimizing route: "+lineJournal.entryNo, 0);

			lineJournal.status = 2;
			lineJournal.save(database);

			if (this.checkLineJournalOrderGeographics(lineJournal))
			{
				if (createSingleRouteFile(lineJournal))
				{
					log("Route file created.", 0);

					System.IO.FileInfo returnFile = new System.IO.FileInfo(configuration.planReturnFilePath+"\\userout.csv");
					if (returnFile.Exists) returnFile.Delete();

					System.Diagnostics.Process process = System.Diagnostics.Process.Start(configuration.planExePath, "/F\""+configuration.planReturnFilePath+"\\orders.csv\" /h /s /x\""+configuration.planReturnFilePath+"\"");
					process.WaitForExit();

					returnFile = new System.IO.FileInfo(configuration.planReturnFilePath+"\\userout.csv");
					if (returnFile.Exists) 
					{
						log("Return file is created.", 0);
						if (!importSingleRouteFile(lineJournal)) success = false;
					}

				}
			}
			else
			{
				success = true;
			}

			LineJournals lineJournals = new LineJournals();
			lineJournal = lineJournals.getEntry(database, lineJournal.entryNo.ToString());

			if (success)
			{
				if (lineJournal.status < 7)
				{
					lineJournal.status = 3;
					lineJournal.save(database);
					
					if (!lineJournal.isReadyToSend(database)) lineJournal.removeJournal(database);
				}
			}
			else
			{
				if (lineJournal.status < 7)
				{
					lineJournal.status = 1;
					lineJournal.save(database);
				}
			}

		}

		public void optimizeMultiRoute(LineJournal lineJournal)
		{
			bool success = true;

			log("Optimizing organization: "+lineJournal.organizationNo, 0);

			bool containsLoadedOrders;

			composeListOfLineOrdersPerOrganization(lineJournal);

			if (createMultiRouteFile(lineJournal, out containsLoadedOrders))
			{
				log("Route file created.", 0);

				System.IO.FileInfo returnFile = new System.IO.FileInfo(configuration.planReturnFilePath+"\\userout.csv");
				if (returnFile.Exists) returnFile.Delete();

				/*
				if (containsLoadedOrders)
				{
					log("Optimizing loaded orders.", 0);
					setRunType(2);
					System.Diagnostics.Process process = System.Diagnostics.Process.Start(configuration.planExePath, "/F\""+configuration.planReturnFilePath+"\\orders.csv\" /h /s /x\""+configuration.planReturnFilePath+"\"");
					process.WaitForExit();
					log("Optimizing non-loaded orders.", 0);
					setRunType(4);
					process = System.Diagnostics.Process.Start(configuration.planExePath, "/h /s /x\""+configuration.planReturnFilePath+"\"");
					process.WaitForExit();
				}
				else
				{
					setRunType(1);
					System.Diagnostics.Process process = System.Diagnostics.Process.Start(configuration.planExePath, "/F\""+configuration.planReturnFilePath+"\\orders.csv\" /h /s /x\""+configuration.planReturnFilePath+"\"");
					process.WaitForExit();
				}
				*/

				setRunType(1);
				System.Diagnostics.Process process = System.Diagnostics.Process.Start(configuration.planExePath, "/F\""+configuration.planReturnFilePath+"\\orders.csv\" /h /s /x\""+configuration.planReturnFilePath+"\"");
				process.WaitForExit();

				returnFile = new System.IO.FileInfo(configuration.planReturnFilePath+"\\userout.csv");
				if (returnFile.Exists) 
				{
					log("Return file is created.", 0);
					if (!importMultiRouteFile()) success = false;
				}

			}

			if (success)
			{
				setLineJournalStatusToOptimized();

				log("Organization "+lineJournal.organizationNo+" optimized.", 0);
			}

		}


		private bool createSingleRouteFile(LineJournal lineJournal)
		{

			if (checkLineJournalOrderGeographics(lineJournal))
			{
				Factories factories = new Factories();
				Factory departureFactory = factories.getEntry(database, lineJournal.departureFactoryCode);
				Factory arrivalFactory = factories.getEntry(database, lineJournal.arrivalFactoryCode);

				GeoCoding geoCoding = new GeoCoding("rt90_2.5_gon_v");

				if ((departureFactory != null) && (arrivalFactory != null))
				{
					System.IO.TextWriter textWriter = System.IO.File.CreateText(configuration.planReturnFilePath+"\\orders.csv");
					textWriter.WriteLine("type;action;callref;ordref;name;cd;prod1;prod2;address_1;depname;prefdep;vehname;group;vehcap1;vehcap2;shift;sleeper");
					textWriter.WriteLine("I;D;;;;;;;;;;********;;;;;"); // Deletes all vehicles

					/*
					AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
					Agents agents = new Agents();
					DataSet agentDataSet = agents.getDataSet(database, lineJournal.organizationNo, Agents.TYPE_LINE);

					int z = 0;
					ArrayList arrayList = new ArrayList();

					while (z < agentDataSet.Tables[0].Rows.Count)
					{
						Agent agent = new Agent(agentDataSet.Tables[0].Rows[z]);
						
						if (!arrayList.Contains(agent.agentStorageGroup))
						{
							textWriter.WriteLine("I;A;;;;;;;;;;"+agent.agentStorageGroup+";"+agent.agentStorageGroup+";"+agent.getNoOfContainers(database)+";"+agent.getVolumeStorage(database)+";Dagsskift;Y");				
							arrayList.Add(agent.agentStorageGroup);
						}

						z++;
					}
					*/

					textWriter.WriteLine("I;A;;;;;;;;;;A;A;8;50;Dagsskift;Y");				

					textWriter.WriteLine(";D;;********;;;;;;;;;;;;;"); // Deletes all orders

					double[] latLon = geoCoding.GetWGS84(departureFactory.positionY, departureFactory.positionX);
					textWriter.WriteLine("D;;;;"+lineJournal.departureFactoryCode.Replace(",", " ")+";C;;"+latLon[0].ToString().Replace(",",".").Substring(0, 7)+" "+latLon[1].ToString().Replace(",",".").Substring(0, 7)+";"+lineJournal.departureFactoryCode.Replace(",", " ")+";;;;;;;");

					LineOrders lineOrders = new LineOrders();
					DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo);

					int i = 0;
					while (i < lineOrderDataSet.Tables[0].Rows.Count)
					{
						LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

						int noOfContainers = lineOrder.countContainers(database, 0);
						int volumeStorage = lineOrder.countContainers(database, 1);

						if (noOfContainers == 0) noOfContainers = 1;

						latLon = geoCoding.GetWGS84(lineOrder.positionY, lineOrder.positionX);
						textWriter.WriteLine("A;A;"+lineOrder.shippingCustomerNo+";;"+washText(lineOrder.shippingCustomerName)+";;;;"+latLon[0].ToString().Replace(",",".").Substring(0, 7)+" "+latLon[1].ToString().Replace(",",".").Substring(0, 7)+";;;;;;;;");
						textWriter.WriteLine("T;A;"+lineOrder.shippingCustomerNo+";"+lineOrder.entryNo+";"+washText(lineOrder.shippingCustomerName)+";C;"+noOfContainers+";"+volumeStorage+";;"+arrivalFactory.no+";"+arrivalFactory.no+";;;;;");

						i++;
					}

					latLon = geoCoding.GetWGS84(arrivalFactory.positionY, arrivalFactory.positionX);
					textWriter.WriteLine("D;;;;"+lineJournal.arrivalFactoryCode.Replace(",", " ")+";C;;"+latLon[0].ToString().Replace(",",".").Substring(0, 7)+" "+latLon[1].ToString().Replace(",",".").Substring(0, 7)+";"+lineJournal.arrivalFactoryCode.Replace(",", " ")+";;;;;;;");

					textWriter.Flush();
					textWriter.Close();

					return true;
				}
				else
				{
					log("Factories are invalid on journal: "+lineJournal.entryNo, 2);
				}
			}

			return true;

		}


		private bool createMultiRouteFile(LineJournal originalLineJournal, out bool containsLoadedOrders)
		{
			containsLoadedOrders = false;
			
			routeList = new ArrayList();
			optimizedRouteList = new ArrayList();

			LineJournals lineJournals = new LineJournals();
			DataSet lineJournalDataSet = lineJournals.getActiveDataSet(database, "", originalLineJournal.organizationNo, originalLineJournal.shipDate, originalLineJournal.shipDate);

			System.IO.TextWriter textWriter = System.IO.File.CreateText(configuration.planReturnFilePath+"\\orders.csv");
			textWriter.WriteLine("type;action;callref;ordref;name;cd;prod1;prod2;address_1;depname;prefdep;group;priority;vehicle;zone;vehname;group;vehcap1;vehcap2;shift;sleepercab");
			
			textWriter.WriteLine("I;D;;;;;;;;;;;;;;********;;;;;"); // Deletes all vehicles

			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();
			Agents agents = new Agents();
			DataSet agentDataSet = agents.getPlanningDataSet(database, originalLineJournal.organizationNo);

			int z = 0;
			ArrayList arrayList = new ArrayList();

			while (z < agentDataSet.Tables[0].Rows.Count)
			{
				Agent agent = new Agent(agentDataSet.Tables[0].Rows[z]);
						
				if (!arrayList.Contains(agent.agentStorageGroup))
				{
					textWriter.WriteLine("I;A;;;;;;;;;;;;;;"+agent.agentStorageGroup+";"+agent.agentStorageGroup+";"+agent.getNoOfContainers(database)+";"+agent.getVolumeStorage(database)+";Dagsskift;Y");				
					arrayList.Add(agent.agentStorageGroup);
				}

				z++;
			}		
			
			
			textWriter.WriteLine(";D;;********;;;;;;;;;;;;;;;;;;"); // Deletes all orders

			int count = 0;
			bool ordersExported = false;

			while ((count < lineJournalDataSet.Tables[0].Rows.Count) && (count < 9))
			{
				LineJournal lineJournal = new LineJournal(lineJournalDataSet.Tables[0].Rows[count]);

				if ((lineJournal.status < 7) && (!lineJournal.forcedAssignment))
				{
					//if (!lineJournal.allOrdersLoaded(database))
					if (lineJournal.countOrdersLoaded(database) == 0)
					{
						if (checkLineJournalOrderGeographics(lineJournal))
						{
							log("Geographics for journal "+lineJournal.entryNo+" checked OK.", 0);

							lineJournal.status = 2;
							lineJournal.save(database);

							Factories factories = new Factories();
							Factory departureFactory = factories.getEntry(database, lineJournal.departureFactoryCode);
							Factory arrivalFactory = factories.getEntry(database, lineJournal.arrivalFactoryCode);

							GeoCoding geoCoding = new GeoCoding("rt90_2.5_gon_v");

							if ((departureFactory != null) && (arrivalFactory != null))
							{
								string groupCode = addLineJournalRoute(lineJournal.entryNo);
						
								double[] latLon = geoCoding.GetWGS84(departureFactory.positionY, departureFactory.positionX);
								textWriter.WriteLine("D;;;;"+lineJournal.departureFactoryCode.Replace(",", " ")+";C;;"+latLon[0].ToString().Replace(",",".").Substring(0, 7)+" "+latLon[1].ToString().Replace(",",".").Substring(0, 7)+";"+lineJournal.departureFactoryCode.Replace(",", " ")+";;;;;;;;;;;");

								LineOrders lineOrders = new LineOrders();
								DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo);

								int i = 0;
								while (i < lineOrderDataSet.Tables[0].Rows.Count)
								{
									LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
									lineOrder.planningAgentCode = lineJournal.agentCode;
									lineOrder.save(database, false);

									string vehicleCode = lineOrder.getPreferredAgentStorageGroup(database); //lineJournal.agentStorageGroup;


									if (lineOrder.priority == 0) lineOrder.priority = 9;

									string lineOrderGroup = "";
									if (lineOrder.routeGroupCode != "") 
									{
										lineOrderGroup = this.translateRouteGroupCode(lineOrder.routeGroupCode, groupCode);
									}
									if (lineOrder.status > 6)
									{
										lineOrderGroup = groupCode; 
										containsLoadedOrders = true;
									}

									int noOfContainers = lineOrder.countContainers(database, 0);
									int volumeStorage = lineOrder.countContainers(database, 1);

									string preferedFactoryNo = lineJournal.arrivalFactoryCode;
									if (lineOrder.arrivalFactoryCode != "") preferedFactoryNo = lineOrder.arrivalFactoryCode;

									latLon = geoCoding.GetWGS84(lineOrder.positionY, lineOrder.positionX);
									textWriter.WriteLine("A;A;"+lineOrder.shippingCustomerNo+";;"+washText(lineOrder.shippingCustomerName)+";;;;"+latLon[0].ToString().Replace(",",".").Substring(0, 7)+" "+latLon[1].ToString().Replace(",",".").Substring(0, 7)+";;;;;;;;;;;;");
									textWriter.WriteLine("T;A;"+lineOrder.shippingCustomerNo+";"+lineOrder.entryNo+";"+washText(lineOrder.shippingCustomerName)+";C;"+noOfContainers+";"+volumeStorage+";;"+preferedFactoryNo+";"+preferedFactoryNo+";"+lineOrderGroup+";"+lineOrder.priority.ToString()+";"+vehicleCode+";"+lineOrderGroup+";;;;;;");

									i++;
								}

								latLon = geoCoding.GetWGS84(arrivalFactory.positionY, arrivalFactory.positionX);
								textWriter.WriteLine("D;;;;"+lineJournal.arrivalFactoryCode.Replace(",", " ")+";C;;"+latLon[0].ToString().Replace(",",".").Substring(0, 7)+" "+latLon[1].ToString().Replace(",",".").Substring(0, 7)+";"+lineJournal.arrivalFactoryCode.Replace(",", " ")+";;;;;;;;;;;");

								ordersExported = true;
							}
							else
							{
								log("Factories are invalid on journal: "+lineJournal.entryNo, 2);
							}
						}
						else
						{
							lineJournal.status = 3;
							lineJournal.save(database);
						}
					}
					else
					{
						lineJournal.status = 3;
						lineJournal.save(database);
					}
				}

				count++;

			}

			textWriter.Flush();
			textWriter.Close();

			if (ordersExported)	return true;

			return false;

		}



		private bool importSingleRouteFile(LineJournal lineJournal)
		{
			System.IO.TextReader textReader = System.IO.File.OpenText(configuration.planReturnFilePath+"\\userout.csv");
			textReader.ReadLine();
			textReader.ReadLine();

			LineOrders lineOrders = new LineOrders();

			LineJournals lineJournals = new LineJournals();
			lineJournal = lineJournals.getEntry(database, lineJournal.entryNo.ToString());

			if (lineJournal.status != 2)
			{
				log("Line Journal status has changed. Re-optimization required.", 0);
				textReader.Close();
				return false;
			}

			string textLine = textReader.ReadLine();
			while(textLine != null)
			{

				if (textLine[0] == '1')
				{
					DataRoute dataRoute = new DataRoute(textLine);
				}
				if (textLine[0] == '3')
				{
					DataOrder dataOrder = new DataOrder(textLine);

					if (dataOrder.orderNumber != "")
					{
						LineOrder lineOrder = lineOrders.getEntry(database, dataOrder.orderNumber);
						if (lineOrder.lineJournalEntryNo == lineJournal.entryNo)
						{
							if (lineOrder.isLoaded)
							{
								log("Line Order status has changed. Re-optimization required.", 0);
								textReader.Close();

								lineJournal.status = 2;
								lineJournal.save(database);

								return false;
							}
							lineOrder.optimizingSortOrder = dataOrder.sequence;
							lineOrder.travelDistance = dataOrder.travelDistance;
							lineOrder.travelTime = dataOrder.travelTime;
							if (lineOrder.status < 7) lineOrder.status = 3;
							lineOrder.planningAgentCode = lineJournal.agentCode;
							lineOrder.save(database, true);
						}
					}
					else
					{
						if (dataOrder.sequence > 1)
						{
							lineJournal = lineJournals.getEntry(database, lineJournal.entryNo.ToString());
							if (lineJournal.status == 2)
							{
								lineJournal.calculatedDistance = dataOrder.totalDistance;
								lineJournal.endingTravelDistance = dataOrder.travelDistance;
								lineJournal.endingTravelTime = dataOrder.travelTime;
								lineJournal.totalDistance = dataOrder.totalDistance;
								lineJournal.totalTime = dataOrder.totalTime;						
								lineJournal.save(database);

								lineJournal.applyLineOrderAgent(database);
								lineJournal.recalculateArrivalTime(database);
							}
						}
					}
				}

				textLine = textReader.ReadLine();

			}

			textReader.Close();

			
			return true;
		}


		private bool importMultiRouteFile()
		{
			System.IO.TextReader textReader = System.IO.File.OpenText(configuration.planReturnFilePath+"\\userout.csv");
			textReader.ReadLine();
			textReader.ReadLine();

			LineOrders lineOrders = new LineOrders();

			if (checkLineOrderAndJournalStatus())
			{
				PlannedRoute plannedRoute = null;

				ArrayList plannedRouteList = new ArrayList();

				string textLine = textReader.ReadLine();

				DataRoute dataRoute = null;

				while(textLine != null)
				{
					if (textLine[0] == '1')
					{
						dataRoute = new DataRoute(textLine);					
						plannedRoute = new PlannedRoute();
						plannedRoute.arrivalFactoryNo = dataRoute.arrivalFactoryCode;
					}
					if (textLine[0] == '3')
					{
						DataOrder dataOrder = new DataOrder(textLine);
						if (dataOrder.orderNumber != "")
						{
							LineOrder lineOrder = lineOrders.getEntry(database, dataOrder.orderNumber);

							log("Read order: "+lineOrder.entryNo, 0);

							if (lineOrder.lineJournalEntryNo > 0)
							{

								//if (lineOrder.lineJournalEntryNo != lineJournal.entryNo) lineOrder.unassignToRoute(database);
								//lineOrder.lineJournalEntryNo = lineJournal.entryNo;
								lineOrder.optimizingSortOrder = dataOrder.sequence;
								lineOrder.travelDistance = dataOrder.travelDistance;
								lineOrder.travelTime = dataOrder.travelTime;
								if (lineOrder.status > 6) 
								{
									plannedRoute.lineJournalEntryNo = lineOrder.lineJournalEntryNo;
								}
								else
								{
									lineOrder.status = 3;
								}
								lineOrder.save(database, true);
								plannedRoute.addOrder(lineOrder);
							}
						}
						else
						{
							if (dataOrder.sequence > 1)
							{
								if (dataOrder.customerName != "")
								{
									plannedRoute.calculatedDistance = dataOrder.totalDistance;
									plannedRoute.endingDistance = dataOrder.travelDistance;
									plannedRoute.endingTime = dataOrder.travelTime;
									plannedRoute.totalDistance = dataOrder.totalDistance;
									plannedRoute.totalTime = dataOrder.totalTime;
									
									if (dataRoute != null) plannedRoute.agentStorageGroup = dataRoute.agentCode;
						
									plannedRouteList.Add(plannedRoute);

									/*
									LineJournals lineJournals = new LineJournals();
									lineJournal = lineJournals.getEntry(database, lineJournal.entryNo.ToString());
									if (lineJournal.status == 2)
									{
										lineJournal.calculatedDistance = dataOrder.totalDistance;
										lineJournal.endingTravelDistance = dataOrder.travelDistance;
										lineJournal.endingTravelTime = dataOrder.travelTime;
										lineJournal.totalDistance = dataOrder.totalDistance;
										lineJournal.totalTime = dataOrder.totalTime;
										lineJournal.save(database);

										lineJournal.recalculateArrivalTime(database);

										if (lineJournal.countContainers(database) < lineJournal.qtyAvailableContainers) 
										{
											log("Removing linejournal from "+lineJournal.agentCode, 0);
											lineJournal.removeJournal(database);
										}
									}
									*/
								}
							}
						}
					}

					textLine = textReader.ReadLine();
				}

			

				// Process data and update routes.
				// Partially shipped routes.

				int k = 0;
				while (k < plannedRouteList.Count)
				{
					plannedRoute = (PlannedRoute)plannedRouteList[k];
					if (plannedRoute.lineJournalEntryNo > 0)
					{
						log("Update of partially loaded route: "+plannedRoute.lineJournalEntryNo, 0);
						LineJournals lineJournals = new LineJournals();
						LineJournal lineJournal = lineJournals.getEntry(database, plannedRoute.lineJournalEntryNo.ToString());
						if (lineJournal != null)
						{
							lineJournal.calculatedDistance = plannedRoute.totalDistance;
							lineJournal.endingTravelDistance = plannedRoute.endingDistance;
							lineJournal.endingTravelTime = plannedRoute.endingTime;
							lineJournal.totalDistance = plannedRoute.totalDistance;
							lineJournal.totalTime = plannedRoute.totalTime;

							lineJournal.arrivalFactoryCode = plannedRoute.arrivalFactoryNo;
							lineJournal.departureFactoryCode = plannedRoute.arrivalFactoryNo;

							lineJournal.save(database);
							
							lineJournal.recalculateArrivalTime(database);
							if (!lineJournal.isReadyToSend(database)) lineJournal.removeJournal(database);


							int l = 0;
							while (l < plannedRoute.orders.Count)
							{
								LineOrder lineOrder = (LineOrder)plannedRoute.orders[l];
								log("LineOrder: "+lineOrder.entryNo, 0);
								
								lineOrder.moveToRoute(database, lineJournal.entryNo);

								l++;
							}
							
							this.routeList.Remove(lineJournal.entryNo.ToString());
						}
						else
						{
							log("Fatal error in data update. Re-optimization required.", 0);
							textReader.Close();
							return false;
						}

					}

					k++;
				}

				// All other routes.

				k = 0;
				while (k < plannedRouteList.Count)
				{
					plannedRoute = (PlannedRoute)plannedRouteList[k];

					if (plannedRoute.lineJournalEntryNo == 0)
					{
						LineJournals lineJournals = new LineJournals();
						log("No of routes left to plan: "+routeList.Count, 0);

						if (routeList.Count > 0)
						{
							string lineJournalEntryNo = (string)this.routeList[0];
							log("Update of non-loaded route: "+lineJournalEntryNo, 0);
							LineJournal lineJournal = lineJournals.getEntry(database, lineJournalEntryNo);
							if (lineJournal != null)
							{

								lineJournal.calculatedDistance = plannedRoute.totalDistance;
								lineJournal.endingTravelDistance = plannedRoute.endingDistance;
								lineJournal.endingTravelTime = plannedRoute.endingTime;
								lineJournal.totalDistance = plannedRoute.totalDistance;
								lineJournal.totalTime = plannedRoute.totalTime;

								lineJournal.arrivalFactoryCode = plannedRoute.arrivalFactoryNo;
								lineJournal.departureFactoryCode = plannedRoute.arrivalFactoryNo;

								lineJournal.save(database);

								lineJournal.recalculateArrivalTime(database);
								if (!lineJournal.isReadyToSend(database)) lineJournal.removeJournal(database);

								int l = 0;
								while (l < plannedRoute.orders.Count)
								{
									LineOrder lineOrder = (LineOrder)plannedRoute.orders[l];
									log("LineOrder: "+lineOrder.entryNo, 0);
								
									lineOrder.moveToRoute(database, lineJournal.entryNo);

									l++;
								}
						
								this.routeList.Remove(lineJournal.entryNo.ToString());

								lineJournal.applyLineOrderAgent(database);
							}
							else
							{
								log("Fatal error in data update. Re-optimization required.", 0);
								textReader.Close();
								return false;
							}
						}
						else
						{
							//Create new route

							LineJournal lineJournal = null;
							
							log("Create new journal", 0);

							int l = 0;
							while (l < plannedRoute.orders.Count)
							{
								LineOrder lineOrder = (LineOrder)plannedRoute.orders[l];
								log("LineOrder: "+lineOrder.entryNo, 0);
								
								if (lineJournal == null)
								{
									/*
									Agents agents = new Agents();
									Agent choosenAgent = agents.findAgentForLineOrder(database, lineOrder, lineOrder.shipDate);
									if (choosenAgent != null)
									{
										lineJournal = lineJournals.createJournal(database, choosenAgent.code, lineOrder.shipDate);

										lineJournal.calculatedDistance = plannedRoute.totalDistance;
										lineJournal.endingTravelDistance = plannedRoute.endingDistance;
										lineJournal.endingTravelTime = plannedRoute.endingTime;
										lineJournal.totalDistance = plannedRoute.totalDistance;
										lineJournal.totalTime = plannedRoute.totalTime;
										lineJournal.save(database);
									}
									*/

									log("The creating moment.", 0);
									log("Organization: "+lineOrder.organizationNo, 0);
									log("Storage Group: "+plannedRoute.agentStorageGroup, 0);

									Organizations organizations = new Organizations();
									Organization organization = organizations.getOrganization(database, lineOrder.organizationNo);
									if (organization != null)
									{
									
										lineJournal = lineJournals.createJournal(database, organization, lineOrder.shipDate);
										log("Journal created... Applying values.", 0);
										lineJournal.calculatedDistance = plannedRoute.totalDistance;
										lineJournal.endingTravelDistance = plannedRoute.endingDistance;
										lineJournal.endingTravelTime = plannedRoute.endingTime;
										lineJournal.totalDistance = plannedRoute.totalDistance;
										lineJournal.totalTime = plannedRoute.totalTime;

										lineJournal.arrivalFactoryCode = plannedRoute.arrivalFactoryNo;
										lineJournal.departureFactoryCode = plannedRoute.arrivalFactoryNo;

										lineJournal.agentStorageGroup = plannedRoute.agentStorageGroup;

										lineJournal.save(database);
									}

									log("Journal created.", 0);
								}
								if (lineJournal != null)
								{
									log("Finishing up 1", 0);
									lineOrder.moveToRoute(database, lineJournal.entryNo);
									log("Finishing up 2", 0);
									lineJournal.recalculateArrivalTime(database);
								}

								l++;
							}

							log("Removing...", 0);
							this.routeList.Remove(lineJournal.entryNo.ToString());
							
							if (lineJournal != null)
							{
								log("Applying agents...", 0);
								lineJournal.applyLineOrderAgent(database);
							}
						}

					}

					k++;
				}

				log("Update done.", 0);

			}
			else
			{
				log("Line Journal status has changed. Re-optimization required.", 0);
				textReader.Close();
				return false;
			}

			textReader.Close();

			return true;
		}

		private bool checkLineOrderAndJournalStatus()
		{
			bool changedLineJournals = false;

			int i = 0;
			while (i < this.routeList.Count)
			{
				LineJournals lineJournals = new LineJournals();
				LineJournal lineJournal = lineJournals.getEntry(database, routeList[i].ToString());

				if (lineJournal != null)
				{
					if (lineJournal.status != 2) changedLineJournals = true;
					if (lineJournal.forcedAssignment) changedLineJournals = true;
				}
				else
				{
					changedLineJournals = true;
				}

				i++;
			}

			if (changedLineJournals)
			{
				i = 0;
				while (i < this.routeList.Count)
				{
					LineJournals lineJournals = new LineJournals();
					LineJournal lineJournal = lineJournals.getEntry(database, routeList[i].ToString());

					if (lineJournal != null)
					{
						if (lineJournal.status < 6) 
						{
							lineJournal.status = 1;
							lineJournal.save(database);
						}
					}

					i++;
				}

				return false;
			}
			else
			{
				int j = 0;
				while (j < orderList.Count)
				{
					
					LineOrders lineOrders = new LineOrders();
					LineOrder lineOrder = lineOrders.getEntry(database, ((int)orderList[j]).ToString());
					if (lineOrder != null)
					{
						LineJournal lineJournal = lineOrder.getJournal(database);
						if (lineJournal != null)
						{
							if (lineJournal.forcedAssignment) return false;
						}
						else
						{
							return false;
						}
					}
					else
					{
						return false;
					}
					j++;
				}
				return true;
			}

		}



		private bool checkLineJournalOrderGeographics(LineJournal lineJournal)
		{
			Factories factories = new Factories();
			Factory departureFactory = factories.getEntry(database, lineJournal.departureFactoryCode);
			if (departureFactory == null) return false;
			if ((departureFactory.positionX == 0) || (departureFactory.positionY == 0)) return false;

			if (lineJournal.departureFactoryCode != lineJournal.arrivalFactoryCode)
			{
				Factory arrivalFactory = factories.getEntry(database, lineJournal.arrivalFactoryCode);
				if (arrivalFactory == null) return false;
				if ((arrivalFactory.positionX == 0) || (arrivalFactory.positionY == 0)) return false;


			}

			LineOrders lineOrders = new LineOrders();
			DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo);
			int i = 0;
			while(i < lineOrderDataSet.Tables[0].Rows.Count)
			{
				LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);

				if ((lineOrder.positionX == 0) || (lineOrder.positionY == 0)) return false;

				i++;
			}

			return true;
		}

		private void log(string message, int type)
		{
			logger.write("[Optimization] "+message, type);
		}

		private string washText(string text)
		{
			text = text.Replace(",", "");
			text = text.Replace("Å", "A");
			text = text.Replace("Ä", "A");
			text = text.Replace("Ö", "O");
			text = text.Replace("å", "a");
			text = text.Replace("ä", "a");
			text = text.Replace("ö", "o");
			text = text.Replace("é", "e");
			text = text.Replace("É", "E");

			return text;
		}


		private string addLineJournalRoute(int lineJournalEntryNo)
		{
			string groups = "123456789";

			routeList.Add(lineJournalEntryNo.ToString());
			optimizedRouteList.Add(lineJournalEntryNo.ToString());
			return groups[routeList.IndexOf(lineJournalEntryNo.ToString())].ToString();
		}

		private string translateRouteGroupCode(string routeGroupCode, string group)
		{
			if (this.routeGroupList == null) routeGroupList = new Hashtable();
			
			if (routeGroupList[routeGroupCode] == null) routeGroupList.Add(routeGroupCode, group);

			return routeGroupList[routeGroupCode].ToString();

		}
		
		private int getLineJournalRoute(string groupCode)
		{
			string groups = "123456789";

			return int.Parse(routeList[groups.IndexOf(groupCode[1])].ToString());
		}


		private void setLineJournalStatusToOptimized()
		{
			LineJournals lineJournals = new LineJournals();
			
			if (optimizedRouteList != null)
			{
				int i = 0;
				while (i < this.optimizedRouteList.Count)
				{
					LineJournal lineJournal = lineJournals.getEntry(database, optimizedRouteList[i].ToString());
					lineJournal.status = 3;
					lineJournal.save(database);

					i++;
				}
			}

		}


		public void createCustomerFile()
		{

			log("Exporting customers.", 0);

			System.IO.TextWriter textWriter = System.IO.File.CreateText(configuration.planReturnFilePath+"\\customers.csv");
			textWriter.WriteLine("type;action;callref;ordref;name;cd;prod1;address_1;depname;prefdep;group;priority;vehicle");
			textWriter.WriteLine(";D;;********;;;;;;;;;"); // Deletes all orders

			ShippingCustomers shippingCustomers = new ShippingCustomers();
			DataSet shippingCustomerDataSet = shippingCustomers.getDataSet(database);

			int count = 0;
			
			GeoCoding geoCoding = new GeoCoding("rt90_2.5_gon_v");

			while (count < shippingCustomerDataSet.Tables[0].Rows.Count)
			{
				ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, shippingCustomerDataSet.Tables[0].Rows[count].ItemArray.GetValue(0).ToString());

				if ((shippingCustomer.positionX > 0) && (shippingCustomer.positionY > 0))
				{

					double[] latLon = geoCoding.GetWGS84(shippingCustomer.positionY, shippingCustomer.positionX);
					textWriter.WriteLine("A;A;"+shippingCustomer.no+";;"+washText(shippingCustomer.name)+";;;"+latLon[0].ToString().Replace(",",".").Substring(0, 7)+" "+latLon[1].ToString().Replace(",",".").Substring(0, 7)+";;;;;");

				}
				count++;
			}

			textWriter.Flush();
			textWriter.Close();

			log("Importing to PlanLogix.", 0);

			System.Diagnostics.Process process = System.Diagnostics.Process.Start(configuration.planExePath, "/F\""+configuration.planReturnFilePath+"\\customers.csv\" /h /x\""+configuration.planReturnFilePath+"\"");
			process.WaitForExit();


			log("Export done.", 0);
		}

				
		private void setRunType(int type)
		{
			log("Configuring PlanLogix.", 0);

			ArrayList arrayList = new ArrayList();
			string currentGroup = "";
			string command = "";

			System.IO.TextReader textReader = System.IO.File.OpenText(configuration.planReturnFilePath+"\\LogixIE.ini");

			string data = textReader.ReadLine();
			while (data != null)
			{
				if (data != "")
				{
					if (data[0] == '[')
					{
						currentGroup = data.Substring(1, data.Length-2);
					}
					else
					{
						command = "";
						if (data.IndexOf("=") > 0)
						{
							command = data.Substring(0, data.IndexOf("="));
						}
					}

					
					if ((currentGroup.ToUpper() == "AUTO_SCHEDULE") && (command.ToUpper() == "RUNTYPE"))
					{
						data = "RunType="+type;
					}
				}

				arrayList.Add(data);

				data = textReader.ReadLine();
			}
			
			textReader.Close();


			System.IO.TextWriter textWriter = System.IO.File.CreateText(configuration.planReturnFilePath+"\\LogixIE.ini");

			int i = 0;
			while (i < arrayList.Count)
			{
				textWriter.WriteLine(arrayList[i]);
				i++;
			}
			
			textWriter.Close();
			

			log("PlanLogix configured.", 0);
		}

		private void composeListOfLineOrdersPerOrganization(LineJournal originalLineJournal)
		{
			orderList = new ArrayList();

			LineJournals lineJournals = new LineJournals();
			DataSet lineJournalDataSet = lineJournals.getActiveDataSet(database, "", originalLineJournal.organizationNo, originalLineJournal.shipDate, originalLineJournal.shipDate);

			int count = 0;

			while (count < lineJournalDataSet.Tables[0].Rows.Count)
			{
				LineJournal lineJournal = new LineJournal(lineJournalDataSet.Tables[0].Rows[count]);

				if ((lineJournal.status < 7) && (!lineJournal.forcedAssignment))
				{

					LineOrders lineOrders = new LineOrders();
					DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo);

					int i = 0;
					while (i < lineOrderDataSet.Tables[0].Rows.Count)
					{
						LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
						orderList.Add(lineOrder.entryNo);
						i++;
					}

				}

				count++;

			}

		}

	}
}
