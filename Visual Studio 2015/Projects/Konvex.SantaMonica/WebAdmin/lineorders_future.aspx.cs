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
	/// Summary description for lineorders.
	/// </summary>
	public class lineorders_future : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeLineOrders;
		protected DataSet organizationDataSet;
		protected LineOrders lineOrders;
		protected DateTime shipDate;
		protected Agents agentsClass;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected DataSet activeAgents;

		protected DateTime fromDate;
		protected DateTime toDate;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "lineorders_future.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			shipDate = DateTime.Now;

			fromDate = DateTime.Today;
			toDate = new DateTime(2999, 1, 1);
			
			lineOrders = new LineOrders();

			agentsClass = new Agents();
			Organizations organizationsClass = new Organizations();
			organizationDataSet = organizationsClass.getDataSet(database);

			if (currentOrganization.allowLineOrderSupervision)
			{
				if ((Request["organizationCode"] != "") && (Request["organizationCode"] != null))
				{
					string agentCode = Request["agent"];
					if (agentCode != "")
					{
						Agent agent = agentsClass.getAgent(database, agentCode);
						if (agent.organizationNo != Request["organizationCode"]) agentCode = "";
					}

					activeLineOrders = lineOrders.getActiveOrganizationDataSet(database, Request["organizationCode"], agentCode, fromDate, toDate);			
					activeAgents = agentsClass.getDataSet(database, Request["organizationCode"], Agents.TYPE_LINE);
				}
				else
				{
					activeLineOrders = lineOrders.getActiveDataSet(database, Request["agent"], fromDate, toDate);			
					activeAgents = agentsClass.getDataSet(database, Agents.TYPE_LINE);
				}
			}
			else
			{
				activeLineOrders = lineOrders.getActiveOrganizationDataSet(database, currentOrganization.no, Request["agent"], fromDate, toDate);
				activeAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_LINE);
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
