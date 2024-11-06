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
	/// Summary description for shipments.
	/// </summary>
	public class shipments : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeShipments;
		protected DataSet activeAgents;
		protected DataSet activeContainers;
		protected DataSet organizationDataSet;
		protected ShipmentHeaders shipmentsClass;
		protected Agents agentsClass;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected DateTime startDate;
		protected DateTime endDate;

		protected bool showInfo;

		protected string selectedOrganizationNo;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "shipments.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			if (Session["showInfo"] != null) showInfo = (bool)Session["showInfo"];

			startDate = DateTime.Now;
			endDate = DateTime.Now;

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

			Organizations organizations = new Organizations();
			organizationDataSet = organizations.getCallCenterMemberDataSet(database);

			agentsClass = new Agents();
			activeAgents = agentsClass.getDataSet(database, currentOrganization.no, Agents.TYPE_SINGLE);

			shipmentsClass = new ShipmentHeaders();
			activeContainers = shipmentsClass.getContainers(database, currentOrganization.no);

			selectedOrganizationNo = currentOrganization.no;

			if (currentOrganization.callCenterMaster)
			{
				if ((Request["organizationNo"] != null) && (Request["organizationNo"] != ""))
				{
					selectedOrganizationNo = Request["organizationNo"];
				}
				else
				{
					if (organizationDataSet.Tables[0].Rows.Count > 0) selectedOrganizationNo = organizationDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
				}

			}

			activeShipments = shipmentsClass.getDataSet(database, selectedOrganizationNo, Request["agent"], Request["container"], startDate, endDate);

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
