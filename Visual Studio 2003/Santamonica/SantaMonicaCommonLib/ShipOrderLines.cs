using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLines.
	/// </summary>
	public class ShipOrderLines
	{
		private Database database;

		public ShipOrderLines()
		{
		}

		public ShipOrderLines(Database database, int shipOrderEntryNo, DataSet shipOrderLinesDataSet, DataSet shipOrderLineIdsDataSet)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			fromDataSet(shipOrderEntryNo, shipOrderLinesDataSet, shipOrderLineIdsDataSet);
			
		}

		public DataSet getDataSet(Database database, int shipOrderEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Ship Order Entry No], [Item No], [Quantity], [Connection Quantity], [Unit Price], [Amount], [Connection Unit Price], [Connection Amount], [Total Amount], [Connection Item No], [Test Quantity] FROM [Ship Order Line] WHERE [Ship Order Entry No] = '"+shipOrderEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrderLine");
			adapter.Dispose();

			return dataSet;
			
		}

		public ShipOrderLine getEntry(Database database, int shipOrderEntryNo, int entryNo)
		{
			ShipOrderLine shipOrderLine = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Ship Order Entry No], [Item No], [Quantity], [Connection Quantity], [Unit Price], [Amount], [Connection Unit Price], [Connection Amount], [Total Amount], [Connection Item No], [Test Quantity] FROM [Ship Order Line] WHERE [Entry No] = '"+entryNo+"' AND [Ship Order Entry No] = '"+shipOrderEntryNo+"'");
			if (dataReader.Read())
			{
				shipOrderLine = new ShipOrderLine(dataReader);
			}
			
			dataReader.Close();
			return shipOrderLine;
		}

		public DataSet getDataSetEntry(Database database, int entryNo)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Ship Order Entry No], [Item No], [Quantity], [Connection Quantity], [Unit Price], [Amount], [Connection Unit Price], [Connection Amount], [Total Amount], [Connection Item No], [Test Quantity] FROM [Ship Order Line] WHERE [Entry No] = '"+entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrderLine");
			adapter.Dispose();

			return dataSet;
		}

		
		public void fromDataSet(int shipOrderEntryNo, DataSet dataset, DataSet idDataSet)
		{

			int i = 0;
			while (i < dataset.Tables[0].Rows.Count)
			{

				ShipOrderLine shipOrderLine = new ShipOrderLine(database, dataset.Tables[0].Rows[i], shipOrderEntryNo);
				
				ShipOrderLineIds shipOrderLineIds = new ShipOrderLineIds(database, shipOrderEntryNo, shipOrderLine.entryNo, shipOrderLine.originalEntryNo, idDataSet);

				i++;
			}

		}

	}
}
