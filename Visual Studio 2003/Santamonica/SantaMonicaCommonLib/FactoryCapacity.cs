using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ConsumerCapacity.
	/// </summary>
	public class FactoryCapacity
	{
		public string factoryNo;
		public DateTime date;
		public DateTime timeOfDay;
		public float plannedCapacity;

		private string updateMethod;


		public FactoryCapacity()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public FactoryCapacity(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.factoryNo = dataReader.GetValue(0).ToString();
			this.date = dataReader.GetDateTime(1);
			this.timeOfDay = dataReader.GetDateTime(2);
			this.plannedCapacity = float.Parse(dataReader.GetValue(3).ToString());
			
			updateMethod = "";

		}

		public FactoryCapacity(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.factoryNo = dataRow.ItemArray.GetValue(0).ToString();
			this.date = DateTime.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.timeOfDay = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.plannedCapacity = float.Parse(dataRow.ItemArray.GetValue(3).ToString());
			
			updateMethod = "";

		}

		public void save(Database database)
		{
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Factory Capacity] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [Time Of Day] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Factory No] FROM [Factory Capacity] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [Time Of Day] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Factory Capacity] SET [Planned Capacity] = '"+plannedCapacity.ToString().Replace(",", ".")+"' WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [Time Of Day] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Factory Capacity] ([Factory No], [Date], [Time Of Day], [Planned Capacity]) VALUES ('"+this.factoryNo+"', '"+this.date.ToString("yyyy-MM-dd")+"', '"+this.timeOfDay.ToString("1754-01-01 HH:mm:ss")+"','"+this.plannedCapacity.ToString().Replace(",", ".")+"')");
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on factory capacity update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

		public float calcActualCapacity(Database database)
		{
			ScaleEntries scaleEntries = new ScaleEntries();
			return scaleEntries.calcSumPerHour(database, "0", this.factoryNo, new DateTime(date.Year, date.Month, date.Day, timeOfDay.Hour, 0, 0)); 
		}

	}
}
