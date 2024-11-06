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
	/// Summary description for shippingCustomer_view.
	/// </summary>
	public class shippingCustomer_view : System.Web.UI.Page
	{

		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected ShippingCustomer currentShippingCustomer;
		protected DataSet shippingCustomerOrganizationDataSet;
		protected DataSet shippingCustomerUserDataSet;
		protected DataSet containerDataSet;
		protected DataSet shippingCustomerScheduleDataSet;
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

			if (!currentOperator.checkSecurity(database, currentOrganization, "shippingCustomers.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			ShippingCustomers shippingCustomers = new ShippingCustomers();
			ShippingCustomerOrganizations shippingCustomerOrganizations = new ShippingCustomerOrganizations();

			if (Request["shippingCustomerNo"] != null)
			{
				currentShippingCustomer = shippingCustomers.getEntry(database, Request["shippingCustomerNo"]);
			}
			else
			{
				currentShippingCustomer = new ShippingCustomer();
			}
		
			if (Request["command"] == "deleteTransport")
			{
				ShippingCustomerOrganization shippingCustomerOrganization = shippingCustomerOrganizations.getShippingCustomerOrganization(database, currentShippingCustomer.no, int.Parse(Request["orderType"]), int.Parse(Request["type"]), Request["code"]);

				shippingCustomerOrganization.delete(database);
			}

			if (Request["command"] == "deleteUser")
			{
				ShippingCustomerUsers shippingCustomerUsers = new ShippingCustomerUsers();
				ShippingCustomerUser shippingCustomerUser = shippingCustomerUsers.getEntry(database, Request["userId"]);

				shippingCustomerUser.delete(database);
			}

			if (Request["command"] == "deleteSchedule")
			{
				ShippingCustomerSchedules shippingCustomerSchedules = new ShippingCustomerSchedules();
				ShippingCustomerSchedule shippingCustomerSchedule = shippingCustomerSchedules.getEntry(database, currentShippingCustomer.no, int.Parse(Request["scheduleEntryNo"]));

				shippingCustomerSchedule.delete(database);
			}

			if (Request["command"] == "clearPosition")
			{
				currentShippingCustomer.positionX = 0;
				currentShippingCustomer.positionY = 0;
				if (currentShippingCustomer.no != "") currentShippingCustomer.save(database);
			}

			this.shippingCustomerOrganizationDataSet = shippingCustomerOrganizations.getShippingCustomerDataSet(database, currentShippingCustomer.no);
	
			Containers containers = new Containers();
			containerDataSet = containers.getFullLocationDataSet(database, 1, currentShippingCustomer.no);

			this.shippingCustomerUserDataSet = currentShippingCustomer.getUsers(database);
			this.shippingCustomerScheduleDataSet = currentShippingCustomer.getSchedules(database);
		
			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentShippingCustomer.positionY, currentShippingCustomer.positionX);
			mapServer.setPointMode(currentShippingCustomer.name);
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
