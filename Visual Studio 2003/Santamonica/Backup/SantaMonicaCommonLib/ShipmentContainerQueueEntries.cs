using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class ShipmentContainerQueueEntries
	{

		public ShipmentContainerQueueEntries()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public ShipmentContainerQueueEntry getFirstEntry(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Shipment No], [Container No], [Enqueued Date], [Enqueued Time] FROM [Shipment Container Queue] ORDER BY [Entry No]");
			if (dataReader.Read())
			{
				ShipmentContainerQueueEntry queueEntry = new ShipmentContainerQueueEntry(dataReader);
				dataReader.Close();
				return queueEntry;
			}
			dataReader.Close();
			
			return null;
		}

		public void enqueue(Database database, string shipmentNo, string containerNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Shipment No], [Container No], [Enqueued Date], [Enqueued Time] FROM [Shipment Container Queue] WHERE [Shipment No] = '"+shipmentNo+"'");
			if (!dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Shipment Container Queue] ([Shipment No], [Container No], [Enqueued Date], [Enqueued Time]) VALUES ('"+shipmentNo+"','"+containerNo+"', '"+DateTime.Now.ToString("yyyy-MM-dd 00:00:00")+"', '"+DateTime.Now.ToString("1754-01-01 HH:mm:ss")+"')");
				return;
			}
			dataReader.Close();

		}

	}
}
