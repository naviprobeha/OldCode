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
	public class ConsumerInventories
	{

		public ConsumerInventories()
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
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No] FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Date] <= '"+endDate.ToString("yyyy-MM-dd")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerInventory");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string consumerNo, int type, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No] FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' AND [Type] = '"+type+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerInventory");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getDataSet(Database database, string consumerNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No] FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [TimeOfDay] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerInventory");
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
				ConsumerInventory consumerInventory = new ConsumerInventory(dataSet.Tables[0].Rows[i]);
				hashTable.Add(new DateTime(consumerInventory.date.Year, consumerInventory.date.Month, consumerInventory.date.Day, consumerInventory.timeOfDay.Hour, 0, 0), consumerInventory);

				i++;
			}


			return hashTable;
		}

		public ConsumerInventory getEntry(Database database, string consumerNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Consumer No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No] FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			ConsumerInventory consumerInventory = null;

			if (dataReader.Read())
			{
				consumerInventory = new ConsumerInventory(dataReader);
			}
			dataReader.Close();

			return consumerInventory;
		}

		public void deleteInventory(Database database, string consumerNo, DateTime fromDateTime, DateTime toDateTime)
		{
			string toDateQuery = "";
			
			if (toDateTime.Year > 1753) toDateQuery = " AND ([Date] < '"+toDateTime.ToString("yyyy-MM-dd")+"' OR ([Date] = '"+toDateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] <= '"+toDateTime.ToString("1754-01-01 HH:mm:ss")+"'))"; 
		
			database.nonQuery("DELETE FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND (([Date] = '"+fromDateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] >= '"+fromDateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] > '"+fromDateTime.ToString("yyyy-MM-dd")+"')) AND [Type] = 1 "+toDateQuery);

		}

		public ConsumerInventory findLastActualEntry(Database database, string consumerNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Consumer No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No] FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND (([Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] < '"+dateTime.ToString("yyyy-MM-dd")+"')) AND [Type] = 0 ORDER BY [Date] DESC, [TimeOfDay] DESC");

			ConsumerInventory consumerInventory = null;

			if (dataReader.Read())
			{
				consumerInventory = new ConsumerInventory(dataReader);
			}
			dataReader.Close();

			return consumerInventory;
		}

		public ConsumerInventory findNextActualEntry(Database database, string consumerNo, DateTime dateTime)
		{
		
			SqlDataReader dataReader = database.query("SELECT [Consumer No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No] FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND (([Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] > '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Date] > '"+dateTime.ToString("yyyy-MM-dd")+"')) AND [Type] = 0 ORDER BY [Date], [TimeOfDay]");

			ConsumerInventory consumerInventory = null;

			if (dataReader.Read())
			{
				consumerInventory = new ConsumerInventory(dataReader);
			}
			dataReader.Close();

			return consumerInventory;
		}

		public void recalculateInventories(Database database, string consumerNo, DateTime dateTime)
		{
			ConsumerInventories consumerInventories = new ConsumerInventories();
			DateTime currentDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0);
			ConsumerCapacities consumerCapacities = new ConsumerCapacities();
			ConsumerInventory consumerInventory = consumerInventories.findLastActualEntry(database, consumerNo, dateTime);

			if (consumerInventory != null)
			{
				DateTime endingDateTime = currentDateTime;
				currentDateTime = new DateTime(consumerInventory.date.Year, consumerInventory.date.Month, consumerInventory.date.Day, consumerInventory.timeOfDay.Hour, 0, 0);


				ConsumerInventory nextConsumerInventory = consumerInventories.findNextActualEntry(database, consumerNo, dateTime.AddHours(1));
				if (nextConsumerInventory != null)
				{
					endingDateTime = new DateTime(nextConsumerInventory.date.Year, nextConsumerInventory.date.Month, nextConsumerInventory.date.Day, nextConsumerInventory.timeOfDay.Hour, 0, 0);
					consumerInventories.deleteInventory(database, consumerNo, currentDateTime.AddHours(1), endingDateTime);
				}
				else
				{
					consumerInventories.deleteInventory(database, consumerNo, currentDateTime.AddHours(1), new DateTime(1753, 1, 1));

					FactoryOrders factoryOrders = new FactoryOrders();
					FactoryOrder factoryOrder = factoryOrders.findLastConsumerEntry(database, consumerNo);
					if (factoryOrder != null)
					{
						endingDateTime = factoryOrder.arrivalDateTime;
					}
					
				}

				

				bool positiveInventory = false;
				if (consumerInventory.inventory > 0) positiveInventory = true;
				float inventory = consumerInventory.inventory;

				//throw new Exception("CurrDate: "+currentDateTime.ToString("yyyy-MM-dd HH:mm")+", EndingDate: "+endingDateTime.ToString("yyyy-MM-dd HH:mm")+", "+positiveInventory.ToString());
				while ((positiveInventory) || (currentDateTime < endingDateTime))
				{

					currentDateTime = currentDateTime.AddHours(1);

					if (!consumerCapacities.capacityExists(database, consumerNo, currentDateTime)) return;
					inventory = calcInventory(database, consumerNo, currentDateTime, inventory);

					if (inventory == 0)
					{
						positiveInventory = false;	
					}
					else
					{
						positiveInventory = true;
					}

				}
				
			
			}

		

		}

		private float calcInventory(Database database, string consumerNo, DateTime currentDateTime, float inventory)
		{
			ConsumerInventories consumerInventories = new ConsumerInventories();
			float prevInventory = inventory;
			ConsumerCapacities consumerCapacities = new ConsumerCapacities();
			if (!consumerCapacities.capacityExists(database, consumerNo, currentDateTime)) return 0;

			ConsumerInventory dateInventory = consumerInventories.getEntry(database, consumerNo, currentDateTime);
			if (dateInventory == null)
			{
				dateInventory = new ConsumerInventory();
				dateInventory.type = 1;
						
			}
			if (dateInventory.type == 0)
			{
				return dateInventory.inventory;
			}
			else
			{
				dateInventory.consumerNo = consumerNo;
				dateInventory.date = currentDateTime;
				dateInventory.timeOfDay = new DateTime(1754, 1, 1, currentDateTime.Hour, 0, 0);						
							
				ConsumerCapacity consumerCapacity = consumerCapacities.getEntry(database, consumerNo, currentDateTime);
				if (consumerCapacity != null)
				{
					if (consumerCapacity.actualCapacity > 0)
					{
						inventory = inventory - consumerCapacity.actualCapacity;
					}
					else
					{
						inventory = inventory - consumerCapacity.plannedCapacity;
					}

					FactoryOrders factoryOrders = new FactoryOrders();
					
					DataSet factoryOrderDataSet = factoryOrders.getConsumerEntries(database, consumerNo, currentDateTime);
					int i = 0;
					while (i < factoryOrderDataSet.Tables[0].Rows.Count)
					{
						FactoryOrder factoryOrder = new FactoryOrder(factoryOrderDataSet.Tables[0].Rows[i]);
						inventory = inventory + factoryOrder.quantity;
						i++;
					}

					if (inventory < 0) inventory = 0;
				}

				dateInventory.inventory = inventory;
				if (prevInventory != inventory)
				{
					dateInventory.save(database);
				}
			}

			return inventory;
		}


		public void recalculateInventories_old(Database database, string consumerNo, DateTime dateTime)
		{
			DateTime currentDateTime = dateTime;
			ConsumerCapacities consumerCapacities = new ConsumerCapacities();
			ConsumerInventory consumerInventory = findLastActualEntry(database, consumerNo, dateTime);

			if (consumerInventory != null)
			{
				DateTime endingDateTime = currentDateTime;

				ConsumerInventory nextConsumerInventory = findNextActualEntry(database, consumerNo, dateTime);
				if (nextConsumerInventory != null)
				{
					endingDateTime = new DateTime(nextConsumerInventory.date.Year, nextConsumerInventory.date.Month, nextConsumerInventory.date.Day, nextConsumerInventory.timeOfDay.Hour, 0, 0);
					deleteInventory(database, consumerNo, currentDateTime, endingDateTime);
				}
				else
				{
					deleteInventory(database, consumerNo, currentDateTime, new DateTime(1753, 1, 1));

					FactoryOrders factoryOrders = new FactoryOrders();
					FactoryOrder factoryOrder = factoryOrders.findLastConsumerEntry(database, consumerNo);
					if (factoryOrder != null)
					{
						endingDateTime = factoryOrder.arrivalDateTime;
					}
					
				}

				

				bool positiveInventory = false;
				if (consumerInventory.inventory > 0) positiveInventory = true;
				float inventory = consumerInventory.inventory;

				//throw new Exception("CurrDate: "+currentDateTime.ToString("yyyy-MM-dd HH:mm")+", EndingDate: "+endingDateTime.ToString("yyyy-MM-dd HH:mm")+", "+positiveInventory.ToString());
				while ((positiveInventory) || (currentDateTime < endingDateTime))
				{

					currentDateTime = currentDateTime.AddHours(1);

					if (!consumerCapacities.capacityExists(database, consumerNo, currentDateTime)) return;
					inventory = calcInventory(database, consumerNo, currentDateTime, inventory);

					if (inventory == 0)
					{
						positiveInventory = false;	
					}
					else
					{
						positiveInventory = true;
					}


				}
				
			
			}

		

		}

		private float calcInventory_old(Database database, string consumerNo, DateTime currentDateTime, float inventory)
		{
			float prevInventory = inventory;
			ConsumerCapacities consumerCapacities = new ConsumerCapacities();
			if (!consumerCapacities.capacityExists(database, consumerNo, currentDateTime)) return 0;

			ConsumerInventory dateInventory = getEntry(database, consumerNo, currentDateTime);
			if (dateInventory == null)
			{
				dateInventory = new ConsumerInventory();
				dateInventory.type = 1;
						
			}
			if (dateInventory.type == 0)
			{
				return 0;
			}
			else
			{
				dateInventory.consumerNo = consumerNo;
				dateInventory.date = currentDateTime;
				dateInventory.timeOfDay = new DateTime(1754, 1, 1, currentDateTime.Hour, 0, 0);						
							
				ConsumerCapacity consumerCapacity = consumerCapacities.getEntry(database, consumerNo, currentDateTime);
				if (consumerCapacity != null)
				{
					if (consumerCapacity.actualCapacity > 0)
					{
						inventory = inventory - consumerCapacity.actualCapacity;
					}
					else
					{
						inventory = inventory - consumerCapacity.plannedCapacity;
					}

					FactoryOrders factoryOrders = new FactoryOrders();
					DataSet factoryOrderDataSet = factoryOrders.getConsumerEntries(database, consumerNo, currentDateTime);
					int i = 0;
					while (i < factoryOrderDataSet.Tables[0].Rows.Count)
					{
						FactoryOrder factoryOrder = new FactoryOrder(factoryOrderDataSet.Tables[0].Rows[i]);
						inventory = inventory + factoryOrder.quantity;
						i++;
					}

					if (inventory < 0) inventory = 0;
				}

				dateInventory.inventory = inventory;
				if (prevInventory != inventory)
				{
					dateInventory.save(database);
				}
			}

			return inventory;
		}

		public void recalculateInventories(Database database, string consumerNo, DateTime fromDateTime, DateTime toDateTime)
		{
			DataSet dataSet = getDataSet(database, consumerNo, 0, fromDateTime, toDateTime);
			int i = 0;
			while(i < dataSet.Tables[0].Rows.Count)
			{
				ConsumerInventory consumerInventory = new ConsumerInventory(dataSet.Tables[0].Rows[i]);

				DateTime currentDateTime = new DateTime(consumerInventory.date.Year, consumerInventory.date.Month, consumerInventory.date.Day, consumerInventory.timeOfDay.Hour, 0, 0);				
				recalculateInventories(database, consumerNo, currentDateTime);

				i++;
			}

		}

		public void setActualInventory(Database database, FactoryOrder factoryOrder)
		{
			float consumerLevel = factoryOrder.consumerLevel;
			//if (factoryOrder.consumerPresentationUnit == 1) consumerLevel = (float)((factoryOrder.consumerLevel / 0.8);
			DateTime arrivalDateTime = DateTime.Parse(factoryOrder.arrivalDateTime.ToString("yyyy-MM-dd HH:00:00"));

			ConsumerInventory consumerInventory = getEntry(database, factoryOrder.consumerNo, arrivalDateTime);
			if (consumerInventory != null)
			{
				consumerInventory.type = 0;
				consumerInventory.inventory = consumerLevel;
				consumerInventory.save(database);
			}
			else
			{
				consumerInventory = new ConsumerInventory();
				consumerInventory.consumerNo = factoryOrder.consumerNo;
				consumerInventory.date = arrivalDateTime;
				consumerInventory.timeOfDay = arrivalDateTime;
				consumerInventory.inventory = consumerLevel;
				consumerInventory.type = 0;
				consumerInventory.save(database);
			}

			ConsumerInventoryOrder consumerInventoryOrder = new ConsumerInventoryOrder();
			consumerInventoryOrder.consumerNo = factoryOrder.consumerNo;
			consumerInventoryOrder.date = arrivalDateTime;
			consumerInventoryOrder.timeOfDay = arrivalDateTime;
			consumerInventoryOrder.factoryOrderEntryNo = factoryOrder.entryNo;
			consumerInventoryOrder.save(database);

		}
	}
}
