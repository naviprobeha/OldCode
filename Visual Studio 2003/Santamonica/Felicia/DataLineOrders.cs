using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataLineOrders
	{
		private SmartDatabase smartDatabase;

		public DataLineOrders(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataSet getActiveDataSet(DataLineJournal dataLineJournal)
		{
			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shippingCustomerName, city, comments, type, status, shipTime, ' ' as qtyContainers, details FROM lineOrder WHERE lineJournalEntryNo = '"+dataLineJournal.entryNo+"' AND status < 7 ORDER BY status, optimizingSortOrder");
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public int countLoadedOrders(DataLineJournal dataLineJournal)
		{
			DataSet dataSet = new DataSet();

			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM lineOrder WHERE lineJournalEntryNo = '"+dataLineJournal.entryNo+"' AND status >= 7");

			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());	

			}

			dataReader.Close();


			return count;

		}

		public DataSet getHistoryDataSet()
		{
			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shippingCustomerName, city, comments, type, status, shipTime, ' ' as qtyContainers FROM lineOrder WHERE status > 6 ORDER BY entryNo DESC");
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public int countContainers(int lineOrderEntryNo)
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM lineOrderContainer lc, container c WHERE lineOrderEntryNo = '"+lineOrderEntryNo+"' AND lc.containerNo = c.no");
			
			int qtyContainers = 0;
			if (dataReader.Read())
			{
				qtyContainers = dataReader.GetInt32(0);
			}

			dataReader.Close();

			return qtyContainers;
		}

		public void cleanUp()
		{
			DateTime fromDateTime = DateTime.Now.AddDays(-14);
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM lineOrder WHERE shipDate < '"+fromDateTime.ToString("yyyy-MM-dd")+"'");
			while(dataReader.Read())
			{
				deleteEntry(dataReader.GetInt32(0));	
			}
			dataReader.Close();
		}

		public void deleteEntry(int entryNo)
		{
			smartDatabase.nonQuery("DELETE FROM lineOrder WHERE entryNo = '"+entryNo+"'");
			smartDatabase.nonQuery("DELETE FROM lineOrderContainer WHERE lineOrderEntryNo = '"+entryNo+"'");

		}
	}
}
