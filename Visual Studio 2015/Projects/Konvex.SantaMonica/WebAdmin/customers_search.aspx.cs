using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.WebAdmin
{
	/// <summary>
	/// Summary description for customers_search.
	/// </summary>
	public class customers_search : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet customerDataSet;
		protected Navipro.SantaMonica.Common.Customers customers;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Session["current.user.operator"] == null)
			{
				Response.Redirect("default.htm");				
			}

			database = (Database)Session["database"];

			currentOperator = (UserOperator)Session["current.user.operator"];
			currentOrganization = (Organization)Session["current.user.organization"];


			if (!currentOperator.checkSecurity(database, currentOrganization, "customers.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			customers = new Navipro.SantaMonica.Common.Customers();

			if (Request["command"] == "searchCustomer")
			{
				customers.setSearchCriteria(Request["searchCustomerNo"], Request["searchRegNo"], Request["searchName"], Request["searchCity"], Request["searchProdSite"], Request["searchPhoneNo"], Request["searchPaymentType"]);
				if (currentOrganization.callCenterMaster) customers.setCallCenter("");
			}

			if (Request["command"] == "setCustomer")
			{
				Customer customer = customers.getEntry(database, Request["organizationNo"], Request["customerNo"]);

				if (Request["origin"] == "scheduledOrders")
				{

					ScheduledShipOrders shipOrders = new ScheduledShipOrders();
					ScheduledShipOrder shipOrder = shipOrders.getEntry(database, currentOrganization.no, Request["shipOrderNo"]);
					if (shipOrder == null)
					{
						shipOrder = new ScheduledShipOrder(currentOrganization.no);
					}

					if (Request["mode"] == "2")
					{
						shipOrder.applyBillToCustomer(customer);

					}
					else
					{
						shipOrder.applySellToCustomer(customer);

						if (Request["customerShipAddressNo"] != "")
						{
							CustomerShipAddresses customerShipAddresses = new CustomerShipAddresses();
							CustomerShipAddress shipAddress = customerShipAddresses.getEntry(database, Request["organizationNo"], customer.no, Request["customerShipAddressNo"]);

							shipOrder.applyShipToAddress(shipAddress);
						}
					}

					shipOrder.save(database);

					Response.Redirect("scheduled_orders_modify.aspx?shipOrderNo="+shipOrder.entryNo, true);


				}
				else
				{

					ShipOrders shipOrders = new ShipOrders();
					ShipOrder shipOrder = shipOrders.getEntry(database, currentOrganization.no, Request["shipOrderNo"]);
					if (currentOrganization.callCenterMaster) shipOrder = shipOrders.getEntry(database, Request["shipOrderNo"]);

					if (shipOrder == null)
					{
						shipOrder = new ShipOrder(currentOrganization.no);
					}

					if (Request["mode"] == "2")
					{
						shipOrder.applyBillToCustomer(customer);

					}
					else
					{
						shipOrder.applySellToCustomer(customer);

						if (Request["customerShipAddressNo"] != "")
						{
							CustomerShipAddresses customerShipAddresses = new CustomerShipAddresses();
							CustomerShipAddress shipAddress = customerShipAddresses.getEntry(database, currentOrganization.no, customer.no, Request["customerShipAddressNo"]);

							shipOrder.applyShipToAddress(shipAddress);

						}
					}

					shipOrder.save(database);

					if (currentOrganization.callCenterMaster)
					{
						shipOrder.changeOrganization(database, customer.organizationNo, currentOperator.userId);
					}


					Response.Redirect("orders_modify_full.aspx?shipOrderNo="+shipOrder.entryNo, true);
				}
			}

			string organizationNo = currentOrganization.no;
			if (currentOrganization.callCenterMaster)
			{
				ShipOrders shipOrders = new ShipOrders();
				ShipOrder shipOrder = shipOrders.getEntry(database, Request["shipOrderNo"]);
				if (shipOrder != null)
				{
					currentOrganization.no = shipOrder.organizationNo;
				}
			}
			customerDataSet = customers.getDataSet(database, Request["organizationNo"]);

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
