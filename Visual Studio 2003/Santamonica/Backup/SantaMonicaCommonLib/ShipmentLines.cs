using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLines.
	/// </summary>
	public class ShipmentLines
	{
		private Database database;
		private string agentCode;

		public ShipmentLines(Database database, string agentCode, DataSet shipmentLinesDataSet, DataSet shipmentLineIdsDataSet)
		{
			this.database = database;
			this.agentCode = agentCode;
			//
			// TODO: Add constructor logic here
			//
			fromDataSet(shipmentLinesDataSet, shipmentLineIdsDataSet);
			
		}

		public ShipmentLines(Database database)
		{
			this.database = database;

		}
		
		public void fromDataSet(DataSet dataset, DataSet idDataSet)
		{

			int i = 0;
			while (i < dataset.Tables[0].Rows.Count)
			{

				if (!originalLineExists(agentCode +"-"+ dataset.Tables[0].Rows[i].ItemArray.GetValue(1).ToString(), int.Parse(dataset.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())))
				{
					ShipmentLine shipmentLine = new ShipmentLine(database, agentCode, dataset.Tables[0].Rows[i]);
				
					ShipmentLineIds shipmentLineIds = new ShipmentLineIds(database, agentCode, shipmentLine.originalEntryNo, shipmentLine.entryNo, idDataSet);
				}

				i++;
			}

		}

		public DataSet getShipmentLinesDataSet(string shipmentNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Shipment No], [Item No], [Description], [Quantity], [Connection Quantity], [Unit Price], [Amount], [Connection Unit Price], [Connection Amount], [Total Amount], [Connection Item No], [Extra Payment], [Test Quantity] FROM [Shipment Line] WHERE [Shipment No] = '"+shipmentNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLine");
			adapter.Dispose();

			return dataSet;
			
		}

		public bool originalLineExists(string shipmentNo, int originalEntryNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Shipment Line] WHERE [Shipment No] = '"+shipmentNo+"' AND [Original Entry No] = '"+originalEntryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				return true;

			}
			dataReader.Close();

			return false;
		}


		public int countShipmentTestings(string shipmentNo)
		{
			SqlDataReader dataReader = database.query("SELECT SUM([Test Quantity]) FROM [Shipment Line] WHERE [Shipment No] = '"+shipmentNo+"'");

			int quantity = 0;

			if (dataReader.Read())
			{
				try
				{
					quantity = int.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception)
				{
					quantity = 0;
				}
			}

			dataReader.Close();

			return quantity;
		}

		public string getCategory(string shipmentNo)
		{
			Categories categories = new Categories();

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Shipment No], [Item No], [Description], [Quantity], [Connection Quantity], [Unit Price], [Amount], [Connection Unit Price], [Connection Amount], [Total Amount], [Connection Item No], [Extra Payment], [Test Quantity] FROM [Shipment Line] WHERE [Shipment No] = '"+shipmentNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLine");
			adapter.Dispose();

			Items items = new Items();

			string categoryCode = "";

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				Item item = items.getEntry(database, dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
				if (item.categoryCode != "")
				{
					if (categoryCode == "") categoryCode = item.categoryCode;
				
					Category currentCategory = categories.getEntry(database, categoryCode);
					Category itemCategory = categories.getEntry(database, item.categoryCode);
					if (itemCategory.higherThan(currentCategory)) categoryCode = item.categoryCode;
				}

				i++;
			}
			
			return categoryCode;
		}


		public float getWeight(string shipmentNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Shipment No], [Item No], [Description], [Quantity], [Connection Quantity], [Unit Price], [Amount], [Connection Unit Price], [Connection Amount], [Total Amount], [Connection Item No], [Extra Payment], [Test Quantity] FROM [Shipment Line] WHERE [Shipment No] = '"+shipmentNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLine");
			adapter.Dispose();

			Items items = new Items();
			ItemUnitOfMeasures itemUnitOfMeasures = new ItemUnitOfMeasures();

			int i = 0;
			float weight = 0;

			while (i < dataSet.Tables[0].Rows.Count)
			{
				Item item = items.getEntry(database, dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
				ItemUnitOfMeasure itemUnitOfMeasure = itemUnitOfMeasures.getEntry(database, item.no, item.unitOfMeasure);

				if (itemUnitOfMeasure != null)
				{
					if (itemUnitOfMeasure.quantity != 1)
					{
						weight = weight + (((float)itemUnitOfMeasure.quantity) * float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()));
					}
					else
					{
						weight = weight + float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString());
					}
				}
				else
				{
					weight = weight + float.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString());
				}

				i++;
			}
			
			return weight;
		}
	}
}
