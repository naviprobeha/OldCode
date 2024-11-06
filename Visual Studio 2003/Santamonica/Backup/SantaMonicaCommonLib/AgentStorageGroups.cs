using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Categories.
	/// </summary>
	public class AgentStorageGroups
	{
		public AgentStorageGroups()
		{
			//
			// TODO: Add constructor logic here		
			//
		}

		public AgentStorageGroup getEntry(Database database, string code)
		{
			AgentStorageGroup agentStorageGroup = null;
			
			SqlDataReader dataReader = database.query("SELECT [Code], [Description], [No Of Containers], [Volume Storage] FROM [Agent Storage Group] WHERE [Code] = '"+code+"'");
			if (dataReader.Read())
			{
				agentStorageGroup = new AgentStorageGroup(dataReader);
			}
			
			dataReader.Close();
			return agentStorageGroup;
		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [No Of Containers], [Volume Storage] FROM [Agent Storage Group]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agentStorageGroup");
			adapter.Dispose();

			return dataSet;

		}	
	
		public DataSet getDataSetEntry(Database database, string code)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [No Of Containers], [Volume Storage] FROM [Agent Storage Group] WHERE [Code] = '"+code+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agentStorageGroup");
			adapter.Dispose();

			return dataSet;

		}	
	}
}
