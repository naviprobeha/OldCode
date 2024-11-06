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
	/// Summary description for customers_view.
	/// </summary>
	public class shippingCustomers_schedules_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected ShippingCustomer currentShippingCustomer;
		protected ShippingCustomerSchedule currentShippingCustomerSchedule;
		protected string message;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "shippingCustomers.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			ShippingCustomers shippingCustomers = new ShippingCustomers();
			ShippingCustomerSchedules shippingCustomerSchedules = new ShippingCustomerSchedules();

			
			if (Request["shippingCustomerNo"] != null)
			{
				currentShippingCustomer = shippingCustomers.getEntry(database, Request["shippingCustomerNo"]);
			}
			else
			{
				currentShippingCustomer = new ShippingCustomer();
			}
		
			if ((Request["scheduleEntryNo"] != null) && (Request["scheduleEntryNo"] != "") && (Request["scheduleEntryNo"] != "0"))
			{
				currentShippingCustomerSchedule = shippingCustomerSchedules.getEntry(database, Request["shippingCustomerNo"], int.Parse(Request["scheduleEntryNo"]));
			}
			else
			{
				currentShippingCustomerSchedule = new ShippingCustomerSchedule();
			}


			if (Request["command"] == "saveSchedule")
			{
				
				currentShippingCustomerSchedule.shippingCustomerNo = currentShippingCustomer.no;
				currentShippingCustomerSchedule.type = int.Parse(Request["type"]);
				if (Request["mondays"] == "on") currentShippingCustomerSchedule.mondays = true;
				if (Request["tuesdays"] == "on") currentShippingCustomerSchedule.tuesdays = true;
				if (Request["wednesdays"] == "on") currentShippingCustomerSchedule.wednesdays = true;
				if (Request["thursdays"] == "on") currentShippingCustomerSchedule.thursdays = true;
				if (Request["fridays"] == "on") currentShippingCustomerSchedule.fridays = true;
				if (Request["saturdays"] == "on") currentShippingCustomerSchedule.saturdays = true;
				if (Request["sundays"] == "on") currentShippingCustomerSchedule.sundays = true;

				currentShippingCustomerSchedule.week = int.Parse(Request["week"]);
				try
				{
					currentShippingCustomerSchedule.quantity = float.Parse(Request["quantity"]);
				}
				catch(Exception)
				{}

				currentShippingCustomerSchedule.time = new DateTime(1754, 1, 1, int.Parse(Request["timeHour"]), int.Parse(Request["timeMinute"]), 0);

				currentShippingCustomerSchedule.save(database);

				Response.Redirect("shippingCustomers_view.aspx?shippingCustomerNo="+currentShippingCustomer.no);
				
				
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
