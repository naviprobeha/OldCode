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
	public class FactoryCapacities
	{

		public FactoryCapacities()
		{

		}


		public DataSet getDataSet(Database database, string factoryNo, int year, int weekNo)
		{
			DateTime monday = CalendarHelper.GetFirstDayOfWeek(year, weekNo);
			DateTime sunday = monday.AddDays(6);

			return getDataSet(database, factoryNo, monday, sunday);
		}

		public DataSet getDataSet(Database database, string factoryNo, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory No], [Date], [Time Of Day], [Planned Capacity] FROM [Factory Capacity] WHERE [Factory No] = '"+factoryNo+"' AND [Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Date] <= '"+endDate.ToString("yyyy-MM-dd")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryCapacity");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory No], [Date], [Time Of Day], [Planned Capacity] FROM [Factory Capacity] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [Time Of Day] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [Time Of Day] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

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
				FactoryCapacity factoryCapacity = new FactoryCapacity(dataSet.Tables[0].Rows[i]);
				hashTable.Add(new DateTime(factoryCapacity.date.Year, factoryCapacity.date.Month, factoryCapacity.date.Day, factoryCapacity.timeOfDay.Hour, 0, 0), factoryCapacity);

				i++;
			}


			return hashTable;
		}

		public FactoryCapacity getEntry(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Factory No], [Date], [Time Of Day], [Planned Capacity] FROM [Factory Capacity] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [Time Of Day] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [Time Of Day] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			FactoryCapacity factoryCapacity = null;

			if (dataReader.Read())
			{

				factoryCapacity = new FactoryCapacity(dataReader);
			}
			dataReader.Close();

			return factoryCapacity;
		}

		public bool capacityEntriesExists(Database database, string factoryNo, DateTime startDate, DateTime startTimeOfDay)
		{

			SqlDataReader dataReader = database.query("SELECT [Factory No] FROM [Factory Capacity] WHERE [Factory No] = '"+factoryNo+"' AND (([Date] = '"+startDate.ToString("yyyy-MM-dd")+"' AND [Time Of Day] > '"+startTimeOfDay.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] = '"+startDate.ToString("yyyy-MM-dd")+"'))");
			if (dataReader.Read())
			{
				dataReader.Close();
				return true;
			}
			dataReader.Close();

			return false;

		}

	}
}
