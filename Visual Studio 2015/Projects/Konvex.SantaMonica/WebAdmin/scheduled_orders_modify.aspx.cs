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
	/// Summary description for orders_new.
	/// </summary>
	public class scheduled_orders_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected Navipro.SantaMonica.Common.Customers customers;
		protected DataSet customerShipAddressDataSet;
		protected Navipro.SantaMonica.Common.Customer billToCustomer;
		protected Navipro.SantaMonica.Common.Customer sellToCustomer;

		protected ScheduledShipOrder currentShipOrder;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "scheduled_orders.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			CustomerShipAddresses custShipAddresses = new CustomerShipAddresses();

			customers = new Navipro.SantaMonica.Common.Customers();


			if (Request["shipOrderNo"] != null)
			{
				ScheduledShipOrders shipOrders = new ScheduledShipOrders();
				
				currentShipOrder = shipOrders.getEntry(database, currentOrganization.no, Request["shipOrderNo"]);
				if (currentShipOrder == null)
				{
					currentShipOrder = new ScheduledShipOrder(currentOrganization.no);
				}
				
			}
			else
			{
				currentShipOrder = new ScheduledShipOrder(currentOrganization.no);
			}


			if (Request["command"] != null)
			{
				currentShipOrder.customerShipAddressNo = Request["customerShipAddressNo"];
				currentShipOrder.shipName = Request["shipName"];
				currentShipOrder.shipAddress = Request["shipAddress"];
				currentShipOrder.shipAddress2 = Request["shipAddress2"];
				currentShipOrder.shipPostCode = Request["shipPostCode"];
				currentShipOrder.shipCity = Request["shipCity"];

				currentShipOrder.comments = Request["comments"];
				currentShipOrder.paymentType = int.Parse(Request["paymentType"]);

				currentShipOrder.phoneNo = Request["phoneNo"];
				currentShipOrder.cellPhoneNo = Request["cellPhoneNo"];

				currentShipOrder.monday = false;
				currentShipOrder.tuesday = false;
				currentShipOrder.wednesday = false;
				currentShipOrder.thursday = false;
				currentShipOrder.friday = false;
				currentShipOrder.saturday = false;
				currentShipOrder.sunday = false;

				if (Request["monday"] == "on") currentShipOrder.monday = true;
				if (Request["tuesday"] == "on") currentShipOrder.tuesday = true;
				if (Request["wednesday"] == "on") currentShipOrder.wednesday = true;
				if (Request["thursday"] == "on") currentShipOrder.thursday = true;
				if (Request["friday"] == "on") currentShipOrder.friday = true;
				if (Request["saturday"] == "on") currentShipOrder.saturday = true;
				if (Request["sunday"] == "on") currentShipOrder.sunday = true;

				currentShipOrder.weekType = int.Parse(Request["weekType"]);

				if (Request["directionComment"].Length > 200)
				{
					currentShipOrder.directionComment = Request["directionComment"].Substring(0, 200);
					currentShipOrder.directionComment2 = Request["directionComment"].Substring(200);
				}
				else
				{
					currentShipOrder.directionComment = Request["directionComment"];
				}

			}



			if (Request["command"] == "setShipAddress")
			{
				CustomerShipAddress custShipAddress = custShipAddresses.getEntry(database, currentOrganization.no, Request["customerNo"], Request["customerShipAddressNo"]);
				if (custShipAddress != null)
				{

					currentShipOrder.customerShipAddressNo = custShipAddress.entryNo;
					currentShipOrder.shipName = custShipAddress.name;
					currentShipOrder.shipAddress = custShipAddress.address;
					currentShipOrder.shipAddress2 = custShipAddress.address2;
					currentShipOrder.shipPostCode = custShipAddress.postCode;
					currentShipOrder.shipCity = custShipAddress.city;

					currentShipOrder.positionX = custShipAddress.positionX;
					currentShipOrder.positionY = custShipAddress.positionY;

					currentShipOrder.directionComment = custShipAddress.directionComment;
					currentShipOrder.directionComment2 = custShipAddress.directionComment2;
				}
				else
				{
					currentShipOrder.customerShipAddressNo = "";
					currentShipOrder.shipName = currentShipOrder.customerName;
					currentShipOrder.shipAddress = currentShipOrder.address;
					currentShipOrder.shipAddress2 = currentShipOrder.address2;
					currentShipOrder.shipPostCode = currentShipOrder.postCode;
					currentShipOrder.shipCity = currentShipOrder.city;

				}					
			}

			if (Request["command"] == "saveAddress")
			{
				CustomerShipAddress custShipAddress = new CustomerShipAddress();
				custShipAddress.organizationNo = currentOrganization.no;
				custShipAddress.customerNo = Request["customerNo"];
				custShipAddress.name = Request["shipName"];
				custShipAddress.address = Request["shipAddress"];
				custShipAddress.address2 = Request["shipAddress2"];
				custShipAddress.postCode = Request["shipPostCode"];
				custShipAddress.city = Request["shipCity"];

				custShipAddress.save(database);
				currentShipOrder.customerShipAddressNo = custShipAddress.entryNo;
			}

			if (Request["command"] == "saveOrder")
			{
				currentShipOrder.save(database);
				Response.Redirect("scheduled_orders.aspx");
			}

			if (Request["command"] == "deleteOrder")
			{
				currentShipOrder.delete(database);

				Response.Redirect("scheduled_orders.aspx");
			}
		
			if (Request["command"] == "searchCustomer")
			{
				Response.Redirect("customers_search.aspx?organizationNo="+currentOrganization.no+"&shipOrderNo="+currentShipOrder.entryNo+"&mode="+Request["mode"]+"&origin=scheduledOrders");
			}


			customerShipAddressDataSet = custShipAddresses.getDataSet(database, currentOrganization.no, currentShipOrder.customerNo);
			
			
			if (currentShipOrder.billToCustomerNo != "") billToCustomer = customers.getEntry(database, currentOrganization.no, currentShipOrder.billToCustomerNo);
			if (currentShipOrder.customerNo != "") sellToCustomer = customers.getEntry(database, currentOrganization.no, currentShipOrder.customerNo);

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
