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
	/// Summary description for _default.
	/// </summary>
	public class scheduled_orders : System.Web.UI.Page
	{
	
		protected Database database;
		protected DataSet scheduledShipOrders;

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


			if (!currentOperator.checkSecurity(database, currentOrganization, "scheduled_orders.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			ScheduledShipOrders shipOrders = new ScheduledShipOrders();
			scheduledShipOrders = shipOrders.getDataSet(database, currentOrganization.no, Request["sorting"]);


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
