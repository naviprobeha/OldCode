using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLine.
	/// </summary>
	public class ShipOrderLine
	{

		public int entryNo;
		public int shipOrderEntryNo;
		public string itemNo;
		public int quantity;
		public int connectionQuantity;
		public float unitPrice;
		public float amount;
		public float connectionUnitPrice;
		public float connectionAmount;
		public float totalAmount;
		public string connectionItemNo;
		public int testQuantity;

		public int originalEntryNo;

		private string updateMethod;

		public ShipOrderLine(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.shipOrderEntryNo = dataReader.GetInt32(1);
			this.itemNo = dataReader.GetValue(2).ToString();
			this.quantity = dataReader.GetInt32(3);
			this.connectionQuantity = dataReader.GetInt32(4);
			this.unitPrice = float.Parse(dataReader.GetDecimal(5).ToString());
			this.amount = float.Parse(dataReader.GetDecimal(6).ToString());
			this.connectionUnitPrice = float.Parse(dataReader.GetDecimal(7).ToString());
			this.connectionAmount = float.Parse(dataReader.GetDecimal(8).ToString());
			this.totalAmount = float.Parse(dataReader.GetDecimal(9).ToString());
			this.connectionItemNo = dataReader.GetValue(10).ToString();
			this.testQuantity = dataReader.GetInt32(11);
		}

		public ShipOrderLine(ShipOrder shipOrder)
		{

			this.shipOrderEntryNo = shipOrder.entryNo;
		}

		public ShipOrderLine()
		{

		}

		public ShipOrderLine(Database database, DataRow dataRow, int shipOrderEntryNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.shipOrderEntryNo = shipOrderEntryNo;

			fromDataRow(dataRow);
			
			updateMethod = "";
			save(database);


		}


		private void fromDataRow(DataRow dataRow)
		{
			originalEntryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			//shipmentNo = agentCode +"-"+dataRow.ItemArray.GetValue(1).ToString();
			this.itemNo = dataRow.ItemArray.GetValue(2).ToString();
			this.quantity = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.connectionQuantity = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.unitPrice = float.Parse(dataRow.ItemArray.GetValue(6).ToString());
			this.amount = float.Parse(dataRow.ItemArray.GetValue(7).ToString());
			this.connectionUnitPrice = float.Parse(dataRow.ItemArray.GetValue(8).ToString());
			this.connectionAmount = float.Parse(dataRow.ItemArray.GetValue(9).ToString());		
			this.totalAmount = float.Parse(dataRow.ItemArray.GetValue(10).ToString());		
			this.connectionItemNo = dataRow.ItemArray.GetValue(11).ToString();
			
			try
			{
				this.testQuantity = int.Parse(dataRow.ItemArray.GetValue(12).ToString());
			}
			catch(Exception e)
			{
				if (e.Message != "") {}
			}

		}

		public void save(Database database)
		{

			ShipOrders shipOrders = new ShipOrders();
			ShipOrder shipOrder = shipOrders.getEntry(database, shipOrderEntryNo.ToString());
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
			

			SqlDataReader dataReader = database.query("SELECT * FROM [Ship Order Line] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Ship Order Line] WHERE [Entry No] = '"+entryNo+"'");
					if (shipOrder.agentCode != "") synchQueue.enqueue(database, shipOrder.agentCode, 9, entryNo.ToString(), 2);
				}

				else
				{

					database.nonQuery("UPDATE [Ship Order Line] SET [Ship Order Entry No] = '"+this.shipOrderEntryNo+"', [Item No] = '"+this.itemNo+"', [Quantity] = '"+this.quantity+"', [Connection Quantity] = '"+this.connectionQuantity+"', [Unit Price] = '"+this.unitPrice.ToString().Replace(",", ".")+"', [Amount] = '"+this.amount.ToString().Replace(",", ".")+"', [Connection Unit Price] = '"+this.connectionUnitPrice.ToString().Replace(",", ".")+"', [Connection Amount] = '"+this.connectionAmount.ToString().Replace(",", ".")+"', [Total Amount] = '"+this.totalAmount.ToString().Replace(",", ".")+"', [Connection Item No] = '"+this.connectionItemNo+"', [Test Quantity] = '"+this.testQuantity+"' WHERE [Entry No] = '"+this.entryNo+"'");
					if (shipOrder.agentCode != "") synchQueue.enqueue(database, shipOrder.agentCode, 9, entryNo.ToString(), 0);

				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Ship Order Line] ([Ship Order Entry No], [Item No], [Quantity], [Connection Quantity], [Unit Price], [Amount], [Connection Unit Price], [Connection Amount], [Total Amount], [Connection Item No], [Test Quantity]) VALUES ('"+this.shipOrderEntryNo+"','"+this.itemNo+"','"+this.quantity+"','"+this.connectionQuantity+"','"+this.unitPrice.ToString().Replace(",", ".")+"','"+this.amount.ToString().Replace(",", ".")+"','"+this.connectionUnitPrice.ToString().Replace(",", ".")+"','"+this.connectionAmount.ToString().Replace(",", ".")+"','"+this.totalAmount.ToString().Replace(",", ".")+"','"+this.connectionItemNo+"','"+this.testQuantity+"')");
				entryNo = (int)database.getInsertedSeqNo();

				if (shipOrder.agentCode != "") synchQueue.enqueue(database, shipOrder.agentCode, 9, entryNo.ToString(), 0);

			}


		}

		public void delete(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			database.nonQuery("DELETE FROM [Ship Order Line] WHERE [Entry No] = '"+entryNo+"' AND [Ship Order Entry No] = '"+shipOrderEntryNo+"'");
			
			
			ShipOrderLineIds shipOrderLineIds = new ShipOrderLineIds();
			DataSet shipOrderLineIdDataSet = shipOrderLineIds.getDataSet(database, shipOrderEntryNo, entryNo);
			int j = 0;
			while(j < shipOrderLineIdDataSet.Tables[0].Rows.Count)
			{
				synchQueue.enqueue(database, "", 10, shipOrderLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), 2);
				

				j++;
			}
			
			
			
			database.nonQuery("DELETE FROM [Ship Order Line ID] WHERE [Ship Order Line Entry No] = '"+entryNo+"' AND [Ship Order Entry No] = '"+shipOrderEntryNo+"'");
		}

	}
}
