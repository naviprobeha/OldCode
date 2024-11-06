using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataShipOrderLineIds
	{
		private SmartDatabase smartDatabase;

		public DataShipOrderLineIds(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}


		public DataSet getDataSet(int shipOrderEntryNo, int shipOrderLineEntryNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipOrderEntryNo, shipOrderLineEntryNo, unitId, bseTesting, postMortem FROM shipOrderLineId WHERE shipOrderEntryNo = '"+shipOrderEntryNo.ToString()+"' AND shipOrderLineEntryNo = '"+shipOrderLineEntryNo.ToString()+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrderLineId");
			adapter.Dispose();

			return dataSet;
		}

	}
}
