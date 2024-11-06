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
	public class LineOrderAssigner
	{
		private Logger logger;
		private Configuration configuration;

		private Thread thread;
		private bool running;

		public LineOrderAssigner(Logger logger, Configuration configuration)
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

			//Assign Line Orders to Organizations
			LineOrders lineOrders = new LineOrders();
			DataSet lineOrdersDataSet = lineOrders.getNewLineOrdersDataSet(database);
			int i = 0;
			while (i < lineOrdersDataSet.Tables[0].Rows.Count)
			{				
				LineOrder lineOrder = new LineOrder(lineOrdersDataSet.Tables[0].Rows[i]);

				if ((lineOrder.shipTime.ToString("HH:mm") != "00:00") && (lineOrder.enableAutoPlan))
				{
					assignLineOrderToOrganization(lineOrder, database);
				}

				i++;
			}

			
			//Assign Line Orders to Agents
			lineOrdersDataSet = lineOrders.getUnAssignedDataSet(database);
			i = 0;
			while (i < lineOrdersDataSet.Tables[0].Rows.Count)
			{
				LineOrder lineOrder = new LineOrder(lineOrdersDataSet.Tables[0].Rows[i]);

				if ((lineOrder.shipTime.ToString("HH:mm") != "00:00") && (lineOrder.enableAutoPlan))
				{
					assignLineOrderToAgent(lineOrder, database);
				}

				i++;
			}
			

			//Send to Agent
			LineJournals lineJournals = new LineJournals();
			DataSet lineJournalsDataSet = lineJournals.getStatusDataSet(database, 3);
			int k = 0;
			while (k < lineJournalsDataSet.Tables[0].Rows.Count)
			{
				LineJournal lineJournal = new LineJournal(lineJournalsDataSet.Tables[0].Rows[k]);
				sendToAgent(lineJournal, database);

				k++;
			}

			/*
			//Remove Smaller Routes
			lineJournalsDataSet = lineJournals.getStatusDataSet(database, 5);
			int l = 0;
			while (l < lineJournalsDataSet.Tables[0].Rows.Count)
			{
				LineJournal lineJournal = new LineJournal(lineJournalsDataSet.Tables[0].Rows[l]);
				removeSmallRoute(lineJournal);

				l++;
			}

			*/

			database.close();
		}


		private void assignLineOrderToAgent(LineOrder lineOrder, Database database)
		{
			// Tilldela linjeorder till bilar/körjournal (lineOrderJournal)


			DateTime shipDate = lineOrder.shipDate;
			if (shipDate < DateTime.Today) shipDate = DateTime.Today;

		
			Agents agents = new Agents();
			
			Agent choosenAgent = agents.findAgentForLineOrder(database, lineOrder, shipDate);

			if (choosenAgent != null)
			{
				
				log("Assigning lineorder "+lineOrder.entryNo, 0);
				log("Assigning to "+choosenAgent.code, 0);

			
				assignLineOrderToRoute(lineOrder, choosenAgent, shipDate, database);

					log("Assignment done.", 0);
			}
			

		}

		private void assignLineOrderToOrganization(LineOrder lineOrder, Database database)
		{
			// Tilldela linjeorder till transportör

			ShippingCustomerOrganizations shippingCustomerOrganizations = new ShippingCustomerOrganizations();
			DataSet shippingCustomerOrganizationDataSet = shippingCustomerOrganizations.getShippingCustomerDataSet(database, lineOrder.shippingCustomerNo, ShippingCustomerOrganizations.ORDER_TYPE_LINEORDER);

			log("Assigning lineorder "+lineOrder.entryNo+" to organization...", 0);

			if (shippingCustomerOrganizationDataSet.Tables[0].Rows.Count > 0)
			{
				int i = 0;
				string organizationNo = "";

				while ((i < shippingCustomerOrganizationDataSet.Tables[0].Rows.Count) && (organizationNo == ""))
				{
					ShippingCustomerOrganization shippingCustomerOrganization = new ShippingCustomerOrganization(shippingCustomerOrganizationDataSet.Tables[0].Rows[i]);
                    
					if (shippingCustomerOrganization.type == 0)
					{
						Organization organization = shippingCustomerOrganization.getOrganization(database);
						log("Trying organization "+organization.no, 0);

						if (organization.hasAvailableAgents(database, Agents.TYPE_LINE)) organizationNo = organization.no;
					}
					if (shippingCustomerOrganization.type == 1)
					{
						Agent agent = shippingCustomerOrganization.getAgent(database);

						if (directAssignmentOfLineOrderToAgent(lineOrder, agent, database)) return;
					}

					i++;
				}

				if (organizationNo != "")
				{
					lineOrder.organizationNo = organizationNo;
					lineOrder.save(database, false);
				}
			}
			
			log("Organization assignment done.", 0);
		}
		

		private void optimizeRoutes(LineJournal lineJournal, Database database)
		{	
			lineJournal.status = 2;
			lineJournal.save(database);

			System.Threading.Thread.Sleep(15000);
			
			lineJournal.status = 3;
			lineJournal.save(database);
			
		}

		private void sendToAgent(LineJournal lineJournal, Database database)
		{	
			if (lineJournal.isReadyToSend(database))
			{
				lineJournal.status = 4;
				lineJournal.save(database);

				lineJournal.setOrdersAssigned(database);
			}
		}
		

		private void removeSmallRoute(LineJournal lineJournal, Database database)
		{	
			Agents agents = new Agents();
			Agent agent = agents.getAgent(database, lineJournal.agentCode);

			if ((agent.getNoOfContainers(database) > lineJournal.countContainers(database, 0)) && (!lineJournal.forcedAssignment))
			{
				System.Threading.Thread.Sleep(5000);

				lineJournal.status = 1;
				lineJournal.save(database);

				lineJournal.removeJournal(database);
			}
		}


		private bool assignLineOrderToRoute(LineOrder lineOrder, Agent agent, DateTime shipDate, Database database)
		{
			return lineOrder.assignToRoute(database, agent.code, shipDate);			
		}

		/*
		private ArrayList findAgentsForLineOrder(LineOrder lineOrder)
		{
			ArrayList agentList = new ArrayList();

			ShippingCustomerOrganizations shippingCustomerOrganizations = new ShippingCustomerOrganizations();
			AgentServiceSchedules agentServiceSchedules = new AgentServiceSchedules();
			DataSet organizationDataSet = shippingCustomerOrganizations.getShippingCustomerDataSet(database, lineOrder.shippingCustomerNo);

			int i = 0;
			while(i < organizationDataSet.Tables[0].Rows.Count)
			{
				ShippingCustomerOrganization shippingCustomerOrganization = new ShippingCustomerOrganization(organizationDataSet.Tables[0].Rows[i]);
				
				if (shippingCustomerOrganization.type == 0)
				{
					Agents agents = new Agents();
					DataSet agentDataSet = agents.getDataSet(database, shippingCustomerOrganization.code, Agents.TYPE_LINE);

					int j = 0;
					while(j < agentDataSet.Tables[0].Rows.Count)
					{
						if (!agentServiceSchedules.checkServiceSchedule(database, agentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), lineOrder.shipDate))
						{
							if (!agentList.Contains(agentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
							{
								agentList.Add(agentDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
							}
						}

						j++;
					}
				}
				if (shippingCustomerOrganization.type == 1)
				{
					if (!agentList.Contains(shippingCustomerOrganization.code))
					{
						agentList.Add(shippingCustomerOrganization.code);
					}
				}

				i++;
			}

			return agentList;

		}
		*/

		private bool directAssignmentOfLineOrderToAgent(LineOrder lineOrder, Agent agent, Database database)
		{
			if (agent.enabled)
			{
				AgentServiceSchedules agentServiceSchedules = new AgentServiceSchedules();

				DateTime shipDate = lineOrder.shipDate;
				if (shipDate < DateTime.Today) shipDate = DateTime.Today;

				if (!agentServiceSchedules.checkServiceSchedule(database, agent.code, lineOrder.shipDate))
				{
					log("Line order direct assignment to "+agent.code, 0);

					assignLineOrderToRoute(lineOrder, agent, shipDate, database);

					return true;
				}
		

				
			}

			return false;

		}


		private void log(string message, int type)
		{
			logger.write("[LineOrderAssigner] "+message, type);
		}

		private string arrayListToString(ArrayList arrayList)
		{
			string result = "";

			int i = 0;
			while (i < arrayList.Count)
			{
				if (i > 0) result = result + ", ";
				result = result + arrayList[i].ToString();
				i++;
			}

			return result;
		}
	}
}
