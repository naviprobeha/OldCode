using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLines.
	/// </summary>
	public class LineOrderShipments
	{
		public LineOrderShipments()
		{

		}

		
		public DataSet getDataSet(Database database, int lineOrderEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Line Order Entry No], [Container No], [Shipment No] FROM [Line Order Shipment] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderShipment");
			adapter.Dispose();

			return dataSet;
			
		}

		public DataSet getDataSet(Database database, int lineOrderEntryNo, string containerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Line Order Entry No], [Container No], [Shipment No] FROM [Line Order Shipment] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"' AND [Container No] = '"+containerNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderShipment");
			adapter.Dispose();

			return dataSet;
			
		}



		public DataSet getContentDataSet(Database database, int lineOrderEntryNo, string containerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT l.[Entry No], l.[Line Order Entry No], l.[Container No], l.[Shipment No], s.[Entry No] as lineEntryNo, s.[Item No], s.[Description], s.[Quantity], s.[Test Quantity] FROM [Line Order Shipment] l, [Shipment Line] s WHERE l.[Line Order Entry No] = '"+lineOrderEntryNo+"' AND l.[Container No] = '"+containerNo+"' AND s.[Shipment No] = l.[Shipment No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderShipment");
			adapter.Dispose();

			return dataSet;
			
		}

		public LineOrderShipment getEntry(Database database, int lineOrderEntryNo, int entryNo)
		{
			LineOrderShipment lineOrderShipment = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Line Order Entry No], [Container No], [Shipment No] FROM [Line Order Shipment] WHERE [Entry No] = '"+entryNo+"' AND [Line Order Entry No] = '"+lineOrderEntryNo+"'");
			if (dataReader.Read())
			{
				lineOrderShipment = new LineOrderShipment(dataReader);
			}
			
			dataReader.Close();
			return lineOrderShipment;
		}

		public LineOrderShipment getEntry(Database database, string shipmentNo)
		{
			LineOrderShipment lineOrderShipment = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Line Order Entry No], [Container No], [Shipment No] FROM [Line Order Shipment] WHERE [Shipment No] = '"+shipmentNo+"'");
			if (dataReader.Read())
			{
				lineOrderShipment = new LineOrderShipment(dataReader);
			}
			
			dataReader.Close();
			return lineOrderShipment;
		}

		public DataSet getDataSetEntry(Database database, int entryNo)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Line Order Entry No], [Container No], [Shipment No] FROM [Line Order Shipment] WHERE [Entry No] = '"+entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderShipment");
			adapter.Dispose();

			return dataSet;
		}

	}
}
