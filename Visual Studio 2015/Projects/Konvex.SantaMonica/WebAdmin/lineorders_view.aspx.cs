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
	public class lineorders_view : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected Navipro.SantaMonica.Common.ShippingCustomers shippingCustomers;
		protected DataSet lineOrderContainerDataSet;
		protected DataSet containersDataSet;
		protected DataSet categoryDataSet;
		protected DataSet containerEntryDataSet;
		protected DataSet reasonReportedDataSet;
		protected Organization lineOrderOrganization;

		protected LineOrder currentLineOrder;
		protected LineOrderContainers lineOrderContainers;

		protected Categories categories;
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
			categories = new Categories();
			containersClass = new Containers();

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


			if (Request["command"] == "saveOrder")
			{
				Response.Redirect("lineorders.aspx");
			}

			if (Request["command"] == "deleteContainer")
			{
				LineOrderContainer lineOrderContainer = lineOrderContainers.getEntry(database, currentLineOrder.entryNo, int.Parse(Request["entryNo"]));
				lineOrderContainer.delete(database);
			}

			if (Request["command"] == "addContainer")
			{
				LineOrderContainer lineOrderContainer = new LineOrderContainer(currentLineOrder);
				lineOrderContainer.containerNo = Request["containerNo"];
				lineOrderContainer.categoryCode = Request["categoryCode"];
				
				try
				{
					lineOrderContainer.weight = float.Parse(Request["weight"]);
				}
				catch(Exception e1) 
				{
					if (e1.Message != "") {}
				
				}

				lineOrderContainer.save(database);	
			}

			if (Request["command"] == "toggleAutoPlan")
			{
				if (currentLineOrder.enableAutoPlan) 
					currentLineOrder.enableAutoPlan = false;
				else
					currentLineOrder.enableAutoPlan = true;

				currentLineOrder.save(database, false);
			}

			if (Request["command"] == "loadOrder")
			{
				if (currentOperator.systemRoleCode == "SUPER")
				{
					currentLineOrder.setOrderLoaded(database, 1, currentOperator.userId);
				}
			}
			
			lineOrderContainerDataSet = lineOrderContainers.getDataSet(database, currentLineOrder.entryNo);
			containersDataSet = containersClass.getDataSet(database);
			lineOrderOrganization = currentLineOrder.getOrganization(database);
			categoryDataSet = categories.getDataSet(database);

			Navipro.SantaMonica.Common.ContainerEntries containerEntries = new ContainerEntries();
			containerEntryDataSet = containerEntries.getDocumentDataSet(database, 1, currentLineOrder.entryNo.ToString());

			ReasonReportedLineOrders reasonReportedLineOrders = new ReasonReportedLineOrders();
			reasonReportedDataSet = reasonReportedLineOrders.getDataSet(database, currentLineOrder.entryNo);


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
