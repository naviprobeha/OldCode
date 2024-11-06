using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataShipmentLines.
	/// </summary>
	public class DataShipmentLineIds
	{
		private SmartDatabase smartDatabase;

		public DataShipmentLineIds(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getShipmentLineDataSet(DataShipmentLine dataShipmentLine)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipmentEntryNo, lineEntryNo, unitId, type, reMarkUnitId, bseTesting, postMortem, '' as bseTestingText, '' as postMortemText FROM shipmentLineId WHERE lineEntryNo = '"+dataShipmentLine.entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLineId");
			adapter.Dispose();

			return dataSet;
		
		}

		public DataSet getShipmentDataSet(DataShipmentHeader dataShipmentHeader)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipmentEntryNo, lineEntryNo, unitId, type, reMarkUnitId, bseTesting, postMortem FROM shipmentLineId WHERE shipmentEntryNo = '"+dataShipmentHeader.entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLineId");
			adapter.Dispose();

			return dataSet;
		
		}


		public bool checkIfIdsEntered(DataShipmentLine dataShipmentLine)
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM shipmentLineId WHERE lineEntryNo = '"+dataShipmentLine.entryNo+"'");
			bool result = false;

			if (dataReader.Read())
			{
				result = true;
			}
			dataReader.Close();
			dataReader.Dispose();

			return result;


		}

		public DataSet getEntriesDataSet(int shipmentNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipmentEntryNo, lineEntryNo, unitId, type, reMarkUnitId, bseTesting, postMortem FROM shipmentLineId WHERE shipmentEntryNo = '"+shipmentNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipmentLineId");
			adapter.Dispose();

			return dataSet;
		
		}

		public int countTestings(DataShipmentLine dataShipmentLine)
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM shipmentLineId WHERE lineEntryNo = '"+dataShipmentLine.entryNo+"' AND bseTesting = 1");
			
			int count = 0;
			if (dataReader.Read())
			{
				try
				{
					count = int.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception e)
				{
					if (e.Message != "") {}
				}
			}

			dataReader.Close();

			return count;
		}



	}
}
