using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class ConsumerStatus
	{

		public string consumerNo;
		public float inventoryLevel;
		public DateTime updatedDateTime;

		private string updateMethod;

		public ConsumerStatus()
		{
			updatedDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
		}

		public ConsumerStatus(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.consumerNo = dataReader.GetValue(0).ToString();
			this.inventoryLevel = float.Parse(dataReader.GetValue(1).ToString());

			DateTime updatedDate = dataReader.GetDateTime(2);
			DateTime updatedTime = dataReader.GetDateTime(3);
			updatedDateTime = DateTime.Parse(updatedDate.ToString("yyyy-MM-dd")+" "+updatedTime.ToString("HH:mm:ss"));

			updateMethod = "";

		}

		public ConsumerStatus(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.consumerNo = dataRow.ItemArray.GetValue(0).ToString();
			this.inventoryLevel = float.Parse(dataRow.ItemArray.GetValue(1).ToString());

			DateTime updatedDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			DateTime updatedTime = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
			updatedDateTime = DateTime.Parse(updatedDate.ToString("yyyy-MM-dd")+" "+updatedTime.ToString("HH:mm:ss"));

			updateMethod = "";

		}


		public void save(Database database)
		{
			SynchronizationQueueEntries synchQueueEntries = new SynchronizationQueueEntries();

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Consumer Status] WHERE [Consumer No] = '"+consumerNo+"'");
					synchQueueEntries.enqueue(database, Agents.TYPE_TANK, SynchronizationQueueEntries.SYNC_CONSUMER_STATUS, this.consumerNo, 2);
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Consumer No] FROM [Consumer Status] WHERE [Consumer No] = '"+consumerNo+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Consumer Status] SET [Consumer No] = '"+consumerNo+"', [Inventory Level] = '"+inventoryLevel.ToString().Replace(",", ".")+"', [Updated Date] = '"+updatedDateTime.ToString("yyyy-MM-dd")+"', [Updated Time] = '"+updatedDateTime.ToString("1754-01-01 HH:mm:ss")+"' WHERE [Consumer No] = '"+consumerNo+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Consumer Status] ([Consumer No], [Inventory Level], [Updated Date], [Updated Time]) VALUES ('"+this.consumerNo+"','"+inventoryLevel.ToString().Replace(",", ".")+"', '"+updatedDateTime.ToString("yyyy-MM-dd")+"', '"+updatedDateTime.ToString("1754-01-01 HH:mm:ss")+"')");
					}

					synchQueueEntries.enqueue(database, Agents.TYPE_TANK, SynchronizationQueueEntries.SYNC_CONSUMER_STATUS, this.consumerNo, 0);
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on consymer status update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


	}
}
