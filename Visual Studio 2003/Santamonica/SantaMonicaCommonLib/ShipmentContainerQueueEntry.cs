using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class ShipmentContainerQueueEntry
	{
		public int entryNo;
		public string shipmentNo;
		public string containerNo;
		public DateTime enqueuedDateTime;


		public ShipmentContainerQueueEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.shipmentNo = dataReader.GetValue(1).ToString();
			this.containerNo = dataReader.GetValue(2).ToString();
			DateTime enqueuedDate = dataReader.GetDateTime(3);
			DateTime enqueuedTime = dataReader.GetDateTime(4);

			this.enqueuedDateTime = new DateTime(enqueuedDate.Year, enqueuedDate.Month, enqueuedDate.Day, enqueuedTime.Hour, enqueuedTime.Minute, enqueuedTime.Second);
		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Shipment Container Queue] WHERE [Entry No] = '"+entryNo+"'");
		}

	}
}
