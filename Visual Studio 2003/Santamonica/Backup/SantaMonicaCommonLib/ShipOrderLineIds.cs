using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLines.
	/// </summary>
	public class ShipOrderLineIds
	{
		private Database database;

		public ShipOrderLineIds()
		{
		}

		public ShipOrderLineIds(Database database, int shipOrderEntryNo, int shipOrderLineEntryNo, int originalLineEntryNo, DataSet shipmentLineIdsDataSet)
		{
			this.database = database;
			//
			// TODO: Add constructor logic here
			//
			fromDataSet(shipmentLineIdsDataSet, shipOrderEntryNo, shipOrderLineEntryNo, originalLineEntryNo);
			
		}

		
		public void fromDataSet(DataSet dataset, int shipOrderEntryNo, int shipOrderLineEntryNo, int originalLineEntryNo)
		{
			
			DataRow[] dataRow = dataset.Tables[0].Select("lineEntryNo = '"+originalLineEntryNo+"'");

			int i = 0;
			while (i < dataRow.Length)
			{

				ShipOrderLineId shipOrderLineId = new ShipOrderLineId(database, shipOrderEntryNo, shipOrderLineEntryNo, dataRow[i]);

				i++;
			}

		}

		public DataSet getDataSet(Database database, int shipOrderEntryNo, int shipOrderLineEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Ship Order Entry No], [Ship Order Line Entry No], [Unit ID], [BSE Testing], [Post Mortem] FROM [Ship Order Line ID] WHERE [Ship Order Entry No] = '"+shipOrderEntryNo+"' AND [Ship Order Line Entry No] = '"+shipOrderLineEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrderLineId");
			adapter.Dispose();

			return dataSet;
			
		}

		public ShipOrderLineId getEntry(Database database, int shipOrderEntryNo, int shipOrderLineEntryNo, int entryNo)
		{
			ShipOrderLineId shipOrderLineId = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Ship Order Entry No], [Ship Order Line Entry No], [Unit ID], [BSE Testing], [Post Mortem] FROM [Ship Order Line ID] WHERE [Entry No] = '"+entryNo+"' AND [Ship Order Entry No] = '"+shipOrderEntryNo+"' AND [Ship Order Line Entry No] = '"+shipOrderLineEntryNo+"'");
			if (dataReader.Read())
			{
				shipOrderLineId = new ShipOrderLineId(dataReader);
			}
			
			dataReader.Close();
			return shipOrderLineId;
		}

		public DataSet getDataSetEntry(Database database, int entryNo)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Ship Order Entry No], [Ship Order Line Entry No], [Unit ID], [BSE Testing], [Post Mortem] FROM [Ship Order Line ID] WHERE [Entry No] = '"+entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrderLineId");
			adapter.Dispose();

			return dataSet;
		}

	}
}
