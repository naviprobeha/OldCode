using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLine.
	/// </summary>
	public class ShipOrderLogLine
	{

		public int entryNo;
		public int shipOrderEntryNo;
		public DateTime date;
		public DateTime timeOfDay;
		public string source;
		public string text;

		private string updateMethod;

		public ShipOrderLogLine(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.shipOrderEntryNo = dataReader.GetInt32(1);
			this.date = dataReader.GetDateTime(2);
			this.timeOfDay = dataReader.GetDateTime(3);
			this.source = dataReader.GetValue(4).ToString();
			this.text = dataReader.GetValue(5).ToString();

			updateMethod = "";

		}

		public ShipOrderLogLine(ShipOrder shipOrder)
		{
			this.shipOrderEntryNo = shipOrder.entryNo;
		}


		public ShipOrderLogLine(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			fromDataRow(dataRow);
			
		}


		private void fromDataRow(DataRow dataRow)
		{
			entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			shipOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			date = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			timeOfDay = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
			source = dataRow.ItemArray.GetValue(4).ToString();
			text = dataRow.ItemArray.GetValue(5).ToString();

		}

		public void save(Database database)
		{
			this.timeOfDay = new DateTime(1754, 01, 01, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);

			SqlDataReader dataReader = database.query("SELECT * FROM [Ship Order Log Line] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Ship Order Log Line] WHERE [Entry No] = '"+entryNo+"'");
				}

				else
				{
					database.nonQuery("UPDATE [Ship Order Log Line] SET [Ship Order Entry No] = '"+this.shipOrderEntryNo+"', [Date] = '"+this.date.ToString("yyyy-MM-dd")+"', [Time Of Day] = '"+this.timeOfDay.ToString("yyyy-MM-dd HH:mm:ss")+"', [Source] = '"+this.source+"', [Text] = '"+this.text+"' WHERE [Entry No] = '"+this.entryNo+"'");
				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Ship Order Log Line] ([Ship Order Entry No], [Date], [Time Of Day], [Source], [Text]) VALUES ('"+this.shipOrderEntryNo+"','"+this.date.ToString("yyyy-MM-dd")+"','"+this.timeOfDay.ToString("yyyy-MM-dd HH:mm:ss")+"','"+this.source+"','"+this.text+"')");
				entryNo = (int)database.getInsertedSeqNo();
			}


		}

		public void delete(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			database.nonQuery("DELETE FROM [Ship Order Log Line] WHERE [Entry No] = '"+entryNo+"' AND [Ship Order Entry No] = '"+shipOrderEntryNo+"'");
			
		}

	}
}
