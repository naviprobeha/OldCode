using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransactions.
	/// </summary>
	public class AgentTransactions
	{
		public AgentTransactions()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public AgentTransaction getCurrentAgentTransaction(Database database, string agentCode)
		{
			SqlDataReader dataReader = database.query("SELECT TOP 1 [Agent Code], [Entry No], [Date], [Timestamp], [Position X], [Position Y], [Speed], [Height], [Heading], [Status], [User Name], [Trip Meter] FROM [Agent Status] WHERE [Agent Code] = '"+agentCode+"' ORDER BY [Entry No] DESC");

			AgentTransaction agentTransaction = null;

			if (dataReader.Read())
			{
				agentTransaction = new AgentTransaction(dataReader);
			}

			dataReader.Close();

			
			return agentTransaction;


		}

		public AgentTransaction getAgentTransaction(Database database, string agentCode, DateTime date)
		{
			SqlDataReader dataReader = database.query("SELECT [Agent Code], [Entry No], [Date], [Timestamp], [Position X], [Position Y], [Speed], [Height], [Heading], [Status], [User Name], [Trip Meter] FROM [Agent Status] WHERE [Agent Code] = '"+agentCode+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' ORDER BY [Timestamp]");

			AgentTransaction agentTransaction = null;

			if (dataReader.Read())
			{
				agentTransaction = new AgentTransaction(dataReader);
			}

			dataReader.Close();

			
			return agentTransaction;


		}

		public DataSet getAgentTransactions(Database database, string agentCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Agent Code], [Entry No], [Date], [Timestamp], [Position X], [Position Y], [Speed], [Height], [Heading], [Status], [User Name], [Trip Meter] FROM [Agent Status] WHERE [Agent Code] = '"+agentCode+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agentStatus");
			adapter.Dispose();

			return dataSet;


		}

		public DataSet getAgentTransactions(Database database, string agentCode, DateTime date)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Agent Code], [Entry No], [Date], [Timestamp], [Position X], [Position Y], [Speed], [Height], [Heading], [Status], [User Name], [Trip Meter] FROM [Agent Status] WHERE [Agent Code] = '"+agentCode+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' ORDER BY [Timestamp]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agentStatus");
			adapter.Dispose();

			return dataSet;


		}

		public AgentTransaction getFirstAgentTransactions(Database database, string agentCode, DateTime date)
		{
			SqlDataReader dataReader = database.query("SELECT TOP 1 [Agent Code], [Entry No], [Date], [Timestamp], [Position X], [Position Y], [Speed], [Height], [Heading], [Status], [User Name], [Trip Meter] FROM [Agent Status] WHERE [Agent Code] = '"+agentCode+"' AND [Date] >= '"+date.ToString("yyyy-MM-dd")+"' ORDER BY [Date], [Timestamp]");

			AgentTransaction agentTransaction = null;

			if (dataReader.Read())
			{
				agentTransaction = new AgentTransaction(dataReader);
			}

			dataReader.Close();

			return agentTransaction;

		}

		public AgentTransaction getLastAgentTransactions(Database database, string agentCode, DateTime date)
		{
			SqlDataReader dataReader = database.query("SELECT TOP 1 [Agent Code], [Entry No], [Date], [Timestamp], [Position X], [Position Y], [Speed], [Height], [Heading], [Status], [User Name], [Trip Meter] FROM [Agent Status] WHERE [Agent Code] = '"+agentCode+"' AND [Date] <= '"+date.ToString("yyyy-MM-dd")+"' AND [Trip Meter] > 0 ORDER BY [Date] DESC, [Timestamp] DESC");

			AgentTransaction agentTransaction = null;

			if (dataReader.Read())
			{
				agentTransaction = new AgentTransaction(dataReader);	
			}

			dataReader.Close();

			return agentTransaction;

		}
	}
}
