using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLines.
	/// </summary>
	public class LineJournals
	{
		public LineJournals()
		{

		}

		public DataSet getAssignableDataSet(Database database)
		{

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time] [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] < 8 AND [Ship Date] >= '"+DateTime.Today.ToString("yyyy-MM-dd")+"' ORDER BY [Ship Date]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}	

		public DataSet getActiveDataSet(Database database, string factoryCode, string organizationNo, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = "AND [Arrival Factory Code] = '"+factoryCode+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] < 8 AND [Organization No] = '"+organizationNo+"' AND [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+factoryQuery+" ORDER BY [Ship Date] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}	

		public DataSet getActiveDataSet(Database database, string factoryCode, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = "AND [Arrival Factory Code] = '"+factoryCode+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] < 8 AND [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+factoryQuery+" ORDER BY [Ship Date] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}	

		public DataSet getFactoryDataSet(Database database, string factoryCode, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = "AND [Arrival Factory Code] = '"+factoryCode+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] < 8 AND [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+factoryQuery+" ORDER BY [Ship Date] DESC, [Status] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}	


		public DataSet getReportedDataSet(Database database, string factoryCode, string organizationNo, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = "AND [Arrival Factory Code] = '"+factoryCode+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] > 7 AND [Organization No] = '"+organizationNo+"' AND [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+factoryQuery+" ORDER BY [Ship Date] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}	

		public DataSet getReportedDataSet(Database database, string factoryCode, DateTime fromDate, DateTime toDate)
		{
			string factoryQuery = "";
			if ((factoryCode != "") && (factoryCode != null)) factoryQuery = "AND [Arrival Factory Code] = '"+factoryCode+"'";

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] > 7 AND [Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND [Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' "+factoryQuery+" ORDER BY [Ship Date] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}	

		public DataSet getDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] ORDER BY [Ship Date] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}

		public DataSet getStatusDataSet(Database database, int status)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] = '"+status+"' ORDER BY [Ship Date] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}


		public LineJournal getEntry(Database database, string entryNo)
		{
			LineJournal lineJournal = null;
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Entry No] = '"+entryNo+"'");
			if (dataReader.Read())
			{
				lineJournal = new LineJournal(dataReader);
			}
			
			dataReader.Close();
			return lineJournal;
		}

		public ArrayList getJournals(Database database, string agentCode, DateTime shipDate)
		{
			ArrayList journalList = new ArrayList();
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Agent Code] = '"+agentCode+"' AND [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND [Status] < 6");
			while (dataReader.Read())
			{
				LineJournal lineJournal = new LineJournal(dataReader);
				journalList.Add(lineJournal);
			}
			
			dataReader.Close();
			return journalList;
		}

		public ArrayList getPlanningJournalsByArrivalTime(Database database, string agentCode)
		{
			ArrayList journalList = new ArrayList();
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Agent Code] = '"+agentCode+"' AND [Status] < 7 ORDER BY [Arrival Date], [Arrival Time]");
			while (dataReader.Read())
			{
				LineJournal lineJournal = new LineJournal(dataReader);
				journalList.Add(lineJournal);
			}
			
			dataReader.Close();
			return journalList;
		}

		public DataSet getUnSentFactoryLineJournals(Database database, string factoryCode)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] >= 7 AND [Sent To Factory] = 0 AND [Arrival Factory Code] = '"+factoryCode+"' ORDER BY [Entry No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
		}


		public ArrayList getJournals(Database database, string agentCode, DateTime shipDate, string routeGroupCode)
		{
			ArrayList journalList = new ArrayList();
			
			SqlDataReader dataReader = database.query("SELECT j.[Entry No], j.[Organization No], j.[Ship Date], j.[Agent Code], j.[Status], j.[Departure Factory Code], j.[Arrival Factory Code], j.[Arrival Date], j.[Arrival Time], j.[Calculated Distance], j.[Measured Distance], j.[Reported Distance], j.[Forced Assignment], j.[Ending Travel Distance], j.[Ending Travel Time], j.[Total Distance], j.[Total Time], j.[Departure Date], j.[Departure Time], j.[Sent To Factory], j.[Reported Distance Single], j.[Reported Distance Trailer], j.[Agent Storage Group], j.[Invoice Received], j.[Parent Journal Entry No], j.[Drop Wait Time], j.[Factory Confirmed Wait Time] FROM [Line Journal] j, [Line Order] o WHERE j.[Agent Code] = '"+agentCode+"' AND j.[Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND j.[Status] < 6 AND o.[Line Journal Entry No] = j.[Entry No] AND o.[Route Group Code] = '"+routeGroupCode+"'");
			while (dataReader.Read())
			{
				LineJournal lineJournal = new LineJournal(dataReader);
				journalList.Add(lineJournal);
			}
			
			dataReader.Close();
			return journalList;
		}
		
		public ArrayList getAutomaticJournals(Database database, string agentCode, DateTime shipDate)
		{
			ArrayList journalList = new ArrayList();
			
			SqlDataReader dataReader = database.query("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Agent Code] = '"+agentCode+"' AND [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND [Status] < 6 AND [Forced Assignment] = 0");
			while (dataReader.Read())
			{
				LineJournal lineJournal = new LineJournal(dataReader);
				journalList.Add(lineJournal);
			}
			
			dataReader.Close();
			return journalList;
		}

		public ArrayList getAutomaticJournals(Database database, string agentCode, DateTime shipDate, string routeGroupCode)
		{
			ArrayList journalList = new ArrayList();
			
			SqlDataReader dataReader = database.query("SELECT j.[Entry No], j.[Organization No], j.[Ship Date], j.[Agent Code], j.[Status], j.[Departure Factory Code], j.[Arrival Factory Code], j.[Arrival Date], j.[Arrival Time], j.[Calculated Distance], j.[Measured Distance], j.[Reported Distance], j.[Forced Assignment], j.[Ending Travel Distance], j.[Ending Travel Time], j.[Total Distance], j.[Total Time], j.[Departure Date], j.[Departure Time], j.[Sent To Factory], j.[Reported Distance Single], j.[Reported Distance Trailer], j.[Agent Storage Group], j.[Invoice Received], j.[Parent Journal Entry No], j.[Drop Wait Time], j.[Factory Confirmed Wait Time] FROM [Line Journal] j, [Line Order] o WHERE j.[Agent Code] = '"+agentCode+"' AND j.[Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND j.[Status] < 6 AND o.[Line Journal Entry No] = j.[Entry No] AND o.[Route Group Code] = '"+routeGroupCode+"' AND j.[Forced Assignment] = 0");
			while (dataReader.Read())
			{
				LineJournal lineJournal = new LineJournal(dataReader);
				journalList.Add(lineJournal);
			}
			
			dataReader.Close();
			return journalList;
		}


		/*
		public int countContainers(Database database, string agentCode, DateTime shipDate)
		{
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] o, [Line Order Container] c, [Line Journal] j WHERE o.[Line Journal Entry No] = j.[Entry No] AND j.[Agent Code] = '"+agentCode+"' AND j.[Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND o.[Entry No] = c.[Line Order Entry No]");
	
			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;

		}
		*/
		
		public int countContainers(Database database, string agentCode, DateTime shipDate, int calculationType)
		{

			int sum = 0;

			ContainerUnits containerUnits = new ContainerUnits();
			DataSet containerUnitDataSet = containerUnits.getDataSet(database, calculationType);
			int i = 0;
			while (i < containerUnitDataSet.Tables[0].Rows.Count)
			{
				ContainerUnit containerUnit = new ContainerUnit(containerUnitDataSet.Tables[0].Rows[i]);

				if (calculationType == 0) sum = sum + countContainers(database, agentCode, shipDate, containerUnit.code);	
				if (calculationType == 1) sum = sum + (calcContainers(database, agentCode, shipDate, containerUnit.code) * containerUnit.volumeFactor);	

				i++;
			}

			return sum;
		}


		private int countContainers(Database database, string agentCode, DateTime shipDate, string unitCode)
		{

			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] o, [Line Order Container] oc, [Line Journal] j, [Container] c, [Container Type] ct WHERE o.[Line Journal Entry No] = j.[Entry No] AND j.[Agent Code] = '"+agentCode+"' AND j.[Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND o.[Entry No] = oc.[Line Order Entry No] AND oc.[Container No] = c.[No] AND ct.[Code] = c.[Container Type Code] AND ct.[Unit Code] = '"+unitCode+"'");
	
			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;

		}

		private int calcContainers(Database database, string agentCode, DateTime shipDate, string unitCode)
		{

			ContainerTypes containerTypes = new ContainerTypes();
			DataSet containerTypeDataSet = containerTypes.getDataSet(database, unitCode);

			int volume = 0;

			int i = 0;
			while (i < containerTypeDataSet.Tables[0].Rows.Count)
			{

				SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order] o, [Line Order Container] oc, [Line Journal] j, [Container] c, [Container Type] ct WHERE o.[Line Journal Entry No] = j.[Entry No] AND j.[Agent Code] = '"+agentCode+"' AND j.[Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"' AND o.[Entry No] = oc.[Line Order Entry No] AND oc.[Container No] = c.[No] AND c.[Container Type Code] = '"+containerTypeDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"'");
	
				int count = 0;

				if (dataReader.Read())
				{
					count = int.Parse(dataReader.GetValue(0).ToString());
				}
				dataReader.Close();

				volume = volume + (int.Parse(containerTypeDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()) * count);

				i++;
			}

			return volume;

		}



		public DataSet getDataSetEntry(Database database, string agentCode, string entryNo)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Entry No] = '"+entryNo+"' AND [Agent Code] = '"+agentCode+"' AND [Status] < 8");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getDataSetEntry(Database database, string entryNo)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Entry No] = '"+entryNo+"' AND [Status] < 8");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getFactoryDataSetEntry(Database database, string entryNo, string factoryCode)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Entry No] = '"+entryNo+"' AND [Arrival Factory Code] = '"+factoryCode+"'");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
		}

		public DataSet getLineJournalAgentsForOperator(Database database, string operatorCode)
		{
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT DISTINCT j.[Agent Code] FROM [Line Journal] AS j INNER JOIN [Operator Factory] AS o ON j.[Arrival Factory Code] = o.[Factory No] WHERE (j.Status < 8) AND (o.[User ID] = '"+operatorCode+"') AND j.[Agent Code] <> ''");
			
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
		}


		public LineJournal createJournal(Database database, string agentCode, DateTime shipDate)
		{
			Agents agents = new Agents();
			Agent agent = agents.getAgent(database, agentCode);
			Organizations organizations = new Organizations();		
			Organization organization = organizations.getOrganization(database, agent.organizationNo);

			Factories factories = new Factories();
			Factory factory = factories.getEntry(database, organization.factoryCode);
			if (!factory.checkConditions(database))
			{
				factory = factories.getFirstActiveFactory(database);
			}

			if (factory != null)
			{

				LineJournal lineJournal = new LineJournal();
				if (organization.autoAssignJournals) lineJournal.agentCode = agentCode;
				lineJournal.shipDate = shipDate;
				lineJournal.organizationNo = organization.no;
				lineJournal.departureFactoryCode = factory.no;
				lineJournal.arrivalFactoryCode = factory.no;
				lineJournal.status = 0;
				lineJournal.agentStorageGroup = agent.agentStorageGroup;

				lineJournal.save(database);

				return lineJournal;
			}
			return null;
		}

		public LineJournal createJournal(Database database, Organization organization, DateTime shipDate)
		{

			Factories factories = new Factories();
			Factory factory = factories.getEntry(database, organization.factoryCode);
			if (!factory.checkConditions(database))
			{
				factory = factories.getFirstActiveFactory(database);
			}

			if (factory != null)
			{

				LineJournal lineJournal = new LineJournal();
				lineJournal.shipDate = shipDate;
				lineJournal.organizationNo = organization.no;
				lineJournal.departureFactoryCode = factory.no;
				lineJournal.arrivalFactoryCode = factory.no;
				lineJournal.status = 0;
				lineJournal.save(database);

				return lineJournal;
			}
			return null;
		}

		public void setStatus(Database database, string entryNo, int status)
		{

			SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Line Order] WHERE [Line Journal Entry No] = '"+entryNo+"' AND [Status] > 6");
 
			if (dataReader.Read())
			{
				if (status < 6) status = 6;
			}

			dataReader.Close();

			LineJournal lineJournal = getEntry(database, entryNo);

			if (lineJournal != null)
			{
				ServerLogging serverLogging = new ServerLogging(database);
				serverLogging.log("AUTO", "Setting status for Line Journal: "+entryNo+" from "+lineJournal.status+" to "+status);

				lineJournal.status = status;
				lineJournal.save(database);
			
			}

		}

		public LineJournal reportJournal(Database database, string agentCode, DataSet dataset)
		{
			ServerLogging serverLogging = new ServerLogging(database);

			int entryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
		
			LineJournal lineJournal = this.getEntry(database, entryNo.ToString());
			if (lineJournal != null)
			{
				if (lineJournal.agentCode == agentCode)
				{
					lineJournal.status = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());
					lineJournal.measuredDistance = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
					lineJournal.reportedDistance = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());

					try
					{
						lineJournal.reportedDistanceSingle = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());
						lineJournal.reportedDistanceTrailer = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());
					}
					catch(Exception)
					{}
					

					try
					{
						lineJournal.dropWaitTime = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
					}
					catch(Exception)
					{}


					if (lineJournal.status == 7)
					{
						lineJournal.shipDate = DateTime.Today;
					}

					lineJournal.save(database);

					serverLogging.log(agentCode, "Status: "+lineJournal.status);

					if (lineJournal.status >= 7)
					{
						serverLogging.log(agentCode, "Finding factory: "+lineJournal.arrivalFactoryCode);


						Factories factories = new Factories();
						Factory factory = factories.getEntry(database, lineJournal.arrivalFactoryCode);
						if (factory != null)
						{
							serverLogging.log(agentCode, "Checking droppoint for linejournal: "+lineJournal.entryNo+", factory: "+factory.no);

							if (factory.dropPoint)
							{
								serverLogging.log(agentCode, "Droppoint found.");

								ShippingCustomers shippingCustomers = new ShippingCustomers();
								ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, factory.shippingCustomerNo);
								if (shippingCustomer != null)
								{
									serverLogging.log(agentCode, "Found shipping customer: "+shippingCustomer.name);

									LineOrders lineOrders = new LineOrders();
									DataSet parentLineJournalDataSet = lineOrders.getParentLineJournalDataSet(database, lineJournal.entryNo);
									if (parentLineJournalDataSet.Tables[0].Rows.Count == 0)
									{
										DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo);
										int i = 0;
										while (i < lineOrderDataSet.Tables[0].Rows.Count)
										{
											LineOrder originalLineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
											serverLogging.log(agentCode, "Processing lineorder: "+originalLineOrder.entryNo);

											ShippingCustomer originalShippingCustomer = shippingCustomers.getEntry(database, originalLineOrder.shippingCustomerNo);
											if (originalShippingCustomer != null)
											{

												LineOrder lineOrder = new LineOrder();
												lineOrder.applyShippingCustomer(originalShippingCustomer);
												lineOrder.shipDate = lineJournal.arrivalDateTime;
												lineOrder.shipTime = lineJournal.arrivalDateTime;
												lineOrder.comments = "Hämtas: "+shippingCustomer.name;
												lineOrder.confirmedToDateTime = lineJournal.arrivalDateTime;
												lineOrder.createdByType = 0;
												lineOrder.enableAutoPlan = false;
												lineOrder.type = 2;
												lineOrder.arrivalFactoryCode = "";
												lineOrder.parentLineJournalEntryNo = lineJournal.entryNo;
												lineOrder.save(database);

												serverLogging.log(agentCode, "New lineorder created: "+lineOrder.entryNo);

												DataSet containerDataSet = originalLineOrder.getContainers(database);
												int j = 0;
												while (j < containerDataSet.Tables[0].Rows.Count)
												{
													LineOrderContainer originalLineOrderContainer = new LineOrderContainer(containerDataSet.Tables[0].Rows[j]);

													LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrder);
													lineOrderContainer.categoryCode = originalLineOrderContainer.categoryCode;
													lineOrderContainer.containerNo = originalLineOrderContainer.containerNo;
													lineOrderContainer.weight = originalLineOrderContainer.weight;
													lineOrderContainer.save(database);

													j++;
												}

												lineOrder.enableAutoPlan = true;
												lineOrder.save(database);
											}

											i++;
										}
									}
									else
									{
										serverLogging.log(agentCode, "Journal already exists.");
									}
								}
							}
						}
					}

					return lineJournal;
				}
			}

			return null;
		}

		public DataSet getDataSet(Database database, string factoryCode, DateTime arrivalDateTime)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Status] < 8 AND (([Arrival Date] = '"+arrivalDateTime.ToString("yyyy-MM-dd")+"' AND [Arrival Time] > '"+arrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"') OR ([Arrival Date] > '"+arrivalDateTime.ToString("yyyy-MM-dd")+"')) AND [Arrival Factory Code] = '"+factoryCode+"' ORDER BY [Arrival Date], [Status] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}	

		public ArrayList getPostMortemLineOrders(Database database, DateTime fromDate, DateTime toDate)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT DISTINCT lo.[Entry No] FROM [Line Journal] lj, [Line Order] lo, [Line Order Shipment] los, [Shipment Line ID] sli WHERE lj.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND lj.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND lj.[Entry No] = lo.[Line Journal Entry No] AND los.[Line Order Entry No] = lo.[Entry No] AND los.[Shipment No] = sli.[Shipment No] AND sli.[Post Mortem] = 1");

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
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT DISTINCT lo.[Entry No] FROM [Line Journal] lj, [Line Order] lo, [Line Order Shipment] los, [Shipment Line ID] sli WHERE lj.[Ship Date] >= '"+fromDate.ToString("yyyy-MM-dd")+"' AND lj.[Ship Date] <= '"+toDate.ToString("yyyy-MM-dd")+"' AND lj.[Entry No] = lo.[Line Journal Entry No] AND los.[Line Order Entry No] = lo.[Entry No] AND los.[Shipment No] = sli.[Shipment No] AND sli.[BSE Testing] = 1");

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

		public DataSet getUnConfirmedDataSet(Database database, string factoryCode)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Organization No], [Ship Date], [Agent Code], [Status], [Departure Factory Code], [Arrival Factory Code], [Arrival Date], [Arrival Time], [Calculated Distance], [Measured Distance], [Reported Distance], [Forced Assignment], [Ending Travel Distance], [Ending Travel Time], [Total Distance], [Total Time], [Departure Date], [Departure Time], [Sent To Factory], [Reported Distance Single], [Reported Distance Trailer], [Agent Storage Group], [Invoice Received], [Parent Journal Entry No], [Drop Wait Time], [Factory Confirmed Wait Time] FROM [Line Journal] WHERE [Drop Wait Time] > 0 AND [Factory Confirmed Wait Time] = '' AND [Arrival Factory Code] = '"+factoryCode+"' ORDER BY [Arrival Date], [Status] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "lineJournal");
			adapter.Dispose();

			return dataSet;
			
		}	

	}
}
