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
	public class customers_shipAddress_modify : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeAgents;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Customer currentCustomer;
		protected CustomerShipAddress currentShipAddress;

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
			CustomerShipAddresses customerShipAddresses = new CustomerShipAddresses();

			if (Request["customerNo"] != null)
			{
				currentCustomer = customers.getEntry(database, currentOrganization.no, Request["customerNo"]);
				if (Request["organizationNo"] != null)
				{
					currentCustomer = customers.getEntry(database, Request["organizationNo"], Request["customerNo"]);
				}

			}
			else
			{
				currentCustomer = new Customer();
			}
		
			if (Request["customerShipAddressNo"] != null)
			{
				currentShipAddress = customerShipAddresses.getEntry(database, currentCustomer.organizationNo, currentCustomer.no, Request["customerShipAddressNo"]);
			}
			else
			{
				currentShipAddress = new CustomerShipAddress();
			}

			currentShipAddress.directionComment = currentShipAddress.directionComment.Replace((Char)13, '?').Replace((Char)10, '?').Replace("?", "");
			currentShipAddress.directionComment2 = currentShipAddress.directionComment2.Replace((Char)13, '?').Replace((Char)10, '?').Replace("?", "");
			

			if (Request["command"] == "saveCustomerAddress")
			{
				
				currentShipAddress.name = Request["name"];
				currentShipAddress.address = Request["address"];
				currentShipAddress.address2 = Request["address2"];
				currentShipAddress.postCode = Request["postCode"];
				currentShipAddress.city = Request["city"];
				currentShipAddress.contactName = Request["contactName"];
				currentShipAddress.phoneNo = Request["phoneNo"];
				currentShipAddress.productionSite = Request["productionSite"];

				if (Request["directionComment"].Length > 250)
				{
					currentShipAddress.directionComment = Request["directionComment"].Substring(1, 250);
					currentShipAddress.directionComment2 = Request["directionComment"].Substring(251);
				}
				else
				{
					currentShipAddress.directionComment = Request["directionComment"];
					currentShipAddress.directionComment2 = "";
				}
				currentShipAddress.save(database);

				
				Response.Redirect("customers_view.aspx?organizationNo="+currentCustomer.organizationNo+"&customerNo="+currentCustomer.no);

			}

			if (Request["command"] == "clearPosition")
			{
				currentShipAddress.positionX = 0;
				currentShipAddress.positionY = 0;
				if (currentShipAddress.entryNo != "") currentShipAddress.save(database);
			}


			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentShipAddress.positionY, currentShipAddress.positionX);
			mapServer.setPointMode(currentShipAddress.name);


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
