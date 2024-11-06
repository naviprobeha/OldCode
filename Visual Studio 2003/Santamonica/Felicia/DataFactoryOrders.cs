using System;
using System.Xml;
using System.Data;
using System.Data.SqlServerCe;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for DataItems.
	/// </summary>
	public class DataFactoryOrders
	{
		private SmartDatabase smartDatabase;

		public DataFactoryOrders(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
		}

		public DataSet getEntry(int entryNo)
		{		

			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, organizationNo, shipDate, factoryType, factoryNo, factoryName, factoryAddress, factoryAddress2, factoryPostCode, factoryCity, factoryCountryCode, factoryPhoneNo, consumerNo, consumerName, consumerAddress, consumerAddress2, consumerPostCode, consumerCity, consumerCountryCode, consumerPhoneNo, categoryCode, categoryDescription, quantity, realQuantity, factoryPositionX, factoryPositionY, consumerPositionX, consumerPositionY, type, status, closedDateTime, shipTime, creationDate, arrivalDateTime, agentCode, consumerLevel, loadDuration, loadWaitDuration, dropDuration, dropWaitDuration, phValueShipping, loadReasonValue, loadReasonText, dropReasonValue, dropReasonText, extraDist, extraTime FROM factoryOrder WHERE entryNo = '"+entryNo+"'");
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getActiveDataSet()
		{
			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipDate, factoryName, consumerName, consumerLevel, '' as statusText, status FROM factoryOrder WHERE status < 4 ORDER BY shipDate, status DESC, shipTime");
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getHistoryDataSet()
		{
			DataSet dataSet = new DataSet();

			SqlCeDataAdapter adapter = smartDatabase.dataAdapterQuery("SELECT entryNo, shipDate, factoryName, consumerName, consumerLevel FROM factoryOrder WHERE status = 4 ORDER BY shipDate DESC");
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}


		public void cleanUp()
		{
			DateTime fromDateTime = DateTime.Now.AddDays(-14);
			SqlCeDataReader dataReader = smartDatabase.query("SELECT entryNo FROM factoryOrder WHERE shipDate < '"+fromDateTime.ToString("yyyy-MM-dd")+"'");
			while(dataReader.Read())
			{
				deleteEntry(dataReader.GetInt32(0));	
			}
			dataReader.Close();
		}

		public void deleteEntry(int entryNo)
		{
			smartDatabase.nonQuery("DELETE FROM factoryOrder WHERE entryNo = '"+entryNo+"'");

		}
	}
}
