using System;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class FactoryOrder
	{
		public int entryNo;
		public string organizationNo;
		public DateTime shipDate;

		public int factoryType;
		public string factoryNo;
		public string factoryName;
		public string factoryAddress;
		public string factoryAddress2;
		public string factoryPostCode;
		public string factoryCity;
		public string factoryCountryCode;
		public string factoryPhoneNo;

		public string consumerNo;
		public string consumerName;
		public string consumerAddress;
		public string consumerAddress2;
		public string consumerPostCode;
		public string consumerCity;
		public string consumerCountryCode;
		public string consumerPhoneNo;

		public string categoryCode;
		public string categoryDescription;
		public float quantity;
		public float realQuantity;

		public int factoryPositionX;
		public int factoryPositionY;
		public int consumerPositionX;
		public int consumerPositionY;

		public int type;
		public int status;
		public DateTime closedDateTime;

		public DateTime shipTime;
		public DateTime creationDate;
		public DateTime arrivalDateTime;
		public DateTime pickupDateTime;
		public DateTime plannedArrivalDateTime;

		public int createdByType;
		public string createdByCode;

		public string agentCode;
		public string driverName;
		public string dropDriverName;

		public float consumerLevel;
		public int consumerPresentationUnit;

		public int loadDuration;
		public int loadWaitDuration;
		public int dropDuration;
		public int dropWaitDuration;

		public float phValueFactory;
		public float phValueShipping;

		public int planningType;

		public bool transportInvoiceReceived;
		public string comments;

		public int loadReasonValue;
		public string loadReasonText;
		public int dropReasonValue;
		public string dropReasonText;

		public int navisionStatus;

		public int extraDist;
		public int extraTime;


		public FactoryOrder(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.entryNo = dataReader.GetInt32(0);
			this.organizationNo = dataReader.GetValue(1).ToString();
			this.shipDate = dataReader.GetDateTime(2);
		
			this.factoryNo = dataReader.GetValue(3).ToString();
			this.factoryName = dataReader.GetValue(4).ToString();
			this.factoryAddress = dataReader.GetValue(5).ToString();
			this.factoryAddress2 = dataReader.GetValue(6).ToString();
			this.factoryPostCode = dataReader.GetValue(7).ToString();
			this.factoryCity = dataReader.GetValue(8).ToString();
			this.factoryCountryCode = dataReader.GetValue(9).ToString();
			this.factoryPhoneNo = dataReader.GetValue(10).ToString();
			
			this.consumerNo = dataReader.GetValue(11).ToString();
			this.consumerName = dataReader.GetValue(12).ToString();
			this.consumerAddress = dataReader.GetValue(13).ToString();
			this.consumerAddress2 = dataReader.GetValue(14).ToString();
			this.consumerPostCode = dataReader.GetValue(15).ToString();
			this.consumerCity = dataReader.GetValue(16).ToString();
			this.consumerCountryCode = dataReader.GetValue(17).ToString();			
			this.consumerPhoneNo = dataReader.GetValue(18).ToString();

			this.categoryCode = dataReader.GetValue(19).ToString();
			this.categoryDescription = dataReader.GetValue(20).ToString();
			this.quantity = float.Parse(dataReader.GetValue(21).ToString());

			this.factoryPositionX = dataReader.GetInt32(22);
			this.factoryPositionY = dataReader.GetInt32(23);
			this.consumerPositionX = dataReader.GetInt32(24);
			this.consumerPositionY = dataReader.GetInt32(25);

			this.type = dataReader.GetInt32(26);
			this.status = dataReader.GetInt32(27);

			DateTime closedDate = dataReader.GetDateTime(28);
			DateTime closedTime = dataReader.GetDateTime(29);
			this.closedDateTime = new DateTime(closedDate.Year, closedDate.Month, closedDate.Day, closedTime.Hour, closedTime.Minute, closedTime.Second);

			this.shipTime = dataReader.GetDateTime(30);
			this.creationDate = dataReader.GetDateTime(31);

			this.createdByType = dataReader.GetInt32(32);
			this.createdByCode = dataReader.GetValue(33).ToString();


			this.agentCode = dataReader.GetValue(34).ToString();

			this.factoryType = int.Parse(dataReader.GetValue(35).ToString());

			DateTime arrivalDate = dataReader.GetDateTime(36);
			DateTime arrivalTime = dataReader.GetDateTime(37);
			this.arrivalDateTime = new DateTime(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day, arrivalTime.Hour, arrivalTime.Minute, arrivalTime.Second);

			this.driverName = dataReader.GetValue(38).ToString();
			this.realQuantity = float.Parse(dataReader.GetValue(39).ToString());

			this.consumerLevel = float.Parse(dataReader.GetValue(40).ToString());

			this.loadDuration = dataReader.GetInt32(41);
			this.loadWaitDuration = dataReader.GetInt32(42);
			this.dropDuration = dataReader.GetInt32(43);
			this.dropWaitDuration = dataReader.GetInt32(44);

			this.phValueFactory = float.Parse(dataReader.GetValue(45).ToString());
			this.phValueShipping = float.Parse(dataReader.GetValue(46).ToString());

			this.consumerPresentationUnit = int.Parse(dataReader.GetValue(47).ToString());

			DateTime pickupDate = dataReader.GetDateTime(48);
			DateTime pickupTime = dataReader.GetDateTime(49);
			this.pickupDateTime = new DateTime(pickupDate.Year, pickupDate.Month, pickupDate.Day, pickupTime.Hour, pickupTime.Minute, pickupTime.Second);

			DateTime plannedArrivalDate = dataReader.GetDateTime(50);
			DateTime plannedArrivalTime = dataReader.GetDateTime(51);
			this.plannedArrivalDateTime = new DateTime(plannedArrivalDate.Year, plannedArrivalDate.Month, plannedArrivalDate.Day, plannedArrivalTime.Hour, plannedArrivalTime.Minute, plannedArrivalTime.Second);
			
			this.planningType = dataReader.GetInt32(52);

			this.transportInvoiceReceived = false;
			if (dataReader.GetValue(53).ToString() == "1") this.transportInvoiceReceived = true;

			this.comments = dataReader.GetValue(54).ToString();

			this.loadReasonValue = dataReader.GetInt32(55);
			this.loadReasonText = dataReader.GetValue(56).ToString();
			this.dropReasonValue = dataReader.GetInt32(57);
			this.dropReasonText = dataReader.GetValue(58).ToString();

			this.navisionStatus = dataReader.GetInt32(59);

			this.dropDriverName = dataReader.GetValue(60).ToString();

			this.extraDist = dataReader.GetInt32(61);
			this.extraTime = dataReader.GetInt32(62);
		}

		public FactoryOrder(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.organizationNo = dataRow.ItemArray.GetValue(1).ToString();
			this.shipDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
		
			this.factoryNo = dataRow.ItemArray.GetValue(3).ToString();
			this.factoryName = dataRow.ItemArray.GetValue(4).ToString();
			this.factoryAddress = dataRow.ItemArray.GetValue(5).ToString();
			this.factoryAddress2 = dataRow.ItemArray.GetValue(6).ToString();
			this.factoryPostCode = dataRow.ItemArray.GetValue(7).ToString();
			this.factoryCity = dataRow.ItemArray.GetValue(8).ToString();
			this.factoryCountryCode = dataRow.ItemArray.GetValue(9).ToString();
			this.factoryPhoneNo = dataRow.ItemArray.GetValue(10).ToString();
			
			this.consumerNo = dataRow.ItemArray.GetValue(11).ToString();
			this.consumerName = dataRow.ItemArray.GetValue(12).ToString();
			this.consumerAddress = dataRow.ItemArray.GetValue(13).ToString();
			this.consumerAddress2 = dataRow.ItemArray.GetValue(14).ToString();
			this.consumerPostCode = dataRow.ItemArray.GetValue(15).ToString();
			this.consumerCity = dataRow.ItemArray.GetValue(16).ToString();
			this.consumerCountryCode = dataRow.ItemArray.GetValue(17).ToString();			
			this.consumerPhoneNo = dataRow.ItemArray.GetValue(18).ToString();

			this.categoryCode = dataRow.ItemArray.GetValue(19).ToString();
			this.categoryDescription = dataRow.ItemArray.GetValue(20).ToString();
			this.quantity = float.Parse(dataRow.ItemArray.GetValue(21).ToString());

			this.factoryPositionX = int.Parse(dataRow.ItemArray.GetValue(22).ToString());
			this.factoryPositionY = int.Parse(dataRow.ItemArray.GetValue(23).ToString());
			this.consumerPositionX =int.Parse(dataRow.ItemArray.GetValue(24).ToString());
			this.consumerPositionY = int.Parse(dataRow.ItemArray.GetValue(25).ToString());

			this.type = int.Parse(dataRow.ItemArray.GetValue(26).ToString());
			this.status = int.Parse(dataRow.ItemArray.GetValue(27).ToString());

			DateTime closedDate = DateTime.Parse(dataRow.ItemArray.GetValue(28).ToString());
			DateTime closedTime = DateTime.Parse(dataRow.ItemArray.GetValue(29).ToString());
			this.closedDateTime = new DateTime(closedDate.Year, closedDate.Month, closedDate.Day, closedTime.Hour, closedTime.Minute, closedTime.Second);

			this.shipTime = DateTime.Parse(dataRow.ItemArray.GetValue(30).ToString());
			this.creationDate = DateTime.Parse(dataRow.ItemArray.GetValue(31).ToString());

			this.createdByType = int.Parse(dataRow.ItemArray.GetValue(32).ToString());
			this.createdByCode = dataRow.ItemArray.GetValue(33).ToString();


			this.agentCode = dataRow.ItemArray.GetValue(34).ToString();

			this.factoryType = int.Parse(dataRow.ItemArray.GetValue(35).ToString());

			DateTime arrivalDate = DateTime.Parse(dataRow.ItemArray.GetValue(36).ToString());
			DateTime arrivalTime = DateTime.Parse(dataRow.ItemArray.GetValue(37).ToString());
			this.arrivalDateTime = new DateTime(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day, arrivalTime.Hour, arrivalTime.Minute, arrivalTime.Second);

			this.driverName = dataRow.ItemArray.GetValue(38).ToString();
			this.realQuantity = float.Parse(dataRow.ItemArray.GetValue(39).ToString());

			this.consumerLevel = float.Parse(dataRow.ItemArray.GetValue(40).ToString());

			this.loadDuration = int.Parse(dataRow.ItemArray.GetValue(41).ToString());
			this.loadWaitDuration = int.Parse(dataRow.ItemArray.GetValue(42).ToString());
			this.dropDuration = int.Parse(dataRow.ItemArray.GetValue(43).ToString());
			this.dropWaitDuration = int.Parse(dataRow.ItemArray.GetValue(44).ToString());

			this.phValueFactory = float.Parse(dataRow.ItemArray.GetValue(45).ToString());
			this.phValueShipping = float.Parse(dataRow.ItemArray.GetValue(46).ToString());

			this.consumerPresentationUnit = int.Parse(dataRow.ItemArray.GetValue(47).ToString());

			DateTime pickupDate = DateTime.Parse(dataRow.ItemArray.GetValue(48).ToString());
			DateTime pickupTime = DateTime.Parse(dataRow.ItemArray.GetValue(49).ToString());
			this.pickupDateTime = new DateTime(pickupDate.Year, pickupDate.Month, pickupDate.Day, pickupTime.Hour, pickupTime.Minute, pickupTime.Second);

			DateTime plannedArrivalDate = DateTime.Parse(dataRow.ItemArray.GetValue(50).ToString());
			DateTime plannedArrivalTime = DateTime.Parse(dataRow.ItemArray.GetValue(51).ToString());
			this.plannedArrivalDateTime = new DateTime(plannedArrivalDate.Year, plannedArrivalDate.Month, plannedArrivalDate.Day, plannedArrivalTime.Hour, plannedArrivalTime.Minute, plannedArrivalTime.Second);

			this.planningType = int.Parse(dataRow.ItemArray.GetValue(52).ToString());

			this.transportInvoiceReceived = false;
			if (dataRow.ItemArray.GetValue(53).ToString() == "1") this.transportInvoiceReceived = true;

			this.comments = dataRow.ItemArray.GetValue(54).ToString();

			this.loadReasonValue = int.Parse(dataRow.ItemArray.GetValue(55).ToString());
			this.loadReasonText = dataRow.ItemArray.GetValue(56).ToString();
			this.dropReasonValue = int.Parse(dataRow.ItemArray.GetValue(57).ToString());
			this.dropReasonText = dataRow.ItemArray.GetValue(58).ToString();

			this.navisionStatus = int.Parse(dataRow.ItemArray.GetValue(59).ToString());

			this.dropDriverName = dataRow.ItemArray.GetValue(60).ToString();

			this.extraDist = int.Parse(dataRow.ItemArray.GetValue(61).ToString());
			this.extraTime = int.Parse(dataRow.ItemArray.GetValue(62).ToString());

		}


		public FactoryOrder(Factory factory, Consumer consumer)
		{
			this.closedDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.shipDate = DateTime.Now;
			this.shipTime = new DateTime(1754, 01, 01, 0, 0, 0, 0);
			this.factoryNo = "";
			this.consumerNo = "";
			this.creationDate = DateTime.Today;
			this.plannedArrivalDateTime = DateTime.Now;


			applyFactory(factory);
			applyConsumer(consumer);
		}

		public FactoryOrder()
		{
			this.closedDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.shipDate = DateTime.Now;
			this.shipTime = new DateTime(1754, 01, 01, 0, 0, 0, 0);
			this.factoryNo = "";
			this.consumerNo = "";
			this.creationDate = DateTime.Today;
			this.arrivalDateTime = new DateTime(1754, 1, 1, 0, 0, 0);
			this.pickupDateTime = new DateTime(1754, 1, 1, 0, 0, 0);
			this.plannedArrivalDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
		}

		public FactoryOrder(FactoryOrder factoryOrder)
		{
			this.closedDateTime = factoryOrder.closedDateTime;
			this.shipDate = factoryOrder.shipDate;
			this.shipTime = factoryOrder.shipTime;
			this.creationDate = factoryOrder.creationDate;
			this.arrivalDateTime = factoryOrder.arrivalDateTime;
			this.pickupDateTime = factoryOrder.pickupDateTime;
			this.plannedArrivalDateTime = factoryOrder.plannedArrivalDateTime;
			this.agentCode = factoryOrder.agentCode;
			this.categoryCode = factoryOrder.categoryCode;
			this.categoryDescription = factoryOrder.categoryDescription;
			this.factoryNo = factoryOrder.factoryNo;
			this.factoryType = factoryOrder.factoryType;
			this.factoryAddress = factoryOrder.factoryAddress;
			this.factoryAddress2 = factoryOrder.factoryAddress2;
			this.factoryPostCode = factoryOrder.factoryPostCode;
			this.factoryCity = factoryOrder.factoryCity;
			this.factoryCountryCode = factoryOrder.factoryCountryCode;
			this.factoryName = factoryOrder.factoryName;
			this.factoryPositionX = factoryOrder.factoryPositionX;
			this.factoryPositionY = factoryOrder.factoryPositionY;
			this.consumerNo = factoryOrder.consumerNo;
			this.consumerName = factoryOrder.consumerName;
			this.consumerAddress = factoryOrder.consumerAddress;
			this.consumerAddress2 = factoryOrder.consumerAddress2;
			this.consumerPostCode = factoryOrder.consumerPostCode;
			this.consumerCity = factoryOrder.consumerCity;
			this.consumerCountryCode = factoryOrder.consumerCountryCode;
			this.consumerPositionX = factoryOrder.consumerPositionX;
			this.consumerPositionY = factoryOrder.consumerPositionY;
			this.consumerPresentationUnit = factoryOrder.consumerPresentationUnit;
			this.createdByCode = factoryOrder.createdByCode;
			this.createdByType = factoryOrder.createdByType;
			this.organizationNo = factoryOrder.organizationNo;
			this.planningType = factoryOrder.planningType;
			this.quantity = factoryOrder.quantity;
			this.type = factoryOrder.type;
			this.comments = factoryOrder.comments;


		}

		public void save(Database database)
		{
			save(database, true, true);
		}

		public void save(Database database, bool synch)
		{
			save(database, synch, true);
		}

		public void save(Database database, bool synch, bool updateInventory)
		{
			int transportInvoiceReceivedVal = 0;
			if (transportInvoiceReceived) transportInvoiceReceivedVal = 1;

			this.shipTime = new DateTime(1754, 01, 01, shipTime.Hour, shipTime.Minute, shipTime.Second, shipTime.Millisecond);
			this.updateArrivalTime(database, false);

			if (entryNo == 0)
			{
				database.nonQuery("INSERT INTO [Factory Order] ([Organization No], [Ship Date], [Factory No], [Factory Name], [Factory Address], [Factory Address 2], [Factory Post Code], [Factory City], [Factory Country Code], [Factory Phone No], [Consumer No], [Consumer Name], [Consumer Address], [Consumer Address 2], [Consumer Post Code], [Consumer City], [Consumer Country Code], [Consumer Phone No], [Category Code], [Category Description], [Quantity], [Factory Position X], [Factory Position Y], [Consumer Position X], [Consumer Position Y], [Type], [Status], [Closed Date], [Closed Time], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Agent Code], [Factory Type], [Arrival Date], [Arrival Time], [Driver Name], [Real Quantity], [Consumer Level], [Load Duration], [Load Wait Duration], [Drop Duration], [Drop Wait Duration], [PH Value Factory], [PH Value Shipping], [Consumer Presentation Unit], [Pickup Date], [Pickup Time], [Planned Arrival Date], [Planned Arrival Time], [Planning Type], [Transport Invoice Received], [Comments], [Load Reason Value], [Load Reason Text], [Drop Reason Value], [Drop Reason Text], [Navision Status], [Drop Driver Name], [Extra Dist], [Extra Time]) "+
					"VALUES ('"+this.organizationNo+"','"+shipDate.ToString("yyyy-MM-dd")+"','"+this.factoryNo+"','"+this.factoryName+"','"+this.factoryAddress+"','"+this.factoryAddress2+"','"+this.factoryPostCode+"','"+this.factoryCity+"','"+this.factoryCountryCode+"','"+this.factoryPhoneNo+"','"+this.consumerNo+"','"+this.consumerName+"','"+this.consumerAddress+"','"+this.consumerAddress2+"','"+this.consumerPostCode+"','"+this.consumerCity+"','"+this.consumerCountryCode+"','"+this.consumerPhoneNo+"','"+this.categoryCode+"','"+this.categoryDescription+"','"+this.quantity+"','"+this.factoryPositionX+"','"+this.factoryPositionY+"','"+this.consumerPositionX+"','"+this.consumerPositionY+"',"+this.type+","+this.status+",'"+this.closedDateTime.ToString("yyyy-MM-dd")+"', '"+closedDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"','"+creationDate.ToString("yyyy-MM-dd 00:00:00")+"','"+createdByType+"','"+createdByCode+"', '"+this.agentCode+"', '"+this.factoryType+"', '"+this.arrivalDateTime.ToString("yyyy-MM-dd")+"', '"+this.arrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+driverName+"', '"+realQuantity+"', '"+consumerLevel+"', '"+loadDuration+"', '"+loadWaitDuration+"', '"+dropDuration+"', '"+dropWaitDuration+"', '"+phValueFactory.ToString().Replace(",", ".")+"', '"+phValueShipping.ToString().Replace(",", ".")+"', '"+consumerPresentationUnit+"', '"+this.arrivalDateTime.ToString("yyyy-MM-dd")+"', '"+this.arrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+this.plannedArrivalDateTime.ToString("yyyy-MM-dd")+"', '"+this.plannedArrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+this.planningType+"', '"+transportInvoiceReceivedVal+"', '"+this.comments+"', '"+loadReasonValue+"', '"+loadReasonText+"', '"+dropReasonValue+"', '"+dropReasonText+"', '"+navisionStatus+"', '"+dropDriverName+"', '"+extraDist+"', '"+extraTime+"')");
				entryNo = (int)database.getInsertedSeqNo();

			}
			else
			{
				database.nonQuery("UPDATE [Factory Order] SET [Organization No] = '"+this.organizationNo+"', [Ship Date] = '"+this.shipDate.ToString("yyyy-MM-dd")+"', [Factory No] = '"+this.factoryNo+"', [Factory Name] = '"+this.factoryName+"', [Factory Address] = '"+this.factoryAddress+"', [Factory Address 2] = '"+this.factoryAddress2+"', [Factory Post Code] = '"+this.factoryPostCode+"', [Factory City] = '"+this.factoryCity+"', [Factory Country Code] = '"+this.factoryCountryCode+"', [Factory Phone No] = '"+this.factoryPhoneNo+"', [Consumer No] = '"+this.consumerNo+"', [Consumer Name] = '"+this.consumerName+"', [Consumer Address] = '"+this.consumerAddress+"', [Consumer Address 2] = '"+this.consumerAddress2+"', [Consumer Post Code] = '"+this.consumerPostCode+"', [Consumer City] = '"+this.consumerCity+"', [Consumer Country Code] = '"+this.consumerCountryCode+"', [Consumer Phone No] = '"+this.consumerPhoneNo+"', [Category Code] = '"+this.categoryCode+"', [Category Description] = '"+this.categoryDescription+"', [Quantity] = '"+this.quantity.ToString().Replace(",", ".")+"', [Factory Position X] = '"+this.factoryPositionX+"', [Factory Position Y] = '"+this.factoryPositionY+"', [Consumer Position X] = '"+this.consumerPositionX+"', [Consumer Position Y] = '"+this.consumerPositionY+"', [Type] = '"+this.type+"', [Status] = '"+this.status+"', [Closed Date] = '"+this.closedDateTime.ToString("yyyy-MM-dd")+"', [Closed Time] = '"+this.closedDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Ship Time] = '"+this.shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', [Creation Date] = '"+this.creationDate.ToString("yyyy-MM-dd")+"', [Created By Type] = '"+this.createdByType+"', [Created By Code] = '"+this.createdByCode+"', [Agent Code] = '"+this.agentCode+"', [Factory Type] = '"+this.factoryType+"', [Arrival Date] = '"+this.arrivalDateTime.ToString("yyyy-MM-dd")+"', [Arrival Time] = '"+this.arrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Driver Name] = '"+driverName+"', [Real Quantity] = '"+realQuantity.ToString().Replace(",", ".")+
                        "', [Consumer Level] = '"+consumerLevel.ToString().Replace(",", ".")+"', [Load Duration] = '"+loadDuration+"', [Load Wait Duration] = '"+loadWaitDuration+"', [Drop Duration] = '"+dropDuration+"', [Drop Wait Duration] = '"+dropWaitDuration+"', [PH Value Factory] = '"+phValueFactory.ToString().Replace(",", ".")+"', [PH Value Shipping] = '"+phValueShipping.ToString().Replace(",", ".")+"', [Consumer Presentation Unit] = '"+consumerPresentationUnit+"', [Pickup Date] = '"+pickupDateTime.ToString("yyyy-MM-dd")+"', [Pickup Time] = '"+pickupDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Planned Arrival Date] = '"+plannedArrivalDateTime.ToString("yyyy-MM-dd")+"', [Planned Arrival Time] = '"+plannedArrivalDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Planning Type] = '"+this.planningType+"', [Transport Invoice Received] = '"+transportInvoiceReceivedVal+"', [Comments] = '"+this.comments+"', [Load Reason Value] = '"+loadReasonValue+"', [Load Reason Text] = '"+loadReasonText+"', [Drop Reason Value] = '"+dropReasonValue+"', [Drop Reason Text] = '"+dropReasonText+"', [Navision Status] = '"+navisionStatus+"', [Drop Driver Name] = '"+dropDriverName+"', [Extra Dist] = '"+extraDist+"', [Extra Time] = '"+extraTime+"' WHERE [Entry No] = '"+entryNo+"'");
				if (synch) updateOrder(database);
			}

			if (updateInventory)
			{
				ConsumerInventories consumerInventories = new ConsumerInventories();
				consumerInventories.recalculateInventories(database, this.consumerNo, this.arrivalDateTime.AddHours(-1));
			}
		}

		public void delete(Database database)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
			if (this.agentCode != "") synchQueue.enqueue(database, this.agentCode, SynchronizationQueueEntries.SYNC_FACTORY_ORDER, entryNo.ToString(), 2);			


			database.nonQuery("DELETE FROM [Factory Order] WHERE [Entry No] = '"+entryNo+"'");

			ConsumerInventories consumerInventories = new ConsumerInventories();
			consumerInventories.recalculateInventories(database, this.consumerNo, this.arrivalDateTime.AddHours(-1));

		}



		public void applyFactory(Factory factory)
		{
			this.factoryType = 0;
			this.factoryNo = factory.no;
			this.factoryName = factory.name;
			this.factoryAddress = factory.address;
			this.factoryAddress2 = factory.address2;
			this.factoryPostCode = factory.postCode;
			this.factoryCity = factory.city;
			this.factoryCountryCode = factory.countryCode;
			this.factoryPhoneNo = factory.phoneNo;

			this.factoryPositionX = factory.positionX;
			this.factoryPositionY = factory.positionY;


		}

		public void applyFactory(ShippingCustomer shippingCustomer)
		{
			this.factoryType = 1;
			this.factoryNo = shippingCustomer.no;
			this.factoryName = shippingCustomer.name;
			this.factoryAddress = shippingCustomer.address;
			this.factoryAddress2 = shippingCustomer.address2;
			this.factoryPostCode = shippingCustomer.postCode;
			this.factoryCity = shippingCustomer.city;
			this.factoryCountryCode = shippingCustomer.countryCode;
			this.factoryPhoneNo = shippingCustomer.phoneNo;

			this.factoryPositionX = shippingCustomer.positionX;
			this.factoryPositionY = shippingCustomer.positionY;


		}


		public void applyConsumer(Consumer consumer)
		{

			this.consumerNo = consumer.no;
			this.consumerName = consumer.name;
			this.consumerAddress = consumer.address;
			this.consumerAddress2 = consumer.address2;
			this.consumerPostCode = consumer.postCode;
			this.consumerCity = consumer.city;
			this.consumerCountryCode = consumer.countryCode;
			this.consumerPhoneNo = consumer.phoneNo;

			this.consumerPositionX = consumer.positionX;
			this.consumerPositionY = consumer.positionY;

			this.consumerPresentationUnit = consumer.presentationUnit;
		}

		public void updateArrivalTime(Database database, bool save)
		{
			int travelTime = 0;

			ConsumerRelations consumerRelations = new ConsumerRelations();

			int localFactoryType = 0;
			if (this.factoryType == 0) localFactoryType = 1;

			ConsumerRelation consumerRelation = consumerRelations.getEntry(database, this.consumerNo, localFactoryType, this.factoryNo);
			if (consumerRelation != null)
			{
				travelTime = consumerRelation.travelTime;

			}
		
			if (status < 4)
			{
				if (status < 3)
				{
					if (this.planningType == 0)
					{
						this.pickupDateTime = new DateTime(shipDate.Year, shipDate.Month, shipDate.Day, shipTime.Hour, shipTime.Minute, 0);
					}

					if (this.planningType == 1)
					{
						this.arrivalDateTime = new DateTime(plannedArrivalDateTime.Year, plannedArrivalDateTime.Month, plannedArrivalDateTime.Day, plannedArrivalDateTime.Hour, plannedArrivalDateTime.Minute, 0);
						this.pickupDateTime = this.arrivalDateTime.AddMinutes(travelTime*-1);
					}

					if (this.planningType == 2)
					{
						this.pickupDateTime = new DateTime(shipDate.Year, shipDate.Month, shipDate.Day, shipTime.Hour, shipTime.Minute, 0);
						this.arrivalDateTime = new DateTime(plannedArrivalDateTime.Year, plannedArrivalDateTime.Month, plannedArrivalDateTime.Day, plannedArrivalDateTime.Hour, plannedArrivalDateTime.Minute, 0);
					}
				}

				if (this.planningType == 0)
				{
					this.arrivalDateTime = pickupDateTime.AddMinutes(travelTime);
				}

				if (save) this.save(database);

			}
		}

		public void updateArrivalTime(Database database)
		{
			this.updateArrivalTime(database, true);
		}

		public void setArrivalTime(Database database, DateTime newArrivalDateTime)
		{
			DateTime oldArrivalDateTime = this.arrivalDateTime;
			this.arrivalDateTime = newArrivalDateTime;
			this.save(database, false, true);

			ConsumerInventories consumerInventories = new ConsumerInventories();
			consumerInventories.recalculateInventories(database, this.consumerNo, oldArrivalDateTime);
		}

		public string getStatusText()
		{
			if (status == 0) 
			{
				return "";
			}
			if (status == 1) 
			{
				return "Tilldelad";
			}
			if (status == 2) 
			{
				return "Skickad";
			}
			if (status == 3) 
			{
				return "Lastad";
			}
			if (status == 4) 
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
				return "ind_yellow.gif";
			}
			if (status == 2) 
			{
				return "ind_green.gif";
			}
			if (status == 3) 
			{
				return "ind_blue.gif";
			}
			if (status == 4) 
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

		public Organization getOrganization(Database database)
		{
			Organizations organizations = new Organizations();
			return organizations.getOrganization(database, this.organizationNo);
		}

		public string getOrganizationName(Database database)
		{
			Organizations organizations = new Organizations();
			Organization organization = organizations.getOrganization(database, this.organizationNo);
			if (organization != null) return organization.name;
			return "";
		}

		public void updateOrder(Database database)
		{

			if (this.agentCode != "")
			{
				if (status < 3)
				{
					SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();
					synchQueue.enqueue(database, this.agentCode, SynchronizationQueueEntries.SYNC_FACTORY_ORDER, entryNo.ToString(), 0);
				}
			}
		}

		public void assignOrder(Database database, string newAgentCode)
		{
			SynchronizationQueueEntries synchQueue = new SynchronizationQueueEntries();

			bool deleteFirst = false;
			string oldAgentCode = this.agentCode;

			if ((this.agentCode != newAgentCode) && (this.agentCode != "")) deleteFirst = true;

			if (newAgentCode != "") 
				this.status = 1;
			else
				this.status = 0;

			this.agentCode = newAgentCode;

			if (newAgentCode != "")
			{
				Agents agents = new Agents();
				Agent agent = agents.getAgent(database, newAgentCode);
				this.organizationNo = agent.organizationNo;
			}

			this.save(database, true, false);


			if (deleteFirst) synchQueue.enqueue(database, oldAgentCode, SynchronizationQueueEntries.SYNC_FACTORY_ORDER, entryNo.ToString(), 2);

		}


		public string getConsumerUnit()
		{
			if (this.consumerPresentationUnit == 0) return "ton";
			if (this.consumerPresentationUnit == 1) return "kubikmeter";
			return "";
		}

		public string getPickupDateTime()
		{
			if (this.pickupDateTime.Year > 1754) return this.pickupDateTime.ToString("yyyy-MM-dd HH:mm");
			return "";
		}

		public string getArrivalDateTime()
		{
			if (this.arrivalDateTime.Year > 1754) return this.arrivalDateTime.ToString("yyyy-MM-dd HH:mm");
			return "";
		}

		public string getPlanningType()
		{
			if (this.planningType == 0) return "Lås hämtdatum";
			if (this.planningType == 1) return "Lås planerat leveransdatum";
			if (this.planningType == 2) return "Lås hämtdatum + planerat leveransdatum";
			return "";
		}

		public float getQuantity()
		{
			if (realQuantity > 0) return realQuantity;
			return quantity;
		}

		public string getTransportInvoiceStatus()
		{
			if (this.transportInvoiceReceived) return "Ja";
			return "";
		}

		public void setTransportInvoiceStatus(Database database, bool transportInvoiceReceived)
		{
			this.transportInvoiceReceived = transportInvoiceReceived;
			this.save(database);
			
		}


		public XmlElement toDOM(XmlDocument xmlDoc)
		{
			XmlElement factoryOrderElement = xmlDoc.CreateElement("FACTORY_ORDER");

			XmlElement entryNoElement = xmlDoc.CreateElement("ENTRY_NO");
			entryNoElement.AppendChild(xmlDoc.CreateTextNode(this.entryNo.ToString()));
			factoryOrderElement.AppendChild(entryNoElement);

			XmlElement organizationElement = xmlDoc.CreateElement("ORGANIZATION_NO");
			organizationElement.AppendChild(xmlDoc.CreateTextNode(this.organizationNo));
			factoryOrderElement.AppendChild(organizationElement);

			XmlElement factoryTypeElement = xmlDoc.CreateElement("FACTORY_TYPE");
			factoryTypeElement.AppendChild(xmlDoc.CreateTextNode(this.factoryType.ToString()));
			factoryOrderElement.AppendChild(factoryTypeElement);

			XmlElement factoryNoElement = xmlDoc.CreateElement("FACTORY_NO");
			factoryNoElement.AppendChild(xmlDoc.CreateTextNode(this.factoryNo));
			factoryOrderElement.AppendChild(factoryNoElement);

			XmlElement factoryNameElement = xmlDoc.CreateElement("FACTORY_NAME");
			factoryNameElement.AppendChild(xmlDoc.CreateTextNode(this.factoryName));
			factoryOrderElement.AppendChild(factoryNameElement);

			XmlElement consumerNoElement = xmlDoc.CreateElement("CONSUMER_NO");
			consumerNoElement.AppendChild(xmlDoc.CreateTextNode(this.consumerNo));
			factoryOrderElement.AppendChild(consumerNoElement);

			XmlElement consumerNameElement = xmlDoc.CreateElement("CONSUMER_NAME");
			consumerNameElement.AppendChild(xmlDoc.CreateTextNode(this.consumerName));
			factoryOrderElement.AppendChild(consumerNameElement);

			XmlElement categoryCodeElement = xmlDoc.CreateElement("CATEGORY_CODE");
			categoryCodeElement.AppendChild(xmlDoc.CreateTextNode(this.categoryCode));
			factoryOrderElement.AppendChild(categoryCodeElement);

			XmlElement quantityElement = xmlDoc.CreateElement("QUANTITY");
			quantityElement.AppendChild(xmlDoc.CreateTextNode(this.quantity.ToString()));
			factoryOrderElement.AppendChild(quantityElement);

			XmlElement realQuantityElement = xmlDoc.CreateElement("REAL_QUANTITY");
			realQuantityElement.AppendChild(xmlDoc.CreateTextNode(this.realQuantity.ToString()));
			factoryOrderElement.AppendChild(realQuantityElement);

			XmlElement pickupDateElement = xmlDoc.CreateElement("PICKUP_DATE");
			pickupDateElement.AppendChild(xmlDoc.CreateTextNode(this.pickupDateTime.ToString("yyyy-MM-dd")));
			factoryOrderElement.AppendChild(pickupDateElement);

			XmlElement pickupTimeElement = xmlDoc.CreateElement("PICKUP_TIME");
			pickupTimeElement.AppendChild(xmlDoc.CreateTextNode(this.pickupDateTime.ToString("HH:mm:ss")));
			factoryOrderElement.AppendChild(pickupTimeElement);

			XmlElement arrivalDateElement = xmlDoc.CreateElement("ARRIVAL_DATE");
			arrivalDateElement.AppendChild(xmlDoc.CreateTextNode(this.arrivalDateTime.ToString("yyyy-MM-dd")));
			factoryOrderElement.AppendChild(arrivalDateElement);

			XmlElement arrivalTimeElement = xmlDoc.CreateElement("ARRIVAL_TIME");
			arrivalTimeElement.AppendChild(xmlDoc.CreateTextNode(this.arrivalDateTime.ToString("HH:mm:ss")));
			factoryOrderElement.AppendChild(arrivalTimeElement);

			XmlElement loadDurationElement = xmlDoc.CreateElement("LOAD_DURATION");
			loadDurationElement.AppendChild(xmlDoc.CreateTextNode(this.loadDuration.ToString()));
			factoryOrderElement.AppendChild(loadDurationElement);

			XmlElement loadWaitDurationElement = xmlDoc.CreateElement("LOAD_WAIT_DURATION");
			loadWaitDurationElement.AppendChild(xmlDoc.CreateTextNode(this.loadWaitDuration.ToString()));
			factoryOrderElement.AppendChild(loadWaitDurationElement);

			XmlElement dropDurationElement = xmlDoc.CreateElement("DROP_DURATION");
			dropDurationElement.AppendChild(xmlDoc.CreateTextNode(this.dropDuration.ToString()));
			factoryOrderElement.AppendChild(dropDurationElement);

			XmlElement dropWaitDurationElement = xmlDoc.CreateElement("DROP_WAIT_DURATION");
			dropWaitDurationElement.AppendChild(xmlDoc.CreateTextNode(this.dropWaitDuration.ToString()));
			factoryOrderElement.AppendChild(dropWaitDurationElement);

			XmlElement loadReasonValueElement = xmlDoc.CreateElement("LOAD_REASON_VALUE");
			loadReasonValueElement.AppendChild(xmlDoc.CreateTextNode(this.loadReasonValue.ToString()));
			factoryOrderElement.AppendChild(loadReasonValueElement);

			XmlElement loadReasonTextElement = xmlDoc.CreateElement("LOAD_REASON_TEXT");
			loadReasonTextElement.AppendChild(xmlDoc.CreateTextNode(this.loadReasonText));
			factoryOrderElement.AppendChild(loadReasonTextElement);

			XmlElement dropReasonValueElement = xmlDoc.CreateElement("DROP_REASON_VALUE");
			dropReasonValueElement.AppendChild(xmlDoc.CreateTextNode(this.dropReasonValue.ToString()));
			factoryOrderElement.AppendChild(dropReasonValueElement);

			XmlElement dropReasonTextElement = xmlDoc.CreateElement("DROP_REASON_TEXT");
			dropReasonTextElement.AppendChild(xmlDoc.CreateTextNode(this.dropReasonText));
			factoryOrderElement.AppendChild(dropReasonTextElement);

			XmlElement agentCodeElement = xmlDoc.CreateElement("AGENT_CODE");
			agentCodeElement.AppendChild(xmlDoc.CreateTextNode(this.agentCode));
			factoryOrderElement.AppendChild(agentCodeElement);

			return factoryOrderElement;

		}

	}
}
