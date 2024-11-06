using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

namespace Navipro.SantaMonica.Goldfinger.WebService
{
	/// <summary>
	/// Summary description for Quiksilver.
	/// </summary>
	
	public class Quiksilver : System.Web.Services.WebService
	{
		private Navipro.SantaMonica.Goldfinger.Quiksilver quiksilver;

		public Quiksilver()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

			quiksilver = new Navipro.SantaMonica.Goldfinger.Quiksilver();
			quiksilver.init();

		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

		[WebMethod]
		public DataSet getAvailableShipments()
		{
			DataSet shipmentDataSet = quiksilver.getAvailableShipments();
			quiksilver.dispose();
			return shipmentDataSet;
		}

		[WebMethod]
		public DataSet getShipmentLines(string shipmentNo)
		{
			DataSet shipmentLinesDataSet = quiksilver.getShipmentLines(shipmentNo);
			quiksilver.dispose();
			return shipmentLinesDataSet;
		}

		[WebMethod]
		public DataSet getShipmentLineIds(string shipmentNo, int shipmentLineNo)
		{
			DataSet shipmentLineIdDataSet = quiksilver.getShipmentLineIds(shipmentNo, shipmentLineNo);
			quiksilver.dispose();
			return shipmentLineIdDataSet;
		}

		[WebMethod]
		public void setShipmentStatus(string shipmentNo, int status)
		{
			quiksilver.setShipmentStatus(shipmentNo, status);
			quiksilver.dispose();
		}

		[WebMethod]
		public void getOrganizationInfo(string organizationNo, ref string userName, ref string passWord, ref bool callCenterMember)
		{
			quiksilver.getOrganizationInfo(organizationNo, ref userName, ref passWord, ref callCenterMember);
			quiksilver.dispose();
		}

		[WebMethod]
		public DataSet getOrganizations(string syncGroupCode)
		{
			DataSet organizationDataSet = quiksilver.getOrganizations(syncGroupCode);
			quiksilver.dispose();
			return organizationDataSet;
		}

		[WebMethod]
		public void updateCustomer(string xmlRecord, string organizationNo)
		{
			quiksilver.updateCustomer(xmlRecord, organizationNo);
			quiksilver.dispose();
		}

		[WebMethod]
		public void updateItem(string xmlRecord)
		{
			quiksilver.updateItem(xmlRecord);
			quiksilver.dispose();
		}

		[WebMethod]
		public void updateItemPrice(string xmlRecord)
		{
			quiksilver.updateItemPrice(xmlRecord);
			quiksilver.dispose();
		}

		[WebMethod]
		public void updateItemPriceExtended(string xmlRecord)
		{
			quiksilver.updateItemPriceExtended(xmlRecord);
			quiksilver.dispose();
		}

		[WebMethod]
		public void updatePurchasePrice(string xmlRecord)
		{
			quiksilver.updatePurchasePrice(xmlRecord);
			quiksilver.dispose();
		}

		[WebMethod]
		public void updateShippingCustomer(string xmlRecord)
		{
			quiksilver.updateShippingCustomer(xmlRecord);
			quiksilver.dispose();
		}

		[WebMethod]
		public DataSet getUpdatedCustomers()
		{
			DataSet customerDataSet = quiksilver.getUpdatedCustomers();
			quiksilver.dispose();
			return customerDataSet;
		}

		[WebMethod]
		public void setCustomerUpdated(string organizationNo, string customerNo, bool updated)
		{
			quiksilver.setCustomerUpdated(organizationNo, customerNo, updated);
			quiksilver.dispose();
		}


		[WebMethod]
		public DataSet getScaleEntries()
		{
			DataSet scaleEntryDataSet = quiksilver.getScaleEntries();
			quiksilver.dispose();
			return scaleEntryDataSet;
		}

		[WebMethod]
		public void setScaleEntryStatus(string factoryCode, int entryNo, int status)
		{
			quiksilver.setScaleEntryStatus(factoryCode, entryNo, status);
			quiksilver.dispose();
		}

		[WebMethod]
		public DataSet getFactoryOrderEntries()
		{
			DataSet factoryOrderEntryDataSet = quiksilver.getFactoryOrderEntries();
			quiksilver.dispose();
			return factoryOrderEntryDataSet;
		}

		[WebMethod]
		public void setFactoryOrderEntryStatus(int entryNo, int status)
		{
			quiksilver.setFactoryOrderEntryStatus(entryNo, status);
			quiksilver.dispose();
		}

		[WebMethod]
		public DataSet getUnInvoicedRoutes()
		{
			DateTime fromDate = DateTime.Parse(System.Web.HttpContext.Current.Request["fromDate"]);
			DateTime toDate = DateTime.Parse(System.Web.HttpContext.Current.Request["toDate"]);

			DataSet invoiceSet = quiksilver.getUnInvoicedRoutes(fromDate, toDate);
			quiksilver.dispose();
			return invoiceSet;
		}

		[WebMethod]
		public DataSet getUnInvoicedFactoryOrders()
		{
			DateTime fromDate = DateTime.Parse(System.Web.HttpContext.Current.Request["fromDate"]);
			DateTime toDate = DateTime.Parse(System.Web.HttpContext.Current.Request["toDate"]);

			DataSet invoiceSet = quiksilver.getUnInvoicedFactoryOrders(fromDate, toDate);
			quiksilver.dispose();
			return invoiceSet;
		}

		[WebMethod]
		public DataSet getUnInvoicedTransportFactoryOrders()
		{
			DateTime fromDate = DateTime.Parse(System.Web.HttpContext.Current.Request["fromDate"]);
			DateTime toDate = DateTime.Parse(System.Web.HttpContext.Current.Request["toDate"]);

			DataSet invoiceSet = quiksilver.getUnInvoicedTransportFactoryOrders(fromDate, toDate);
			quiksilver.dispose();
			return invoiceSet;
		}

		[WebMethod]
		public DataSet getFactoryInventory()
		{
			DateTime date = DateTime.Parse(System.Web.HttpContext.Current.Request["date"]);

			DataSet inventorySet = quiksilver.getFactoryInventoryStatus(date);
			quiksilver.dispose();
			return inventorySet;
		}

		[WebMethod]
		public void updateCustomerEx(string vendorNo, string no, string name, string address, string address2, string postCode, string city, string countryCode, string billToCustomerNo, string contactName, int blocked, string phoneNo, string cellPhoneNo, string email, string faxNo, string dairyCode, string dairyNo, bool forceCashPayment, bool hide, string personNo, string priceGroupCode, string productionSite, string registrationNo)
		{

			quiksilver.updateCustomer(vendorNo, no, name, address, address2, postCode, city, countryCode, billToCustomerNo, contactName, blocked, phoneNo, cellPhoneNo, email, faxNo, dairyCode, dairyNo, forceCashPayment, hide, personNo, priceGroupCode, productionSite, registrationNo);
			quiksilver.dispose();

		}
	}
}
