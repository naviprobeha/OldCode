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
	/// Summary description for shippingCustomers.
	/// </summary>
	public class ShippingCustomers : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet shippingCustomerDataSet;
		protected Navipro.SantaMonica.Common.ShippingCustomers shippingCustomers;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "shippingCustomers.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			shippingCustomers = new Navipro.SantaMonica.Common.ShippingCustomers();

			if (Request["command"] == "searchShippingCustomer")
			{
				shippingCustomers.setSearchCriteria(Request["searchShippingCustomerNo"], Request["searchRegNo"], Request["searchName"], Request["searchCity"], Request["searchProdSite"], Request["searchPhoneNo"]);
			}


			if (Request["command"] == "createOrder")
			{
				ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, Request["shippingCustomerNo"]);

				
				LineOrder lineOrder = new LineOrder();
				lineOrder.type = LineOrders.TYPE_ENTERED;
				lineOrder.applyShippingCustomer(shippingCustomer);

				lineOrder.createdByType = 1;
				lineOrder.createdByCode = currentOperator.userId;


				lineOrder.save(database);

				Response.Redirect("lineorders_modify.aspx?lineOrderNo="+lineOrder.entryNo, true);
			}
	

			shippingCustomerDataSet = shippingCustomers.getDataSet(database);

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
