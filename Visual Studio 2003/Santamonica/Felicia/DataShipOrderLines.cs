using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataShipOrderLines
	{
		private SmartDatabase smartDatabase;

		public DataShipOrderLines(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}



		public DataSet getDataSet(int shipOrderEntryNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipOrderEntryNo, itemNo, quantity, connectionQuantity, unitPrice, amount, connectionUnitPrice, connectionAmount, totalAmount, connectionItemNo, testQuantity FROM shipOrderLine WHERE shipOrderEntryNo = '"+shipOrderEntryNo.ToString()+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrderLine");
			adapter.Dispose();

			return dataSet;
		}

	}
}
