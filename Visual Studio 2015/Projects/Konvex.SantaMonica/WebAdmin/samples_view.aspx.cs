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
	/// Summary description for shipments_view.
	/// </summary>
	public class samples_view : System.Web.UI.Page
	{

		protected Database database;
		protected Navipro.SantaMonica.Common.ShipmentHeader currentShipmentHeader;
		protected Navipro.SantaMonica.Common.ShipmentHeader shipmentLines;
		protected DataSet shipmentLinesDataSet;
		protected Navipro.SantaMonica.Common.ShipmentSending shipmentSending;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "samples.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			ShipmentLineIds shipmentLineIds = new ShipmentLineIds(database);
			ShipmentLineId shipmentLineId = shipmentLineIds.getEntry(database, Request["idEntryNo"]);
			if (shipmentLineId == null) Response.Redirect("shipment_samples.aspx");

			ShipmentSendings shipmentSendings = new ShipmentSendings();		
			shipmentSending = shipmentSendings.getId(database, shipmentLineId.entryNo.ToString());
			if (shipmentSending == null)
			{
				shipmentLineId.setAsSent(database, currentOperator.userId);
				shipmentSending = shipmentSendings.getId(database, shipmentLineId.entryNo.ToString());
			}


			if (shipmentSending == null) Response.End();
			shipmentSending.comments = shipmentSending.comments.Replace((Char)13, '?').Replace((Char)10, '?').Replace("?", "");


			if (Request["command"] == "saveComments")
			{

				shipmentSending.comments = Request["comments"];
				if (shipmentSending.comments.Length > 250) shipmentSending.comments = shipmentSending.comments.Substring(1, 250);
				shipmentSending.save(database);

				Response.Redirect("samples.aspx");

			}

			currentShipmentHeader = new ShipmentHeader(database, shipmentSending.shipmentNo);
			shipmentLinesDataSet = currentShipmentHeader.getShipmentLinesDataSet();

			
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
