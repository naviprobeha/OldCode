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
	public class consumerInventoryView : System.Web.UI.Page
	{
		protected Database database;
		protected DataSet activeConsumers;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;
		protected Consumer currentConsumer;
		protected ConsumerCapacities consumerCapacities;
		protected FactoryOrders factoryOrders;

		protected int currentYear;
		protected int currentWeek;
		protected DateTime firstDay;

		protected Hashtable inventoryTable;
		protected Hashtable capacityTable;

		protected ShippingCustomer currentShippingCustomer;



		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Session["current.customer"] == null)
			{
				Response.Redirect("default.htm");				
			}

			database = (Database)Session["database"];
			Consumers consumers = new Consumers();

			currentShippingCustomer = (ShippingCustomer)Session["current.customer"];

			currentConsumer = consumers.getFromShippingCustomer(database, currentShippingCustomer.no);
			if (currentConsumer == null)				
			{
				Response.Redirect("default.htm");				
			}


			this.activeConsumers = consumers.getDataSet(database);



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

			ConsumerInventories consumerInventories = new ConsumerInventories();
			inventoryTable = consumerInventories.getHashtable(database, currentConsumer.no, firstDay, firstDay.AddDays(6));
			
			consumerCapacities = new ConsumerCapacities();
			factoryOrders = new FactoryOrders();
			capacityTable = consumerCapacities.getHashtable(database, currentConsumer.no, firstDay, firstDay.AddDays(6));

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
