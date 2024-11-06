using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class FactoryInventoryEntries
	{

		public FactoryInventoryEntries()
		{

		}


		public DataSet getDataSet(Database database, string factoryNo, int year, int weekNo)
		{
			DateTime monday = CalendarHelper.GetFirstDayOfWeek(year, weekNo);
			DateTime sunday = monday.AddDays(6);

			return getDataSet(database, factoryNo, monday, sunday);
		}

		public DataSet getPreOrderDataSet(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Factory No], [Type], [Status], [Date], [Time Of Day], [Line Order Entry No], [Container No], [Weight], [Closed], [Remaining Weight] FROM [Factory Inventory Entry] WHERE [Factory No] = '"+factoryNo+"' AND (([Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [Time Of Day] < '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] < '"+dateTime.ToString("yyyy-MM-dd")+"')) AND [Closed] = 0 ORDER BY [Date] DESC, [Time Of Day] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryCapacity");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getPostOrderDataSet(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Factory No], [Type], [Status], [Date], [Time Of Day], [Line Order Entry No], [Container No], [Weight], [Closed], [Remaining Weight] FROM [Factory Inventory Entry] WHERE [Factory No] = '"+factoryNo+"' AND (([Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [Time Of Day] > '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] > '"+dateTime.ToString("yyyy-MM-dd")+"')) AND [Closed] = 0 ORDER BY [Date], [Time Of Day]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryCapacity");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getDataSet(Database database, string factoryNo, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Factory No], [Type], [Status], [Date], [Time Of Day], [Line Order Entry No], [Container No], [Weight], [Closed], [Remaining Weight] FROM [Factory Inventory Entry] WHERE [Factory No] = '"+factoryNo+"' AND [Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Date] <= '"+endDate.ToString("yyyy-MM-dd")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryCapacity");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory No], [Type], [Status], [Date], [Time Of Day], [Line Order Entry No], [Container No], [Weight], [Closed], [Remaining Weight] FROM [Factory Inventory Entry] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [TimeOfDay] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryCapacity");
			adapter.Dispose();

			return dataSet;

		}

		public Hashtable getHashtable(Database database, string factoryNo, DateTime startDate, DateTime endDate)
		{
			Hashtable hashTable = new Hashtable();

			DataSet dataSet = this.getDataSet(database, factoryNo, startDate, endDate);

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				FactoryInventoryEntry factoryInventoryEntry = new FactoryInventoryEntry(dataSet.Tables[0].Rows[i]);
				
				ArrayList inventoryList = (ArrayList)hashTable[new DateTime(factoryInventoryEntry.date.Year, factoryInventoryEntry.date.Month, factoryInventoryEntry.date.Day, factoryInventoryEntry.timeOfDay.Hour, 0, 0)];
				if (inventoryList != null)
				{
					inventoryList.Add(factoryInventoryEntry);
				}
				else
				{
					inventoryList = new ArrayList();
					inventoryList.Add(factoryInventoryEntry);
					hashTable.Add(new DateTime(factoryInventoryEntry.date.Year, factoryInventoryEntry.date.Month, factoryInventoryEntry.date.Day, factoryInventoryEntry.timeOfDay.Hour, 0, 0), inventoryList);
				}

				i++;
			}


			return hashTable;
		}

		public FactoryInventoryEntry getEntry(Database database, int entryNo)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Factory No], [Type], [Status], [Date], [Time Of Day], [Line Order Entry No], [Container No], [Weight], [Closed], [Remaining Weight] FROM [Factory Inventory Entry] WHERE [Entry No] = '"+entryNo+"'");

			FactoryInventoryEntry factoryInventoryEntry = null;

			if (dataReader.Read())
			{

				factoryInventoryEntry = new FactoryInventoryEntry(dataReader);
			}
			dataReader.Close();

			return factoryInventoryEntry;
		}

		public FactoryInventoryEntry getLineOrderEntry(Database database, int lineOrderEntryNo)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Factory No], [Type], [Status], [Date], [Time Of Day], [Line Order Entry No], [Container No], [Weight], [Closed], [Remaining Weight] FROM [Factory Inventory Entry] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"'");

			FactoryInventoryEntry factoryInventoryEntry = null;

			if (dataReader.Read())
			{

				factoryInventoryEntry = new FactoryInventoryEntry(dataReader);
			}
			dataReader.Close();

			return factoryInventoryEntry;
		}

		public float calcInventoryTotal(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT SUM([Remaining Weight]) FROM [Factory Inventory Entry] WHERE [Factory No] = '"+factoryNo+"' AND (([Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [Time Of Day] < '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] < '"+dateTime.ToString("yyyy-MM-dd")+"')) AND [Closed] = 0");

			float weight = 0;

			if (dataReader.Read())
			{
				if (dataReader.GetValue(0).ToString() != "")
				{
					weight = float.Parse(dataReader.GetValue(0).ToString());
				}
			}
			dataReader.Close();

			return weight;
		}


		public void deleteLineOrderEntries(Database database, int lineOrderEntryNo)
		{
			database.nonQuery("DELETE FROM [Factory Inventory Entry] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"' AND [Closed] = 0");

		}

		public int countInventoryEntries(Database database, string factoryNo, DateTime date, DateTime timeOfDay)
		{
			int count = 0;

			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Factory Inventory Entry] WHERE [Factory No] = '"+factoryNo+"' AND [Type] = '0' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [Time Of Day] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;

		}

	}
}
