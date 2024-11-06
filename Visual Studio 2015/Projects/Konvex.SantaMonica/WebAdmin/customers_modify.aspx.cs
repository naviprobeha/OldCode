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
	public class customers_modify : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeAgents;
		protected DataSet organizationMaps;
		protected DataSet customerShipAddressDataSet;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Customer currentCustomer;
		protected MapServer mapServer;
		protected string productionSiteErrorMessage = "";

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

			if (!currentOrganization.callCenterMaster)
			{
				if (currentOrganization.no != Request["organizationNo"]) Response.End();
			}


			Agents agentsClass = new Agents();
			activeAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_SINGLE);

			Customers customers = new Customers();

			if (Request["customerNo"] != null)
			{
				currentCustomer = customers.getEntry(database, Request["organizationNo"], Request["customerNo"]);
			}
			else
			{
				currentCustomer = new Customer();
			}

			currentCustomer.directionComment = currentCustomer.directionComment.Replace((Char)13, '?').Replace((Char)10, '?').Replace("?", "");
			currentCustomer.directionComment2 = currentCustomer.directionComment2.Replace((Char)13, '?').Replace((Char)10, '?').Replace("?", "");

			if (Request["command"] == "saveCustomer")
			{

				if (Request["directionComment"].Length > 250)
				{
					currentCustomer.directionComment = Request["directionComment"].Substring(1, 250);
					currentCustomer.directionComment2 = Request["directionComment"].Substring(251);

				}
				else
				{
					currentCustomer.directionComment = Request["directionComment"];
					currentCustomer.directionComment2 = "";
				}

				currentCustomer.name = Request["name"];
				currentCustomer.address = Request["address"];
				currentCustomer.address2 = Request["address2"];
				currentCustomer.postCode = Request["postCode"];
				currentCustomer.city = Request["city"];
				currentCustomer.phoneNo = Request["phoneNo"];
				currentCustomer.productionSite = Request["productionSite"];
				//currentCustomer.registrationNo = Request["registrationNo"];
				currentCustomer.cellPhoneNo = Request["cellPhoneNo"];
				currentCustomer.dairyCode = Request["dairyCode"];
				currentCustomer.dairyNo = Request["dairyNo"];

				if (currentCustomer.productionSite != "")
				{
					Customer customer = customers.getProductionSiteCustomer(database, currentCustomer.no, currentCustomer.productionSite);
					if (customer != null)
					{	
						productionSiteErrorMessage = "Produktionsplatsnummret finns redan på kund "+customer.no+" ("+customer.organizationNo+"), "+customer.name+".";
					}
				}
				
				if (productionSiteErrorMessage == "")
				{
					currentCustomer.setUpdated();

					currentCustomer.save(database);

					Response.Redirect("customers_view.aspx?customerNo="+currentCustomer.no+"&organizationNo="+currentCustomer.organizationNo);

				}
			}

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
