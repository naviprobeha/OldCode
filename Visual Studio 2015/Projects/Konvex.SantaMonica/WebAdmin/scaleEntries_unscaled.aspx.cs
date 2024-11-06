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
	public class scaleEntries_unscaled : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet lineOrderContainerDataSet;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected DataSet activeFactories;
		protected Factory currentFactory;

		protected bool supervisor;


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

			if (currentOperator.systemRoleCode == "SUPER") this.supervisor = true;

			if (!currentOperator.checkSecurity(database, currentOrganization, "scaleEntries_unscaled.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			LineOrderContainers lineOrderContainers = new LineOrderContainers();


			if (Request["command"] == "deleteEntry")
			{
				if (supervisor)
				{
					LineOrderContainer lineOrderContainer = lineOrderContainers.getEntry(database, int.Parse(Request["entryNo"]));
					lineOrderContainer.isScaled = true;
					lineOrderContainer.save(database);
				}
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

			lineOrderContainerDataSet = lineOrderContainers.getUnScaledDataSet(database, currentFactory.no);
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
