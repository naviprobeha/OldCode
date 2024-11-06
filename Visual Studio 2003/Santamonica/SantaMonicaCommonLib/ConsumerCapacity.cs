using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ConsumerCapacity.
	/// </summary>
	public class ConsumerCapacity
	{
		public string consumerNo;
		public DateTime date;
		public DateTime timeOfDay;
		public float plannedCapacity;
		public float actualCapacity;

		private string updateMethod;


		public ConsumerCapacity()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ConsumerCapacity(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.consumerNo = dataReader.GetValue(0).ToString();
			this.date = dataReader.GetDateTime(1);
			this.timeOfDay = dataReader.GetDateTime(2);
			this.plannedCapacity = float.Parse(dataReader.GetValue(3).ToString());
			this.actualCapacity = float.Parse(dataReader.GetValue(4).ToString());
			
			updateMethod = "";

		}

		public ConsumerCapacity(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.consumerNo = dataRow.ItemArray.GetValue(0).ToString();
			this.date = DateTime.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.timeOfDay = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.plannedCapacity = float.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.actualCapacity = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
			
			updateMethod = "";

		}

		public void save(Database database)
		{
		
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Consumer Capacity] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Consumer No] FROM [Consumer Capacity] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Consumer Capacity] SET [Planned Capacity] = '"+plannedCapacity.ToString().Replace(",", ".")+"', [Actual Capacity] = '"+actualCapacity.ToString().Replace(",", ".")+"' WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+date.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] = '"+timeOfDay.ToString("1754-01-01 HH:mm:ss")+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Consumer Capacity] ([Consumer No], [Date], [TimeOfDay], [Planned Capacity], [Actual Capacity]) VALUES ('"+this.consumerNo+"', '"+this.date.ToString("yyyy-MM-dd")+"', '"+this.timeOfDay.ToString("1754-01-01 HH:mm:ss")+"','"+this.plannedCapacity.ToString().Replace(",", ".")+"','"+this.actualCapacity.ToString().Replace(",", ".")+"')");
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on consumer capacity update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
