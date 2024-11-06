using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class Items
	{
		public Items(XmlElement tableElement, Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			fromDOM(tableElement, database);
		}

		public Items()
		{

		}

		public Item getEntry(Database database, string no)
		{
			Item item = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Description], [Search Description], [Unit Price], [Add STOP-item], [Require ID], [Invoice To JBV], [STOP Item No], [Connection Item No], [Unit Of Measure], [Put To Death], [Available In Mobile], [Require Cash Payment], [Direct Cost], [Category Code], [Available On Web], [ID Group Code] FROM [Item] WHERE [No] = '"+no+"'");
			if (dataReader.Read())
			{
				item = new Item(dataReader);
			}
			
			dataReader.Close();
			
			return item;
		}

		public Item findEntry(Database database, string description)
		{
			Item item = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Description], [Search Description], [Unit Price], [Add STOP-item], [Require ID], [Invoice To JBV], [STOP Item No], [Connection Item No], [Unit Of Measure], [Put To Death], [Available In Mobile], [Require Cash Payment], [Direct Cost], [Category Code], [Available On Web], [ID Group Code] FROM [Item] WHERE UPPER([Description]) LIKE '"+description.ToUpper()+"%'");
			if (dataReader.Read())
			{
				item = new Item(dataReader);
			}
			
			dataReader.Close();
			
			return item;
		}


		public DataSet getDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Description], [Unit Price], [Add STOP-item], [Require ID], [Invoice To JBV], [STOP Item No], [Connection Item No], [Unit Of Measure], [Put To Death], [Available In Mobile], [Require Cash Payment], [Direct Cost], [Category Code], [Available On Web], [ID Group Code] FROM [Item] ORDER BY No");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "item");
			adapter.Dispose();

			return dataSet;

		}



		public DataSet getDataSet(Database database, int order)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Description], [Unit Price], [Add STOP-item], [Require ID], [Invoice To JBV], [STOP Item No], [Connection Item No], [Unit Of Measure], [Put To Death], [Available In Mobile], [Require Cash Payment], [Direct Cost], [Category Code], [Available On Web], [ID Group Code] FROM [Item] WHERE LEN([No]) = "+order+" ORDER BY No");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "item");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getWebDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Description], [Unit Price], [Add STOP-item], [Require ID], [Invoice To JBV], [STOP Item No], [Connection Item No], [Unit Of Measure], [Put To Death], [Available In Mobile], [Require Cash Payment], [Direct Cost], [Category Code], [Available On Web], [ID Group Code] FROM [Item] WHERE [Available On Web] = '1' ORDER BY No");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "item");
			adapter.Dispose();

			return dataSet;

		}

		public void fromDOM(XmlElement tableElement, Database database)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("R");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				Item item = new Item(record, database, true);

				i++;
			}
		}

		public DataSet getDataSetEntry(Database database, string no)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Description], [Search Description], [Unit Price], [Add STOP-item], [Require ID], [Invoice To JBV], [STOP Item No], [Connection Item No], [Unit Of Measure], [Put To Death], [Available In Mobile], [Require Cash Payment], [Direct Cost], [Category Code], [Available On Web], [ID Group Code] FROM [Item] WHERE [No] = '"+no+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "item");
			adapter.Dispose();

			return dataSet;
		}

		public Hashtable getBseTestings_2(Database database, DateTime startDate, DateTime endDate)
		{
			Hashtable hashTable = new Hashtable();
			
			SqlDataReader dataReader = database.query("SELECT i.[No], COUNT(i.[No]) FROM Item i, [Shipment Header] s, [Shipment Line] l, [Shipment Line ID] li WHERE s.[Shipment Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND s.[Shipment Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' AND i.[No] = l.[Item No] AND s.[No] = l.[Shipment No] AND s.[No] = li.[Shipment No] AND l.[Entry No] = li.[Shipment Line Entry No] AND li.[BSE Testing] = 1 GROUP BY i.[No]");
			while (dataReader.Read())
			{
				hashTable.Add(dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
			}
			
			dataReader.Close();
			
			return hashTable;

		}

		public Hashtable getPostMortems_2(Database database, DateTime startDate, DateTime endDate)
		{
			Hashtable hashTable = new Hashtable();
			
			SqlDataReader dataReader = database.query("SELECT i.[No], COUNT(i.[No]) FROM Item i, [Shipment Header] s, [Shipment Line] l, [Shipment Line ID] li WHERE s.[Shipment Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND s.[Shipment Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' AND i.[No] = l.[Item No] AND s.[No] = l.[Shipment No] AND s.[No] = li.[Shipment No] AND l.[Entry No] = li.[Shipment Line Entry No] AND li.[Post Mortem] = 1 GROUP BY i.[No]");
			while (dataReader.Read())
			{
				hashTable.Add(dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
			}
			
			dataReader.Close();
			
			return hashTable;

		}

		public Hashtable getBseTestings(Database database, string factoryCode, DateTime startDate, DateTime endDate)
		{
			Hashtable hashTable = new Hashtable();
			
			SqlDataReader dataReader = database.query("SELECT i.[No], COUNT(i.[No]) FROM Item i, [Line Journal] j, [Line Order] lo, [Line Order Shipment] s, [Shipment Line] l, [Shipment Line ID] li WHERE j.[Entry No] = lo.[Line Journal Entry No] AND j.[Arrival Factory Code] = '"+factoryCode+"' AND lo.[Creation Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND lo.[Creation Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' AND lo.[Entry No] = s.[Line Order Entry No] AND i.[No] = l.[Item No] AND s.[Shipment No] = l.[Shipment No] AND s.[Shipment No] = li.[Shipment No] AND l.[Entry No] = li.[Shipment Line Entry No] AND li.[BSE Testing] = 1 GROUP BY i.[No]");
			while (dataReader.Read())
			{
				hashTable.Add(dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
			}
			
			dataReader.Close();
			
			return hashTable;

		}

		public Hashtable getPostMortems(Database database, string factoryCode, DateTime startDate, DateTime endDate)
		{
			Hashtable hashTable = new Hashtable();
			
			SqlDataReader dataReader = database.query("SELECT i.[No], COUNT(i.[No]) FROM Item i, [Line Journal] j, [Line Order] lo, [Line Order Shipment] s, [Shipment Line] l, [Shipment Line ID] li WHERE j.[Entry No] = lo.[Line Journal Entry No] AND j.[Arrival Factory Code] = '"+factoryCode+"' AND lo.[Creation Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND lo.[Creation Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' AND lo.[Entry No] = s.[Line Order Entry No] AND i.[No] = l.[Item No] AND s.[Shipment No] = l.[Shipment No] AND s.[Shipment No] = li.[Shipment No] AND l.[Entry No] = li.[Shipment Line Entry No] AND li.[Post Mortem] = 1 GROUP BY i.[No]");
			while (dataReader.Read())
			{
				hashTable.Add(dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
			}
			
			dataReader.Close();
			
			return hashTable;

		}
	}
}
