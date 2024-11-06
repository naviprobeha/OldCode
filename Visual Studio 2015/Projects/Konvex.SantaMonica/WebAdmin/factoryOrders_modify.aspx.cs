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
	/// Summary description for lineorders_modify.
	/// </summary>
	public class factoryOrders_modify : System.Web.UI.Page
	{
		protected Database database;

		protected UserOperator currentOperator;
		protected Organization currentOrganization;

		protected MapServer mapServer;


		protected FactoryOrder currentFactoryOrder;
		protected DataSet factoryDataSet;
		protected DataSet consumerDataSet;
		protected DataSet categoryDataSet;
		protected DataSet shippingCustomersDataSet;
		protected DataSet organizationDataSet;

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

			if (!currentOperator.checkSecurity(database, currentOrganization, "factoryOrders.aspx"))
			{
				Response.Redirect("default.htm");				
			}


			if (Request["factoryOrderNo"] != null)
			{
				FactoryOrders factoryOrders = new FactoryOrders();
				
				currentFactoryOrder = factoryOrders.getEntry(database, Request["factoryOrderNo"]);
				if (currentFactoryOrder == null)
				{
					currentFactoryOrder = new FactoryOrder();
				}
				
			}
			else
			{
				currentFactoryOrder = new FactoryOrder();
			}
			
			Factories factories = new Factories();
			Consumers consumers = new Consumers();
			ShippingCustomers shippingCustomers = new ShippingCustomers();
			Organizations organizations = new Organizations();


			if (Request["command"] != null)
			{
				if (Request["shipDateYear"] != null) currentFactoryOrder.shipDate = new DateTime(int.Parse(Request["shipDateYear"]), 1, 1, 0,0,0);
				if (Request["shipDateMonth"] != null) currentFactoryOrder.shipDate = currentFactoryOrder.shipDate.AddMonths(int.Parse(Request["shipDateMonth"])-1);
				if (Request["shipDateDay"] != null) currentFactoryOrder.shipDate = currentFactoryOrder.shipDate.AddDays(int.Parse(Request["shipDateDay"])-1);

				if (Request["creationDateYear"] != null) currentFactoryOrder.creationDate = new DateTime(int.Parse(Request["creationDateYear"]), 1, 1, 0,0,0);
				if (Request["creationDateMonth"] != null) currentFactoryOrder.creationDate = currentFactoryOrder.creationDate.AddMonths(int.Parse(Request["creationDateMonth"])-1);
				if (Request["creationDateDay"] != null) currentFactoryOrder.creationDate = currentFactoryOrder.creationDate.AddDays(int.Parse(Request["creationDateDay"])-1);

				if (Request["plannedArrivalDateYear"] != null) currentFactoryOrder.plannedArrivalDateTime = new DateTime(int.Parse(Request["plannedArrivalDateYear"]), 1, 1, 0,0,0);
				if (Request["plannedArrivalDateMonth"] != null) currentFactoryOrder.plannedArrivalDateTime = currentFactoryOrder.plannedArrivalDateTime.AddMonths(int.Parse(Request["plannedArrivalDateMonth"])-1);
				if (Request["plannedArrivalDateDay"] != null) currentFactoryOrder.plannedArrivalDateTime = currentFactoryOrder.plannedArrivalDateTime.AddDays(int.Parse(Request["plannedArrivalDateDay"])-1);
				if (Request["plannedArrivalTimeHour"] != null) currentFactoryOrder.plannedArrivalDateTime = currentFactoryOrder.plannedArrivalDateTime.AddHours(int.Parse(Request["plannedArrivalTimeHour"]));
				if (Request["plannedArrivalTimeMinute"] != null) currentFactoryOrder.plannedArrivalDateTime = currentFactoryOrder.plannedArrivalDateTime.AddMinutes(int.Parse(Request["plannedArrivalTimeMinute"]));


				if (Request["factoryType"] == "0")
				{
												  
					Factory factory = factories.getEntry(database, Request["factoryNo"]);
					if (factory != null)
					{
						currentFactoryOrder.applyFactory(factory);
					}
				}
				if (Request["factoryType"] == "1")
				{
												  
					ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, Request["factoryNo"]);
					if (shippingCustomer != null)
					{
						currentFactoryOrder.applyFactory(shippingCustomer);
					}
				}

				currentFactoryOrder.organizationNo = Request["organizationNo"];

				currentFactoryOrder.factoryName = Request["factoryName"];
				currentFactoryOrder.factoryAddress = Request["factoryAddress"];
				currentFactoryOrder.factoryAddress2 = Request["factoryAddress2"];
				currentFactoryOrder.factoryPostCode = Request["factoryPostCode"];
				currentFactoryOrder.factoryCity = Request["factoryCity"];
				currentFactoryOrder.factoryPhoneNo = Request["factoryPhoneNo"];

				Consumer consumer = consumers.getEntry(database, Request["consumerNo"]);
				if (consumer != null)
				{
					currentFactoryOrder.applyConsumer(consumer);
				}

				currentFactoryOrder.consumerName = Request["consumerName"];
				currentFactoryOrder.consumerAddress = Request["consumerAddress"];
				currentFactoryOrder.consumerAddress2 = Request["consumerAddress2"];
				currentFactoryOrder.consumerPostCode = Request["consumerPostCode"];
				currentFactoryOrder.consumerCity = Request["consumerCity"];
				currentFactoryOrder.consumerPhoneNo = Request["consumerPhoneNo"];
				

				
				if (currentFactoryOrder.createdByCode == "")
				{
					currentFactoryOrder.createdByType = 1;
					currentFactoryOrder.createdByCode = currentOperator.userId;
				}

				currentFactoryOrder.categoryCode = Request["categoryCode"];
				currentFactoryOrder.categoryDescription = Request["categoryDescription"];

				try
				{
					currentFactoryOrder.quantity = float.Parse(Request["quantity"]);
				}
				catch(Exception)
				{}

				try
				{
					currentFactoryOrder.realQuantity = float.Parse(Request["realQuantity"]);
				}
				catch(Exception)
				{}

				int shipTimeHour = 16;
				int shipTimeMinute = 0;
				try
				{
					shipTimeHour = int.Parse(Request["shipTimeHour"]);
				}
				catch(Exception ex) 
				{
					if (ex.Message != "") {}
				}

				try
				{
					shipTimeMinute = int.Parse(Request["shipTimeMinute"]);
				}
				catch(Exception ex) 
				{
					if (ex.Message != "") {}				
				}

				currentFactoryOrder.shipTime = new DateTime(1754, 01, 01, shipTimeHour, shipTimeMinute, 0);

				try
				{
					currentFactoryOrder.phValueFactory = float.Parse(Request["phValueFactory"].Replace(".", ","));
				}
				catch(Exception)
				{}
				try
				{
					currentFactoryOrder.phValueShipping = float.Parse(Request["phValueShipping"].Replace(".", ","));
				}
				catch(Exception)
				{}

				currentFactoryOrder.planningType = int.Parse(Request["planningType"]);
				currentFactoryOrder.agentCleaningComment = Request["agentCleaningComment"];
				currentFactoryOrder.consumerCleaningComment = Request["consumerCleaningComment"];

			}



			if (Request["command"] == "saveOrder")
			{
				currentFactoryOrder.save(database, false);
				Response.Redirect("factoryOrders_view.aspx?factoryOrderNo="+currentFactoryOrder.entryNo);
			}

			if (Request["command"] == "deleteOrder")
			{
				currentFactoryOrder.delete(database);

				Response.Redirect("factoryOrders.aspx");
			}

			factoryDataSet = factories.getDataSet(database);
			consumerDataSet = consumers.getDataSet(database);
			shippingCustomersDataSet = shippingCustomers.getDataSet(database, 1);
			organizationDataSet = organizations.getDataSet(database);
			

			Categories categories = new Categories();
			categoryDataSet = categories.getDataSet(database, true);


			mapServer = new MapServer(database.getConfiguration());
			mapServer.setSession(Session.SessionID, currentOperator.userId, currentOrganization.no);
			mapServer.setPosition(currentFactoryOrder.consumerPositionY, currentFactoryOrder.consumerPositionX);
			mapServer.setPointMode(currentFactoryOrder.consumerName);

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
