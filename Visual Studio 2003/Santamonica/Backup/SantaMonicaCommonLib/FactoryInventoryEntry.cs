using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ConsumerCapacity.
	/// </summary>
	public class FactoryInventoryEntry
	{
		public int entryNo;
		public string factoryNo;
		public int type;
		public int status;
		public DateTime date;
		public DateTime timeOfDay;
		public int lineOrderEntryNo;
		public string containerNo;
		public float weight;
		public bool closed;
		public float remainingWeight;

		private string updateMethod;


		public FactoryInventoryEntry()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public FactoryInventoryEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.factoryNo = dataReader.GetValue(1).ToString();
			this.type = dataReader.GetInt32(2);
			this.status = dataReader.GetInt32(3);
			this.date = dataReader.GetDateTime(4);
			this.timeOfDay = dataReader.GetDateTime(5);
			this.lineOrderEntryNo = dataReader.GetInt32(6);
			this.containerNo = dataReader.GetValue(7).ToString();
			this.weight = float.Parse(dataReader.GetValue(8).ToString());

			this.closed = false;
			if (dataReader.GetValue(9).ToString() == "1") this.closed = true;

			this.remainingWeight = float.Parse(dataReader.GetValue(10).ToString());

		
			updateMethod = "";

		}

		public FactoryInventoryEntry(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.factoryNo = dataRow.ItemArray.GetValue(1).ToString();
			this.type = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.status = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.date = DateTime.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.timeOfDay = DateTime.Parse(dataRow.ItemArray.GetValue(5).ToString());
			this.lineOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(6).ToString());
			this.containerNo = dataRow.ItemArray.GetValue(7).ToString();
			this.weight = float.Parse(dataRow.ItemArray.GetValue(8).ToString());

			this.closed = false;
			if (dataRow.ItemArray.GetValue(9).ToString() == "1") this.closed = true;
	
			this.remainingWeight = float.Parse(dataRow.ItemArray.GetValue(10).ToString());
	

			updateMethod = "";

		}

		public void save(Database database)
		{
			int closedVal = 0;
			if (this.closed) closedVal = 1;
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Factory Inventory Entry] WHERE [Entry No] = '"+entryNo+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Factory Inventory Entry] WHERE [Entry No] = '"+entryNo+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Factory Inventory Entry] SET [Factory No] = '"+factoryNo+"', [Type] = '"+type+"', [Status] = '"+status+"', [Date] = '"+date.ToString("yyyy-MM-dd")+"', [Time Of Day] = '"+this.timeOfDay.ToString("1754-01-01 HH:mm:ss")+"', [Line Order Entry No] = '"+this.lineOrderEntryNo+"', [Container No] = '"+containerNo+"', [Weight] = '"+weight.ToString().Replace(",", ".")+"', [Closed] = '"+closedVal+"', [Remaining Weight] = '"+remainingWeight.ToString().Replace(",", ".")+"' WHERE [Entry No] = '"+entryNo+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Factory Inventory Entry] ([Factory No], [Type], [Status], [Date], [Time Of Day], [Line Order Entry No], [Container No], [Weight], [Closed], [Remaining Weight]) VALUES ('"+this.factoryNo+"', '"+type+"', '"+status+"', '"+this.date.ToString("yyyy-MM-dd")+"', '"+this.timeOfDay.ToString("1754-01-01 HH:mm:ss")+"', '"+lineOrderEntryNo+"', '"+containerNo+"', '"+this.weight.ToString().Replace(",", ".")+"', '"+closedVal+"', '"+remainingWeight.ToString().Replace(",", ".")+"')");
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on factory inventory update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

		public void checkCapacity(Database database)
		{
			FactoryInventoryEntries factoryInventoryEntries = new FactoryInventoryEntries();
			FactoryCapacities factoryCapacities = new FactoryCapacities();
			FactoryCapacity factoryCapacity = factoryCapacities.getEntry(database, this.factoryNo, new DateTime(date.Year, date.Month, date.Day, this.timeOfDay.Hour, this.timeOfDay.Minute, this.timeOfDay.Second));
			if (factoryCapacity != null)
			{
				if (factoryInventoryEntries.countInventoryEntries(database, this.factoryNo, this.date, this.timeOfDay) >= factoryCapacity.plannedCapacity)
				{
					this.timeOfDay = this.timeOfDay.AddHours(1);
					this.checkCapacity(database);
				}
			}
			else
			{
				if (factoryCapacities.capacityEntriesExists(database, this.factoryNo, this.date, this.timeOfDay))
				{
					this.timeOfDay = this.timeOfDay.AddHours(1);
					this.checkCapacity(database);
				}
			}

		}

		public string getStatus()
		{
			if (status == 0) return "Uppskattad";
			if (status == 1) return "Vägd";
			return "";
		}

		public string getType()
		{
			if (status == 0) return "In";
			if (status == 1) return "Ut";
			return "";
		}

		public LineOrder getLineOrder(Database database)
		{
			LineOrders lineOrders = new LineOrders();
			return lineOrders.getEntry(database, this.lineOrderEntryNo.ToString());
		}

		public DateTime createDateTime()
		{
			return new DateTime(date.Year, date.Month, date.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second);


		}
	}
}
