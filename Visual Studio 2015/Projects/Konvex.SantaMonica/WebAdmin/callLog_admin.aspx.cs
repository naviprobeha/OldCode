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
	public class callLog_admin : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet phoneLogDataSet;
		protected DateTime currentDate;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected DataSet callLogUserOperatorDataSet;

		protected DateTime fromDate;
		protected DateTime toDate;

		protected string currentCallLogOperator;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "callLog_admin.aspx"))
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

			currentCallLogOperator = Request["currentCallLogOperator"];
			if ((currentCallLogOperator == "") || (currentCallLogOperator == null)) currentCallLogOperator = currentOperator.userId;

			

			UserOperators userOperators = new UserOperators();
			this.callLogUserOperatorDataSet = userOperators.getPhoneOperators(database);

			PhoneLogEntries phoneLogEntries = new PhoneLogEntries();
			
			if ((Request["callType"] == "") || (Request["callType"] == null))
			{
				phoneLogDataSet = phoneLogEntries.getDataSet(database, currentCallLogOperator, fromDate, toDate);
			}
			else
			{
				phoneLogDataSet = phoneLogEntries.getDataSet(database, currentCallLogOperator, fromDate, toDate, int.Parse(Request["callType"]));
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
