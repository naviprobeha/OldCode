using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class LineOrder
	{
		public int entryNo;
		public string organizationNo;
		public int lineJournalEntryNo;
		public DateTime shipDate;
		public string shippingCustomerNo;
		public string shippingCustomerName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;

		public string phoneNo;
		public string cellPhoneNo;

		public string details;
		public string comments;

		public string directionComment;
		public string directionComment2;

		public int optimizingSortOrder;
		public int travelDistance;
		public int travelTime;

		public int positionX;
		public int positionY;

		public int type;
		public int status;
		public DateTime closedDateTime;

		public DateTime shipTime;
		public DateTime creationDate;

		public int createdByType;
		public string createdByCode;

		public DateTime confirmedToDateTime;

		public bool enableAutoPlan;

		public string routeGroupCode;
		public int priority;

		public bool isLoaded;

		public string planningAgentCode;

		public string driverName;

		public string arrivalFactoryCode;

		public int deletedByType;
		public string deletedByCode;

		public int loadWaitTime;
		public int parentLineJournalEntryNo;


		public LineOrder(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.organizationNo = dataReader.GetValue(1).ToString();
			this.lineJournalEntryNo = dataReader.GetInt32(2);
			this.shipDate = dataReader.GetDateTime(3);
		
			this.shippingCustomerNo = dataReader.GetValue(4).ToString();
			this.shippingCustomerName = dataReader.GetValue(5).ToString();
			this.address = dataReader.GetValue(6).ToString();
			this.address2 = dataReader.GetValue(7).ToString();
			this.postCode = dataReader.GetValue(8).ToString();
			this.city = dataReader.GetValue(9).ToString();
			this.countryCode = dataReader.GetValue(10).ToString();
			this.phoneNo = dataReader.GetValue(11).ToString();
			this.cellPhoneNo = dataReader.GetValue(12).ToString();
			this.details = dataReader.GetValue(13).ToString();
			this.comments = dataReader.GetValue(14).ToString();
			
			this.status = dataReader.GetInt32(15);
			DateTime closedDate = dataReader.GetDateTime(16);
			this.shipTime = dataReader.GetDateTime(17);
			this.creationDate = dataReader.GetDateTime(18);

			this.directionComment = dataReader.GetValue(19).ToString();
			this.directionComment2 = dataReader.GetValue(20).ToString();
		
			this.positionX = dataReader.GetInt32(21);
			this.positionY = dataReader.GetInt32(22);

			this.type = dataReader.GetInt32(23);

			this.optimizingSortOrder = dataReader.GetInt32(24);

			this.createdByType = dataReader.GetInt32(25);
			this.createdByCode = dataReader.GetValue(26).ToString();

			DateTime confirmedToDate = dataReader.GetDateTime(27);
			DateTime confirmedToTime = dataReader.GetDateTime(28);
			this.confirmedToDateTime = new DateTime(confirmedToDate.Year, confirmedToDate.Month, confirmedToDate.Day, confirmedToTime.Hour, confirmedToTime.Minute, confirmedToTime.Second);

			this.enableAutoPlan = false;
			if (dataReader.GetValue(29).ToString() == "1") this.enableAutoPlan = true;

			this.travelDistance = dataReader.GetInt32(30);
			this.travelTime = dataReader.GetInt32(31);

			DateTime closedTime = dataReader.GetDateTime(32);

			this.closedDateTime = new DateTime(closedDate.Year, closedDate.Month, closedDate.Day, closedTime.Hour, closedTime.Minute, closedTime.Second);

			this.routeGroupCode = dataReader.GetValue(33).ToString();
			this.priority = dataReader.GetInt32(34);

			this.isLoaded = false;
			if (dataReader.GetValue(35).ToString() == "1") this.isLoaded = true;

			this.planningAgentCode = dataReader.GetValue(36).ToString();
			this.driverName = dataReader.GetValue(37).ToString();

			this.arrivalFactoryCode = dataReader.GetValue(38).ToString();

			this.loadWaitTime = dataReader.GetInt32(39);
			this.parentLineJournalEntryNo = dataReader.GetInt32(40);
		}

		public LineOrder(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.organizationNo = dataRow.ItemArray.GetValue(1).ToString();
			this.lineJournalEntryNo = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.shipDate = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
		
			this.shippingCustomerNo = dataRow.ItemArray.GetValue(4).ToString();
			this.shippingCustomerName = dataRow.ItemArray.GetValue(5).ToString();
			this.address = dataRow.ItemArray.GetValue(6).ToString();
			this.address2 = dataRow.ItemArray.GetValue(7).ToString();
			this.postCode = dataRow.ItemArray.GetValue(8).ToString();
			this.city = dataRow.ItemArray.GetValue(9).ToString();
			this.countryCode = dataRow.ItemArray.GetValue(10).ToString();
			this.phoneNo = dataRow.ItemArray.GetValue(11).ToString();
			this.cellPhoneNo = dataRow.ItemArray.GetValue(12).ToString();
			this.details = dataRow.ItemArray.GetValue(13).ToString();
			this.comments = dataRow.ItemArray.GetValue(14).ToString();
			
			this.status = int.Parse(dataRow.ItemArray.GetValue(15).ToString());
			DateTime closedDate = DateTime.Parse(dataRow.ItemArray.GetValue(16).ToString());
			this.shipTime = DateTime.Parse(dataRow.ItemArray.GetValue(17).ToString());
			this.creationDate = DateTime.Parse(dataRow.ItemArray.GetValue(18).ToString());

			this.directionComment = dataRow.ItemArray.GetValue(19).ToString();
			this.directionComment2 = dataRow.ItemArray.GetValue(20).ToString();
		
			this.positionX = int.Parse(dataRow.ItemArray.GetValue(21).ToString());
			this.positionY = int.Parse(dataRow.ItemArray.GetValue(22).ToString());

			this.type = int.Parse(dataRow.ItemArray.GetValue(23).ToString());

			this.optimizingSortOrder = int.Parse(dataRow.ItemArray.GetValue(24).ToString());

			this.createdByType = int.Parse(dataRow.ItemArray.GetValue(25).ToString());
			this.createdByCode = dataRow.ItemArray.GetValue(26).ToString();

			DateTime confirmedToDate = DateTime.Parse(dataRow.ItemArray.GetValue(27).ToString());
			DateTime confirmedToTime = DateTime.Parse(dataRow.ItemArray.GetValue(28).ToString());
			this.confirmedToDateTime = new DateTime(confirmedToDate.Year, confirmedToDate.Month, confirmedToDate.Day, confirmedToTime.Hour, confirmedToTime.Minute, confirmedToTime.Second);

			this.enableAutoPlan = false;
			if (dataRow.ItemArray.GetValue(29).ToString() == "1") this.enableAutoPlan = true;

			this.travelDistance = int.Parse(dataRow.ItemArray.GetValue(30).ToString());
			this.travelTime = int.Parse(dataRow.ItemArray.GetValue(31).ToString());

			DateTime closedTime = DateTime.Parse(dataRow.ItemArray.GetValue(32).ToString());

			this.closedDateTime = new DateTime(closedDate.Year, closedDate.Month, closedDate.Day, closedTime.Hour, closedTime.Minute, closedTime.Second);

			this.routeGroupCode = dataRow.ItemArray.GetValue(33).ToString();
			this.priority = int.Parse(dataRow.ItemArray.GetValue(34).ToString());

			this.isLoaded = false;
			if (dataRow.ItemArray.GetValue(35).ToString() == "1") this.isLoaded = true;

			this.planningAgentCode = dataRow.ItemArray.GetValue(36).ToString();
			this.driverName = dataRow.ItemArray.GetValue(37).ToString();

			this.arrivalFactoryCode = dataRow.ItemArray.GetValue(38).ToString();

			this.loadWaitTime = int.Parse(dataRow.ItemArray.GetValue(39).ToString());
			this.parentLineJournalEntryNo = int.Parse(dataRow.ItemArray.GetValue(40).ToString());

		}


		public LineOrder(ShippingCustomer shippingCustomer)
		{
			this.closedDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.shipDate = DateTime.Now;
			this.shipTime = new DateTime(1754, 01, 01, 0, 0, 0, 0);
			this.shippingCustomerNo = "";
			this.creationDate = DateTime.Today;
			this.optimizingSortOrder = 0;
			this.confirmedToDateTime = new DateTime(1753, 01, 01, 0, 0, 0);
			this.parentLineJournalEntryNo = 0;

			applyShippingCustomer(shippingCustomer);
		}

		public LineOrder()
		{
			this.closedDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.shipDate = DateTime.Now;
			this.shipTime = new DateTime(1754, 01, 01, 0, 0, 0, 0);
			this.shippingCustomerNo = "";
			this.creationDate = DateTime.Today;
			this.optimizingSortOrder = 0;
			this.confirmedToDateTime = new DateTime(1753, 01, 01, 0, 0, 0);
			this.parentLineJournalEntryNo = 0;
		}

		public void save(Database database)
		{
			save(database, true);
		}

		public void save(Database database, bool synch)
		{
			this.shipTime = new DateTime(1754, 01, 01, shipTime.Hour, shipTime.Minute, shipTime.Second, shipTime.Millisecond);

			int enableAutoPlanVal = 0;
			if (this.enableAutoPlan) enableAutoPlanVal = 1;

			int isLoadedVal = 0;
			if (this.isLoaded) isLoadedVal = 1;

			if (entryNo == 0)
			{
				database.nonQuery("INSERT INTO [Line Order] ([Organization No], [Line Journal Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Details], [Comments], [Status], [Closed Date], [Ship Time], [Creation Date], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Type], [Optimizing Sort Order], [Created By Type], [Created By Code], [Confirmed To Date], [Confirmed To Time], [Enable Auto Plan], [Travel Distance], [Travel Time], [Closed Time], [Route Group Code], [Priority], [Is Loaded], [Planning Agent Code], [Driver Name], [Arrival Factory Code], [Load Wait Time], [Parent Line Journal Entry No]) VALUES ('"+this.organizationNo+"','"+this.lineJournalEntryNo+"','"+shipDate.ToString("yyyy-MM-dd")+"','"+this.shippingCustomerNo+"','"+this.shippingCustomerName+"','"+this.address+"','"+this.address2+"','"+this.postCode+"','"+this.city+"','"+this.countryCode+"','"+this.phoneNo+"','"+this.cellPhoneNo+"','"+this.details+"','"+this.comments+"',"+this.status+",'"+this.closedDateTime.ToString("yyyy-MM-dd")+"', '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"','"+creationDate.ToString("yyyy-MM-dd 00:00:00")+"','"+this.directionComment+"','"+this.directionComment2+"','"+positionX+"','"+positionY+"','"+type+"','"+optimizingSortOrder+"', '"+createdByType+"', '"+createdByCode+"', '"+this.confirmedToDateTime.ToString("yyyy-MM-dd 00:00:00")+"', '"+this.confirmedToDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+enableAutoPlanVal+"', '"+this.travelDistance+"', '"+this.travelTime+"', '"+this.closedDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+this.routeGroupCode+"', '"+this.priority+"', '"+isLoadedVal+"', '"+this.planningAgentCode+"', '"+this.driverName+"', '"+this.arrivalFactoryCode+"', '"+loadWaitTime+"', '"+this.parentLineJournalEntryNo+"')");
				entryNo = (int)database.getInsertedSeqNo();
				
			}
			else
			{
				database.nonQuery("UPDATE [Line Order] SET [Organization No] = '"+this.organizationNo+"', [Line Journal Entry No] = '"+this.lineJournalEntryNo+"', [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"', [Shipping Customer No] = '"+shippingCustomerNo+"', [Shipping Customer Name] = '"+shippingCustomerName+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Phone No] = '"+phoneNo+"', [Cell Phone No] = '"+cellPhoneNo+"', [Details] = '"+details+"', [Comments] = '"+comments+"', [Status] = '"+status+"', [Closed Date] = '"+closedDateTime.ToString("yyyy-MM-dd")+"', [Ship Time] = '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', [Creation Date] = '"+creationDate.ToString("yyyy-MM-dd")+"', [Direction Comment] = '"+directionComment+"', [Direction Comment 2] = '"+directionComment2+"', [Position X] = '"+positionX+"', [Position Y] = '"+positionY+"', [Type] = '"+type+"', [Optimizing Sort Order] = '"+this.optimizingSortOrder+"', [Created By Type] = '"+this.createdByType+"', [Created By Code] = '"+this.createdByCode+"', [Confirmed To Date] = '"+this.confirmedToDateTime.ToString("yyyy-MM-dd 00:00:00")+"', [Confirmed To Time] = '"+confirmedToDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Enable Auto Plan] = '"+enableAutoPlanVal+"', [Travel Distance] = '"+this.travelDistance+"', [Travel Time] = '"+this.travelTime+"', [Closed Time] = '"+this.closedDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Route Group Code] = '"+this.routeGroupCode+"', [Priority] = '"+this.priority+"', [Is Loaded] = '"+isLoadedVal+"', [Planning Agent Code] = '"+this.planningAgentCode+"', [Driver Name] = '"+this.driverName+"', [Arrival Factory Code] = '"+this.arrivalFactoryCode+"', [Load Wait Time] = '"+loadWaitTime+"', [Parent Line Journal Entry No] = '"+this.parentLineJournalEntryNo+"' WHERE [Entry No] = '"+entryNo+"'");

				if (synch) updateOrder(database);
			}


		}

		public void delete(Database database)
		{
			this.unassignToRoute(database);

			LineOrderHistory lineOrderHistory = new LineOrderHistory(this);
			lineOrderHistory.save(database);

			database.nonQuery("DELETE FROM [Line Order] WHERE [Entry No] = '"+entryNo+"'");
			database.nonQuery("DELETE FROM [Line Order Shipment] WHERE [Line Order Entry No] = '"+entryNo+"'");

			FactoryInventoryEntries factoryInventoryEntries = new FactoryInventoryEntries();
			factoryInventoryEntries.deleteLineOrderEntries(database, this.entryNo);
		}



		public void applyShippingCustomer(ShippingCustomer shippingCustomer)
		{

			shippingCustomerNo = shippingCustomer.no;

			phoneNo = shippingCustomer.phoneNo;
			cellPhoneNo = shippingCustomer.cellPhoneNo;
			
			directionComment = shippingCustomer.directionComment;
			directionComment2 = shippingCustomer.directionComment2;

			this.shippingCustomerName = shippingCustomer.name;
			this.address = shippingCustomer.address;
			this.address2 = shippingCustomer.address2;
			this.postCode = shippingCustomer.postCode;
			this.city = shippingCustomer.city;

			positionX = shippingCustomer.positionX;
			positionY = shippingCustomer.positionY;

			this.routeGroupCode = shippingCustomer.routeGroupCode;

			this.priority = shippingCustomer.priority;

			this.arrivalFactoryCode = shippingCustomer.preferedFactoryNo;
		}



		public string getStatusText()
		{
			if (status == 0) 
			{
				return "";
			}
			if (status == 1)
			{
				return "Avböjd";
			}
			if (status == 2)
			{
				return "Osäker";
			}
			if (status == 3) 
			{
				return "Inväntar rutt";
			}
			if (status == 4) 
			{
				return "Tilldelad";
			}
			if (status == 5) 
			{
				return "Skickad";
			}
			if (status == 6) 
			{
				return "Bekräftad";
			}
			if (status == 7) 
			{
				return "Lastad";
			}
			if (status == 8) 
			{
				return "Makulerad";
			}
			if (status == 10) 
			{
				return "Lossad";
			}

			return "";


		}


		public string getStatusIcon()
		{
			if (status == 0) 
			{
				return "ind_white.gif";
			}
			if (status == 1)
			{
				return "ind_red.gif";
			}
			if (status == 2) 
			{
				return "ind_orange.gif";
			}
			if (status == 3) 
			{
				return "ind_yellow.gif";
			}
			if (status == 4) 
			{
				return "ind_yellow.gif";
			}
			if (status == 5) 
			{
				return "ind_green.gif";
			}
			if (status == 6) 
			{
				return "ind_green.gif";
			}
			if (status == 7) 
			{
				return "ind_black.gif";
			}
			if (status == 8) 
			{
				return "ind_blue.gif";
			}
			if (status == 10) 
			{
				return "ind_black.gif";
			}

			return "ind_white.gif";
		}

		public string getType()
		{
			if (type == 0) return "Planerad";
			if (type == 1) return "Anmäld";
			if (type == 2) return "Bekräftad";

			return "";
		}

		public string getCreatedByType()
		{
			if (createdByType == 0) return "Automatplanering";
			if (createdByType == 1) return "Operator";
			if (createdByType == 2) return "Kund";

			return "";
		}

		public LineJournal getJournal(Database database)
		{
			LineJournals lineJournals = new LineJournals();
			return lineJournals.getEntry(database, this.lineJournalEntryNo.ToString());

		}

		public string getRouteName(Database database)
		{
			LineJournals lineJournals = new LineJournals();
			LineJournal lineJournal = lineJournals.getEntry(database, this.lineJournalEntryNo.ToString());

			if (lineJournal != null)
			{
				string routeName = lineJournal.entryNo.ToString()+" ("+lineJournal.agentCode+")";
				if (lineJournal.status == 8) routeName = routeName + " R";
				return routeName;
			}
			
			return "";
		}

		public string getAgentName(Database database)
		{
			LineJournals lineJournals = new LineJournals();
			LineJournal lineJournal = lineJournals.getEntry(database, this.lineJournalEntryNo.ToString());

			if (lineJournal != null)
			{
				return lineJournal.agentCode;
			}
			
			return "";
		}

		public bool checkOrderLoaded(Database database)
		{
			ContainerEntries containerEntries = new ContainerEntries();
			DataSet dataSet = containerEntries.getDocumentDataSet(database, 1, this.entryNo.ToString());
			if (dataSet.Tables[0].Rows.Count > 0) return true;
			return false;
		}

		public string getOrganizationName(Database database)
		{
			LineJournals lineJournals = new LineJournals();
			LineJournal lineJournal = lineJournals.getEntry(database, this.lineJournalEntryNo.ToString());

			if (lineJournal != null)
			{
				Organizations organizations = new Organizations();
				Organization organization = organizations.getOrganization(database, lineJournal.organizationNo);
				return organization.name;
			}
			
			return "";
		}

		public string getAgentMobileUser(Database database)
		{
			return this.driverName;
		}

		public Factory getArrivalFactory(Database database)
		{
			LineJournal lineJournal = this.getJournal(database);
			if (lineJournal != null)
			{
				Factories factories = new Factories();
				Factory factory = factories.getEntry(database, lineJournal.arrivalFactoryCode);
				if (factory != null) return factory;
			}

			return null;
		}

		public string getCreatedByName(Database database)
		{
			if (this.createdByType == 0) return "AUTO";
			if (this.createdByType == 1)
			{
				UserOperators userOperators = new UserOperators();
				UserOperator userOperator = userOperators.getOperator(database, this.createdByCode);
				return userOperator.name;
			}
			if (this.createdByType == 2)
			{
				ShippingCustomerUsers shippingCustomerUsers = new ShippingCustomerUsers();
				ShippingCustomerUser shippingCustomerUser = shippingCustomerUsers.getEntry(database, this.createdByCode);
				return shippingCustomerUser.name;
			}
			return "";

		}

		public DataSet getContainers(Database database)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			return lineOrderContainers.getDataSet(database, this.entryNo);
		}

		public Organization getOrganization(Database database)
		{
			Organizations organizations = new Organizations();
			return organizations.getOrganization(database, this.organizationNo);
		}

		public int countContainers(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order Container] WHERE [Line Order Entry No] = '"+this.entryNo+"'");
	
			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;

		}

		private int countContainers(Database database, string unitCode)
		{
			SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order Container] oc, [Container] c, [Container Type] ct WHERE oc.[Line Order Entry No] = '"+this.entryNo+"' AND c.[No] = oc.[Container No] AND ct.[Code] = c.[Container Type Code] AND [Unit Code] = '"+unitCode+"'");
	
			int count = 0;

			if (dataReader.Read())
			{
				count = int.Parse(dataReader.GetValue(0).ToString());
			}
			dataReader.Close();

			return count;

		}

		public int countContainers(Database database, int calculationType)
		{

			int sum = 0;

			ContainerUnits containerUnits = new ContainerUnits();
			DataSet containerUnitDataSet = containerUnits.getDataSet(database, calculationType);
			int i = 0;
			while (i < containerUnitDataSet.Tables[0].Rows.Count)
			{
				ContainerUnit containerUnit = new ContainerUnit(containerUnitDataSet.Tables[0].Rows[i]);

				if (calculationType == 0) sum = sum + countContainers(database, containerUnit.code);	
				if (calculationType == 1) sum = sum + (calcContainers(database, containerUnit.code) * containerUnit.volumeFactor);	

				i++;
			}

			return sum;

		}


		private int calcContainers(Database database, string unitCode)
		{
		
			ContainerTypes containerTypes = new ContainerTypes();
			DataSet containerTypeDataSet = containerTypes.getDataSet(database, unitCode);

			int volume = 0;

			int i = 0;
			while (i < containerTypeDataSet.Tables[0].Rows.Count)
			{

				SqlDataReader dataReader = database.query("SELECT COUNT(*) FROM [Line Order Container] oc, [Container] c WHERE oc.[Line Order Entry No] = '"+this.entryNo+"' AND c.[No] = oc.[Container No] AND c.[Container Type Code] = '"+containerTypeDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()+"'");
	
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

		public void updateCategoryInformation(Database database)
		{

			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, this.entryNo);
			int i = 0;
			while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
			{
				LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);

				lineOrderContainer.updateCategoryInformation(database);

				i++;
			}


		}

		public void updateWeight(Database database)
		{

			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, this.entryNo);
			int i = 0;
			while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
			{
				LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);

				lineOrderContainer.updateWeight(database);

				i++;
			}


		}

		public void unassignToRoute(Database database)
		{

			LineJournals lineJournals = new LineJournals();
			LineJournal lineJournal = lineJournals.getEntry(database, this.lineJournalEntryNo.ToString());
			
			this.status = 0;
			this.lineJournalEntryNo = 0;
			this.organizationNo = "";

			save(database);


			if (lineJournal != null)
			{
				SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
				synchQueue.enqueue(database, lineJournal.agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER, entryNo.ToString(), 2);

				lineJournal.unassignOrder(database, this.entryNo);

			}
		}

		public void moveToRoute(Database database, int lineJournalEntryNo)
		{
			LineJournals lineJournals = new LineJournals();

			if (this.lineJournalEntryNo > 0)
			{
				LineJournal lineJournal = lineJournals.getEntry(database, this.lineJournalEntryNo.ToString());
			
				if (lineJournal != null)
				{
					SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
					synchQueue.enqueue(database, lineJournal.agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER, entryNo.ToString(), 2);
				}
			}

			this.lineJournalEntryNo = lineJournalEntryNo;		
			save(database);

			LineJournal newLineJournal = lineJournals.getEntry(database, lineJournalEntryNo.ToString());
			newLineJournal.status = 1;
			newLineJournal.save(database);


		}

		public bool assignToRoute(Database database, string agentCode, DateTime shipDate)
		{
			return assignToRoute(database, agentCode, shipDate, false);
		}

		public bool assignToRoute(Database database, string agentCode, DateTime shipDate, bool forcedCreateNew)
		{
			if (this.lineJournalEntryNo > 0) this.unassignToRoute(database);

			Agents agents = new Agents();
			Agent agent = agents.getAgent(database, agentCode);

			LineJournals lineJournals = new LineJournals();
			ArrayList journalList = lineJournals.getAutomaticJournals(database, agentCode, shipDate);
			if (this.routeGroupCode != "")
			{
				journalList = lineJournals.getAutomaticJournals(database, agentCode, shipDate, routeGroupCode);
			}

			LineJournal lineJournal = null;


			if (!forcedCreateNew)
			{
				int i = 0;
				while ((i < journalList.Count) && (lineJournal == null))
				{
					lineJournal = (LineJournal)journalList[i];
					
					if (lineJournal.countOrdersLoaded(database) == 0)
					{
						int journalUnitContainers = lineJournal.countContainers(database, 0);
						int journalCubContainers = lineJournal.countContainers(database, 1);

						if (this.routeGroupCode != "")
						{
							journalUnitContainers = lineJournal.countContainers(database, this.routeGroupCode, 0);
							journalCubContainers = lineJournal.countContainers(database, this.routeGroupCode, 1);
						}

						if ((this.countContainers(database, "ST") + journalUnitContainers) > lineJournal.getNoOfContainers(database)) lineJournal = null;						
						if (lineJournal != null)
						{
							if ((this.countContainers(database, "KUB") + journalCubContainers) > lineJournal.getVolumeStorage(database)) lineJournal = null;
						}

					}
					else
					{
						lineJournal = null;
					}

					i++;
				}
			}

			if (lineJournal == null) lineJournal = lineJournals.createJournal(database, agentCode, shipDate);


			if (lineJournal != null)
			{				
				if (assignToRoute(database, lineJournal.entryNo))
				{
					int journalUnitContainers = lineJournal.countContainers(database, 0);
					int journalNoOfContainers = lineJournal.getNoOfContainers(database);

					if (journalUnitContainers > journalNoOfContainers)
					{
						LineOrders lineOrders = new LineOrders();
						DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo, "");
						int i = 0;
						while (i < lineOrderDataSet.Tables[0].Rows.Count)
						{
							if (journalUnitContainers > journalNoOfContainers)
							{
								LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
								lineOrder.unassignToRoute(database);

								journalUnitContainers = lineJournal.countContainers(database, 0);
							}
							i++;
						}
					
					}

					
					int journalCubContainers = lineJournal.countContainers(database, 1);
					int journalVolumeStorage = lineJournal.getVolumeStorage(database);

					if (journalCubContainers > journalVolumeStorage)
					{
						LineOrders lineOrders = new LineOrders();
						DataSet lineOrderDataSet = lineOrders.getJournalDataSet(database, lineJournal.entryNo, "");
						int i = 0;
						while (i < lineOrderDataSet.Tables[0].Rows.Count)
						{
							if (journalCubContainers > journalVolumeStorage)
							{
								LineOrder lineOrder = new LineOrder(lineOrderDataSet.Tables[0].Rows[i]);
								lineOrder.unassignToRoute(database);

								journalCubContainers = lineJournal.countContainers(database, 1);
							}
							i++;
						}
					
					}


					return true;
				}
			}

			return false;
		}

		public bool assignToRoute(Database database, int lineJournalEntryNo)
		{
			if (this.lineJournalEntryNo > 0) this.unassignToRoute(database);

			LineJournals lineJournals = new LineJournals();
			LineJournal lineJournal = lineJournals.getEntry(database, lineJournalEntryNo.ToString());

			if (lineJournal != null)
			{
				this.lineJournalEntryNo = lineJournal.entryNo;
				this.shipDate = shipDate;
				this.status = 3;
				this.organizationNo = lineJournal.organizationNo;
				save(database, false);

				lineJournal.assignOrder(database, this.entryNo);

				return true;
			}

			return false;
		}


		public void updateOrder(Database database)
		{
			this.updateDetails(database);

			if (this.lineJournalEntryNo > 0)
			{
				LineJournals lineJournals = new LineJournals();
				LineJournal lineJournal = lineJournals.getEntry(database, this.lineJournalEntryNo.ToString());

				if (lineJournal.status >= 4) // Assigned
				{
					SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
					synchQueue.enqueue(database, lineJournal.agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER, entryNo.ToString(), 0);
					
					LineOrderContainers lineOrderContainers = new LineOrderContainers();
					DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, this.entryNo);

					int i = 0;
					while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
					{
						synchQueue.enqueue(database, lineJournal.agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER_CONTAINER, lineOrderContainerDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 0);
						
						i++;
					}
				}

			}

		}

		public void updateOrderDeleteContainer(Database database, int lineOrderContainerEnntryNo)
		{
			if (this.lineJournalEntryNo > 0)
			{
				LineJournals lineJournals = new LineJournals();
				LineJournal lineJournal = lineJournals.getEntry(database, this.lineJournalEntryNo.ToString());

				if (lineJournal.status >= 4) // Assigned
				{
					SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
					synchQueue.enqueue(database, lineJournal.agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER_CONTAINER, lineOrderContainerEnntryNo.ToString(), 2);
					synchQueue.enqueue(database, lineJournal.agentCode, SynchronizationQueueEntries.SYNC_LINE_ORDER, this.entryNo.ToString(), 0);
					
				}

			}

		}

		public void confirmOrder(Database database, DateTime arrivalDateTime, string shippingCustomerNo)
		{
			bool rePlan = false;

			if (shippingCustomerNo != this.shippingCustomerNo)
			{

				ShippingCustomers shippingCustomers = new ShippingCustomers();
				ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, shippingCustomerNo);
				if (shippingCustomer != null)
				{
					this.applyShippingCustomer(shippingCustomer);
					rePlan = true;
				}

			}

			this.type = 2; // Confirmed
			
			this.confirmedToDateTime = arrivalDateTime;
			if (this.shipDate <= this.confirmedToDateTime.Date) 
			{
				this.shipDate = this.confirmedToDateTime.Date;
				this.shipTime = new DateTime(1754, 1, 1, this.confirmedToDateTime.Hour, this.confirmedToDateTime.Minute, this.confirmedToDateTime.Second);
			}
			else
			{
				this.shipTime = new DateTime(1754, 1, 1, 8, 0, 0);
			}

			if ((shipTime.Hour == 0) && (shipTime.Minute == 0)) shipTime = new DateTime(1754, 1, 1, 1, 0, 0);


			this.save(database, false);

			if (rePlan) this.unassignToRoute(database);

		}

		public bool hasContainers(Database database)
		{
			bool found = false;

			SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Line Order Container] WHERE [Line Order Entry No] = '"+this.entryNo+"'");
			if (dataReader.Read())
			{
				found = true;
			}
			dataReader.Close();

			return found;

		}

		public void updateDetails(Database database)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();

			System.Data.DataSet lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, this.entryNo);

			int i = 0;
			this.details = "";

			while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
			{
				LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);

				if (details != "") details = details + ", ";
				details = details + lineOrderContainer.containerNo;
				if (lineOrderContainer.containsPostMortem(database)) 
				{
					details = details + "(O)";
				}
			
				i++;
			}

			save(database, false);
		}


		public void setOrderLoaded(Database database, int creatorType, string creatorNo)
		{
			if (this.lineJournalEntryNo > 0)
			{
				LineJournal lineJournal = this.getJournal(database);

				DataSet lineOrderContainerDataSet = this.getContainers(database);

				int i = 0;
				while (i < lineOrderContainerDataSet.Tables[0].Rows.Count)
				{
					LineOrderContainer lineOrderContainer = new LineOrderContainer(lineOrderContainerDataSet.Tables[0].Rows[i]);

					ContainerEntry containerEntry = new ContainerEntry();
					containerEntry.containerNo = lineOrderContainer.containerNo;
					containerEntry.type = 0;
					containerEntry.sourceType = 0;
					containerEntry.sourceCode = lineJournal.agentCode;
					containerEntry.creatorType = creatorType;
					containerEntry.creatorNo = creatorNo;
					containerEntry.documentType = 1;
					containerEntry.documentNo = this.entryNo.ToString();
					containerEntry.entryDateTime = DateTime.Now;
					containerEntry.locationType = 1;
					containerEntry.locationCode = this.shippingCustomerNo;
					containerEntry.receivedDateTime = DateTime.Now;
					containerEntry.save(database);

					i++;
				}

				this.status = 7;
				this.isLoaded = true;
				this.save(database, true);
			}
		}
	
		public bool containsPostMortem(Database database)
		{
			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			DataSet shipmentDataSet = shipmentHeaders.getLineOrderDataSet(database, this.entryNo.ToString());

			int i = 0;
			while (i < shipmentDataSet.Tables[0].Rows.Count)
			{
				ShipmentHeader shipmentHeader = new ShipmentHeader(shipmentDataSet.Tables[0].Rows[i]);
				if (shipmentHeader.containsPostMortems(database) == true) return true;

				i++;
			}

			return false;
		}

		public string getPreferredAgentStorageGroup(Database database)
		{
			ShippingCustomerOrganizations shippingCustomerOrganizations = new ShippingCustomerOrganizations();
			DataSet shippingCustomerOrganizationDataSet = shippingCustomerOrganizations.getShippingCustomerDataSet(database, this.shippingCustomerNo, ShippingCustomerOrganizations.ORDER_TYPE_LINEORDER);
			int i = 0;
			while (i < shippingCustomerOrganizationDataSet.Tables[0].Rows.Count)
			{
				if (shippingCustomerOrganizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1") 
				{
					Agents agents = new Agents();
					Agent agent = agents.getAgent(database, shippingCustomerOrganizationDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
					if (agent != null) return agent.agentStorageGroup;
				}

				i++;
			}

			return "";
		}

	}
}
