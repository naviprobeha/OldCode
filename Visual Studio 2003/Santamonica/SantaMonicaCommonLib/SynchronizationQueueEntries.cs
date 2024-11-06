using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class SynchronizationQueueEntries
	{
		public static int SYNC_ALL = -1;
		public static int SYNC_SHIP_ORDER = 0;
		public static int SYNC_CUSTOMER = 1;
		public static int SYNC_ITEM = 2;
		public static int SYNC_ITEM_PRICE = 3;
		public static int SYNC_MAP = 4;
		public static int SYNC_MESSAGE = 5;
		public static int SYNC_ORGANIZATION = 6;
		public static int SYNC_ITEM_PRICE_EXTENDED = 7;
		public static int SYNC_MOBILE_USER = 8;
		public static int SYNC_SHIP_ORDER_LINE = 9;
		public static int SYNC_SHIP_ORDER_LINE_ID = 10;
		public static int SYNC_AGENT = 11;
		public static int SYNC_CONTAINER = 12;
		public static int SYNC_LINE_JOURNAL = 13;
		public static int SYNC_LINE_ORDER = 14;
		public static int SYNC_LINE_ORDER_CONTAINER = 15;
		public static int SYNC_ORGANIZATION_LOCATION = 16;
		public static int SYNC_CATEGORY = 17;
		public static int SYNC_CUSTOMER_SHIP_ADDRESS = 18;
		public static int SYNC_FACTORY_ORDER = 19;
		public static int SYNC_SQL = 20;
		public static int SYNC_CONSUMER_STATUS = 21;

		private string agentCode;

		public SynchronizationQueueEntries()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public SynchronizationQueueEntries(string agentCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.agentCode = agentCode;
		}

		public int getCount(Database database)
		{
			int count = 0;
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentCode+"'");
			if (dataReader.Read())
			{
				count = dataReader.GetInt32(0);
			}

			dataReader.Close();
			
			return count;
		}

		public SynchronizationQueueEntry getFirstEntry(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT [Agent Code], [Entry No], [Type], [Primary Key], [Action] FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentCode+"' ORDER BY [Entry No]");
			if (dataReader.Read())
			{
				SynchronizationQueueEntry synchEntry = new SynchronizationQueueEntry(dataReader);
				dataReader.Close();
				return synchEntry;
			}
			dataReader.Close();
			
			return null;
		}

		public SynchronizationQueueEntry getFirstEntry(Database database, int type)
		{
			SqlDataReader dataReader = database.query("SELECT [Agent Code], [Entry No], [Type], [Primary Key], [Action] FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentCode+"' AND [Type] = '"+type+"' ORDER BY [Entry No]");
			if (dataReader.Read())
			{
				SynchronizationQueueEntry synchEntry = new SynchronizationQueueEntry(dataReader);
				dataReader.Close();
				return synchEntry;
			}
			dataReader.Close();			

			return null;
		}

		public SynchronizationQueueEntry getEntry(Database database, int entryNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Agent Code], [Entry No], [Type], [Primary Key], [Action] FROM [Synchronization Queue] WHERE [Entry No] = '"+entryNo+"'");
			if (dataReader.Read())
			{
				SynchronizationQueueEntry synchEntry = new SynchronizationQueueEntry(dataReader);
				dataReader.Close();
				return synchEntry;
			}
			
			return null;
		}

		public void enqueue(Database database, int agentType, int type, string primaryKey, int action)
		{
			Agents agents = new Agents();
			DataSet agentDataSet = agents.getDataSet(database, agentType);

			int i = 0;
			while (i < agentDataSet.Tables[0].Rows.Count)
			{
				
				bool alreadyExists = false;

				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"' AND [Type] = '"+type+"' AND [Primary Key] = '"+primaryKey+"' AND [Action] = '"+action+"'");
				if (dataReader.Read())
				{
					alreadyExists = true;
				}
				dataReader.Close();
				

				if (!alreadyExists)	database.nonQuery("INSERT INTO [Synchronization Queue] ([Agent Code], [Type], [Primary Key], [Action]) VALUES ('"+agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"','"+type+"','"+primaryKey+"','"+action+"')");

				i++;
			}
		}


		public void enqueue(Database database, string agentCode, int type, string primaryKey, int action)
		{
			Agents agents = new Agents();
			Agent agent = agents.getAgent(database, agentCode);
			if (agent != null)
			{
				if (agent.cloneToAgentCode != "") enqueue(database, agent.cloneToAgentCode, type, primaryKey, action);
			}

			database.nonQuery("DELETE FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentCode+"' AND [Type] = '"+type+"' AND [Primary Key] = '"+primaryKey+"' AND [Action] = '"+action+"'");
			
			if (action == 0)
			{
				database.nonQuery("DELETE FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentCode+"' AND [Type] = '"+type+"' AND [Primary Key] = '"+primaryKey+"' AND [Action] = '2'");
			}

			if (action == 2)
			{
				database.nonQuery("DELETE FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentCode+"' AND [Type] = '"+type+"' AND [Primary Key] = '"+primaryKey+"' AND [Action] = '0'");
			}

			database.nonQuery("INSERT INTO [Synchronization Queue] ([Agent Code], [Type], [Primary Key], [Action]) VALUES ('"+agentCode+"','"+type+"','"+primaryKey+"','"+action+"')");
			return;

		}

		public void enqueueAllAgents(Database database, int type, string primaryKey, int action)
		{
			Agents agents = new Agents();
			DataSet agentDataSet = agents.getAllDataSet(database);

			int i = 0;
			while (i < agentDataSet.Tables[0].Rows.Count)
			{	
				bool alreadyExists = false;

				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"' AND [Type] = '"+type+"' AND [Primary Key] = '"+primaryKey+"' AND [Action] = '"+action+"'");
				if (dataReader.Read())
				{
					alreadyExists = true;
				}
				dataReader.Close();

				if (!alreadyExists)	database.nonQuery("INSERT INTO [Synchronization Queue] ([Agent Code], [Type], [Primary Key], [Action]) VALUES ('"+agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"','"+type+"','"+primaryKey+"','"+action+"')");
				
				
				i++;
			}
		}

		public void enqueueContainerAgents(Database database, int type, string primaryKey, int action)
		{
			Agents agents = new Agents();
			DataSet agentDataSet = agents.getContainerDataSet(database);

			int i = 0;
			while (i < agentDataSet.Tables[0].Rows.Count)
			{	
				bool alreadyExists = false;

				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"' AND [Type] = '"+type+"' AND [Primary Key] = '"+primaryKey+"' AND [Action] = '"+action+"'");
				if (dataReader.Read())
				{
					alreadyExists = true;
				}
				dataReader.Close();

				if (!alreadyExists)	database.nonQuery("INSERT INTO [Synchronization Queue] ([Agent Code], [Type], [Primary Key], [Action]) VALUES ('"+agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"','"+type+"','"+primaryKey+"','"+action+"')");
				
				
				i++;
			}
		}

		public void enqueueAgentsInOrganization(Database database, string organizationNo, int agentType, int type, string primaryKey, int action)
		{
			Agents agents = new Agents();
			DataSet agentDataSet = agents.getDataSet(database, organizationNo, agentType);

			int i = 0;
			while (i < agentDataSet.Tables[0].Rows.Count)
			{
				
				bool alreadyExists = false;

				SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"' AND [Type] = '"+type+"' AND [Primary Key] = '"+primaryKey+"' AND [Action] = '"+action+"'");
				if (dataReader.Read())
				{
					alreadyExists = true;
				}
				dataReader.Close();
				

				if (!alreadyExists)	database.nonQuery("INSERT INTO [Synchronization Queue] ([Agent Code], [Type], [Primary Key], [Action]) VALUES ('"+agentDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"','"+type+"','"+primaryKey+"','"+action+"')");

				i++;
			}
		}

	}
}
