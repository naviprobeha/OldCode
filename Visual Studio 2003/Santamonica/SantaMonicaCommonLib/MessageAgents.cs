using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Messages.
	/// </summary>
	public class MessageAgents
	{
		public MessageAgents()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getDataSet(Database database, string organizationNo, string messageEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Agent Code], [Status] FROM [Message Agent] WHERE [Organization No] = '"+organizationNo+"' AND [Message Entry No] = '"+messageEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "messageAgent");
			adapter.Dispose();

			return dataSet;

		}

		public MessageAgent getEntry(Database database, int messageEntryNo, string agentCode)
		{
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Message Entry No], [Agent Code], [Status], [Acknowledged Date], [Acknowledged Time] FROM [Message Agent] WHERE [Message Entry No] = '"+messageEntryNo+"' AND [Agent Code] = '"+agentCode+"'");

			MessageAgent messageAgent = null;

			if (dataReader.Read())
			{
				messageAgent = new MessageAgent(dataReader);
			}
			dataReader.Close();

			return messageAgent;
		}
	}
}
