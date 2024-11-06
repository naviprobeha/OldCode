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
	public class reason_lineOrders : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet reasonLineOrders;
		protected LineOrders lineOrders;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected DataSet reasonDataSet;

		protected string reasonCode;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "reason_lineOrders.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			Reasons reasons = new Reasons();
			reasonDataSet = reasons.getDataSet(database);
			reasonCode = reasonDataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

			if ((Request["reasonCode"] != null) && (Request["reasonCode"] != ""))
			{
				reasonCode = Request["reasonCode"];
			}

			if (Request["command"] == "report")
			{
				try
				{
					ReasonReportedLineOrder reasonReportedLineOrder = new ReasonReportedLineOrder();
					reasonReportedLineOrder.reasonCode = reasonCode;
					reasonReportedLineOrder.lineOrderEntryNo = int.Parse(Request["lineOrderEntryNo"]);
					reasonReportedLineOrder.entryDateTime = DateTime.Now;
					reasonReportedLineOrder.operatorNo = currentOperator.userId;
					reasonReportedLineOrder.save(database);
				}
				catch(Exception)
				{}
			}

			lineOrders = new LineOrders();

			reasonLineOrders = lineOrders.getReasonDataSet(database, reasonCode);			

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
