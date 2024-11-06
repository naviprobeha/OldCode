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
	/// Summary description for linejournals_view.
	/// </summary>
	public class factoryOperation_container : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected DataSet shipmentLineDataSet;

		protected LineOrder currentLineOrder;
		protected ShipmentSendings shipmentSendings;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "factoryOperation.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			shipmentSendings = new ShipmentSendings();

			if ((Request["containerNo"] != null) && (Request["containerNo"] != "") && (Request["lineOrderNo"] != null) && (Request["lineOrderNo"] != ""))
			{
				LineOrders lineOrders = new LineOrders();
				
				currentLineOrder = lineOrders.getEntry(database, Request["lineOrderNo"]);
				if (currentLineOrder == null)
				{
					currentLineOrder = new LineOrder();
				}

				LineOrderShipments lineOrderShipments = new LineOrderShipments();
                shipmentLineDataSet = lineOrderShipments.getContentDataSet(database, currentLineOrder.entryNo, Request["containerNo"]);

			}
			else
			{
				Response.Redirect("default.htm");				
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
