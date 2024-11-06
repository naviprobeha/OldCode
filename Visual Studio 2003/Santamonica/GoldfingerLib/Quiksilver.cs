using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.Goldfinger
{
	/// <summary>
	/// Summary description for Quiksliver.
	/// </summary>
	public class Quiksilver : Logger
	{
		private Configuration configuration;
		private Database database;

		public Quiksilver()
		{
		}

		public bool init()
		{
			configuration = new Configuration();

			if (!configuration.initWeb())
			{
				return false;
			}

			database = new Database(this, configuration);
			 
			return true;
		}

		public DataSet getAvailableShipments()
		{
			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			resendDocuments();
			DataSet shipmentHeaderDataSet = shipmentHeaders.getAvailableDataSet(database);


			return shipmentHeaderDataSet;
		}

		public DataSet getShipmentLines(string shipmentNo)
		{
			ShipmentLines shipmentLines = new ShipmentLines(database);
			DataSet shipmentLineDataSet = shipmentLines.getShipmentLinesDataSet(shipmentNo);

			return shipmentLineDataSet;
		}

		public DataSet getShipmentLineIds(string shipmentNo, int shipmentLineNo)
		{
			ShipmentLineIds shipmentLineIds = new ShipmentLineIds(database);
			DataSet shipmentLineIdDataSet = shipmentLineIds.getShipmentLineIdDataSet(shipmentNo, shipmentLineNo);

			return shipmentLineIdDataSet;
		}

		public void setShipmentStatus(string shipmentNo, int status)
		{
			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			shipmentHeaders.setStatus(database, shipmentNo, status);			

		}

		public DataSet getUpdatedCustomers()
		{
			Customers customers = new Customers();
			DataSet customerDataSet = customers.getUpdatedDataSet(database);

			return customerDataSet;
		}

		public void setCustomerUpdated(string organizationNo, string customerNo, bool updated)
		{
			Customers customers = new Customers();
			Customer customer = customers.getEntry(database, organizationNo, customerNo);			
			if (customer != null)
			{
				customer.updated = updated;
				customer.save(database);
			}

		}

		public DataSet getScaleEntries()
		{
			ScaleEntries scaleEntries = new ScaleEntries();
			DataSet scaleEntryDataSet = scaleEntries.getNotSentDataSet(database);

			return scaleEntryDataSet;
		}

		public void setScaleEntryStatus(string factoryCode, int entryNo, int status)
		{
			ScaleEntries scaleEntries = new ScaleEntries();
			ScaleEntry scaleEntry = scaleEntries.getEntry(database, factoryCode, entryNo);
			
			if (scaleEntry != null)
			{
				scaleEntry.navisionStatus = status;
				scaleEntry.save(database);
			}

		}

		public DataSet getFactoryOrderEntries()
		{
			FactoryOrders factoryOrders = new FactoryOrders();
			DataSet factoryOrderDataSet = factoryOrders.getNotSentDataSet(database);

			return factoryOrderDataSet;
		}

		public void setFactoryOrderEntryStatus(int entryNo, int status)
		{
			FactoryOrders factoryOrders = new FactoryOrders();
			FactoryOrder factoryOrder = factoryOrders.getEntry(database, entryNo.ToString());
			
			if (factoryOrder != null)
			{
				//ServerLogging serverLogging = new ServerLogging(database);
				//serverLogging.log("QUIK", "FactoryOrderStatus: "+factoryOrder.status+", "+factoryOrder.navisionStatus);
				factoryOrder.navisionStatus = status;
				factoryOrder.save(database);
			}

		}

		public DataSet getOrganizations(string syncGroupCode)
		{
			Organizations organizations = new Organizations();
			DataSet organizationDataSet = organizations.getDataSet(database, syncGroupCode);

			return organizationDataSet;
		}

		public void getOrganizationInfo(string organizationNo, ref string userName, ref string passWord)
		{
			Organizations organizations = new Organizations();
			Organization organization = organizations.getOrganization(database, organizationNo);
			userName = organization.navisionUserId;
			passWord = organization.navisionPassword;

		}

		public void updateItem(string xmlRecord)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlRecord);
            			
			Item itemFromNavision = new Item(xmlDoc.DocumentElement, database, false);

			Items items = new Items();
			Item item = items.getEntry(database, itemFromNavision.no);

			if (item != null)
			{
				//Save new item if not same as original
				if (!item.sameAs(itemFromNavision))
				{
					item.fromDOM(xmlDoc.DocumentElement);
					item.save(database);
				}
			}
			else
			{
				itemFromNavision.save(database);
			}

		}

		public void updateItemPrice(string xmlRecord)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xmlRecord);
            
				ItemPrice itemPrice = new ItemPrice(xmlDoc.DocumentElement, database, true);
			}
			catch(Exception e)
			{
				throw new Exception("updateItemPriceException [Data: "+xmlRecord+"] [Message: "+e.Message);
			}
		}

		public void updateItemPriceExtended(string xmlRecord)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlRecord);
            			
			ItemPriceExtended itemPriceExtended = new ItemPriceExtended(xmlDoc.DocumentElement, database, true);

		}

		public void updateCustomer(string xmlRecord, string organizationNo)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlRecord);

			Organizations organizations = new Organizations();
			Organization organization = organizations.getOrganization(database, organizationNo);
            			
			Customer customerFromNavision = new Customer(xmlDoc.DocumentElement, organization, database, false);

			Customers customers = new Customers();
			Customer customer = customers.getEntry(database, organization.no, customerFromNavision.no);
			

			if (customer != null)
			{
				customerFromNavision.directionComment = customer.directionComment;
				customerFromNavision.directionComment2 = customer.directionComment2;
				customerFromNavision.positionX = customer.positionX;
				customerFromNavision.positionY = customer.positionY;


				//Compare customers

				if (!organization.overwriteFromNavision)
				{
					if (customer.phoneNo != "") customerFromNavision.phoneNo = customer.phoneNo;
					if (customer.cellPhoneNo != "") customerFromNavision.cellPhoneNo = customer.cellPhoneNo;
					if (customer.productionSite != "") customerFromNavision.productionSite = customer.productionSite;

					if (customerFromNavision.dairyCode == "") customerFromNavision.dairyCode = customer.dairyCode;
					if (customerFromNavision.dairyNo == "") customerFromNavision.dairyNo = customer.dairyNo;

				}

				//Save new customer if not same as original



				if (!customer.sameAs(customerFromNavision)) customerFromNavision.save(database);

			}
			else
			{
				customerFromNavision.save(database);
			}

		}

		public void updateShippingCustomer(string xmlRecord)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlRecord);
          			
			ShippingCustomer shippingCustomerFromNavision = new ShippingCustomer(xmlDoc.DocumentElement, database, false);

			ShippingCustomers shippingCustomers = new ShippingCustomers();
			ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, shippingCustomerFromNavision.no);

			if (shippingCustomer == null)
			{
				shippingCustomerFromNavision.save(database);
			}
			else
			{
				if (shippingCustomer.name == "") shippingCustomerFromNavision.save(database);
			}

		}

		public void updatePurchasePrice(string xmlRecord)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xmlRecord);
            			
				PurchasePrice purchasePrice = new PurchasePrice(xmlDoc.DocumentElement, database, true);
			}
			catch(Exception e)
			{
				throw new Exception("updatePurchasePriceException [Data: "+xmlRecord+"] [Message: "+e.Message);
			}

		}

		private void resendDocuments()
		{
			DateTime lastResendDate = new DateTime(1753, 01, 01);

			SqlDataReader dataReader = database.query("SELECT [Last Resend Date] FROM [Smart Setup]");
			if (dataReader.Read())
			{
				lastResendDate = dataReader.GetDateTime(0);
			}
			dataReader.Close();

			if (DateTime.Today != lastResendDate)
			{
				ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
				shipmentHeaders.resend(database);

				ScaleEntries scaleEntries = new ScaleEntries();
				scaleEntries.resend(database);

				database.nonQuery("UPDATE [Smart Setup] SET [Last Resend Date] = '"+DateTime.Today+"'");
			}
		}


		public void dispose()
		{
			database.close();
		}


		#region Logger Members

		public void write(string message, int type)
		{
			// TODO:  Add Quiksilver.write implementation
		}

		#endregion
	}
}
