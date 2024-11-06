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
	/// Summary description for agents.
	/// </summary>
	public class agents : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet singleAgentDataSet;
		protected DataSet lineAgentDataSet;
		protected DataSet tankAgentDataSet;
		protected DateTime startDate;
		protected DateTime endDate;

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


			startDate = DateTime.Today;
			endDate = DateTime.Today;

			if (Request["startDateYear"] != null)
			{
				try
				{
					startDate = new DateTime(int.Parse(Request["startDateYear"]), int.Parse(Request["startDateMonth"]), int.Parse(Request["startDateDay"]));
				}
				catch(Exception g)
				{
					try
					{
						startDate = new DateTime(int.Parse(Request["startDateYear"]), int.Parse(Request["startDateMonth"]), 1);
					}
					catch(Exception f)
					{
						startDate = DateTime.Now;

						if (f.Message != "") {}				
					}

					if (g.Message != "") {}				

				}
			}		
	
			if (Request["endDateYear"] != null)
			{
				try
				{
					endDate = new DateTime(int.Parse(Request["endDateYear"]), int.Parse(Request["endDateMonth"]), int.Parse(Request["endDateDay"]));
				}
				catch(Exception g)
				{
					try
					{
						endDate = new DateTime(int.Parse(Request["endDateYear"]), int.Parse(Request["endDateMonth"]), 1);
					}
					catch(Exception f)
					{
						endDate = DateTime.Now;

						if (f.Message != "") {}				
					}

					if (g.Message != "") {}				
				}
			}		

			if (startDate > endDate) endDate = startDate;


			Agents agentsClass = new Agents();

			if (currentOrganization.allowLineOrderSupervision)
			{
				singleAgentDataSet = agentsClass.getDataSet(database, Agents.TYPE_SINGLE);
				lineAgentDataSet = agentsClass.getDataSet(database, Agents.TYPE_LINE);
				tankAgentDataSet = agentsClass.getDataSet(database, Agents.TYPE_TANK);
			}
			else
			{
				singleAgentDataSet = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_SINGLE);
				lineAgentDataSet = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_LINE);
				tankAgentDataSet = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_TANK);
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
