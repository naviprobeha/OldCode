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
	/// Summary description for agent_view.
	/// </summary>
	public class agent_view : System.Web.UI.Page
	{
		protected Database database;
		protected Agent currentAgent;
		protected MapServer mapServer;
		protected DateTime containerDate;
		protected int noOfContainerRecords;
		protected DataSet containerEntryDataSet;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "agents.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			this.containerDate = DateTime.Today;
			this.noOfContainerRecords = 20;

			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);

			Agents agentsClass = new Agents();
			currentAgent = agentsClass.getAgent(database, Request["agentCode"]);

			if (!currentOrganization.allowLineOrderSupervision)
			{
				if (currentAgent.organizationNo != currentOrganization.no)
				{
					Response.Redirect("default.htm");
				}
			}

			mapServer.setPosition(currentAgent.positionY, currentAgent.positionX);
			mapServer.setPointMode(currentAgent.code);

			ContainerEntries containerEntries = new ContainerEntries();
			this.containerEntryDataSet = containerEntries.getAgentDataSet(database, currentAgent.code, containerDate, noOfContainerRecords);


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
