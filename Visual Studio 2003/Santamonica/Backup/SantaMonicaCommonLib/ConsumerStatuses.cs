using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ConsumerStatuses.
	/// </summary>
	public class ConsumerStatuses
	{
		public ConsumerStatuses()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ConsumerStatus getEntry(Database database, string consumerNo)
		{
			ConsumerStatus consumerStatus = null;
			
			SqlDataReader dataReader = database.query("SELECT [Consumer No], [Inventory Level], [Updated Date], [Updated Time] FROM [Consumer Status] WHERE [Consumer No] = '"+consumerNo+"'");
			if (dataReader.Read())
			{
				consumerStatus = new ConsumerStatus(dataReader);
			}
			
			dataReader.Close();
			return consumerStatus;
		}

		public DataSet getDataSetEntry(Database database, string consumerNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Inventory Level], [Updated Date], [Updated Time] FROM [Consumer Status] WHERE [Consumer No] = '"+consumerNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerStatus");
			adapter.Dispose();

			return dataSet;

		}

		public void updateStatus(Database database, string consumerNo, float inventoryLevel)
		{

			ConsumerStatus consumerStatus = getEntry(database, consumerNo);
			if (consumerStatus == null)
			{
				consumerStatus = new ConsumerStatus();
				consumerStatus.consumerNo = consumerNo;
			}
			consumerStatus.inventoryLevel = inventoryLevel;
			consumerStatus.updatedDateTime = DateTime.Now;
			consumerStatus.save(database);

		}
	}
}
