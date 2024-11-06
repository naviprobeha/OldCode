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
	public class ConsumerCapacities
	{

		public ConsumerCapacities()
		{

		}


		public DataSet getDataSet(Database database, string consumerNo, int year, int weekNo)
		{
			DateTime monday = CalendarHelper.GetFirstDayOfWeek(year, weekNo);
			DateTime sunday = monday.AddDays(6);

			return getDataSet(database, consumerNo, monday, sunday);
		}

		public DataSet getDataSet(Database database, string consumerNo, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Date], [TimeOfDay], [Planned Capacity], [Actual Capacity] FROM [Consumer Capacity] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Date] <= '"+endDate.ToString("yyyy-MM-dd")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerCapacity");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string consumerNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Date], [TimeOfDay], [Planned Capacity], [Actual Capacity] FROM [Consumer Capacity] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [TimeOfDay] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerCapacity");
			adapter.Dispose();

			return dataSet;

		}

		public Hashtable getHashtable(Database database, string consumerNo, DateTime startDate, DateTime endDate)
		{
			Hashtable hashTable = new Hashtable();

			DataSet dataSet = this.getDataSet(database, consumerNo, startDate, endDate);

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				ConsumerCapacity consumerCapacity = new ConsumerCapacity(dataSet.Tables[0].Rows[i]);
				hashTable.Add(new DateTime(consumerCapacity.date.Year, consumerCapacity.date.Month, consumerCapacity.date.Day, consumerCapacity.timeOfDay.Hour, 0, 0), consumerCapacity);

				i++;
			}


			return hashTable;
		}

		public ConsumerCapacity getEntry(Database database, string consumerNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Consumer No], [Date], [TimeOfDay], [Planned Capacity], [Actual Capacity] FROM [Consumer Capacity] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			ConsumerCapacity consumerCapacity = null;

			if (dataReader.Read())
			{

				consumerCapacity = new ConsumerCapacity(dataReader);
			}
			dataReader.Close();

			return consumerCapacity;
		}

		public bool capacityExists(Database database, string consumerNo, DateTime dateTime)
		{

			SqlDataReader dataReader = database.query("SELECT [Consumer No] FROM [Consumer Capacity] WHERE [Consumer No] = '"+consumerNo+"' AND (([Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] > '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] > '"+dateTime.ToString("yyyy-MM-dd")+"')) AND [Planned Capacity] > 0");

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
