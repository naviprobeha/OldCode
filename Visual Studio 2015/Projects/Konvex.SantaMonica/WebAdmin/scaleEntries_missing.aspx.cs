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
	public class scaleEntries_missing : System.Web.UI.Page
	{
		protected Database database;
		protected ArrayList missingTransactionList;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected DataSet activeFactories;
		protected Factory currentFactory;

		protected DateTime fromDate;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "scaleEntries.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			fromDate = DateTime.Today;
			
			if (Request["fromDateYear"] != null)
			{
				try
				{
					fromDate = new DateTime(int.Parse(Request["fromDateYear"]), int.Parse(Request["fromDateMonth"]), int.Parse(Request["fromDateDay"]));
				}
				catch(Exception g)
				{
					try
					{
						fromDate = new DateTime(int.Parse(Request["fromDateYear"]), int.Parse(Request["fromDateMonth"]), 1);
					}
					catch(Exception f)
					{
						fromDate = DateTime.Now;

						if (f.Message != "") {}

					}

					if (g.Message != "") {}

				}
			}


			ScaleEntries scaleEntries = new ScaleEntries();

			OperatorFactories operatorFactories = new OperatorFactories();
			Factories factories = new Factories();
			activeFactories = operatorFactories.getDataSet(database, currentOperator.userId);

			missingTransactionList = new ArrayList();

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

			if (Request["command"] == "search")
			{
				DataSet firstDataSet = scaleEntries.getTransactionsFromDate(database, currentFactory.no, fromDate);
				if (firstDataSet.Tables[0].Rows.Count > 0)
				{
					this.missingTransactionList = scaleEntries.findMissingEntries(database, currentFactory.no, int.Parse(firstDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()));
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
