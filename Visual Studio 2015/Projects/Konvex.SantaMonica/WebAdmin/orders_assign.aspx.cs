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
	/// Summary description for orders_assign.
	/// </summary>
	public class orders_assign : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected ShipOrder currentShipOrder;
		protected DataSet shipOrderLogLineDataSet;
		protected Agent lastAgent;
		protected string priorityText;
		protected Customer billToCustomer;

		protected Agents agentsClass;
		protected DataSet activeAgents;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "orders.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			if (Request["shipOrderNo"] != null)
			{
				ShipOrders shipOrders = new ShipOrders();
				
				currentShipOrder = shipOrders.getEntry(database, currentOrganization.no, Request["shipOrderNo"]);
				if (currentOrganization.callCenterMaster) currentShipOrder = shipOrders.getEntry(database,Request["shipOrderNo"]);

				if (currentShipOrder == null)
				{
					currentShipOrder = new ShipOrder(currentOrganization.no);
				}
				
			}
			else
			{
				currentShipOrder = new ShipOrder(currentOrganization.no);
			}

			agentsClass = new Agents();
			activeAgents = agentsClass.getDataSet(database, currentShipOrder.organizationNo, Agents.TYPE_SINGLE);
			ShipOrderLogLines shipOrderLogLines = new ShipOrderLogLines();

			if (currentShipOrder.priority == 0) priorityText = "Normal";
			if (currentShipOrder.priority == 1) priorityText = "Låg";
			if (currentShipOrder.priority == 2) priorityText = "Hög";


			if (Request["command"] == "assignOrder")
			{

				currentShipOrder.assignOrder(database, Request["agentCode"], currentOperator.userId);

				Response.Redirect("orders.aspx");
			}

			if (Request["command"] == "assignOrderToLastAgent")
			{

				currentShipOrder.assignLastAgent(database, currentOperator.userId);

				Response.Redirect("orders.aspx");
			}

			Customers customers = new Customers();
			if (currentShipOrder.billToCustomerNo != "") 
			{
				billToCustomer = customers.getEntry(database, currentOrganization.no, currentShipOrder.billToCustomerNo);
				if (billToCustomer == null) billToCustomer = new Customer();
			}
			else
			{
				billToCustomer = new Customer();
			}

			ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
			lastAgent = shipmentHeaders.getLastAgentForCustomer(database, currentShipOrder.customerNo);
			shipOrderLogLineDataSet = shipOrderLogLines.getDataSet(database, currentShipOrder.entryNo);
			

			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentShipOrder.positionY, currentShipOrder.positionX);
			mapServer.setPointMode(currentShipOrder.customerName);
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
