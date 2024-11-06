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
	public class orders_view : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected Navipro.SantaMonica.Common.Customers customers;
		protected DataSet customerShipAddressDataSet;
		protected DataSet shipOrderLineDataSet;
		protected Navipro.SantaMonica.Common.Customer billToCustomer;

		protected DataSet shipOrderLogLineDataSet;

		protected DataSet itemDataSet1;
		protected DataSet itemDataSet2;
		protected DataSet itemDataSet3;
		protected DataSet itemDataSet4;
		protected Navipro.SantaMonica.Common.Items items;
		protected Navipro.SantaMonica.Common.ShipOrderLineIds shipOrderLineIds;

		protected ShipOrder currentShipOrder;
		protected Customer currentCustomer;

		protected bool notifyUserAboutPayment;

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

			items = new Navipro.SantaMonica.Common.Items();
			itemDataSet1 = items.getDataSet(database, 1);
			itemDataSet2 = items.getDataSet(database, 2);
			itemDataSet3 = items.getDataSet(database, 3);
			itemDataSet4 = items.getDataSet(database, 4);

			ShipOrderLines shipOrderLines = new ShipOrderLines();
			shipOrderLineIds = new ShipOrderLineIds();

			ShipOrderLogLines shipOrderLogLines = new ShipOrderLogLines();

			if (Request["shipOrderNo"] != null)
			{
				ShipOrders shipOrders = new ShipOrders();
				
				currentShipOrder = shipOrders.getEntry(database, currentOrganization.no, Request["shipOrderNo"]);
				if (currentOrganization.callCenterMaster) currentShipOrder = shipOrders.getEntry(database, Request["shipOrderNo"]);
				if (currentShipOrder == null)
				{
					currentShipOrder = new ShipOrder(currentOrganization.no);
				}
			
			}
			else
			{
				currentShipOrder = new ShipOrder(currentOrganization.no);
			}

			if (currentShipOrder.customerNo != "")
			{
				Navipro.SantaMonica.Common.Customers commonCustomers = new Navipro.SantaMonica.Common.Customers();
				currentCustomer = commonCustomers.getEntry(database, currentShipOrder.organizationNo, currentShipOrder.customerNo);
				if (currentCustomer == null) currentCustomer = new Customer();

			}
			else
			{
				currentCustomer = new Customer();
			}


			if (Request["command"] == "saveOrder")
			{
				Response.Redirect("orders.aspx");
			}

			if (Request["command"] == "deleteLine")
			{
				ShipOrderLine shipOrderLine = shipOrderLines.getEntry(database, currentShipOrder.entryNo, int.Parse(Request["lineNo"]));
				shipOrderLine.delete(database);

				currentShipOrder.updateDetails(database);
				currentShipOrder.save(database);

			}

			if (Request["command"] == "addItem")
			{
				ShipOrderLine shipOrderLine = new ShipOrderLine(currentShipOrder);
				
				Customer customer = new Customer();
				if (currentShipOrder.customerNo != "") customer = customers.getEntry(database, currentOrganization.no, currentShipOrder.customerNo);

				int quantity = 0;
				try
				{
					quantity = int.Parse(Request["quantity"]);
				}
				catch(Exception f)
				{
					if (f.Message != "") {}				
				}

				int connectionQuantity = 0;
				try
				{
					connectionQuantity = int.Parse(Request["connectionQuantity"]);
				}
				catch(Exception f)
				{
					if (f.Message != "") {}								
				}

				int testQuantity = 0;
				try
				{
					testQuantity = int.Parse(Request["testQuantity"]);
				}
				catch(Exception f)
				{
					if (f.Message != "") {}				
				}

				Item item = items.getEntry(database, Request["itemNo"]);

				shipOrderLine.itemNo = item.no;
				shipOrderLine.quantity = quantity;

				ItemPriceExtended itemPriceExt = new ItemPriceExtended(database, item, quantity, customer);
				if (itemPriceExt.lineAmount > 0)
				{
					shipOrderLine.amount = (float)itemPriceExt.lineAmount;
					shipOrderLine.unitPrice = (float)(decimal.Round(itemPriceExt.lineAmount / quantity, 2));
				}
				else
				{
					ItemPrice itemPrice = new ItemPrice(database, item, quantity, customer);
					if (itemPrice.unitPrice > 0)
					{
						shipOrderLine.unitPrice = (float)itemPrice.unitPrice;
						shipOrderLine.amount = (float)(itemPrice.unitPrice * quantity);
					}
					else
					{
						shipOrderLine.unitPrice = (float)item.unitPrice;
						shipOrderLine.amount = (float)(item.unitPrice * quantity);
					}
				}
				

				if (item.connectionItemNo != "")
				{
					Item connectionItem = items.getEntry(database, item.connectionItemNo);

					shipOrderLine.connectionItemNo = connectionItem.no;
					shipOrderLine.connectionQuantity = connectionQuantity;

					ItemPriceExtended connItemPriceExt = new ItemPriceExtended(database, connectionItem, connectionQuantity, customer);
					if (connItemPriceExt.lineAmount > 0)
					{
						shipOrderLine.connectionAmount = (float)(connItemPriceExt.lineAmount);
						shipOrderLine.connectionUnitPrice = (float)decimal.Round(connItemPriceExt.lineAmount / connectionQuantity, 2);
					}
					else
					{
						ItemPrice connItemPrice = new ItemPrice(database, connectionItem, connectionQuantity, customer);
						if (connItemPrice.unitPrice > 0)
						{
							shipOrderLine.connectionUnitPrice = (float)connItemPrice.unitPrice;
							shipOrderLine.connectionAmount = (float)(connItemPrice.unitPrice * connectionQuantity);
						}
						else
						{
							shipOrderLine.connectionUnitPrice = (float)item.unitPrice;
							shipOrderLine.connectionAmount = (float)(item.unitPrice * connectionQuantity);
						}
					}

				}

				shipOrderLine.testQuantity = testQuantity;
				shipOrderLine.totalAmount = shipOrderLine.amount + shipOrderLine.connectionAmount;
				shipOrderLine.save(database);

				if (Request["id1"] != "")
				{
					ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
					shipOrderLineId.unitId = Request["id1"];

					if (Request["bse1"] == "on") shipOrderLineId.bseTesting = true;
					if (Request["obd1"] == "on") shipOrderLineId.postMortem = true;

					shipOrderLineId.save(database);
				}

				if (Request["id2"] != "")
				{
					ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
					shipOrderLineId.unitId = Request["id2"];

					if (Request["bse2"] == "on") shipOrderLineId.bseTesting = true;
					if (Request["obd2"] == "on") shipOrderLineId.postMortem = true;
					
					shipOrderLineId.save(database);
				}

				if (Request["id3"] != "")
				{
					ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
					shipOrderLineId.unitId = Request["id3"];

					if (Request["bse3"] == "on") shipOrderLineId.bseTesting = true;
					if (Request["obd3"] == "on") shipOrderLineId.postMortem = true;
					
					shipOrderLineId.save(database);
				}

				if (Request["id4"] != "")
				{
					ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
					shipOrderLineId.unitId = Request["id4"];

					if (Request["bse4"] == "on") shipOrderLineId.bseTesting = true;
					if (Request["obd4"] == "on") shipOrderLineId.postMortem = true;

					shipOrderLineId.save(database);
				}

				if (Request["id5"] != "")
				{
					ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
					shipOrderLineId.unitId = Request["id5"];

					if (Request["bse5"] == "on") shipOrderLineId.bseTesting = true;
					if (Request["obd5"] == "on") shipOrderLineId.postMortem = true;
					
					shipOrderLineId.save(database);
				}

				if (item.addStopItem)
				{
					ShipOrderLine stopShipOrderLine = new ShipOrderLine(currentShipOrder);
					stopShipOrderLine.itemNo = item.stopItemNo;
					stopShipOrderLine.quantity = 1;
					stopShipOrderLine.unitPrice = float.Parse(currentOrganization.stopFee.ToString());
					stopShipOrderLine.amount = float.Parse(currentOrganization.stopFee.ToString());
					stopShipOrderLine.totalAmount = float.Parse(currentOrganization.stopFee.ToString());
					stopShipOrderLine.save(database);

				}

				if (item.requireCashPayment)
				{
					if (currentShipOrder.paymentType == 0)
					{
						currentShipOrder.paymentType = 1;
						notifyUserAboutPayment = true;
					}
				}

				currentShipOrder.updateDetails(database);
				currentShipOrder.save(database, false);
			}

			
			shipOrderLineDataSet = shipOrderLines.getDataSet(database, currentShipOrder.entryNo);
			shipOrderLogLineDataSet = shipOrderLogLines.getDataSet(database, currentShipOrder.entryNo);

			if (currentShipOrder.billToCustomerNo != "") 
			{
				billToCustomer = customers.getEntry(database, currentOrganization.no, currentShipOrder.billToCustomerNo);
				if (billToCustomer == null) billToCustomer = new Customer();
			}
			else
			{
				billToCustomer = new Customer();
			}

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
