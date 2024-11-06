using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataShipmentLines.
	/// </summary>
	public class DataOrderLines
	{
		private SmartDatabase smartDatabase;

		public DataOrderLines(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getOrderDataSet(DataOrderHeader dataOrderHeader)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, orderEntryNo, itemNo, description, quantity, connectionQuantity, Str(unitPrice, 8, 2) as formatedUnitPrice, Str(totalAmount, 8, 2) as formatedAmount FROM orderLine WHERE orderEntryNo = '"+dataOrderHeader.entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "orderLine");
			adapter.Dispose();

			return dataSet;
		
		}

		public DataSet getEntriesDataSet(int orderNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, orderEntryNo, itemNo, description, quantity, connectionQuantity, unitPrice, amount, connectionUnitPrice, connectionAmount, totalAmount, connectionItemNo FROM orderLine WHERE orderEntryNo = '"+orderNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "orderLine");
			adapter.Dispose();

			return dataSet;
		
		}

	}
}
