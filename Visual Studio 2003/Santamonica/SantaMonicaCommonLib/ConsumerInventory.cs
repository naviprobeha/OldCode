using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ConsumerCapacity.
	/// </summary>
	public class ConsumerInventory
	{
		public string consumerNo;
		public DateTime date;
		public DateTime timeOfDay;
		public int type;
		public float inventory;
		public int factoryOrderEntryNo;


		private string updateMethod;


		public ConsumerInventory()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ConsumerInventory(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.consumerNo = dataReader.GetValue(0).ToString();
			this.date = dataReader.GetDateTime(1);
			this.timeOfDay = dataReader.GetDateTime(2);
			this.type = dataReader.GetInt32(3);
			this.inventory = float.Parse(dataReader.GetValue(4).ToString());
			this.factoryOrderEntryNo = dataReader.GetInt32(5);

			updateMethod = "";

		}

		public ConsumerInventory(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.consumerNo = dataRow.ItemArray.GetValue(0).ToString();
			this.date = DateTime.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.timeOfDay = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.type = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.inventory = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
			this.factoryOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			
			updateMethod = "";

		}

		public void save(Database database)
		{
			double invRounded = inventory;
			invRounded = Math.Round(invRounded, 2);
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Consumer No] FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Consumer Inventory] SET [Type] = '"+type+"', [Inventory] = '"+invRounded.ToString().Replace(",", ".")+"', [Factory Order Entry No] = '"+factoryOrderEntryNo+"' WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Consumer Inventory] ([Consumer No], [Date], [TimeOfDay], [Type], [Inventory], [Factory Order Entry No]) VALUES ('"+this.consumerNo+"', '"+this.date.ToString("yyyy-MM-dd")+"', '"+this.timeOfDay.ToString("1754-01-01 HH:mm:ss")+"','"+type+"','"+invRounded.ToString().Replace(",", ".")+"', '"+factoryOrderEntryNo+"')");
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on consumer inventory update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
