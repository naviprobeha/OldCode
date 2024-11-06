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
	/// Summary description for lineorder_view.
	/// </summary>
	public class factoryorders_assign : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected FactoryOrder currentFactoryOrder;
		protected DataSet activeAgents;
		protected DataSet mobileUserDataSet;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "factoryOrders.aspx"))
			{
				Response.Redirect("default.htm");				
			}



			if (Request["factoryOrderNo"] != null)
			{
				FactoryOrders factoryOrders = new FactoryOrders();
				
				currentFactoryOrder = factoryOrders.getEntry(database, Request["factoryOrderNo"]);
				if (currentFactoryOrder == null)
				{
					currentFactoryOrder = new FactoryOrder();
				}
			
			}
			else
			{
				currentFactoryOrder = new FactoryOrder();
			}

			if (Request["command"] == "assign")
			{
				currentFactoryOrder.assignOrder(database, Request["agentCode"]);
				Response.Redirect("factoryOrders.aspx");
			}

			if (Request["command"] == "updateDriver")
			{
				currentFactoryOrder.driverName = Request["loadDriverName"];
				currentFactoryOrder.dropDriverName = Request["dropDriverName"];
				currentFactoryOrder.save(database, false, false);
			}


			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentFactoryOrder.consumerPositionY, currentFactoryOrder.consumerPositionX);
			mapServer.setPointMode(currentFactoryOrder.consumerName);

			Agents agentsClass = new Agents();
			if (currentOrganization.allowLineOrderSupervision)
			{
				activeAgents = agentsClass.getDataSet(database, Agents.TYPE_TANK);
			}
			else
			{
				activeAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_TANK);
			}


			MobileUsers mobileUsers = new MobileUsers();
			if (currentFactoryOrder.organizationNo != "")
			{
				mobileUserDataSet = mobileUsers.getDataSet(database, currentFactoryOrder.organizationNo);
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
