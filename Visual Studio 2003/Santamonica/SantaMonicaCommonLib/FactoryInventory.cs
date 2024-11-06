using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ConsumerCapacity.
	/// </summary>
	public class FactoryInventory
	{
		public string factoryNo;
		public DateTime date;
		public DateTime timeOfDay;
		public int type;
		public float inventory;
		public int factoryOrderEntryNo;
		public float volume;
		public float percent;


		private string updateMethod;


		public FactoryInventory()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public FactoryInventory(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.factoryNo = dataReader.GetValue(0).ToString();
			this.date = dataReader.GetDateTime(1);
			this.timeOfDay = dataReader.GetDateTime(2);
			this.type = dataReader.GetInt32(3);
			this.inventory = float.Parse(dataReader.GetValue(4).ToString());
			this.factoryOrderEntryNo = dataReader.GetInt32(5);

			this.volume = float.Parse(dataReader.GetValue(6).ToString());
			this.percent = float.Parse(dataReader.GetValue(7).ToString());

			updateMethod = "";

		}

		public FactoryInventory(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.factoryNo = dataRow.ItemArray.GetValue(0).ToString();
			this.date = DateTime.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.timeOfDay = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.type = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.inventory = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.factoryOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			
			this.volume = float.Parse(dataRow.ItemArray.GetValue(6).ToString());
			this.percent = float.Parse(dataRow.ItemArray.GetValue(7).ToString());

			updateMethod = "";

		}

		public void save(Database database)
		{
			double invRounded = inventory;
			invRounded = Math.Round(invRounded, 2);

			double volumeRounded = volume;
			volumeRounded = Math.Round(volumeRounded, 2);

			double percentRounded = percent;
			percentRounded = Math.Round(percentRounded, 2);
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Factory No] FROM [Factory Inventory] WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Factory Inventory] SET [Type] = '"+type+"', [Inventory] = '"+invRounded.ToString().Replace(",", ".")+"', [Factory Order Entry No] = '"+factoryOrderEntryNo+"', [Volume] = '"+volumeRounded.ToString().Replace(",", ".")+"', [Percent] = '"+percentRounded.ToString().Replace(",", ".")+"' WHERE [Factory No] = '"+factoryNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Factory Inventory] ([Factory No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No], [Volume], [Percent]) VALUES ('"+this.factoryNo+"', '"+this.date.ToString("yyyy-MM-dd")+"', '"+this.timeOfDay.ToString("1754-01-01 HH:mm:ss")+"','"+type+"','"+invRounded.ToString().Replace(",", ".")+"', '"+factoryOrderEntryNo+"', '"+volumeRounded.ToString().Replace(",", ".")+"', '"+percentRounded.ToString().Replace(",", ".")+"')");
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on factory inventory update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
