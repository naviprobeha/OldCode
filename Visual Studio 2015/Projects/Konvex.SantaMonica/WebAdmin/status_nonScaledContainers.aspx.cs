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
	/// Summary description for linejournals.
	/// </summary>
	public class status_nonScaledContainers : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeFactoryDataSet;
		protected DataSet containerDataSet;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected DateTime fromDate;
		protected DateTime toDate;

		protected string factoryNo;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "status.aspx"))
			{
				Response.Redirect("default.htm");				
			}

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


			if (Request["noOfDaysBack"] != null)
			{
				Session["noOfDaysBack"] = Request["noOfDaysBack"];
			}

			fromDate = toDate.AddDays(int.Parse(Session["noOfDaysBack"].ToString())*-1);


			if (Request["factory"] != null)
			{
				factoryNo = Request["factory"];
			}


			OperatorFactories operatorFactories = new OperatorFactories();
			Factories factories = new Factories();
			activeFactoryDataSet = operatorFactories.getDataSet(database, currentOperator.userId);

			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			containerDataSet = lineOrderContainers.getNonScaledContainerDataSet(database, factoryNo, fromDate, toDate);

			int i = 0;
			while (i < containerDataSet.Tables[0].Rows.Count)
			{
				LineOrderContainer lineOrderContainer = new LineOrderContainer(containerDataSet.Tables[0].Rows[i]);
				if (!lineOrderContainers.containesScaledContainer(database, lineOrderContainer.lineOrderEntryNo))
				{
					containerDataSet.Tables[0].Rows.Remove(containerDataSet.Tables[0].Rows[i]);
				}
				else
				{
					i++;
				}
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