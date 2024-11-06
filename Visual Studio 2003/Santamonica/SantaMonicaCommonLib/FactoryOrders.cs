using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class FactoryOrders
	{

		public static int TYPE_PLANNED = 0;
		public static int TYPE_ENTERED = 1;
		public static int TYPE_CONFIRMED = 2;

		public FactoryOrders()
		{
			//
			// TODO: Add constructor logic here
			//			
		}

		public FactoryOrder getEntry(Database database, string no)
		{
			FactoryOrder factoryOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Entry No] = '"+no+"'");
			if (dataReader.Read())
			{
				factoryOrder = new FactoryOrder(dataReader);
			}
			
			dataReader.Close();
			return factoryOrder;
		}

		public FactoryOrder getLastEntry(Database database, string shippingCustomerNo)
		{
			FactoryOrder factoryOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT TOP 1 [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Factory Type] = 1 AND [Factory No] = '"+shippingCustomerNo+"' ORDER BY [Ship Date] DESC");
			if (dataReader.Read())
			{
				factoryOrder = new FactoryOrder(dataReader);
			}
			
			dataReader.Close();
			return factoryOrder;
		}

		public DataSet getConsumerEntries(Database database, string consumerNo, DateTime dateTime)
		{
			string query = "SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Consumer No] = '"+consumerNo+"' AND [Arrival Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND ([Arrival Time] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"' AND [Arrival Time] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"') AND [Status] < 4 ORDER BY [Ship Date]";
			if (dateTime.Hour == 0) query = "SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Consumer No] = '"+consumerNo+"' AND [Arrival Date] = '"+dateTime.AddDays(-1).ToString("yyyy-MM-dd")+"' AND ([Arrival Time] <= '"+dateTime.ToString("1754-01-02 HH:mm:ss")+"' AND [Arrival Time] > '"+dateTime.AddHours(-1).ToString("1754-01-02 HH:mm:ss")+"') AND [Status] < 4 ORDER BY [Ship Date]";

			SqlDataAdapter adapter = database.dataAdapterQuery(query);

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;
		}

		public ArrayList getConsumerList(Database database, string consumerNo, DateTime dateTime)
		{
			string query = "SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Consumer No] = '"+consumerNo+"' AND [Arrival Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND ([Arrival Time] <= '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"' AND [Arrival Time] > '"+dateTime.AddHours(-1).ToString("1754-01-01 HH:mm:ss")+"') AND [Status] < 4 ORDER BY [Ship Date]";
			if (dateTime.Hour == 0) query = "SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Consumer No] = '"+consumerNo+"' AND [Arrival Date] = '"+dateTime.AddDays(-1).ToString("yyyy-MM-dd")+"' AND ([Arrival Time] <= '"+dateTime.ToString("1754-01-02 HH:mm:ss")+"' AND [Arrival Time] > '"+dateTime.AddHours(-1).ToString("1754-01-02 HH:mm:ss")+"') AND [Status] < 4 ORDER BY [Ship Date]";

				
			SqlDataAdapter adapter = database.dataAdapterQuery(query);

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumerInventoryOrder");
			adapter.Dispose();

			ArrayList arrayList = new ArrayList();

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				FactoryOrder factoryOrder = new FactoryOrder(dataSet.Tables[0].Rows[i]);	
				arrayList.Add(factoryOrder);

				i++;
			}

			return arrayList;
		}

		public FactoryOrder findLastConsumerEntry(Database database, string consumerNo)
		{
			FactoryOrder factoryOrder = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Consumer No] = '"+consumerNo+"' AND [Status] < 4 ORDER BY [Arrival Date] DESC, [Arrival Time] DESC");
			if (dataReader.Read())
			{
				factoryOrder = new FactoryOrder(dataReader);
			}
			
			dataReader.Close();
			return factoryOrder;
		}

		public DataSet getStatusDataSet(Database database, int status)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Status] = '"+status+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getUnAssignedDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Status] = '0' AND [Organization No] != '' AND [Ship Date] >= '"+DateTime.Today.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getPlannedDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Type] = '0' AND [Status] < '4' ORDER BY [Ship Date], [Ship Time]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;
		}

		public float calcPlannedQuantity(Database database, string factoryNo, DateTime dateTime)
		{
			SqlDataReader dataReader = database.query("SELECT SUM([Quantity]) FROM [Factory Order] WHERE [Status] < 3 AND (([Ship Date] = '"+dateTime.ToString("yyyy-MM-dd")+"' AND [Ship Time] < '"+dateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Ship Date] < '"+dateTime.ToString("yyyy-MM-dd")+"'))");

			float weight = 0;

			if (dataReader.Read())
			{
				if (dataReader.GetValue(0).ToString() != "")
				{
					weight = float.Parse(dataReader.GetValue(0).ToString());
				}
			}
			dataReader.Close();

			return weight;
		}


		public DataSet getOrganizationDataSet(Database database, string organizationNo, DateTime shipDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Organization No] = '"+organizationNo+"' AND [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND [Status] = '0'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;
		}


		public DataSet getActiveDataSet(Database database, string agent, DateTime fromDate, DateTime toDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND [Agent Code] = ''";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" ORDER BY [Status], [Ship Date], [Ship Time]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getActiveFactoryDataSet(Database database, string userId, string agent, DateTime fromDate, DateTime toDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND [Agent Code] = ''";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], o.[Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] o, [Operator Factory] f WHERE [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" AND [Factory Type] = 0 AND f.[User ID] = '"+userId+"' AND f.[Factory No] = o.[Factory No] ORDER BY [Status], [Ship Date], [Ship Time]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getActiveCustomerDataSet(Database database, string shippingCustomerNo, DateTime fromDate, DateTime toDate)
		{

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND [Factory Type] = 1 AND [Factory No] = '"+shippingCustomerNo+"' ORDER BY [Status], [Ship Date], [Ship Time]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getActiveConsumerDataSet(Database database, string consumerNo, DateTime fromDate, DateTime toDate)
		{

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND [Consumer No] = '"+consumerNo+"' ORDER BY [Status], [Ship Date], [Ship Time]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}

		
		public DataSet getActiveOrganizationDataSet(Database database, string organizationNo, string agent, DateTime fromDate, DateTime toDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";
			if ((agent != null) && (agent == "-")) agentQuery = " AND [Agent Code] = ''";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Organization No] = '"+organizationNo+"' AND [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" ORDER BY [Status], [Ship Date], [Ship Time]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}

	
		public DataSet getDataSetEntry(Database database, string entryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Entry No] = '"+entryNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;

		}

	
		public void setStatus(Database database, string entryNo, int status)
		{
			database.nonQuery("UPDATE [Factory Order] SET Status = '"+status+"' WHERE [Entry No] = '"+entryNo+"'");


		}


		public bool orderExists(Database database, string shippingCustomerNo, DateTime shipDate)
		{
			bool exists = false;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Factory No] = '"+shippingCustomerNo+"' AND [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"'");
			if (dataReader.Read())
			{
				exists = true;
			}
			
			dataReader.Close();
			return exists;
		}

		public void deleteAll(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Status] < 3");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				FactoryOrder factoryOrder = new FactoryOrder(dataSet.Tables[0].Rows[i]);
				factoryOrder.delete(database);

				i++;
			}


		}

		public int countAgentOrders(Database database, DateTime shipDate, string agentCode)
		{
			int count = 0;
			
			SqlDataReader dataReader = database.query("SELECT COUNT(*) as count FROM [Factory Order] WHERE [Agent Code] = '"+agentCode+"' AND [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"'");
			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			
			dataReader.Close();
			return count;
			

		}

		public DataSet getFactoryOrderAgentsForOperator(Database database, string operatorCode)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT DISTINCT j.[Agent Code] FROM [Factory Order] AS j INNER JOIN [Operator Factory] AS o ON j.[Factory No] = o.[Factory No] WHERE (j.Status = 2) AND (o.[User ID] = '"+operatorCode+"')");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getFactoryOrderAgentsForConsumer(Database database, string consumerNo)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT DISTINCT [Agent Code] FROM [Factory Order] WHERE (Status = 3) AND ([Consumer No] = '"+consumerNo+"')");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;
		}

		public float getFactoryOrderQuantity(Database database, DateTime fromDate, DateTime toDate, string consumerNo, int factoryType, string factoryNo)
		{
			float amount = 0;

			SqlDataReader dataReader = database.query("SELECT SUM([Real Quantity]) FROM [Factory Order] WHERE Status = 4 AND ([Arrival Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Arrival Date] <= '"+toDate.ToString("yyyy-MM-dd")+"') AND [Consumer No] = '"+consumerNo+"' AND [Factory Type] = '"+factoryType+"' AND [Factory No] = '"+factoryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					amount = float.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception)
				{
					amount = 0;
				}
			}

			dataReader.Close();

			return amount;
		}

		public float getFactoryOrderQuantity(Database database, DateTime fromDate, DateTime toDate, int factoryType, string factoryNo)
		{
			float amount = 0;

			SqlDataReader dataReader = database.query("SELECT SUM([Real Quantity]) FROM [Factory Order] WHERE Status = 4 AND ([Arrival Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Arrival Date] <= '"+toDate.ToString("yyyy-MM-dd")+"') AND [Factory Type] = '"+factoryType+"' AND [Factory No] = '"+factoryNo+"'");

			if (dataReader.Read())
			{
				try
				{
					amount = float.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception)
				{
					amount = 0;
				}
			}

			dataReader.Close();

			return amount;
		}

		public float getFactoryOrderQuantity(Database database, DateTime fromDate, DateTime toDate, int factoryType)
		{
			float amount = 0;

			SqlDataReader dataReader = database.query("SELECT SUM([Real Quantity]) FROM [Factory Order] WHERE Status = 4 AND ([Arrival Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Arrival Date] <= '"+toDate.ToString("yyyy-MM-dd")+"') AND [Factory Type] = '"+factoryType+"'");

			if (dataReader.Read())
			{
				try
				{
					amount = float.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception)
				{
					amount = 0;
				}
			}

			dataReader.Close();

			return amount;
		}

		public float getFactoryOrderQuantity(Database database, DateTime fromDate, DateTime toDate, string consumerNo)
		{
			float amount = 0;

			SqlDataReader dataReader = database.query("SELECT SUM([Real Quantity]) FROM [Factory Order] WHERE Status = 4 AND ([Arrival Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Arrival Date] <= '"+toDate.ToString("yyyy-MM-dd")+"') AND [Consumer No] = '"+consumerNo+"'");

			if (dataReader.Read())
			{
				try
				{
					amount = float.Parse(dataReader.GetValue(0).ToString());
				}
				catch(Exception)
				{
					amount = 0;
				}
			}

			dataReader.Close();

			return amount;
		}

		public DataSet getNotSentDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time] FROM [Factory Order] WHERE [Navision Status] = '1'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryOrder");
			adapter.Dispose();

			return dataSet;
		}

		public float calcFactoryTotal(Database database, int factoryType, string factoryNo, DateTime fromDate, DateTime toDate)
		{

			float total = 0;

			SqlDataReader dataReader = database.query("SELECT SUM([Real Quantity]) FROM [Factory Order] WHERE Status >= 3 AND ([Pickup Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Pickup Date] <= '"+toDate.ToString("yyyy-MM-dd")+"') AND [Factory Type] = '"+factoryType+"' AND [Factory No] = '"+factoryNo+"'");

			if (dataReader.Read())
			{
				if (!dataReader.IsDBNull(0)) total = float.Parse(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();

			return total;

		}
	}
}
