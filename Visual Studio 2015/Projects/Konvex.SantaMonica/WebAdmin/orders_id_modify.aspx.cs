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
	public class orders_id_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected Navipro.SantaMonica.Common.Item item;

		protected ShipOrder currentShipOrder;
		protected ShipOrderLine currentShipOrderLine;
		protected ShipOrderLineIds shipOrderLineIds;
		protected ShipOrderLineId currentShipOrderLineId;
		protected Navipro.SantaMonica.Common.Customer currentCustomer;

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



			Navipro.SantaMonica.Common.Items items = new Navipro.SantaMonica.Common.Items();

			ShipOrders shipOrders = new ShipOrders();			
			currentShipOrder = shipOrders.getEntry(database, currentOrganization.no, Request["shipOrderNo"]);
			if (currentOrganization.callCenterMaster) currentShipOrder = shipOrders.getEntry(database,Request["shipOrderNo"]);

			Navipro.SantaMonica.Common.Customers customers = new Navipro.SantaMonica.Common.Customers();
			currentCustomer = customers.getEntry(database, currentShipOrder.organizationNo, currentShipOrder.customerNo);

			ShipOrderLines shipOrderLines = new ShipOrderLines();
			currentShipOrderLine = shipOrderLines.getEntry(database, currentShipOrder.entryNo, int.Parse(Request["shipOrderLineNo"]));

			item = items.getEntry(database, currentShipOrderLine.itemNo);
			shipOrderLineIds = new ShipOrderLineIds();

			currentShipOrderLineId = shipOrderLineIds.getEntry(database, currentShipOrder.entryNo, int.Parse(Request["shipOrderLineNo"]), int.Parse(Request["idNo"]));
	

			if (Request["command"] == "save")
			{
				currentShipOrderLineId.unitId = Request["id"];

				currentShipOrderLineId.bseTesting = false;
				if (Request["bse"] == "on") currentShipOrderLineId.bseTesting = true;

				currentShipOrderLineId.postMortem = false;
				if (Request["obd"] == "on") currentShipOrderLineId.postMortem = true;

				currentShipOrderLineId.save(database);				

				currentShipOrder.updateDetails(database);
				currentShipOrder.save(database);

				Response.Redirect("orders_id.aspx?shipOrderNo="+currentShipOrder.entryNo+"&shipOrderLineNo="+currentShipOrderLineId.shipOrderLineEntryNo);
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
