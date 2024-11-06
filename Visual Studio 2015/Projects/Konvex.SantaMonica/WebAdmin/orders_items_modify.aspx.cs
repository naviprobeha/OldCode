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
	public class orders_items_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected ShipOrderLine currentShipOrderLine;


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


			items = new Navipro.SantaMonica.Common.Items();
			itemDataSet1 = items.getDataSet(database, 1);
			itemDataSet2 = items.getDataSet(database, 2);
			itemDataSet3 = items.getDataSet(database, 3);
			itemDataSet4 = items.getDataSet(database, 4);

			ShipOrders shipOrders = new ShipOrders();
			currentShipOrder = shipOrders.getEntry(database, Request["shipOrderNo"]);

			Navipro.SantaMonica.Common.Customers customers = new Navipro.SantaMonica.Common.Customers();
			currentCustomer = customers.getEntry(database, currentShipOrder.organizationNo, currentShipOrder.customerNo);
			
			ShipOrderLines shipOrderLines = new ShipOrderLines();
			shipOrderLineIds = new ShipOrderLineIds();

			currentShipOrderLine = shipOrderLines.getEntry(database, int.Parse(Request["shipOrderNo"]), int.Parse(Request["entryNo"]));


			if (Request["command"] == "save")
			{
				
				Navipro.SantaMonica.Common.Customers customersClass = new Navipro.SantaMonica.Common.Customers();
				Customer customer = new Customer();
				if (currentShipOrder.customerNo != "") customer = customersClass.getEntry(database, currentShipOrder.organizationNo, currentShipOrder.customerNo);

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


				Item item = items.getEntry(database, Request["itemNo"]);

				currentShipOrderLine.itemNo = item.no;
				currentShipOrderLine.quantity = quantity;

				ItemPriceExtended itemPriceExt = new ItemPriceExtended(database, item, quantity, customer);
				if (itemPriceExt.lineAmount > 0)
				{
					currentShipOrderLine.amount = (float)itemPriceExt.lineAmount;
					currentShipOrderLine.unitPrice = (float)(decimal.Round(itemPriceExt.lineAmount / quantity, 2));
				}
				else
				{
					ItemPrice itemPrice = new ItemPrice(database, item, quantity, customer);
					if (itemPrice.unitPrice > 0)
					{
						currentShipOrderLine.unitPrice = (float)itemPrice.unitPrice;
						currentShipOrderLine.amount = (float)(itemPrice.unitPrice * quantity);
					}
					else
					{
						currentShipOrderLine.unitPrice = (float)item.unitPrice;
						currentShipOrderLine.amount = (float)(item.unitPrice * quantity);
					}
				}

				if (item.connectionItemNo != "")
				{
					Item connectionItem = items.getEntry(database, item.connectionItemNo);

					currentShipOrderLine.connectionItemNo = connectionItem.no;
					currentShipOrderLine.connectionQuantity = connectionQuantity;

					ItemPriceExtended connItemPriceExt = new ItemPriceExtended(database, connectionItem, connectionQuantity, customer);
					if (connItemPriceExt.lineAmount > 0)
					{
						currentShipOrderLine.connectionAmount = (float)(connItemPriceExt.lineAmount);
						currentShipOrderLine.connectionUnitPrice = (float)decimal.Round(connItemPriceExt.lineAmount / connectionQuantity, 2);
					}
					else
					{
						ItemPrice connItemPrice = new ItemPrice(database, connectionItem, connectionQuantity, customer);
						if (connItemPrice.unitPrice > 0)
						{
							currentShipOrderLine.connectionUnitPrice = (float)connItemPrice.unitPrice;
							currentShipOrderLine.connectionAmount = (float)(connItemPrice.unitPrice * connectionQuantity);
						}
						else
						{
							currentShipOrderLine.connectionUnitPrice = (float)item.unitPrice;
							currentShipOrderLine.connectionAmount = (float)(item.unitPrice * connectionQuantity);
						}
					}

				}
				else
				{
					currentShipOrderLine.connectionItemNo = "";
					currentShipOrderLine.connectionQuantity = 0;
					currentShipOrderLine.connectionAmount = 0;
					currentShipOrderLine.connectionUnitPrice = 0;
				}

				currentShipOrderLine.totalAmount = currentShipOrderLine.amount + currentShipOrderLine.connectionAmount;
				currentShipOrderLine.save(database);


				if (item.addStopItem)
				{
					Organizations organizations = new Organizations();
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

				Response.Redirect("orders_items.aspx?shipOrderNo="+currentShipOrder.entryNo);
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
