using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class ItemPricesExtended
	{
		public ItemPricesExtended(XmlElement tableElement, Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			fromDOM(tableElement, database);
		}

		public ItemPricesExtended()
		{

		}

		public void fromDOM(XmlElement tableElement, Database database)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("R");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				ItemPriceExtended itemPriceExtended = new ItemPriceExtended(record, database, true);

				i++;
			}
		}

		public DataSet getDataSet(Database database, string itemNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Item No], [Starting Date], [Ending Date], [Customer Price Group], [Unit of Measure Code], [Quantity From], [Quantity To], [Line Amount] FROM [Item Price Extended] WHERE [Item No] = '"+itemNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "itemPriceExtended");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getFullDataSet(Database database, string itemNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Item No], [Starting Date], [Ending Date], [Customer Price Group], [Unit of Measure Code], [Quantity From], [Quantity To], [Line Amount] FROM [Item Price Extended] WHERE [Item No] = '"+itemNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "itemPriceExtended");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getDataSetEntry(Database database, string entryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Item No], [Starting Date], [Ending Date], [Customer Price Group], [Unit of Measure Code], [Quantity From], [Quantity To], [Line Amount] FROM [Item Price Extended] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "itemPriceExtended");
			adapter.Dispose();

			return dataSet;
		}

		public float calcChecksum(Database database, string itemNo)
		{
			float checksum = 0;
			SqlDataReader dataReader = database.query("SELECT SUM([Line Amount]) FROM [Item Price Extended] WHERE [Item No] = '"+itemNo+"'");
			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) checksum = float.Parse(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();
			
			return checksum;
		}

	}
}