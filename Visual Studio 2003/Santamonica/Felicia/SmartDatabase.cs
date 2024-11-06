using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;


namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for SmartDatabase.
	/// </summary>
	public class SmartDatabase
	{
		private string dbFileName;

		private SqlCeConnection sqlConnection;
		private SqlCeEngine sqlEngine;
		
		private DataSetup dataSetup;

		public bool debug;

		public SmartDatabase(string dbFileName)
		{
			this.dbFileName = dbFileName;
			sqlEngine = new SqlCeEngine();
			sqlConnection = new SqlCeConnection();

			debug = false;
		}

		public bool init()
		{
			try
			{
				dataSetup = new DataSetup(this);
			}
			catch(Exception e)
			{
				if (e.Message != "") {}

				return false;
			}

			if (dataSetup.databasePath != "") dbFileName = dataSetup.databasePath;

			if (File.Exists(dbFileName))
			{
				sqlConnection.ConnectionString = "Data Source = "+dbFileName;
				try
				{
					sqlConnection.Open();
				}
				catch(Exception e)
				{
					System.Windows.Forms.MessageBox.Show("Database error. "+e.Message);
					return false;
				}

				checkTables();

				return true;

			}
			else
				return false;
		
		}

		public void createDatabase()
		{
			sqlEngine.LocalConnectionString = "Data Source = "+dbFileName;
			sqlEngine.CreateDatabase();

			sqlConnection.ConnectionString = "Data Source = "+dbFileName;
			try
			{
				sqlConnection.Open();

				nonQuery("CREATE TABLE item (no nvarchar(20) PRIMARY KEY, description nvarchar(50), searchDescription nvarchar(30), unitPrice float, addStopItem int, requireId int, invoiceToJbv int, stopItemNo nvarchar(20), connectionItemNo nvarchar(20), unitOfMeasure nvarchar(20), putToDeath int, availableInMobile int, requireCashPayment int, idGroupCode nvarchar(20))");
				nonQuery("CREATE TABLE itemPrice (entryNo int PRIMARY KEY, itemNo nvarchar(20), salesType int, salesCode nvarchar(20), startingDate datetime, minimumQuantity int, endingDate datetime, unitPrice float)");
				nonQuery("CREATE TABLE itemPriceExtended (entryNo int PRIMARY KEY, itemNo nvarchar(20), startingDate datetime, endingDate datetime, customerPriceGroup nvarchar(20), unitOfMeasureCode nvarchar(20), quantityFrom float, quantityTo float, lineAmount float)");
				nonQuery("CREATE TABLE setup (primaryKey integer PRIMARY KEY, host nvarchar(150), agentId nvarchar(20), synchInterval int)");
				nonQuery("INSERT INTO setup (primaryKey, host, agentId, synchInterval) values (1, '', '', 1)");

				nonQuery("CREATE TABLE shipOrder (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), shipDate datetime, customerNo nvarchar(20), customerName nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(20), countryCode nvarchar(20), phoneNo nvarchar(20), cellPhoneNo nvarchar(30), details nvarchar(200), comments nvarchar(200), priority integer, receivedDate datetime, status integer, positionX integer, positionY integer, shipName nvarchar(30), shipAddress nvarchar(30), shipAddress2 nvarchar(30), shipPostCode nvarchar(20), shipCity nvarchar(20), billToCustomerNo nvarchar(20), directionComment nvarchar(250), directionComment2 nvarchar(250), paymentType int, agentCode nvarchar(20), shipTime datetime, creationDate datetime, customerShipAddressNo nvarchar(20), productionSite nvarchar(20))");
			
				if (dataSetup.shipmentIdentitySeed > 0)
				{
					System.Windows.Forms.MessageBox.Show("Sista följesedel: "+dataSetup.agentId+"-"+dataSetup.shipmentIdentitySeed);
					nonQuery("CREATE TABLE shipmentHeader (entryNo integer PRIMARY KEY IDENTITY("+dataSetup.shipmentIdentitySeed+",1), organizationNo nvarchar(20), shipDate datetime, customerNo nvarchar(20), customerName nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(20), countryCode nvarchar(20), phoneNo nvarchar(20), cellPhoneNo nvarchar(20), productionSite nvarchar(30), status int, positionX int, positionY int, payment int, dairyCode nvarchar(20), dairyNo nvarchar(30), reference nvarchar(100), mobileUserName nvarchar(30), containerNo nvarchar(30), shipOrderEntryNo integer, customerShipAddressNo nvarchar(20), shipName nvarchar(30), shipAddress nvarchar(30), shipAddress2 nvarchar(30), shipPostCode nvarchar(20), shipCity nvarchar(30), invoiceNo nvarchar(20))");
				}
				else
				{
					nonQuery("CREATE TABLE shipmentHeader (entryNo integer PRIMARY KEY IDENTITY(1,1), organizationNo nvarchar(20), shipDate datetime, customerNo nvarchar(20), customerName nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(20), countryCode nvarchar(20), phoneNo nvarchar(20), cellPhoneNo nvarchar(20), productionSite nvarchar(30), status int, positionX int, positionY int, payment int, dairyCode nvarchar(20), dairyNo nvarchar(30), reference nvarchar(100), mobileUserName nvarchar(30), containerNo nvarchar(30), shipOrderEntryNo integer, customerShipAddressNo nvarchar(20), shipName nvarchar(30), shipAddress nvarchar(30), shipAddress2 nvarchar(30), shipPostCode nvarchar(20), shipCity nvarchar(30), invoiceNo nvarchar(20))");
				}
				nonQuery("CREATE TABLE shipmentLine (entryNo integer PRIMARY KEY IDENTITY(1,1), shipmentEntryNo integer, itemNo nvarchar(20), description nvarchar(30), quantity integer, connectionQuantity integer, unitPrice float, amount float, connectionUnitPrice float, connectionAmount float, totalAmount float, connectionItemNo nvarchar(20), extraPayment int, testQuantity int)");
				nonQuery("CREATE TABLE shipmentLineId (entryNo integer PRIMARY KEY IDENTITY(1,1), shipmentEntryNo integer, lineEntryNo integer, unitId nvarchar(20), type int, reMarkUnitId nvarchar(20), bseTesting int, postMortem int)");

				nonQuery("CREATE TABLE orderHeader (entryNo integer PRIMARY KEY IDENTITY(1,1), organizationNo nvarchar(20), shipDate datetime, customerNo nvarchar(20), customerName nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(20), countryCode nvarchar(20), phoneNo nvarchar(20), cellPhoneNo nvarchar(20), productionSite nvarchar(30), status int, positionX int, positionY int, payment int, dairyCode nvarchar(20), dairyNo nvarchar(30), reference nvarchar(100), mobileUserName nvarchar(30), containerNo nvarchar(30), shipOrderEntryNo integer, comments nvarchar(250), agentCode nvarchar(20), customerShipAddressNo nvarchar(20), shipName nvarchar(30), shipAddress nvarchar(30), shipAddress2 nvarchar(30), shipPostCode nvarchar(20), shipCity nvarchar(30))");
				nonQuery("CREATE TABLE orderLine (entryNo integer PRIMARY KEY IDENTITY(1,1), orderEntryNo integer, itemNo nvarchar(20), description nvarchar(30), quantity integer, connectionQuantity integer, unitPrice float, amount float, connectionUnitPrice float, connectionAmount float, totalAmount float, connectionItemNo nvarchar(20))");
				nonQuery("CREATE TABLE orderLineId (entryNo integer PRIMARY KEY IDENTITY(1,1), orderEntryNo integer, lineEntryNo integer, unitId nvarchar(20), bseTesting int, postMortem int)");

				nonQuery("CREATE TABLE syncAction (entryNo integer PRIMARY KEY IDENTITY(1,1), type integer, action integer, primaryKey nvarchar(20))");
				nonQuery("CREATE TABLE map (code nvarchar(20) PRIMARY KEY, description nvarchar(50), positionTop integer, positionLeft integer, positionBottom integer, positionRight integer)");
				nonQuery("CREATE TABLE customer (no nvarchar(20) PRIMARY KEY, organizationNo nvarchar(20), name nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(20), countryCode nvarchar(20), contactName nvarchar(30), phoneNo nvarchar(20), cellPhoneNo nvarchar(30), faxNo nvarchar(20), eMail nvarchar(100), productionSite nvarchar(30), registrationNo nvarchar(30), personNo nvarchar(20), dairyNo nvarchar(20), lastUpdated datetime, positionX integer, positionY integer, priceGroupCode nvarchar(20), billToCustomerNo nvarchar(20), blocked int, hide int, dairyCode nvarchar(20), forceCashPayment int, modifyable int, directionComment nvarchar(250), directionComment2 nvarchar(250))");

				nonQuery("CREATE TABLE mobileUser (entryNo int PRIMARY KEY, organizationNo nvarchar(20), name nvarchar(30), password nvarchar(20))");
				nonQuery("CREATE TABLE message (entryNo int PRIMARY KEY, organizationNo nvarchar(20), fromName nvarchar(30), message nvarchar(250))");

				nonQuery("CREATE TABLE shipOrderLine (entryNo int PRIMARY KEY, shipOrderEntryNo int, itemNo nvarchar(20), quantity int, connectionQuantity int, unitPrice float, amount float, connectionUnitPrice float, connectionAmount float, totalAmount float, connectionItemNo nvarchar(20), testQuantity int)");
				nonQuery("CREATE TABLE shipOrderLineId (entryNo int PRIMARY KEY, shipOrderEntryNo int, shipOrderLineEntryNo int, unitId nvarchar(100), bseTesting int, postMortem int)");

				nonQuery("CREATE TABLE organization (no nvarchar(20) PRIMARY KEY, name nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(30), countryCode nvarchar(20), contactName nvarchar(30), phoneNo nvarchar(20), faxNo nvarchar(20), eMail nvarchar(50), stopFee float)");
				nonQuery("CREATE TABLE organizationLocation (shippingCustomerNo nvarchar(20) PRIMARY KEY, name nvarchar(30))");

				nonQuery("CREATE TABLE agent (code nvarchar(20) PRIMARY KEY, description nvarchar(30), organizationNo nvarchar(20))");

				nonQuery("CREATE TABLE container (no nvarchar(20) PRIMARY KEY, containerTypeCode nvarchar(20), currentLocationType integer, currentLocationCode nvarchar(20))");
				nonQuery("CREATE TABLE containerEntry (entryNo integer PRIMARY KEY IDENTITY(1,1), containerNo nvarchar(20), type integer, entryDateTime datetime, positionX integer, positionY integer, estimatedArrivalTime datetime, locationType integer, locationCode nvarchar(20), documentType integer, documentNo nvarchar(20))");
				nonQuery("CREATE TABLE status (primaryKey nvarchar(20) PRIMARY KEY, containerNo nvarchar(20), arrivalTime datetime, tripMeter integer)");

				nonQuery("CREATE TABLE lineJournal (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), shipDate datetime, agentCode nvarchar(20), status integer, departureFactoryCode nvarchar(20), arrivalFactoryCode nvarchar(20), calculatedDistance float, measuredDistance float, reportedDistance float, reportedDistanceSingle float, reportedDistanceTrailer float, dropWaitTime integer)");

				nonQuery("CREATE TABLE lineOrder (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), lineJournalEntryNo integer, shipDate datetime, shippingCustomerNo nvarchar(20), shippingCustomerName nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(30), countryCode nvarchar(20), phoneNo nvarchar(20), cellPhoneNo nvarchar(20), details nvarchar(200), comments nvarchar(200), type integer, status integer, shipTime datetime, directionComment1 nvarchar(250), directionComment2 nvarchar(250), optimizingSortOrder integer, positionX integer, positionY integer, loadWaitTime integer)");
				nonQuery("CREATE TABLE lineOrderContainer (entryNo integer PRIMARY KEY, lineOrderEntryNo integer, containerNo nvarchar(20), categoryCode nvarchar(20), weight float)");

				nonQuery("CREATE TABLE containerLoad (containerNo nvarchar(20) PRIMARY KEY)");

				nonQuery("CREATE TABLE category (code nvarchar(20) PRIMARY KEY, description nvarchar(30))");

				nonQuery("CREATE TABLE customerShipAddress (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), customerNo nvarchar(20), name nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(30), countryCode nvarchar(20), contactName nvarchar(30), positionX integer, positionY integer, directionComment nvarchar(250), directionComment2 nvarchar(250), phoneNo nvarchar(20), productionSite nvarchar(30))");

				nonQuery("CREATE TABLE factoryOrder (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), shipDate datetime, factoryType integer, factoryNo nvarchar(20), factoryName nvarchar(30), factoryAddress nvarchar(30), factoryAddress2 nvarchar(30), factoryPostCode nvarchar(20), factoryCity nvarchar(30), factoryCountryCode nvarchar(20), factoryPhoneNo nvarchar(20), consumerNo nvarchar(20), consumerName nvarchar(30), consumerAddress nvarchar(30), consumerAddress2 nvarchar(30), consumerPostCode nvarchar(20), consumerCity nvarchar(30), consumerCountryCode nvarchar(20), consumerPhoneNo nvarchar(20), categoryCode nvarchar(20), categoryDescription nvarchar(30), quantity float, realQuantity float, factoryPositionX integer, factoryPositionY integer, consumerPositionX integer, consumerPositionY integer, type integer, status integer, closedDateTime datetime, shipTime datetime, creationDate datetime, arrivalDateTime datetime, agentCode nvarchar(20), consumerLevel float, loadDuration integer, loadWaitDuration integer, dropDuration integer, dropWaitDuration integer, phValueShipping float, loadReasonValue integer, loadReasonText nvarchar(50), dropReasonValue integer, dropReasonText nvarchar(50), extraDist integer, extraTime integer)");

				nonQuery("CREATE TABLE shipmentInvoice (entryNo integer PRIMARY KEY, updatedDate datetime)");
				if (dataSetup.invoiceIdentitySeed > 0) 
				{
					System.Windows.Forms.MessageBox.Show("Sista kontantnota: "+DateTime.Today.Year.ToString().Substring(2, 2)+"-"+dataSetup.agentId+(dataSetup.invoiceIdentitySeed.ToString().PadLeft(4, '0')));
					nonQuery("INSERT INTO shipmentInvoice (entryNo, updatedDate) VALUES('"+dataSetup.invoiceIdentitySeed+"', '"+DateTime.Today.ToString("yyyy-MM-dd")+"')");
				}

				nonQuery("CREATE TABLE consumerStatus (consumerNo nvarchar(20) PRIMARY KEY, inventoryLevel float)");

			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show("Database creation error. "+e.Message);
			}

		}

		private void checkTables()
		{
			/*
			 		
			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT shipOrderEntryNo FROM shipmentHeader");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipmentHeader ADD shipOrderEntryNo integer");
			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT putToDeath FROM item");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE item ADD putToDeath int");
				nonQuery("UPDATE item SET putToDeath = 0");
			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT availableInMobile FROM item");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE item ADD availableInMobile int");
				nonQuery("UPDATE item SET availableInMobile = 1");
			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT shipTime FROM shipOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipOrder ADD shipTime DATETIME");
				nonQuery("UPDATE shipOrder SET shipTime = '2001-01-01 00:00:00'");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT creationDate FROM shipOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipOrder ADD creationDate DATETIME");
				nonQuery("UPDATE shipOrder SET shipTime = '2001-01-01 00:00:00'");

				if (e.Message != "") {}

			}

			

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT entryNo FROM orderHeader");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE orderHeader (entryNo integer PRIMARY KEY IDENTITY(1,1), organizationNo nvarchar(20), shipDate datetime, customerNo nvarchar(20), customerName nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(20), countryCode nvarchar(20), phoneNo nvarchar(20), cellPhoneNo nvarchar(20), productionSite nvarchar(30), status int, positionX int, positionY int, payment int, dairyCode nvarchar(20), dairyNo nvarchar(30), reference nvarchar(100), mobileUserName nvarchar(30), containerNo nvarchar(30), shipOrderEntryNo integer, comments nvarchar(250), agentCode nvarchar(20))");
				nonQuery("CREATE TABLE orderLine (entryNo integer PRIMARY KEY IDENTITY(1,1), orderEntryNo integer, itemNo nvarchar(20), description nvarchar(30), quantity integer, connectionQuantity integer, unitPrice float, amount float, connectionUnitPrice float, connectionAmount float, totalAmount float, connectionItemNo nvarchar(20))");
				nonQuery("CREATE TABLE orderLineId (entryNo integer PRIMARY KEY IDENTITY(1,1), orderEntryNo integer, lineEntryNo integer, unitId nvarchar(20))");
			
				if (e.Message != "") {}
			
			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT code FROM agent");
				dataReader.Close();		
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE agent (code nvarchar(20) PRIMARY KEY, description nvarchar(30), organizationNo nvarchar(20))");

				if (e.Message != "") {}

			}
			
			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT agentCode FROM shipOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipOrder ADD agentCode nvarchar(20)");

				if (e.Message != "") {}

			}
			

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT entryNo FROM shipOrderLine");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE shipOrderLine (entryNo int PRIMARY KEY, shipOrderEntryNo int, itemNo nvarchar(20), quantity int, connectionQuantity int, unitPrice float, amount float, connectionUnitPrice float, connectionAmount float, totalAmount float, connectionItemNo nvarchar(20), testQuantity int)");
				nonQuery("CREATE TABLE shipOrderLineId (entryNo int PRIMARY KEY, shipOrderEntryNo int, shipOrderLineEntryNo int, unitId nvarchar(100), bseTesting int, postMortem int)");
			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT modifyable FROM customer");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE customer ADD modifyable int");
				nonQuery("UPDATE customer SET modifyable = 0");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT extraPayment FROM shipmentLine");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipmentLine ADD extraPayment int, testQuantity int");
				nonQuery("UPDATE shipmentLine SET extraPayment = 0, testQuantity = 0");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT type FROM shipmentLineId");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipmentLineId ADD type int");
				nonQuery("UPDATE shipmentLineId SET type = 0");

				if (e.Message != "") {}

			}

			
			
			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT requireCashPayment FROM item");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE item ADD requireCashPayment int");
				nonQuery("UPDATE item SET requireCashPayment = 0");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT testQuantity FROM shipOrderLine");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipOrderLine ADD testQuantity int");
				nonQuery("UPDATE shipOrderLine SET testQuantity = 0");

				if (e.Message != "") {}

			}

			
			
			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT no FROM container");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE container (no nvarchar(20) PRIMARY KEY)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT entryNo FROM containerEntry");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE containerEntry (entryNo integer PRIMARY KEY IDENTITY(1,1), containerNo nvarchar(20), type integer, entryDateTime datetime, positionX integer, positionY integer)");
				nonQuery("CREATE TABLE status (primaryKey nvarchar(20) PRIMARY KEY, containerNo nvarchar(20), arrivalTime datetime)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT estimatedArrivalTime FROM containerEntry");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE containerEntry ADD estimatedArrivalTime DATETIME");
				nonQuery("UPDATE containerEntry SET estimatedArrivalTime = '2001-01-01 00:00:00'");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT name FROM organizationLocation");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE organizationLocation (shippingCustomerNo nvarchar(20) PRIMARY KEY, name nvarchar(30))");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT shippingCustomerNo FROM containerEntry");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE containerEntry ADD shippingCustomerNo nvarchar(20)");
				nonQuery("UPDATE containerEntry SET shippingCustomerNo = ''");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT entryNo FROM lineJournal");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE lineJournal (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), shipDate datetime, agentCode nvarchar(20), status integer, departureFactoryCode nvarchar(20), arrivalFactoryCode nvarchar(20))");
				nonQuery("CREATE TABLE lineOrder (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), lineJournalEntryNo integer, shipDate datetime, shippingCustomerNo nvarchar(20), shippingCustomerName nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(30), countryCode nvarchar(20), phoneNo nvarchar(20), cellPhoneNo nvarchar(20), details nvarchar(200), comments nvarchar(200), type integer, status integer, shipTime datetime, directionComment1 nvarchar(250), directionComment2 nvarchar(250), optimizingSortOrder integer, positionX integer, positionY integer)");
				nonQuery("CREATE TABLE lineOrderContainer (entryNo integer PRIMARY KEY, lineOrderEntryNo integer, containerNo nvarchar(20), categoryCode nvarchar(20))");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT entryNo FROM lineOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE lineOrder (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), lineJournalEntryNo integer, shipDate datetime, shippingCustomerNo nvarchar(20), shippingCustomerName nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(30), countryCode nvarchar(20), phoneNo nvarchar(20), cellPhoneNo nvarchar(20), details nvarchar(200), comments nvarchar(200), type integer, status integer, shipTime datetime, directionComment1 nvarchar(250), directionComment2 nvarchar(250), optimizingSortOrder integer, positionX integer, positionY integer)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT weight FROM lineOrderContainer");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE lineOrderContainer ADD weight float");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT containerTypeCode FROM container");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE container ADD containerTypeCode nvarchar(20), currentLocationType int, currentLocationCode nvarchar(20)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT containerNo FROM containerLoad");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE containerLoad (containerNo nvarchar(20) PRIMARY KEY)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT locationType FROM containerEntry");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("DROP TABLE containerEntry");
				nonQuery("CREATE TABLE containerEntry (entryNo integer PRIMARY KEY IDENTITY(1,1), containerNo nvarchar(20), type integer, entryDateTime datetime, positionX integer, positionY integer, estimatedArrivalTime datetime, locationType integer, locationCode nvarchar(20))");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT measuredDistance FROM lineJournal");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE lineJournal ADD measuredDistance float, reportedDistance float");
				nonQuery("UPDATE lineJournal SET measuredDistance = 0, reportedDistance = 0");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT code FROM category");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE category (code nvarchar(20) PRIMARY KEY, description nvarchar(30))");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT documentType FROM containerEntry");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE containerEntry ADD documentType integer, documentNo nvarchar(20)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT tripMeter FROM status");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE status ADD tripMeter integer");
				nonQuery("UPDATE status SET tripMeter = 0");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT reMarkUnitId FROM shipmentLineId");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipmentLineId ADD reMarkUnitId nvarchar(20), bseTesting int, postMortem int");
				nonQuery("UPDATE shipmentLineId SET reMarkUnitId = '', bseTesting = 0, postMortem = 0");

				nonQuery("ALTER TABLE orderLineId ADD bseTesting int, postMortem int");
				nonQuery("UPDATE orderLineId SET bseTesting = 0, postMortem = 0");

				nonQuery("ALTER TABLE shipOrderLineId ADD bseTesting int, postMortem int");
				nonQuery("UPDATE shipOrderLineId SET bseTesting = 0, postMortem = 0");

				if (e.Message != "") {}

			}


			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT bseTesting FROM shipOrderLineId");
				dataReader.Close();
			}
			catch(Exception e)
			{

				nonQuery("ALTER TABLE shipOrderLineId ADD bseTesting int, postMortem int");
				nonQuery("UPDATE shipOrderLineId SET bseTesting = 0, postMortem = 0");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT reportedDistanceSingle FROM lineJournal");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE lineJournal ADD reportedDistanceSingle float, reportedDistanceTrailer float");
				nonQuery("UPDATE lineJournal SET reportedDistanceSingle=0, reportedDistanceTrailer=0");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT entryNo FROM customerShipAddress");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE customerShipAddress (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), customerNo nvarchar(20), name nvarchar(30), address nvarchar(30), address2 nvarchar(30), postCode nvarchar(20), city nvarchar(30), countryCode nvarchar(20), contactName nvarchar(30), positionX integer, positionY integer, directionComment nvarchar(250), directionComment2 nvarchar(250), phoneNo nvarchar(20), productionSite nvarchar(30))");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT directionComment FROM customer");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE customer ADD directionComment nvarchar(250), directionComment2 nvarchar(250)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT shipName FROM shipmentHeader");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipmentHeader ADD customerShipAddressNo nvarchar(20), shipName nvarchar(30), shipAddress nvarchar(30), shipAddress2 nvarchar(30), shipPostCode nvarchar(20), shipCity nvarchar(30)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT shipName FROM orderHeader");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE orderHeader ADD customerShipAddressNo nvarchar(20), shipName nvarchar(30), shipAddress nvarchar(30), shipAddress2 nvarchar(30), shipPostCode nvarchar(20), shipCity nvarchar(30)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT customerShipAddressNo FROM shipOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipOrder ADD customerShipAddressNo nvarchar(20)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT entryNo FROM factoryOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE factoryOrder (entryNo integer PRIMARY KEY, organizationNo nvarchar(20), shipDate datetime, factoryType integer, factoryNo nvarchar(20), factoryName nvarchar(30), factoryAddress nvarchar(30), factoryAddress2 nvarchar(30), factoryPostCode nvarchar(20), factoryCity nvarchar(30), factoryCountryCode nvarchar(20), factoryPhoneNo nvarchar(20), consumerNo nvarchar(20), consumerName nvarchar(30), consumerAddress nvarchar(30), consumerAddress2 nvarchar(30), consumerPostCode nvarchar(20), consumerCity nvarchar(30), consumerCountryCode nvarchar(20), consumerPhoneNo nvarchar(20), categoryCode nvarchar(20), categoryDescription nvarchar(30), quantity float, realQuantity float, factoryPositionX integer, factoryPositionY integer, consumerPositionX integer, consumerPositionY integer, type integer, status integer, closedDateTime datetime, shipTime datetime, creationDate datetime, arrivalDateTime datetime, agentCode nvarchar(20), consumerLevel float, loadDuration integer, loadWaitDuration integer, dropDuration integer, dropWaitDuration integer, phValueShipping float)");

				if (e.Message != "") {}

			}

			nonQuery("DELETE FROM customerShipAddress");


			if (dataSetup.agentId == "UKM")
			{
				try
				{
					nonQuery("DELETE FROM shipmentHeader WHERE entryNo = 9407");
				}
				catch(Exception e)
				{

					if (e.Message != "") {}

				}
			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT invoiceNo FROM shipmentHeader");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipmentHeader ADD invoiceNo nvarchar(20)");
				nonQuery("UPDATE shipmentHeader SET invoiceNo = ''");
				
				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT entryNo FROM shipmentInvoice");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE shipmentInvoice (entryNo integer PRIMARY KEY, updatedDate datetime)");

				if (e.Message != "") {}

			}

			*/

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT calculatedDistance FROM lineJournal");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE lineJournal ADD calculatedDistance float");
				nonQuery("UPDATE lineJournal SET calculatedDistance = 0");

				if (e.Message != "") {}

			}


			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT consumerNo FROM consumerStatus");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("CREATE TABLE consumerStatus (consumerNo nvarchar(20) PRIMARY KEY, inventoryLevel float)");

				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT loadReasonValue FROM factoryOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE factoryOrder ADD loadReasonValue integer, loadReasonText nvarchar(50), dropReasonValue integer, dropReasonText nvarchar(50)");
				nonQuery("UPDATE factoryOrder SET loadReasonValue = 0, loadReasonText = '', dropReasonValue = 0, dropReasonText = ''");
				
				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT loadWaitTime FROM lineOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE lineOrder ADD loadWaitTime integer");
				nonQuery("UPDATE lineOrder SET loadWaitTime = 0");
				
				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT dropWaitTime FROM lineJournal");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE lineJournal ADD dropWaitTime integer");
				nonQuery("UPDATE lineJournal SET dropWaitTime = 0");
				
				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT productionSite FROM shipOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE shipOrder ADD productionSite nvarchar(20)");
				nonQuery("UPDATE shipOrder SET productionSite = ''");
				
				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT extraDist FROM factoryOrder");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE factoryOrder ADD extraDist integer, extraTime integer");
				nonQuery("UPDATE factoryOrder SET extraDist = 0, extraTime = 0");
				
				if (e.Message != "") {}

			}

			try
			{
				SqlCeDataReader dataReader = this.nonErrorQuery("SELECT idGroupCode FROM item");
				dataReader.Close();
			}
			catch(Exception e)
			{
				nonQuery("ALTER TABLE item ADD idGroupCode nvarchar(20)");
				nonQuery("UPDATE item SET idGroupCode = ''");
				
				if (e.Message != "") {}

			}

		}

		
		public SqlCeDataReader query(string queryString)
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = queryString;
			try
			{
				return cmd.ExecuteReader();
			}
			catch (SqlCeException e) 
			{
				ShowErrors(e);
			}
			
			return null;
		}

		private SqlCeDataReader nonErrorQuery(string queryString)
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = queryString;
			return cmd.ExecuteReader();
			
		}

		public SqlCeDataAdapter dataAdapterQuery(string queryString)
		{
			return new SqlCeDataAdapter(queryString, sqlConnection);
		}

		public void nonQuery(string queryString)
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = queryString;
			
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlCeException e) 
			{
				ShowErrors(e);
			}
		}

		public int getInsertedId()
		{
			SqlCeCommand cmd = sqlConnection.CreateCommand();

			cmd.CommandText = "SELECT @@IDENTITY as ID";

			SqlCeDataReader dataReader = cmd.ExecuteReader();
			if (dataReader.Read())
			{
				try
				{
					return int.Parse(dataReader.GetValue(0).ToString());
				}
				catch (SqlCeException e) 
				{
					ShowErrors(e);
				}
			}
			return 0;
		}

		public void ShowErrors(SqlCeException e) 
		{
			SqlCeErrorCollection errorCollection = e.Errors;

			System.Text.StringBuilder bld = new System.Text.StringBuilder();
			Exception   inner = e.InnerException;

			foreach (SqlCeError err in errorCollection) 
			{
				bld.Append("\n Error Code: " + err.HResult.ToString("X"));
				bld.Append("\n Message   : " + err.Message);
				bld.Append("\n Minor Err.: " + err.NativeError);
				bld.Append("\n Source    : " + err.Source);
                
				foreach (int numPar in err.NumericErrorParameters) 
				{
					if (0 != numPar) bld.Append("\n Num. Par. : " + numPar);
				}
                
				foreach (string errPar in err.ErrorParameters) 
				{
					if (String.Empty != errPar) bld.Append("\n Err. Par. : " + errPar);
				}

				System.Windows.Forms.MessageBox.Show(bld.ToString());
				bld.Remove(0, bld.Length);
			}
		}

		public DataSetup getSetup()
		{
			return dataSetup;
		}

	}
}
