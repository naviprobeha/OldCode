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
	public class orders_modify_full : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected Navipro.SantaMonica.Common.Customers customers;
		protected DataSet customerShipAddressDataSet;
		protected Navipro.SantaMonica.Common.Customer billToCustomer;
		protected Navipro.SantaMonica.Common.Customer sellToCustomer;

		protected ShipOrder currentShipOrder;
		protected DataSet shipOrderLineDataSet;
		protected ShipOrderLineIds shipOrderLineIds;
		protected Navipro.SantaMonica.Common.Items items;

		protected DataSet itemDataSet1;
		protected DataSet itemDataSet2;
		protected DataSet itemDataSet3;
		protected DataSet itemDataSet4;

		protected bool notifyUserAboutPayment;
		protected string productionSiteErrorMessage = "";

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
			ShipOrderLines shipOrderLines = new ShipOrderLines();
			shipOrderLineIds = new ShipOrderLineIds();


			customers = new Navipro.SantaMonica.Common.Customers();

			items = new Navipro.SantaMonica.Common.Items();
			itemDataSet1 = items.getDataSet(database, 1);
			itemDataSet2 = items.getDataSet(database, 2);
			itemDataSet3 = items.getDataSet(database, 3);
			itemDataSet4 = items.getDataSet(database, 4);


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

			Organizations organizations = new Organizations();
			
			if (currentShipOrder.billToCustomerNo != "") billToCustomer = customers.getEntry(database, currentShipOrder.organizationNo, currentShipOrder.billToCustomerNo);
			if (currentShipOrder.customerNo != "") sellToCustomer = customers.getEntry(database, currentShipOrder.organizationNo, currentShipOrder.customerNo);

			if (billToCustomer == null) billToCustomer = new Customer();
			if (sellToCustomer == null) sellToCustomer = new Customer();


			if (Request["command"] != null)
			{
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
				currentShipOrder.productionSite = Request["productionSite"];

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

			if (currentShipOrder.productionSite != "")
			{
				Customer customer = customers.getProductionSiteCustomer(database, currentShipOrder.customerNo, currentShipOrder.productionSite);
				if (customer != null)
				{	
					productionSiteErrorMessage = "Produktionsplatsnummret finns redan på kund "+customer.no+" ("+customer.organizationNo+"), "+customer.name+".";
				}
			}

			if (Request["command"] == "setShipAddress")
			{
				CustomerShipAddress custShipAddress = custShipAddresses.getEntry(database, currentShipOrder.organizationNo, Request["customerNo"], Request["customerShipAddressNo"]);
				if (custShipAddress != null)
				{

					currentShipOrder.customerShipAddressNo = custShipAddress.entryNo;
					currentShipOrder.shipName = custShipAddress.name;
					currentShipOrder.shipAddress = custShipAddress.address;
					currentShipOrder.shipAddress2 = custShipAddress.address2;
					currentShipOrder.shipPostCode = custShipAddress.postCode;
					currentShipOrder.shipCity = custShipAddress.city;
					currentShipOrder.phoneNo = custShipAddress.phoneNo;
					currentShipOrder.productionSite = custShipAddress.productionSite;
					

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
						currentShipOrder.productionSite = customer.productionSite;
					}
				}					
			}

			if (Request["command"] == "saveAddress")
			{

				CustomerShipAddress custShipAddress = new CustomerShipAddress();
				if (Request["customerShipAddressNo"] != "") custShipAddress.entryNo = Request["customerShipAddressNo"];
				custShipAddress.organizationNo = currentShipOrder.organizationNo;
				custShipAddress.customerNo = Request["customerNo"];
				custShipAddress.name = Request["shipName"];
				custShipAddress.address = Request["shipAddress"];
				custShipAddress.address2 = Request["shipAddress2"];
				custShipAddress.postCode = Request["shipPostCode"];
				custShipAddress.phoneNo = Request["phoneNo"];
				custShipAddress.productionSite = Request["productionSite"];

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


			if (Request["command"] == "deleteOrder")
			{
				currentShipOrder.delete(database);

				Response.Redirect("orders.aspx");
			}

			if (Request["command"] == "clearAdminFee")
			{
				currentShipOrder.clearAdminFee(database);

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
				Response.Redirect("customers_search.aspx?organizationNo="+currentShipOrder.organizationNo+"&shipOrderNo="+currentShipOrder.entryNo+"&mode="+Request["mode"]);
			}

			if (Request["command"] == "deleteLine")
			{
				ShipOrderLine shipOrderLine = shipOrderLines.getEntry(database, currentShipOrder.entryNo, int.Parse(Request["lineNo"]));
				if (shipOrderLine != null)
				{
					shipOrderLine.delete(database);

					currentShipOrder.updateDetails(database);
					currentShipOrder.save(database);
				}
			}

			if ((Request["command"] == "addItem") || (Request["command"] == "saveOrder"))
			{
				ShipOrderLine shipOrderLine = new ShipOrderLine(currentShipOrder);
				
				Customer customer = new Customer();
				if (currentShipOrder.customerNo != "") customer = customers.getEntry(database, currentShipOrder.organizationNo, currentShipOrder.customerNo);

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

				if (quantity > 0)
				{
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

					string id1 = Request["id1"];
					string id2 = Request["id2"];
					string id3 = Request["id3"];
					string id4 = Request["id4"];
					string id5 = Request["id5"];
					bool bse1 = false;
					bool bse2 = false;
					bool bse3 = false;
					bool bse4 = false;
					bool bse5 = false;
					if (Request["bse1"] == "on") bse1 = true;
					if (Request["bse2"] == "on") bse2 = true;
					if (Request["bse3"] == "on") bse3 = true;
					if (Request["bse4"] == "on") bse4 = true;
					if (Request["bse5"] == "on") bse5 = true;

					if ((item.invoiceToJbv) && (id1 == ""))
					{
						id1 = "OKÄND";
						bse1 = true;
					}

					if (id1 != "")
					{
						ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
						shipOrderLineId.unitId = id1;

						if (bse1) shipOrderLineId.bseTesting = true;
						if (Request["obd1"] == "on") shipOrderLineId.postMortem = true;

						shipOrderLineId.save(database);
					}

					if (id2 != "")
					{
						ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
						shipOrderLineId.unitId = id2;

						if (bse2) shipOrderLineId.bseTesting = true;
						if (Request["obd2"] == "on") shipOrderLineId.postMortem = true;
					
						shipOrderLineId.save(database);
					}

					if (id3 != "")
					{
						ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
						shipOrderLineId.unitId = id3;

						if (bse3) shipOrderLineId.bseTesting = true;
						if (Request["obd3"] == "on") shipOrderLineId.postMortem = true;
					
						shipOrderLineId.save(database);
					}

					if (id4 != "")
					{
						ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
						shipOrderLineId.unitId = id4;

						if (bse4) shipOrderLineId.bseTesting = true;
						if (Request["obd4"] == "on") shipOrderLineId.postMortem = true;

						shipOrderLineId.save(database);
					}

					if (id5 != "")
					{
						ShipOrderLineId shipOrderLineId = new ShipOrderLineId(shipOrderLine);
						shipOrderLineId.unitId = id5;

						if (bse5) shipOrderLineId.bseTesting = true;
						if (Request["obd5"] == "on") shipOrderLineId.postMortem = true;
					
						shipOrderLineId.save(database);
					}

					if (item.addStopItem)
					{
						Organization organization = organizations.getOrganization(database, currentShipOrder.organizationNo);
						if (organization != null)
						{
							ShipOrderLine stopShipOrderLine = new ShipOrderLine(currentShipOrder);
							stopShipOrderLine.itemNo = item.stopItemNo;
							stopShipOrderLine.quantity = 1;
							stopShipOrderLine.unitPrice = float.Parse(organization.stopFee.ToString());
							stopShipOrderLine.amount = float.Parse(organization.stopFee.ToString());
							stopShipOrderLine.totalAmount = float.Parse(organization.stopFee.ToString());
							stopShipOrderLine.save(database);
						}
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
			}

			if (Request["command"] == "saveOrder")
			{

				if (currentShipOrder.organizationNo != Request["organizationNo"])
				{
					if (currentShipOrder.customerShipAddressNo != "")
					{
						CustomerShipAddress custShipAddress = custShipAddresses.getEntry(database, currentShipOrder.organizationNo, currentShipOrder.customerNo, currentShipOrder.customerShipAddressNo);
						if (custShipAddress != null)
						{
							custShipAddress.organizationNo = Request["organizationNo"];
							custShipAddress.save(database);

							currentShipOrder.customerShipAddressNo = custShipAddress.entryNo;
							currentShipOrder.save(database, false);
						}

					}

					currentShipOrder.assignOrder(database, "", currentOperator.userId);
					currentShipOrder.changeOrganization(database, Request["organizationNo"], currentOperator.userId);

					if (customers.getEntry(database, currentShipOrder.organizationNo, sellToCustomer.no) == null)
					{
						sellToCustomer.organizationNo = currentShipOrder.organizationNo;
						sellToCustomer.save(database);
					}
					if (customers.getEntry(database, currentShipOrder.organizationNo, billToCustomer.no) == null)
					{
						billToCustomer.organizationNo = currentShipOrder.organizationNo;
						billToCustomer.save(database);
					}

					Response.Redirect("orders.aspx");
				}

				currentShipOrder.save(database, false);

				if (productionSiteErrorMessage == "")
				{
					if (Request["update"] == "true") setCustomerData(currentShipOrder);

					if (currentShipOrder.agentCode == "")
					{
						Response.Redirect("orders_assign.aspx?shipOrderNo="+currentShipOrder.entryNo);
					}
					else
					{
						Response.Redirect("orders_view.aspx?shipOrderNo="+currentShipOrder.entryNo);
					}
				}
			}



			customerShipAddressDataSet = custShipAddresses.getDataSet(database, currentShipOrder.organizationNo, currentShipOrder.customerNo);		
			shipOrderLineDataSet = shipOrderLines.getDataSet(database, currentShipOrder.entryNo);
			
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
				Customer billcustomer = customers.getEntry(database, shipOrder.organizationNo, shipOrder.billToCustomerNo);

				//if ((shipOrder.phoneNo != "") && ((customer.phoneNo == null) || (customer.phoneNo == ""))) customer.phoneNo = shipOrder.phoneNo;
				//if ((shipOrder.cellPhoneNo != "") && ((customer.cellPhoneNo == null) || (customer.cellPhoneNo == ""))) customer.cellPhoneNo = shipOrder.cellPhoneNo;
				
				if (!billcustomer.editable)
				{
					//if ((billcustomer.name != shipOrder.customerName) || (billcustomer.address != shipOrder.address) || (billcustomer.address2 != shipOrder.address2) || (billcustomer.postCode != shipOrder.postCode)
					billcustomer.name = shipOrder.customerName;
					billcustomer.address = shipOrder.address;
					billcustomer.address2 = shipOrder.address2;
					billcustomer.postCode = shipOrder.postCode;
					billcustomer.city = shipOrder.city;

					billcustomer.save(database);


					billcustomer.setUpdated();
					billcustomer.save(database);

				}

				if (!customer.editable)
				{
					customer = customers.getEntry(database, shipOrder.organizationNo, shipOrder.customerNo);
					

					customer.cellPhoneNo = shipOrder.cellPhoneNo;

					if (shipOrder.customerShipAddressNo != "")
					{
						CustomerShipAddresses shipAddresses = new CustomerShipAddresses();
						CustomerShipAddress shipAddress = shipAddresses.getEntry(database, shipOrder.organizationNo, shipOrder.customerNo, shipOrder.customerShipAddressNo);
						shipAddress.directionComment = shipOrder.directionComment;
						shipAddress.directionComment2 = shipOrder.directionComment2;
						shipAddress.productionSite = shipOrder.productionSite;
						shipAddress.phoneNo = shipOrder.phoneNo;
						shipAddress.save(database);
					}
					else
					{
						customer.phoneNo = shipOrder.phoneNo;
						customer.productionSite = shipOrder.productionSite;
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
