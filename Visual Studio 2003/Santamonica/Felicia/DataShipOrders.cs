using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataShipOrders
	{
		private SmartDatabase smartDatabase;

		public DataShipOrders(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataSet getDataSet()
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, customerName, city, status, '' as statusText, priority, shipName, shipCity, comments FROM shipOrder WHERE status <> 6 ORDER BY status, priority");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;
		}

		public int countPriority(int priority)
		{
			int count = 0;

			SqlCeDataReader dataReader = smartDatabase.query("SELECT COUNT(*) FROM shipOrder WHERE status <> 6 AND priority = '"+priority+"'");

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();

			return count;
		}


		public DataSet getDataSet(int priority)
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, customerName, city, status, '' as statusText, priority, shipName, shipCity, comments FROM shipOrder WHERE (status = 4) OR (status <> 6 AND priority = '"+priority+"') ORDER BY status, priority, shipCity");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getShippedDataSet(bool todaysShipOrdersOnly)
		{
			string dateFilter;
			if (todaysShipOrdersOnly) 
			{
				dateFilter = "AND shipDate >= '"+DateTime.Now.ToString("yyyy-MM-dd")+"' ";
			}
			else
			{
				dateFilter = "AND shipDate < '"+DateTime.Now.ToString("yyyy-MM-dd")+"' ";
			}


			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, customerName, city, status, '' as statusText FROM shipOrder WHERE status = 6 "+dateFilter+"ORDER BY shipDate DESC");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;
		}


		public DataSet getPrioDataSet()
		{
			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, customerName, city, status, '' as statusText, comments FROM shipOrder ORDER BY priority");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;
		}

		public bool checkIfNewShipOrders()
		{
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM shipOrder WHERE status = 4");
			bool result = false;

			if (dataReader.Read())
			{
				result = true;
			}
			dataReader.Close();
			dataReader.Dispose();

			return result;


		}

		public void deleteEntry(int entryNo)
		{
			smartDatabase.nonQuery("DELETE FROM shipOrder WHERE entryNo = '"+entryNo+"'");
			smartDatabase.nonQuery("DELETE FROM shipOrderLine WHERE shipOrderEntryNo = '"+entryNo+"'");
			smartDatabase.nonQuery("DELETE FROM shipOrderLineId WHERE shipOrderEntryNo = '"+entryNo+"'");

		}


		public void cleanUp()
		{
			DateTime fromDateTime = DateTime.Now.AddDays(-7);
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM shipOrder WHERE status = 6 AND shipDate < '"+fromDateTime.ToString("yyyy-MM-dd")+"'");
			while(dataReader.Read())
			{
				deleteEntry(dataReader.GetInt32(0));	
			}
			dataReader.Close();
		}

	}
}
