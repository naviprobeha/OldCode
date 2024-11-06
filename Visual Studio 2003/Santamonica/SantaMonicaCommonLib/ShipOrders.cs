using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class ShipOrders
	{
		private string agentCode;

		public ShipOrders()
		{
			//
			// TODO: Add constructor logic here
			//			
		}

		public ShipOrders(string agentCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.agentCode = agentCode;
		}

	
		public ShipOrder getEntry(Database database, string organizationNo, string no)
		{
			ShipOrder shipOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Priority], [Agent Code], [Status], [Closed Date], [Position X], [Position Y], [Ship Date], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type], [Ship Time], [Creation Date], [Production Site] FROM [Ship Order] WHERE [Organization No] = '"+organizationNo+"' AND [Entry No] = '"+no+"'");
			if (dataReader.Read())
			{
				shipOrder = new ShipOrder(dataReader);
			}
			
			dataReader.Close();
			return shipOrder;
		}

		public ShipOrder getEntry(Database database, string no)
		{
			ShipOrder shipOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Priority], [Agent Code], [Status], [Closed Date], [Position X], [Position Y], [Ship Date], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type], [Ship Time], [Creation Date], [Production Site] FROM [Ship Order] WHERE [Entry No] = '"+no+"'");
			if (dataReader.Read())
			{
				shipOrder = new ShipOrder(dataReader);
			}
			
			dataReader.Close();
			return shipOrder;
		}

		public ShipOrder getAgentEntry(Database database, string agentCode, string no)
		{
			ShipOrder shipOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Priority], [Agent Code], [Status], [Closed Date], [Position X], [Position Y], [Ship Date], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type], [Ship Time], [Creation Date], [Production Site] FROM [Ship Order] WHERE [Agent Code] = '"+agentCode+"' AND [Entry No] = '"+no+"'");
			if (dataReader.Read())
			{
				shipOrder = new ShipOrder(dataReader);
			}
			
			dataReader.Close();
			return shipOrder;
		}

		
		public DataSet getActiveDataSet(Database database, string organizationNo, string agent, DateTime fromDate, DateTime toDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND [Agent Code] = ''";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Ship Address], [Ship Post Code], [Ship City], [Phone No], [Status], [Agent Code], [Cell Phone No], [Ship Date], [Details], [Comments], [Creation Date], [Production Site] FROM [Ship Order] WHERE [Organization No] = '"+organizationNo+"' AND [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" ORDER BY [Status], [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getActiveCallCenterDataSet(Database database, string organizationNo, string agent, DateTime fromDate, DateTime toDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND [Agent Code] = ''";

			string organizationQuery = " AND [Organization No] IN (SELECT [No] FROM [Organization] WHERE [Call Center Member] = '1')";
			if ((organizationNo != null) && (organizationNo != "")) organizationQuery = " AND [Organization No] = '"+organizationNo+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Ship Address], [Ship Post Code], [Ship City], [Phone No], [Status], [Agent Code], [Cell Phone No], [Ship Date], [Details], [Comments], [Creation Date] FROM [Ship Order] WHERE [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" "+organizationQuery+" ORDER BY [Status], [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getUnVerifiedDataSet(Database database, string organizationNo, string agent, DateTime fromDate, DateTime toDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND [Agent Code] = ''";

			string organizationQuery = " AND [Organization No] IN (SELECT [No] FROM [Organization] WHERE [Call Center Member] = '1')";
			if ((organizationNo != null) && (organizationNo != "")) organizationQuery = " AND [Organization No] = '"+organizationNo+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Ship Address], [Ship Post Code], [Ship City], [Phone No], [Status], [Agent Code], [Cell Phone No], [Ship Date], [Details], [Comments], [Creation Date] FROM [Ship Order] WHERE [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND [Customer No] IN (SELECT [No] FROM [Customer] WHERE [Unverified] = 1) "+agentQuery+" "+organizationQuery+" ORDER BY [Status], [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getCustomerShipOrderDataSet(Database database, string organizationNo, string agent, string customerNo)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND [Agent Code] = ''";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Ship Address], [Ship Post Code], [Ship City], [Phone No], [Status], [Agent Code], [Cell Phone No], [Ship Date], [Details], [Comments] FROM [Ship Order] WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' "+agentQuery+" ORDER BY [Status], [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getOngoingCustomerShipOrderDataSet(Database database, string organizationNo, string agent, string customerNo)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND [Agent Code] = ''";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Ship Address], [Ship Post Code], [Ship City], [Phone No], [Status], [Agent Code], [Cell Phone No], [Ship Date], [Details], [Comments] FROM [Ship Order] WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' AND [Status] < 6 "+agentQuery+" ORDER BY [Status], [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;

		}


		public bool checkCustomerShipOrderExists(Database database, string organizationNo, string customerNo, string customerShipAddressNo)
		{
			Customers customers = new Customers();
			Customer customer = customers.getEntry(database, organizationNo, customerNo);
			if (customer != null)
			{
				if (customer.editable) return false;
			}

			string customerShipAddressNoQuery = "";
			if ((customerShipAddressNo != null) && (customerShipAddressNo != "")) customerShipAddressNoQuery = " AND [Customer Ship Address No] = '"+customerShipAddressNo+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Ship Address], [Ship Post Code], [Ship City], [Phone No], [Status], [Agent Code], [Cell Phone No], [Ship Date], [Details], [Comments] FROM [Ship Order] WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' AND [Status] < 6 "+customerShipAddressNoQuery+" ORDER BY [Status], [Entry No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			if (dataSet.Tables[0].Rows.Count > 0) return true;
			return false;

		}

		public DataSet getDataSetEntry(Database database, string entryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Ship Date], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Phone No], [Cell Phone No], [Details], [Comments], [Priority], [Status], [Position X], [Position Y], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type], [Creation Date], [Production Site] FROM [Ship Order] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getMapDataSet(Database database, string organizationNo, DateTime shipDate)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Priority], [Agent Code], [Status], [Closed Date], [Position X], [Position Y], [Ship Date], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type], [Ship Time], [Creation Date], [Production Site] FROM [Ship Order] WHERE [Organization No] = '"+organizationNo+"' AND ([Position X] > 0 OR [Position Y] > 0) AND [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND [Status] < 6");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getMapDataSet(Database database, DateTime shipDate)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [Entry No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Priority], [Agent Code], [Status], [Closed Date], [Position X], [Position Y], [Ship Date], [Bill-to Customer No], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Direction Comment], [Direction Comment 2], [Payment Type], [Ship Time], [Creation Date], [Production Site] FROM [Ship Order] WHERE ([Position X] > 0 OR [Position Y] > 0) AND [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND [Status] < 6");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipOrder");
			adapter.Dispose();

			return dataSet;

		}

		public void setStatus(Database database, string entryNo, int status)
		{
			database.nonQuery("UPDATE [Ship Order] SET Status = '"+status+"' WHERE [Entry No] = '"+entryNo+"' AND [Agent Code] = '"+this.agentCode+"'");


		}

		public void changeShipDateOnNotLoadedOrders(Database database, string organizationNo, DateTime shipDate)
		{
			database.nonQuery("UPDATE [Ship Order] SET [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' WHERE [Organization No] = '"+organizationNo+"' AND [Ship Date] < '"+DateTime.Now.ToString("yyyy-MM-dd")+"' AND [Status] < 6");

		}


	}
}
