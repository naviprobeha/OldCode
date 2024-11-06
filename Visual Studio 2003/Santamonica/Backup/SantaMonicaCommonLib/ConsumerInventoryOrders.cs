using System;
using System.Collections;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class ConsumerInventoryOrders
	{

		public ConsumerInventoryOrders()
		{

		}


		public DataSet getDataSet(Database database, string consumerNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Date], [TimeOfDay], [Factory Order Entry No] FROM [Consumer Inventory Order] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [TimeOfDay] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [TimeOfDay] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerInventoryOrder");
			adapter.Dispose();

			return dataSet;

		}

		public ArrayList getFactoryOrders(Database database, string consumerNo, DateTime dateTime)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Consumer No], [Date], [TimeOfDay], [Factory Order Entry No] FROM [Consumer Inventory Order] WHERE [Consumer No] = '"+consumerNo+"' AND [Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND (([TimeOfDay] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [TimeOfDay] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([TimeOfDay] = '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'))");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerInventoryOrder");
			adapter.Dispose();

			ArrayList arrayList = new ArrayList();

			FactoryOrders factoryOrders = new FactoryOrders();

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				FactoryOrder factoryOrder = factoryOrders.getEntry(database, dataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());	
				arrayList.Add(factoryOrder);

				i++;
			}

			return arrayList;
		}

		public float calcConsumerTotal(Database database, string consumerNo, DateTime fromDate, DateTime toDate)
		{
			SqlDataReader dataReader = database.query("SELECT SUM(o.[Real Quantity]) FROM [Consumer Inventory Order] i, [Factory Order] o WHERE i.[Consumer No] = '"+consumerNo+"' AND i.[Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND i.[Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND o.[Entry No] = i.[Factory Order Entry No]");

			float total = 0;

			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) total = float.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return total;

		}
	}
}
