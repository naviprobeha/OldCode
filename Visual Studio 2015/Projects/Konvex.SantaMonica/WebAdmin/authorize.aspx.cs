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
	/// Summary description for authorize.
	/// </summary>
	public class authorize : System.Web.UI.Page
	{
		public Database database;


		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			database = (Database)Session["database"];

			Organizations organizations = new Organizations();

			if ((Request["username"] != null) && (Request["password"] != null))
			{
				string userId = Request["username"].ToUpper();
				if (userId.Length > 20) userId = userId.Substring(1, 20);

				string password = Request["password"].ToString();
				if (password.Length > 20) password = password.Substring(1, 20);

				UserOperators userOperators = new UserOperators();
				UserOperator userOperator = userOperators.getOperator(database, userId, password);
			
				if (userOperator != null)
				{
					System.Collections.ArrayList relationList = userOperator.getRelations(database);
					if (relationList.Count > 0)
					{
						Organization organization = organizations.getOrganization(database, ((OrganizationOperator)relationList[0]).organizationNo);
						Session.Add("current.user.operator", userOperator);
						Session.Add("current.user.relations", relationList);
						Session.Add("current.user.organization", organization);

						//Show extended info in order-view.
						Session["showInfo"] = true;

						Response.Redirect(this.getStartPage(userOperator, organization));
						//Response.Write(relationList.Count.ToString());
						//Response.End();
					}

				}
				else
				{
					Navipro.SantaMonica.Common.ShippingCustomerUsers shippingCustomerUsers = new Navipro.SantaMonica.Common.ShippingCustomerUsers();
					Navipro.SantaMonica.Common.ShippingCustomerUser shippingCustomerUser = shippingCustomerUsers.getEntry(database, Request["username"].ToUpper(), Request["password"]);

					if (shippingCustomerUser != null)
					{
						Navipro.SantaMonica.Common.ShippingCustomers shippingCustomers = new Navipro.SantaMonica.Common.ShippingCustomers();
						Navipro.SantaMonica.Common.ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, shippingCustomerUser.shippingCustomerNo);

						if (shippingCustomer != null)
						{
							Session.Add("current.customer", shippingCustomer);
							Session.Add("current.user.shippingCustomerUser", shippingCustomerUser);

							
							ShippingCustomerOrganizations shippingCustomerOrganizations = new ShippingCustomerOrganizations();
							if (shippingCustomerOrganizations.checkType(database, shippingCustomerUser.shippingCustomerNo, 0))
							{
								Response.Redirect("shippingCustomerLineOrders.aspx");
							}
							if (shippingCustomerOrganizations.checkType(database, shippingCustomerUser.shippingCustomerNo, 1))
							{
								Response.Redirect("shippingCustomerFactoryOrders.aspx");
							}

							Consumers consumers = new Consumers();
							if (consumers.checkShippingCustomer(database, shippingCustomerUser.shippingCustomerNo))
							{
								Response.Redirect("consumerFactoryOrders.aspx");
							}

						}
					}

				}



			}
			else
			{
				if (Request["command"] == "changeOrganization")
				{
					if ((Request["index"] != null) && (Request["index"] != ""))
					{
						int index = int.Parse(Request["index"]);
						ArrayList relationList = (ArrayList)Session["current.user.relations"];
						if (relationList != null)
						{
							Organization organization = organizations.getOrganization(database, ((OrganizationOperator)relationList[index]).organizationNo);
							Session["current.user.organization"] = organization;

							Response.Redirect(this.getStartPage((UserOperator)Session["current.user.operator"], organization));
					
						}
					}

				}

				if (Request["command"] == "viewCustomer")
				{
					if ((Request["organizationNo"] != null) && (Request["organizationNo"] != ""))
					{
						Organization organization = organizations.getOrganization(database, Request["organizationNo"]);
						Session["current.user.organization"] = organization;

						Response.Redirect("customers_view.aspx?customerNo="+Request["customerNo"]);
					}

				}

				if (Request["command"] == "createOrder")
				{
					if ((Request["organizationNo"] != null) && (Request["organizationNo"] != ""))
					{
						Organization organization = organizations.getOrganization(database, Request["organizationNo"]);
						Session["current.user.organization"] = organization;

						Response.Redirect("customers.aspx?customerNo="+Request["customerNo"]+"&command=createOrder&customerShipAddressNo=");
					}

				}

			}


			Response.Redirect("default.htm");


		}

		private string getStartPage(UserOperator userOperator, Organization organization)
		{
			RoleMenuItems roleMenuItems = new RoleMenuItems();
			MenuItem menuItem = roleMenuItems.getFirstUserMenuItem(database, organization.no, userOperator.userId);

			if (menuItem != null)
			{
				//Response.Write(menuItem.target);
				//Response.End();
			
				return menuItem.target;
			}
			
			return "default.htm";

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
