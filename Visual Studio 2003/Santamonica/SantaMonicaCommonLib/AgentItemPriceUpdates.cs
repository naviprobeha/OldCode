using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Organizations.
	/// </summary>
	public class AgentItemPriceUpdates
	{
		public AgentItemPriceUpdates()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet(Database database, string agentCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Agent Code], [Item No], [Update], [Checksum], [Reported Checksum] FROM [Agent Item Price Update] WHERE [Agent Code] = '"+agentCode+"' AND [Update] = 1");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agentItemPriceUpdate");
			adapter.Dispose();

			return dataSet;

		}

		public AgentItemPriceUpdate getEntry(Database database, string agentCode, string itemNo)
		{
			AgentItemPriceUpdate agentItemPriceUpdate = null;

			SqlDataReader dataReader = database.query("SELECT [Agent Code], [Item No], [Update], [Checksum], [Reported Checksum] FROM [Agent Item Price Update] WHERE [Agent Code] = '"+agentCode+"' AND [Item No] = '"+itemNo+"'");

			if (dataReader.Read())
			{
				agentItemPriceUpdate = new AgentItemPriceUpdate(dataReader);
			}
			dataReader.Close();

			return agentItemPriceUpdate;
		}

		public void acknowledge(Database database, string agentCode, string itemNo, float checksum)
		{
			AgentItemPriceUpdate agentItemPriceUpdate = getEntry(database, agentCode, itemNo);

			if (agentItemPriceUpdate == null)
			{
				agentItemPriceUpdate = new AgentItemPriceUpdate();
				agentItemPriceUpdate.agentCode = agentCode;
				agentItemPriceUpdate.itemNo = itemNo;
				agentItemPriceUpdate.save(database);
			}

			agentItemPriceUpdate.acknowledge(database, checksum);

		}

		public void setUpdate(Database database, string itemNo)
		{
			Agents agents = new Agents();
			DataSet dataSet = agents.getDataSet(database, Agents.TYPE_SINGLE);
			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				Agent agent = new Agent(dataSet.Tables[0].Rows[i]);
				setUpdate(database, itemNo, agent.code);

				i++;
			}

		}

		public void setUpdate(Database database, string itemNo, string agentCode)
		{
			AgentItemPriceUpdate agentItemPriceUpdate = getEntry(database, agentCode, itemNo);
			if (agentItemPriceUpdate == null)
			{
				agentItemPriceUpdate = new AgentItemPriceUpdate();
				agentItemPriceUpdate.agentCode = agentCode;
				agentItemPriceUpdate.itemNo = itemNo;
				agentItemPriceUpdate.save(database);
			}
			agentItemPriceUpdate.update = true;
			agentItemPriceUpdate.save(database);

		}

	}

}
