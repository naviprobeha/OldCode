using System;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class ScaleEntries
	{

		public ScaleEntries()
		{

		}


		public DataSet getScaledContainer(Database database, string containerNo, int lineOrderEntryNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry] WHERE [Container No] = '"+containerNo+"' AND [Line Order Entry No] = '"+lineOrderEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "scaleEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "scaleEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getNotSentDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry] WHERE [Navision Status] = 1");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "scaleEntry");
			adapter.Dispose();

			return dataSet;

		}

		public ScaleEntry getEntry(Database database, string factoryCode, int entryNo)
		{
			ScaleEntry scaleEntry = null;
		
			SqlDataReader dataReader = database.query("SELECT [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry] WHERE [Factory Code] = '"+factoryCode+"' AND [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				scaleEntry = new ScaleEntry(dataReader);
			}
			dataReader.Close();

			return scaleEntry;

		}

		public DataSet getDataSet(Database database, string factoryCode, int status)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry] WHERE [Factory Code] = '"+factoryCode+"' AND [Status] = '"+status+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "scaleEntry");
			adapter.Dispose();

			return dataSet;

		}	

		public DataSet getDataSet(Database database, string type, string factoryCode, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = " AND [Factory Code] = '"+factoryCode+"'";
		
			string typeQuery = "";
			if ((type != "") && (type != null)) typeQuery = " AND [Type] = '"+type+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry] WHERE [Entry Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Entry Date] <= '"+toDate.ToString("yyyy-MM-dd")+"'"+factoryQuery+typeQuery+" ORDER BY [Entry No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "scaleEntry");
			adapter.Dispose();

			return dataSet;

		}	


		public float calcSum(Database database, string type, string factoryCode, DateTime fromDate, DateTime toDate)
		{
			return calcSum(database, type, factoryCode, "", "", fromDate, toDate);
		}	

		public float calcSum(Database database, string type, string factoryCode, string shippingCustomerNo, DateTime fromDate, DateTime toDate)
		{
			return calcSum(database, type, factoryCode, shippingCustomerNo, "", fromDate, toDate);
		}	

		public float calcSum(Database database, string type, string factoryCode, string shippingCustomerNo, string categoryCode, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = " AND [Factory Code] = '"+factoryCode+"'";

			string shippingCustomerQuery = "";
			if ((shippingCustomerNo != "") && (shippingCustomerNo != null)) shippingCustomerQuery = " AND [Shipping Customer No] = '"+shippingCustomerNo+"'";

			string categoryCodeQuery = "";
			if ((categoryCode != "") && (categoryCode != null)) categoryCodeQuery = " AND [Category Code] = '"+categoryCode+"'";
		
			string typeQuery = "";
			if ((type != "") && (type != null)) typeQuery = " AND [Type] = '"+type+"'";

			SqlDataReader dataReader = database.query("SELECT SUM([Weight]) FROM [Scale Entry] WHERE [Entry Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Entry Date] <= '"+toDate.ToString("yyyy-MM-dd")+"'"+factoryQuery+typeQuery+shippingCustomerQuery+categoryCodeQuery+" AND ([Status] = 2 OR [Status] = 9)");

			float weight = 0;

			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0))
				{
					weight = float.Parse(dataReader.GetValue(0).ToString());
				}
			}

			dataReader.Close();



			dataReader = database.query("SELECT SUM([Weight]) FROM [Scale Entry] WHERE [Entry Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Entry Date] <= '"+toDate.ToString("yyyy-MM-dd")+"'"+factoryQuery+typeQuery+shippingCustomerQuery+categoryCodeQuery+" AND [Status] = 8");

			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0))
				{
					weight = weight - float.Parse(dataReader.GetValue(0).ToString());
				}
			}

			dataReader.Close();


			return weight;
		}	


		public float calcSumPerHour(Database database, string type, string factoryCode, DateTime dateTime)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = " AND [Factory Code] = '"+factoryCode+"'";
		
			string typeQuery = "";
			if ((type != "") && (type != null)) typeQuery = " AND [Type] = '"+type+"'";

			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Scale Entry] WHERE [Entry Date] = '"+dateTime.AddHours(-1).ToString("yyyy-MM-dd")+"' AND [Entry Time] >= '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"' AND [Entry Time] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"'"+factoryQuery+typeQuery+" AND [Status] = 2");

			float weight = 0;

			if (dataReader.Read())
			{
				try
				{
					weight = float.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception e)
				{
					weight = 0;
					if (e.Message != "") {}
				}
			}

			dataReader.Close();

			return weight;
		}	


		public DataSet getDataSet(Database database, string reference)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry] WHERE [Reference] LIKE '%"+reference+"%' ORDER BY [Entry No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "scaleEntry");
			adapter.Dispose();

			return dataSet;

		}	

		public float calcSum(Database database, string reference)
		{

			SqlDataReader dataReader = database.query("SELECT SUM([Weight]) FROM [Scale Entry] WHERE [Reference] LIKE '%"+reference+"%' AND [Status] = 2");

			float weight = 0;

			if (dataReader.Read())
			{
				try
				{
					weight = float.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception e)
				{
					weight = 0;
					if (e.Message != "") {}
				}
			}

			dataReader.Close();

			dataReader = database.query("SELECT SUM([Weight]) FROM [Scale Entry] WHERE [Reference] LIKE '%"+reference+"%' AND [Status] = 8");

			if (dataReader.Read())
			{
				try
				{
					weight = weight - float.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception e)
				{
					if (e.Message != "") {}
				}
			}

			dataReader.Close();


			return weight;
		}	

		public void resend(Database database)
		{
			database.nonQuery("UPDATE [Scale Entry] SET [Navision Status] = '1' WHERE [Navision Status] = '2'");
		}


		public DataSet getTransactionsFromDate(Database database, string factoryNo, DateTime date)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT TOP 1 [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry] WHERE [Factory Code] = '"+factoryNo+"' AND [Entry Date] >= '"+date.ToString("yyyy-MM-dd")+"' ORDER BY [Entry No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "scaleEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getTransactionsFromEntryNo(Database database, string factoryNo, int entryNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment] FROM [Scale Entry] WHERE [Factory Code] = '"+factoryNo+"' AND [Entry No] >= '"+entryNo+"' ORDER BY [Entry No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "scaleEntry");
			adapter.Dispose();

			return dataSet;

		}


		public ArrayList findMissingEntries(Database database, string factoryNo, int firstEntryNo)
		{
			ArrayList missingList = new ArrayList();

			DataSet	scaleEntriesDataSet = getTransactionsFromEntryNo(database, factoryNo, firstEntryNo);
			if (scaleEntriesDataSet.Tables[0].Rows.Count > 0)
			{
				int firstTransNo = int.Parse(scaleEntriesDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

				ArrayList completeList = new ArrayList();

				int i = 0;
				while (i < scaleEntriesDataSet.Tables[0].Rows.Count)
				{
					ScaleEntry scaleEntry = new ScaleEntry(scaleEntriesDataSet.Tables[0].Rows[i]);

					completeList.Add(scaleEntry.entryNo.ToString());

					i++;
				}

				int lastTransNo = int.Parse(completeList[completeList.Count-1].ToString());

				i = firstTransNo;
				while (i < lastTransNo)
				{
					if (!completeList.Contains(i.ToString()))
					{
						missingList.Add(i.ToString());
					}

					i++;
				}

			}

			return missingList;
		}
	}
}
