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
	public class factoryInventory : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeFactories;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Factory currentFactory;

		protected int currentYear;
		protected int currentWeek;
		protected DateTime firstDay;

		protected Hashtable inventoryTable;
		protected Hashtable totalInventoryTable;


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

			if (!currentOperator.checkSecurity(database, currentOrganization, "factoryInventory.aspx"))
			{
				Response.Redirect("default.htm");				
			}

			OperatorFactories operatorFactories = new OperatorFactories();
			Factories factories = new Factories();
			activeFactories = operatorFactories.getDataSet(database, currentOperator.userId);

			if ((Request["factoryNo"] != "") && (Request["factoryNo"] != null))
			{
				currentFactory = factories.getEntry(database, Request["factoryNo"]);
			}

			if (currentFactory == null)
			{
				currentFactory = factories.getEntry(database, activeFactories.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
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

			FactoryInventoryEntries factoryInventoryEntries = new FactoryInventoryEntries();
			inventoryTable = factoryInventoryEntries.getHashtable(database, currentFactory.no, firstDay, firstDay.AddDays(6));

			FactoryInventories factoryInventories = new FactoryInventories();
			totalInventoryTable = factoryInventories.getHashtable(database, currentFactory.no, firstDay, firstDay.AddDays(6));
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
