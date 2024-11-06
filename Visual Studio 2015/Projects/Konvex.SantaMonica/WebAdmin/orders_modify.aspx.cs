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
	public class orders_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected Navipro.SantaMonica.Common.Customers customers;
		protected DataSet customerShipAddressDataSet;
		protected Navipro.SantaMonica.Common.Customer billToCustomer;
		protected Navipro.SantaMonica.Common.Customer sellToCustomer;

		protected ShipOrder currentShipOrder;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "orders.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			CustomerShipAddresses custShipAddresses = new CustomerShipAddresses();


			customers = new Navipro.SantaMonica.Common.Customers();


			if (Request["shipOrderNo"] != null)
			{
				ShipOrders shipOrders = new ShipOrders();
				
				currentShipOrder = shipOrders.getEntry(database, currentOrganization.no, Request["shipOrderNo"]);
				if (currentShipOrder == null)
				{
					currentShipOrder = new ShipOrder(currentOrganization.no);
				}
				
			}
			else
			{
				currentShipOrder = new ShipOrder(currentOrganization.no);
			}
			
			if (currentShipOrder.billToCustomerNo != "") billToCustomer = customers.getEntry(database, currentOrganization.no, currentShipOrder.billToCustomerNo);
			if (currentShipOrder.customerNo != "") sellToCustomer = customers.getEntry(database, currentOrganization.no, currentShipOrder.customerNo);


			if (Request["command"] != null)
			{
				sellToCustomer.productionSite = Request["productionSite"];

				if (Request["shipDateYear"] != null) currentShipOrder.shipDate = new DateTime(int.Parse(Request["shipDateYear"]), 1, 1, 0,0,0);
				if (Request["shipDateMonth"] != null) currentShipOrder.shipDate = currentShipOrder.shipDate.AddMonths(int.Parse(Request["shipDateMonth"])-1);
				if (Request["shipDateDay"] != null) currentShipOrder.shipDate = currentShipOrder.shipDate.AddDays(int.Parse(Request["shipDateDay"])-1);

				if (Request["creationDateYear"] != null) currentShipOrder.creationDate = new DateTime(int.Parse(Request["creationDateYear"]), 1, 1, 0,0,0);
				if (Request["creationDateMonth"] != null) currentShipOrder.creationDate = currentShipOrder.creationDate.AddMonths(int.Parse(Request["creationDateMonth"])-1);
				if (Request["creationDateDay"] != null) currentShipOrder.creationDate = currentShipOrder.creationDate.AddDays(int.Parse(Request["creationDateDay"])-1);

				currentShipOrder.customerShipAddressNo = Request["customerShipAddressNo"];
				currentShipOrder.shipName = Request["shipName"];
				currentShipOrder.shipAddress = Request["shipAddress"];
				currentShipOrder.shipAddress2 = Request["shipAddress2"];
				currentShipOrder.shipPostCode = Request["shipPostCode"];
				currentShipOrder.shipCity = Request["shipCity"];

				currentShipOrder.customerName = Request["customerName"];
				currentShipOrder.address = Request["address"];
				currentShipOrder.address2 = Request["address2"];
				currentShipOrder.postCode = Request["postCode"];
				currentShipOrder.city = Request["city"];

				currentShipOrder.phoneNo = Request["phoneNo"];
				currentShipOrder.cellPhoneNo = Request["cellPhoneNo"];
				currentShipOrder.comments = Request["comments"];
				currentShipOrder.paymentType = int.Parse(Request["paymentType"]);

				if (Request["directionComment"].Length > 200)
				{
					currentShipOrder.directionComment = Request["directionComment"].Substring(0, 200);
					currentShipOrder.directionComment2 = Request["directionComment"].Substring(200);
				}
				else
				{
					currentShipOrder.directionComment = Request["directionComment"];
					currentShipOrder.directionComment2 = "";
				}

				if (Request["priority"] != null) currentShipOrder.priority = int.Parse(Request["priority"]);

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
					currentShipOrder.phoneNo = custShipAddress.phoneNo;
					

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

					Customer customer = customers.getEntry(database, currentOrganization.no, currentShipOrder.customerNo);
					if (customer != null)
					{
						currentShipOrder.directionComment = customer.directionComment;
						currentShipOrder.directionComment2 = customer.directionComment2;
					}
				}					
			}

			if (Request["command"] == "saveAddress")
			{

				CustomerShipAddress custShipAddress = new CustomerShipAddress();
				if (Request["customerShipAddressNo"] != "") custShipAddress.entryNo = Request["customerShipAddressNo"];
				custShipAddress.organizationNo = currentOrganization.no;
				custShipAddress.customerNo = Request["customerNo"];
				custShipAddress.name = Request["shipName"];
				custShipAddress.address = Request["shipAddress"];
				custShipAddress.address2 = Request["shipAddress2"];
				custShipAddress.postCode = Request["shipPostCode"];
				custShipAddress.phoneNo = Request["phoneNo"];

				custShipAddress.city = Request["shipCity"];

				if (Request["directionComment"].Length > 200)
				{
					custShipAddress.directionComment = Request["directionComment"].Substring(0, 200);
					custShipAddress.directionComment2 = Request["directionComment"].Substring(200);
				}
				else
				{
					custShipAddress.directionComment = Request["directionComment"];
					custShipAddress.directionComment2 = "";
				}

				custShipAddress.save(database);
				currentShipOrder.customerShipAddressNo = custShipAddress.entryNo;
			}

			if (Request["command"] == "saveOrder")
			{
				sellToCustomer.productionSite = Request["productionSite"];
				sellToCustomer.save(database);

				currentShipOrder.save(database, false);
				setCustomerData(currentShipOrder);

				Response.Redirect("orders_view.aspx?shipOrderNo="+currentShipOrder.entryNo);
			}

			if (Request["command"] == "deleteOrder")
			{
				currentShipOrder.delete(database);

				Response.Redirect("orders.aspx");
			}

			if (Request["command"] == "markOrder")
			{
				currentShipOrder.assignOrder(database, "", "WEB");
				currentShipOrder.status = 7;
				currentShipOrder.save(database, false);

				Response.Redirect("orders.aspx");
			}
		
			if (Request["command"] == "searchCustomer")
			{
				Response.Redirect("customers_search.aspx?organizationNo="+currentOrganization.no+"&shipOrderNo="+currentShipOrder.entryNo+"&mode="+Request["mode"]);
			}


			customerShipAddressDataSet = custShipAddresses.getDataSet(database, currentOrganization.no, currentShipOrder.customerNo);
			
			
		}


		public void createDatePicker(string name, DateTime selectedDate)
		{
			Response.Write("<select name='"+name+"Year' class='Textfield'>");

			int year = DateTime.Now.Year-2;
			while (year < DateTime.Now.Year+1)
			{
				year++;
				if (year == selectedDate.Year)
				{
					Response.Write("<option value='"+year.ToString()+"' selected>"+year.ToString()+"</option>");
				}
				else
				{
					Response.Write("<option value='"+year.ToString()+"'>"+year.ToString()+"</option>");
				}
			}
			Response.Write("</select> - <select name='"+name+"Month' class='Textfield'>");

			int month = 0;
			while (month < 12)
			{
				month++;
				string monthStr = ""+month;
				if (monthStr.Length == 1) monthStr = "0"+month;

				if (month == selectedDate.Month)
				{
					Response.Write("<option value='"+monthStr+"' selected>"+monthStr+"</option>");
				}
				else
				{
					Response.Write("<option value='"+monthStr+"'>"+monthStr+"</option>");
				}
			}
			
			Response.Write("</select> - <select name='"+name+"Day' class='Textfield'>");
			
			int day = 0;
			while (day < 31)
			{
				day++;
				string dayStr = ""+day;
				if (dayStr.Length == 1) dayStr = "0"+day;

				if (day == selectedDate.Day)
				{
					Response.Write("<option value='"+dayStr+"' selected>"+dayStr+"</option>");
				}
				else
				{
					Response.Write("<option value='"+dayStr+"'>"+dayStr+"</option>");
				}
			}

			Response.Write("</select>");
		}																																						   

		private void setCustomerData(ShipOrder shipOrder)
		{
			if (shipOrder.customerNo != "")
			{
				Navipro.SantaMonica.Common.Customers commonCustomers = new Navipro.SantaMonica.Common.Customers();
				Customer customer = customers.getEntry(database, shipOrder.organizationNo, shipOrder.customerNo);
				
				//if ((shipOrder.phoneNo != "") && ((customer.phoneNo == null) || (customer.phoneNo == ""))) customer.phoneNo = shipOrder.phoneNo;
				//if ((shipOrder.cellPhoneNo != "") && ((customer.cellPhoneNo == null) || (customer.cellPhoneNo == ""))) customer.cellPhoneNo = shipOrder.cellPhoneNo;
				
				if (!customer.editable)
				{
					customer.phoneNo = shipOrder.phoneNo;
					customer.cellPhoneNo = shipOrder.cellPhoneNo;

					if (shipOrder.customerShipAddressNo != "")
					{
						CustomerShipAddresses shipAddresses = new CustomerShipAddresses();
						CustomerShipAddress shipAddress = shipAddresses.getEntry(database, shipOrder.organizationNo, shipOrder.customerNo, shipOrder.customerShipAddressNo);
						shipAddress.directionComment = shipOrder.directionComment;
						shipAddress.directionComment2 = shipOrder.directionComment2;
						shipAddress.save(database);
					}
					else
					{
						customer.directionComment = shipOrder.directionComment;
						customer.directionComment2 = shipOrder.directionComment2;
					}

					customer.setUpdated();
					customer.save(database);
				}

			}
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
