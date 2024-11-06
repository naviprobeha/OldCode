using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataShipmentLines.
	/// </summary>
	public class DataShipmentLines
	{
		private SmartDatabase smartDatabase;

		public DataShipmentLines(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getShipmentDataSet(DataShipmentHeader dataShipmentHeader)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipmentEntryNo, itemNo, description, quantity, connectionQuantity, Str(unitPrice, 8, 2) as formatedUnitPrice, Str(totalAmount, 8, 2) as formatedTotalAmount, extraPayment, testQuantity, Str(amount, 8, 2) as formatedAmount, Str(connectionAmount, 8, 2) as formatedConnectionAmount FROM shipmentLine WHERE shipmentEntryNo = '"+dataShipmentHeader.entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLine");
			adapter.Dispose();

			return dataSet;
		
		}

		public DataSet getEntriesDataSet(int shipmentNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipmentEntryNo, itemNo, description, quantity, connectionQuantity, unitPrice, amount, connectionUnitPrice, connectionAmount, totalAmount, connectionItemNo, extraPayment, testQuantity FROM shipmentLine WHERE shipmentEntryNo = '"+shipmentNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLine");
			adapter.Dispose();

			return dataSet;
		
		}

		public DataSet getReportDataSet(DateTime fromDate, DateTime toDate, string mobileUser)
		{
			string mobileUserQuery = "";
			if (mobileUser != "") mobileUserQuery = "AND h.mobileUserName = '"+mobileUser+"'";

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT shipmentEntryNo, customerNo, customerName, itemNo, description, quantity, Str(totalAmount, 8, 2) as formatedAmount, payment, l.entryNo, connectionQuantity, connectionAmount, h.containerNo, testQuantity FROM shipmentHeader h, shipmentLine l WHERE l.shipmentEntryNo = h.entryNo AND shipDate >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND shipDate < '"+toDate.AddDays(1).ToString("yyyy-MM-dd")+"' AND h.status > 0 "+mobileUserQuery+" ORDER BY shipmentEntryNo");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLine");
			adapter.Dispose();

			return dataSet;
		
		}

		public int countItems(string itemNo, DateTime fromDate, DateTime toDate)
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT connectionQuantity FROM shipmentHeader h, shipmentLine l WHERE l.shipmentEntryNo = h.entryNo AND shipDate >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND shipDate < '"+toDate.AddDays(1).ToString("yyyy-MM-dd")+"' AND h.status > 0 AND l.connectionItemNo = '"+itemNo+"'");
			
			int connectionQuantity = 0;

			while(dataReader.Read())
			{
				try
				{
					connectionQuantity = connectionQuantity + int.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception e)
				{
					if (e.Message != "") {}

				}
			}

			dataReader.Close();


			dataReader = smartDatabase.query("SELECT quantity FROM shipmentHeader h, shipmentLine l WHERE l.shipmentEntryNo = h.entryNo AND shipDate >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND shipDate < '"+toDate.AddDays(1).ToString("yyyy-MM-dd")+"' AND h.status > 0 AND l.itemNo = '"+itemNo+"'");
			
			int quantity = 0;

			while (dataReader.Read())
			{
				try
				{
					quantity = quantity + int.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception e)
				{
					if (e.Message != "") {}

				}
			}

			dataReader.Close();



			return connectionQuantity + quantity;
		
		}
	}
}
