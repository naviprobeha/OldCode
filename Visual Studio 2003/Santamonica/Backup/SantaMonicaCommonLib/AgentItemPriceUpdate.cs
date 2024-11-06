using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for UserOperator.
	/// </summary>
	public class AgentItemPriceUpdate
	{
		public string agentCode;
		public string itemNo;
		public bool update;
		public float checksum;
		public float reportedChecksum;

		public AgentItemPriceUpdate()
		{
			checksum = 0;
			reportedChecksum = 0;
		}

		public AgentItemPriceUpdate(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.agentCode = dataReader.GetValue(0).ToString();
			this.itemNo = dataReader.GetValue(1).ToString();
			this.update = false;
			if (int.Parse(dataReader.GetValue(2).ToString()) == 1) this.update = true;
			this.checksum = float.Parse(dataReader.GetValue(3).ToString());
			this.reportedChecksum = float.Parse(dataReader.GetValue(4).ToString());
		}

		public AgentItemPriceUpdate(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.agentCode = dataRow.ItemArray.GetValue(0).ToString();
			this.itemNo = dataRow.ItemArray.GetValue(1).ToString();
			if (int.Parse(dataRow.ItemArray.GetValue(2).ToString()) == 1) this.update = true;
			this.checksum = float.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.reportedChecksum = float.Parse(dataRow.ItemArray.GetValue(4).ToString());
		}

		public void save(Database database)
		{
			int updateVal = 0;
			if (update) updateVal = 1;

			SqlDataReader dataReader = database.query("SELECT [Agent Code] FROM [Agent Item Price Update] WHERE [Agent Code] = '"+agentCode+"' AND [Item No] = '"+itemNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("UPDATE [Agent Item Price Update] SET [Update] = '"+updateVal+"', [Checksum] = '"+this.checksum.ToString().Replace(",", ".")+"', [Reported Checksum] = '"+this.reportedChecksum.ToString().Replace(",", ".")+"' WHERE [Agent Code] = '"+agentCode+"' AND [Item No] = '"+itemNo+"'");

			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Agent Item Price Update] ([Agent Code], [Item No], [Update], [Checksum], [Reported Checksum]) VALUES ('"+agentCode+"','"+itemNo+"','"+updateVal+"','"+checksum.ToString().Replace(",", ".")+"','"+reportedChecksum.ToString().Replace(",", ".")+"')");
			}

			
		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Agent Item Price Update] WHERE [Agent Code] = '"+agentCode+"' AND [Item No] = '"+itemNo+"'");
		}

		public void enqueuePrices(Database database)
		{
			SynchronizationQueueEntries synchEntries = new SynchronizationQueueEntries();

			ItemPrices itemPrices = new ItemPrices();
			DataSet itemPriceDataSet = itemPrices.getFullDataSet(database, this.itemNo);
			int i = 0;
			while (i < itemPriceDataSet.Tables[0].Rows.Count)
			{
				int entryNo = int.Parse(itemPriceDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());
				synchEntries.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_ITEM_PRICE, entryNo.ToString(), 0);

				i++;
			}

			ItemPricesExtended itemPricesExtended = new ItemPricesExtended();
			itemPriceDataSet = itemPricesExtended.getFullDataSet(database, this.itemNo);
			int j = 0;
			while (j < itemPriceDataSet.Tables[0].Rows.Count)
			{
				int entryNo = int.Parse(itemPriceDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
				synchEntries.enqueue(database, agentCode, SynchronizationQueueEntries.SYNC_ITEM_PRICE_EXTENDED, entryNo.ToString(), 0);

				j++;
			}

			this.update = false;
			save(database);

			if ((i==0) && (j==0)) this.delete(database);
		}

		public void acknowledge(Database database, float checksum)
		{
			ItemPrices itemPrices = new ItemPrices();
			float calculatedChecksum = itemPrices.calcChecksum(database, itemNo);
			ItemPricesExtended itemPricesExtended = new ItemPricesExtended();
			calculatedChecksum = calculatedChecksum + itemPricesExtended.calcChecksum(database, itemNo);

			this.checksum = calculatedChecksum;
			this.reportedChecksum = checksum;
			if (this.checksum != this.reportedChecksum)
			{
				save(database);
			}
			else
			{
				delete(database);
			}
		}
	}
}
