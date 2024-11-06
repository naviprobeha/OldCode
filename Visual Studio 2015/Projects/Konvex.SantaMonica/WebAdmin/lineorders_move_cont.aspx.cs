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

namespace WebAdmin
{
	/// <summary>
	/// Summary description for lineorder_view.
	/// </summary>
	public class lineorders_move_cont : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected Navipro.SantaMonica.Common.ShippingCustomers shippingCustomers;
		protected DataSet containersDataSet;
		protected DataSet shipmentDataSet;
		protected Organization lineOrderOrganization;

		protected LineOrder currentLineOrder;
		protected LineOrderContainers lineOrderContainers;

		protected ShippingCustomer currentShippingCustomer;
		protected Containers containersClass;

		protected MapServer mapServer;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "lineorders.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			shippingCustomers = new ShippingCustomers();
			lineOrderContainers = new LineOrderContainers();
			containersClass = new Containers();
			LineOrderShipments lineOrderShipments = new LineOrderShipments();


			if (Request["lineOrderNo"] != null)
			{
				LineOrders lineOrders = new LineOrders();
				
				currentLineOrder = lineOrders.getEntry(database, Request["lineOrderNo"]);
				if (currentLineOrder == null)
				{
					currentLineOrder = new LineOrder();
				}
			
			}
			else
			{
				currentLineOrder = new LineOrder();
			}

			if (currentLineOrder.shippingCustomerNo != "")
			{
				currentShippingCustomer = shippingCustomers.getEntry(database, currentLineOrder.shippingCustomerNo);
			}
			else
			{
				currentShippingCustomer = new ShippingCustomer();
			}


			if (Request["command"] == "moveShipments")
			{
				LineOrders lineOrders = new LineOrders();
				LineOrder newContainerLineOrder = lineOrders.getContainerLineOrder(database, Request["moveToContainerNo"]);
				if (newContainerLineOrder == null)
				{	
					ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, currentLineOrder.shippingCustomerNo);

					newContainerLineOrder = new LineOrder(shippingCustomer);
					newContainerLineOrder.shipDate = DateTime.Today;
					newContainerLineOrder.creationDate = DateTime.Today;
					newContainerLineOrder.createdByType = 1;
					newContainerLineOrder.createdByCode = currentOperator.userId;
					newContainerLineOrder.comments = currentLineOrder.comments;
					newContainerLineOrder.save(database, false);

					LineOrderContainer oldContainer = lineOrderContainers.getEntry(database, currentLineOrder.entryNo, Request["containerNo"]);

					LineOrderContainer lineOrderContainer = new LineOrderContainer(newContainerLineOrder);
					lineOrderContainer.containerNo = Request["moveToContainerNo"];
					lineOrderContainer.categoryCode = oldContainer.categoryCode;
					lineOrderContainer.save(database);	

				}

				shipmentDataSet = lineOrderShipments.getDataSet(database, currentLineOrder.entryNo, Request["containerNo"]);
				int i = 0;
				while (i < shipmentDataSet.Tables[0].Rows.Count)
				{
					LineOrderShipment lineOrderShipment = new LineOrderShipment(database, shipmentDataSet.Tables[0].Rows[i]);


					if (Request["move_"+lineOrderShipment.shipmentNo] == "on")
					{
						lineOrderShipment.move(database, newContainerLineOrder, Request["moveToContainerNo"]);
					}


					i++;
				}

				currentLineOrder.updateWeight(database);
				newContainerLineOrder.updateWeight(database);

				Response.Redirect("lineorders.aspx");
			}


			
			
			containersDataSet = containersClass.getDataSet(database);
			lineOrderOrganization = currentLineOrder.getOrganization(database);

			shipmentDataSet = lineOrderShipments.getDataSet(database, currentLineOrder.entryNo, Request["containerNo"]);

			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentLineOrder.positionY, currentLineOrder.positionX);
			mapServer.setPointMode(currentLineOrder.shippingCustomerName);

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
