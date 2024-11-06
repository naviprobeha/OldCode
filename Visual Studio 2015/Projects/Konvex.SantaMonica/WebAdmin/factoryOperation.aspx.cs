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
	public class factoryOperation : System.Web.UI.Page
	{
		protected Database database;
		protected LineJournals lineJournals;
		protected DataSet activeLineJournals;
		protected DataSet reportedLineJournals;
		protected DataSet activeFactories;
		protected DateTime shipDate;
		protected ArrayList postMortemList;
		protected ArrayList bseTestingList;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Factory currentFactory;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "factoryOperation.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			OperatorFactories operatorFactories = new OperatorFactories();
			Factories factories = new Factories();
			activeFactories = operatorFactories.getDataSet(database, currentOperator.userId);

			if (activeFactories.Tables[0].Rows.Count == 0)
			{
				Response.Redirect("default.htm");
			}

			if ((Request["factory"] != "") && (Request["factory"] != null))
			{
				OperatorFactory operatorFactory = operatorFactories.getEntry(database, currentOperator.userId, Request["factory"]);
				if (operatorFactory != null)
				{
					currentFactory = factories.getEntry(database, operatorFactory.factoryNo);
				}
			}

			if (currentFactory == null)
			{
				currentFactory = factories.getEntry(database, activeFactories.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
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

			if (Request["noOfDaysBack"] != null)
			{
				Session["noOfDaysBack"] = Request["noOfDaysBack"];
			}

			fromDate = toDate.AddDays(int.Parse(Session["noOfDaysBack"].ToString())*-1);



			if (Request["command"] == "reportService")
			{
				Containers containers = new Containers();
				Navipro.SantaMonica.Common.Container container = containers.getEntry(database, Request["containerNo"]);
				if (container != null)
				{
					container.reportService(database, 1, currentOperator.userId, 1, currentFactory.no);
					Response.Redirect("factoryOperation.aspx");
				}
			}


			lineJournals = new LineJournals();

			activeLineJournals = lineJournals.getFactoryDataSet(database, currentFactory.no, fromDate, toDate);
			reportedLineJournals = lineJournals.getReportedDataSet(database, currentFactory.no, fromDate, toDate);

			postMortemList = lineJournals.getPostMortemLineOrders(database, fromDate, toDate);
			bseTestingList = lineJournals.getTestingLineOrders(database, fromDate, toDate);
		
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
