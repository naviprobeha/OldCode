using System;
using System.Collections;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class FactoryInventories
	{

		public FactoryInventories()
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
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No], [Volume], [Percent] FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND [Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Date] <= '"+endDate.ToString("yyyy-MM-dd")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryInventory");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string factoryNo, int type, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No], [Volume], [Percent] FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND [Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' AND [Type] = '"+type+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryInventory");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getDataSet(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No], [Volume], [Percent] FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [TimeOfDay] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryInventory");
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
				FactoryInventory factoryInventory = new FactoryInventory(dataSet.Tables[0].Rows[i]);
				hashTable.Add(new DateTime(factoryInventory.date.Year, factoryInventory.date.Month, factoryInventory.date.Day, factoryInventory.timeOfDay.Hour, 0, 0), factoryInventory);

				i++;
			}


			return hashTable;
		}

		public FactoryInventory getEntry(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Factory No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No], [Volume], [Percent] FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			FactoryInventory factoryInventory = null;

			if (dataReader.Read())
			{
				factoryInventory = new FactoryInventory(dataReader);
			}
			dataReader.Close();

			return factoryInventory;
		}

		public void deleteInventory(Database database, string factoryNo, DateTime fromDateTime, DateTime toDateTime)
		{
			string toDateQuery = "";
			
			if (toDateTime.Year > 1753) toDateQuery = " AND ([Date] < '"+toDateTime.ToString("yyyy-MM-dd")+"' OR ([Date] = '"+toDateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] <= '"+toDateTime.ToString("1754-01-01 HH:mm:ss")+"'))"; 
		
			database.nonQuery("DELETE FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND (([Date] = '"+fromDateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] >= '"+fromDateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] > '"+fromDateTime.ToString("yyyy-MM-dd")+"')) AND [Type] = 1 "+toDateQuery);

		}

		public FactoryInventory findLastActualEntry(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Factory No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No], [Volume], [Percent] FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND (([Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] < '"+dateTime.ToString("yyyy-MM-dd")+"')) AND [Type] = 0 ORDER BY [Date] DESC, [TimeOfDay] DESC");

			FactoryInventory factoryInventory = null;

			if (dataReader.Read())
			{
				factoryInventory = new FactoryInventory(dataReader);
			}
			dataReader.Close();

			return factoryInventory;
		}

		public FactoryInventory findNextActualEntry(Database database, string factoryNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Factory No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No], [Volume], [Percent] FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND (([Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] > '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] > '"+dateTime.ToString("yyyy-MM-dd")+"')) AND [Type] = 0 ORDER BY [Date], [TimeOfDay]");

			FactoryInventory factoryInventory = null;

			if (dataReader.Read())
			{
				factoryInventory = new FactoryInventory(dataReader);
			}
			dataReader.Close();

			return factoryInventory;
		}


	}
}
