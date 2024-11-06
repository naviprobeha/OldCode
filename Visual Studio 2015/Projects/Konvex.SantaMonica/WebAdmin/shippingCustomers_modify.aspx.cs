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
	/// Summary description for shippingCustomers_modify.
	/// </summary>
	public class shippingCustomers_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected ShippingCustomer currentShippingCustomer;
		protected DataSet factoryDataSet;
		protected DataSet reasonDataSet;

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
			Factories factories = new Factories();
			factoryDataSet = factories.getDataSet(database);

			if (Request["shippingCustomerNo"] != null)
			{
				currentShippingCustomer = shippingCustomers.getEntry(database, Request["shippingCustomerNo"]);
			}
			else
			{
				currentShippingCustomer = new ShippingCustomer();
			}

	
			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentShippingCustomer.positionY, currentShippingCustomer.positionX);
			mapServer.setPointMode(currentShippingCustomer.name);
	

			if (Request["command"] == "saveCustomer")
			{

				currentShippingCustomer.name = Request["name"];
				currentShippingCustomer.address = Request["address"];
				currentShippingCustomer.address2 = Request["address2"];
				currentShippingCustomer.postCode = Request["postCode"];
				currentShippingCustomer.city = Request["city"];
				currentShippingCustomer.contactName = Request["contactName"];
				currentShippingCustomer.email = Request["email"];
				currentShippingCustomer.productionSite = Request["productionSite"];
				currentShippingCustomer.registrationNo = Request["registrationNo"];
				currentShippingCustomer.phoneNo = Request["phoneNo"];
				currentShippingCustomer.cellPhoneNo = Request["cellPhoneNo"];
				currentShippingCustomer.faxNo = Request["faxNo"];
				currentShippingCustomer.preferedFactoryNo = Request["preferedFactoryNo"];
				currentShippingCustomer.reasonCode = Request["reasonCode"];

				if (Request["directionComment"].Length > 250)
				{
					currentShippingCustomer.directionComment = Request["directionComment"].Substring(1, 250);
					currentShippingCustomer.directionComment2 = Request["directionComment"].Substring(251);

				}
				else
				{
					currentShippingCustomer.directionComment = Request["directionComment"];
				}

			

				currentShippingCustomer.save(database);

				Response.Redirect("shippingCustomers_view.aspx?shippingCustomerNo="+currentShippingCustomer.no);

			}

			Reasons reasons = new Reasons();
			reasonDataSet = reasons.getDataSet(database);

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
