using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentServiceSchedules.
	/// </summary>
	public class AgentServiceSchedules
	{
		public AgentServiceSchedules()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getOrganizationDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Agent Code], [From Date], [To Date] FROM [Agent Service Schedule] WHERE [Organization No] = '"+organizationNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agentServiceSchedule");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getAgentDataSet(Database database, string agentCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Agent Code], [From Date], [To Date] FROM [Agent Service Schedule] WHERE [Agent Code] = '"+agentCode+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agentServiceSchedule");
			adapter.Dispose();

			return dataSet;

		}

		public bool checkServiceSchedule(Database database, string agentCode, DateTime date)
		{
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Agent Code], [From Date], [To Date] FROM [Agent Service Schedule] WHERE [Agent Code] = '"+agentCode+"' AND [From Date] <= '"+date.ToString("yyyy-MM-dd")+"' AND [To Date] >= '"+date.ToString("yyyy-MM-dd")+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				return true;
			}

			dataReader.Close();
			return false;
		}

	}
}
