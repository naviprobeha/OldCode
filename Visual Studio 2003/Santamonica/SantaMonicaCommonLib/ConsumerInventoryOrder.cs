using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ConsumerCapacity.
	/// </summary>
	public class ConsumerInventoryOrder
	{
		public string consumerNo;
		public DateTime date;
		public DateTime timeOfDay;
		public int factoryOrderEntryNo;


		private string updateMethod;


		public ConsumerInventoryOrder()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ConsumerInventoryOrder(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.consumerNo = dataReader.GetValue(0).ToString();
			this.date = dataReader.GetDateTime(1);
			this.timeOfDay = dataReader.GetDateTime(2);
			this.factoryOrderEntryNo = dataReader.GetInt32(3);

			updateMethod = "";

		}

		public ConsumerInventoryOrder(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.consumerNo = dataRow.ItemArray.GetValue(0).ToString();
			this.date = DateTime.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.timeOfDay = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.factoryOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(3).ToString());
			
			updateMethod = "";

		}

		public void save(Database database)
		{
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Consumer Inventory Order] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"' AND [Factory Order Entry No] = '"+factoryOrderEntryNo+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Consumer No] FROM [Consumer Inventory] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"' AND [Factory Order Entry No] = '"+factoryOrderEntryNo+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Consumer Inventory Order] ([Consumer No], [Date], [TimeOfDay], [Factory Order Entry No]) VALUES ('"+this.consumerNo+"', '"+this.date.ToString("yyyy-MM-dd")+"', '"+this.timeOfDay.ToString("1754-01-01 HH:mm:ss")+"', '"+factoryOrderEntryNo+"')");
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on consumer inventory order update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
