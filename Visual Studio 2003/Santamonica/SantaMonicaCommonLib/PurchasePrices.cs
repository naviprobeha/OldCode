using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class PurchasePrices
	{
		public PurchasePrices(XmlElement tableElement, Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			fromDOM(tableElement, database);
		}

		public PurchasePrices()
		{

		}

		public void fromDOM(XmlElement tableElement, Database database)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("R");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				PurchasePrice purchasePrice = new PurchasePrice(record, database, true);
				

				i++;
			}
		}

		public DataSet getDataSet(Database database, string itemNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Item No], [Vendor No], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Cost] FROM [Purchase Price] WHERE [Item No] = '"+itemNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "purchasePrice");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, string entryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Item No], [Vendor No], [Starting Date], [Minimum Quantity], [Ending Date], [Unit Cost] FROM [Purchase Price] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "purchasePrice");
			adapter.Dispose();

			return dataSet;
		}

	}
}