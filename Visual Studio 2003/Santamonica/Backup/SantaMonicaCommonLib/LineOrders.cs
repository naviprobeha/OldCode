using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class LineOrders
	{

		public static int TYPE_PLANNED = 0;
		public static int TYPE_ENTERED = 1;
		public static int TYPE_CONFIRMED = 2;

		public LineOrders()
		{
			//
			// TODO: Add constructor logic here
			//			
		}

		public LineOrder getEntry(Database database, string no)
		{
			LineOrder lineOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Entry No] = '"+no+"'");
			if (dataReader.Read())
			{
				lineOrder = new LineOrder(dataReader);
			}
			
			dataReader.Close();
			return lineOrder;
		}

		public DataSet getStatusDataSet(Database database, int status)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Status] = '"+status+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getUnAssignedDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Status] = '0' AND [Organization No] != ''");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getOrganizationDataSet(Database database, string organizationNo, DateTime shipDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Organization No] = '"+organizationNo+"' AND [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND [Status] = '0'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;
		}


		public DataSet getNewLineOrdersDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Status] = '0' AND [Organization No] = ''");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;
		}


		public DataSet getUnhandledDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Status] < '7' AND [Ship Date] < '"+DateTime.Today.ToString("yyyy-MM-dd")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;
		}


		public DataSet getUnhandledAwaitingDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Status] = '3' AND [Organization No] < ''");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;
		}


		public LineOrder getContainerLineOrder(Database database, string lineOrderContainerNo) //Bruk - Returns active LineOrder.
		{
			LineOrder lineOrder = null;
			 		
			SqlDataReader dataReader = database.query("SELECT l.[Entry No], l.[Organization No], l.[Line Journal Entry No], l.[Ship Date], l.[Shipping Customer No], l.[Shipping Customer Name], l.[Address], l.[Address 2], l.[Post Code], l.[City], l.[Country Code], l.[Phone No], l.[Cell Phone No], l.[Details], l.[Comments], l.[Status], l.[Closed Date], l.[Ship Time], l.[Creation Date], l.[Direction Comment], l.[Direction Comment 2], l.[Position X], l.[Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] l, [Line Order Container] c WHERE l.[Entry No] = c.[Line Order Entry No] AND c.[Container No] = '"+lineOrderContainerNo+"' AND l.[Status] < '7'");

			if (dataReader.Read())
			{
				lineOrder = new LineOrder(dataReader);
				dataReader.Close();
			}

			dataReader.Close();
				
			return lineOrder;
		}

		public LineOrder getContainerLineOrder(Database database, string containerNo, string agentCode)
		{
			LineOrder lineOrder = null;
			 		
			SqlDataReader dataReader = database.query("SELECT l.[Entry No], l.[Organization No], l.[Line Journal Entry No], l.[Ship Date], l.[Shipping Customer No], l.[Shipping Customer Name], l.[Address], l.[Address 2], l.[Post Code], l.[City], l.[Country Code], l.[Phone No], l.[Cell Phone No], l.[Details], l.[Comments], l.[Status], l.[Closed Date], l.[Ship Time], l.[Creation Date], l.[Direction Comment], l.[Direction Comment 2], l.[Position X], l.[Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] l, [Line Order Container] c, [Line Order Shipment] s, [Shipment Header] h WHERE l.[Entry No] = c.[Line Order Entry No] AND c.[Container No] = '"+containerNo+"' AND l.[Status] < '7' AND s.[Line Order Entry No] = l.[Entry No] AND s.[Shipment No] = h.[No] AND h.[Agent Code] = '"+agentCode+"'");

			if (dataReader.Read())
			{
				lineOrder = new LineOrder(dataReader);
				dataReader.Close();
			}

			dataReader.Close();
				
			return lineOrder;
		}



		public LineOrder getShippingCustomerLineOrder (Database database, string shippingCustomerNo) //Bruk - Returns active LineOrder.
		{
			LineOrder lineOrder = null;
			 		
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Status] < '"+6+"'");

			if (dataReader.Read())
			{
				lineOrder = new LineOrder(dataReader);
				dataReader.Close();
			}

			dataReader.Close();
				
			return lineOrder;
		}


		public LineOrder getAgentEntry(Database database, string no)
		{
			LineOrder lineOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Entry No] = '"+no+"'");
			if (dataReader.Read())
			{
				lineOrder = new LineOrder(dataReader);
			}
			
			dataReader.Close();
			return lineOrder;
		}

		public DataSet getActiveDataSet(Database database, string agent, DateTime fromDate, DateTime toDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND j.[Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND o.[Line Journal Entry No] = '0'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Line Order] o LEFT JOIN [Line Journal] j ON j.[Entry No] = o.[Line Journal Entry No] WHERE o.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND o.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" ORDER BY o.[Status], o.[Line Journal Entry No], o.[Shipping Customer No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		
		public DataSet getActiveOrganizationDataSet(Database database, string organizationNo, string agent, DateTime fromDate, DateTime toDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND j.[Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND o.[Line Journal Entry No] = '0'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Line Order] o LEFT JOIN [Line Journal] j ON j.[Entry No] = o.[Line Journal Entry No] WHERE o.[Organization No] = '"+organizationNo+"' AND o.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND o.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" ORDER BY o.[Status], o.[Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getNonLoadedDataSet(Database database, string organizationNo, DateTime fromDate, DateTime toDate)
		{

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Line Order] o, [Line Journal] j WHERE o.[Line Journal Entry No] = j.[Entry No] AND o.[Organization No] = '"+organizationNo+"' AND o.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND o.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND j.[Status] >= 8 AND o.[Status] < 7 ORDER BY o.[Status], o.[Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getNonContainerDataSet(Database database, int lineJournalEntryNo)
		{

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Line Order] o LEFT JOIN [Line Order Container] c ON c.[Line Order Entry No] = o.[Entry No] WHERE o.[Line Journal Entry No] = '"+lineJournalEntryNo+"' AND c.[Container No] IS NULL ORDER BY o.[Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getActiveCustomerDataSet(Database database, string shippingCustomerNo, DateTime fromDate, DateTime toDate)
		{

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Line Order] o LEFT JOIN [Line Journal] j ON j.[Entry No] = o.[Line Journal Entry No] WHERE o.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND o.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND o.[Shipping Customer No] = '"+shippingCustomerNo+"' ORDER BY o.[Status], o.[Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getCustomerLineOrderDataSet(Database database, string shippingCustomerNo)
		{

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' ORDER BY [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;


		}

		public DataSet getOrganizationLineOrderDataSet(Database database, string organizationNo, DateTime fromDate, DateTime toDate)
		{

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Line Order] o LEFT JOIN [Line Journal] j ON j.[Entry No] = o.[Line Journal Entry No] WHERE o.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND o.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND (([Shipping Customer No] IN (SELECT [Shipping Customer No] FROM [Organization] WHERE [No] = '"+organizationNo+"')) OR ([Shipping Customer No] IN (SELECT [Shipping Customer No] FROM [Organization Location] WHERE [Organization No] = '"+organizationNo+"'))) ORDER BY o.[Status], o.[Ship Date] DESC, o.[Line Journal Entry No], o.[Shipping Customer No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getDataSetEntry(Database database, string entryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getJournalDataSet(Database database, int lineJournalEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Line Journal Entry No] = '"+lineJournalEntryNo+"' ORDER BY [Optimizing Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getJournalDataSet(Database database, int lineJournalEntryNo, string routeGroupCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Line Journal Entry No] = '"+lineJournalEntryNo+"' AND [Route Group Code] = '"+routeGroupCode+"' ORDER BY [Optimizing Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public void setStatus(Database database, string entryNo, int status)
		{
			database.nonQuery("UPDATE [Line Order] SET Status = '"+status+"' WHERE [Entry No] = '"+entryNo+"' AND [Line Journal Entry No] > 0 AND [Status] < '"+status+"'");


		}

		public DataSet getMapDataSet(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Line Order] o LEFT JOIN [Line Journal] j ON o.[Line Journal Entry No] = j.[Entry No] WHERE o.[Line Journal Entry No] > 0 AND j.[Status] < 8 AND j.[Organization No] = '"+organizationNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getMapDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Line Order] o LEFT JOIN [Line Journal] j ON o.[Line Journal Entry No] = j.[Entry No] WHERE o.[Line Journal Entry No] > 0 AND j.[Status] < 8");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public ArrayList getPostMortemLineOrders(Database database, DateTime fromDate, DateTime toDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT DISTINCT lo.[Entry No] FROM [Line Order] lo, [Line Order Shipment] los, [Shipment Line ID] sli WHERE lo.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND lo.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND los.[Line Order Entry No] = lo.[Entry No] AND los.[Shipment No] = sli.[Shipment No] AND sli.[Post Mortem] = 1");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			ArrayList resultList = new ArrayList();

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				resultList.Add(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

				i++;
			}

			return resultList;

		}

		public ArrayList getTestingLineOrders(Database database, DateTime fromDate, DateTime toDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT DISTINCT lo.[Entry No] FROM [Line Order] lo, [Line Order Shipment] los, [Shipment Line ID] sli WHERE lo.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND lo.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND los.[Line Order Entry No] = lo.[Entry No] AND los.[Shipment No] = sli.[Shipment No] AND sli.[BSE Testing] = 1");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			ArrayList resultList = new ArrayList();

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				resultList.Add(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

				i++;
			}

			return resultList;

		}

		public DataSet getReasonDataSet(Database database, string reasonCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[Entry No], o.[Organization No], o.[Line Journal Entry No], o.[Ship Date], o.[Shipping Customer No], o.[Shipping Customer Name], o.[Address], o.[Address 2], o.[Post Code], o.[City], o.[Country Code], o.[Phone No], o.[Cell Phone No], o.[Details], o.[Comments], o.[Status], o.[Closed Date], o.[Ship Time], o.[Creation Date], o.[Direction Comment], o.[Direction Comment 2], o.[Position X], o.[Position Y], o.[Type], o.[Optimizing Sort Order], o.[Created By Type], o.[Created By Code], o.[Confirmed To Date], o.[Confirmed To Time], o.[Enable Auto Plan], o.[Travel Distance], o.[Travel Time], o.[Closed Time], o.[Route Group Code], o.[Priority], o.[Is Loaded], o.[Planning Agent Code], o.[Driver Name], o.[Arrival Factory Code], o.[Load Wait Time], o.[Parent Line Journal Entry No] FROM [Shipping Customer] c, [Line Order] o LEFT JOIN [Reason Reported Line Order] r ON r.[Line Order Entry No] = o.[Entry No] AND r.[Reason Code] = '"+reasonCode+"' WHERE o.[Shipping Customer No] = c.[No] AND c.[Reason Code] = '"+reasonCode+"' AND r.[Reason Code] IS NULL ORDER BY o.[Status], o.[Line Journal Entry No], o.[Shipping Customer No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getParentLineJournalDataSet(Database database, int parentLineJournalEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No] FROM [Line Order] WHERE [Parent Line Journal Entry No] = '"+parentLineJournalEntryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineOrder");
			adapter.Dispose();

			return dataSet;
		}
	}
}
