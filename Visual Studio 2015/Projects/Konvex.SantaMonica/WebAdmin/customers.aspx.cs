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
	/// Summary description for customers.
	/// </summary>
	public class Customers : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet customerDataSet;
		protected DataSet organizationDataSet;
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

			Organizations organizations = new Organizations();
			organizationDataSet = organizations.getCallCenterMemberDataSet(database);

			if (Request["command"] == "searchCustomer")
			{
				customers.setSearchCriteria(Request["searchCustomerNo"], Request["searchRegNo"], Request["searchName"], Request["searchCity"], Request["searchProdSite"], Request["searchPhoneNo"], Request["searchPaymentType"]);
				if (currentOrganization.callCenterMaster) customers.setCallCenter(Request["organizationNo"]);
			}


			if (Request["command"] == "createOrder")
			{
				string organizationNo = currentOrganization.no;
				if (currentOrganization.callCenterMaster) organizationNo = Request["orgNo"];
				
				Organization organization = organizations.getOrganization(database, organizationNo);

				Customer customer = customers.getEntry(database, organizationNo, Request["customerNo"]);

				ShipOrder shipOrder = new ShipOrder(customer);		

				if (Request["customerShipAddressNo"] != "")
				{
					CustomerShipAddresses customerShipAddresses = new CustomerShipAddresses();
					CustomerShipAddress shipAddress = customerShipAddresses.getEntry(database, currentOrganization.no, customer.no, Request["customerShipAddressNo"]);

					shipOrder.applyShipToAddress(shipAddress);
				}


				shipOrder.comments = currentOperator.userId+"> "+shipOrder.comments;
				shipOrder.save(database);
				shipOrder.log(database, currentOperator.userId, "Körordern skapad");



				Response.Redirect("orders_modify_full.aspx?shipOrderNo="+shipOrder.entryNo, true);
			}
	

			customerDataSet = customers.getDataSet(database, currentOrganization.no);

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
