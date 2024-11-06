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
	public class shipments_print : System.Web.UI.Page
	{

		protected Database database;
		protected Navipro.SantaMonica.Common.ShipmentHeader currentShipmentHeader;
		protected Navipro.SantaMonica.Common.ShipmentHeader shipmentLines;
		protected DataSet shipmentLinesDataSet;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Organization shipmentOrganization;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "shipments.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			currentShipmentHeader = new ShipmentHeader(database, Request["no"].ToString());
	
			shipmentLinesDataSet = currentShipmentHeader.getShipmentLinesDataSet();
			
			Organizations organizations = new Organizations();
			shipmentOrganization = organizations.getOrganization(database, currentShipmentHeader.organizationNo);
			if (shipmentOrganization == null)
			{
				shipmentOrganization = currentOrganization;
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
