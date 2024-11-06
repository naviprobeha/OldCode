using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Shipments.
	/// </summary>
	public class ShipmentHeaders
	{
		public ShipmentHeaders()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ShipmentHeader getEntry(Database database, string no)
		{
			ShipmentHeader shipmentHeader = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Production Site], [Shipment Date], [Agent Code], [Payment Type], [Dairy Code], [Dairy No], [Reference], [Container No], [User Name], [Ship Order Entry No], [Position X], [Position Y], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Invoice No], [Line Order Entry No] FROM [Shipment Header] WHERE No = '"+no+"'");
			if (dataReader.Read())
			{
				shipmentHeader = new ShipmentHeader(database, dataReader);
			}
			
			dataReader.Close();
			return shipmentHeader;
		}
		public DataSet getDataSet(Database database, string organizationNo, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [No], [Customer No], [Customer Name], [Address], [City], [Shipment Date], [Agent Code], [Status], [Ship Order Entry No], [Position X], [Position Y], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Invoice No], [Line Order Entry No] FROM [Shipment Header] WHERE [Organization No] = '"+organizationNo+"' AND [Shipment Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Shipment Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' ORDER BY [Shipment Date] DESC, [No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getLineOrderDataSet(Database database, string lineOrderEntryNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Organization No], [Shipment Date], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Production Site], [Payment Type], [Dairy Code], [Dairy No], [Reference], [Agent Code], s.[Container No], [User Name], [Invoice No], s.[Line Order Entry No] FROM [Shipment Header] s, [Line Order Shipment] l WHERE l.[Line Order Entry No] = '"+lineOrderEntryNo+"' AND s.[No] = l.[Shipment No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public int getLineOrderForShipment(Database database, string containerNo, DateTime shipDate)
		{
			int lineOrderEntryNo = 0;

			SqlDataReader dataReader = database.query("SELECT [Line Order Entry No] FROM [Shipment Header] WHERE [Container No] = '"+containerNo+"' AND [Shipment Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND [Line Order Entry No] > 0 ORDER BY [No] DESC");

			if (dataReader.Read())
			{
				lineOrderEntryNo = dataReader.GetInt32(0);
			}

			dataReader.Close();

			return lineOrderEntryNo;
		}


		public DataSet getDataSet(Database database, string organizationNo, string agent, string containerNo, DateTime startDate, DateTime endDate)
		{
			string agentQuery = "";
			if ((agent != null) && (agent != "")) agentQuery = " AND [Agent Code] = '"+agent+"'";

			string containerQuery = "";
			if ((containerNo != null) && (containerNo != "")) containerQuery = " AND [Container No] = '"+containerNo+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [No], [Customer No], [Customer Name], [Address], [City], [Shipment Date], [Agent Code], [Status], [Ship Order Entry No], [Ship Name], [Ship Address], [Ship City], [Line Order Entry No], [Container No] FROM [Shipment Header] WHERE [Organization No] = '"+organizationNo+"' AND [Shipment Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Shipment Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' "+agentQuery+containerQuery+" ORDER BY [Shipment Date] DESC, [No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getCustomerShipmentDataSet(Database database, string organizationNo, string customerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [No], [Customer No], [Customer Name], [Address], [City], [Shipment Date], [Agent Code], [Status], [Ship Order Entry No] FROM [Shipment Header] WHERE [Organization No] = '"+organizationNo+"' AND [Customer No] = '"+customerNo+"' ORDER BY [Shipment Date] DESC, [No] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public int countAgentShipments(Database database, string agentCode, DateTime startDate, DateTime endDate)
		{
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Shipment Header] WHERE [Agent Code] = '"+agentCode+"' AND [Shipment Date] >= '"+startDate+"' AND [Shipment Date] <= '"+endDate+"'");

			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();

			return count;

		}


		public DataSet getSummaryDataSet(Database database, string organizationNo, DateTime startDate, DateTime endDate, string agent)
		{
			string agentQuery = "";
			if (agent != "") agentQuery = "AND [Agent Code] = '"+agent+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [No], [Customer No], [Customer Name], [Address], [City], [Shipment Date], [Agent Code], [Status], [Ship Order Entry No], [Payment Type] FROM [Shipment Header] WHERE [Organization No] = '"+organizationNo+"' AND [Shipment Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Shipment Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}
		//Bruk - använd denna koppling som underlag för att söka i två tabeller 
		public DataSet getItemSummaryDataSet(Database database, string organizationNo, string itemNo, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT h.[Organization No], h.[No], h.[Customer No], h.[Customer Name], h.[Address], h.[City], h.[Shipment Date], h.[Agent Code], h.[Status], h.[Ship Order Entry No], h.[Production Site] FROM [Shipment Header] h, [Shipment Line] l WHERE h.[Organization No] = '"+organizationNo+"' AND h.[Shipment Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND h.[Shipment Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' AND l.[Shipment No] = h.[No] AND l.[Item No] = '"+itemNo+"' ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getMarkedIDSummaryDataSet(Database database, string organizationNo, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT h.[Organization No], h.[No], h.[Customer No], h.[Customer Name], h.[Address], h.[City], h.[Shipment Date], h.[Agent Code], h.[Status], h.[Ship Order Entry No], h.[Production Site] FROM [Shipment Header] h, [Shipment Line ID] i WHERE h.[Organization No] = '"+organizationNo+"' AND h.[Shipment Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND h.[Shipment Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' AND i.[Shipment No] = h.[No] AND i.[ReMark Unit ID] <> '' ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getSummaryCashDataSet(Database database, string organizationNo, DateTime startDate, DateTime endDate, string agent)
		{
			string agentQuery = "";
			if (agent != "") agentQuery = "AND [Agent Code] = '"+agent+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Organization No], [No], [Customer No], [Customer Name], [Address], [City], [Shipment Date], [Agent Code], [Status], [Ship Order Entry No] FROM [Shipment Header] WHERE [Organization No] = '"+organizationNo+"' AND [Shipment Date] >= '"+startDate.ToString("yyyy-MM-dd")+"' AND [Shipment Date] <= '"+endDate.ToString("yyyy-MM-dd")+"' "+agentQuery+" AND [Payment Type] = 1 ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getAvailableDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Organization No], [Shipment Date], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Production Site], [Payment Type], [Dairy Code], [Dairy No], [Reference], [Agent Code], [Container No], [User Name], [Invoice No], [Line Order Entry No] FROM [Shipment Header] WHERE [Status] = 1 ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getContainers(Database database, string organizationNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Container No] FROM [Shipment Header] WHERE [Organization No] = '"+organizationNo+"' GROUP BY [Container No] ORDER BY [Container No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shipment");
			adapter.Dispose();

			return dataSet;

		}

		public void setStatus(Database database, string shipmentNo, int status)
		{
			database.nonQuery("UPDATE [Shipment Header] SET [Status] = '"+status.ToString()+"' WHERE [No] = '"+shipmentNo+"'");

		}

		public void resend(Database database)
		{
			database.nonQuery("UPDATE [Shipment Header] SET [Status] = '1' WHERE [Status] = '2'");
		}


		public string getShipmentContent(Database database, string shipmentNo)
		{
			ShipmentLines shipmentLines = new ShipmentLines(database);
			DataSet shipmentLinesDataSet = shipmentLines.getShipmentLinesDataSet(shipmentNo);

			int i = 0;
			string content = "";

			Items items = new Items();

			while (i < shipmentLinesDataSet.Tables[0].Rows.Count)
			{
				Item item = items.getEntry(database, shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());

				content = content + shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString()+" "+item.unitOfMeasure+" "+shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();

				ShipmentLineIds shipmentLineIds = new ShipmentLineIds(database);
				DataSet shipmentLineIdDataSet = shipmentLineIds.getShipmentLineIdDataSet(shipmentNo, int.Parse(shipmentLinesDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()));

				int j = 0;

				string ids = "";

				if (shipmentLineIdDataSet.Tables[0].Rows.Count > 0)
				{
					content = content + ": ";
					while (j < shipmentLineIdDataSet.Tables[0].Rows.Count)
					{
						if (ids != "") ids = ids + ", ";
						ids = ids + shipmentLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString();
						if (shipmentLineIdDataSet.Tables[0].Rows[j].ItemArray.GetValue(5).ToString() != "") ids = ids + "(R)";

						j++;
					}
				}

				ids = ids + ";";

				content = content + ids;
				

				i++;
			}

			return content;
		}

		public ShipmentHeader getLastShipmentForCustomer(Database database, string customerNo)
		{
			ShipmentHeader shipmentHeader = null;
			
			SqlDataReader dataReader = database.query("SELECT [Organization No], [No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Production Site], [Shipment Date], [Agent Code], [Payment Type], [Dairy Code], [Dairy No], [Reference], [Container No], [User Name], [Ship Order Entry No], [Position X], [Position Y], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Invoice No], [Line Order Entry No] FROM [Shipment Header] WHERE [Customer No] = '"+customerNo+"' ORDER BY [Shipment Date] DESC");
			if (dataReader.Read())
			{
				shipmentHeader = new ShipmentHeader(database, dataReader);
			}
			
			dataReader.Close();
			return shipmentHeader;

		}


	}
}
