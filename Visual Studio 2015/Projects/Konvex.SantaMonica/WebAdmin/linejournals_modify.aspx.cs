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
	/// Summary description for linejournals_modify.
	/// </summary>
	public class linejournals_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected DataSet factoryDataSet;
		protected DataSet agentDataSet;
		protected DataSet agentStorageGroupDataSet;

		protected LineJournal currentLineJournal;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "linejournals.aspx"))
			{
				Response.Redirect("default.htm");				
			}



			if (Request["lineJournalNo"] != null)
			{
				LineJournals lineJournals = new LineJournals();
				
				currentLineJournal = lineJournals.getEntry(database, Request["lineJournalNo"]);
				if (currentLineJournal == null)
				{
					currentLineJournal = new LineJournal();
				}
			
			}
			else
			{
				currentLineJournal = new LineJournal();
			}

			if (Request["command"] == "saveJournal")
			{
				if (currentLineJournal.agentCode != Request["agentCode"])
				{
					Agents agentClass = new Agents();
					Agent agent = agentClass.getAgent(database, Request["agentCode"]);
					if (agent != null)
					{
						currentLineJournal.removeJournal(database);
						currentLineJournal.organizationNo = agent.organizationNo;
						currentLineJournal.agentCode = agent.code;
						currentLineJournal.agentStorageGroup = agent.agentStorageGroup;
						currentLineJournal.status = 3;
					}
					if (Request["agentCode"] == "")
					{
						currentLineJournal.removeJournal(database);
						currentLineJournal.agentCode = "";
						currentLineJournal.status = 3;
					}
				}

				if ((currentLineJournal.departureFactoryCode != Request["departureFactoryCode"]) || (currentLineJournal.arrivalFactoryCode != Request["arrivalFactoryCode"]))
				{
					currentLineJournal.departureFactoryCode = Request["departureFactoryCode"];
					currentLineJournal.arrivalFactoryCode = Request["arrivalFactoryCode"];
					currentLineJournal.forcedAssignment = true;
					currentLineJournal.status = 1;

				}

				try
				{
					//currentLineJournal.qtyAvailableContainers = int.Parse(Request["qtyAvailableContainers"]);
				}
				catch(Exception ex)
				{
					if (ex.Message != "") {}
				}

		
				currentLineJournal.save(database);
				currentLineJournal.clearScaleStatus(database);

				Response.Redirect("linejournals_view.aspx?lineJournalNo="+currentLineJournal.entryNo);
			}

			Factories factories = new Factories();
			Agents agentsClass = new Agents();
			AgentStorageGroups agentStorageGroups = new AgentStorageGroups();

			factoryDataSet = factories.getDataSet(database);
			agentStorageGroupDataSet = agentStorageGroups.getDataSet(database);

			if (currentOrganization.allowLineOrderSupervision)
			{
				this.agentDataSet = agentsClass.getDataSet(database, Agents.TYPE_LINE);
			}
			else
			{
				this.agentDataSet = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_LINE);
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
