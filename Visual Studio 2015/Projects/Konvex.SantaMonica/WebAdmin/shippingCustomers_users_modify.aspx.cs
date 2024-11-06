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
	public class shippingCustomers_users_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected ShippingCustomer currentShippingCustomer;
		protected ShippingCustomerUser currentShippingCustomerUser;
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
			ShippingCustomerUsers shippingCustomerUsers = new ShippingCustomerUsers();

			
			if (Request["shippingCustomerNo"] != null)
			{
				currentShippingCustomer = shippingCustomers.getEntry(database, Request["shippingCustomerNo"]);
			}
			else
			{
				currentShippingCustomer = new ShippingCustomer();
			}
		
			if ((Request["userId"] != null) && (Request["userId"] != ""))
			{
				currentShippingCustomerUser = shippingCustomerUsers.getEntry(database, Request["userId"]);
			}
			else
			{
				currentShippingCustomerUser = new ShippingCustomerUser();
			}


			if (Request["command"] == "saveUser")
			{
				bool saveUser = true;

				if (currentShippingCustomerUser.userId.ToUpper() != Request["newUserId"].ToUpper())
				{
					if (shippingCustomerUsers.userExists(database, Request["newUserId"].ToUpper()))			
					{
						message = "Användarnamn "+Request["newUserId"]+" finns redan.";
						saveUser = false;
					}
				}

				if (saveUser)
				{
					currentShippingCustomerUser.userId = Request["newUserId"].ToUpper();
					currentShippingCustomerUser.shippingCustomerNo = currentShippingCustomer.no;
					currentShippingCustomerUser.name = Request["name"];
					currentShippingCustomerUser.password = Request["password"];

					currentShippingCustomerUser.save(database);

					Response.Redirect("shippingCustomers_view.aspx?shippingCustomerNo="+currentShippingCustomer.no);

				}
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
