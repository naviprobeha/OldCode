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
	public class lineorders : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeLineOrders;
		protected DataSet organizationDataSet;
		protected LineOrders lineOrders;
		protected DateTime shipDate;
		protected Agents agentsClass;
		protected ArrayList postMortemList;
		protected ArrayList bseTestingList;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected DataSet activeAgents;

		protected DateTime fromDate;
		protected DateTime toDate;
		protected bool showInfo;

		protected string organizationCode;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "lineorders.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			shipDate = DateTime.Now;

			if (Session["toDate"] == null) Session["toDate"] = DateTime.Now;
			if (Session["noOfDaysBack"] == null) Session["noOfDaysBack"] = "0";
			fromDate = (DateTime)Session["toDate"];
			toDate = (DateTime)Session["toDate"];
			
			if (Request["workDateYear"] != null)
			{
				try
				{
					toDate = new DateTime(int.Parse(Request["workDateYear"]), int.Parse(Request["workDateMonth"]), int.Parse(Request["workDateDay"]));
				}
				catch(Exception g)
				{
					try
					{
						toDate = new DateTime(int.Parse(Request["workDateYear"]), int.Parse(Request["workDateMonth"]), 1);
					}
					catch(Exception f)
					{
						toDate = DateTime.Now;

						if (f.Message != "") {}

					}

					if (g.Message != "") {}

				}
			}

			Session["toDate"] = toDate;
			showInfo = true;
			if (Session["showInfo"] != null) showInfo = (bool)Session["showInfo"];


			if (Request["noOfDaysBack"] != null)
			{
				Session["noOfDaysBack"] = Request["noOfDaysBack"];
			}

			fromDate = toDate.AddDays(int.Parse(Session["noOfDaysBack"].ToString())*-1);


			if (Request["organizationCode"] != null)
			{
				Session.Add("organizationCode", Request["organizationCode"]);
			}

			if (Request["command"] == "toggleInfo")
			{
				if (showInfo)
				{
					showInfo = false;
					Session["showInfo"] = showInfo;
				}
				else
				{
					showInfo = true;
					Session["showInfo"] = showInfo;

				}

			}

			organizationCode = "";
			if (Session["organizationCode"] != null)
			{
				organizationCode = Session["organizationCode"].ToString();
				if (organizationCode == "-")  organizationCode = "";
				
			}

			lineOrders = new LineOrders();

			agentsClass = new Agents();
			Organizations organizationsClass = new Organizations();
			organizationDataSet = organizationsClass.getDataSet(database);

			if (currentOrganization.allowLineOrderSupervision)
			{
				if (organizationCode != "")
				{
					string agentCode = Request["agent"];
					if (agentCode != "")
					{
						Agent agent = agentsClass.getAgent(database, agentCode);
						if (agent != null)
						{
							if (agent.organizationNo != organizationCode) agentCode = "";
						}
					}

					//if (Session["organizationCode"] != null) throw new Exception("Hepp");

					activeLineOrders = lineOrders.getActiveOrganizationDataSet(database, organizationCode, agentCode, fromDate, toDate);			
					activeAgents = agentsClass.getDataSet(database, organizationCode, Agents.TYPE_LINE);
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

			postMortemList = lineOrders.getPostMortemLineOrders(database, fromDate, toDate);
			bseTestingList = lineOrders.getTestingLineOrders(database, fromDate, toDate);
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
