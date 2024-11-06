using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class ItemPrices
	{
		public ItemPrices(XmlElement tableElement, Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			fromDOM(tableElement, database);
		}

		public ItemPrices()
		{

		}

		public void fromDOM(XmlElement tableElement, Database database)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("R");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				ItemPrice itemPrice = new ItemPrice(record, database, true);

				i++;
			}
		}

		public DataSet getDataSet(Database database, string itemNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Item No], [Sales Type], [Sales Code], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Price] FROM [Item Price] WHERE [Item No] = '"+itemNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "itemPrice");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getFullDataSet(Database database, string itemNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Item No], [Sales Type], [Sales Code], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Price] FROM [Item Price] WHERE [Item No] = '"+itemNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "itemPrice");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, string entryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Item No], [Sales Type], [Sales Code], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Price] FROM [Item Price] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "itemPrice");
			adapter.Dispose();

			return dataSet;
		}

		public float calcChecksum(Database database, string itemNo)
		{
			float checksum = 0;
			SqlDataReader dataReader = database.query("SELECT SUM([Unit Price]) FROM [Item Price] WHERE [Item No] = '"+itemNo+"'");
			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) checksum = float.Parse(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();
			
			return checksum;
		}
		
	}
}