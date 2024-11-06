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
	/// Summary description for lineorders_modify.
	/// </summary>
	public class lineorders_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected LineOrder currentLineOrder;

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

				currentLineOrder.createdByType = 1;
				currentLineOrder.createdByCode = currentOperator.userId;
				
			}
			
			if (currentLineOrder.shipTime.Year == 1753) currentLineOrder.shipTime = new DateTime(1754, 01, 01, 16, 0, 0);

			if (Request["command"] != null)
			{
				if (Request["shipDateYear"] != null) currentLineOrder.shipDate = new DateTime(int.Parse(Request["shipDateYear"]), 1, 1, 0,0,0);
				if (Request["shipDateMonth"] != null) currentLineOrder.shipDate = currentLineOrder.shipDate.AddMonths(int.Parse(Request["shipDateMonth"])-1);
				if (Request["shipDateDay"] != null) currentLineOrder.shipDate = currentLineOrder.shipDate.AddDays(int.Parse(Request["shipDateDay"])-1);

				if (Request["creationDateYear"] != null) currentLineOrder.creationDate = new DateTime(int.Parse(Request["creationDateYear"]), 1, 1, 0,0,0);
				if (Request["creationDateMonth"] != null) currentLineOrder.creationDate = currentLineOrder.creationDate.AddMonths(int.Parse(Request["creationDateMonth"])-1);
				if (Request["creationDateDay"] != null) currentLineOrder.creationDate = currentLineOrder.creationDate.AddDays(int.Parse(Request["creationDateDay"])-1);

				currentLineOrder.shippingCustomerName = Request["shippingCustomerName"];
				currentLineOrder.address = Request["address"];
				currentLineOrder.address2 = Request["address2"];
				currentLineOrder.postCode = Request["postCode"];
				currentLineOrder.city = Request["city"];
				
				currentLineOrder.phoneNo = Request["phoneNo"];
				currentLineOrder.cellPhoneNo = Request["cellPhoneNo"];
				currentLineOrder.comments = Request["comments"];

				int shipTimeHour = 16;
				int shipTimeMinute = 0;
				try
				{
					shipTimeHour = int.Parse(Request["shipTimeHour"]);
				}
				catch(Exception ex) 
				{
					if (ex.Message != "") {}
				}

				try
				{
					shipTimeMinute = int.Parse(Request["shipTimeMinute"]);
				}
				catch(Exception ex) 
				{
					if (ex.Message != "") {}				
				}

				currentLineOrder.shipTime = new DateTime(1754, 01, 01, shipTimeHour, shipTimeMinute, 0);
				currentLineOrder.confirmedToDateTime = new DateTime(currentLineOrder.shipDate.Year, currentLineOrder.shipDate.Month, currentLineOrder.shipDate.Day, shipTimeHour, shipTimeMinute, 0);

				if (Request["directionComment"].Length > 200)
				{
					currentLineOrder.directionComment = Request["directionComment"].Substring(0, 200);
					currentLineOrder.directionComment2 = Request["directionComment"].Substring(200);
				}
				else
				{
					currentLineOrder.directionComment = Request["directionComment"];
					currentLineOrder.directionComment2 = "";
				}

			}



			if (Request["command"] == "saveOrder")
			{
				currentLineOrder.save(database, false);
				Response.Redirect("lineorders_view.aspx?lineOrderNo="+currentLineOrder.entryNo);
			}

			if (Request["command"] == "deleteOrder")
			{
				currentLineOrder.deletedByType = 1;
				currentLineOrder.deletedByCode = currentOperator.userId;

				currentLineOrder.delete(database);

				Response.Redirect("lineorders.aspx");
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
