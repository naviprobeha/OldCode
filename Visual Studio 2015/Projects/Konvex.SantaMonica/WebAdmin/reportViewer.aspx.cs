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
	/// Summary description for reportViewer.
	/// </summary>
	public class reportViewer : System.Web.UI.Page
	{
		protected Organization currentOrganization;
		protected UserOperator currentOperator;

		protected DateTime fromDate;
		protected DateTime toDate;
		protected Database database;

		protected DataSet reportDataSet;
		protected Report currentReport;
	
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

			if (!currentOperator.checkSecurity(database, currentOrganization, "reportViewer.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			DataReports dataReports = new DataReports();
			reportDataSet = dataReports.getDataSet(database);

			//if (Request["reportName"] == "Materialflöde") currentReport = new ReportMaterialOverview();
			//if (Request["reportName"] == "Samtalsstatistik") currentReport = new ReportCallStatistics();
			//if (Request["reportName"] == "Samtal per timme") currentReport = new ReportNumCalls();
			if (Request["reportName"] == null) 
			{
				currentReport = new ReportMaterialOverview();	
			}
			else
			{
				currentReport = dataReports.getReport(Request["reportName"]);
			}

			currentReport.setDatabase(database);

			if (Request["fromDateYear"] != null)
			{
				try
				{
					fromDate = new DateTime(int.Parse(Request["fromDateYear"]), int.Parse(Request["fromDateMonth"]), int.Parse(Request["fromDateDay"]));
				}
				catch(Exception)
				{
					try
					{
						fromDate = new DateTime(int.Parse(Request["fromDateYear"]), int.Parse(Request["fromDateMonth"]), 1);
					}
					catch(Exception)
					{
						fromDate = DateTime.Now;
					}
				}
				currentReport.setParameter("FROM_DATE", fromDate.ToString("yyyy-MM-dd"));
			}

			if (Request["toDateYear"] != null)
			{
				try
				{
					toDate = new DateTime(int.Parse(Request["toDateYear"]), int.Parse(Request["toDateMonth"]), int.Parse(Request["toDateDay"]));
				}
				catch(Exception)
				{
					try
					{
						toDate = new DateTime(int.Parse(Request["toDateYear"]), int.Parse(Request["toDateMonth"]), 1);
					}
					catch(Exception)
					{
						toDate = DateTime.Now;
					}

				}
				currentReport.setParameter("TO_DATE", toDate.ToString("yyyy-MM-dd"));
			}

			fromDate = DateTime.Parse(currentReport.getParameter("FROM_DATE"));
			toDate = DateTime.Parse(currentReport.getParameter("TO_DATE"));
	
			
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
