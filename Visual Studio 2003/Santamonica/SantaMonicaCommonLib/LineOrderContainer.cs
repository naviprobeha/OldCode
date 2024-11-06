using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLine.
	/// </summary>
	public class LineOrderContainer
	{

		public int entryNo;
		public int lineOrderEntryNo;
		public string containerNo;
		public string categoryCode;
		public float weight;
		public float realWeight;
		public DateTime scaledDateTime;
		public bool isScaled;
		public bool isSentToScaling;

		private string updateMethod;

		public LineOrderContainer(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//			
			this.entryNo = dataReader.GetInt32(0);
			this.lineOrderEntryNo = dataReader.GetInt32(1);
			this.containerNo = dataReader.GetValue(2).ToString();
			this.categoryCode = dataReader.GetValue(3).ToString();
			this.weight = float.Parse(dataReader.GetValue(4).ToString());
			this.realWeight = float.Parse(dataReader.GetValue(5).ToString());

			DateTime scaledDate = dataReader.GetDateTime(6);
			DateTime scaledTime = dataReader.GetDateTime(7);
			this.scaledDateTime = new DateTime(scaledDate.Year, scaledDate.Month, scaledDate.Day, scaledTime.Hour, scaledTime.Minute, scaledTime.Second);

			this.isScaled = false;
			if (dataReader.GetValue(8).ToString() == "1") this.isScaled = true;

			this.isSentToScaling = false;
			if (dataReader.GetValue(9).ToString() == "1") this.isSentToScaling = true;

		}

		public LineOrderContainer(LineOrder lineOrder)
		{

			this.lineOrderEntryNo = lineOrder.entryNo;
			this.scaledDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
		}

		public LineOrderContainer(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			lineOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			containerNo = dataRow.ItemArray.GetValue(2).ToString();
			categoryCode = dataRow.ItemArray.GetValue(3).ToString();
			weight = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
			realWeight = float.Parse(dataRow.ItemArray.GetValue(5).ToString());

			DateTime scaledDate = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());
			DateTime scaledTime = DateTime.Parse(dataRow.ItemArray.GetValue(7).ToString());
			this.scaledDateTime = new DateTime(scaledDate.Year, scaledDate.Month, scaledDate.Day, scaledTime.Hour, scaledTime.Minute, scaledTime.Second);


			this.isScaled = false;
			if (dataRow.ItemArray.GetValue(8).ToString() == "1") this.isScaled = true;

			this.isSentToScaling = false;
			if (dataRow.ItemArray.GetValue(9).ToString() == "1") this.isSentToScaling = true;

		}


		public void save(Database database)
		{
			int isScaledValue = 0;
			if (isScaled) isScaledValue = 1;
			int isSentToScalingValue = 0;
			if (isSentToScaling) isSentToScalingValue = 1;

			LineOrders lineOrders = new LineOrders();
			LineOrder lineOrder = lineOrders.getEntry(database, lineOrderEntryNo.ToString());
			
			SqlDataReader dataReader = database.query("SELECT * FROM [Line Order Container] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Line Order Container] WHERE [Entry No] = '"+entryNo+"'");
					if (lineOrder != null) lineOrder.updateOrderDeleteContainer(database, entryNo);

				}

				else
				{

					database.nonQuery("UPDATE [Line Order Container] SET [Container No] = '"+this.containerNo+"', [Category Code] = '"+this.categoryCode+"', [Weight] = '"+this.weight.ToString().Replace(",", ".")+"', [Real Weight] = '"+this.realWeight.ToString().Replace(",", ".")+"', [Scaled Date] = '"+scaledDateTime.ToString("yyyy-MM-dd")+"', [Scaled Time] = '"+scaledDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Is Scaled] = '"+isScaledValue+"', [Is Sent To Scaling] = '"+isSentToScalingValue+"' WHERE [Entry No] = '"+this.entryNo+"' AND [Line Order Entry No] = '"+this.lineOrderEntryNo+"'");

				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Line Order Container] ([Line Order Entry No], [Container No], [Category Code], [Weight], [Real Weight], [Scaled Date], [Scaled Time], [Is Scaled], [Is Sent To Scaling]) VALUES ('"+this.lineOrderEntryNo+"','"+this.containerNo+"','"+this.categoryCode+"', '"+this.weight.ToString().Replace(",", ".")+"', '"+this.realWeight.ToString().Replace(",", ".")+"', '"+scaledDateTime.ToString("yyyy-MM-dd")+"', '"+scaledDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+isScaledValue+"', '"+isSentToScalingValue+"')");
				entryNo = (int)database.getInsertedSeqNo();

			}

			if (lineOrder != null)
			{
				if (lineOrder.lineJournalEntryNo > 0)
				{
					LineJournal lineJournal = lineOrder.getJournal(database);
					if (lineJournal != null)
					{
						if (lineJournal.status < 7)
						{
							lineJournal.status = 1;
							lineJournal.save(database);
						}
					}
				}
				lineOrder.updateOrder(database);
			}




		}

		public void delete(Database database)
		{
			updateMethod = "D";
			save(database);
		}

		public void updateCategoryInformation(Database database)
		{
			string currentCategoryCode = "";

			Categories categories = new Categories();
			LineOrderShipments lineOrderShipments = new LineOrderShipments();

			DataSet lineOrderShipmentDataSet = lineOrderShipments.getDataSet(database, this.lineOrderEntryNo, this.containerNo);
			int i = 0;
			while (i < lineOrderShipmentDataSet.Tables[0].Rows.Count)
			{
				LineOrderShipment lineOrderShipment = new LineOrderShipment(database, lineOrderShipmentDataSet.Tables[0].Rows[i]);

				ShipmentLines shipmentLines = new ShipmentLines(database);			

				string shipmentCategoryCode = shipmentLines.getCategory(lineOrderShipment.shipmentNo);

				if (currentCategoryCode == "") currentCategoryCode = shipmentCategoryCode;


				if (currentCategoryCode != "")
				{
					Category currentCategory = categories.getEntry(database, currentCategoryCode);
					Category shipmentCategory = categories.getEntry(database, shipmentCategoryCode);
					if (shipmentCategory != null)
					{
						if (shipmentCategory.higherThan(currentCategory)) currentCategoryCode = shipmentCategoryCode;
					}
				}

				i++;
			}

			this.categoryCode = currentCategoryCode;
			save(database);
		}

		public void updateWeight(Database database)
		{

			Categories categories = new Categories();
			LineOrderShipments lineOrderShipments = new LineOrderShipments();

			DataSet lineOrderShipmentDataSet = lineOrderShipments.getDataSet(database, this.lineOrderEntryNo, this.containerNo);
			int i = 0;
			float weight = 0;

			while (i < lineOrderShipmentDataSet.Tables[0].Rows.Count)
			{
				LineOrderShipment lineOrderShipment = new LineOrderShipment(database, lineOrderShipmentDataSet.Tables[0].Rows[i]);

				ShipmentLines shipmentLines = new ShipmentLines(database);			
				
				float subWeight = shipmentLines.getWeight(lineOrderShipment.shipmentNo);
				//Console.WriteLine("Shipment: "+lineOrderShipment.shipmentNo+", Weight: "+subWeight.ToString());
				weight = weight + subWeight;

				
				i++;
			}

			this.weight = weight;

			save(database);
		}

		public Category getCategory(Database database)
		{
			Categories categories = new Categories();
			return categories.getEntry(database, this.categoryCode);
		}

		public int countTestings(Database database)
		{
			int quantity = 0;
			
			/*
			LineOrderShipments lineOrderShipments = new LineOrderShipments();
			DataSet shipmentDataSet = lineOrderShipments.getDataSet(database, this.lineOrderEntryNo, this.containerNo);

			int i = 0;
			while (i < shipmentDataSet.Tables[0].Rows.Count)
			{
				LineOrderShipment lineOrderShipment = new LineOrderShipment(database, shipmentDataSet.Tables[0].Rows[i]);

				
				ShipmentLines shipmentLines = new ShipmentLines(database);
				int qty = shipmentLines.countShipmentTestings(lineOrderShipment.shipmentNo);

				quantity = quantity + qty;
			
			
				i++;
			}
			*/

			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order Shipment] s, [Shipment Line ID] i WHERE s.[Shipment No] = i.[Shipment No] AND i.[BSE Testing] = 1 AND s.[Line Order Entry No] = '"+this.lineOrderEntryNo+"'");
			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) quantity = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return quantity;
		}

		public bool containsPostMortem(Database database)
		{
			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			DataSet shipmentDataSet = shipmentHeaders.getLineOrderDataSet(database, this.lineOrderEntryNo.ToString());

			int i = 0;
			while (i < shipmentDataSet.Tables[0].Rows.Count)
			{
				ShipmentHeader shipmentHeader = new ShipmentHeader(shipmentDataSet.Tables[0].Rows[i]);
				if (shipmentHeader.containsPostMortems(database) == true) return true;

				i++;
			}

			return false;
		}

		public void updateInventory(Database database)
		{
			
			LineOrders lineOrders = new LineOrders();
			LineOrder lineOrder = lineOrders.getEntry(database, lineOrderEntryNo.ToString());
				
			if (lineOrder == null) return;

			LineJournal lineJournal = lineOrder.getJournal(database);
				
			if (lineJournal == null) return;
				
			updateInventory(database, lineJournal);
			

		}

		public void updateInventory(Database database, LineJournal lineJournal)
		{
			
			FactoryInventoryEntries factoryInventoryEntries = new FactoryInventoryEntries();
			FactoryInventoryEntry factoryInventoryEntry = factoryInventoryEntries.getLineOrderEntry(database, this.lineOrderEntryNo);

			if (factoryInventoryEntry == null)
			{
				factoryInventoryEntry = new FactoryInventoryEntry();
				factoryInventoryEntry.lineOrderEntryNo = lineOrderEntryNo;
				factoryInventoryEntry.containerNo = containerNo;
				factoryInventoryEntry.status = 0;						
			}

			if (factoryInventoryEntry.status == 0)
			{
							
				factoryInventoryEntry.factoryNo = lineJournal.arrivalFactoryCode;
				factoryInventoryEntry.date = lineJournal.arrivalDateTime;
				factoryInventoryEntry.timeOfDay = new DateTime(1754, 1, 1, lineJournal.arrivalDateTime.AddHours(1).Hour, 0, 0);
				factoryInventoryEntry.type = 0;
				factoryInventoryEntry.checkCapacity(database);

			}


			if (isScaled)
			{
				factoryInventoryEntry.date = this.scaledDateTime;
				factoryInventoryEntry.timeOfDay = new DateTime(1754, 1, 1, this.scaledDateTime.AddHours(1).Hour, 0, 0);
				factoryInventoryEntry.weight = realWeight * 1000;
				factoryInventoryEntry.remainingWeight = realWeight * 1000;
				factoryInventoryEntry.status = 1;
			}
			else
			{
				factoryInventoryEntry.weight = this.weight;
				factoryInventoryEntry.remainingWeight = weight;
				factoryInventoryEntry.status = 0;
			}
			factoryInventoryEntry.save(database);
			

		}

		public int countOrderContainers(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order Container] WHERE [Line Order Entry No] = '"+this.lineOrderEntryNo+"'");

			int count = 0;

			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) count = int.Parse(dataReader.GetValue(0).ToString());

			}

			dataReader.Close();
			return count;

		}

		public int countScaledOrderContainers(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order Container] WHERE [Line Order Entry No] = '"+this.lineOrderEntryNo+"' AND [Is Scaled] = 1");

			int count = 0;

			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) count = int.Parse(dataReader.GetValue(0).ToString());

			}

			dataReader.Close();
			return count;

		}

		public string getScalingStatus(Database database, LineOrder lineOrder)
		{
			LineJournal lineJournal = lineOrder.getJournal(database);
			if (lineJournal != null)
			{
				if (lineJournal.status >= 7)
				{
					if (this.isSentToScaling)
					{
						if (this.isScaled) return "Invägd";
						return "Inväntar vikt";
					}
					return "Uppköad";
				}
			}
			return "";
		}


	}
}
