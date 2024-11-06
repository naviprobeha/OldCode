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
	/// Summary description for shippingCustomers_addTransport.
	/// </summary>
	public class shippingCustomers_addTransport : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet organizationDataSet;
		protected DataSet agentDataSet;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected ShippingCustomer currentShippingCustomer;

		protected int selectedType = 0;

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

			if (Request["shippingCustomerNo"] != null)
			{
				currentShippingCustomer = shippingCustomers.getEntry(database, Request["shippingCustomerNo"]);
			}
			else
			{
				currentShippingCustomer = new ShippingCustomer();
			}

			if ((Request["type"] != null) && (Request["type"] != "")) selectedType = int.Parse(Request["type"]);


			if (Request["command"] == "saveTransport")
			{
				ShippingCustomerOrganization shippingCustomerOrganization = new ShippingCustomerOrganization();

				shippingCustomerOrganization.shippingCustomerNo = Request["shippingCustomerNo"];
				shippingCustomerOrganization.type = int.Parse(Request["type"]);
				shippingCustomerOrganization.code = Request["code"];
				shippingCustomerOrganization.sortOrder = int.Parse(Request["sortOrder"]);
				shippingCustomerOrganization.orderType = int.Parse(Request["orderType"]);
				shippingCustomerOrganization.save(database);

				Response.Redirect("shippingCustomers_view.aspx?shippingCustomerNo="+currentShippingCustomer.no);
			}


			Organizations organizations = new Organizations();
			this.organizationDataSet = organizations.getDataSet(database);

			Agents agents = new Agents();
			this.agentDataSet = agents.getDataSet(database, Agents.TYPE_LINE);

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
