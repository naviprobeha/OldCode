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
	/// Summary description for customers.
	/// </summary>
	public class items_sample : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet itemDataSet;
		protected Navipro.SantaMonica.Common.Items items;
		protected DataSet activeFactories;

		protected Hashtable bseTestingList;
		protected Hashtable postMortemList;

		protected DateTime startDate;
		protected DateTime endDate;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Factory currentFactory;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "items_sample.aspx"))
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


			items = new Navipro.SantaMonica.Common.Items();
			itemDataSet = items.getDataSet(database);

			bseTestingList = items.getBseTestings(database, currentFactory.no, startDate, endDate);
			postMortemList = items.getPostMortems(database, currentFactory.no, startDate, endDate);

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
