using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLines.
	/// </summary>
	public class LineOrderContainers
	{
		public LineOrderContainers()
		{

		}

		
		public DataSet getDataSet(Database database, int lineOrderEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Line Order Entry No], [Container No], [Category Code], [Weight], [Real Weight], [Scaled Date], [Scaled Time], [Is Scaled], [Is Sent To Scaling] FROM [Line Order Container] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
			
		}

		public DataSet getJournalDataSet(Database database, int lineJournalEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[Entry No], c.[Line Order Entry No], c.[Container No], c.[Category Code], c.[Weight], c.[Real Weight], c.[Scaled Date], c.[Scaled Time], c.[Is Scaled], o.[Shipping Customer Name], o.[Confirmed To Date], o.[Confirmed To Time] FROM [Line Order Container] c, [Line Order] o WHERE c.[Line Order Entry No] = o.[Entry No] AND o.[Line Journal Entry No] = '"+lineJournalEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
			
		}


		public LineOrderContainer getEntry(Database database, int lineOrderEntryNo, int entryNo)
		{
			LineOrderContainer lineOrderContainer = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Line Order Entry No], [Container No], [Category Code], [Weight], [Real Weight], [Scaled Date], [Scaled Time], [Is Scaled], [Is Sent To Scaling] FROM [Line Order Container] WHERE [Entry No] = '"+entryNo+"' AND [Line Order Entry No] = '"+lineOrderEntryNo+"'");
			if (dataReader.Read())
			{
				lineOrderContainer = new LineOrderContainer(dataReader);
			}
			
			dataReader.Close();
			return lineOrderContainer;
		}

		public LineOrderContainer getEntry(Database database, int entryNo)
		{
			LineOrderContainer lineOrderContainer = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Line Order Entry No], [Container No], [Category Code], [Weight], [Real Weight], [Scaled Date], [Scaled Time], [Is Scaled], [Is Sent To Scaling] FROM [Line Order Container] WHERE [Entry No] = '"+entryNo+"'");
			if (dataReader.Read())
			{
				lineOrderContainer = new LineOrderContainer(dataReader);
			}
			
			dataReader.Close();
			return lineOrderContainer;
		}


		
		public LineOrderContainer getEntry(Database database, int lineOrderEntryNo, string containerNo)
		{
			LineOrderContainer lineOrderContainer = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Line Order Entry No], [Container No], [Category Code], [Weight], [Real Weight], [Scaled Date], [Scaled Time], [Is Scaled], [Is Sent To Scaling] FROM [Line Order Container] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"' AND [Container No] = '"+containerNo+"'");
			if (dataReader.Read())
			{
				lineOrderContainer = new LineOrderContainer(dataReader);
			}
			
			dataReader.Close();
			return lineOrderContainer;
		}
		

		public DataSet getDataSetEntry(Database database, int entryNo)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Line Order Entry No], [Container No], [Category Code], [Weight], [Real Weight], [Scaled Date], [Scaled Time], [Is Scaled], [Is Sent To Scaling] FROM [Line Order Container] WHERE [Entry No] = '"+entryNo+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
		}
		

		public DataSet getUnScaledDataSet(Database database, string factoryCode)
		{

			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = " AND j.[Arrival Factory Code] = '"+factoryCode+"'";
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[Entry No], c.[Line Order Entry No], c.[Container No], c.[Category Code], c.[Weight], c.[Real Weight], c.[Scaled Date], c.[Scaled Time], j.[Arrival Factory Code], o.[Shipping Customer No], o.[Line Journal Entry No], e.[Entry Date], e.[Entry Time] FROM [Line Order Container] c, [Line Order] o, [Line Journal] j, [Container Entry] e WHERE c.[Line Order Entry No] = o.[Entry No] AND o.[Line Journal Entry No] = j.[Entry No] AND c.[Container No] = e.[Container No] AND e.[Document Type] = 2 AND e.[Type] = 1 AND j.[Entry No] = e.[Document No] AND c.[Is Scaled] = '0'"+factoryQuery+" ORDER BY e.[Entry Date] DESC, e.[Entry Time] DESC");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getUnScaledDataSet(Database database, string factoryCode, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = " AND j.[Arrival Factory Code] = '"+factoryCode+"'";
					
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[Entry No], c.[Line Order Entry No], c.[Container No], c.[Category Code], c.[Weight], c.[Real Weight], c.[Scaled Date], c.[Scaled Time], j.[Arrival Factory Code], o.[Shipping Customer No], o.[Line Journal Entry No], e.[Entry Date], e.[Entry Time] FROM [Line Order Container] c, [Line Order] o, [Line Journal] j, [Container Entry] e WHERE c.[Line Order Entry No] = o.[Entry No] AND o.[Line Journal Entry No] = j.[Entry No] AND c.[Container No] = e.[Container No] AND e.[Document Type] = 2 AND e.[Type] = 1 AND j.[Entry No] = e.[Document No] AND c.[Is Scaled] = '0'"+factoryQuery+" AND e.[Entry Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND e.[Entry Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' ORDER BY e.[Entry Date] DESC, e.[Entry Time] DESC");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getContainersToScaleDataSet(Database database, string factoryCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[Entry No], c.[Line Order Entry No], c.[Container No], c.[Category Code], c.[Weight], c.[Real Weight], c.[Scaled Date], c.[Scaled Time] FROM [Line Order Container] c, [Line Order] o, [Line Journal] j WHERE c.[Line Order Entry No] = o.[Entry No] AND o.[Line Journal Entry No] = j.[Entry No] AND c.[Is Scaled] = '0' AND c.[Is Sent To Scaling] = '0' AND j.[Arrival Factory Code] = '"+factoryCode+"' AND j.[Status] >= 7 ORDER BY o.[Entry No]");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
		}


		public DataSet getNonUnLoadedContainerDataSet(Database database, string organizationNo, DateTime fromDate, DateTime toDate)
		{
			string organizationQuery = "";
			if ((organizationNo != "") && (organizationNo != null)) organizationQuery = " AND o.[Organization No] = '"+organizationNo+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[Entry No], c.[Line Order Entry No], c.[Container No], c.[Category Code], c.[Weight], c.[Real Weight], c.[Scaled Date], c.[Scaled Time], c.[Is Scaled], c.[Is Sent To Scaling] FROM [Line Order] o, [Line Journal] j, [Line Order Container] c LEFT JOIN [Container Entry] e ON e.[Document Type] = 2 AND e.[Type] = 1 AND e.[Container No] = c.[Container No] WHERE c.[Line Order Entry No] = o.[Entry No]"+organizationQuery+" AND o.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND o.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND o.[Status] >= 7 AND j.[Entry No] = o.[Line Journal Entry No] AND j.[Status] >= 7 AND e.[Entry No] IS NULL ORDER BY [Line Order Entry No], [Container No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
			
		}

		public DataSet getNonLoadedContainerDataSet(Database database, string organizationNo, DateTime fromDate, DateTime toDate)
		{
			string organizationQuery = "";
			if ((organizationNo != "") && (organizationNo != null)) organizationQuery = " AND o.[Organization No] = '"+organizationNo+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[Entry No], c.[Line Order Entry No], c.[Container No], c.[Category Code], c.[Weight], c.[Real Weight], c.[Scaled Date], c.[Scaled Time], c.[Is Scaled], c.[Is Sent To Scaling] FROM  [Line Order] o, [Line Order Container] c LEFT JOIN [Container Entry] e ON e.[Document Type] = 1 AND c.[Line Order Entry No] = e.[Document No] AND e.[Type] = 0 AND e.[Container No] = c.[Container No] WHERE c.[Line Order Entry No] = o.[Entry No]"+organizationQuery+" AND o.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND o.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND o.[Status] >= 7 AND e.[Entry No] IS NULL ORDER BY [Line Order Entry No], [Container No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
			
		}

		public DataSet getNonScaledContainerDataSet(Database database, string factoryNo, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryNo != "") && (factoryNo != null)) factoryQuery = " AND j.[Arrival Factory Code] = '"+factoryNo+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT c.[Entry No], c.[Line Order Entry No], c.[Container No], c.[Category Code], c.[Weight], c.[Real Weight], c.[Scaled Date], c.[Scaled Time], c.[Is Scaled], c.[Is Sent To Scaling] FROM  [Line Order] o, [Line Order Container] c, [Line Journal] j WHERE c.[Line Order Entry No] = o.[Entry No] AND o.[Line Journal Entry No] = j.[Entry No]"+factoryQuery+" AND o.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND o.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND j.[Status] >= 7 AND c.[Is Scaled] = 0");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrderContainer");
			adapter.Dispose();

			return dataSet;
		}

		public bool containesScaledContainer(Database database, int lineOrderEntryNo)
		{
			SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Line Order Container] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"' AND [Is Scaled] = 1");

			bool found = false;

			if (dataReader.Read())
			{
				found = true;

			}

			dataReader.Close();
			return found;

		}


	}
}
