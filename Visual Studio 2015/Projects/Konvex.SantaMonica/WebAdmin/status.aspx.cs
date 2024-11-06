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
	public class status : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeFactoryDataSet;
		protected DataSet organizationDataSet;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Factory currentFactory;

		protected int currentYear;
		protected int currentWeek;
		protected DateTime firstDay;


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

			OperatorFactories operatorFactories = new OperatorFactories();
			Factories factories = new Factories();
			activeFactoryDataSet = operatorFactories.getDataSet(database, currentOperator.userId);

			Organizations organizations = new Organizations();
			organizationDataSet = organizations.getDataSet(database);

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
		}


		protected string getNonFinishedJournals(string organizationNo)
		{
			LineJournals lineJournals = new LineJournals();
			DataSet activeLineJournalDataSet = lineJournals.getActiveDataSet(database, "", organizationNo, firstDay, firstDay.AddDays(6));
			if (activeLineJournalDataSet.Tables[0].Rows.Count > 0) return "<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\"><tr><td class=\"jobDescription\" style=\"color: red;\">"+activeLineJournalDataSet.Tables[0].Rows.Count.ToString()+"</td><td><a href=\"linejournals.aspx?organizationCode="+organizationNo+"&workDateYear="+firstDay.AddDays(6).ToString("yyyy")+"&workDateMonth="+firstDay.AddDays(6).Month+"&workDateDay="+firstDay.AddDays(6).Day+"&noOfDaysBack=6\"><img src=\"images/button_assist.gif\" border=\"0\"></a></td></tr></table>";
			return "";

		}

		protected string getNonLoadedOrders(string organizationNo)
		{
			LineOrders lineOrders = new LineOrders();
			DataSet nonLoadedOrderDataSet = lineOrders.getNonLoadedDataSet(database, organizationNo, firstDay, firstDay.AddDays(6));
			if (nonLoadedOrderDataSet.Tables[0].Rows.Count > 0) return "<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\"><tr><td class=\"jobDescription\" style=\"color: red;\">"+nonLoadedOrderDataSet.Tables[0].Rows.Count.ToString()+"</td><td><a href=\"lineorders.aspx?organizationCode="+organizationNo+"&workDateYear="+firstDay.AddDays(6).ToString("yyyy")+"&workDateMonth="+firstDay.AddDays(6).Month+"&workDateDay="+firstDay.AddDays(6).Day+"&noOfDaysBack=6\"><img src=\"images/button_assist.gif\" border=\"0\"></a></td></tr></table>";
			return "";

		}

		protected string getNonLoadedContainers(string organizationNo)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet nonLoadedContainerDataSet = lineOrderContainers.getNonLoadedContainerDataSet(database, organizationNo, firstDay, firstDay.AddDays(6));
			if (nonLoadedContainerDataSet.Tables[0].Rows.Count > 0) return "<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\"><tr><td class=\"jobDescription\" style=\"color: red;\">"+nonLoadedContainerDataSet.Tables[0].Rows.Count.ToString()+"</td><td><a href=\"status_nonLoadedContainers.aspx?organizationCode="+organizationNo+"&workDateYear="+firstDay.AddDays(6).ToString("yyyy")+"&workDateMonth="+firstDay.AddDays(6).Month+"&workDateDay="+firstDay.AddDays(6).Day+"&noOfDaysBack=6&type=0\"><img src=\"images/button_assist.gif\" border=\"0\"></a></td></tr></table>";
			return "";

		}

		protected string getNonUnLoadedContainers(string organizationNo)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet nonUnLoadedContainerDataSet = lineOrderContainers.getNonUnLoadedContainerDataSet(database, organizationNo, firstDay, firstDay.AddDays(6));
			if (nonUnLoadedContainerDataSet.Tables[0].Rows.Count > 0) return "<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\"><tr><td class=\"jobDescription\" style=\"color: red;\">"+nonUnLoadedContainerDataSet.Tables[0].Rows.Count.ToString()+"</td><td><a href=\"status_nonLoadedContainers.aspx?organizationCode="+organizationNo+"&workDateYear="+firstDay.AddDays(6).ToString("yyyy")+"&workDateMonth="+firstDay.AddDays(6).Month+"&workDateDay="+firstDay.AddDays(6).Day+"&noOfDaysBack=6&type=1\"><img src=\"images/button_assist.gif\" border=\"0\"></a></td></tr></table>";
			return "";

		}

		protected string getNonScaledContainers(string factoryNo)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet nonScaledContainerDataSet = lineOrderContainers.getNonScaledContainerDataSet(database, factoryNo, firstDay, firstDay.AddDays(6));

			int i = 0;
			while (i < nonScaledContainerDataSet.Tables[0].Rows.Count)
			{
				LineOrderContainer lineOrderContainer = new LineOrderContainer(nonScaledContainerDataSet.Tables[0].Rows[i]);
				if (!lineOrderContainers.containesScaledContainer(database, lineOrderContainer.lineOrderEntryNo))
				{
					nonScaledContainerDataSet.Tables[0].Rows.Remove(nonScaledContainerDataSet.Tables[0].Rows[i]);
				}
				else
				{
					i++;
				}
			}

			if (nonScaledContainerDataSet.Tables[0].Rows.Count > 0) return "<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\"><tr><td class=\"jobDescription\" style=\"color: red;\">"+nonScaledContainerDataSet.Tables[0].Rows.Count.ToString()+"</td><td><a href=\"status_nonScaledContainers.aspx?factory="+factoryNo+"&workDateYear="+firstDay.AddDays(6).ToString("yyyy")+"&workDateMonth="+firstDay.AddDays(6).Month+"&workDateDay="+firstDay.AddDays(6).Day+"&noOfDaysBack=6\"><img src=\"images/button_assist.gif\" border=\"0\"></a></td></tr></table>";
			return "";

		}

		protected string getUnScaledContainers(string factoryNo)
		{
			LineOrderContainers lineOrderContainers = new LineOrderContainers();
			DataSet unScaledContainerDataSet = lineOrderContainers.getUnScaledDataSet(database, factoryNo, firstDay, firstDay.AddDays(6));

			if (unScaledContainerDataSet.Tables[0].Rows.Count > 0) return "<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\"><tr><td class=\"jobDescription\" style=\"color: red;\">"+unScaledContainerDataSet.Tables[0].Rows.Count.ToString()+"</td><td><a href=\"status_unScaledContainers.aspx?factory="+factoryNo+"&workDateYear="+firstDay.AddDays(6).ToString("yyyy")+"&workDateMonth="+firstDay.AddDays(6).Month+"&workDateDay="+firstDay.AddDays(6).Day+"&noOfDaysBack=6\"><img src=\"images/button_assist.gif\" border=\"0\"></a></td></tr></table>";
			return "";

		}

		protected string getNonLoadedLineOrders(string organizationNo)
		{
			LineOrders lineOrders = new LineOrders();
			DataSet nonLoadedLineOrderDataSet = lineOrders.getNotLoadedActiveDataSet(database, organizationNo, DateTime.Today.AddDays(-7));

			if (nonLoadedLineOrderDataSet.Tables[0].Rows.Count > 0) return "<table cellspacing=\"0\" cellpadding=\"2\" border=\"0\"><tr><td class=\"jobDescription\" style=\"color: red;\">"+nonLoadedLineOrderDataSet.Tables[0].Rows.Count.ToString()+"</td><td><a href=\"lineorders.aspx?organizationCode="+organizationNo+"\"><img src=\"images/button_assist.gif\" border=\"0\"></a></td></tr></table>";
			return "";

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
