using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class FactoryInventoryQueueEntries
	{

		public FactoryInventoryQueueEntries()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public FactoryInventoryQueueEntry getFirstEntry(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Line Journal Entry No], [Enqueued Date], [Enqueued Time] FROM [Factory Inventory Queue] ORDER BY [Entry No]");
			if (dataReader.Read())
			{
				FactoryInventoryQueueEntry queueEntry = new FactoryInventoryQueueEntry(dataReader);
				dataReader.Close();
				return queueEntry;
			}
			dataReader.Close();
			
			return null;
		}

		public void enqueue(Database database, int lineJournalEntryNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Line Journal Entry No], [Enqueued Date], [Enqueued Time] FROM [Factory Inventory Queue] WHERE [Line Journal Entry No] = '"+lineJournalEntryNo+"'");
			if (!dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Factory Inventory Queue] ([Line Journal Entry No], [Enqueued Date], [Enqueued Time]) VALUES ('"+lineJournalEntryNo+"', '"+DateTime.Now.ToString("yyyy-MM-dd 00:00:00")+"', '"+DateTime.Now.ToString("1754-01-01 HH:mm:ss")+"')");
				return;
			}
			dataReader.Close();

		}

	}
}
