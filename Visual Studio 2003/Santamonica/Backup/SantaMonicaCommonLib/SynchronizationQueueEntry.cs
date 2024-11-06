using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class SynchronizationQueueEntry
	{
		public string agentCode;
		public int entryNo;
		public int type;
		public string primaryKey;
		public int action;
	


		public SynchronizationQueueEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.agentCode = dataReader.GetValue(0).ToString();
			this.entryNo = dataReader.GetInt32(1);
			this.type = dataReader.GetInt32(2);
			this.primaryKey = dataReader.GetValue(3).ToString();
			this.action = dataReader.GetInt32(4);
		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Synchronization Queue] WHERE [Agent Code] = '"+agentCode+"' AND [Entry No] = '"+entryNo+"'");
		}

	}
}
