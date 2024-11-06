using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLines.
	/// </summary>
	public class ShipmentLineIds
	{
		private Database database;
		private string agentCode;

		public ShipmentLineIds(Database database, string agentCode, int originalLineEntryNo, int newLineEntryNo, DataSet shipmentLineIdsDataSet)
		{
			this.database = database;
			this.agentCode = agentCode;
			//
			// TODO: Add constructor logic here
			//
			fromDataSet(shipmentLineIdsDataSet, originalLineEntryNo, newLineEntryNo);
			
		}

		public ShipmentLineIds(Database database)
		{
			this.database = database;

		}
		
		public void fromDataSet(DataSet dataset, int originalLineEntryNo, int newLineEntryNo)
		{
			
			DataRow[] dataRow = dataset.Tables[0].Select("lineEntryNo = '"+originalLineEntryNo+"'");

			int i = 0;
			while (i < dataRow.Length)
			{
				if (!originalLineExists(agentCode +"-"+dataRow[i].ItemArray.GetValue(1).ToString(), int.Parse(dataRow[i].ItemArray.GetValue(0).ToString())))
				{

					ShipmentLineId shipmentLineId = new ShipmentLineId(database, agentCode, newLineEntryNo, dataRow[i]);
				}
				i++;
			}

		}

		public DataSet getShipmentLineIdDataSet(string shipmentNo, int lineEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Shipment No], [Shipment Line Entry No], [Unit ID], [Type], [ReMark Unit ID], [BSE Testing], [Post Mortem] FROM [Shipment Line ID] WHERE [Shipment No] = '"+shipmentNo+"' AND [Shipment Line Entry No] = '"+lineEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLineId");
			adapter.Dispose();

			return dataSet;
			
		}

		public bool originalLineExists(string shipmentNo, int originalEntryNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Shipment Line ID] WHERE [Shipment No] = '"+shipmentNo+"' AND [Original Entry No] = '"+originalEntryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				return true;

			}
			dataReader.Close();

			return false;
		}


		public bool shipmentContainsPostMortem(string shipmentNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Shipment No], [Shipment Line Entry No], [Unit ID], [Type], [ReMark Unit ID], [BSE Testing], [Post Mortem] FROM [Shipment Line ID] WHERE [Shipment No] = '"+shipmentNo+"' AND [Post Mortem] = 1");

			bool found = false;

			if (dataReader.Read())
			{
				found = true;
			}
			
			dataReader.Close();

			return found;
		}


		public DataSet getMarkedShipmentLineIdDataSet(string shipmentNo, int lineEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Shipment No], [Shipment Line Entry No], [Unit ID], [Type], [ReMark Unit ID], [BSE Testing], [Post Mortem] FROM [Shipment Line ID] WHERE [Shipment No] = '"+shipmentNo+"' AND [Shipment Line Entry No] = '"+lineEntryNo+"' AND [ReMark Unit ID] <> ''");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLineId");
			adapter.Dispose();

			return dataSet;
			
		}
	}
}
