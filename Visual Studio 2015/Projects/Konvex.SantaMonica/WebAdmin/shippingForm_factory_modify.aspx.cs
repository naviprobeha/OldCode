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
	/// Summary description for shippingForm_print.
	/// </summary>
	public class shippingForm_factory_modify : System.Web.UI.Page
	{
		protected ShippingCustomer currentShippingCustomer;
		protected Database database;
		protected FactoryOrder currentFactoryOrder;
		protected DataSet consumerDataSet;
		protected DataSet factoryDataSet;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			database = (Database)Session["database"];
			FactoryOrders factoryOrders = new FactoryOrders();

			if ((Request["sid"] != "") || (Request["sid"] != null))
			{
				currentFactoryOrder = factoryOrders.getEntry(database, Request["factoryOrderEntryNo"]);
				
				if (currentFactoryOrder.factoryType == 1)
				{
					ShippingCustomers shippingCustomers = new ShippingCustomers();
					currentShippingCustomer = shippingCustomers.getEntry(database, currentFactoryOrder.factoryNo);
				}
			}
			else
			{
				this.currentShippingCustomer = (ShippingCustomer)Session["current.customer"];
			}

			if (currentShippingCustomer == null) Response.Redirect("default.htm");

			
			Consumers consumers = new Consumers();
			this.consumerDataSet = consumers.getDataSet(database);

			Factories factories = new Factories();
			this.factoryDataSet = factories.getDataSet(database, "KONVEX");

			currentFactoryOrder = factoryOrders.getEntry(database, Request["factoryOrderEntryNo"]);

			if (currentFactoryOrder.factoryNo != currentShippingCustomer.no)
			{
				Response.Redirect("default.htm");
			}

			if (Request["command"] == "saveOrder")
			{
				try
				{

					currentFactoryOrder.phValueFactory = float.Parse(Request["phValueFactory"].Replace(".", ","));
					currentFactoryOrder.quantity = int.Parse(Request["quantity"])/1000;
				}
				catch(Exception)
				{}
				
				if (Request["shipDateYear"] != null) currentFactoryOrder.shipDate = new DateTime(int.Parse(Request["shipDateYear"]), 1, 1, 0,0,0);
				if (Request["shipDateMonth"] != null) currentFactoryOrder.shipDate = currentFactoryOrder.shipDate.AddMonths(int.Parse(Request["shipDateMonth"])-1);
				if (Request["shipDateDay"] != null) currentFactoryOrder.shipDate = currentFactoryOrder.shipDate.AddDays(int.Parse(Request["shipDateDay"])-1);

				currentFactoryOrder.shipTime = new DateTime(1754, 1, 1, int.Parse(Request["shipTimeHour"]), int.Parse(Request["shipTimeMinute"]), 0);
				currentFactoryOrder.comments = Request["comments"];

				currentFactoryOrder.save(database, false, false);
				Response.Redirect("shippingForm_factory_print.aspx?factoryOrderEntryNo="+currentFactoryOrder.entryNo);

			}

			if (Request["command"] == "deleteOrder")
			{
				if (currentFactoryOrder.status == 0)
				{
					currentFactoryOrder.delete(database);
				}

				Response.Redirect("shippingCustomerFactoryOrders.aspx");
			
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
