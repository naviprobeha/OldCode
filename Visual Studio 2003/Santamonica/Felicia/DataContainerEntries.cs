using System;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataContainerEntries.
	/// </summary>
	public class DataContainerEntries
	{
		private SmartDatabase smartDatabase;

		public DataContainerEntries(SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			this.smartDatabase = smartDatabase;
		}

		public DataSet getEntryDataSet(int entryNo)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, containerNo, type, entryDateTime, positionX, positionY, estimatedArrivalTime, locationCode, locationType, documentType, documentNo FROM containerEntry WHERE entryNo = '"+entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerEntry");
			adapter.Dispose();

			return dataSet;
		
		}

		public void deleteEntry(int entryNo)
		{
			smartDatabase.nonQuery("DELETE FROM containerEntry WHERE entryNo = '"+entryNo+"'");
					
		}

	}
}
