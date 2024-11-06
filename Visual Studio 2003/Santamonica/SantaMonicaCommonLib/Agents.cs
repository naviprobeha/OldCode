using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Agents.
	/// </summary>
	public class Agents
	{
		public static int TYPE_SINGLE = 0;
		public static int TYPE_LINE = 1;
		public static int TYPE_MULTI = 2;
		public static int TYPE_TANK = 3;

		public Agents()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent] WHERE [Organization No] = '"+organizationNo+"' AND [Enabled] = 1");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string organizationNo, int type)
		{
			string typeQuery = "";
			if ((type == 0) ||(type == 1)) typeQuery = "OR [Type] = '2'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent] WHERE [Organization No] = '"+organizationNo+"' AND [Enabled] = 1 AND ([Type] = '"+type+"' "+typeQuery+") ORDER BY [Type]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getPlanningDataSet(Database database, string organizationNo)
		{
			int type = Agents.TYPE_LINE;
			string typeQuery = "";
			if ((type == 0) ||(type == 1)) typeQuery = "OR [Type] = '2'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent] WHERE [Organization No] = '"+organizationNo+"' AND [Enabled] = 1 AND ([Type] = '"+type+"' "+typeQuery+") AND  [Auto Plan Enabled] = 1 ORDER BY [Type]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string organizationNo, string agentStorageGroup)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent] WHERE [Organization No] = '"+organizationNo+"' AND [Enabled] = 1 AND [Agent Storage Group] = '"+agentStorageGroup+"' ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getDataSet(Database database, int type)
		{
			string typeQuery = "";
			if ((type == 0) ||(type == 1)) typeQuery = "OR [Type] = '2'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent] WHERE [Enabled] = 1 AND ([Type] = '"+type+"' "+typeQuery+") ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, string organizationNo, string no)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Organization No], [No Of Containers], [Volume Storage] FROM [Agent] WHERE [Organization No] = '"+organizationNo+"' AND [Code] = '"+no+"' AND [Enabled] = 1");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getAllDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getContainerDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent] WHERE [Type] < 3");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getAllDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent] WHERE [Organization No] = '"+organizationNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public Agent getAgent(Database database, string code)
		{
			SqlDataReader dataReader = database.query("SELECT [Code], [Description], [Enabled], [Last Updated Date], [Last Updated Timestamp], [Status], [Type], [Organization No], [No Of Containers], [Position X], [Position Y], [Speed], [Height], [Heading], [User Name], [Auto Plan Enabled], [Volume Storage], [Agent Storage Group], [Clone To Agent Code] FROM [Agent] WHERE Code = '"+code+"'");
			
			Agent agent = null;

			if (dataReader.Read())
			{
				agent = new Agent(dataReader);
			}
			dataReader.Close();

			return agent;


		}

		public void updateAgent(Database database, AgentTransaction agentTransaction)
		{
			Agent agent = getAgent(database, agentTransaction.agentCode);
			if (agent != null)
			{
				agent.lastUpdated = agentTransaction.updatedDateTime;
				agent.status = agentTransaction.status;
				agent.positionX = agentTransaction.positionX;
				agent.positionY = agentTransaction.positionY;
				agent.speed = agentTransaction.speed;
				agent.height = agentTransaction.height;
				agent.heading = agentTransaction.heading;
				agent.userName = agentTransaction.userName;

				agent.save(database);
			}

		}


		public Agent findAgentForLineOrder(Database database, LineOrder lineOrder, DateTime shipDate)
		{
			// Sätt in lämpliga bilar för rutten i en lista.
			ArrayList agentList = findAgentsForLineOrder(database, lineOrder);

			Agent choosenAgent = null;

			int i = 0;
			while ((i < agentList.Count) && (choosenAgent == null))
			{
				Agent agent = getAgent(database, agentList[i].ToString());
				if (((agent.getNoOfContainers(database) - agent.countContainers(database, shipDate, 0)) >= lineOrder.countContainers(database, 0)) && 
					((agent.getVolumeStorage(database) - agent.countContainers(database, shipDate, 1)) >= lineOrder.countContainers(database, 1))) choosenAgent = agent;
				i++;
			}

			if (choosenAgent == null)
			{
				DateTime lastArrivalDateTime = new DateTime(2100, 1, 1, 0, 0, 0);

				i = 0;
				while (i < agentList.Count)
				{
					Agent agent = getAgent(database, agentList[i].ToString());
					if ((agent.getNoOfContainers(database) >= lineOrder.countContainers(database, 0)) && 
						(agent.getVolumeStorage(database) >= lineOrder.countContainers(database, 1)))
					{

						LineJournals lineJournals = new LineJournals();
						ArrayList lineJournalList = lineJournals.getPlanningJournalsByArrivalTime(database, agentList[i].ToString());
						if (lineJournalList.Count > 0)
						{
							LineJournal lineJournal = (LineJournal)lineJournalList[lineJournalList.Count-1];
							if (lastArrivalDateTime > lineJournal.arrivalDateTime)
							{
								lastArrivalDateTime = lineJournal.arrivalDateTime;
								choosenAgent = getAgent(database, agentList[i].ToString());
							}
						}
					}

					i++;
				}
			}

			if (choosenAgent == null)
			{
				i = 0;
				while (i < agentList.Count)
				{

					Agent agent = getAgent(database, agentList[i].ToString());
					//Console.WriteLine("Testing "+agent.code+", Volume: "+agent.getVolumeStorage(database)+", Containers: "+agent.getNoOfContainers(database));
					//Console.WriteLine("Order "+lineOrder.entryNo+" Volume: "+lineOrder.countContainers(database, 1)+", Containers: "+lineOrder.countContainers(database, 0));
					if ((agent.getNoOfContainers(database) >= lineOrder.countContainers(database, 0)) && 
						(agent.getVolumeStorage(database) >= lineOrder.countContainers(database, 1))) choosenAgent = agent;
					i++;
				}
			}

			return choosenAgent;

		}

		private ArrayList findAgentsForLineOrder(Database database, LineOrder lineOrder)
		{
			ArrayList agentList = new ArrayList();

			AgentServiceSchedules agentServiceSchedules = new AgentServiceSchedules();

			DataSet agentDataSet = getDataSet(database, lineOrder.organizationNo, Agents.TYPE_LINE);

			int j = 0;
			while(j < agentDataSet.Tables[0].Rows.Count)
			{
				Agent agent = new Agent(agentDataSet.Tables[0].Rows[j]);

				if (!agentServiceSchedules.checkServiceSchedule(database, agent.code, lineOrder.shipDate))
				{
					if (!agentList.Contains(agent.code))
					{
						if (agent.autoPlanEnabled)
						{
							agentList.Add(agent.code);
						}
					}
				}

				j++;
			}

			return agentList;

		}

	}
}
