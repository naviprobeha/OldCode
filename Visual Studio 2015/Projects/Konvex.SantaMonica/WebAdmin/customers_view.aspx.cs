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
	public class customers_view : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeAgents;
		protected DataSet organizationMaps;
		protected DataSet customerShipAddressDataSet;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Customer currentCustomer;

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



			if (!currentOperator.checkSecurity(database, currentOrganization, "customers.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			Agents agentsClass = new Agents();
			activeAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_SINGLE);

			Customers customers = new Customers();

			if (Request["customerNo"] != null)
			{
				currentCustomer = customers.getEntry(database, currentOrganization.no, Request["customerNo"]);
				if (currentOrganization.callCenterMaster) currentCustomer = customers.getEntry(database, Request["organizationNo"], Request["customerNo"]);
				if (currentCustomer == null) currentCustomer = new Customer();

			}
			else
			{
				currentCustomer = new Customer();
			}
		

			CustomerShipAddresses custShipAddresses = new CustomerShipAddresses();

			if (Request["command"] == "deleteShipAddress")
			{
				CustomerShipAddress custShipAddress = custShipAddresses.getEntry(database, currentCustomer.organizationNo, currentCustomer.no, Request["customerShipAddressEntryNo"]);				
				custShipAddress.delete(database);
			}

			if (Request["command"] == "clearPosition")
			{
				currentCustomer.positionX = 0;
				currentCustomer.positionY = 0;
				if (currentCustomer.no != "") currentCustomer.save(database);
			}


			customerShipAddressDataSet = custShipAddresses.getDataSet(database, currentCustomer.organizationNo, currentCustomer.no);

			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentCustomer.positionY, currentCustomer.positionX);
			mapServer.setPointMode(currentCustomer.name);

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
