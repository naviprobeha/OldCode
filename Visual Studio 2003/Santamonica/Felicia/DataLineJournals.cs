using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataLineJournals
	{
		private SmartDatabase smartDatabase;

		public DataLineJournals(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataSet getDataSet()
		{
			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, shipDate, agentCode, status, departureFactoryCode, arrivalFactoryCode, '' as statusText, measuredDistance, reportedDistance, '' as noOfLoadedOrders FROM lineJournal WHERE status < 8 ORDER BY shipDate, entryNo");
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getEntry(int entryNo)
		{
			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, shipDate, agentCode, status, departureFactoryCode, arrivalFactoryCode, calculatedDistance, measuredDistance, reportedDistance, reportedDistanceSingle, reportedDistanceTrailer, dropWaitTime FROM lineJournal WHERE entryNo = '"+entryNo+"'");
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;

		}


		public DataLineJournal getFirstLineJournal()
		{
			int entryNo = 0;

			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM lineJournal WHERE status < 6 ORDER BY shipDate");
			if (dataReader.Read())
			{
				entryNo = dataReader.GetInt32(0);
			}
			dataReader.Close();

			if (entryNo > 0)
			{
				return new DataLineJournal(smartDatabase, entryNo);
			}

			return null;

		}

		public void cleanUp()
		{
			DateTime fromDateTime = DateTime.Now.AddDays(-14);
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM lineJournal WHERE shipDate < '"+fromDateTime.ToString("yyyy-MM-dd")+"'");
			while(dataReader.Read())
			{
				deleteEntry(dataReader.GetInt32(0));	
			}
			dataReader.Close();
		}

		public void deleteEntry(int entryNo)
		{
			smartDatabase.nonQuery("DELETE FROM lineJournal WHERE entryNo = '"+entryNo+"'");

		}
	}
}
