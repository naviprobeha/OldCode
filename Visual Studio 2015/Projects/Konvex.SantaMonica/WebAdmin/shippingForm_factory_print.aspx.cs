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
	public class shippingForm_factory_print : System.Web.UI.Page
	{
		protected ShippingCustomer currentShippingCustomer;
		protected Database database;
		protected FactoryOrder currentFactoryOrder;
		protected DataSet consumerDataSet;
		protected DataSet factoryDataSet;
		protected bool showModify;
		protected string returnPage;
		protected string productionSite;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			database = (Database)Session["database"];
			
			Factories factories = new Factories();
			FactoryOrders factoryOrders = new FactoryOrders();
			ShippingCustomers shippingCustomers = new ShippingCustomers();

			if ((Request["sid"] != "") && (Request["sid"] != null))
			{
				currentFactoryOrder = factoryOrders.getEntry(database, Request["factoryOrderEntryNo"]);
				
				if (currentFactoryOrder.factoryType == 1)
				{
					currentShippingCustomer = shippingCustomers.getEntry(database, currentFactoryOrder.factoryNo);
				}
				if (currentFactoryOrder.factoryType == 0)
				{
					Factory factory = factories.getEntry(database, currentFactoryOrder.factoryNo);
					if (factory != null)
					{
						currentShippingCustomer = new ShippingCustomer();
						currentShippingCustomer.no = factory.no;
						currentShippingCustomer.name = factory.name;
						currentShippingCustomer.address = factory.address;
						currentShippingCustomer.address2 = factory.address2;
						currentShippingCustomer.postCode = factory.postCode;
						currentShippingCustomer.city = factory.city;
						currentShippingCustomer.contactName = factory.contactName;					
					}
				}
			}
			else
			{
				this.currentShippingCustomer = (ShippingCustomer)Session["current.customer"];
			}

			if (currentShippingCustomer == null) Response.Redirect("default.htm");

			

			Consumers consumers = new Consumers();

			this.showModify = true;
			this.returnPage = "shippingCustomerFactoryOrders.aspx";
			if (consumers.checkShippingCustomer(database, currentShippingCustomer.no)) 
			{
				this.showModify = false;
				this.returnPage = "consumerFactoryOrders.aspx";
			}

			this.consumerDataSet = consumers.getDataSet(database);

			this.factoryDataSet = factories.getDataSet(database, "KONVEX");

			currentFactoryOrder = factoryOrders.getEntry(database, Request["factoryOrderEntryNo"]);

			productionSite = "";
			if (currentFactoryOrder.factoryType == 0)
			{
				Factory factory = factories.getEntry(database, currentFactoryOrder.factoryNo);
				if (factory != null)
				{
					productionSite = factory.confirmationIdNo;
				}				
			}
			else
			{
				ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, currentFactoryOrder.factoryNo);
				if (shippingCustomer != null)
				{
					productionSite = shippingCustomer.productionSite;
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
