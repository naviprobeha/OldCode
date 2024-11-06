using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataShipmentLines.
	/// </summary>
	public class DataOrderLineIds
	{
		private SmartDatabase smartDatabase;

		public DataOrderLineIds(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getOrderLineDataSet(DataOrderLine dataOrderLine)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, orderEntryNo, lineEntryNo, unitId, bseTesting, postMortem, '' as bseTestingText, '' as postMortemText FROM orderLineId WHERE lineEntryNo = '"+dataOrderLine.entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLineId");
			adapter.Dispose();

			return dataSet;
		
		}

		public DataSet getOrderDataSet(DataOrderHeader dataOrderHeader)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, orderEntryNo, lineEntryNo, unitId, bseTesting, postMortem FROM orderLineId WHERE orderEntryNo = '"+dataOrderHeader.entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLineId");
			adapter.Dispose();

			return dataSet;
		
		}


		public bool checkIfIdsEntered(DataOrderLine dataOrderLine)
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM orderLineId WHERE lineEntryNo = '"+dataOrderLine.entryNo+"'");
			bool result = false;

			if (dataReader.Read())
			{
				result = true;
			}
			dataReader.Close();
			dataReader.Dispose();

			return result;


		}

		public DataSet getEntriesDataSet(int orderNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, orderEntryNo, lineEntryNo, unitId, bseTesting, postMortem FROM orderLineId WHERE orderEntryNo = '"+orderNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLineId");
			adapter.Dispose();

			return dataSet;
		
		}

	}
}
