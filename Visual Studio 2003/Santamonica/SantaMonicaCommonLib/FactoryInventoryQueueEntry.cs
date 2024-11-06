using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class FactoryInventoryQueueEntry
	{
		public int entryNo;
		public int lineJournalEntryNo;
		public DateTime enqueuedDateTime;


		public FactoryInventoryQueueEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.lineJournalEntryNo = dataReader.GetInt32(1);
			DateTime enqueuedDate = dataReader.GetDateTime(2);
			DateTime enqueuedTime = dataReader.GetDateTime(3);

			this.enqueuedDateTime = new DateTime(enqueuedDate.Year, enqueuedDate.Month, enqueuedDate.Day, enqueuedTime.Hour, enqueuedTime.Minute, enqueuedTime.Second);
		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Factory Inventory Queue] WHERE [Entry No] = '"+entryNo+"'");
		}

	}
}
