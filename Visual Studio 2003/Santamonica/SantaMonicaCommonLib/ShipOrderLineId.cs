using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLine.
	/// </summary>
	public class ShipOrderLineId
	{

		public int entryNo;
		public int shipOrderEntryNo;
		public int shipOrderLineEntryNo;
		public string unitId;
		public bool bseTesting;
		public bool postMortem;

		private string updateMethod;

		public ShipOrderLineId(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.shipOrderEntryNo = dataReader.GetInt32(1);
			this.shipOrderLineEntryNo = dataReader.GetInt32(2);
			this.unitId = dataReader.GetValue(3).ToString();

			this.bseTesting = false;
			if (dataReader.GetValue(4).ToString() == "1") this.bseTesting = true;

			this.postMortem = false;
			if (dataReader.GetValue(5).ToString() == "1") this.postMortem = true;

		}

		public ShipOrderLineId(ShipOrderLine shipOrderLine)
		{

			this.shipOrderEntryNo = shipOrderLine.shipOrderEntryNo;
			this.shipOrderLineEntryNo = shipOrderLine.entryNo;
		}

		public ShipOrderLineId(Database database, int shipOrderEntryNo, int shipOrderLineEntryNo, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.shipOrderEntryNo = shipOrderEntryNo;
			this.shipOrderLineEntryNo = shipOrderLineEntryNo;

			fromDataRow(dataRow);
			updateMethod = "";
			
			save(database);
		}


		private void fromDataRow(DataRow dataRow)
		{
			unitId = dataRow.ItemArray.GetValue(3).ToString();

			if (dataRow.ItemArray.Length > 4)
			{
				this.bseTesting = false;
				if (dataRow.ItemArray.GetValue(4).ToString() == "1") this.bseTesting = true;

				this.postMortem = false;
				if (dataRow.ItemArray.GetValue(5).ToString() == "1") this.postMortem = true;
			}

		}


		public void save(Database database)
		{
			int bseTestingVal = 0;
			if (this.bseTesting) bseTestingVal = 1;

			int postMortemVal = 0;
			if (this.postMortem) postMortemVal = 1;

			ShipOrders shipOrders = new ShipOrders();
			ShipOrder shipOrder = shipOrders.getEntry(database, shipOrderEntryNo.ToString());
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			SqlDataReader dataReader = database.query("SELECT * FROM [Ship Order Line ID] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Ship Order Line ID] WHERE [Entry No] = '"+entryNo+"'");
					if (shipOrder.agentCode != "") synchQueue.enqueue(database, shipOrder.agentCode, 10, entryNo.ToString(), 2);

				}

				else
				{

					database.nonQuery("UPDATE [Ship Order Line ID] SET [Unit ID] = '"+this.unitId+"', [BSE Testing] = '"+bseTestingVal+"', [Post Mortem] = '"+postMortemVal+"' WHERE [Entry No] = '"+this.entryNo+"' AND [Ship Order Entry No] = '"+this.shipOrderEntryNo+"' AND [Ship Order Line Entry No] = '"+this.shipOrderLineEntryNo+"'");
					if (shipOrder.agentCode != "") synchQueue.enqueue(database, shipOrder.agentCode, 10, entryNo.ToString(), 0);

				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Ship Order Line ID] ([Ship Order Entry No], [Ship Order Line Entry No], [Unit ID], [BSE Testing], [Post Mortem]) VALUES ('"+this.shipOrderEntryNo+"','"+this.shipOrderLineEntryNo+"','"+this.unitId+"', '"+bseTestingVal+"', '"+postMortemVal+"')");
				entryNo = (int)database.getInsertedSeqNo();

				if (shipOrder.agentCode != "") synchQueue.enqueue(database, shipOrder.agentCode, 10, entryNo.ToString(), 0);

			}

		}

		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}
	}
}
