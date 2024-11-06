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
	public class lineJournals_waittime : System.Web.UI.Page
	{
		protected Database database;
		protected LineJournals lineJournals;
		protected DataSet reportedLineJournals;
		protected DataSet activeFactories;
		protected ArrayList postMortemList;
		protected ArrayList bseTestingList;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Factory currentFactory;

		protected int currentYear;
		protected int currentWeek;
		protected DateTime firstDay;
		protected DateTime lastDay;

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

			currentYear = DateTime.Today.Year;
			currentWeek = CalendarHelper.GetWeek(DateTime.Today);

			if ((Request["firstDay"] != "") && (Request["firstDay"] != null))
			{
				firstDay = DateTime.Parse(Request["firstDay"]);
				currentYear = firstDay.Year;
				currentWeek = CalendarHelper.GetWeek(firstDay);

			}

			if (Request["command"] == "changeDate") 
			{
				currentYear = int.Parse(Request["currentYear"]);
				currentWeek = int.Parse(Request["currentWeek"]);
			}
		
			firstDay = Navipro.SantaMonica.Common.CalendarHelper.GetFirstDayOfWeek(currentYear, currentWeek);
			lastDay = firstDay.AddDays(7);
			

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

	
			lineJournals = new LineJournals();

		
			if (Request["command"] == "confirmWaitTime")
			{
				LineJournal lineJournal = lineJournals.getEntry(database, Request["lineJournalEntryNo"]);
				if (lineJournal != null)
				{
					lineJournal.factoryConfirmedWaitTime = currentOperator.userId;
					lineJournal.save(database);
				}

			}


			reportedLineJournals = lineJournals.getUnConfirmedDataSet(database, currentFactory.no, firstDay, lastDay);

	
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
